using System;
using FluentMigrator;

namespace UnpDataMigration
{
    [Migration(2)]
    public class Curso_23012019 : Migration
    {
        private readonly string NomeDaTabela = "Curso";

        public override void Down()
        {
            Delete.Table(NomeDaTabela);
        }

        public override void Up()
        {
            if (Schema.Table(NomeDaTabela).Exists())
            {
                Delete.ForeignKey("FK_Inscricao_CursoId_Curso_Id").OnTable("Inscricao");
                Delete.Column("CursoId").FromTable("Inscricao");

                Delete.Table(NomeDaTabela);
            }

            Create
                .Table(NomeDaTabela)
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(200).NotNullable()
                .WithColumn("PermiteBolsa").AsBoolean().NotNullable();

            InserirCursos();
        }

        private void InserirCursos()
        {
            Insert
                .IntoTable(NomeDaTabela)
                .Row(new { Nome = "Curso 01", PermiteBolsa = false })
                .Row(new { Nome = "Curso 02", PermiteBolsa = false })
                .Row(new { Nome = "Curso 03", PermiteBolsa = false })
                .Row(new { Nome = "Curso 04", PermiteBolsa = false })
                .Row(new { Nome = "Curso 05", PermiteBolsa = true });
        }
    }
}
