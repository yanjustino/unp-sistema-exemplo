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
        public DateTime DataNacimento { get; set; }
    }

    public class RegistroDeInscricaoDeCandidatoExistente
    {
        public long CandidatoId { get; set; }
        public long CursoPretendido { get; set; }
    }
}
