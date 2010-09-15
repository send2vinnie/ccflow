using System;
using System.Text;
using System.IO;

namespace HelpSoft
{
    /// <summary>
    /// MyParser 的摘要说明。
    /// </summary>
    public class DataReader
    {
        /// <summary>
        /// 为了防止分隔符和文件字段属性分在两个数据块中处理，每次需预留重复处理的字段数
        /// </summary>
        public const int splitLength = 100;
        Encoding m_encoding;
        byte[] m_boundary;
        byte[] m_crlf;
        byte[] m_constName; //文件字段标志符
        private bool m_finished = true;//表示上一个文件已处理完，下次需重新读取文件标志
        /// <summary>
        /// 当前正在上传的文件信息
        /// </summary>
        private UpFileInfo _fileInfo;
        private FileStream fs;

        /// <summary>
        /// 以下变量用以重写请求头，在重写请求头时，将文件内容数据去除
        /// </summary>
        private int m_stepLength = 4096;
        private byte[] m_rewriteRequestData;
        private int m_rewriteIndex = 0;

      

        public DataReader(Encoding encoding, byte[] boundary)
        {
            this.m_encoding = encoding;
            this.m_crlf = this.m_encoding.GetBytes("\r\n");
            this.m_constName = this.m_encoding.GetBytes("filename=");
            m_boundary = boundary;
            m_rewriteRequestData = new byte[m_stepLength];
        }
        /// <summary>
        /// 从字节数组中，读取上传编号,此上传编号应在读取数据的第一个块中，
        /// 若在第一个块中没找到，暂认为页面没有使用本上传模块
        /// </summary>
        /// <param name="buffer">上传的数据块</param>
        /// <param name="startindex">开始查找的位置</param>
        /// <param name="MaxLength">最大有效长度</param>
        /// <returns>若找到，则返回编号</returns>
        public string GetUploadId(byte[] buffer, int startindex, int maxLength)
        {
            byte[] signData = ConcatArray(m_boundary, m_crlf, m_encoding.GetBytes("Content-Disposition: form-data; name=\"UploadID\""), m_crlf, m_crlf);


            int pos = DestTag(buffer, signData, startindex, maxLength);
            if (pos < 0)
                return string.Empty;
            int pos2 = DestTag(buffer, m_boundary, pos + signData.Length, maxLength);
            if (pos2 < 0)
                return string.Empty;
            return m_encoding.GetString(buffer, pos + signData.Length, pos2 - pos - signData.Length - 2); //因为在分隔符前有一个回车换行符，所在-2
        }
        /// <summary>
        /// 获取文件上传信息
        /// </summary>
        /// <param name="buffer">数据块</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="maxLength">最大有效长度</param>
        /// <returns>如找到文件信息，则返回文件信息，否则，文件信息的所有字段为空</returns>
        public UpFileInfo GetFileInfo(byte[] buffer, int startIndex, int maxLength)
        {
            UpFileInfo lfileInfo = new UpFileInfo();
            while (startIndex < maxLength)
            {
                int splitPos = DestTag(buffer, m_boundary, startIndex, maxLength); //读取分隔符
                if (splitPos < startIndex)
                    break;

                int lineEndPos = DestTag(buffer, m_crlf, splitPos + m_boundary.Length + 2, maxLength);
                if (lineEndPos < splitPos + m_boundary.Length)
                    break;

                int signFilePos = DestTag(buffer, this.m_constName, splitPos + m_boundary.Length + 2, lineEndPos);
                if (signFilePos > splitPos + m_boundary.Length - 1) //找到文件标志示“fileName="”
                {
                    string Content_Disposition = this.m_encoding.GetString(buffer, splitPos + m_boundary.Length + 2, lineEndPos - splitPos - m_boundary.Length - 2).ToLower();
                    int nextLineEnd = DestTag(buffer, ConcatArray(m_crlf, m_crlf), lineEndPos + 2, maxLength);
                    string Content_Type = this.m_encoding.GetString(buffer, lineEndPos + 2, nextLineEnd - lineEndPos - 2).ToLower();

                    string[] Disp = Content_Disposition.Split(new char[] { '\"' });
                    lfileInfo.FileControl = Disp[1];
                    lfileInfo.PathFileName = Disp[3];
                    string[] FileType = Content_Type.Split(new char[] { ':' });
                    lfileInfo.FileType = FileType[1];
                    lfileInfo.startIndex = nextLineEnd + 4;

                    lfileInfo.endIndex = DestTag(buffer, m_boundary, lfileInfo.startIndex, maxLength);
                    if (lfileInfo.endIndex > 0) lfileInfo.endIndex -= 2; //去掉数据区后的回车换行符
                    break;
                }
                startIndex = lineEndPos;
            }
            return lfileInfo;
        }
        /// <summary>
        /// 分析数据，若是文件，则写入文件，在上传上下文件记录参数
        /// </summary>
        /// <remarks>
        /// 算法说明：若上一个文件已结束，一、则查找新文件标志符，没找到则退出，找到又分两种情况：1、文件在当前块内结束，则写文件，关闭文件，回到开始继续查找文件标志，
        /// 2、文件在当前块内没结束，则写文件，并退出处理过程
        /// 二、上一个文件未结束，则找文件结束标志，然后又分两种情况：1、找到：当前文件结束，写文件，关闭文件，回到开始，继续分析处理数据，
        /// 2、没找到，则文件还没结束，则写文件，退出处理过程
        /// </remarks>
        /// <param name="buffer">数据块</param>
        /// <param name="startIndex">有效数据起始索引</param>
        /// <param name="maxLength">有效数据结束索引</param>
        /// <param name="uploadContext">文件上传处理上下文</param>
        /// <returns>应预留数据长度，防止边界（如文件说明，内容分隔符）位于两个数据块中，若不能确定预留长，则预留
        /// 全局常量splitLength所指示的长度</returns>
        public int ReadData(byte[] buffer, int startIndex, int maxLength, UploadContext uploadContext)
        {
            while (true)
            {
                if (m_finished)
                {
                    _fileInfo = GetFileInfo(buffer, startIndex, maxLength);
                    if ((_fileInfo.PathFileName == null) || (_fileInfo.PathFileName.Trim() == ""))//没有找到文件标志
                    {
                        RewriteRequest(buffer, startIndex, maxLength - startIndex - splitLength);
                        return splitLength;
                    }
                    uploadContext.FileConIds = ArrayAddItem(uploadContext.FileConIds, _fileInfo.FileControl);
                    uploadContext.FileNames = ArrayAddItem(uploadContext.FileNames, _fileInfo.PathFileName);

                    RewriteRequest(buffer, startIndex, _fileInfo.startIndex - startIndex + 10);
                    fs = File.Create(uploadContext.TmepFileDir + uploadContext.GUID + uploadContext.FileNames.Length.ToString() + GetFileName(_fileInfo.PathFileName));
                    if (_fileInfo.endIndex > -1)
                    {
                        m_finished = true;
                        fs.Write(buffer, _fileInfo.startIndex, _fileInfo.endIndex - _fileInfo.startIndex);
                        fs.Close();
                        startIndex = _fileInfo.endIndex;
                    }
                    else
                    {
                        m_finished = false;
                        if (maxLength - _fileInfo.startIndex > splitLength)
                        {
                            fs.Write(buffer, _fileInfo.startIndex, maxLength - _fileInfo.startIndex - splitLength);
                            return splitLength;
                        }
                        else
                        {
                            return maxLength - _fileInfo.startIndex; //防止将标志写入了文件
                        }
                    }
                }
                else
                {
                    _fileInfo.endIndex = DestTag(buffer, m_boundary, startIndex, maxLength);
                    if (_fileInfo.endIndex > -1) //已结束
                    {
                        _fileInfo.endIndex -= 2; //去掉数据区后的回车换行符
                        m_finished = true;
                        fs.Write(buffer, startIndex, _fileInfo.endIndex - startIndex);
                        fs.Close();
                        startIndex = _fileInfo.endIndex;
                    }
                    else
                    {
                        m_finished = false; //当前数据块中，文件体未结束
                        if (maxLength - startIndex > splitLength)
                        {
                            fs.Write(buffer, startIndex, maxLength - startIndex - splitLength);
                            return splitLength;
                        }
                        else
                        {
                            return maxLength - startIndex;
                        }

                    }
                }
            }
        }
        /// <summary>
        /// 在指定的数组中检索短数组
        /// </summary>
        /// <param name="buffer">要检索的大数组</param>
        /// <param name="tag">待检索的小字数组</param>
        /// <param name="startIndex">检完索的启始索引</param>
        /// <param name="maxLength">大数组的有效长度</param>
        /// <returns>如检索到，则返回启始索引，否则返回-1</returns>
        public int DestTag(byte[] buffer, byte[] tag, int startIndex, int maxLength)
        {
            bool temp = true;
            int endIndex = maxLength - tag.Length;
            while (startIndex < maxLength - tag.Length)
            {
                temp = true;
                int pos = Array.IndexOf(buffer, tag[0], startIndex, endIndex - startIndex);
                if (pos < 0)
                {
                    return -1;
                }
                for (int j = 0; j < tag.Length; j++) //匹配所有字节
                {
                    if (buffer[pos + j] != tag[j])
                    {
                        if (pos > startIndex)
                            startIndex = pos;
                        else
                            startIndex++;
                        temp = false;
                        break;
                    }
                }
                if (temp == true)
                {
                    return pos;
                }

            }
            return -1;
        }

