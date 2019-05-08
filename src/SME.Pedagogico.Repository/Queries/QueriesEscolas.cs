using System;

namespace SME.Pedagogico.Repository.Queries
{
    internal static class QueriesEscolas
    {
        internal static string BuscaEscolas()
        {
            return @"SELECT esc.cd_escola CodigoEscola, 
                     dre.nm_unidade_educacao NomeDRE, 
                     te.dc_tipo_escola TipoEscola, 
                     te.sg_tp_escola SiglaTipoEscola
                     FROM escola esc INNER JOIN v_cadastro_unidade_educacao vcue ON esc.cd_escola = vcue.cd_unidade_educacao
                     INNER JOIN tipo_escola te ON esc.tp_escola = te.tp_escola
                     INNER JOIN v_cadastro_unidade_educacao dre ON dre.cd_unidade_educacao = vcue.cd_unidade_administrativa_referencia ";
        }

        internal static string BuscaModalidadesEnsino()
        {
            return @"SELECT LTRIM(RTRIM(dc_etapa_ensino)) FROM etapa_ensino";
        }

        internal static string BuscaTiposUE()
        {
            return @"SELECT LTRIM(RTRIM(dc_tipo_escola)) FROM tipo_escola";
        }
    }
}
