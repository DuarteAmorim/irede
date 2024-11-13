using Dapper;
using irede.core.Entities;
using irede.core.Interfaces.Repositories;
using irede.infra.Database;
using irede.infra.Interfaces;
using irede.shared.Extensions;
using irede.shared.Notifications;
using System.Data;

namespace irede.infra.Repositories
{
    public class CategoriaRepository : Notifiable, ICategoriaRepository
    {
        private readonly IDapperContext _context;
        private readonly IScriptCache _scriptCache;
        private bool _disposed = false;

        public CategoriaRepository(IDapperContext context, IScriptCache scriptCache)
        {
            _context = context;
            _scriptCache = scriptCache;
        }

        

        public async Task<Categoria> GetByIdAsync(int id)
        {
            var script = _scriptCache.GetScript("GetCategoriaById.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    dbConnection.Open();

                    var response = await dbConnection.QueryFirstOrDefaultAsync<Categoria>(script, new { Id = id });
                    
                    if (response==null) AddNotification("Categoria não encontrada.");

                    return response;
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao obter a categoria por ID. \nErro: {0}".ToFormat(ex.Message));
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            var script = _scriptCache.GetScript("GetAllCategorias.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    if (dbConnection.State == ConnectionState.Closed) dbConnection.Open();


                    //dbConnection.Open();
                    var result = await dbConnection.QueryAsync<Categoria>(script);
                    return result;
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao obter todas as categorias. \nErro: {0}".ToFormat(ex.Message));
                    return null;
                }
            }
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            var script = _scriptCache.GetScript("AddCategoria.sql").ToLower();

            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    if (dbConnection.State == ConnectionState.Closed) dbConnection.Open();

                    categoria.SetId(await dbConnection.QuerySingleAsync<int>(script, categoria));
                    return categoria;
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao adicionar a categoria. \nErro: {0}".ToFormat(ex.Message));
                    return null;

                }
            }
        }
        public async Task UpdateAsync(Categoria categoria)
        {
            var script = _scriptCache.GetScript("UpdateCategoria.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    dbConnection.Open();

                    var rowsAffected = await dbConnection.ExecuteAsync(script, categoria);
                    if (rowsAffected == 0)
                    {
                        AddNotification("Nenhum registro foi atualizado. Verifique se o ID da categoria é válido.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao atualizar a categoria. \nErro: {0}".ToFormat(ex.Message));
                    return;
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var script = _scriptCache.GetScript("DeleteCategoria.sql");
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    dbConnection.Open();

                    var rowsAffected = await dbConnection.ExecuteAsync(script, new { Id = id });
                    if (rowsAffected == 0)
                    {
                        AddNotification("Nenhum registro foi deletado. Verifique se o ID da categoria é válido.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao deletar a categoria. \nErro: {0}".ToFormat(ex.Message));
                    return;
                }
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
                //_scriptCache?.Dispose();
                //_context?.Dispose();
            }


            _disposed = true;
        }
    }
}
