using Microsoft.Extensions.Options;
using SME.Pedagogico.Interface;
using SME.Pedagogico.Interface.Autenticacao;

namespace SME.Pedagogico.Repository
{
    public class AutenticacaoRepository : QueryRepository, IAutenticacaoRepository
    {
        private readonly ConnectionStrings connectionStrings;

        public AutenticacaoRepository(IOptions<ConnectionStrings> connectionStrings) =>
            this.connectionStrings = connectionStrings?.Value;

        public void TesteConexaoPostGre()
        {

            var query = "select \"Id\" from \"LoggedUsers\" where \"Username\" = @Username";

            var param = new { Username = "caique.amcom" };

            var teste = QueryFirstOrDefaultPostgres<string>(connectionStrings.SgpConnection, query, param);
        }

        public bool TesteEscola(string username)
        {
            var query = @"select t4.dc_tipo_escola
                            from [dbo].[v_cadastro_unidade_educacao] t1 inner join [dbo].[v_cadastro_unidade_educacao] t2
                            on t1.cd_unidade_administrativa_referencia=t2.cd_unidade_educacao
                            inner join escola t3 on t1.cd_unidade_educacao =t3.cd_escola 
                            inner join tipo_escola t4 on t3.tp_escola=t4.tp_escola
                            inner join tipo_situacao_unidade t5
                            on t1.tp_situacao_unidade=t5.tp_situacao_unidade
                            ";

            var param = new { Username = username };

            var teste = QueryFirstOrDefaultSQLParameterless<string>(connectionStrings.EolConnection, query);

            return false;
        }
    }
}
