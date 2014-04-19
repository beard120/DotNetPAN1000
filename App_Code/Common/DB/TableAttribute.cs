namespace Common.DB
{
    using System;



    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {

        public string TableName
        {
            get;
            set;
        }

        public static string GetTableName(Type typ)
        {
            TableAttribute tableAttribute = null;

            object[] attributes = typ.GetCustomAttributes(typeof(TableAttribute), false);




            if (attributes.Length > 0)
            {
                tableAttribute = (TableAttribute)attributes[0];
                return tableAttribute.TableName;
            }
            else
            {
                return typ.Name;
            }
        }
    }

}