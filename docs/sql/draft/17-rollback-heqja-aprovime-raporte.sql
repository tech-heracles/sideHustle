/*
  Rollback of 17-heqja-aprovime-raporte.sql: every deleted row goes back (parents before children).
  Works only before 17-pastrimi-perfundimtar.sql.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 17-rollback-heqja-aprovime-raporte.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_17') IS NULL
BEGIN
    RAISERROR('Nuk ka asgje per te kthyer: hequr.T_RENDI_17 nuk ekziston.', 16, 1);
    RETURN;
END

BEGIN TRAN;

DECLARE @tabela sysname, @kopja sysname, @kol nvarchar(max), @sql nvarchar(max), @ident bit;
DECLARE r CURSOR LOCAL FAST_FORWARD FOR SELECT tabela, kopja FROM hequr.T_RENDI_17 ORDER BY nr DESC;
OPEN r; FETCH NEXT FROM r INTO @tabela, @kopja;
WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @kol = STRING_AGG(CAST(QUOTENAME(c.name) AS nvarchar(max)), ',') WITHIN GROUP (ORDER BY c.column_id)
    FROM sys.columns c
    WHERE c.object_id = OBJECT_ID('dbo.' + @tabela) AND c.is_computed = 0 AND TYPE_NAME(c.system_type_id) NOT IN ('timestamp', 'rowversion')
      AND COL_LENGTH('hequr.' + @kopja, c.name) IS NOT NULL;
    SET @ident = OBJECTPROPERTY(OBJECT_ID('dbo.' + @tabela), 'TableHasIdentity');
    SET @sql = CASE WHEN @ident = 1 THEN N'SET IDENTITY_INSERT dbo.' + QUOTENAME(@tabela) + N' ON;' ELSE N'' END
             + N'INSERT dbo.' + QUOTENAME(@tabela) + N' (' + @kol + N') SELECT ' + @kol + N' FROM hequr.' + QUOTENAME(@kopja) + N';'
             + CASE WHEN @ident = 1 THEN N'SET IDENTITY_INSERT dbo.' + QUOTENAME(@tabela) + N' OFF;' ELSE N'' END
             + N'DROP TABLE hequr.' + QUOTENAME(@kopja) + N';';
    EXEC (@sql);
    FETCH NEXT FROM r INTO @tabela, @kopja;
END
CLOSE r; DEALLOCATE r;

DROP TABLE hequr.T_RENDI_17;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');

COMMIT;
