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

        private Inscricao()
        {
        }

        public Inscricao(Candidato candidato, Curso cursoPretendido, SituacaoCandidato situacao)
        {
            Candidato = candidato;
            Situacao = situacao;
            CursoPretendido = cursoPretendido;
        }

        public void Aprovar()
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

        public static class Fabrica
        {
            public static Inscricao NovaInscricao(RegistroDeInscricaoNovoCandidato comando, Curso cursoPretendido)
            {
                var inscricao = new Inscricao()
                {
                    Candidato = new Candidato(comando),
                    Situacao = SituacaoCandidato.Nenhuma,
                    CursoPretendido = cursoPretendido
                };

                if (!inscricao.Candidato.MaiorIdade())
                    throw new InvalidOperationException("Candidato de menor");

                return inscricao;
            }
        }
    }
}