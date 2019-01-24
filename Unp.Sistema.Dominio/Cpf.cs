using System;
using Flunt.Validations;
using Unp.Sistema.Core;

namespace Unp.Sistema.Dominio
{
    public class Cpf : ObjetoDeValor<Cpf>, IValidatable
    {
        public string Numero { get; }
        public string NumeroComMascara { get; }

        public Cpf(string numero)
        {
            Numero = numero;
            NumeroComMascara = Formatar(numero);
        }

        string Formatar(string numero) => numero;

        protected override bool EqualsCore(Cpf other)
        {
            return Numero == other.Numero;
        }

        protected override int GetHashCodeCore()
        {
            return Numero.GetHashCode();
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsLowerThan(11, Numero.Length, "CPF", "Cpf Inválido")
            );
        }
    }
}
