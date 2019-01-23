using System;
using Unp.Sistema.Core;
using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class Inscricao : Agregado
    {
        public Candidato Candidato { get; private set; }
        public Curso CursoPretendido { get; private set; }
        public SituacaoCandidato Situacao { get; private set; } = SituacaoCandidato.Nenhuma;
        public DateTime DataInscricao { get; private set; }
        public bool BolsaEstudo { get; private set; }

        private Inscricao()
        {
        }

        public Inscricao(Candidato candidato, Curso cursoPretendido)
        {
            Candidato = candidato;
            Situacao = SituacaoCandidato.Nenhuma;
            CursoPretendido = cursoPretendido;
            DataInscricao = DateTime.Today;
            BolsaEstudo = false;
        }

        public void AprovarCandidato()
        {
            if (Situacao == SituacaoCandidato.Reprovado)
                throw new InvalidOperationException("Candidato Já está reprovado");

            Situacao = SituacaoCandidato.Aprovado;
        }

        public void Reprovar()
        {
            if (Situacao == SituacaoCandidato.Aprovado)
                throw new InvalidOperationException("Candidato Já está Aprovado");

            Situacao = SituacaoCandidato.Reprovado;
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
                    Situacao = SituacaoCandidato.Nenhuma,
                    CursoPretendido = cursoPretendido,
                    DataInscricao = DateTime.Today,
                    BolsaEstudo = false
                };

                if (!inscricao.Candidato.MaiorIdade())
                    throw new InvalidOperationException("Candidato de menor");

                return inscricao;
            }
        }
    }
}