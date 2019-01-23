using System;
using FluentMigrator;

namespace UnpDataMigration
{
    [Migration(1)]
    public class Candidato_23012019 : Migration
    {
        private readonly string NomeDaTabela = "Candidato";


        public override void Down()
        {
            Delete.Table(NomeDaTabela);
        }

        public override void Up()
        {
            if (Schema.Table(NomeDaTabela).Exists())
                Delete.Table(NomeDaTabela);

            if (Schema.Table(NomeDaTabela).Exists())
            {
                Delete.ForeignKey("FK_Inscricao_CandidatoId_Candidato_Id").OnTable("Inscricao");
                Delete.Column("CandidatoId").FromTable("Inscricao");

                Delete.Table(NomeDaTabela);
            }

            Create
                .Table(NomeDaTabela)
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Cpf").AsString(11).NotNullable()
                .WithColumn("DataNascimento").AsDateTime().NotNullable();
        }
    }
}
