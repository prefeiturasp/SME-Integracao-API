using Shouldly;
using SME.Pedagogico.BuildTest;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.Service;
using System.Collections.Generic;
using Xunit;

namespace SME.Pedagogico.Test.DRE
{
    public class DREServiceTest
    {
        private readonly DREBuildTests testBuilder;
        private readonly DiretoriaRegionalEducacaoService dreService;

        public DREServiceTest()
        {
            testBuilder = new DREBuildTests();
            dreService = new DiretoriaRegionalEducacaoService(testBuilder.dreRepositoryMock.Object);
        }

        [Fact]
        public void TestBuscaEscolaDRECodigoDREVazio_ShouldBeNull()
        {
            var retorno = dreService.BuscarEscolasPor(string.Empty, string.Empty);

            retorno.ShouldBeNull();
        }

        [Fact]
        public void TestBuscaEscolaDRECodigoExistente_ShouldBeSuccessful()
        {
            testBuilder.dreRepositoryMock
                .Setup(serv => serv.BuscarEscolasPor("1", "1"))
                .Returns(testBuilder.GetTestEscolasPorDREDto());

            var retorno = dreService.BuscarEscolasPor("1", "1");

            retorno.ShouldBeOfType<List<EscolasPorDREDTO>>();
            retorno.Count.ShouldBeGreaterThan(0);
        }
    }
}