using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;

namespace Tax666.Common
{
    public class FileOption
    {
        #region 获取最大可上传文件大小(返回KB大小)
        /// <summary>
        /// 获取最大可上传文件大小(返回KB大小)。
        /// </summary>
        /// <returns></returns>
        public static double GetMaxRequestLength()
        {
            double maxLength = 0;
            string sPath = typeof(String).Assembly.Location;
            sPath = Path.GetDirectoryName(sPath);
            sPath = Path.Combine(sPath, "CONFIG\\machine.config");
            XmlDocument doc = new XmlDocument();
            doc.Load(sPath);
            maxLength = Convert.ToDouble(doc.SelectSingleNode("configuration/system.web/httpRuntime/@maxRequestLength").Value);

            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/system.web/httpRuntime/@maxRequestLength");
            if (node != null)
            {
                double length = Convert.ToDouble(node.Value);
                if (length < maxLength)
                    maxLength = length;
            }

            return maxLength;
        }
        #endregion

        #region 上传文件操作(也可用于无特殊要求的图片文件上传)
        /// <summary>
        /// 上传文件操作(也可用于无特殊要求的图片文件上传)。
        /// </summary>
        /// <param name="upfile"></param>
        /// <param name="fileType">允许上传的文件格式：如rar,exe,pdf,doc,xls</param>
        /// <param name="fileMaxSize">允许上传的文件最大值，单位：KB</param>
        /// <param name="filePath">文件保存的相对路径。格式：Upload/UserPic/UserUpload/</param>
        /// <param name="resFileTypeID">获取扩展名的ID</param>
        /// <returns>string的顺序：1－成功；0－失败|结果描述|文件扩展名|文件大小|原文件名称|新文件名称(全路径)</returns>
        public static string[] UpLoadFile(System.Web.HttpPostedFile upfile, string fileType, int fileMaxSize, string filePath, string resFileTypeID)
        {
            string resultVale = null;

            if (upfile.FileName != "" || upfile.FileName != string.Empty)
            {
                string f_Name = upfile.FileName.Substring(upfile.FileName.LastIndexOf("\\") + 1);
                string f_Extent = upfile.FileName.Substring(upfile.FileName.LastIndexOf(".") + 1).ToLower();
                int f_Size = upfile.ContentLength;

                //判断文件类型是否满足fileType的要求；
                string[] f_fileType = fileType.Split(new char[] { ',' });
                bool isAllow = false;
                for (int i = 0; i < f_fileType.Length; i++)
                {
                    if (f_Extent == f_fileType[i])
                        isAllow = true;
                }

                if (f_Size <= fileMaxSize * 1024)
                {
                    if (isAllow)
                    {
                        //拼合新的文件名称；
                        string f_NewName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + f_Extent;
                        string f_savePath = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\") + f_NewName;
                        string returnPath = filePath + f_NewName;

                        //获取扩展名的ID；
                        //string resFileTypeID;
                        //ResFileTypeData resData = (new ResFileTypeFacade()).GetResFileTypeByExt("." + f_Extent);
                        //if (resData.Tables[ResFileTypeData.ResFileType_Table].Rows.Count > 0)
                        //    resFileTypeID = resData.Tables[ResFileTypeData.ResFileType_Table].Rows[0][ResFileTypeData.ResFileTypeID_Field].ToString();
                        //else
                        //resFileTypeID = "10";		//其它未知格式；

                        upfile.SaveAs(f_savePath);
                        resultVale = "1|文件上传成功！|" + resFileTypeID + "|" + f_Size.ToString() + "|" + f_Name + "|" + returnPath;
                    }
                    else
                    {
                        resultVale = "0|上传文件类型不满足要求！允许上传格式：" + fileType;
                    }
                }
                else
                {
                    resultVale = "0|上传文件大小超出限制范围！";
                }
            }
            else
            {
                resultVale = "0|上传文件为空！";
            }

            return resultVale.Split(new char[] { '|' });
        }
        #endregion

