using System;
using Unp.Sistema.Dominio;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class Inscricao
    {

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CursoPretendido { get; private set; }
        public Cpf Cpf { get; private set; }


        public Inscricao(string nome, string email, string cursoPretendido, string cpf)
        {
            Nome = nome;
            Email = email;
            CursoPretendido = cursoPretendido;
            Cpf = new Cpf(cpf);
        }
    }
}
