using irede.shared.Notifications;

namespace irede.core.Entities
{
    public class Produto: Notifiable
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double Preco { get; private set; }
        public DateTime Data_validade { get; private set; }
        public string Imagem { get; private set; }
        public int Id_categoria { get; private set; }
        public Categoria Categoria { get; private set; }

        public Produto(string nome, string descricao, double preco, DateTime dataValidade, string imagem, Categoria categoria)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Data_validade = dataValidade;
            Imagem = imagem;
            Categoria = categoria;
            Id_categoria = categoria.Id;

            Validate();
        }

        public Produto(int id, string nome, string descricao, double preco, DateTime data_validade, string imagem, int id_categoria, Categoria categoria)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Data_validade = data_validade;
            Imagem = imagem;
            Id_categoria = id_categoria;
            Categoria = categoria;
        }

        public void SetId(int id) => Id = id;
        public void Validate()
        {
            if (string.IsNullOrEmpty(Nome) || Nome.Length > 50)
                AddNotification("O nome do produto é obrigatório e não pode exceder 50 caracteres.");

            if (!string.IsNullOrEmpty(Descricao) && Descricao.Length > 200)
                AddNotification("A descrição não pode exceder 200 caracteres.");

            if (Preco <= 0)
                AddNotification("O preço deve ser um valor positivo.");

            //TODO: verificar desafio para validar isso aqui: validação acontece somento para um novo produto
            if (Id>0 && Data_validade < DateTime.Now.Date) 
                AddNotification("A data de validade não pode ser anterior à data atual.");

            if (string.IsNullOrEmpty(Imagem))
                AddNotification("A imagem do produto é obrigatória.");

            if (Categoria == null)
                AddNotification("O produto deve estar associado a uma categoria.");
        }
    }
}
