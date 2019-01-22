using System;
using Unp.Sistema.Core;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class SituacaoCandidato : Enumeracao
    {
        public static SituacaoCandidato Nenhuma => new SituacaoCandidatoEnum(-1, "");
        public static SituacaoCandidato Aprovado => new SituacaoCandidatoEnum(0, "Aprovado");
        public static SituacaoCandidato Reprovado => new SituacaoCandidatoEnum(1, "Reprovado");
        public static SituacaoCandidato Suplente => new SituacaoCandidatoEnum(2, "Suplente");

        protected SituacaoCandidato()
        {
        }

        protected SituacaoCandidato(int value, string displayName) : base(value, displayName)
        {
        }
    }
}
