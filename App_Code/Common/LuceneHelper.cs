namespace Common.LuceneHelper
{

    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using PanGu;
    using System;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;


    /// <summary>
    /// 搜索器
    /// </summary>
    public class Searcher
    {
        public static Searcher Instance
        {
            get { return new Searcher(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LucenePath"])); }
        }


        string indexPath;
        public Searcher(string indexPath)
        {
            this.indexPath = indexPath;
        }


        /// <summary>
        /// 对搜索词进行分词
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static string SplitWords(string keywords)
        {
            StringBuilder result = new StringBuilder();

            ICollection<WordInfo> words = new PanGuTokenizer().SegmentToWordInfos(keywords);

            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }

                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }

            return result.ToString().Trim();
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="q">关键字</param>
        /// <param name="page">锁定页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderby">排序</param>
        /// <param name="recordCount">记录行</param>
        /// <param name="fields">字段</param>
        /// <returns>查询到的结果</returns>
        public List<Document> Search(string q, int page, int pageSize, string orderby, out int recordCount, params string[] fields)
        {
            string keywords = q;

            IndexSearcher search = new IndexSearcher(indexPath, true);

            Sort sort = null; ;

            if (!string.IsNullOrEmpty(orderby))
            {


                int loopTimes = 0;
                orderby = orderby.Trim();
                while (orderby.IndexOf("  ") >= 0)
                {
                    loopTimes++;
                    if (loopTimes > 10)
                    {
                        break;
                    }
                    orderby = orderby.Replace("  ", string.Empty);
                }

                string[] arr = orderby.Split(' ');
                if (arr.Length == 2)
                {
                    if (arr[1].Equals("desc", StringComparison.OrdinalIgnoreCase))
                    {
                        sort = new Sort(arr[0], true);
                    }
                    else if (arr[1].Equals("asc", StringComparison.OrdinalIgnoreCase))
                    {
                        sort = new Sort(arr[0], false);
                    }

                }
            }

            /*
            if (sort == null)
            {
                q = SplitWords(q);
            }
             */
            q = SplitWords(q);

            BooleanQuery bq = new BooleanQuery();

            foreach (string field in fields)
            {
                QueryParser parser = new QueryParser(field, new PanGuAnalyzer(true));
                Query query = parser.Parse(q);
                bq.Add(query, BooleanClause.Occur.SHOULD);
            }

            Hits hits = null;

            if (sort != null)
            {
                hits = search.Search(bq, sort);
            }
            else
            {
                hits = search.Search(bq);
            }


            List<Document> result = new List<Document>();

            recordCount = hits.Length();
            int i = (page - 1) * pageSize;

            while (i < recordCount && result.Count < pageSize)
            {
                Document doc = null;

                try
                {

                    doc = hits.Doc(i);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    result.Add(doc);
                    i++;
                }
            }

            search.Close();
            return result;
        }


    }


    /// <summary>
    /// 索引器
    /// </summary>
    public class Indexer
    {

        public static Indexer Instance
        {
            get { return new Indexer(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LucenePath"])); }
        }

        IndexWriter writer = null;

        public Indexer(string indexPath)
        {
            writer = new IndexWriter(indexPath, new PanGuAnalyzer(), true);
        }

        public void Write(Hashtable analyzed, Hashtable notAnalyzed = null)
        {
            Document doc = new Document();

            if (analyzed != null)
            {
                foreach (string key in analyzed.Keys)
                {
                    Field f = new Field(key, analyzed[key].ToString(), Field.Store.YES, Field.Index.ANALYZED);
                    doc.Add(f);
                }
            }

            if (notAnalyzed != null)
            {

                foreach (string key in notAnalyzed.Keys)
                {
                    Field f = new Field(key, notAnalyzed[key].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
                    doc.Add(f);
                }
            }

            writer.AddDocument(doc);

        }


        public void Delete(string field, string value)
        {
            writer.DeleteDocuments(new Term(field, value));
        }


        public void Commit()
        {
            writer.Commit();
        }

        public void Optimize()
        {
            writer.Optimize();
        }

        public void Close()
        {
            writer.Close();
        }
    }

}