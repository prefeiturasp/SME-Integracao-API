using System.Collections.Generic;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.Escolas;

namespace SME.Pedagogico.Service
{
    public class EscolaService : IEscolaService
    {
        private readonly IEscolaRepository escolaRepository;

        public EscolaService(IEscolaRepository escolaRepository)
        {
            this.escolaRepository = escolaRepository;
        }

        public IReadOnlyList<string> BuscaModalidadesEnsino() =>
            escolaRepository.BuscaModalidadesEnsino();

        public EscolaDTO BuscarEscolaPor(string codigoEOL) =>
            escolaRepository.BuscarEscolaPor(codigoEOL);

        public IReadOnlyList<string> BuscaTiposUE() =>
            escolaRepository.BuscaTiposUE();
    }
}
