using irede.core.Entities;
using irede.core.Interfaces.Base;
using irede.shared.Notifications;

namespace irede.core.Interfaces.Services
{
    public interface ICategoriaService: INotifiable, IServiceBase
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> GetByIdAsync(int id);
        Task<Categoria> AddAsync(Categoria categoria);
        Task UpdateAsync(Categoria categoria);
        Task UpdatePartialAsync(Categoria categoria);
        Task DeleteAsync(int id);
    }
}