        #region 图片类文件上传操作(包括：gif、jpg、jpeg、png、bmp五种类型)
        /// <summary>
        /// 图片类文件上传操作(包括：gif、jpg、jpeg、png、bmp五种类型)。
        /// </summary>
        /// <remarks>
        /// 缩放文件命名规则和存放规则：
        ///		文件名：原始文件yyyyMMddHHmmss_Ori；缩放文件yyyyMMddHHmmss_Res
        ///		原始文件存放在：OriginImages目录；缩放文件存放在：ZoomImages目录；
        /// </remarks>
        /// <param name="upfile"></param>
        /// <param name="fileMaxSize">允许上传的文件最大值，单位：KB</param>
        /// <param name="filePath">文件保存的相对路径。格式：Upload/UserPic/UserUpload/</param>
        /// <param name="isZoom">是否进行图片的缩放操作；</param>
        /// <param name="resWidth">缩放的最大宽度，0－无限制；</param>
        /// <param name="resHeight">缩放的最大高度，0－无限制；</param>
        /// <returns>string的顺序：1－成功；0－失败|结果描述|文件扩展名|文件大小|原始文件名称(全路径)|缩放文件名称(全路径)|原始图片的Width|原始图片的Height|缩放后图片的Width|缩放后图片的Height</returns>
        public static string[] UpLoadImgFile(System.Web.HttpPostedFile upfile, int fileMaxSize, string filePath, bool isZoom, int resWidth, int resHeight)
        {
            string resultVale = null;
            if (upfile.FileName != "" || upfile.FileName != string.Empty)
            {
                string f_Name = upfile.FileName.Substring(upfile.FileName.LastIndexOf("\\") + 1);
                string f_Extent = upfile.FileName.Substring(upfile.FileName.LastIndexOf(".") + 1).ToLower();
                int f_Size = upfile.ContentLength;

                //判断文件类型是否满足fileType的要求；
                string fileType = "gif,jpg,jpeg,png,bmp";
                string[] f_fileType = fileType.Split(new char[] { ',' });
                bool isAllow = false;
                for (int i = 0; i < f_fileType.Length; i++)
                {
                    if (f_Extent == f_fileType[i])
                        isAllow = true;
                }

                if (f_Size <= fileMaxSize * 1024)
                {
                    if (isAllow)
                    {
                        System.Drawing.Image oriImg, resImg;
                        oriImg = System.Drawing.Image.FromStream(upfile.InputStream);
                        int oriImageW = oriImg.Width;
                        int oriImageH = oriImg.Height;

                        //判断目录是否存在，如果不存在，则创建；
                        string oriSaveDir = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\") + "OriginImages\\";
                        string oriRetDir = filePath + "OriginImages/";
                        string resSaveDir = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\") + "ZoomImages\\";
                        string resRetDir = filePath + "ZoomImages/";

                        try
                        {
                            DirectoryInfo dirOriInfo = new DirectoryInfo(oriSaveDir);
                            if (!dirOriInfo.Exists)
                            {
                                dirOriInfo.Create();
                            }
                        }
                        catch (Exception)
                        {
                            oriSaveDir = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\");
                            oriRetDir = filePath;
                        }

                        try
                        {
                            DirectoryInfo resOriInfo = new DirectoryInfo(resSaveDir);
                            if (!resOriInfo.Exists)
                            {
                                resOriInfo.Create();
                            }
                        }
                        catch (Exception)
                        {
                            resSaveDir = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\");
                            resRetDir = filePath;
                        }

                        //产生一个随机数用于新命名的图片
                        Random ro = new Random();
                        string stro = ro.Next(100, 100000000).ToString();
                        string newName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;

                        //拼合新的文件名称；
                        string f_savePathOri = oriSaveDir + newName + "_Ori." + f_Extent;
                        string f_savePathRes = resSaveDir + newName + "_Res." + f_Extent;
                        string returnPathOri = oriRetDir + newName + "_Ori." + f_Extent;
                        string returnPathRes = resRetDir + newName + "_Res." + f_Extent;

                        if (isZoom)
                        {
                            int imageResizeWidth = resWidth;
                            int imageResizeHeight = resHeight;

                            if (imageResizeHeight == 0 && imageResizeWidth > 0)
                            {
                                //
                                //以宽度为缩放标准；
                                //
                                if (oriImg.Width <= imageResizeWidth)
                                {
                                    imageResizeHeight = oriImg.Height;
                                    imageResizeWidth = oriImg.Width;
                                }
                                else
                                {
                                    imageResizeHeight = oriImg.Height * imageResizeWidth / oriImg.Width;
                                }
                            }
                            if (imageResizeHeight > 0 && imageResizeWidth == 0)
                            {
                                //
                                //以高度为缩放标准；
                                //
                                if (oriImg.Height <= imageResizeHeight)
                                {
                                    imageResizeHeight = oriImg.Height;
                                    imageResizeWidth = oriImg.Width;
                                }
                                else
                                {
                                    imageResizeWidth = oriImg.Width * imageResizeHeight / oriImg.Height;
                                }
                            }
                            if (imageResizeHeight > 0 && imageResizeWidth > 0)
                            {
                                //
                                //以高度和宽度为缩放标准，取缩放比大的为基准；	
                                //
                                if (oriImg.Width <= imageResizeWidth)
                                {
                                    if (oriImg.Height <= imageResizeHeight)
                                    {
                                        //保持图片的原来大小；
                                        imageResizeHeight = oriImg.Height;
                                        imageResizeWidth = oriImg.Width;
                                    }
                                    else
                                    {
                                        //按imageResizeHeight的高度缩放宽度，imageResizeHeight高度不变；
                                        imageResizeWidth = oriImg.Width * imageResizeHeight / oriImg.Height;
                                    }
                                }
                                else
                                {
                                    if (oriImg.Height <= imageResizeHeight)
                                    {
                                        //按imageResizeWidth的高度缩放高度，imageResizeWidth宽度不变；
                                        imageResizeHeight = oriImg.Height * imageResizeWidth / oriImg.Width;
                                    }
                                    else
                                    {
                                        //同时进行缩放，按缩放比大的为缩放准则；
                                        if (imageResizeWidth / oriImg.Width > imageResizeHeight / oriImg.Height)
                                        {
                                            imageResizeWidth = oriImg.Width * imageResizeWidth / oriImg.Width;
                                            imageResizeHeight = oriImg.Height * imageResizeHeight / oriImg.Height;
                                        }
                                        else
                                        {
                                            imageResizeWidth = oriImg.Width * imageResizeHeight / oriImg.Height;
                                            imageResizeHeight = oriImg.Height * imageResizeHeight / oriImg.Height;
                                        }
                                    }
                                }
                            }

                            if (oriImg.Width <= imageResizeWidth && oriImg.Height <= imageResizeHeight)
                            {
                                //如果原尺寸都小于缩放尺寸，则不进行缩放操作（缩放后图片的质量损失很大）
                                resImg = System.Drawing.Image.FromStream(upfile.InputStream);
                                resImg.Save(f_savePathRes);
                            }
                            else
                            {
                                resImg = oriImg.GetThumbnailImage(imageResizeWidth, imageResizeHeight, null, new System.IntPtr(0));
                                switch (f_Extent)
                                {
                                    case "jpg":
                                        resImg.Save(f_savePathRes, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        break;
                                    case "jpeg":
                                        resImg.Save(f_savePathRes, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        break;
                                    case "gif":
                                        resImg.Save(f_savePathRes);
                                        break;
                                    case "png":
                                        resImg.Save(f_savePathRes, System.Drawing.Imaging.ImageFormat.Png);
                                        break;
                                    case "bmp":
                                        resImg.Save(f_savePathRes, System.Drawing.Imaging.ImageFormat.Bmp);
                                        break;
                                }
                            }

                            //resImg.Save(f_savePathRes);
                            oriImg.Save(f_savePathOri);
                            resImg.Dispose();
                            oriImg.Dispose();
                            resultVale = "1|文件上传成功！|" + f_Extent + "|" + f_Size.ToString() + "|" + returnPathOri + "|" + returnPathRes + "|" + oriImageW.ToString() + "|" + oriImageH.ToString() + "|" + imageResizeWidth.ToString() + "|" + imageResizeHeight.ToString();
                        }
                        else
                        {
                            //不进行图片的缩放；
                            oriImg.Save(f_savePathOri);
                            oriImg.Dispose();
                            resultVale = "1|文件上传成功！|" + f_Extent + "|" + f_Size.ToString() + "|" + returnPathOri + "|" + returnPathRes + "|" + oriImageW.ToString() + "|" + oriImageH.ToString() + "|0|0";
                        }
                    }
                    else
                    {
                        resultVale = "0|上传文件类型不满足要求！允许上传格式：gif,jpg,jpeg,png,bmp。";
                    }
                }
                else
                {
                    resultVale = "0|上传文件大小超出限制范围！";
                }
            }
            else
            {
                resultVale = "0|上传文件为空！";
            }

            return resultVale.Split(new char[] { '|' });
        }
        #endregion

        #region 删除文件操作
        /// <summary>
        /// 删除文件操作。
        /// </summary>
        /// <param name="filePath">文件对应的虚拟路径，格式：Upload/Images/xxxx.jpg。</param>
        /// <returns>1-成功；0－失败；</returns>
        public static string[] DelFile(string filePath)
        {
            string resultValue = null;
            if (filePath == null)
            {
                return "0|文件不存在！".Split(new char[] { '|' });
            }
            else
            {
                string fn = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\");
                string f_Name = fn.Substring(fn.LastIndexOf("\\") + 1);

                FileInfo fi = new FileInfo(fn);
                if (fi.Exists)
                {
                    try
                    {
                        fi.Delete();
                        resultValue = "1|" + f_Name + "文件成功删除！";
                    }
                    catch (Exception)
                    {
                        resultValue = "0|" + f_Name + "文件删除失败！";
                    }
                }
                else
                {
                    resultValue = "0|" + f_Name + "文件不存在！";
                }

                return resultValue.Split(new char[] { '|' });
            }
        }
        #endregion

        #region 返回文件是否存在
        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件对应的虚拟路径，格式：Upload/Images/xxxx.jpg</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filePath)
        {
            bool retVal = false;

            string fn = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\");

            FileInfo fi = new FileInfo(fn);
            if (fi.Exists)
            {
                retVal = true;
            }
            return retVal;
        }
        #endregion

        #region 创建指定的路径的目录
        /// <summary>
        /// 创建指定的路径的目录；
        /// </summary>
        /// <param name="pathName">虚拟路径，格式：Upload/Images/</param>
        /// <returns></returns>
        public static bool CreateDirPath(string pathName)
        {
            bool retVal = false;
            string dictory = HttpContext.Current.Request.PhysicalApplicationPath + pathName.Replace("/", "\\");
            DirectoryInfo di = new DirectoryInfo(dictory);

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                    retVal = true;
                }
                catch
                {
                    retVal = false;
                }
            }
            else
                retVal = true;

