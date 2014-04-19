
using Common.DB;


namespace Entities
{
    [Table(TableName = "[Topics]")]
    public struct Topic
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Name { get; set; }

        [Field(FieldName = "[Key]")]
        public string Key { get; set; }
        public int TempleteID { get; set; }
        public string TempletePath { get; set; }

        public string Tags { get; set; }
        public string TagsIds { get; set; }
        public string Keywords { get; set; }
        public string KeywordsIds { get; set; }
    }

}