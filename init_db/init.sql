-- init_db/init.sql

-- Seleciona o banco de dados a ser usado
USE iredeDb;

-- Criação da tabela Categoria
CREATE TABLE IF NOT EXISTS categoria (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL
);

-- Criação da tabela Produto
CREATE TABLE IF NOT EXISTS produto (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(50) NOT NULL,
    descricao VARCHAR(200),
    preco DOUBLE NOT NULL,
    data_validade DATE NOT NULL,
    imagem VARCHAR(255) NOT NULL,
    id_categoria INT NOT NULL,
    CONSTRAINT FK_Categoria FOREIGN KEY (id_categoria) REFERENCES categoria(id)
);