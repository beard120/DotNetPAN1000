using Common.DB;
using System;
namespace Entities
{
    [Table(TableName = "[DownInfosLinks]")]
    public struct DownInfoLink
    {
        public int InfoID { get; set; }
        public string Link { get; set; }
        public int TypeID { get; set; }
        public bool Enable { get; set; }

        [Field(AllowModifyUndefined = true)]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Field(AllowModifyUndefined = true)]
        public int DownTimes { get; set; }

    }

}