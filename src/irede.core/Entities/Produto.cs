﻿using irede.core.Dtos.Core;
using irede.shared.Notifications;

namespace irede.core.Entities
{
    public class Produto: Notifiable
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double Preco { get; private set; }
        public DateTime Data_Validade { get; private set; }
        public string Imagem { get; private set; }
        public int CategoriaId { get; private set; }
        public virtual Categoria Categoria { get; private set; }
        public void SetCategoria(Categoria categoria)
        {
            Categoria = categoria;
        }
        public Produto()
        {
                
        }
        public Produto(string nome, string descricao, double preco, DateTime dataValidade, string imagem, int idCategoria)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Data_Validade = dataValidade;
            Imagem = imagem;
            CategoriaId = idCategoria;

            Validate();
        }

        public Produto(int id, string nome, string descricao, double preco, DateTime data_validade, string imagem, int id_categoria)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Data_Validade = data_validade;
            Imagem = imagem;
            CategoriaId = id_categoria;

            
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

            if (Id>0 && Data_Validade < DateTime.Now.Date) 
                AddNotification("A data de validade não pode ser anterior à data atual.");

            if (string.IsNullOrEmpty(Imagem))
                AddNotification("A imagem do produto é obrigatória.");
        }
        public void ValidateUpdate()
        {
            if (Id <= 0)
                AddNotification($"O Id da categoria é inválido.");

            Validate();
        }

        public static explicit operator Produto(ProdutoDto dto)
        {
            return new Produto(dto.Id, dto.Nome, dto.Descricao, dto.Preco, dto.Data_Validade, dto.Imagem, dto.CategoriaId);
        }


    }
}
