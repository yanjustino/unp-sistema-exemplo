using System;
namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public interface IRepositorioCandidato
    {
        Candidato RecuperarPorId(long id);
    }
}
