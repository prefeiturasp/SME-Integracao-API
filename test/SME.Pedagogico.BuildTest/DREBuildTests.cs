using Moq;
using SME.Pedagogico.Interface.Autenticacao;
using SME.Pedagogico.Interface.DREs;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Interface.Logs;
using SME.Pedagogico.Interface.Paginador;
using System.Collections.Generic;

namespace SME.Pedagogico.BuildTest
{
    public class DREBuildTests
    {
        public Mock<IDiretoriaRegionalEducacaoRepository> dreRepositoryMock { get; private set; }
        public Mock<IDiretoriaRegionalEducacaoService> dreServiceMock { get; private set; }
        public Mock<IAutenticacaoService> autenticacaoServiceMock { get; private set; }
        public Mock<ILogService> logService { get; private set; }

        public DREBuildTests()
        {
            dreRepositoryMock = new Mock<IDiretoriaRegionalEducacaoRepository>();
            dreServiceMock = new Mock<IDiretoriaRegionalEducacaoService>();
            autenticacaoServiceMock = new Mock<IAutenticacaoService>();
            logService = new Mock<ILogService>();
        }

        public IReadOnlyList<EscolasPorDREDTO> GetTestEscolasPorDREDto()
        {
            var escolasPorDre = new List<EscolasPorDREDTO>();

            escolasPorDre.Add(new EscolasPorDREDTO()
            {
                CodigoEscola = "012345"
            });
            escolasPorDre.Add(new EscolasPorDREDTO()
            {
                CodigoEscola = "065432"
            });

            return escolasPorDre;
        }
    }
}
