using System;
namespace Unp.Sistema.Inscricao.Command.Dominio
{
    public class ServicoDeVerificacaoDeBolsaDeEstudo
    {
        private IServicoProgramaBolsa _servicoProgramaBolsa;

        public ServicoDeVerificacaoDeBolsaDeEstudo(IServicoProgramaBolsa servicoProgramaBolsa)
        {
            _servicoProgramaBolsa = servicoProgramaBolsa;
        }

        public bool TentarAplicarBolsaEstudo(Candidato candidato, Curso curso)
        {
            if (curso.PermiteBolsa)
                return _servicoProgramaBolsa.Aplicar(candidato);
            else
                return false;
        }
    }
}
