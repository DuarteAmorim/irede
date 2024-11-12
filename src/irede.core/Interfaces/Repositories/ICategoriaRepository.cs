using irede.core.Entities;
using irede.shared.Notifications;

namespace irede.core.Interfaces.Repositories
{
    public interface ICategoriaRepository: INotifiable, IDisposable
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> GetByIdAsync(int id);
        Task<Categoria> AddAsync(Categoria categoria);
        Task UpdateAsync(Categoria categoria);
        Task DeleteAsync(int id);
    }
}
