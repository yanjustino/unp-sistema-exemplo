using System;
namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public interface IRepositorioCursos
    {
        Curso RecuperarPorId(long id);
    }
}
