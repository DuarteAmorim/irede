SELECT 
    p.id, 
    p.nome, 
    p.descricao, 
    p.preco, 
    p.data_validade, 
    p.imagem, 
    p.id_categoria as categoriaid,
    c.id AS Id, 
    c.nome AS Nome
FROM 
    produto p
JOIN 
    categoria c ON p.id_categoria = c.id
WHERE p.id = @Id;