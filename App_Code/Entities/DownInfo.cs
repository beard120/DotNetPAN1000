using System;
using Common.DB;


namespace Entities
{

    [Table(TableName = "[DownInfos]")]
    public struct DownInfo
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public int SeriesID { get; set; }
        public string Tags { get; set; }
        public string TagsIds { get; set; }
        public string Keywords { get; set; }
        public string KeywordsIds { get; set; }
        public int CatalogID { get; set; }
        public string CatalogPath { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }

        public string Details { get; set; }

        public string Languages { get; set; }
        public string LanguagesIds { get; set; }
        public bool Recommend { get; set; }
        public bool Published { get; set; }
        public string Cover { get; set; }


        [Field(AllowModifyUndefined = true)]
        public int VoteA { get; set; }


        [Field(AllowModifyUndefined = true)]
        public int VoteB { get; set; }

        [Field(AllowModifyUndefined = true)]
        public double Score { get; set; }

        [Field(AllowModifyUndefined = true)]
        public int ViewTimes { get; set; }

        [Field(AllowModifyUndefined = true)]
        public int DayViewTimes { get; set; }


        [Field(AllowModifyUndefined = true)]
        public int WeekViewTimes { get; set; }

        [Field(AllowModifyUndefined = true)]
        public int MonthViewTimes { get; set; }


        [Field(AllowModifyUndefined = true)]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

}