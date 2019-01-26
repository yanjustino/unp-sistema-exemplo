using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unp.Sistema.Inscricao.Command.Aplicacao.Comandos;
using Unp.Sistema.Inscricao.Command.Aplicacao.Servicos;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unp.Sistema.Inscricao.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class InscricoesController : Controller
    {
        private readonly InscricaoService _service;

        public InscricoesController(InscricaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public ActionResult Post([FromBody]RegistroDeInscricaoNovoCandidato command)
        {
            _service.Executar(command);

            if (_service.Invalid)
                return BadRequest(_service.Notifications);
            else
                return Ok();
        }
    }
}
