using irede.core.Entities;
using irede.shared.Notifications;

namespace irede.core.Dtos.Core
{
    public class ProdutoDto: Notifiable
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public DateTime DataValidade { get; set; }
        public string Imagem { get; set; }
        public int CategoriaId { get; set; }

        public static explicit operator ProdutoDto(Produto entidade)
        {
            return new ProdutoDto()
            {
                Nome = entidade.Nome,
                Descricao = entidade.Descricao,
                Preco = entidade.Preco,
                DataValidade = entidade.Data_validade,
                Imagem = entidade.Imagem,
                CategoriaId = entidade.Id_categoria
            };
        }
    }
}
