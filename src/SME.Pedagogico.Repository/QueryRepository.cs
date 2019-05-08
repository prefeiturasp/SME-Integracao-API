using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SME.Pedagogico.Repository
{
    public class QueryRepository
    {
        protected T QueryFirstOrDefaultSQL<T>(string connectionString, string query, object param)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.QueryFirstOrDefault<T>(query, param);
            }
        }

        protected IEnumerable<T> QueryCollectionSQL<T>(string connectionString, string query, object param)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<T>(query, param);
            }
        }

        protected T QueryFirstOrDefaultSQLParameterless<T>(string connectionString, string query)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.QueryFirstOrDefault<T>(query);
            }
        }

        protected IEnumerable<T> QueryCollectionSQLParameterless<T>(string connectionString, string query)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<T>(query);
            }
        }

        protected T QueryFirstOrDefaultPostgres<T>(string connectionString, string query, object param)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                return conn.Query<T>(query, param).FirstOrDefault();
            }
        }

        protected string QueryConstructor(string query, string where)
        {
            var sb = new StringBuilder();
            sb.Append(query);
            sb.AppendLine(where);

            return sb.ToString();
        }
    }
}
