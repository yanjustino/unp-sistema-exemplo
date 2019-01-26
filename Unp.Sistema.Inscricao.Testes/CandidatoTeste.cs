using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;
using Unp.Sistema.Inscricao.Command.Dominio;

namespace Unp.Sistema.Inscricao.Testes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CpfCandidatoInvalido()
        {
            var commando = new RegistroDeInscricaoNovoCandidato
            {
                CursoId = 1,
                DataNascimento = new System.DateTime(2019, 1, 24),
                Email = "teste@teste.com",
                Nome = "",
                Cpf = "1"
            };

            var candidato = new Candidato(commando);
            candidato.Validate();

            Assert.IsTrue(candidato.Invalid, string.Join(',', candidato.Notifications));
        }
    }
}
