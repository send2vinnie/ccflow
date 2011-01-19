using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using Tax666.DataEntity;
using Tax666.BusinessFacade;

namespace Tax666.AppWeb
{
    public class ArticleCommon
    {
        #region 添加某文章的关键字表记录
        /// <summary>
        /// 添加某文章的关键字表记录；
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="keywordStr"></param>
        public static void InsertArticleKeys(int articleID, string keywordStr)
        {
            //删除该ID的所有关键字；
            (new ArticleKeywordsFacade()).DelArticleKeywords(articleID);

            if (keywordStr != "" || keywordStr != string.Empty)
            {
                //分割字符串；
                if (keywordStr.IndexOf(",", keywordStr.Length - 1) != -1)
                    keywordStr = keywordStr.Substring(0, keywordStr.Length - 1);

                string[] keyItem = keywordStr.Split(new char[] { ',' });
                for (int i = 0; i < keyItem.Length; i++)
                {
                    (new ArticleKeywordsFacade()).InsertArticleKeywords(articleID, keyItem[i]);
                }
            }
        }
        #endregion

        #region 资讯文章关键字处理
        /// <summary>
        /// 根据ArticleID获取该文章的全部关键字；
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public static string GetAricleKeys(int articleID)
        {
            string retVal = "";
            ArticleKeywordsData keyData = (new ArticleKeywordsFacade()).GetArticleKeywordsByID(articleID);
            if (keyData.Tables[ArticleKeywordsData.ArticleKeywords_Table].Rows.Count > 0)
            {
                DataTable table = keyData.Tables[ArticleKeywordsData.ArticleKeywords_Table];
                int rowNum = table.Rows.Count;
                for (int i = 0; i < rowNum; i++)
                {
                    retVal += table.Rows[i][ArticleKeywordsData.Keyword_Field].ToString();
                    if (i < rowNum - 1)
                        retVal += ",";
                }
            }

            return retVal;
        }
        #endregion

        #region 绑定文章类别（大类和小类）下拉列表
        /// <summary>
        /// 绑定文章类别（大类和小类）下拉列表。
        /// </summary>
        /// <param name="dplTypeList">下拉列表控件</param>
        /// <param name="isAll">是否绑定全部类别</param>
        /// <param name="isValid">0-无效；1-有效；2-全部</param>
        public static void BindArticleTypeList(DropDownList dplTypeList, bool isAll, int isValid)
        {
            ArticleBigTypeData bigData = null;
            ArticleSmallTypeData smallData = null;
            dplTypeList.Items.Clear();

            if (isAll)
                dplTypeList.Items.Add(new ListItem("全部类别文章", "0|0"));

            //获取所有大类名称和BigTypeID，拼合导航菜单(格式：BigTypeID|SmallTypeID)；
            bigData = (new ArticleBigTypeFacade()).GetArticleBigTypeByListType(2, 0, 0, 0, true);
            DataTable bigTable = bigData.Tables[ArticleBigTypeData.ArticleBigType_Table];
            int bigNum = bigTable.Rows.Count;

            for (int i = 0; i < bigNum; i++)
            {
                string bigTypeName = "";
                int bigTypeID = int.Parse(bigTable.Rows[i][ArticleBigTypeData.BigTypeID_Field].ToString());
                if (i == 0 && !isAll)
                    bigTypeName = "　┌ " + bigTable.Rows[i][ArticleBigTypeData.BigTypeName_Field].ToString();
                else if (i < bigNum - 1)
                    bigTypeName = "　├ " + bigTable.Rows[i][ArticleBigTypeData.BigTypeName_Field].ToString();
                else
                    bigTypeName = "　└ " + bigTable.Rows[i][ArticleBigTypeData.BigTypeName_Field].ToString();

                String biglistValue = bigTypeID.ToString() + "|0";
                dplTypeList.Items.Add(new ListItem(bigTypeName, biglistValue));

                //获取该SmallTypeID下所有的栏目；
                smallData = (new ArticleSmallTypeFacade()).GetArticleSmallTypeByList(2, bigTypeID, 0, isValid, 0, 0, true);
                DataTable smallTable = smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table];
                int smallNum = smallTable.Rows.Count;

                for (int j = 0; j < smallNum; j++)
                {
                    String smallListText = "";
                    String smallListValue = bigTypeID.ToString() + "|" + smallTable.Rows[j][ArticleSmallTypeData.SmallTypeID_Field].ToString();
                    if (i < bigNum - 1)
                    {
                        if (j < smallNum - 1)
                            smallListText = "　│　├ " + smallTable.Rows[j][ArticleSmallTypeData.SmallTypeName_Field].ToString();
                        else
                            smallListText = "　│　└ " + smallTable.Rows[j][ArticleSmallTypeData.SmallTypeName_Field].ToString();
                    }
                    else
                    {
                        if (j < smallNum - 1)
                            smallListText = "　　　├ " + smallTable.Rows[j][ArticleSmallTypeData.SmallTypeName_Field].ToString();
                        else
                            smallListText = "　　　└ " + smallTable.Rows[j][ArticleSmallTypeData.SmallTypeName_Field].ToString();
                    }

                    dplTypeList.Items.Add(new ListItem(smallListText, smallListValue));
                }
            }
        }
        #endregion

