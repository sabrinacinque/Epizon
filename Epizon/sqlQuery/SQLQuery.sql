INSERT INTO Categorie (Nome, Descrizione)
VALUES ('Informatica', 'Categorie di prodotti informatici e tecnologie'),
       ('Piccoli e grandi elettrodomestici', 'Categorie di elettrodomestici di varie dimensioni');

SELECT * FROM categorie;
USE Epizon;
SELECT * FROM Articoli;
SELECT * FROM Utente;
SELECT * FROM Ordini;
Delete from Articoli where id =3;
SELECT * FROM OrdineArticoli;
Delete from utente where id = 10;
DROP table Articoli;
DROP table _EFMigrationsHistory;
DROP table Categorie;
ALTER TABLE Articoli
DROP COLUMN CategoriaId;
DROP INDEX IX_Articoli_CategoriaId ON Articoli;
ALTER TABLE Articoli
DROP CONSTRAINT FK_Articoli_Categorie_CategoriaId;
ALTER TABLE Articoli
DROP COLUMN CategoriaId;
ALTER TABLE Ordini
DROP COLUMN RivenditoreId;
ALTER TABLE Ordini
DROP CONSTRAINT FK_Ordini_Utente_RivenditoreId;
DROP INDEX IX_Ordini_RivenditoreId ON Ordini;

SELECT COLUMN_NAME 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Articoli' AND COLUMN_NAME = 'RivenditoreId';
