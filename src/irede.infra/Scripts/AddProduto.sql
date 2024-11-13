INSERT INTO produto
	(Nome, Descricao, Preco, Data_Validade, Imagem, id_categoria)
VALUES 
	(@Nome, @Descricao, @Preco, @Data_Validade, @Imagem, @categoriaId);
SELECT LAST_INSERT_ID();