using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;
using Unp.Sistema.Inscricao.Command.Aplicacao.Servicos;
using Unp.Sistema.Inscricao.Command.Dominio;

namespace Unp.Sistema.Inscricao.Testes
{

    [TestClass]
    public class ServicoInscricaoTeste
    {
        public ServicoInscricaoTeste()
        {
        }

        [TestMethod]
        public void TesteNovaInscricao()
        {
            var commando = new RegistroDeInscricaoNovoCandidato
            {
                CursoId = 1,
                DataNascimento = new System.DateTime(1980, 1, 6),
                Email = "teste@teste.com",
                Nome = "Yan de Lima Justino",
                Cpf = "11111111111"
            };

            var mock = new Mock<IRepositorioInscricao>();

            mock.Setup(framework => framework.Salvar(null));

            IRepositorioInscricao irepositorio = mock.Object;

            InscricaoService service = new InscricaoService(irepositorio, new ServicoDeVerificacaoDeBolsaDeEstudo());
            service.Executar(commando);

            Assert.IsTrue(service.Valid);

            mock.Verify(framework => framework.Salvar(null), Times.AtMostOnce());
        }
    }
}
