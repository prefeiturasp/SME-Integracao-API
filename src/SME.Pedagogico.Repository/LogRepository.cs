using SME.Pedagogico.Interface.Logs;
using SME.Pedagogico.Repository.Context;
using System;
using System.Threading.Tasks;

namespace SME.Pedagogico.Repository
{
    public class LogRepository : CRUDRepository<Logger>, ILogRepository
    {
        private readonly ApiContext apiContext;

        public LogRepository(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        public void GravaLog(string metodo, string parametros, string servico, string usuario)
        {
            var logger = new Logger();
            logger.Guid = Guid.NewGuid().ToString();
            logger.Metodo = metodo;
            logger.Parametros = parametros;
            logger.Servico = servico;
            logger.Usuario = usuario;
            logger.DataCriacao = DateTime.Now;

            Add(logger, apiContext);
        }
    }
}
