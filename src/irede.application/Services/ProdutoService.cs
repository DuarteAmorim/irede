using irede.core.Entities;
using irede.core.Interfaces.Repositories;
using irede.core.Interfaces.Services;
using irede.shared.Notifications;

namespace irede.application.Services
{
    public class ProdutoService : Notifiable, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private bool _disposed = false;
        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            try { return await _produtoRepository.GetAllAsync(); }
            catch (Exception)
            {
                AddNotifications(_produtoRepository.Notifications);
                throw;
            }

        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            try
            {
                var result = await _produtoRepository.GetByIdAsync(id);
                if (!_produtoRepository.IsValid())
                {
                    AddNotifications(_produtoRepository.Notifications);
                    return null;
                }

                if (result == null)
                    AddNotification("Produto não encontrado.");

                return result;
            }
            catch (Exception ex)
            {
                AddNotifications(_produtoRepository?.Notifications);
                throw;
            }
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            try
            {
                // Validações e regras de negócio
                produto.Validate();
                if (!produto.IsValid())
                {
                    AddNotifications(produto.Notifications);
                    return null;
                }

                // Verificar se a categoria existe
                var categoria = await _categoriaRepository.GetByIdAsync(produto.Id_categoria);
                if (!_categoriaRepository.IsValid())
                {
                    AddNotification("Categoria associada não encontrada.");
                    return null;
                }

                return await _produtoRepository.AddAsync(produto);
            }
            catch (Exception)
            {
                AddNotifications(_produtoRepository.Notifications);
                throw;
            }

        }

        public async Task UpdateAsync(Produto produto)
        {
            try
            {
                // Validações e regras de negócio
                produto.Validate();
                if (!produto.IsValid())
                {
                    AddNotifications(produto.Notifications);
                    return;
                }


                // Verificar se o produto existe
                var existingProduto = await _produtoRepository.GetByIdAsync(produto.Id);
                if (!_produtoRepository.IsValid())
                {
                    AddNotification("Produto não encontrado.");
                    return;
                }

                // Verificar se a categoria existe
                var categoria = await _categoriaRepository.GetByIdAsync(produto.Id_categoria);
                if (!_categoriaRepository.IsValid())
                {
                    AddNotification("Categoria associada não encontrada.");
                    return;
                }

                await _produtoRepository.UpdateAsync(produto);
            }
            catch (Exception)
            {
                AddNotifications(_produtoRepository.Notifications);
                throw;
            }
        }

        public async Task UpdatePartialAsync(Produto produto)
        {
            try
            {

                // Verificar se o produto existe
                var existingProduto = await _produtoRepository.GetByIdAsync(produto.Id);
                if (existingProduto == null)
                {
                    produto.AddNotification("Produto não encontrado.");
                    return;
                }


                produto.Validate();
                if (!existingProduto.IsValid())
                {
                    //produto.AddNotifications(existingProduto.Notifications);
                    return;
                }

                // Verificar se a categoria existe
                var categoria = await _categoriaRepository.GetByIdAsync(existingProduto.Id_categoria);
                if (!_categoriaRepository.IsValid())
                {
                    AddNotification("Categoria associada não encontrada.");
                    return;
                }

                await _produtoRepository.UpdateAsync(existingProduto);
            }
            catch (Exception)
            {
                AddNotifications(_produtoRepository.Notifications);
                throw;
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {

                // Verificar se o produto existe
                var existingProduto = await _produtoRepository.GetByIdAsync(id);
                if (existingProduto == null)
                {
                    AddNotification("Produto não encontrado.");

                    return;
                }

                await _produtoRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                AddNotifications(_produtoRepository.Notifications);
                throw;
            }

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
