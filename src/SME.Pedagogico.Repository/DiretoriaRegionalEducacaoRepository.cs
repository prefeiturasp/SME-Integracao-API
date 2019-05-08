using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SME.Pedagogico.Interface;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.DREs;
using SME.Pedagogico.Repository.Queries;
using SME.Pedagogico.Interface.Extensions;

namespace SME.Pedagogico.Repository
{
    public class DiretoriaRegionalEducacaoRepository : QueryRepository, IDiretoriaRegionalEducacaoRepository
    {
        private readonly ConnectionStrings connectionStrings;

        public DiretoriaRegionalEducacaoRepository(IOptions<ConnectionStrings> connectionStrings) =>
            this.connectionStrings = connectionStrings?.Value;
        
        public IReadOnlyList<EscolasPorDREDTO> BuscarEscolasPor(string codigoDRE, string tipoEscola)
        {
            var campos = QueriesEscolas.BuscaEscolas();
            var where = " where dre.nm_exibicao_unidade like 'DRE %'";
            object parametros = default;

            if (codigoDRE.IsNotNull() && tipoEscola.IsNotNull())
            {
                where = $"{where} and vcue.cd_unidade_administrativa_referencia = @CodigoDRE and esc.tp_escola = @TipoEscola";
                parametros = new { CodigoDRE = codigoDRE, TipoEscola = tipoEscola};
            } else
            {
                if (codigoDRE.IsNotNull())
                {
                    where = $"{where} and vcue.cd_unidade_administrativa_referencia = @CodigoDRE";
                    parametros = new { CodigoDRE = codigoDRE };
                } else if (tipoEscola.IsNotNull())
                {
                    where = $"{where} and esc.tp_escola = @TipoEscola";
                    parametros = new { TipoEscola = tipoEscola };
                }
            }

            var query = QueryConstructor(campos, where);

            return QueryCollectionSQL<EscolasPorDREDTO>(connectionStrings.EolConnection, 
                                              query, 
                                              parametros)
                                              .ToList();
        }
    }
}
