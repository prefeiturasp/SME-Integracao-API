using SME.Pedagogico.Interface.DTO;
using System.Collections.Generic;

namespace SME.Pedagogico.Interface.Paginador
{
    public interface IPaginador
    {
        ResultadoPaginadoDTO<T> Pagina<T>(IEnumerable<T> itens, int pagina, string url);
    }
}
