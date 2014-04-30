using System.Text;
namespace Mgr.Services
{
    /// <summary>
    /// ArticlesCatalogsService 的摘要说明
    /// </summary>
    public static class ArticlesCatalogsService
    {





        static Common.DB.IDBEntityHelper dbeh = Common.DB.Factory.CreateDBEntityHelper();

        public static string GetCategoryPath(int id)
        {
            StringBuilder sb = new StringBuilder();

            global::Entities.ArticleCatalog ent = dbeh.GetEntity<global::Entities.ArticleCatalog>(id);

            if (ent.ParentID > 0)
            {
                sb.Append(GetCategoryPath(ent.ParentID));

                sb.Append(" &gt; ");
            }


            sb.Append(ent.Name);


            return sb.ToString();
        }
        public static string GetCategoryList()
        {
            StringBuilder sb = new StringBuilder();
            System.Collections.Generic.List<global::Entities.ArticleCatalog> list = dbeh.GetDataList<global::Entities.ArticleCatalog>("ParentID=" + 0);
            for (int i = 0; i < list.Count; i++)
            {
                global::Entities.ArticleCatalog ent = list[i];
                sb.Append(GetCategoryList(ent.ID));
            }

            return sb.ToString();
        }

        public static string GetCategoryList(int pid)
        {
            StringBuilder sb = new StringBuilder();
            System.Collections.Generic.List<global::Entities.ArticleCatalog> list = dbeh.GetDataList<global::Entities.ArticleCatalog>("ParentID=" + pid);

            global::Entities.ArticleCatalog pent = dbeh.GetEntity<global::Entities.ArticleCatalog>(pid);
            if (list != null)
            {
                sb.Append("<li>\n");
                if (list.Count > 0)
                {
                    if (pent.ID > 0)
                    {
                        sb.Append("<span class=\"folder\">" + pent.Name + "</span>\n");
                    }

                    sb.Append("<ul style=\"display:none\">\n");
                    for (int i = 0; i < list.Count; i++)
                    {
                        global::Entities.ArticleCatalog ent = list[i];
                        sb.Append(GetCategoryList(ent.ID));
                    }
                    sb.Append("</ul>\n");
                }
                else
                {
                    sb.Append("<span class=\"file\" onclick=\"SelectThis('" + GetCategoryPath(pent.ID) + "'," + pent.ID + ");\">" + pent.Name + "</span>\n");
                }
                sb.Append("</li>\n");
            }

            return sb.ToString();
        }
    }

}