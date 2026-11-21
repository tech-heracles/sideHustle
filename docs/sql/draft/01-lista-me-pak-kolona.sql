/*
  DRAFT: list screens show at most 6 columns (user's request, 2026-11-22).

  Scope: column layouts of list and lookup grids (T_GRIDAKOKA names 'ASPxGridView%' and 'gvLupa%'), in every
  configuration (model). Document lines ('%trupi%'), reports (gvRaporti%) and price grids are not touched.
  1. Technical columns are hidden: Klasa, Autorizimet, the English description, the accounting scheme id,
     the codification descriptions.
  2. Of what is still visible, the first 6 by position stay; the rest are hidden.
  Hidden columns stay available under "Zgjidh kolonat".

  Safe to run again. Undo: 01-rollback-lista-me-pak-kolona.sql (restores from AVEC_BAK_01_GRIDATRUPI).
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRAN;

IF OBJECT_ID('dbo.AVEC_BAK_01_GRIDATRUPI') IS NULL
    CREATE TABLE dbo.AVEC_BAK_01_GRIDATRUPI (GRIDATRUPIID int PRIMARY KEY, GRIDATRUPIVISIBLE bit NULL);

;WITH grids AS (
    SELECT k.GRIDKOKAID
    FROM T_GRIDAKOKA k
    WHERE (k.GRIDKOKAEMRI LIKE 'ASPxGridView%' OR k.GRIDKOKAEMRI LIKE 'gvLupa%')
      AND k.GRIDKOKAEMRI NOT LIKE '%trupi%'
      AND k.GRIDKOKAEMRI NOT LIKE '%Cmim%'
      AND k.GRIDKOKAEMRI NOT LIKE '%Raport%'
),
visible AS (
    SELECT t.GRIDATRUPIID, t.GRIDAKOKAID, t.GRIDATRUPIKODI, t.GRIDATRUPIINDEX,
           CASE WHEN t.GRIDATRUPIKODI IN ('Klasa', 'Autorizimet', 'PershkrimiAngArtikulli', 'IdSkemaKontabilitetiArtikulli')
                  OR t.GRIDATRUPIKODI LIKE 'PershkrimKodifikimi%'
                THEN 1 ELSE 0 END AS teknike
    FROM T_GRIDATRUPI t
    JOIN grids g ON g.GRIDKOKAID = t.GRIDAKOKAID
    WHERE t.GRIDATRUPIVISIBLE = 1
),
ranked AS (
    SELECT v.*, ROW_NUMBER() OVER (PARTITION BY v.GRIDAKOKAID, v.teknike ORDER BY v.GRIDATRUPIINDEX, v.GRIDATRUPIID) AS rn
    FROM visible v
),
hide AS (
    SELECT GRIDATRUPIID FROM ranked WHERE teknike = 1 OR rn > 6
)
INSERT INTO dbo.AVEC_BAK_01_GRIDATRUPI (GRIDATRUPIID, GRIDATRUPIVISIBLE)
SELECT t.GRIDATRUPIID, t.GRIDATRUPIVISIBLE
FROM T_GRIDATRUPI t JOIN hide h ON h.GRIDATRUPIID = t.GRIDATRUPIID
WHERE NOT EXISTS (SELECT 1 FROM dbo.AVEC_BAK_01_GRIDATRUPI b WHERE b.GRIDATRUPIID = t.GRIDATRUPIID);

UPDATE t SET t.GRIDATRUPIVISIBLE = 0
FROM T_GRIDATRUPI t JOIN dbo.AVEC_BAK_01_GRIDATRUPI b ON b.GRIDATRUPIID = t.GRIDATRUPIID
WHERE t.GRIDATRUPIVISIBLE = 1;

SELECT @@ROWCOUNT AS kolona_te_fshehura;
COMMIT;
