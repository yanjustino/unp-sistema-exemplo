using System;
using FluentMigrator;

namespace UnpDataMigration
{
    [Migration(3)]
    public class Inscricao_23012019 : Migration
    {
        private readonly string NomeDaTabela = "Inscricao";

        public override void Down()
        {
            Delete.Table(NomeDaTabela);
        }

        public override void Up()
        {
            if (Schema.Table(NomeDaTabela).Exists())
                Delete.Table(NomeDaTabela);

            Create
                .Table(NomeDaTabela)
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("CandidatoId").AsInt64().ForeignKey("Candidato", "Id")
                .WithColumn("CursoId").AsInt64().ForeignKey("Curso", "Id")
                .WithColumn("BolsaEstudo").AsBoolean().NotNullable()
                .WithColumn("Situacao").AsString(10).NotNullable()
                .WithColumn("DataInscricao").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
        }
    }
}
