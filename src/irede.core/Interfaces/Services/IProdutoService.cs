using irede.core.Dtos.Core;
using irede.core.Interfaces.Base;
using irede.shared.Notifications;

namespace irede.core.Interfaces.Services
{
    public interface IProdutoService: INotifiable, IServiceBase
    {
        Task<PaginatedResult<ProdutoDto>> GetAllAsync(int pagina = 1, int tamanhoPagina = 10);
        Task<ProdutoDto> GetByIdAsync(int id);
        
        Task<PaginatedResult<ProdutoDto>> SearchAsync(string termoNome, string termoDescricao, int pagina = 1, int tamanhoPagina = 10);
        Task<ProdutoDto> AddAsync(ProdutoDto produtoDto);
        Task UpdateAsync(ProdutoDto produtoDto);
        Task UpdatePartialAsync(ProdutoDto produtoDto);
        Task DeleteAsync(int id);
    }
}
