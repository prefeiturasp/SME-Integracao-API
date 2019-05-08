using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using SME.Pedagogico.BuildTest;
using SME.Pedagogico.Interface.DTO;
using SME.Pedagogico.WebAPI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SME.Pedagogico.Test.DRETest
{
    public class DREControllerTest
    {
        private readonly DREBuildTests testBuilder;
        private readonly DiretoriaRegionalEducacaoController dreController;
        private string testeOutParam;

        public DREControllerTest()
        {
            testBuilder = new DREBuildTests();
            testeOutParam = string.Empty;
            dreController = new DiretoriaRegionalEducacaoController(testBuilder.autenticacaoServiceMock.Object,
                                    testBuilder.dreServiceMock.Object,
                                    testBuilder.logService.Object);
        }
        

        [Fact]
        public void TestBuscaEscolaDRE_ErroBadRequest()
        {
            testBuilder.autenticacaoServiceMock
                .Setup(auth => auth.IsValido(It.IsAny<string>(), out testeOutParam))
                .Returns(true);

            testBuilder.logService
                .Setup(log => log.GravaLog(It.IsAny<string>(), 
                                           It.IsAny<string>(), 
                                           It.IsAny<string>(), 
                                           It.IsAny<string>()));

            var retorno = dreController.BuscaEscolasPor(string.Empty, string.Empty, It.IsAny<string>());

            retorno.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public void TestBuscaEscolaDRE_ErroNotFound()
        {
            testBuilder.autenticacaoServiceMock
                .Setup(auth => auth.IsValido(It.IsAny<string>(), out testeOutParam))
                .Returns(true);

            testBuilder.dreServiceMock
                .Setup(serv => serv.BuscarEscolasPor("1", It.IsAny<string>()))
                .Returns(new List<EscolasPorDREDTO>());

            testBuilder.logService
                .Setup(log => log.GravaLog(It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>()));

            var retorno = dreController.BuscaEscolasPor("1", It.IsAny<string>(), It.IsAny<string>());

            retorno.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestBuscaEscolaDRE_ErroUnauthorized()
        {
            var retorno = dreController.BuscaEscolasPor("1", "2", It.IsAny<string>());

            retorno.ShouldBeOfType<UnauthorizedResult>();
        }

        [Fact]
        public void TestBuscaEscolaDRE_OK()
        {
            testBuilder.autenticacaoServiceMock
                .Setup(auth => auth.IsValido(It.IsAny<string>(), out testeOutParam))
                .Returns(true);

            testBuilder.dreServiceMock
                .Setup(serv => serv.BuscarEscolasPor("1", "1"))
                .Returns(testBuilder.GetTestEscolasPorDREDto());

            testBuilder.logService
                .Setup(log => log.GravaLog(It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>()));

            var retorno = dreController.BuscaEscolasPor("1", "1", It.IsAny<string>());

            retorno.ShouldBeOfType<OkObjectResult>();
        }
    }
}
