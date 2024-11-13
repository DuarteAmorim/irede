using irede.core.Entities;
using irede.shared.Notifications;

namespace irede.core.Interfaces.Repositories
{
    public interface IProdutoRepository: INotifiable, IDisposable
    {
        Task<IEnumerable<Produto>> GetAllAsync(int limit, int offset);
        Task<IEnumerable<Produto>> SearchAsync(string termoNome, string termoDescricao);

        Task<IEnumerable<Produto>> SearchAsync(string termoNome, string termoDescricao, int pagina = 1, int tamanhoPagina = 10);

        Task<Produto> GetByIdAsync(int id);
        Task<int> CountAllProdutosAsync();
        Task<Produto> AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
