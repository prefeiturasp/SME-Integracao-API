using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SME.Pedagogico.Interface.Autenticacao;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.DREs;
using SME.Pedagogico.Interface.Paginador;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SME.Pedagogico.Interface.Logs;
using System.Threading.Tasks;

namespace SME.Pedagogico.WebAPI.Controllers
{
    [Route("api/DREs")]
    [ApiController]
    public class DiretoriaRegionalEducacaoController : Controller
    {
        private readonly IDiretoriaRegionalEducacaoService dreService;
        private readonly IAutenticacaoService autenticacaoService;
        private readonly ILogService logService;

        public DiretoriaRegionalEducacaoController(IAutenticacaoService autenticacaoService,
                                IDiretoriaRegionalEducacaoService dreService,
                                ILogService logService)
        {
            this.autenticacaoService = autenticacaoService;
            this.dreService = dreService;
            this.logService = logService;
        }

        [HttpGet("{codigoDRE}/escolas/{tipoEscola}")]
        [HttpGet("{codigoDRE}/escolas/")]
        [HttpGet("escolas/{tipoEscola}")]
        public IActionResult BuscaEscolasPor(string codigoDRE, 
                                             string tipoEscola, 
                                 [FromHeader]string token)
        {
            string username = string.Empty;
            var tokenValido = autenticacaoService.IsValido(token, out username);
            
            if (tokenValido)
            {
                var method = MethodBase.GetCurrentMethod();
                logService.GravaLog(metodo: method.Name,
                                    servico: method.ReflectedType.Name,
                                    parametros: $"Codigo DRE: {codigoDRE} / TipoEscola: {tipoEscola}",
                                    usuario: username);

                var escolasPorDRE = dreService.BuscarEscolasPor(codigoDRE, tipoEscola);

                if (escolasPorDRE == default(IReadOnlyList<EscolasPorDREDTO>))
                {
                    return BadRequest();
                }
                else
                if (escolasPorDRE.Count == 0)
                {
                    return NotFound();
                }

                return Ok(escolasPorDRE);
            }

            return Unauthorized();
        }     
    }
}