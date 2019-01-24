using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Unp.Sistema.Inscricao.Command.Dominio;

namespace Unp.Sistema.Inscricao.Infraestrutura.Repositorios
{
    public class RepositorioInscricao : BaseRepositorio, IRepositorioInscricao
    {
        public RepositorioInscricao(string connectionString) : base(connectionString)
        {
        }

        public Candidato RecuperarCandidatoPorId(long id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return dbConnection.Query<Candidato>(
                    "Select * from Candidato where Id = @Id",
                    new { Id = id }).Single();
            }
        }

        public Command.Dominio.Inscricao RecuperarPorId(long id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                var query = "select * Form Inscricao where id=@Id";

                var inscricao = dbConnection.Query(query, new { Id = id })
                    .Cast<IDictionary<string, object>>()
                    .Single();

                var candidato = RecuperarCandidatoPorId((long)inscricao["CandidatoId"]);
                var curso = RecuperarCursoPorId((long)inscricao["CursoId"]);

                return Command.Dominio.Inscricao.Fabrica.Carregar
                (
                    (long)inscricao["Id"],
                    (DateTime)inscricao["DataInscricao"],
                    (string)inscricao["Situacao"],
                    (bool)inscricao["BolsaEstudo"],
                    candidato,
                    curso
                );
            }

            throw new NotImplementedException();
        }

        public void Salvar(Command.Dominio.Inscricao inscricao)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            using (IDbTransaction dbTransaction = dbConnection.BeginTransaction())
            {
                try
                {
                    if (inscricao.Transitorio())
                        InserirInscricao(inscricao, dbConnection, dbTransaction);
                    else
                        AtualizarInscricao(inscricao, dbConnection, dbTransaction);

                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        void InserirInscricao(Command.Dominio.Inscricao inscricao, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var candidatoId = InserirCandidato(inscricao.Candidato, dbConnection, dbTransaction);

            string sQueryB = "INSERT INTO Inscricao ( Id, CandidatoId, CursoId, Situacao, BolsaEstudo ) " +
                            "VALUES(@Id, @CandidatoId, @CursoId, @Situacao, @BolsaEstudo )";

            dbConnection.Execute(sQueryB, new
            {
                inscricao.Id,
                CandidatoId = candidatoId,
                CursoId = inscricao.CursoPretendido.Id,
                inscricao.Situacao,
                inscricao.BolsaEstudo
            }, dbTransaction);
        }

        int InserirCandidato(Candidato candidato, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            string sQuery = "INSERT INTO Candidato (Id, Nome, Email, Cpf, DataNascimento) " +
                            "VALUES(@Id, @Nome, @Email, @Cpf, @DataNascimento); " +
                            "SELECT CAST(SCOPE_IDENTITY() as int)";

            return dbConnection.Execute(sQuery, candidato, dbTransaction);
        }

        void AtualizarInscricao(Command.Dominio.Inscricao inscricao, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            string sQueryB = "UPDATE Inscricao SET Id = @Id, CandidatoId = @CandidatoId, CursoId = @CursoId, Situacao = @Situacao, BolsaEstudo = @BolsaEstudo ) " +
                             "WHERE Id = @Id";

            dbConnection.Execute(sQueryB, new
            {
                inscricao.Id,
                CandidatoId = inscricao.Candidato.Id,
                CursoId = inscricao.CursoPretendido.Id,
                inscricao.Situacao,
                inscricao.BolsaEstudo
            }, dbTransaction);
        }

        public Curso RecuperarCursoPorId(long id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return dbConnection.Query<Curso>("Select * from Curso where Id = @Id",
                    new { Id = id }).Single();
            }
        }
    }
}
