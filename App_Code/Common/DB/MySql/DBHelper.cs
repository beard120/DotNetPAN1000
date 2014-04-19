namespace Common.DB.MySql
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using global::MySql.Data.MySqlClient;

    /// <summary>
    ///SQLServerHandler 的摘要说明
    /// </summary>
    public class DBHelper : IDBHelper
    {
        string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public DBHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbCommand CreateCommand()
        {
            return new MySqlCommand();

            
        }

        public DbConnection CreateConnection()
        {

            return new MySqlConnection(connectionString);
        }


        public DbParameter CreateParameter(string name, object value)
        {
            DbParameter parameter = CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public DbParameter CreateParameter(string name)
        {
            DbParameter parameter = CreateParameter();
            parameter.ParameterName = name;
            return parameter;
        }

        public DbParameter CreateParameter()
        {
            return new MySqlParameter();
        }


        public T ExecuteScalar<T>(string sql, params DbParameter[] parameters)
        {

            using (DbConnection connection = CreateConnection())
            {
                DbCommand cmd = CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                connection.Open();
                object o = cmd.ExecuteScalar();
                connection.Close();
                return (T)Convert.ChangeType(o, typeof(T));
            }
        }


        public DbDataReader ExecuteReader(string sql, params DbParameter[] parameters)
        {
            DbConnection connection = CreateConnection();

            DbCommand cmd = CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(parameters);
            connection.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteNoneQuery(string sql, params DbParameter[] parameters)
        {
            using (DbConnection connection = CreateConnection())
            {

                DbCommand cmd = CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                connection.Open();
                int num = cmd.ExecuteNonQuery();
                connection.Close();
                return num;
            }
        }

        public List<Hashtable> GetDataList(string sql, params DbParameter[] parameters)
        {
            List<Hashtable> list = new List<Hashtable>();
            using (DbDataReader reader = ExecuteReader(sql, parameters))
            {
                while (reader.Read())
                {
                    Hashtable hs = new Hashtable();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        hs[reader.GetName(i)] = reader.GetValue(i);
                    }

                    list.Add(hs);
                }
                reader.Close();
                reader.Dispose();
            }

            return list;
        }

        public Hashtable GetData(string sql, params DbParameter[] parameters)
        {
            Hashtable hs = null;
            using (DbDataReader reader = ExecuteReader(sql, parameters))
            {
                while (reader.Read())
                {
                    hs = new Hashtable();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        hs[reader.GetName(i)] = reader.GetValue(i);
                    }
                }
                reader.Close();
                reader.Dispose();
            }

            return hs;
        }



    }


}