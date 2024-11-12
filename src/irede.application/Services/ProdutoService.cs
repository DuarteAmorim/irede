using AutoMapper;
using irede.core.Dtos.Core;
using irede.core.Entities;
using irede.core.Interfaces.Repositories;
using irede.core.Interfaces.Services;
using irede.shared.Notifications;

namespace irede.application.Services
{
    public class ProdutoService : Notifiable, IProdutoService
    {
        private readonly IProdutoRepository _iProdutoRepository;
        private readonly ICategoriaRepository _iCategoriaRepository;
        private readonly IMapper _mapper;
        private bool _disposed = false;
        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _iProdutoRepository = produtoRepository;
            _iCategoriaRepository = categoriaRepository;
            _mapper = mapper;
        }



        public async Task<PaginatedResult<ProdutoDto>> GetAllAsync(int pagina, int tamanhoPagina)
        {
            try {
                //num da página
                if (pagina <= 0)
                {
                    AddNotification("Página deve ser maior que zero.");
                    return null;
                }

                //tamanho da página
                if (tamanhoPagina <= 0)
                {
                    AddNotification("Tamanho da página deve ser maior que zero.");
                    return null;
                }
                int offset = (pagina - 1) * tamanhoPagina;

                var result = await _iProdutoRepository.GetAllAsync(tamanhoPagina, offset);

                var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(result);
                

                int totalRegistros = await _iProdutoRepository.CountAllProdutosAsync();
                
                int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanhoPagina);

                var pagResult = new PaginatedResult<ProdutoDto>
                {
                    Items = produtosDto,
                    TotalRegistros = totalRegistros,
                    PaginaAtual = pagina,
                    TamanhoPagina = tamanhoPagina,
                    TotalPaginas = totalPaginas
                };

                return pagResult;
            }
            catch (Exception)
            {
                AddNotifications(_iProdutoRepository.Notifications);
                throw;
            }

        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            try
            {
                var produtoDb = await _iProdutoRepository.GetByIdAsync(id);
                if (!_iProdutoRepository.IsValid())
                {
                    AddNotifications(_iProdutoRepository.Notifications);
                    return null;
                }

                if (produtoDb == null)
                    AddNotification("Produto não encontrado.");

                var produtoDto = _mapper.Map<ProdutoDto>(produtoDb);
                return produtoDto;
            }
            catch (Exception ex)
            {
                AddNotifications(_iProdutoRepository.Notifications);
                throw;
            }
        }

        public async Task<ProdutoDto> AddAsync(ProdutoDto produtoDto)
        {
            try
            {
                //var newProduto = (Produto)produtoDto;
                var newProduto = _mapper.Map<Produto>(produtoDto);
                if (!newProduto.IsValid())
                {
                    AddNotifications(newProduto.Notifications);
                    return null;
                }

                var categoria = await _iCategoriaRepository.GetByIdAsync(newProduto.Id_Categoria);
                if (!_iCategoriaRepository.IsValid())
                {
                    AddNotifications(_iCategoriaRepository.Notifications);
                    return null;
                }

                var response = await _iProdutoRepository.AddAsync(newProduto);

                if (!_iProdutoRepository.IsValid())
                    AddNotifications(_iProdutoRepository.Notifications);

                var dto = _mapper.Map<ProdutoDto>(newProduto);

                return dto;
            }
            catch (Exception)
            {
                AddNotifications(_iProdutoRepository.Notifications);
                throw;
            }

        }

        public async Task UpdateAsync(ProdutoDto produtoDto)
        {
            try
            {
                //var newProduto = (Produto)produtoDto;
                var newProduto = _mapper.Map<Produto>(produtoDto);

                newProduto.ValidateUpdate();
                if (!newProduto.IsValid())
                {
                    AddNotifications(newProduto.Notifications);
                    return;
                }

                // Verificar se o produto existe
                var existingProduto = await _iProdutoRepository.GetByIdAsync(newProduto.Id);
                if (!_iProdutoRepository.IsValid())
                {
                    AddNotification("Produto não encontrado.");
                    return;
                }

                // Verificar se a categoria existe
                var categoria = await _iCategoriaRepository.GetByIdAsync(newProduto.Id_Categoria);
                if (!_iCategoriaRepository.IsValid())
                {
                    AddNotifications(_iProdutoRepository.Notifications);

                    return;
                }

                _mapper.Map(produtoDto, existingProduto);

                await _iProdutoRepository.UpdateAsync(existingProduto);
                
                if (!_iProdutoRepository.IsValid())
                    AddNotifications(_iProdutoRepository.Notifications);

            }
            catch (Exception)
            {
                AddNotifications(_iProdutoRepository.Notifications);
                throw;
            }
        }

        public async Task UpdatePartialAsync(ProdutoDto produtoDto)
        {
            try
            {
                //var newProduto = (Produto)produtoDto;
                var newProduto = _mapper.Map<Produto>(produtoDto);

                newProduto.ValidateUpdate();

                if (!newProduto.IsValid())
                {
                    AddNotifications(newProduto.Notifications);
                    return;
                }

                // Verificar se o produto existe
                var existingProduto = await _iProdutoRepository.GetByIdAsync(newProduto.Id);
                if (existingProduto == null)
                {
                    AddNotification("Produto não encontrado.");
                    return;
                }

                // Verificar se a categoria existe
                var categoria = await _iCategoriaRepository.GetByIdAsync(newProduto.Id_Categoria);
                if (!_iCategoriaRepository.IsValid())
                {
                    AddNotifications(_iProdutoRepository.Notifications);

                    return;
                }

                _mapper.Map(produtoDto, existingProduto);


                await _iProdutoRepository.UpdateAsync(existingProduto);
                if (!_iProdutoRepository.IsValid())
                    AddNotifications(_iProdutoRepository.Notifications);
            }
            catch (Exception)
            {
                AddNotifications(_iProdutoRepository.Notifications);
                throw;
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {

                // Verificar se o produto existe
                var existingProduto = await _iProdutoRepository.GetByIdAsync(id);
                if (existingProduto == null)
                {
                    AddNotification("Produto não encontrado.");

                    return;
                }

                await _iProdutoRepository.DeleteAsync(id);
                if (!_iProdutoRepository.IsValid())
                    AddNotifications(_iProdutoRepository.Notifications);
            }
            catch (Exception)
            {
                AddNotifications(_iProdutoRepository.Notifications);
                throw;
            }

        }
        public async Task<PaginatedResult<ProdutoDto>> SearchAsync(string termoNome, string termoDescricao, int pagina = 1, int tamanhoPagina = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(termoNome) && string.IsNullOrWhiteSpace(termoDescricao))
                {
                    AddNotification("Pelo menos um termo de busca (nome ou descrição) é obrigatório.");
                    return null;
                }

                if (pagina <= 0)
                {
                    AddNotification("Página deve ser maior que zero.");
                    return null;
                }

                if (tamanhoPagina <= 0)
                {
                    AddNotification("Tamanho da página deve ser maior que zero.");
                    return null;
                }

                int offset = (pagina - 1) * tamanhoPagina;

                // Obter produtos filtrados
                var produtos = await _iProdutoRepository.SearchAsync(termoNome, termoDescricao, tamanhoPagina, offset);
                AddNotifications(_iProdutoRepository.Notifications);

                var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);

                int totalRegistros = await _iProdutoRepository.CountAllProdutosAsync();

                int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanhoPagina);

                var result = new PaginatedResult<ProdutoDto>
                {
                    Items = produtosDto,
                    TotalRegistros = totalRegistros,
                    PaginaAtual = pagina,
                    TamanhoPagina = tamanhoPagina,
                    TotalPaginas = totalPaginas
                };

                return result;
            }
            catch (Exception ex)
            {
                AddNotifications(_iProdutoRepository.Notifications);
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
                _iCategoriaRepository?.Dispose();
                _iProdutoRepository?.Dispose();
            }

            // Liberar recursos não gerenciados (se houver)

            _disposed = true;
        }
    }
}
