using Common.DB;


namespace Entities
{

    [Table(TableName = "[ArticlesKeywords]")]
    public struct ArticleKeyword
    {

        public int ID { get; set; }
        public string Name { get; set; }


        [Field(AllowModifyUndefined = true)]
        public int UsageCount { get; set; }
    }

}