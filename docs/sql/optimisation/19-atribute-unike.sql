-- 19 - Fushat identifikuese (unike) te llojit te dokumentit
--
-- Kontrolli i dublikateve (prc_T_KOKASHITJE_ekzistonRegjistrimShitjeSipasIdentifikuese dhe ai i magazines) therritet
-- 2-3 here per cdo dokument te ruajtur (krijim, ruajtje dhe trigger-i KONTROLLOEKZISTENCE). Hapi i pare lexon fushat
-- unike te konfigurimit: T_ATRIBUTETRUPI WHERE IDKONFIGAMBJENTE = ... AND UNIKE = 1. Indeksi ishte vetem mbi
-- IDKONFIGAMBJENTE, keshtu qe per ~400 atributet e konfigurimit lexohej rreshti i plote per te pare UNIKE
-- (~800 lexime, 1,3 ms per thirrje). Me UNIKE ne indeks (i njejti emer, e njejta kolone e pare): kerkim i drejtperdrejte.
-- Rikthimi: 19-rollback-atribute-unike.sql
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_ATRIBUTETRUPI') AND name = N'I_t_atribute_trupi_konfigAmbjenti')
    CREATE NONCLUSTERED INDEX I_t_atribute_trupi_konfigAmbjenti ON dbo.T_ATRIBUTETRUPI (IDKONFIGAMBJENTE, UNIKE)
        WITH (DROP_EXISTING = ON);
ELSE
    CREATE NONCLUSTERED INDEX I_t_atribute_trupi_konfigAmbjenti ON dbo.T_ATRIBUTETRUPI (IDKONFIGAMBJENTE, UNIKE);
GO
