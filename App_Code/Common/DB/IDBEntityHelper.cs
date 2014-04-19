
namespace Common.DB
{
    using System.Collections.Generic;
    using System.Data.Common;

    public interface IDBEntityHelper
    {
        IDBHelper DBHelper { get; set; }

        T GetEntityBySql<T>(string sql, params DbParameter[] parameters);

        T GetEntity<T>(int id);

        T GetEntity<T, IDT>(IDT id);

        T GetEntityById<T, IDT>(IDT id);

        T GetEntity<T>(string where, params DbParameter[] parameters);

        T GetEntity<T>(string where, string sort, params DbParameter[] parameters);

        T GetEntityByWhere<T>(string where, params DbParameter[] parameters);

        T GetEntityByWhere<T>(string where, string sort, params DbParameter[] parameters);

        List<T> GetDataList<T>(string queryString, params DbParameter[] parameters);

        List<T> GetDataList<T>(int num, string where, string sort, params DbParameter[] parameters);

        List<T> GetDataList<T>();

        List<T> GetPageDataList<T>(int pageNumber, int pageSize, string where, string sort, out int recordCount, params DbParameter[] parameters);

        bool UpdateData<T>(T entity);

        bool InsertData<T>(T entity);

        RT InsertDataAndGetID<T, RT>(T entity);
    }

}