using System;
using Flunt.Validations;
using Unp.Sistema.Core;
using Unp.Sistema.Dominio;
using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;

namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class Candidato : Entidade, IValidatable
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateTime DataNascimento { get; set; }

        public Candidato()
        {

        }

        public Candidato(RegistroDeInscricaoNovoCandidato command)
        {
            Nome = command.Nome;
            Email = command.Email;
            Cpf = new Cpf(command.Cpf);
            DataNascimento = command.DataNacimento;
        }

        public void Validate()
        {
            Cpf.Validate();
            if (Cpf.Invalid)
                AddNotifications(Cpf);

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, "Nome", "Nome Obrigatorio")
                .IsGreaterThan(18, (DateTime.Today.Year - DataNascimento.Year), "DataNascimento", "Deve ser maior idade")
            );
        }
    }
}
