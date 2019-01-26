using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Unp.Sistema.Inscricao.Query.Results;

namespace Unp.Sistema.Inscricao.Query.Queries
{
    public class CandidatoQuery : QueryBase
    {
        public CandidatoQuery(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<ListaEmailCandidatos> RecuperarListaEmailCandidatos(long id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return dbConnection.Query<ListaEmailCandidatos>(
                    "Select Nome, Email from Candidato where Id = @Id",
                    new { Id = id });
            }
        }
    }
}
