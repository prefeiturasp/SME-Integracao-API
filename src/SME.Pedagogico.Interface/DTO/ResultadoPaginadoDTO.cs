using System.Collections.Generic;

namespace SME.Pedagogico.Interface.DTO
{
    public class ResultadoPaginadoDTO<T>
    {
        public int QtdResultados { get; set; }
        public string ProximaPagina { get; set; }
        public string PaginaAnterior { get; set; }
        public List<T> Itens { get; set; }
    }
}
