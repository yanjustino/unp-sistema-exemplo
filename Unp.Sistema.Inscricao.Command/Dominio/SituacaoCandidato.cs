using System;
using Unp.Sistema.Core;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class SituacaoCandidato : Enumeracao
    {
        public static SituacaoCandidato Nenhuma => new SituacaoCandidato(-1, "");
        public static SituacaoCandidato Aprovado => new SituacaoCandidato(0, "Aprovado");
        public static SituacaoCandidato Reprovado => new SituacaoCandidato(1, "Reprovado");
        public static SituacaoCandidato Suplente => new SituacaoCandidato(2, "Suplente");

        protected SituacaoCandidato()
        {
        }

        protected SituacaoCandidato(int value, string displayName) : base(value, displayName)
        {
        }
    }
}
