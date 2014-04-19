using Common.DB;


namespace Entities
{
    [Table(TableName = "[Tags]")]
    public class Tag
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Name { get; set; }

        [Field(AllowModifyUndefined = true)]
        public int UsageCount { get; set; }
    }

}