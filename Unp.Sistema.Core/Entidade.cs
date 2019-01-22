using System;

namespace Unp.Sistema.Core
{
    public abstract class Entidade
    {
        public virtual long Id { get; protected set; }

        //protected Entidade()
        //{
        //}

        //public Entidade(Identidade id)
        //    : this()
        //{
        //    Id = id;
        //}

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
                //Id.Id == entidadeComparacao.Id.Id)
                return true;

            if (Transitorio() || entidadeComparacao.Transitorio())
                return false;

            return Id == entidadeComparacao.Id;
            //return Id.Id == entidadeComparacao.Id.Id;
        }

        public virtual bool Transitorio()
        {
            return Id == default(long);
            //return Id.Id == default(Guid);
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
            //return (GetType().ToString() + Id.Id).GetHashCode();
        }

    }
}