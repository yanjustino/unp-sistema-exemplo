using System;
namespace Unp.Sistema.Inscricao.Query
{
    public class QueryBase
    {
        public string ConnectionString { get; }

        public QueryBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
