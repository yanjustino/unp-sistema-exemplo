namespace Unp.Sistema.Core
{
    public interface IManipuladorDeEvento<T>
        where T : IEventoDeDominio
    {
        void Executar(T eventoDeDominio);
    }
}