using Microsoft.Extensions.Options;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.Paginador;
using SME.Pedagogico.Interface.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SME.Pedagogico.Service
{
    public class PaginadorService : IPaginador
    {
        private readonly PaginacaoSettings paginacaoSettings;

        public PaginadorService(IOptions<PaginacaoSettings> paginacaoSettings)
        {
            this.paginacaoSettings = paginacaoSettings?.Value;
        }

        public ResultadoPaginadoDTO<T> Pagina<T>(IEnumerable<T> itens, int pagina, string url)
        {
            var resultadoPaginado = new ResultadoPaginadoDTO<T>();
            resultadoPaginado.Itens = GetRegistrosPagina(itens, pagina);
            resultadoPaginado.QtdResultados = paginacaoSettings.QtdPagina;
            resultadoPaginado.ProximaPagina = MontaUrlComPaginaNova(url, proximaPagina: true);
            if (pagina > 1)
            {
                resultadoPaginado.PaginaAnterior = MontaUrlComPaginaNova(url, proximaPagina: false);
            } else
            {
                resultadoPaginado.PaginaAnterior = string.Empty;
            }

            return resultadoPaginado;

        }

        private List<T> GetRegistrosPagina<T>(IEnumerable<T> itens, int pagina)
        {
             return itens
                        .Skip(paginacaoSettings.QtdPagina * Convert.ToInt32(pagina))
                        .Take(paginacaoSettings.QtdPagina)
                        .ToList();
        }

        private string MontaUrlComPaginaNova(string url, bool proximaPagina)
        {
            var urlSplit = url.Split("/");

            var numPaginaUrl = Convert.ToInt32(urlSplit[urlSplit.Length-1]);

            if (proximaPagina)
            {
                numPaginaUrl++;
            } else
            {
                numPaginaUrl--;
            }

            urlSplit[urlSplit.Length-1] = numPaginaUrl.ToString();

            return string.Join('/', urlSplit);
        }
    }
}
