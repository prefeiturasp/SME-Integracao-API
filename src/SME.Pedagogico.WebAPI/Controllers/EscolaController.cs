using Microsoft.AspNetCore.Mvc;
using SME.Pedagogico.Interface.Autenticacao;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.Escolas;
using SME.Pedagogico.Interface.Extensions;
using SME.Pedagogico.Interface.Logs;
using System.Reflection;
using System.Threading.Tasks;

namespace SME.Pedagogico.WebAPI.Controllers
{
    [Route("api/escolas")]
    [ApiController]
    public class EscolaController : Controller
    {
        private readonly IEscolaService escolaService;
        private readonly IAutenticacaoService autenticacaoService;
        private readonly ILogService logService;

        public EscolaController(IAutenticacaoService autenticacaoService,
                                IEscolaService escolaService,
                                ILogService logService)
        {
            this.autenticacaoService = autenticacaoService;
            this.escolaService = escolaService;
            this.logService = logService;
        }

        [HttpGet("{codigoEOL}")]
        public IActionResult BuscaEscolasPor(string codigoEOL, [FromHeader]string token)
        {
            var username = string.Empty;

            var tokenValido = autenticacaoService.IsValido(token, out username);

            if (tokenValido)
            {
                if (codigoEOL.IsNull())
                {
                    return BadRequest();
                }

                EscolaDTO escolaPorCodigoEOL = escolaService.BuscarEscolaPor(codigoEOL);

                if (escolaPorCodigoEOL == default(EscolaDTO))
                {
                    return NotFound();
                }

                return Ok(escolaPorCodigoEOL);
            }

            return Unauthorized();
        }

        [HttpGet("modalidades_ensino")]
        public IActionResult BuscaModalidadesEnsino([FromHeader]string token)
        {
            string username = string.Empty;
            var tokenValido = autenticacaoService.IsValido(token, out username);

            if (tokenValido)
            {
                var method = MethodBase.GetCurrentMethod();
                logService.GravaLog(metodo: method.Name,
                                    servico: method.ReflectedType.Name,
                                    parametros: string.Empty,
                                    usuario: username);

                var modalidadesEnsino = escolaService.BuscaModalidadesEnsino();

                if (modalidadesEnsino.Count == 0)
                {
                    return NotFound();
                }

                return Ok(modalidadesEnsino);
            }

            return Unauthorized();
        }

        [HttpGet("tipos_unidade_educacao")]
        public IActionResult BuscaTiposUE([FromHeader]string token)
        {
            string username = string.Empty;
            var tokenValido = autenticacaoService.IsValido(token, out username);

            if (tokenValido)
            {
                var method = MethodBase.GetCurrentMethod();
                logService.GravaLog(metodo: method.Name,
                                    servico: method.ReflectedType.Name,
                                    parametros: string.Empty,
                                    usuario: username);

                var tiposUE = escolaService.BuscaTiposUE();

                if (tiposUE.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tiposUE);
            }

            return Unauthorized();
        }
    }
}