using System;
using System.Web;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Text;
using System.Globalization;

namespace HelpSoft
{
    /// <summary>
    /// 大文件上传处理模块
    /// </summary>
    public class BigFileUploadModuleHandle : System.Web.IHttpModule
    {
        public BigFileUploadModuleHandle()
        {

        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get
            {
                return "BigFileUploadModuleHandle";
            }
        }
        /// <summary>
        ///  初始化事件处理程序
        /// </summary>
        /// <param name="context">应用程序对象</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }
        public void Dispose()
        {

        }
        private void context_BeginRequest(object sender, EventArgs e)
        {
            
            HttpApplication app = sender as HttpApplication;
            HttpWorkerRequest request = GetWorkerRequest(app.Context);

            if (!IsUploadRequest(app.Request)) return; //不是文件上传，则退出
            

            string sContentType = app.Request.ContentType.ToLower();
            byte[] arrBoundary = GetMultipartBoundary(sContentType);
            int ContentLength = app.Request.ContentLength; //信息体的总长度

            DataReader dataReader = new DataReader(app.Context.Request.ContentEncoding, arrBoundary);
            DateTime startDate = DateTime.Now;
            byte[] arrBuffer = request.GetPreloadedEntityBody();
            //StreamWriter tempFile = new StreamWriter(@"C:\myupload\ALl.txt");
            if (arrBuffer == null)
            {
                arrBuffer = new Byte[0];
                //tempFile.Close();
                return; //没有读取到信息体
            }
            else
            {
                string mGuid = dataReader.GetUploadId(arrBuffer, 0, arrBuffer.Length);
                UploadContext upload = (UploadContext)HttpContext.Current.Cache[mGuid];
                //没有读取到上传上下文，所以认为用户没初始化上传上下文，也可能是过期（一天时间），
                //因此页面不能获取文件，所以用户不需要使用此上传处理模块
                if (upload == null)
                {
                    return;
                }

                upload.Status = uploadStatus.Uploading;
                upload.StartReadDateTime = startDate;

                upload.TotalLength = app.Request.ContentLength;
                upload.Readedlength = arrBuffer.Length;
                int goBackLeng = DataReader.splitLength; //后退长度

                //tempFile.WriteLine("StartRead:" + DateTime.Now.ToString());
                goBackLeng = dataReader.ReadData(arrBuffer, 0, arrBuffer.Length, upload);
                //tempFile.Write(System.Text.Encoding.ASCII.GetString(arrBuffer,0, arrBuffer.Length ));
                
                int bulkSize = 10240; // 分块大小
                byte[] Buffer = new byte[bulkSize];
                byte[] TempBuffer = new byte[bulkSize - DataReader.splitLength];
                int bytesRead;
                int maxLength = 0;

                Array.Copy(arrBuffer, arrBuffer.Length - goBackLeng, Buffer, 0, goBackLeng);

                bool hasMoreData = false;
                int maxReadlen = 0;

                while (request.IsClientConnected() && !request.IsEntireEntityBodyIsPreloaded() &&
                    upload.Readedlength < upload.TotalLength)
                {
                    hasMoreData = true;
                    //tempFile.WriteLine("ReadData:" + DateTime.Now.ToString());
                    maxReadlen = upload.TotalLength - upload.Readedlength;
                    maxReadlen = maxReadlen > TempBuffer.Length ? TempBuffer.Length : maxReadlen;

                    bytesRead = request.ReadEntityBody(TempBuffer, 0, maxReadlen);
                    //tempFile.Write(System.Text.Encoding.ASCII.GetString(TempBuffer,0, bytesRead ));
                    //tempFile.Write(DateTime.Now.ToString());
                    upload.Readedlength += bytesRead;

                    Array.Copy(TempBuffer, 0, Buffer, goBackLeng, bytesRead);
                    maxLength = bytesRead + goBackLeng;
                    //tempFile.WriteLine("ReadEnd :" + DateTime.Now.ToString());
                    goBackLeng = dataReader.ReadData(Buffer, 0, bytesRead + goBackLeng, upload);

                    Array.Copy(Buffer, maxLength - goBackLeng, Buffer, 0, goBackLeng);
                    Thread.Sleep(1);
                    if (upload.Abort)
                    {
                        dataReader.Dispose(true);
                        upload.Status = uploadStatus.UserCancel;
                        upload.Dispose(true);
                        request.CloseConnection();
                        return;
                    }
                }
                
                if (!request.IsClientConnected())
                {
                    dataReader.Dispose(true);
                    upload.Status = uploadStatus.HasError;
                    upload.Dispose(true);
                }
                else
                {
                    //StreamWriter temp2File = new StreamWriter(@"C:\myupload\After.txt");
                    
                    if (hasMoreData)
                    {
                        byte[] data = dataReader.GetRewriteRequest(Buffer, maxLength - goBackLeng, goBackLeng);
                        InjectTextParts(app.Request, data);
                    }
                    else
                    {
                        InjectTextParts(app.Request, arrBuffer);
                    }
                    //temp2File.Close();
                    upload.Status = uploadStatus.Complete;
                }
                //tempFile.WriteLine("OperationEnd :" + DateTime.Now.ToString());
                //tempFile.Close();
            }
        }

