using irede.core.Entities;
using irede.shared.Notifications;

namespace irede.core.Interfaces.Repositories
{
    public interface IProdutoRepository: INotifiable
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
