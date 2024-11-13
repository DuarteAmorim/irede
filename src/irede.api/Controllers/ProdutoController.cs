using irede.api.Controllers.Base;
using irede.core.Dtos.Core;
using irede.core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace irede.api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoService _iProdutoService;

        public ProdutoController(IProdutoService iProdutoService)
        {
            _iProdutoService = iProdutoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            try
            {
                var produtos = await _iProdutoService.GetAllAsync(pagina, tamanhoPagina);
                return await ResponseAsync(produtos, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var produto = await _iProdutoService.GetByIdAsync(id);

                return await ResponseAsync(produto, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Busca produtos por nome e/ou descrição com paginação.
        /// </summary>
        /// <param name="termoNome">Termo de busca para o nome</param>
        /// <param name="termoDescricao">Termo de busca para a descrição</param>
        /// <param name="pagina">Número da página</param>
        /// <param name="tamanhoPagina">Tamanho da página</param>
        /// <returns>Objeto PaginatedResult com lista de ProdutoDto e metadados de paginação ou BadRequest</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string termoNome = null, [FromQuery] string termoDescricao = null, [FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            try
            {
                var result = await _iProdutoService.SearchAsync(termoNome, termoDescricao, pagina, tamanhoPagina);
                return await ResponseAsync(result, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoDto produtoDto)
        {
            try
            {
                var produto = await _iProdutoService.AddAsync(produtoDto);

                var result = "";
                if (_iProdutoService.IsValid())
                {
                    result = "Produto incluído com sucesso.";
                }
                return await ResponseAsync(result, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }


        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] ProdutoDto produtoDto)
        {
            try
            {
                await _iProdutoService.UpdateAsync(produtoDto);
                var result = "";
                if (_iProdutoService.IsValid())
                {
                    result = "Produto alterado com sucesso.";
                }
                return await ResponseAsync(result, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] ProdutoDto produtoDto)
        {
            try
            {
                await _iProdutoService.UpdatePartialAsync(produtoDto);
                var result = "";
                if (_iProdutoService.IsValid())
                {
                    result = "Produto alterado com sucesso.";
                }
                return await ResponseAsync(result, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _iProdutoService.DeleteAsync(id);
                var result = "";
                if (_iProdutoService.IsValid())
                {
                    result = "Produto excluído com sucesso.";
                }
                return await ResponseAsync(result, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}
