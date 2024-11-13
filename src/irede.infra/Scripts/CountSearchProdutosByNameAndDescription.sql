SELECT COUNT(*) 
FROM 
    produto p
JOIN 
    categoria c ON p.id_categoria = c.id
WHERE 
    (@TermoNome IS NULL OR p.nome LIKE CONCAT('%', @TermoNome, '%'))
    AND (@TermoDescricao IS NULL OR p.descricao LIKE CONCAT('%', @TermoDescricao, '%'));
