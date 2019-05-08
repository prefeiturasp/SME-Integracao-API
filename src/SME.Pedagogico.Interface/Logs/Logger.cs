using System;

namespace SME.Pedagogico.Interface.Logs
{
    public class Logger
    {
        public string Guid { get; set; }
        public string Metodo { get; set; }
        public string Parametros { get; set; }
        public string Servico { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Usuario { get; set; }
    }
}
