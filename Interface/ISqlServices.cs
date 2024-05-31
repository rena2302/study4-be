using Microsoft.Data.SqlClient;
using System.Data;

namespace study4_be.Interface
{
    public interface ISqlService
    {
        SqlParameter CreateOutputParameter(string name, SqlDbType type);
        SqlParameter CreateOutputParameter(string name, SqlDbType type, int size);

        (int, string) ExecuteNonQuery(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
        Task<(int, string)> ExecuteNonQueryAsync(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
        (DataTable, string) FillDataTable(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
        Task<(DataTable, string)> FillDataTableAsync(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
    }
}
