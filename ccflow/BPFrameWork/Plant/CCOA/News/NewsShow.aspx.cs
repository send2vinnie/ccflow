using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BP.CCOA;

public partial class CCOA_News_NewsShow : System.Web.UI.Page
{
    protected Article ThisArticle;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string no = Request.QueryString["no"];

            ThisArticle = new Article(no);

            //CreateHtmlFile(article.FullChannelPath, article.Content, article.FullUrl);
        }
    }

    /// <summary>
    /// 生成HTML文件
    /// </summary>
    /// <param name="FilePath">保存文件路径</param>
    /// <param name="Content">文件流内容</param>
    /// <param name="FileName">文件名</param>
    public static void CreateHtmlFile(string FilePath, string Content, string FileName)
    {
        if (Directory.Exists(FilePath) == false)
        {
            Directory.CreateDirectory(FilePath);
        }
        using (StreamWriter m_streamWriter = new StreamWriter(FilePath + "\\" + FileName, false, System.Text.UnicodeEncoding.GetEncoding("UTF-8")))
        {
            m_streamWriter.WriteLine(Content);
            m_streamWriter.Flush();
            m_streamWriter.Close();
        }
    }
}