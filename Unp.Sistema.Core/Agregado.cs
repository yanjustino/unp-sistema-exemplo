using System.Collections.Generic;

namespace Unp.Sistema.Core
{
    public class Agregado : Entidade
    {
        private readonly List<IEventoDeDominio> _eventosDeDominio = new List<IEventoDeDominio>();
        public virtual IReadOnlyList<IEventoDeDominio> EventosDeDominio => _eventosDeDominio;

        //protected Agregado()
        //{
        //}

        //public Agregado(Identidade id) : base(id)
        //{
        //}

        protected virtual void AdicionarEventosDeDominio(IEventoDeDominio evento)
        {
            _eventosDeDominio.Add(evento);
        }

        public virtual void LimparEventos()
        {
            _eventosDeDominio.Clear();
        }
    }
}
