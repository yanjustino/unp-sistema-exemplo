using System;
namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public interface IRepositorioInscricao
    {
        void Salvar(Inscricao inscricao);

        Inscricao RecuperarPorId(long id);
    }
}
