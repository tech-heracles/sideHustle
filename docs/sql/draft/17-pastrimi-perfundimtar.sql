/*
  Final cleanup after 17-heqja-aprovime-raporte.sql, once the program has been checked: drops the rows saved in schema [hequr].
  After this only a database backup brings them back.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 17-pastrimi-perfundimtar.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_17') IS NULL
BEGIN
    RAISERROR('hequr.T_RENDI_17 nuk ekziston: asgje per te pastruar nga skripti 17.', 16, 1);
    RETURN;
END

BEGIN TRAN;
DECLARE @sql nvarchar(max) = N'';
SELECT @sql = @sql + N'DROP TABLE hequr.' + QUOTENAME(kopja) + N';' + CHAR(10) FROM hequr.T_RENDI_17;
EXEC (@sql);
DROP TABLE hequr.T_RENDI_17;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');
COMMIT;
