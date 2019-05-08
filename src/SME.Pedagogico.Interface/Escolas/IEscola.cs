using SME.Pedagogico.Interface.DTO;
using System.Collections.Generic;

namespace SME.Pedagogico.Interface.Escolas
{
    public interface IEscola
    {
        EscolaDTO BuscarEscolaPor(string codigoEOL);
        IReadOnlyList<string> BuscaModalidadesEnsino();
        IReadOnlyList<string> BuscaTiposUE();
    }
}
