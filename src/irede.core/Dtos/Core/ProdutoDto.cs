using irede.core.Entities;
using irede.shared.Notifications;

namespace irede.core.Dtos.Core
{
    public class ProdutoDto: Notifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public DateTime Data_Validade { get; set; }
        public string Imagem { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; } // Nome da Categoria relacionada

        public static explicit operator ProdutoDto(Produto entidade)
        {
            return new ProdutoDto()
            {
                Nome = entidade.Nome,
                Descricao = entidade.Descricao,
                Preco = entidade.Preco,
                Data_Validade = entidade.Data_Validade,
                Imagem = entidade.Imagem,
                CategoriaId = entidade.Id_Categoria
            };
        }
    }
}
