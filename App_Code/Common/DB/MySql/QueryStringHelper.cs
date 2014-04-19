namespace Common.DB.MySql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;


    public class QueryStringHelper
    {
        private int absolutePage = 1;

        private string fields = " * ";
        private int pageSize = 20;
        private string identity = " ID ";

        private string sort = " ID desc ";
        private string table = string.Empty;
        private string where = " 1=1 ";

        public string GetQueryString()
        {
            int p = this.absolutePage;

            if (this.absolutePage < 1)
            {
                this.absolutePage = 1;
            }

            StringBuilder builder = new StringBuilder();

            builder.Append("select ");
            builder.Append(this.fields);
            builder.Append(" from ");
            builder.Append(this.table);
            builder.Append(" where ");
            builder.Append(this.where);
            builder.Append(" order by ");
            builder.Append(this.sort);

            builder.Append(" limit (" + (this.pageSize * (this.absolutePage - 1)) + "),");
            builder.Append(this.pageSize.ToString());



            return builder.ToString();

        }

        public string GetCountQueryString()
        {
            return "select count(" + this.identity + ") from " + this.table + " where " + this.where;
        }

        public int AbsolutePage
        {
            get
            {
                return this.absolutePage;
            }

            set
            {
                this.absolutePage = value;
            }
        }



        public string Fields
        {
            get
            {
                return this.fields;
            }
            set
            {
                this.fields = value;
            }
        }


        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        public string Identity
        {
            get
            {
                return this.identity;
            }
            set
            {
                this.identity = value;
            }
        }



        public string Sort
        {
            get
            {
                return this.sort;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.sort = value;
                }
            }
        }

        public string Table
        {
            get
            {
                return this.table;
            }
            set
            {
                this.table = value;
            }
        }

        public string Where
        {
            get
            {
                return this.where;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.where = value;
                }

            }
        }
    }


}