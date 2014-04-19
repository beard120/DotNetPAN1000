using Common.DB;


namespace Entities
{
    [Table(TableName = "[TopicsTempletes]")]
    public struct TopicTemplete
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Name { get; set; }

        [Field(FieldName = "[Path]")]
        public string Path { get; set; }
    }

}