using Moq;
using SME.Pedagogico.Interface.Autenticacao;
using SME.Pedagogico.Interface.Escolas;
using SME.Pedagogico.Interface.Logs;
using System.Collections.Generic;

namespace SME.Pedagogico.BuildTest
{
    public class EscolaBuildTests
    {
        public Mock<IEscolaRepository> escolaRepositoryMock { get; private set; }
        public Mock<IEscolaService> escolaServiceMock { get; private set; }
        public Mock<IAutenticacaoService> autenticacaoServiceMock { get; private set; }
        public Mock<ILogService> logService { get; private set; }

        public EscolaBuildTests()
        {
            escolaRepositoryMock = new Mock<IEscolaRepository>();
            escolaServiceMock = new Mock<IEscolaService>();
            autenticacaoServiceMock = new Mock<IAutenticacaoService>();
            logService = new Mock<ILogService>();
        }

        public IReadOnlyList<string> GetMockList()
        {
            var mockList = new List<string>();

            mockList.Add("STRING 1");
            mockList.Add("STRING 2");

            return mockList;
        }
    }
}
