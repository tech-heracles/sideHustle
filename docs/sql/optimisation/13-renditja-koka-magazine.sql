-- 13 - Numri i renditjes se dokumentit te magazines (prc_T_KOKAMAGAZINA_ktheMaxIdRenditjes, per cdo dokument qe ruhet)
--
-- Procedura kerkon renditjen me te madhe te magazines ne ate date (MAX(IDRENDITJES) per IDMAG + DTDOK) dhe llojin e
-- dokumentit qe e ka. Indeksi i vetem sipas dates ishte vetem (DTDOK): per cdo dokument te dates kerkohej ne tabele
-- IDMAG, statusi dhe renditja (~1500 lexime, ~5 ms per dokument). Indeksi tani renditet (DTDOK, IDMAG, IDRENDITJES) dhe
-- perfshin kolonat e tjera qe lexon procedura. DTDOK mbetet kolona e pare, keshtu qe pyetjet qe e perdornin vazhdojne
-- ta perdorin. Procedura nuk ndryshon.
-- Rikthimi: 13-rollback-renditja-koka-magazine.sql
-- Ruhet gjendja para ndryshimit (00-ruajtje.sql); rikthimi e perdor ate.
EXEC dbo.prc_OPTIMIZIM_Ruaj '13', N'dbo.T_KOKAMAGAZINA', N'IX_T_KOKAMAGAZINA_DTDOK';
GO
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_KOKAMAGAZINA') AND name = N'IX_T_KOKAMAGAZINA_DTDOK')
    CREATE NONCLUSTERED INDEX IX_T_KOKAMAGAZINA_DTDOK ON dbo.T_KOKAMAGAZINA (DTDOK, IDMAG, IDRENDITJES)
        INCLUDE (IDSTATUSDOK, IDLLOJDOKUMENTIMAGAZINE, IDKONFIGGJENERUES)
        WITH (DROP_EXISTING = ON)
ELSE
    CREATE NONCLUSTERED INDEX IX_T_KOKAMAGAZINA_DTDOK ON dbo.T_KOKAMAGAZINA (DTDOK, IDMAG, IDRENDITJES)
        INCLUDE (IDSTATUSDOK, IDLLOJDOKUMENTIMAGAZINE, IDKONFIGGJENERUES);
GO
