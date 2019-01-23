using System;
namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public interface IServicoProgramaBolsa
    {
        bool Aplicar(Candidato candidato);
    }
}
