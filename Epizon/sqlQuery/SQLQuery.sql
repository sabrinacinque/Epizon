

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


SELECT oa.*
FROM OrdineArticoli oa
JOIN Articoli a ON oa.ArticoloId = a.Id
WHERE a.RivenditoreId = rivenditoreId;
SELECT * FROM Articoli WHERE RivenditoreId = 9;


SELECT 
    o.DataOrdine,
    a.Titolo AS ArticoloTitolo,
    a.Descrizione AS ArticoloDescrizione,
    a.ImmagineCopertina AS ArticoloImmagineCopertina,
    a.Prezzo AS ArticoloPrezzo,
    oa.Quantità AS QuantitàOrdinata,
    u.Compratore_nome AS NomeCompratore,
    u.Compratore_cognome AS CognomeCompratore,
    u.Compratore_indirizzo AS IndirizzoCompratore,
    u.Compratore_Citta AS CittàCompratore,
    u.Compratore_CAP AS CAPCompratore,
    u.Compratore_Provincia AS ProvinciaCompratore,
    u.Compratore_telefono AS TelefonoCompratore
FROM 
    Ordini o
JOIN 
    OrdineArticoli oa ON o.Id = oa.OrdineId
JOIN 
    Articoli a ON oa.ArticoloId = a.Id
JOIN 
    Utente u ON o.CompratoreId = u.Id
WHERE 
    a.RivenditoreId = 9;

