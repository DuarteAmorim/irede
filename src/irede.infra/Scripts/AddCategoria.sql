INSERT INTO categoria (nome)
VALUES (@nome);
SELECT LAST_INSERT_ID() AS id;