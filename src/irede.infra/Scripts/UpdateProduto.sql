UPDATE Produto SET 
    Nome = @Nome,
    Descricao = @Descricao,
    Preco = @Preco,
    DataValidade = @DataValidade,
    Imagem = @Imagem,
    CategoriaId = @CategoriaId
WHERE Id = @Id;