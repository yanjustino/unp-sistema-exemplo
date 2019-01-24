using System;
using Flunt.Notifications;

namespace Unp.Sistema.Core
{
    public abstract class Entidade : Notifiable
    {
        public virtual long Id { get; protected set; }

        public override bool Equals(object entidade)
        {
            var entidadeComparacao = entidade as Entidade;

            if (ReferenceEquals(entidadeComparacao, null))
                return false;

            if (ReferenceEquals(this, entidadeComparacao))
                return true;

            if (GetType() != entidadeComparacao.GetType())
                return false;

            if (!Transitorio() &&
                !entidadeComparacao.Transitorio() &&
                Id == entidadeComparacao.Id)
                return true;

            if (Transitorio() || entidadeComparacao.Transitorio())
                return false;

            return Id == entidadeComparacao.Id;
        }

        public virtual bool Transitorio()
        {
            return Id == default(long);
        }

        public static bool operator ==(Entidade entidadeA, Entidade entidadeB)
        {
            if (ReferenceEquals(entidadeA, null) && ReferenceEquals(entidadeB, null))
                return true;

            if (ReferenceEquals(entidadeA, null) || ReferenceEquals(entidadeB, null))
                return false;

            return entidadeA.Equals(entidadeB);
        }

        public static bool operator !=(Entidade entidadeA, Entidade entidadeB)
        {
            return !(entidadeA == entidadeB);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

    }
}