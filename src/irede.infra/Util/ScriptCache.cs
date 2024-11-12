using irede.infra.Interfaces;
using irede.infra.Util;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace irede.infra.Scripts
{
    public class ScriptCache : IScriptCache
    {
        private static readonly ConcurrentDictionary<string, string> Scripts = new ConcurrentDictionary<string, string>();
        //private static readonly string BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts");
        //private readonly string _basePath;
         private readonly string _scriptsPath;
        private bool _disposed = false;

        //public ScriptCache(string basePath)
        //{
        //    // Verifica se o caminho é nulo ou vazio
        //    if (string.IsNullOrWhiteSpace(basePath))
        //        throw new ArgumentException("O caminho base não pode ser nulo ou vazio.", nameof(basePath));

        //    _basePath = basePath;
        //}

        //public string GetScript(string relativePath)
        //{

        //    // Tenta obter o script do cache ou carregá-lo se não estiver presente
        //    return Scripts.GetOrAdd(relativePath, path =>
        //    {
        //        var fullPath = Path.Combine(BasePath, path);

        //        // Verifica se o arquivo existe
        //        if (!File.Exists(fullPath))
        //            throw new FileNotFoundException($"Script SQL não encontrado: {fullPath}");

        //        // Lê o conteúdo do arquivo
        //        return File.ReadAllText(fullPath);
        //    });
        //}

        public ScriptCache(IOptions<ScriptSettings> options)
        {
            // Verifica se o caminho é nulo ou vazio
            if (options == null || string.IsNullOrWhiteSpace(options.Value.ScriptsPath))
                throw new ArgumentException("O caminho base dos scripts não pode ser nulo ou vazio.", nameof(options));

            _scriptsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.Value.ScriptsPath);

            // Verifica se o diretório existe
            if (!Directory.Exists(_scriptsPath))
                throw new DirectoryNotFoundException($"O diretório dos scripts não foi encontrado: {_scriptsPath}");
        }

        public string GetScript(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new ArgumentException("O caminho relativo do script não pode ser nulo ou vazio.", nameof(relativePath));

            // Tenta obter o script do cache ou carregá-lo se não estiver presente
            return Scripts.GetOrAdd(relativePath, path =>
            {
                var fullPath = Path.Combine(_scriptsPath, path);

                // Verifica se o arquivo existe
                if (!File.Exists(fullPath))
                    throw new FileNotFoundException($"Script SQL não encontrado: {fullPath}");

                // Lê o conteúdo do arquivo
                return File.ReadAllText(fullPath);
            });
        }

    }

}