        #region 绑定文章大类的下拉列表
        /// <summary>
        /// 绑定文章大类的下拉列表
        /// </summary>
        /// <param name="list">DropDownList</param>
        /// <param name="isNewRow">是否包含全部</param>
        /// <param name="ParentAgentID"></param>
        public static int BindArticleBigTypeList(DropDownList list, bool isNewRow)
        {
            int m_BigTypeNum = 0;
            ArticleBigTypeData bigTypeData;
            bigTypeData = (new ArticleBigTypeFacade()).GetArticleBigTypeByListType(2, 0, 0, 0, true);
            m_BigTypeNum = bigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Count;

            if (isNewRow)
            {
                //添加新行；
                DataRow row = bigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].NewRow();
                row[ArticleBigTypeData.BigTypeID_Field] = 0;
                row[ArticleBigTypeData.BigTypeName_Field] = "全部大类";
                bigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Add(row);
            }

            list.DataSource = bigTypeData;
            list.DataTextField = ArticleBigTypeData.BigTypeName_Field;
            list.DataValueField = ArticleBigTypeData.BigTypeID_Field;
            list.DataBind();

            return m_BigTypeNum;
        }
        #endregion

        #region 绑定文章小类的下拉列表
        /// <summary>
        /// 绑定文章小类的下拉列表
        /// </summary>
        /// <param name="list">DropDownList Control</param>
        /// <param name="isNewRow">是否创建新行</param>
        public static int BindArticleSamllTypeList(DropDownList list, bool isNewRow, int ParentAgentID, int bigTypeID)
        {
            int m_SmallTypeNum = 0;

            ArticleSmallTypeData smallTypeData;
            smallTypeData = (new ArticleSmallTypeFacade()).GetArticleSmallTypeByList(2, bigTypeID, 0, 2, 0, 0, true);
            m_SmallTypeNum = smallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count;

            if (isNewRow)
            {
                //添加新行；
                DataRow row = smallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].NewRow();
                row[ArticleSmallTypeData.SmallTypeID_Field] = 0;
                row[ArticleSmallTypeData.SmallTypeName_Field] = "全部小类";
                smallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Add(row);
            }

            list.DataSource = smallTypeData;
            list.DataTextField = ArticleSmallTypeData.SmallTypeName_Field;
            list.DataValueField = ArticleSmallTypeData.SmallTypeID_Field;
            list.DataBind();

            return m_SmallTypeNum;
        }
        #endregion

        #region 绑定文章某个大类下的所有小类列表
        /// <summary>
        /// 绑定文章某个大类下的所有小类列表
        /// </summary>
        /// <param name="bigTypeID"></param>
        /// <param name="ParentAgentID"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        public static ArrayList GetArticleSmallTypeListByBigID(int bigTypeID, int ParentAgentID, int valid)
        {
            ArrayList lst = new ArrayList();
            ArticleSmallTypeData smallData = (new ArticleSmallTypeFacade()).GetArticleSmallTypeByList(2, bigTypeID, 0, valid, 0, 0, true);
            if (smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count > 0)
            {
                DataTable table = smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string smallid = table.Rows[i][ArticleSmallTypeData.SmallTypeID_Field].ToString();
                    string smallname = table.Rows[i][ArticleSmallTypeData.SmallTypeName_Field].ToString();
                    lst.Add(string.Format("{0}|{1}", smallid, smallname));
                }
            }

            return lst;
        }
        #endregion
    }
}
