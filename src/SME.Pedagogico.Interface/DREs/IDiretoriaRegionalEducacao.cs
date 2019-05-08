using SME.Pedagogico.Interface.DTO;
using System.Collections.Generic;

namespace SME.Pedagogico.Interface.DREs
{
    public interface IDiretoriaRegionalEducacao
    {
        IReadOnlyList<EscolasPorDREDTO> BuscarEscolasPor(string codigoDRE, string tipoEscola);
    }
}
