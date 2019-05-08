using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using SME.Pedagogico.BuildTest;
using SME.Pedagogico.WebAPI.Controllers;
using System.Collections.Generic;
using Xunit;

namespace SME.Pedagogico.Test.EscolaTest
{
    public class EscolaControllerTest
    {
        private readonly EscolaBuildTests testBuilder;
        private readonly EscolaController escolaController;
        private string testeOutParam;

        public EscolaControllerTest()
        {
            testBuilder = new EscolaBuildTests();
            testeOutParam = string.Empty;
            escolaController = new EscolaController(testBuilder.autenticacaoServiceMock.Object,
                                    testBuilder.escolaServiceMock.Object,
                                    testBuilder.logService.Object);
        }

        [Fact]
        public void TestBuscaModalidadeEnsino_ErroNotFound()
        {
            testBuilder.autenticacaoServiceMock
                .Setup(auth => auth.IsValido(It.IsAny<string>(), out testeOutParam))
                .Returns(true);

            testBuilder.escolaServiceMock
                .Setup(serv => serv.BuscaModalidadesEnsino())
                .Returns(new List<string>());

            testBuilder.logService
                .Setup(log => log.GravaLog(It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>()));

            var retorno = escolaController.BuscaModalidadesEnsino(It.IsAny<string>());

            retorno.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestBuscaModalidadeEnsino_OK()
        {
            testBuilder.autenticacaoServiceMock
                .Setup(auth => auth.IsValido(It.IsAny<string>(), out testeOutParam))
                .Returns(true);

            testBuilder.escolaServiceMock
                .Setup(serv => serv.BuscaModalidadesEnsino())
                .Returns(testBuilder.GetMockList());

            testBuilder.logService
                .Setup(log => log.GravaLog(It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>()));

            var retorno = escolaController.BuscaModalidadesEnsino(It.IsAny<string>());

            retorno.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void TestBuscaTipoUnidade_ErroNotFound()
        {
            testBuilder.autenticacaoServiceMock
                .Setup(auth => auth.IsValido(It.IsAny<string>(), out testeOutParam))
                .Returns(true);

            testBuilder.escolaServiceMock
                .Setup(serv => serv.BuscaTiposUE())
                .Returns(new List<string>());

            testBuilder.logService
                .Setup(log => log.GravaLog(It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>()));

            var retorno = escolaController.BuscaTiposUE(It.IsAny<string>());

            retorno.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestBuscaTipoUnidade_OK()
        {
            testBuilder.autenticacaoServiceMock
                .Setup(auth => auth.IsValido(It.IsAny<string>(), out testeOutParam))
                .Returns(true);

            testBuilder.escolaServiceMock
                .Setup(serv => serv.BuscaTiposUE())
                .Returns(testBuilder.GetMockList());

            testBuilder.logService
                .Setup(log => log.GravaLog(It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>(),
                                           It.IsAny<string>()));

            var retorno = escolaController.BuscaTiposUE(It.IsAny<string>());

            retorno.ShouldBeOfType<OkObjectResult>();
        }
    }
}
