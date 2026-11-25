/*
  Kthimi mbrapsht i 11-objekte-te-pavlefshme.sql: objektet kthehen nga skema [hequr] ne [dbo] dhe 18 procedurat e rregulluara
  marrin tekstin e vjeter (hequr.T_DEFINICIONE_11). Punon vetem para 11-pastrimi-perfundimtar.sql.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 11-rollback-objekte-te-pavlefshme.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_11') IS NULL
BEGIN
    RAISERROR('Nuk ka asgje per te kthyer: hequr.T_RENDI_11 nuk ekziston (11 nuk eshte ekzekutuar ose eshte pastruar).', 16, 1);
    RETURN;
END

BEGIN TRAN;

/* objektet */
DECLARE @emri sysname, @x nvarchar(max);
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_11;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @x = N'ALTER SCHEMA dbo TRANSFER hequr.' + QUOTENAME(@emri);
    EXEC (@x);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

/* procedurat e rregulluara: fshihet versioni i ri dhe kthehet i vjetri nga [hequr].
   Teksti i vjeter nuk mund te ekzekutohet si CREATE (kolonat qe perdor nuk ekzistojne), prandaj nese procedura e vjeter
   nuk eshte ne [hequr] (DRAFT 2026-11-25: 11 u ekzekutua fillimisht me ALTER), versioni i rregulluar mbetet dhe raportohet;
   ai kthehet vetem me backup-in DRAFT_para_11_2026-11-25.bak. */
DECLARE @mbeten TABLE (emri sysname);
DECLARE d CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_DEFINICIONE_11;
OPEN d; FETCH NEXT FROM d INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF OBJECT_ID('hequr.' + QUOTENAME(@emri), 'P') IS NOT NULL
    BEGIN
        SET @x = N'DROP PROCEDURE dbo.' + QUOTENAME(@emri) + N'; ALTER SCHEMA dbo TRANSFER hequr.' + QUOTENAME(@emri) + N';';
        EXEC (@x);
    END
    ELSE INSERT @mbeten VALUES (@emri);
    FETCH NEXT FROM d INTO @emri;
END
CLOSE d; DEALLOCATE d;
SELECT emri AS [mbetet i rregulluar (vetem backup-i e kthen)] FROM @mbeten;

DROP TABLE hequr.T_RENDI_11;
DROP TABLE hequr.T_OBJEKTE_11;
DROP TABLE hequr.T_MBETEN_11;
DROP TABLE hequr.T_DEFINICIONE_11;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');

COMMIT;
