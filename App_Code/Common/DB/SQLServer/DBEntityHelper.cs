namespace Common.DB.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Reflection;
    using System.Text;






    /// <summary>
    /// DbEntityHelper 的摘要说明
    /// </summary>
    public class DBEntityHelper : IDBEntityHelper
    {
        IDBHelper db = null;

        public IDBHelper DBHelper
        {
            get { return db; }
            set { db = value; }
        }


        public DBEntityHelper()
        {

        }




        public T GetEntityBySql<T>(string sql, params DbParameter[] parameters)
        {
            List<T> list = GetDataList<T>(sql, parameters);

            if (list != null)
            {
                if (list.Count == 1)
                {
                    return list[0];
                }
            }

            return default(T);
        }


        public T GetEntity<T>(int id)
        {
            return GetEntityById<T, int>(id);
        }

        public T GetEntity<T, IDT>(IDT id)
        {
            return GetEntityById<T, IDT>(id);
        }

        public T GetEntityById<T, IDT>(IDT id)
        {
            string idFieldName = FieldAttribute.GetIDFieldName(typeof(T));

            if (!string.IsNullOrEmpty(idFieldName))
            {
                return GetEntity<T>(idFieldName + "=@ID", db.CreateParameter("@ID", id));
            }

            return default(T);
        }

        public T GetEntity<T>(string where, params DbParameter[] parameters)
        {
            return GetEntityByWhere<T>(where, parameters);
        }

        public T GetEntity<T>(string where, string sort, params DbParameter[] parameters)
        {
            return GetEntityByWhere<T>(where, sort, parameters);
        }


        public T GetEntityByWhere<T>(string where, params DbParameter[] parameters)
        {
            return GetEntityByWhere<T>(where, null, parameters);
        }

        public T GetEntityByWhere<T>(string where, string sort, params DbParameter[] parameters)
        {
            List<T> list = GetDataList<T>(1, where, sort, parameters);
            if (list != null)
            {
                if (list.Count == 1)
                {
                    return list[0];
                }
            }
            return default(T);
        }





        /// <summary>
        /// sql 获得列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> GetDataList<T>(string queryString, params DbParameter[] parameters)
        {
            queryString = queryString.Trim();
            PropertyInfo[] propertys = typeof(T).GetProperties();
            string sql = null;
            if (queryString.IndexOf("select", StringComparison.OrdinalIgnoreCase) == 0)
            {
                sql = queryString;
            }
            else
            {
                Type typ = typeof(T);

                string tableName = TableAttribute.GetTableName(typ);



                StringBuilder fields = new StringBuilder("0");
                foreach (PropertyInfo pi in propertys)
                {
                    fields.Append(",");

                    string fsql = FieldAttribute.GetFieldSQL(pi);
                    if (!string.IsNullOrEmpty(fsql))
                    {
                        fields.Append(fsql);
                    }
                    else
                    {
                        fields.Append(FieldAttribute.GetFieldName(pi));
                    }

                    

                }

                if (queryString.IndexOf("where", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    sql = "select top 1000 " + fields.ToString() + " from " + tableName + " " + queryString;
                }
                else
                {
                    sql = "select top 1000 " + fields.ToString() + " from " + tableName + " where " + queryString;
                }


            }

            List<T> list = new List<T>(30);


            using (DbDataReader reader = db.ExecuteReader(sql, parameters))
            {
                while (reader.Read())
                {
                    T ins = Activator.CreateInstance<T>();

                    foreach (PropertyInfo pi in propertys)
                    {
                        int inx = reader.GetOrdinal(FieldAttribute.GetFieldName(pi).Replace("[", string.Empty).Replace("]", string.Empty));

                        object value = reader.GetValue(inx);

                        if (value == DBNull.Value)
                        {
                            pi.SetValue(ins, null, null);
                        }
                        else
                        {

                            pi.SetValue(ins, value, null);
                        }
                    }

                    list.Add(ins);
                }
                reader.Close();
                reader.Dispose();
            }





            return list;
        }




        /// <summary>
        /// 获得列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num">选择多少条，如果num &lt;= 0 则选择全部</param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> GetDataList<T>(int num, string where, string sort, params DbParameter[] parameters)
        {
            Type typ = typeof(T);

            string tableName = TableAttribute.GetTableName(typ);

            PropertyInfo[] propertys = typ.GetProperties();

            StringBuilder fields = new StringBuilder("0");
            foreach (PropertyInfo pi in propertys)
            {
                fields.Append(",");

                string fsql = FieldAttribute.GetFieldSQL(pi);
                if (!string.IsNullOrEmpty(fsql))
                {
                    fields.Append(fsql);
                }
                else
                {
                    fields.Append(FieldAttribute.GetFieldName(pi));
                }


                

            }

            where = where ?? string.Empty;
            if (!string.IsNullOrEmpty(where))
            {
                where = " where " + where;
            }

            sort = sort ?? string.Empty;
            if (!string.IsNullOrEmpty(sort))
            {
                sort = " order by " + sort;
            }

            string sql = null;
            if (num > 0)
            {
                sql = "select top " + num + " " + fields.ToString() + " from " + tableName + " " + where + " " + sort;
            }
            else
            {
                sql = "select " + fields.ToString() + " from " + tableName + " " + where + " " + sort;

            }

            return GetDataList<T>(sql, parameters);
        }


        /// <summary>
        /// 选择全部数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetDataList<T>()
        {

            return GetDataList<T>(100, null, null);
        }



        /// <summary>
        /// 获得一个分页数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="recordCount"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> GetPageDataList<T>(int pageNumber, int pageSize, string where, string sort, out int recordCount, params DbParameter[] parameters)
        {
            Type typ = typeof(T);

            string tableName = TableAttribute.GetTableName(typ);

            PropertyInfo[] propertys = typ.GetProperties();

            StringBuilder fields = new StringBuilder("0");
            string identity = string.Empty;
            foreach (PropertyInfo pi in propertys)
            {
                if (FieldAttribute.IsIdentity(pi)) identity = FieldAttribute.GetFieldName(pi);

                fields.Append(",");

                string fsql = FieldAttribute.GetFieldSQL(pi);
                if (!string.IsNullOrEmpty(fsql))
                {
                    fields.Append(fsql);
                }
                else
                {
                    fields.Append(FieldAttribute.GetFieldName(pi));
                }


                //fields.Append(FieldAttribute.GetFieldName(pi));

            }

            QueryStringHelper query = new QueryStringHelper();
            query.Fields = fields.ToString();
            query.Identity = identity;
            query.Table = tableName;
            query.PageSize = pageSize;
            query.AbsolutePage = pageNumber;
            query.Where = where;
            query.Sort = sort;

            recordCount = db.ExecuteScalar<int>(query.GetCountQueryString());

            string selectString = query.GetQueryString();

            return GetDataList<T>(selectString, parameters);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateData<T>(T entity)
        {
            Type typ = typeof(T);
            PropertyInfo[] propertys = typeof(T).GetProperties();
            string tableName = TableAttribute.GetTableName(typ);

            List<DbParameter> list = new List<DbParameter>(propertys.Length);


            StringBuilder fps = new StringBuilder();

            bool first = true;

            string where = string.Empty;
            foreach (PropertyInfo pi in propertys)
            {
                if (FieldAttribute.IsAllowModifyUndefined(pi))
                {
                    continue;
                }

                if (!FieldAttribute.IsIdentity(pi))
                {
                    Type pp = pi.PropertyType;

                    object value = pi.GetValue(entity, null);
                    if (pp.IsValueType)
                    {
                        if (value == null)
                        {
                            continue;
                        }
                    }


                    if (!first)
                    {
                        fps.Append(",");
                    }
                    else
                    {
                        first = false;
                    }


                    fps.Append(FieldAttribute.GetFieldName(pi));
                    fps.Append("=@");
                    fps.Append(pi.Name);


                    list.Add(db.CreateParameter("@" + pi.Name, value));
                }
                else
                {
                    where = FieldAttribute.GetFieldName(pi) + "=@" + pi.Name;
                    list.Add(db.CreateParameter("@" + pi.Name, pi.GetValue(entity, null)));
                }
            }


            int num = db.ExecuteNoneQuery("update " + tableName + " set " + fps + " where " + where, list.ToArray());
            if (num == 1)
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertData<T>(T entity)
        {

            Type typ = typeof(T);
            PropertyInfo[] propertys = typeof(T).GetProperties();
            string tableName = TableAttribute.GetTableName(typ);

            List<DbParameter> list = new List<DbParameter>(propertys.Length);


            StringBuilder fields = new StringBuilder();
            StringBuilder parames = new StringBuilder();

            bool first = true;

            foreach (PropertyInfo pi in propertys)
            {
                if (!FieldAttribute.IsIdentity(pi))
                {
                    Type pp = pi.PropertyType;
                    object value = pi.GetValue(entity, null);
                    if (pp.IsValueType)
                    {
                        if (value == null)
                        {
                            continue;
                        }
                    }

                    if (!first)
                    {
                        fields.Append(",");
                        parames.Append(",");
                    }
                    else
                    {
                        first = false;
                    }


                    fields.Append(FieldAttribute.GetFieldName(pi));


                    parames.Append("@");
                    parames.Append(pi.Name);

                    list.Add(db.CreateParameter("@" + pi.Name, value));
                }
            }



            int num = db.ExecuteNoneQuery("insert into " + tableName + "(" + fields + ") values(" + parames + ")", list.ToArray());
            if (num == 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 插入数据，并返回最终插入的ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="RT"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public RT InsertDataAndGetID<T, RT>(T entity)
        {
            Type typ = typeof(T);
            PropertyInfo[] propertys = typeof(T).GetProperties();
            string tableName = TableAttribute.GetTableName(typ);

            List<DbParameter> list = new List<DbParameter>(propertys.Length);


            StringBuilder fields = new StringBuilder();
            StringBuilder parames = new StringBuilder();

            bool first = true;

            foreach (PropertyInfo pi in propertys)
            {
                if (!FieldAttribute.IsIdentity(pi))
                {
                    Type pp = pi.PropertyType;
                    object value = pi.GetValue(entity, null);
                    if (pp.IsValueType)
                    {
                        if (value == null)
                        {
                            continue;
                        }
                    }

                    if (!first)
                    {
                        fields.Append(",");
                        parames.Append(",");
                    }
                    else
                    {
                        first = false;
                    }


                    fields.Append(FieldAttribute.GetFieldName(pi));


                    parames.Append("@");
                    parames.Append(pi.Name);

                    list.Add(db.CreateParameter("@" + pi.Name, value));
                }
            }


            string sql = "insert into " + tableName + "(" + fields + ") values(" + parames + ");select @@identity;";

            return db.ExecuteScalar<RT>(sql, list.ToArray());
        }


    }





}