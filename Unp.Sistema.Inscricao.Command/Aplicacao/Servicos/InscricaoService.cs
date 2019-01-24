using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;
using Unp.Sistema.Inscricao.Command.Dominio;

namespace Unp.Sistema.Inscricao.Command.Aplicacao.Servicos
{
    public class InscricaoService
    {
        private readonly IRepositorioInscricao _repositorioInscricao;
        private readonly ServicoDeVerificacaoDeBolsaDeEstudo _servicoDeVerificacaoDeBolsaDeEstudo;

        public InscricaoService(
            IRepositorioInscricao repositorioInscricao,
            ServicoDeVerificacaoDeBolsaDeEstudo servicoDeVerificacaoDeBolsaDeEstudo)
        {
            _repositorioInscricao = repositorioInscricao;
            _servicoDeVerificacaoDeBolsaDeEstudo = servicoDeVerificacaoDeBolsaDeEstudo;
        }

        public void Executar(AprovacaoCandidato command)
        {
            var inscricao = _repositorioInscricao.RecuperarPorId(command.InscricaoId);

            inscricao.AprovarCandidato();

            //TODO : verificar se o modelo tá válido

            _repositorioInscricao.Salvar(inscricao);
        }

        public void Executar(RegistroDeInscricaoNovoCandidato command)
        {
            var curso = _repositorioInscricao.RecuperarCursoPorId(command.CursoId);
            var inscricao = Dominio.Inscricao.Fabrica.NovaInscricao(command, curso);

            if (_servicoDeVerificacaoDeBolsaDeEstudo.TentarAplicarBolsaEstudo(inscricao.Candidato, curso))
                inscricao.LiberarBolsaEstudo();

            _repositorioInscricao.Salvar(inscricao);
        }

        public void Executar(SolicitacaoDeNovaInscricao command)
        {
            var curso = _repositorioInscricao.RecuperarCursoPorId(command.CursoId);
            var candidato = _repositorioInscricao.RecuperarCandidatoPorId(command.CandidatoId);
            var inscricao = Dominio.Inscricao.Fabrica.NovaInscricao(candidato, curso);

            if (_servicoDeVerificacaoDeBolsaDeEstudo.TentarAplicarBolsaEstudo(candidato, curso))
                inscricao.LiberarBolsaEstudo();

            _repositorioInscricao.Salvar(inscricao);
        }
    }
}
