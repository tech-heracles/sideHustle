/*
  Rollback of 07-heqja-crm-gis-dashboard-harta.sql: objects go back from schema [hequr] to [dbo] and every deleted row
  is inserted again (parents before children). Works only before 07-pastrimi-perfundimtar.sql (or the 05/06 final
  cleanup, which empties the whole [hequr] schema) has been run.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 07-rollback-heqja-crm-gis-dashboard-harta.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_07') IS NULL
BEGIN
    RAISERROR('Nuk ka asgje per te kthyer: hequr.T_RENDI_07 nuk ekziston (07 nuk eshte ekzekutuar ose eshte pastruar).', 16, 1);
    RETURN;
END

BEGIN TRAN;

/* objektet */
DECLARE @emri sysname, @x nvarchar(max);
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_07;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @x = N'ALTER SCHEMA dbo TRANSFER hequr.' + QUOTENAME(@emri);
    EXEC (@x);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

/* rreshtat: ne rendin e kundert te fshirjes */
DECLARE @tabela sysname, @kol nvarchar(max), @sql nvarchar(max), @ident bit;
DECLARE r CURSOR LOCAL FAST_FORWARD FOR SELECT tabela FROM hequr.T_RENDI_07 ORDER BY nr DESC;
OPEN r; FETCH NEXT FROM r INTO @tabela;
WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @kol = STRING_AGG(QUOTENAME(c.name), ',') WITHIN GROUP (ORDER BY c.column_id)
    FROM sys.columns c
    WHERE c.object_id = OBJECT_ID('dbo.' + @tabela) AND c.is_computed = 0 AND TYPE_NAME(c.system_type_id) NOT IN ('timestamp', 'rowversion')
      AND COL_LENGTH('hequr.' + @tabela + '_07', c.name) IS NOT NULL;
    SET @ident = OBJECTPROPERTY(OBJECT_ID('dbo.' + @tabela), 'TableHasIdentity');
    SET @sql = CASE WHEN @ident = 1 THEN N'SET IDENTITY_INSERT dbo.' + QUOTENAME(@tabela) + N' ON;' ELSE N'' END
             + N'INSERT dbo.' + QUOTENAME(@tabela) + N' (' + @kol + N') SELECT ' + @kol + N' FROM hequr.' + QUOTENAME(@tabela + '_07') + N';'
             + CASE WHEN @ident = 1 THEN N'SET IDENTITY_INSERT dbo.' + QUOTENAME(@tabela) + N' OFF;' ELSE N'' END
             + N'DROP TABLE hequr.' + QUOTENAME(@tabela + '_07') + N';';
    EXEC (@sql);
    FETCH NEXT FROM r INTO @tabela;
END
CLOSE r; DEALLOCATE r;

DROP TABLE hequr.T_RENDI_07;
DROP TABLE hequr.T_OBJEKTE_07;
DROP TABLE hequr.T_MBETEN_07;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');

COMMIT;
