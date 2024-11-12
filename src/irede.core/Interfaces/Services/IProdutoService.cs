using irede.core.Entities;
using irede.core.Interfaces.Base;
using irede.shared.Notifications;

namespace irede.core.Interfaces.Services
{
    public interface IProdutoService: INotifiable, IServiceBase
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task UpdatePartialAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
