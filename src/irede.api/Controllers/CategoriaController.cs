using irede.api.Controllers.Base;
using irede.core.Entities;
using irede.core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace irede.api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaService _iCategoriaService;

        public CategoriaController(ICategoriaService iCategoriaService)
        {
            _iCategoriaService = iCategoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categorias = await _iCategoriaService.GetAllAsync();
                return await ResponseAsync(categorias, _iCategoriaService);
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
                var categoria = await _iCategoriaService.GetByIdAsync(id);
                return await ResponseAsync(categoria, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoriaDto)
        {
            try
            {
                var categoria = await _iCategoriaService.AddAsync(categoriaDto);
                return await ResponseAsync(categoria, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Categoria categoria)
        {
            try
            {
                await _iCategoriaService.UpdateAsync(categoria);
                return await ResponseAsync(categoria, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] Categoria categoria)
        {
            try
            {
                await _iCategoriaService.UpdatePartialAsync(categoria);
                return await ResponseAsync(categoria, _iCategoriaService);
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
                await _iCategoriaService.DeleteAsync(id);
                return await ResponseAsync(null, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

    }
}
