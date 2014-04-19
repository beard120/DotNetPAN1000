
using Common.DB;

namespace Entities
{
    [Table(TableName = "[DownInfosLinksTypes]")]
    public struct DownInfoLinkType
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Prefixes { get; set; }
        public string Names { get; set; }
        public bool Enable { get; set; }
    }

}