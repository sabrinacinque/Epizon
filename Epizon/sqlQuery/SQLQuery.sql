


USE Epizon;
SELECT * FROM Articoli;

SELECT * FROM Utente;

delete from Utente where id=43;

SELECT * FROM Ordini;
delete from Ordini where id=8;


SELECT * FROM OrdineArticoli;

SELECT * FROM OrdineArticoli;



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
    ([Email], [Password], [Ruolo], [Compratore_Nome], [Compratore_Cognome],[Compratore_Indirizzo],[Compratore_Citta],[Compratore_CAP],[Compratore_Provincia],[Compratore_Telefono])
VALUES 
    ('compratore1@gmail.com', 'password', 'Compratore', 'Luigi', 'Verdi', 'via compratore 1',  'Milano', '20100', 'MI',3282525266),
    ('compratore2@gmail.com', 'password', 'Compratore', 'Luisa', 'Bianchi', 'via compratore 2',  'Napoli', '80147', 'NA',3285687446),
    ('compratore3@gmail.com', 'password', 'Compratore', 'Mario', 'Rossi', 'via compratore 3',  'Bologna', '25669', 'BO',3278456912);


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


    INSERT INTO [dbo].[Articoli] 
    ([Titolo], [Descrizione], [Prezzo], [ImmagineCopertina], [Immagine2], [Immagine3], [TempiDiSpedizione], [Categoria], [RivenditoreId])
VALUES 
    ('Tuc Original', 'Tuc Original, Snack Friabili dal Gusto Dolce e Salato con Cottura al Forno e Grano 100% Italiano, 100g', 1.39, 'tucCopertina.jpg', 'tuc2.jpg', 'tuc3.jpg', 3, 'Alimentari e bevande', 30),
    ('Mulino Bianco Grissini Sgranocchi', 'Mulino Bianco Grissini Sgranocchi Croccanti, Perfetti come Snack, 210 g', 2.30, 'sgranocchiCopertina.jpg', 'sgranocchi2.jpg', 'sgranocchi3.jpg', 4, 'Alimentari e bevande', 30);


    INSERT INTO [dbo].[Articoli] 
    ([Titolo], [Descrizione], [Prezzo], [ImmagineCopertina], [Immagine2], [Immagine3], [TempiDiSpedizione], [Categoria], [RivenditoreId])
VALUES 
    ('Wizz Arachidi', 'Aperisnack® - AP04.002.03 – Wizz Arachidi Ricoperte Chili Secchiello Large da 2300gr. Snack Salati e Stuzzichini Ideali per i tuoi aperitivi e le tue feste', 29.43, 'arachidiCopertina.jpg', 'arachidi2.jpg', 'arachidi3.jpg', 4, 'Alimentari e bevande', 30),
    ('Penna Antifungina da 3ml', 'NAIL LAB Trattamento Fungino Premium per Unghie dei Piedi Extra Forte-50ml-Contiene Olio di Albero del Tè, Fungo dell unghia per le Unghie delle Mani - Penna Antifungina da 3ml+Lima per Unghie Gratis', 14.99, 'fungomerCopertina.jpg', 'fungomer2.jpg', 'fungomer3.jpg', 2, 'Farmacia e cura della persona', 31),
    ('Arnica Crema Effetto Termico', 'Dr. Theiss Arnica Crema Effetto Termico - Crema Riscaldante 50 ml - Dona Sollievo e Benessere - Coadiuvante per la Pelle del Corpo, con Peperoncino, Canfora e Oli Essenziali di Rosmarino e Abete', 7.42, 'arnicaCopertina.jpg', 'arnica2.jpg', 'arnica3.jpg', 4, 'Farmacia e cura della persona', 31),
    ('Integratore Anti-Age per Labbra Volumizzate', 'Integratore Anti-Age per Labbra Volumizzate, Idratate, Più giovani Specifico per Donna Dermatologicamente Testato con Collagene Acido Ialuronico Coenzima Q10 Migliora pelle e labbra con effetto filler', 19.70, 'lalipsCopertina.jpg', 'lalips2.jpg', 'lalips3.jpg', 2, 'Farmacia e cura della persona', 31),
    ('Arnica Crema Effetto Termico', 'Dr. Theiss Arnica Crema Effetto Termico - Crema Riscaldante 50 ml - Dona Sollievo e Benessere - Coadiuvante per la Pelle del Corpo, con Peperoncino, Canfora e Oli Essenziali di Rosmarino e Abete', 8.52, 'arnicaCopertina.jpg', 'arnica2.jpg', 'arnica3.jpg', 1, 'Farmacia e cura della persona', 31),
    ('Integratore Con Collagene Marino', 'Rabava 120 Cps Integratore Con Collagene Marino Idrolizzato 1000 Mg, Biotina Capelli Unghie, Acido Ialuronico, Coenzima Q10, Zinco Vitamina C Difese Immunitarie, Collagen Viso Pelle Ossa Articolazioni', 19.90, 'collageneCopertina.jpg', 'collagene2.jpg', 'collagene3.jpg', 2, 'Farmacia e cura della persona', 31);


    INSERT INTO [dbo].[Articoli] 
    ([Titolo], [Descrizione], [Prezzo], [ImmagineCopertina], [Immagine2], [Immagine3], [TempiDiSpedizione], [Categoria], [RivenditoreId])
