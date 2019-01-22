using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unp.Sistema.Dominio.Teste
{
    [TestClass]
    public class CpfTeste
    {
        [TestMethod]
        public void Teste_de_igualdade()
        {
            var cpf1 = new Cpf("11111111111");
            var cpf2 = new Cpf("22222222222");

            Assert.IsFalse(cpf1 == cpf2);
        }
    }
}
