using irede.application.MappingProfiles;
using irede.application.Services;
using irede.core.Interfaces.Repositories;
using irede.core.Interfaces.Services;
using irede.infra.Database;
using irede.infra.Interfaces;
using irede.infra.Repositories;
using irede.infra.Scripts;
using irede.infra.Util;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace irede.api
{
    public class CfgServices
    {
        private readonly WebApplicationBuilder _builder;

        public CfgServices(WebApplicationBuilder builder)
        {
            this._builder = builder;
        }

        public void AddDependenceServices()
        {
        
            _builder.Services.AddAutoMapper(typeof(MappingProfile)); // Especifica o tipo do perfil de mapeamento

            // Configurar ScriptSettings a partir do appsettings.json
            _builder.Services.Configure<ScriptSettings>(_builder.Configuration.GetSection("ScriptSettings"));
            _builder.Services.AddSingleton<IScriptCache, ScriptCache>();

            _builder.Services.AddScoped<IDapperContext, DapperContext>();


            _builder.Services.AddScoped<IProdutoService, ProdutoService>();
            _builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            _builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            _builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();


            _builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
