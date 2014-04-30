using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.DB;


namespace Entities
{
    [Table(TableName = "[Articles]")]
    public struct Article
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int SeriesID { get; set; }
        public int CatalogID { get; set; }
        public string CatalogPath { get; set; }
        public string Keywords { get; set; }
        public string KeywordsIds { get; set; }
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


        public string Description { get; set; }
        public string Content { get; set; }

        public bool Recommend { get; set; }
        public bool Published { get; set; }

        public int CEditorID { get; set; }
        public int MEditorID { get; set; }
    }

}