            return retVal;
        }
        #endregion

        #region 创建指定文件及其文件的内容
        /// <summary>
        /// 创建指定文件及其文件的内容。
        /// </summary>
        /// <param name="filePathName">相对的虚拟路径，如：Upload/ImportDataFiles/20080921130848.xml</param>
        /// <param name="fileContStr">输出文件的内容</param>
        /// <param name="isRewrite">是否重写该文件</param>
        /// <returns></returns>
        public static bool CompileFile(string filePathName, string fileContStr, bool isRewrite)
        {
            bool retVal = false;

            string pathfile = HttpContext.Current.Request.PhysicalApplicationPath + filePathName.Replace("/", "\\");
            FileInfo fi = new FileInfo(pathfile);
            if (fi.Exists && (isRewrite == false))
            {
                retVal = false;
            }
            else
            {
                try
                {
                    StreamWriter sw = File.CreateText(pathfile);
                    sw.WriteLine(fileContStr);

                    sw.Flush();
                    sw.Close();
                    retVal = true;
                }
                catch
                {
                    //new Terminator().ThrowError(ex.Message);
                    retVal = false;
                }
            }

            return retVal;
        }
        #endregion

        #region 获取指定文件的内容（HTML和TEXT等文本文件）
        /// <summary>
        /// 获取指定文件的内容（HTML和TEXT等文本文件）。
        /// </summary>
        /// <param name="filePath">相对的虚拟路径，如：Upload/ImportDataFiles/20080921130848.txt</param>
        /// <returns></returns>
        public static String GetFileContent(string filePath)
        {
            String content = null;
            string pathfile = HttpContext.Current.Request.PhysicalApplicationPath + filePath.Replace("/", "\\");

            FileStream fs = new FileStream(pathfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.ASCIIEncoding.GetEncoding("GB2312"));

            try
            {
                String line = sr.ReadLine();
                while (line != null)
                {
                    content += line;
                    content += "\n";
                    line = sr.ReadLine();
                }
            }
            catch
            {
                content = "";
            }

            sr.Close();
            fs.Close();

            return content;
        }
        #endregion

        #region 以指定的ContentType输出指定文件文件(文件下载)
        /// <summary>
        /// 以指定的ContentType输出指定文件文件(文件下载)
        /// FileOption.ResponseFile(WebRequests.GetMapPath("Upload/编码转换.zip"), "CodeConvert.zip", "zip");
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 缓冲区为10k
            byte[] buffer = new Byte[10000];

            // 文件长度
            int length;

            // 需要读的数据长度
            long dataToRead;

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


                // 需要读的数据长度
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + CodeUtil.UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 判断文件名是否为浏览器可以直接显示的图片文件名
        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }
        #endregion

        #region 备份文件/恢复文件
        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }
        #endregion

        #region 方法 -- 创建新的文件名
        /// <summary>
        /// 创建新的文件名
        /// </summary>
        /// <returns></returns>
        public string CreateFileName()
        {
            string guid = System.Guid.NewGuid().ToString().ToLower();
            guid = guid.Replace("-", "");

            return DateTime.Now.ToString("yyyyMMddhhmmss") + guid.Substring(0, 4);
        }
        #endregion
    }
}
