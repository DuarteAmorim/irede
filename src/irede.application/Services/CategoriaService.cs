using irede.core.Entities;
using irede.core.Interfaces.Repositories;
using irede.core.Interfaces.Services;
using irede.shared.Notifications;

namespace irede.application.Services
{
    public class CategoriaService : Notifiable, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private bool _disposed = false;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            var result = await _categoriaRepository.GetAllAsync();
            AddNotifications(_categoriaRepository.Notifications);
            return result;
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            try
            {
                var result = await _categoriaRepository.GetByIdAsync(id);
                if (!_categoriaRepository.IsValid())
                {
                    AddNotifications(_categoriaRepository.Notifications);
                    return null;
                }

                if (result == null)
                    AddNotification("Categoria não encontrada.");

                return result;
            }
            catch (Exception ex)
            {
                AddNotifications(_categoriaRepository?.Notifications);
                throw;
            }
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            // Validações e regras de negócio
            categoria.Validate();
            if (!categoria.IsValid())
            {
                AddNotifications(categoria.Notifications);
                return null;
            }

            return await _categoriaRepository.AddAsync(categoria);
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            // Validações e regras de negócio
            categoria.Validate();
            if (!categoria.IsValid())
            {
                AddNotifications(categoria.Notifications);
                return;
            }

            // Verificar se a categoria existe
            var existingCategoria = await _categoriaRepository.GetByIdAsync(categoria.Id);
            if (existingCategoria == null)
            {
                AddNotification("Categoria não encontrada.");
                return;
            }
            await _categoriaRepository.UpdateAsync(categoria);
        }

        public async Task UpdatePartialAsync(Categoria categoria)
        {
            // Verificar se a categoria existe
            var existingCategoria = await _categoriaRepository.GetByIdAsync(categoria.Id);
            if (existingCategoria == null)
            {
                AddNotification("Categoria não encontrada.");
                return;
            }

            categoria.Validate();
            if (!categoria.IsValid())
            {
                AddNotifications(categoria.Notifications);
                return;
            }

            await _categoriaRepository.UpdateAsync(categoria);
        }

        public async Task DeleteAsync(int id)
        {
            // Verificar se a categoria existe
            var existingCategoria = await _categoriaRepository.GetByIdAsync(id);
            if (existingCategoria == null)
            {
                AddNotification("Categoria não encontrada.");
                return;
            }

            await _categoriaRepository.DeleteAsync(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Liberar recursos gerenciados que implementam IDisposable
                _categoriaRepository?.Dispose();
            }

            // Liberar recursos não gerenciados (se houver)

            _disposed = true;
        }
    }
}
