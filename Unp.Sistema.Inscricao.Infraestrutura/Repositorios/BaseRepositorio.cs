using System;
namespace Unp.Sistema.Inscricao.Infraestrutura.Repositorios
{
    public class BaseRepositorio
    {
        protected string ConnectionString { get; }

        public BaseRepositorio(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
