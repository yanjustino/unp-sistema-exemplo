using System;
using Flunt.Validations;
using Unp.Sistema.Core;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class Curso : Entidade, IValidatable
    {
        public string Nome { get; private set; }
        public bool PermiteBolsa { get; private set; }

        public Curso()
        {

        }

        public Curso(string nome, bool permiteBolsa = false)
        {
            Nome = nome;
            PermiteBolsa = permiteBolsa;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNull(Nome, "Nome", "O nome deve ser informado")
            );
        }
    }
}
