using System;
using Unp.Sistema.Core;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class Curso : Entidade
    {
        public string Nome { get; private set; }

        public Curso(string nome)
        {
            Nome = nome;
        }
    }
}
