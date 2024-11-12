INSERT INTO produto
	(Nome, Descricao, Preco, Data_Validade, Imagem, Categoria_Id)
VALUES 
	(@Nome, @Descricao, @Preco, @Data_Validade, @Imagem, @Categoria_Id);
SELECT LAST_INSERT_ID();