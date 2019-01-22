using System;
using Unp.Sistema.Core;

namespace Unp.Sistema.Dominio
{
    public class Cpf : ObjetoDeValor<Cpf>
    {
        public string Numero { get; }
        public string NumeroComMascara { get; }

        public Cpf(string numero)
        {
            Numero = numero;
            NumeroComMascara = Formatar(numero);
        }

        bool Valido()
        {
            //TODO : programar lógica
            return true;
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
    }
}
