

using Common.DB;
namespace Entities
{

    [Table(TableName = "[ArticlesContents]")]
    public struct ArticleContent
    {
        public int ID { get; set; }
        public int PageNumber { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }

}