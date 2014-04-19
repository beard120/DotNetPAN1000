using Common.DB;


namespace Entities
{
    [Table(TableName = "[Series]")]
    public struct Series
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Name { get; set; }
    }

}