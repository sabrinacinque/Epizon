


USE Epizon;
SELECT * FROM Articoli;

SELECT * FROM Utente;

delete from Utente where id=42;

SELECT * FROM Ordini;


SELECT * FROM OrdineArticoli;
DELETE FROM OrdineArticoli;
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


    DELETE FROM nome_tabella;


INSERT INTO [dbo].[Utente] 
    ([Email], [Password], [Ruolo], [RagioneSociale], [Nome], [Cognome], [PartitaIva], [Indirizzo], [Citta], [CAP], [Provincia], [Telefono], [Pec], [CodiceDestinatario])
VALUES 
    ('rivenditore2@gmail.com', 'password', 'Rivenditore', 'Rivenditore Due SRL', 'Luigi', 'Verdi', '12345678902', 'Via Rivenditore 2', 'Milano', '20100', 'MI', '0987654321', 'pec2@example.com', 'ABCDE02'),
    ('rivenditore3@gmail.com', 'password', 'Rivenditore', 'Rivenditore Tre SRL', 'Giovanni', 'Bianchi', '12345678903', 'Via Rivenditore 3', 'Napoli', '80100', 'NA', '1122334455', 'pec3@example.com', 'ABCDE03'),
    ('rivenditore4@gmail.com', 'password', 'Rivenditore', 'Rivenditore Quattro SRL', 'Carlo', 'Gialli', '12345678904', 'Via Rivenditore 4', 'Torino', '10100', 'TO', '5566778899', 'pec4@example.com', 'ABCDE04'),
    ('rivenditore5@gmail.com', 'password', 'Rivenditore', 'Rivenditore Cinque SRL', 'Andrea', 'Azzurri', '12345678905', 'Via Rivenditore 5', 'Firenze', '50100', 'FI', '6677889900', 'pec5@example.com', 'ABCDE05'),
    ('rivenditore6@gmail.com', 'password', 'Rivenditore', 'Rivenditore Sei SRL', 'Roberto', 'Neri', '12345678906', 'Via Rivenditore 6', 'Bologna', '40100', 'BO', '7788990011', 'pec6@example.com', 'ABCDE06'),
    ('rivenditore7@gmail.com', 'password', 'Rivenditore', 'Rivenditore Sette SRL', 'Francesco', 'Viola', '12345678907', 'Via Rivenditore 7', 'Genova', '16100', 'GE', '8899001122', 'pec7@example.com', 'ABCDE07'),
    ('rivenditore8@gmail.com', 'password', 'Rivenditore', 'Rivenditore Otto SRL', 'Marco', 'Marroni', '12345678908', 'Via Rivenditore 8', 'Palermo', '90100', 'PA', '9900112233', 'pec8@example.com', 'ABCDE08'),
    ('rivenditore9@gmail.com', 'password', 'Rivenditore', 'Rivenditore Nove SRL', 'Paolo', 'Verdi', '12345678909', 'Via Rivenditore 9', 'Bari', '70100', 'BA', '0011223344', 'pec9@example.com', 'ABCDE09'),
    ('rivenditore10@gmail.com', 'password', 'Rivenditore', 'Rivenditore Dieci SRL', 'Andrea', 'Bianchi', '12345685909', 'Via Rivenditore 10', 'Caserta', '81030', 'CE', '0011223984', 'pec10@example.com', 'ABCDE10');
