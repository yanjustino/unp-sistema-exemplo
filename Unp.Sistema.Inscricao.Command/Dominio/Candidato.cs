using System;
using Unp.Sistema.Core;
using Unp.Sistema.Dominio;
using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class Candidato : Entidade
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateTime DataNascimento { get; set; }

        public Candidato(RegistroDeInscricaoNovoCandidato command)
        {
            Nome = command.Nome;
            Email = command.Email;
            Cpf = new Cpf(command.Cpf);
            DataNascimento = command.DataNacimento;
        }

        public bool MaiorIdade()
        {
            return DateTime.Today.Year - DataNascimento.Year >= 18;
        }
    }
}
