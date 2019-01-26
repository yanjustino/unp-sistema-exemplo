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
        }

        public void Salvar(Command.Dominio.Inscricao inscricao)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        void InserirInscricao(Command.Dominio.Inscricao inscricao, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var candidatoId = InserirCandidato(inscricao.Candidato, dbConnection, dbTransaction);

            string sQueryB = "INSERT INTO Inscricao ( CandidatoId, CursoId, Situacao, BolsaEstudo ) " +
                            "VALUES( @CandidatoId, @CursoId, @Situacao, @BolsaEstudo )";

            dbConnection.Execute(sQueryB, new
            {
                CandidatoId = candidatoId,
                CursoId = inscricao.CursoPretendido.Id,
                inscricao.Situacao,
                inscricao.BolsaEstudo
            }, dbTransaction);
        }

        long InserirCandidato(Candidato candidato, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            string sQuery = "INSERT INTO Candidato (Nome, Email, Cpf, DataNascimento) " +
                            "VALUES(@Nome, @Email, @Cpf, @DataNascimento); " +
                            "SELECT CAST(SCOPE_IDENTITY() as int)";

            return dbConnection.Query<long>(sQuery,
            new
            {
                candidato.Nome,
                candidato.Email,
                Cpf = candidato.Cpf.Numero,
                candidato.DataNascimento
            },
            dbTransaction).Single();
        }

        void AtualizarInscricao(Command.Dominio.Inscricao inscricao, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            string sQueryB = "UPDATE Inscricao SET CandidatoId = @CandidatoId, CursoId = @CursoId, Situacao = @Situacao, BolsaEstudo = @BolsaEstudo " +
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
