using System.Threading.Tasks;

namespace SME.Pedagogico.Interface.Logs
{
    public interface ILog
    {
        void GravaLog(string metodo, string parametros, string servico, string usuario);
    }
}
