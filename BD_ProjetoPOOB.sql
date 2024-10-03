USE master;

DROP DATABASE IF EXISTS ProjetoPOOB;
CREATE DATABASE ProjetoPOOB;

USE ProjetoPOOB

DROP TABLE IF EXISTS cliente;
CREATE TABLE cliente (
	id_cliente INT IDENTITY,
	nome VARCHAR(255) NOT NULL,
	rg VARCHAR(20) NULL,
	cpf CHAR(11) NOT NULL UNIQUE,
	dt_nascimento DATE NULL,
	telefone VARCHAR(15) NULL,
	PRIMARY KEY (id_cliente)
);

DROP TABLE IF EXISTS produto;
CREATE TABLE produto (
	id_produto INT IDENTITY,
	descricao VARCHAR(255) NOT NULL,
	codigo_barras VARCHAR(13) NOT NULL,
	unidade CHAR(10) NULL,
	preco_venda DECIMAL(18,2) NOT NULL DEFAULT '(0.00)',
	estoque_atual INT NOT NULL DEFAULT '(0)',
	PRIMARY KEY (id_produto)
);

DROP TABLE IF EXISTS pedido;
CREATE TABLE pedido (
	id_pedido INT IDENTITY,
	data_hora DATETIME NOT NULL,
	id_cliente INT NOT NULL,
	status CHAR(1) NOT NULL DEFAULT 'P',
	PRIMARY KEY (id_pedido),
	FOREIGN KEY (id_cliente) REFERENCES cliente (id_cliente)
);

DROP TABLE IF EXISTS pedido_item;
CREATE TABLE pedido_item (
	id_pedido_item INT IDENTITY,
	id_pedido INT NOT NULL,
	id_produto INT NOT NULL,
	quantidade INT NOT NULL,
	valor_unitario DECIMAL(18,2) NOT NULL DEFAULT '(0.00)',
	valor_total DECIMAL(18,2) NOT NULL DEFAULT '(0.00)',
	PRIMARY KEY (id_pedido, id_pedido_item),
	FOREIGN KEY (id_pedido) REFERENCES pedido (id_pedido),
	FOREIGN KEY (id_produto) REFERENCES produto (id_produto)
);