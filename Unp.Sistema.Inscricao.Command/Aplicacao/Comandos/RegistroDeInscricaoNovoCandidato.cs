using System;
using Unp.Sistema.Inscricao.Command.Dominio;

namespace Unp.Sistema.Inscricao.Command.Aplicacao.Comandos
{
    //string nome, string email, Curso cursoPretendido, string cpf, DateTime dataNascimento

    public class RegistroDeInscricaoNovoCandidato
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public long CursoId { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }


}
