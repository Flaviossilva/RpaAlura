-- Cria o banco de dados
CREATE DATABASE RpaAlura;
GO

-- Usa o banco de dados criado
USE RpaAlura;
GO

-- Cria a tabela Course
CREATE TABLE Courses
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(500) NOT NULL,
    Professor VARCHAR(500) NOT NULL,
    Carga_Horaria VARCHAR(10) NOT NULL,
    Descricao VARCHAR(500) NOT NULL
);
GO

-- Cria o login para o SQL Server
CREATE LOGIN Desenvolvedor WITH PASSWORD = 'rpaalura';
GO

-- Cria o usuário no banco de dados
CREATE USER Desenvolvedor FOR LOGIN Desenvolvedor;
GO

-- Concede permissões ao usuário
ALTER ROLE db_owner ADD MEMBER Desenvolvedor;
GO
