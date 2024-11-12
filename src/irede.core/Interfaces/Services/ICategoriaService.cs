using irede.core.Dtos.Core;
using irede.core.Entities;
using irede.core.Interfaces.Base;
using irede.shared.Notifications;

namespace irede.core.Interfaces.Services
{
    public interface ICategoriaService: INotifiable, IServiceBase
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> GetByIdAsync(int id);
        Task<Categoria> AddAsync(CategoriaDto categoriaDto);
        Task UpdateAsync(CategoriaDto categoriaDto);
        Task UpdatePartialAsync(CategoriaDto categoriaDto);
        Task DeleteAsync(int id);
    }
}
