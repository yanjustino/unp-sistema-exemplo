using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;
using Unp.Sistema.Inscricao.Command.Dominio;

namespace Unp.Sistema.Inscricao.Command.Aplicacao.Servicos
{
    public class InscricaoService
    {
        private readonly IRepositorioCursos _repositorioDeCursos;
        private readonly IRepositorioInscricao _repositorioInscricao;
        private readonly IRepositorioCandidato _repositorioCandidato;
        private readonly ServicoDeVerificacaoDeBolsaDeEstudo _servicoDeVerificacaoDeBolsaDeEstudo;

        public InscricaoService(
            IRepositorioCursos repositorioDeCursos,
            IRepositorioInscricao repositorioInscricao,
            IRepositorioCandidato repositorioCandidato,
            ServicoDeVerificacaoDeBolsaDeEstudo servicoDeVerificacaoDeBolsaDeEstudo)
        {
            _repositorioDeCursos = repositorioDeCursos;
            _repositorioInscricao = repositorioInscricao;
            _repositorioCandidato = repositorioCandidato;
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
            var curso = _repositorioDeCursos.RecuperarPorId(command.CursoId);
            var inscricao = Dominio.Inscricao.Fabrica.NovaInscricao(command, curso);

            if (_servicoDeVerificacaoDeBolsaDeEstudo.TentarAplicarBolsaEstudo(inscricao.Candidato, curso))
                inscricao.LiberarBolsaEstudo();

            _repositorioInscricao.Salvar(inscricao);
        }

        public void Executar(SolicitacaoDeNovaInscricao command)
        {
            var curso = _repositorioDeCursos.RecuperarPorId(command.CursoId);
            var candidato = _repositorioCandidato.RecuperarPorId(command.CandidatoId);
            var inscricao = new Dominio.Inscricao(candidato, curso);

            if (_servicoDeVerificacaoDeBolsaDeEstudo.TentarAplicarBolsaEstudo(candidato, curso))
                inscricao.LiberarBolsaEstudo();

            _repositorioInscricao.Salvar(inscricao);
        }
    }
}
