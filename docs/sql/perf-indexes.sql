/*
    Performance indexes for AVEC (not applied yet: DB changes are on hold).

    Chosen from sys.dm_db_missing_index_* on the AVEC database (2026-09-25) and from profiling page loads.
    Each index is narrow, only speeds up reads, and changes no data or behaviour. Safe to run more than once.

    Expected effect
      1. T_GRIDATRUPI (1.1M rows) is read by GRIDAKOKAID on every page that shows a grid, and inside the customer
         list procedure. Today each read scans the table: 100-250 ms, several times per page. With the index: ~1 ms.
         (The app now caches most of these reads, but the first load after a change, the customer list and any other
         procedure joining T_GRIDATRUPI still scan.)
      2. T_LLOGARI by account number + company: 8,000+ lookups (imports, document saving).
      3. T_ROLDREJTATRUPI (560k rows) by component: the rights check behind the main menu (~90 ms per load).
      4. The remaining ones: frequent lookups on smaller tables (controls, accounting schemes, grid headers, KPF).

    Run in SSMS against AVEC in a quiet moment (each CREATE INDEX briefly locks its table; T_GRIDATRUPI takes the longest).
    To undo: DROP INDEX <name> ON <table>.
*/
USE AVEC;
GO
SET NOCOUNT ON;

-- 1. grid configuration
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_GRIDATRUPI_GRIDAKOKAID' AND object_id = OBJECT_ID('dbo.T_GRIDATRUPI'))
    CREATE NONCLUSTERED INDEX IX_T_GRIDATRUPI_GRIDAKOKAID
        ON dbo.T_GRIDATRUPI (GRIDAKOKAID, GRIDATRUPIKODI)
        INCLUDE (GRIDATRUPIINDEX, GRIDATRUPIVISIBLE, IDKONFIGLUPA);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_LUPAMULTIPLE_GRIDATRUPIID' AND object_id = OBJECT_ID('dbo.T_LUPAMULTIPLE'))
    CREATE NONCLUSTERED INDEX IX_T_LUPAMULTIPLE_GRIDATRUPIID
        ON dbo.T_LUPAMULTIPLE (GRIDATRUPIID)
        INCLUDE (IDKONFIGAMBJENTELUPA);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_GRIDAKOKA_EMRI_KONFIG' AND object_id = OBJECT_ID('dbo.T_GRIDAKOKA'))
    CREATE NONCLUSTERED INDEX IX_T_GRIDAKOKA_EMRI_KONFIG
        ON dbo.T_GRIDAKOKA (GRIDKOKAEMRI, IDKONFIGURIM)
        INCLUDE (GRIDKOMPID);

-- 2. accounts by number
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_LLOGARI_NR_NDERMARJE' AND object_id = OBJECT_ID('dbo.T_LLOGARI'))
    CREATE NONCLUSTERED INDEX IX_T_LLOGARI_NR_NDERMARJE
        ON dbo.T_LLOGARI (NRLLOGARI, IDNDERMARJE)
        INCLUDE (IDSTATUSDOK);

-- 3. rights per component (main menu)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_ROLDREJTATRUPI_KOMPONENTE' AND object_id = OBJECT_ID('dbo.T_ROLDREJTATRUPI'))
    CREATE NONCLUSTERED INDEX IX_T_ROLDREJTATRUPI_KOMPONENTE
        ON dbo.T_ROLDREJTATRUPI (IDKOMPONENTE, D_AMB)
        INCLUDE (IDDREJTAKOKA, IDNIVELREGJISTRIMI);

-- 4. smaller, frequent lookups
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_KONTROLLE_KOMPONENTE_KOD' AND object_id = OBJECT_ID('dbo.T_KONTROLLE'))
    CREATE NONCLUSTERED INDEX IX_T_KONTROLLE_KOMPONENTE_KOD
        ON dbo.T_KONTROLLE (IDKOMPONENTE, KODKONTROLL);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_SKEMAKONTABILITETIARTIKULLI_KOD' AND object_id = OBJECT_ID('dbo.T_SKEMAKONTABILITETIARTIKULLI'))
    CREATE NONCLUSTERED INDEX IX_T_SKEMAKONTABILITETIARTIKULLI_KOD
        ON dbo.T_SKEMAKONTABILITETIARTIKULLI (KODISKEMAKONTABILITETIARTIKULLI, IDNDERMARJE, LLOJIART);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_KPF_NDERMARJE_STATUS' AND object_id = OBJECT_ID('dbo.T_KPF'))
    CREATE NONCLUSTERED INDEX IX_T_KPF_NDERMARJE_STATUS
        ON dbo.T_KPF (IDNDERMARJE, IDSTATUSDOK)
        INCLUDE (KODIKPF, NIVELIKPF);

-- 5. "document already exists" checks run once per imported document
--    (prc_T_KOKASHITJE_ekzistonRegjistrimShitjeSipasIdentifikuese, prc_T_KOKAMAGAZINA_ekzistonRegjistrimMagazineSipasIdentifikuesi).
--    Their WHERE uses "col = CASE WHEN ... THEN @x ELSE col END", which cannot seek, so each call reads all documents of the
--    company. These narrow indexes make that read much smaller. Adding OPTION (RECOMPILE) to the two procedures would let
--    SQL Server seek on NRDOK directly (a procedure change: left for the DB round).
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_KOKASHITJE_NDERM_NRDOK' AND object_id = OBJECT_ID('dbo.T_KOKASHITJE'))
    CREATE NONCLUSTERED INDEX IX_T_KOKASHITJE_NDERM_NRDOK
        ON dbo.T_KOKASHITJE (IDNDERM, NRDOK)
        INCLUDE (IDSTATUSDOK, IDNIVEL, IDKONFIGAMBJENTE, DTDOK, IDKLIENTFURNITOR, NRSERIAL);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_T_KOKAMAGAZINA_NDERM_NRDOK' AND object_id = OBJECT_ID('dbo.T_KOKAMAGAZINA'))
    CREATE NONCLUSTERED INDEX IX_T_KOKAMAGAZINA_NDERM_NRDOK
        ON dbo.T_KOKAMAGAZINA (IDNDERM, NRDOK)
        INCLUDE (IDSTATUSDOK, IDNIVEL, IDKONFIGAMBJENTE, DTDOK, IDKLIENTFURNITOR, IDMAG);

-- check
SELECT OBJECT_NAME(object_id) AS tabela, name AS indeksi
FROM sys.indexes
WHERE name IN ('IX_T_GRIDATRUPI_GRIDAKOKAID', 'IX_T_LUPAMULTIPLE_GRIDATRUPIID', 'IX_T_GRIDAKOKA_EMRI_KONFIG',
               'IX_T_LLOGARI_NR_NDERMARJE', 'IX_T_ROLDREJTATRUPI_KOMPONENTE', 'IX_T_KONTROLLE_KOMPONENTE_KOD',
               'IX_T_SKEMAKONTABILITETIARTIKULLI_KOD', 'IX_T_KPF_NDERMARJE_STATUS', 'IX_T_KOKASHITJE_NDERM_NRDOK', 'IX_T_KOKAMAGAZINA_NDERM_NRDOK');
