using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.Pedagogico.Interface.Autenticacao;
using SME.Pedagogico.Interface.DREs;
using SME.Pedagogico.Interface.Escolas;
using SME.Pedagogico.Interface.Logs;
using SME.Pedagogico.Interface.Paginador;
using SME.Pedagogico.Logs;
using SME.Pedagogico.Repository;
using SME.Pedagogico.Service;

namespace SME.Pedagogico.IoC
{
    public static class RegisterDependency
    {
        public static void Register(IServiceCollection services)
        {
            RegisterServices(services);
            RegisterRepositories(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.TryAddScoped<IAutenticacaoService, AutenticacaoService>();
            services.TryAddScoped<IDiretoriaRegionalEducacaoService, DiretoriaRegionalEducacaoService>();
            services.TryAddScoped<ILogService, LogService>();
            services.TryAddScoped<IEscolaService, EscolaService>();
            services.TryAddScoped<IPaginador, PaginadorService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.TryAddScoped<IAutenticacaoRepository, AutenticacaoRepository>();
            services.TryAddScoped<IDiretoriaRegionalEducacaoRepository, DiretoriaRegionalEducacaoRepository>();
            services.TryAddScoped<ILogRepository, LogRepository>();
            services.TryAddScoped<IEscolaRepository, EscolaRepository>();
        }
    }
}
