using irede.api.Controllers.Base;
using irede.core.Dtos.Core;
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categorias = await _iCategoriaService.GetAllAsync();
                return await ResponseAsync(categorias, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseAsync(ex.InnerException, _iCategoriaService);

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
        public async Task<IActionResult> Post([FromBody] CategoriaDto categoriaDto)
        {
            try
            {
                var categoria = await _iCategoriaService.AddAsync(categoriaDto);
                var result = "";
                if (_iCategoriaService.IsValid())
                {
                    result = "Categoria incluída com sucesso.";
                }
                return await ResponseAsync(result, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] CategoriaDto categoriaDto)
        {
            try
            {
                await _iCategoriaService.UpdateAsync(categoriaDto);
                var result = "";
                if (_iCategoriaService.IsValid())
                {
                    result = "Categoria alterada com sucesso.";
                }
                return await ResponseAsync(result, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] CategoriaDto categoriaDto)
        {
            try
            {
                await _iCategoriaService.UpdatePartialAsync(categoriaDto);
                var result = "";
                if (_iCategoriaService.IsValid())
                {
                    result = "Categoria alterada com sucesso.";
                }
                return await ResponseAsync(result, _iCategoriaService);
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
                
                var result = "";
                if (_iCategoriaService.IsValid())
                {
                    result = "Categoria excluída com sucesso.";
                }
                return await ResponseAsync(result, _iCategoriaService);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

    }
}