        public int GetLine(byte[] buffer, int startIndex)
        {
            int pos = this.DestTag(buffer, this.m_crlf, startIndex, buffer.Length);
            return pos;
        }
        /// <summary>
        /// 联接字节数组
        /// </summary>
        /// <param name="first">第一个字节数组</param>
        /// <param name="second">第二个字节数组</param>
        /// <returns>联接后的新数组</returns>
        public static byte[] ConcatArray(byte[] first, params byte[][] second)
        {
            int length = first.Length;
            foreach (byte[] data in second)
            {
                length += data.Length;
            }
            byte[] ret = new byte[length];
            first.CopyTo(ret, 0);
            int i = 0;
            int index = first.Length;
            while (i < second.Length)
            {
                second[i].CopyTo(ret, index);
                index += second[i].Length;
                i++;
            }
            return ret;

        }

        /// <summary>
        /// 获取重写请求头的内容
        /// </summary>
        /// <returns></returns>
        public byte[] GetRewriteRequest(byte[] lastData, int startIndex, int length)
        {
            RewriteRequest(lastData, startIndex, length);
            if (m_rewriteIndex < m_rewriteRequestData.Length)
            {
                byte[] tempData = new byte[m_rewriteIndex];
                Array.Copy(m_rewriteRequestData, 0, tempData, 0, m_rewriteIndex);
                return tempData;
            }
            else
                return m_rewriteRequestData;
        }
        /// <summary>
        /// 从文件路径和文件名中，提取文件名
        /// </summary>
        /// <param name="PathFileName">包含路径的全文件名</param>
        /// <returns>不包含路径的文件名</returns>
        private string GetFileName(string PathFileName)
        {
            int index = PathFileName.LastIndexOf("\\");
            return PathFileName.Substring(index + 1, PathFileName.Length - index - 1);
        }
        /// <summary>
        /// 在字符串数组中，新增一个元素
        /// </summary>
        /// <param name="arr">字符串数组</param>
        /// <param name="str">需要新增的字符串</param>
        public string[] ArrayAddItem(string[] arr, string str)
        {
            string[] temp;
            if (arr != null)
            {
                temp = new string[arr.Length + 1];
                Array.Copy(arr, 0, temp, 0, arr.Length);
                temp[temp.Length - 1] = str;
            }
            else
            {
                temp = new string[1];
                temp[0] = str;
            }
            return temp;
        }
        /// <summary>
        /// 将请求的内容，除文件内容数据，重写到一个数据缓存区，以便重写请求头
        /// </summary>
        /// <param name="buffer">数据块</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="length">需要写入的长度</param>
        private void RewriteRequest(byte[] buffer, int startIndex, int length)
        {
            if (length < 1) return;
            if (m_rewriteRequestData.Length - m_rewriteIndex < length) //缓冲区不能容纳新内容，需扩展缓冲区，这里认为一次扩展即足够
            {
                byte[] newData = new byte[m_rewriteRequestData.Length + m_stepLength + length];
                Array.Copy(m_rewriteRequestData, 0, newData, 0, m_rewriteIndex);
                m_rewriteRequestData = newData;
            }
            Array.Copy(buffer, startIndex, m_rewriteRequestData, m_rewriteIndex, length);
            m_rewriteIndex += length;
        }
        /// <summary>
        /// 删除临时文件
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (fs != null)
            {
                fs.Close();
            }
  
        }
    }
    /// <summary>
    /// 上传的文件信息
    /// </summary>
    public struct UpFileInfo
    {
        /// <summary>
        /// 上传的文件控件名称
        /// </summary>
        public string FileControl;
        /// <summary>
        /// 上传的文件的客户端名称和路径
        /// </summary>
        public string PathFileName;
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType;
        /// <summary>
        /// 文件数据的启始索引
        /// </summary>
        public int startIndex;
        /// <summary>
        /// 文件数据结束索引，若当前块没有结束，则返回-1
        /// </summary>
        public int endIndex;
    }
}
