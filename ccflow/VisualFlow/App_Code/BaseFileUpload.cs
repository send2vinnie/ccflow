using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    /// <summary>
    /// FileUpload说明
    /// </summary>
    public class BaseFileUpload
    {
        private string path = null;
        private string fileType = null;
        private int sizes = 0;
        private HttpPostedFile postedFile = null;
        /// <summary>
        /// 初始化变量
        /// </summary>
        public BaseFileUpload()
        {
        }
        //
        public HttpPostedFile PostedFile
        {
            get
            {
                return postedFile;
            }
            set
            {
                postedFile = value;
            }
        }
        /// <summary>
        /// 设置上传路径,如:upload
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = @"\" + value + @"\";
            }
        }

        /// <summary>
        /// 设置上传文件大小,单位为KB
        /// </summary>
        public int Sizes
        {
            get
            {
                return sizes;
            }
            set
            {
                sizes = value * 1024;
            }
        }

        /// <summary>
        /// 上传文件的类型
        /// </summary>
        public string FileType
        {
            get
            {
                return fileType;
            }
            set
            {
                fileType = value;
            }
        }
        public string PathToName(string path)
        {
            int pos = path.LastIndexOf("\\");
            return path.Substring(pos + 1);
        }

        /// <summary>
        /// 上传
        /// </summary>
        public string Upload(string modifyFileName)
        {
            try
            {

                string filePath = null;
                //以当前时间修改图片的名字或创建文件夹的名字
                //string modifyFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string uploadFilePath = null;

                uploadFilePath = System.Web.HttpContext.Current.Server.MapPath(".") + path;

                //获得文件的上传的路径
                string sourcePath = PathToName(PostedFile.FileName);
                //判断上传文件是否为空
                if (sourcePath == "" || sourcePath == null)
                {
                    //message("您没有上传数据呀，是不是搞错了呀!");
                    messageError("您没有上传数据呀，是不是搞错了呀!");
                    return null;
                }
                //获得文件扩展名
                string tFileType = sourcePath.Substring(sourcePath.LastIndexOf(".") + 1);

                //获得上传文件的大小
                long strLen = PostedFile.ContentLength;
                //分解允许上传文件的格式
                string[] temp = fileType.Split('|');
                //设置上传的文件是否是允许的格式
                bool flag = false;
                //判断上传文件大小
                if (strLen >= sizes)
                {

                    //message("上传的图片不能大于" + sizes + "KB");
                    messageError("上传的图片不能大于" + sizes + "KB");
                    return null;
                }
                //判断上传的文件是否是允许的格式
                foreach (string data in temp)
                {
                    if (data == tFileType)
                    {
                        flag = true;
                        break;
                    }
                }
                //如果为真允许上传,为假则不允许上传
                if (!flag)
                {
                    //message("目前本系统支持的格式为:" + fileType);
                    messageError("目前本系统支持的格式为:" + fileType);
                    return null;
                }
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadFilePath);
                //判断文件夹否存在,不存在则创建
                if (!dir.Exists)
                {
                    dir.Create();
                }
                filePath = uploadFilePath + modifyFileName  ;
                PostedFile.SaveAs(filePath);
                filePath = path + modifyFileName  ;

                return modifyFileName ; //只有文件名
                //return filePath; //路径+文件名

            }
            catch
            {
                //异常
                //message("出现未知错误！");
                messageError("出现未知错误！");
                return null;
            }
        }
        /// <summary>
        /// 上传文件，成功后返回文件实体信息，zyh modify 2010-06-30返回的错误信息未完善
        /// </summary>
        /// <returns></returns>
        public MyFile UploadRetInfo()
        {
            int errorcode = 500;
            string errormsg = "未知错误！";
            try
            {
                string filePath = null;
                //以当前时间修改图片的名字或创建文件夹的名字
                //string modifyFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string modifyFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                //获得站点的物理路径
                string uploadFilePath = null;

                uploadFilePath = System.Web.HttpContext.Current.Server.MapPath(".") + path;

                //获得文件的上传的路径
                string sourcePath = PathToName(PostedFile.FileName);
                //判断上传文件是否为空
                if (sourcePath == "" || sourcePath == null)
                {
                    //message("您没有上传数据呀，是不是搞错了呀!");
                    MsgSWFError("您没有上传数据呀，是不是搞错了呀!");
                    return null;
                }
                //获得文件扩展名
                string tFileType = sourcePath.Substring(sourcePath.LastIndexOf(".") + 1);
                //获得上传文件的大小
                long strLen = PostedFile.ContentLength;
                //分解允许上传文件的格式
                string[] temp = fileType.Split('|');
                //设置上传的文件是否是允许的格式
                bool flag = false;
                //判断上传文件大小
                if (strLen >= sizes)
                {
                    //message("上传的图片不能大于" + sizes + "KB");
                    MsgSWFError("上传的图片不能大于" + sizes + "KB");
                    return null;
                }
                //判断上传的文件是否是允许的格式
                foreach (string data in temp)
                {
                    if (data == tFileType)
                    {
                        flag = true;
                        break;
                    }
                }
                //如果为真允许上传,为假则不允许上传
                if (!flag)
                {
                    //message("目前本系统支持的格式为:" + fileType);
                    errorcode = 500;
                    errormsg = "目前本系统支持的格式为:" + fileType;
                    MsgSWFError(errorcode, errormsg);
                    return null;
                }
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadFilePath);
                //判断文件夹否存在,不存在则创建
                if (!dir.Exists)
                {
                    dir.Create();
                }
                filePath = uploadFilePath + modifyFileName + "." + tFileType;
                PostedFile.SaveAs(filePath);
                filePath = path + modifyFileName + "." + tFileType;

                //return modifyFileName + "." + tFileType; //只有文件名
                //return filePath; //路径+文件名

                MyFile myfile = new MyFile();
                myfile.File_Name = modifyFileName;
                myfile.File_Ext = tFileType;
                myfile.File_Title = System.IO.Path.GetFileName(postedFile.FileName);  //获得原文件名（含扩展名）  
                myfile.File_Description = myfile.File_Title.Replace("." + tFileType + "", "");
                myfile.File_Size = strLen;
                myfile.File_Createtime = DateTime.Now;

                return myfile;
                
            }
            catch
            {
                //异常
                //message("出现未知错误！");
                MsgSWFError(errorcode, errormsg);
                return null;
            }
        }

        private void MsgSWFError(string msg)
        {
            MsgSWFError(500, msg);
        }
        private void MsgSWFError(int errorCode, string msg)
        { 
            System.Web.HttpContext.Current.Response.StatusCode = errorCode;
            //System.Web.HttpContext.Current.Response.Status = msg + "status";
            //System.Web.HttpContext.Current.Response.StatusDescription = msg + "description";
            System.Web.HttpContext.Current.Response.Write(msg);
            System.Web.HttpContext.Current.Response.End();
        }
        private void messageError(string msg)
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>alert('" + msg + "');</script>"); //window.history.back();
            
        }

        private void message(string msg, string url)
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>alert('" + msg + "');window.location='" + url + "'</script>");
        }

        private void message(string msg)
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>alert('" + msg + "');</script>");
        }

    }

    public class MyFile
    {
        private string file_name;

        private string file_title;

        private string file_description;

        private long file_size;

        private string file_ext;

        private DateTime file_createtime;


        public string File_Name
        {
            get { return file_name; }
            set { file_name = value; }
        }
        public string File_Title
        {
            get { return file_title; }
            set { file_title = value; }
        }
        public string File_Description
        {
            get { return file_description; }
            set { file_description = value; }
        }
        public long File_Size
        {
            get { return file_size; }
            set { file_size = value; }
        }
        public string File_Ext
        {
            get { return file_ext; }
            set { file_ext = value; }
        }

        public DateTime File_Createtime
        {
            get { return file_createtime; }
            set { file_createtime = value; }
        }
    }


