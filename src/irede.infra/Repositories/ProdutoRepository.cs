using Dapper;
using irede.core.Dtos.Core;
using irede.core.Entities;
using irede.core.Interfaces.Repositories;
using irede.infra.Database;
using irede.infra.Interfaces;
using irede.shared.Extensions;
using irede.shared.Notifications;
using System.Data;

namespace irede.infra.Repositories
{
    public class ProdutoRepository : Notifiable, IProdutoRepository
    {
        private readonly IDapperContext _context;
        private readonly IScriptCache _scriptCache;
        private bool _disposed = false;

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
                    dbConnection.Open();
                    produto.SetId(await dbConnection.QuerySingleAsync<int>(script, produto));
                    return produto;
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao adicionar o produto. \nErro: {0}".ToFormat(ex.Message));
                    throw;
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
                    dbConnection.Open();
                    //QueryFirstOrDefaultAsync
                    var produtos = await dbConnection.QueryAsync<Produto, Categoria, Produto>(
                        script,
                        (p, c) =>
                        {
                            p.SetCategoria(c);
                            return p;
                        },
                        new { Id = id },
                        splitOn: "Id"
                    );
                    var produto = produtos.FirstOrDefault();

                    //return await dbConnection.QueryFirstOrDefaultAsync<Produto>(script, new { Id = id });
                    return produto;
                }
                catch (Exception ex)
                {
                    AddNotification("Erro ao obter o produto por ID. \nErro: {0}".ToFormat(ex.Message));
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Produto>> GetAllAsync(int limit, int offset)
        {
            var script = _scriptCache.GetScript("GetAllProdutos.sql");
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    dbConnection.Open();
                    var produtos = await dbConnection.QueryAsync<Produto, Categoria, Produto>(script,
                        (p, c) =>
                        {
                            p.SetCategoria(c);
                            return p;
                        },
                        new { Limit = limit, Offset = offset },
                        splitOn: "Id"
                    );

                    return produtos;
                }
                catch (Exception ex)
                {
                    // Log e tratamento da exceção
                    AddNotification("Erro ao obter todos os produtos. \nErro: {0}".ToFormat(ex.Message));
                    throw;
                }
            }
        }
        public async Task<IEnumerable<Produto>> SearchAsync(string termoNome, string termoDescricao)
        {
            var script = _scriptCache.GetScript("SearchProdutosByNomeOuDescricao.sql");
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    dbConnection.Open();
                    var produtos = await dbConnection.QueryAsync<Produto, Categoria, Produto>(
                        script,
                        (p, c) =>
                        {
                            p.SetCategoria(c);
                            return p;
                        },
                        new
                        {
                            TermoNome = string.IsNullOrWhiteSpace(termoNome) ? null : termoNome,
                            TermoDescricao = string.IsNullOrWhiteSpace(termoDescricao) ? null : termoDescricao
                        },
                        splitOn: "Id" // Deve corresponder ao alias no SQL
                    );
                    return produtos;
                }
                catch (Exception ex)
                {
                    AddNotification($"Erro ao buscar os produtos. \nErro: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Produto>> SearchAsync(string termoNome, string termoDescricao, int limit, int offset)
        {
            var script = _scriptCache.GetScript("SearchProdutosByNomeOuDescricao.sql");
           
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {
                    dbConnection.Open();
                    var produtos = await dbConnection.QueryAsync<Produto, Categoria, Produto>(
                        script,
                        (p, c) =>
                        {
                            p.SetCategoria(c);
                            return p;
                        },
                        new
                        {
                            TermoNome = string.IsNullOrWhiteSpace(termoNome) ? null : termoNome,
                            TermoDescricao = string.IsNullOrWhiteSpace(termoDescricao) ? null : termoDescricao,
                            Limit = limit,
                            Offset = offset
                        },
                        splitOn: "Id"
                    );
                    return produtos;
                }
                catch (Exception ex)
                {
                    AddNotification($"Erro ao buscar os produtos. \nErro: {ex.Message}");
                    throw;
                }
            }
        }
        public async Task<int> CountAllProdutosAsync()
        {
            // Obter o total de registros para calcular o total de páginas
            var totalRegistrosScript = _scriptCache.GetScript("CountAllProdutos.sql").ToLower();
            int totalRegistros;
            using (var dbConnection = _context.CreateConnection())
            {
                try
                {

                    dbConnection.Open();

                    totalRegistros = await dbConnection.QuerySingleAsync<int>(totalRegistrosScript, null);
                    return totalRegistros;
                }
                catch (Exception ex)
                {
                    AddNotification($"Erro ao contar os produtos. \nErro: {ex.Message}");
                    throw;
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
                    dbConnection.Open();
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
                    AddNotification("Erro ao atualizar o produto. \nErro: {0}".ToFormat(ex.Message));
                    throw;

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
                    dbConnection.Open();
                    var rowsAffected = await dbConnection.ExecuteAsync(script, new { Id = id });
                    if (rowsAffected == 0)
                    {
                        AddNotification("Nenhum registro foi apagado. Verifique se o ID do produto é válido.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // Log e tratamento da exceção
                    AddNotification("Erro ao excluir o produto. \nErro: {0}".ToFormat(ex.Message));
                    throw;
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
                // Liberar recursos gerenciados que implementam IDisposable
                _context?.Dispose();
            }

            // Liberar recursos não gerenciados (se houver)

            _disposed = true;
        }

        
    }
}