        private void context_EndRequest(Object sender, EventArgs e)
        {
        }

        HttpWorkerRequest GetWorkerRequest(HttpContext context)
        {

            IServiceProvider provider = (IServiceProvider)HttpContext.Current;
            return (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));
        }
        /// <summary>
        /// 上传完成后，重写请求头
        /// </summary>
        /// <param name="request">请求头</param>
        /// <param name="textParts">去掉文件内容的请求头</param>
        private void InjectTextParts(HttpRequest request, byte[] textParts)
        {
            BindingFlags flags1 = BindingFlags.NonPublic | BindingFlags.Instance;
            Type type1 = request.GetType();
            FieldInfo info1 = type1.GetField("_rawContent", flags1);
            FieldInfo info2 = type1.GetField("_contentLength", flags1);
            
            if ((info1 != null) && (info2 != null))
            {
                Assembly web = Assembly.GetAssembly(typeof(HttpRequest));
                Type hraw = web.GetType("System.Web.HttpRawUploadedContent");
                object[] argList = new object[2];
                argList[0] = textParts.Length + 1024;
                argList[1] = textParts.Length;

                CultureInfo currCulture = CultureInfo.CurrentCulture;
                object httpRawUploadedContent = Activator.CreateInstance(hraw,
                                                                         BindingFlags.NonPublic | BindingFlags.Instance,
                                                                         null,
                                                                         argList,
                                                                         currCulture,
                                                                         null);

               Type contentType = httpRawUploadedContent.GetType();

               FieldInfo dataField = contentType.GetField("_data", flags1);
               dataField.SetValue(httpRawUploadedContent, textParts);

               FieldInfo lengthField = contentType.GetField("_length", flags1);
               lengthField.SetValue(httpRawUploadedContent, textParts.Length);

               FieldInfo fileCompleted = contentType.GetField("_completed", flags1);
               fileCompleted.SetValue(httpRawUploadedContent, true);

               FieldInfo fileThresholdField = contentType.GetField("_fileThreshold", flags1);
               fileThresholdField.SetValue(httpRawUploadedContent, textParts.Length + 1024);

               info1.SetValue(request, httpRawUploadedContent);
               info2.SetValue(request, textParts.Length);
            }
        }


        private static bool StringStartsWithAnotherIgnoreCase(string s1, string s2)
        {
            return (string.Compare(s1, 0, s2, 0, s2.Length, true, CultureInfo.InvariantCulture) == 0);
        }

        /// <summary>
        /// 是否为附件上传
        /// 判断的根据是ContentType中有无multipart/form-data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool IsUploadRequest(HttpRequest request)
        {
            return StringStartsWithAnotherIgnoreCase(request.ContentType, "multipart/form-data");
        }
        /// <summary>
        /// 获取分隔符的字符数组
        /// </summary>
        /// <param name="ContentType">请求类型</param>
        /// <returns>分隔符字节数组</returns>
        private byte[] GetMultipartBoundary(string ContentType)
        {
            int nPos = ContentType.IndexOf("boundary=");
            string sTemp = ContentType.Substring(nPos + 9);

            string sBoundary = sTemp = "--" + sTemp;
            return Encoding.ASCII.GetBytes(sTemp.ToCharArray());
        }
        private string GetFileName(string PathFileName)
        {
            int index = PathFileName.LastIndexOf("\\");
            return PathFileName.Substring(index + 1, PathFileName.Length - index - 1);
        }
    }
}
