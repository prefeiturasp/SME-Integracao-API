using System.Collections.Generic;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.DREs;
using SME.Pedagogico.Interface.Extensions;

namespace SME.Pedagogico.Service
{
    public class DiretoriaRegionalEducacaoService : IDiretoriaRegionalEducacaoService
    {
        private readonly IDiretoriaRegionalEducacaoRepository dreRepository;

        public DiretoriaRegionalEducacaoService(IDiretoriaRegionalEducacaoRepository dreRepository)
        {
            this.dreRepository = dreRepository;
        }

        public IReadOnlyList<EscolasPorDREDTO> BuscarEscolasPor(string codigoDRE, string tipoEscola)
        {
            if (codigoDRE.IsNotNull() || tipoEscola.IsNotNull())
            {
                return dreRepository.BuscarEscolasPor(codigoDRE, tipoEscola);
            }

            return default;
        }
    }
}
