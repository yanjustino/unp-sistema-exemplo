using System;
using Unp.Sistema.Core;
using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class Inscricao : Agregado
    {
        public Candidato Candidato { get; private set; }
        public Curso CursoPretendido { get; private set; }
        public string Situacao { get; private set; } = SituacaoCandidato.Nenhuma.DisplayName;
        public DateTime DataInscricao { get; private set; }
        public bool BolsaEstudo { get; private set; }

        public Inscricao()
        {
        }

        public void AprovarCandidato()
        {
            if (Situacao == SituacaoCandidato.Reprovado.DisplayName)
                throw new InvalidOperationException("Candidato Já está reprovado");

            Situacao = SituacaoCandidato.Aprovado.DisplayName;
        }

        public void Reprovar()
        {
            if (Situacao == SituacaoCandidato.Aprovado.DisplayName)
                throw new InvalidOperationException("Candidato Já está Aprovado");

            Situacao = SituacaoCandidato.Reprovado.DisplayName;
        }

        public void LiberarBolsaEstudo()
        {
            BolsaEstudo = true;
        }

        public static class Fabrica
        {
            public static Inscricao NovaInscricao(RegistroDeInscricaoNovoCandidato comando, Curso cursoPretendido)
            {
                var inscricao = new Inscricao()
                {
                    Candidato = new Candidato(comando),
                    Situacao = SituacaoCandidato.Nenhuma.DisplayName,
                    CursoPretendido = cursoPretendido,
                    DataInscricao = DateTime.Today,
                    BolsaEstudo = false
                };

                if (!inscricao.Candidato.MaiorIdade())
                    throw new InvalidOperationException("Candidato de menor");

                return inscricao;
            }

            public static Inscricao NovaInscricao(Candidato candidato, Curso cursoPretendido)
            {
                var inscricao = new Inscricao()
                {
                    Candidato = candidato,
                    Situacao = SituacaoCandidato.Nenhuma.DisplayName,
                    CursoPretendido = cursoPretendido,
                    DataInscricao = DateTime.Today,
                    BolsaEstudo = false
                };

                if (!inscricao.Candidato.MaiorIdade())
                    throw new InvalidOperationException("Candidato de menor");

                return inscricao;
            }

            public static Inscricao Carregar(long id, DateTime dataInscricao, string situacao, bool bolsaEstudo, Candidato candidato, Curso cursoPretendido)
            {
                return new Inscricao()
                {
                    Id = id,
                    Candidato = candidato,
                    Situacao = situacao,
                    CursoPretendido = cursoPretendido,
                    DataInscricao = dataInscricao,
                    BolsaEstudo = bolsaEstudo
                };
            }
        }
    }
}