VALUES 
    ('MASTERTOP Scale per Cani', 'MASTERTOP Scale per Cani a 2 Gradini, Scale Antiscivolo per Animali Domestici in Spugna con Fodera Rimovibile e Lavabile, Rampes Cuccioli Inclinata per Divano Letto (Blu)', 37.99, 'scaleCopertina.jpg', 'scale2.jpg', 'scale3.jpg', 2, 'Animali domestici', 32),
    ('Box Pieghevole per Animali Domestici', 'Box Pieghevole per Animali Domestici, Recinti Gioco Portatile Animali Domestici, Impermeabile Oxford Box per Cani, Box Cani per Interno, per Cuccioli, Conigli, Gatti, 73 x 73 x 43 cm', 24.99, 'boxCopertina.jpg', 'box2.jpg', 'box3.jpg', 5, 'Animali domestici', 32),
    ('Distributore Automatico di Crocchette', 'PetSafe PFD19-15521 Healthy Pet Simply Feed Distributore Automatico di Crocchette per Animali Domestici - 2.93 kg', 161.42, 'distributoreCopertina.jpg', 'distributore2.jpg', 'distributore3.jpg', 2, 'Animali domestici', 32),
    ('Collare per cani personalizzato', 'Collare per cani personalizzato con fibbia in metallo, collari per animali domestici personalizzati con nome numero di telefono indirizzo inciso, plaid', 18.99, 'collareCopertina.jpg', 'collare2.jpg', 'collare3.jpg', 3, 'Animali domestici', 32),
    ('Termometro digitale per animali domestici', 'Termometro digitale per animali domestici, per proprietari di animali, cani, gatti, cavalli, veterinari, sonda di temperatura flessibile, include tabella veterinaria Hobdays', 10.95, 'termometroCopertina.jpg', 'termometro2.jpg', 'termometro3.jpg', 1, 'Animali domestici', 32);


    INSERT INTO [dbo].[Articoli] 
    ([Titolo], [Descrizione], [Prezzo], [ImmagineCopertina], [Immagine2], [Immagine3], [TempiDiSpedizione], [Categoria], [RivenditoreId])
VALUES 
    ('Palette di correttori', 'Palette di correttori in 12 colori, blush e correttori, per trucco a coprenza totale, per occhiaie e trucco viso', 24.99, 'palettaCopertina.jpg', 'paletta2.jpg', 'paletta3.jpg', 3, 'Moda e bellezza', 33),
    ('Oulac Rossetto Metallizzato Lucido', 'Oulac Rossetto Metallizzato Lucido, Rossetto Rosa Altamente Pigmentato, Lucentezza 3D, Rossetto Lunga Durata, Formula Cremosa e Idratante, Vegan, 4,3g (15) Velocity', 6.25, 'rossettoCopertina.jpg', 'rossetto2.jpg', 'rossetto3.jpg', 1, 'Moda e bellezza', 33),
    ('VON LILIENFELD® Borsa Shopping Vincent van Gogh', 'VON LILIENFELD® Borsa Shopping Vincent van Gogh: Caffè notturno Spiaggia Shopper Tracolla Spazioso Saccoccia Arte', 29.90, 'borsaCopertina.jpg', 'borsa2.jpg', 'borsa3.jpg', 2, 'Moda e bellezza', 33),
    ('Collare per cani personalizzato', 'Collare per cani personalizzato con fibbia in metallo, collari per animali domestici personalizzati con nome numero di telefono indirizzo inciso, plaid', 18.99, 'collareCopertina.jpg', 'collare2.jpg', 'collare3.jpg', 3, 'Moda e bellezza', 33),
    ('Vestito Donna Elegante', 'QKEPCY Vestito Donna Elegante Corto Abito Spalle Scoperte Casual Moda Vestitino Tunica Primavera Estate Mini Abitino Copricostume Abito a Tinta Unita a Pieghe per Il Tempo Libero', 26.99, 'vestitoCopertina.jpg', 'vestito2.jpg', 'vestito3.jpg', 1, 'Moda e bellezza', 33);
