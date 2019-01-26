using System;
namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class ServicoDeVerificacaoDeBolsaDeEstudo
    {

        public ServicoDeVerificacaoDeBolsaDeEstudo()
        {
        }

        public bool TentarAplicarBolsaEstudo(Candidato candidato, Curso curso)
        {
            if (curso.PermiteBolsa)
                return true;
            else
                return false;
        }
    }
}
