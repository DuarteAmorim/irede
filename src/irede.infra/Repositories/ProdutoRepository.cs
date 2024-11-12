using Dapper;
using irede.core.Entities;
using irede.core.Interfaces.Repositories;
using irede.infra.Database;
using irede.infra.Interfaces;
using irede.shared.Extensions;
using irede.shared.Notifications;

namespace irede.infra.Repositories
{
    public class ProdutoRepository : Notifiable, IProdutoRepository
    {
        private readonly IDapperContext _context;
        private readonly IScriptCache _scriptCache;

        public ProdutoRepository(IDapperContext context, IScriptCache scriptCache)
        {
            _context = context;
            _scriptCache = scriptCache;

        }


        public async Task<Produto> AddAsync(Produto produto)
        {
            var script = _scriptCache.GetScript("AddProduto.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    produto.SetId(await dbConnection.QuerySingleAsync<int>(script, produto));
                    return produto;
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao adicionar o produto. Erro:{0}".ToFormat(ex.Message));
                    return null;
                }
            }
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            var script = _scriptCache.GetScript("GetProdutoById.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    return await dbConnection.QueryFirstOrDefaultAsync<Produto>(script, new { Id = id });
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao obter o produto por ID. Erro:{0}".ToFormat(ex.Message));
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            var script = _scriptCache.GetScript("GetAllProdutos.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    return await dbConnection.QueryAsync<Produto>(script);
                }
                catch (Exception ex)
                {
                    // Log e tratamento da exceção
                    AddNotification("Erro ao obter todos os produtos. Erro:{0}".ToFormat(ex.Message));
                    return null;
                }
            }
        }

        public async Task UpdateAsync(Produto produto)
        {
            var script = _scriptCache.GetScript("UpdateProduto.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    var rowsAffected = await dbConnection.ExecuteAsync(script, produto);
                    if (rowsAffected == 0)
                    {
                        AddNotification("Nenhum registro foi atualizado. Verifique se o ID do produto é válido.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // Log e tratamento da exceção
                    AddNotification("Erro ao atualizar o produto. Erro: {0}".ToFormat(ex.Message));
                    return;
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var script = _scriptCache.GetScript("DeleteProduto.sql").ToLower();
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    var rowsAffected = await dbConnection.ExecuteAsync(script, new { Id = id });
                    if (rowsAffected == 0)
                    {
                        AddNotification("Nenhum registro foi deletado. Verifique se o ID do produto é válido.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // Log e tratamento da exceção
                    AddNotification("Erro ao deletar o produto.".ToFormat(ex.Message));
                    return;
                }
            }
        }
    }
}
