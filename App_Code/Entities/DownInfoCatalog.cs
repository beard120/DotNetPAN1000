using Common.DB;


namespace Entities
{

    [Table(TableName = "[DownInfosCatalogs]")]
    public struct DownInfoCatalog
    {
        [Field(IsId=true)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }

        [Field(FieldName="[Key]")]
        public string Key { get; set; }

        [Field(FieldName="[Path]")]
        public string Path { get; set; }
    }

}