namespace Common.DB
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FieldAttribute : Attribute
    {

        bool isId = false;
        /// <summary>
        /// 是否为标识列
        /// </summary>
        public bool IsId
        {
            get { return isId; }
            set { isId = value; }
        }


        public string FieldName
        {
            get;
            set;
        }

        //(select Email from yxdown.dbo.Admin_UserInfo where Username=tb.username) as Editor

        public string FieldSQL
        {
            get;
            set;
        }

        public static string GetFieldName(PropertyInfo pi)
        {
            FieldAttribute fieldAttribute = null;
            object[] attributes = pi.GetCustomAttributes(typeof(FieldAttribute), false);

            if (attributes.Length > 0)
            {
                fieldAttribute = (FieldAttribute)attributes[0];
                return string.IsNullOrEmpty(fieldAttribute.FieldName) ? pi.Name : fieldAttribute.FieldName;
            }
            else
            {
                return pi.Name;
            }
        }


        public static string GetFieldSQL(PropertyInfo pi)
        {
            FieldAttribute fieldAttribute = null;
            object[] attributes = pi.GetCustomAttributes(typeof(FieldAttribute), false);

            if (attributes.Length > 0)
            {
                fieldAttribute = (FieldAttribute)attributes[0];
                return fieldAttribute.FieldSQL;
            }

            return null;
        }


        /// <summary>
        /// 判断是否为标识列
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static bool IsIdentity(PropertyInfo pi)
        {
            object[] attributes = pi.GetCustomAttributes(typeof(FieldAttribute), true);
            if (attributes.Length > 0)
            {
                return ((FieldAttribute)attributes[0]).IsId;
            }
            return false;
        }






        bool allowModifyUndefined = false;
        /// <summary>
        /// 允许在修改时不定义此字段的值
        /// </summary>
        public bool AllowModifyUndefined
        {
            get { return allowModifyUndefined; }
            set { allowModifyUndefined = value; }
        }

        /// <summary>
        /// 判断在修改的时候是否允许不定义某字段的值
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static bool IsAllowModifyUndefined(PropertyInfo pi)
        {
            object[] attributes = pi.GetCustomAttributes(typeof(FieldAttribute), true);
            if (attributes.Length > 0)
            {
                return ((FieldAttribute)attributes[0]).AllowModifyUndefined;
            }
            return false;
        }


        public static string GetIDFieldName(Type typ)
        {
            PropertyInfo[] pis = typ.GetProperties();
            for (int i = 0; i < pis.Length; i++)
            {
                PropertyInfo pi = pis[i];
                if (IsIdentity(pi))
                {
                    return GetFieldName(pi);
                }
            }

            return null;
        }


    }

}