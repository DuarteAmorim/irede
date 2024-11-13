	SELECT 
    p.id, 
    p.nome, 
    p.descricao, 
    p.preco, 
    p.data_validade, 
    p.imagem, 
    p.id_categoria,
    c.id AS Id, 
    c.nome AS Nome
FROM 
    produto p
JOIN 
    categoria c ON p.id_categoria = c.id
ORDER BY p.id
LIMIT @Limit OFFSET @Offset;
