using irede.api.Controllers.Base;
using irede.core.Entities;
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _iProdutoService.GetAllAsync();
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produtoDto)
        {
            try
            {
                var produto = await _iProdutoService.AddAsync(produtoDto);

                return await ResponseAsync(produto, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }


        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Produto produto)
        {
            try
            {
                await _iProdutoService.UpdateAsync(produto);

                return await ResponseAsync(produto, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] Produto produto)
        {
            try
            {
                await _iProdutoService.UpdatePartialAsync(produto);

                return await ResponseAsync(produto, _iProdutoService);
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

                return await ResponseAsync(null, _iProdutoService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}
