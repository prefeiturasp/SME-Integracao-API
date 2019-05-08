using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SME.Pedagogico.Interface;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.Escolas;
using SME.Pedagogico.Repository.Queries;

namespace SME.Pedagogico.Repository
{
    public class EscolaRepository : QueryRepository, IEscolaRepository
    {
        private readonly ConnectionStrings connectionStrings;

        public EscolaRepository(IOptions<ConnectionStrings> connectionStrings) =>
            this.connectionStrings = connectionStrings?.Value;

        public IReadOnlyList<string> BuscaModalidadesEnsino()
        {
            var campos = QueriesEscolas.BuscaModalidadesEnsino();

            return QueryCollectionSQLParameterless<string>(connectionStrings.EolConnection,
                                              campos).ToList();
        }

        public EscolaDTO BuscarEscolaPor(string codigoEOL)
        {
            var campos = QueriesEscolas.BuscaEscolas();
            var where = "WHERE cd_unidade_administrativa_referencia = @CodigoDRE";
            var query = QueryConstructor(campos, where);

            var parametros = new { CodigoEOL = codigoEOL };

            return QueryFirstOrDefaultPostgres<EscolaDTO>(connectionStrings.EolConnection,
                                              query,
                                              parametros);
        }

        public IReadOnlyList<string> BuscaTiposUE()
        {
            var campos = QueriesEscolas.BuscaTiposUE();

            return QueryCollectionSQLParameterless<string>(connectionStrings.EolConnection,
                                              campos).ToList();
        }
    }
}
