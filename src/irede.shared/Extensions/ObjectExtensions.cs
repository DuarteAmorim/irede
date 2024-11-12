using System.Text.Encodings.Web;
using System.Text.Json;

namespace irede.shared.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converte o objeto para formato JSON
        /// </summary>
        /// <param name="instance">Objeto instanciado</param>
        /// <returns>string do JSON</returns>
        public static string AsJson(this object instance)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, //permite que eu digite acentos e caracteres especiais (exceto os html: <,>,/)
                WriteIndented = true,
                PropertyNameCaseInsensitive = true

            };
            var jsonText = JsonSerializer.Serialize(instance, options);
            return jsonText;

        }
    }
}
