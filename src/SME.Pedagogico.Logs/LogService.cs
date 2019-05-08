using SME.Pedagogico.Interface.Logs;
using System.Threading.Tasks;

namespace SME.Pedagogico.Logs
{
    public class LogService : ILogService
    {
        private readonly ILogRepository logRepository;

        public LogService(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public void GravaLog(string metodo, string parametros, string servico, string usuario)
        {
            logRepository.GravaLog(metodo, parametros, servico, usuario);
        }
    }
}
