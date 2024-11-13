UPDATE Produto SET 
    Nome = @Nome,
    Descricao = @Descricao,
    Preco = @Preco,
    DataValidade = @DataValidade,
    Imagem = @Imagem,
    id_categoria = @CategoriaId
WHERE Id = @Id;