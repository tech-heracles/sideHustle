/*
  Final cleanup after 05-heqja-bi-dhe-mbetjeve.sql, once the program has been checked: drops everything kept in schema
  [hequr] (moved objects and the saved rows) and the Business Intelligence module (T_MODULI 19).
  After this, 05-rollback-heqja-bi-dhe-mbetjeve.sql no longer works: only a database backup brings them back.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 05-pastrimi-perfundimtar.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL
BEGIN
    RAISERROR('Skema [hequr] nuk ekziston: asgje per te pastruar.', 16, 1);
    RETURN;
END

BEGIN TRAN;

DECLARE @sql nvarchar(max) = N'';

/* celesat e huaj qe dalin nga tabelat ne [hequr] ose qe hyjne ne to */
SELECT @sql = @sql + N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
     + N' DROP CONSTRAINT ' + QUOTENAME(fk.name) + N';' + CHAR(10)
FROM sys.foreign_keys fk
WHERE OBJECT_SCHEMA_NAME(fk.parent_object_id) = 'hequr' OR OBJECT_SCHEMA_NAME(fk.referenced_object_id) = 'hequr';
EXEC (@sql);

/* procedurat, view-t, tabelat */
SET @sql = N'';
SELECT @sql = @sql + N'DROP ' + CASE o.type WHEN 'P' THEN N'PROCEDURE ' WHEN 'V' THEN N'VIEW ' WHEN 'U' THEN N'TABLE '
                                         WHEN 'FN' THEN N'FUNCTION ' WHEN 'IF' THEN N'FUNCTION ' WHEN 'TF' THEN N'FUNCTION ' END
     + N'hequr.' + QUOTENAME(o.name) + N';' + CHAR(10)
FROM sys.objects o
WHERE o.schema_id = SCHEMA_ID('hequr') AND o.type IN ('P', 'V', 'FN', 'IF', 'TF', 'U')
ORDER BY CASE o.type WHEN 'U' THEN 2 ELSE 1 END;
EXEC (@sql);

EXEC ('DROP SCHEMA hequr');

/* moduli Business Intelligence: nuk e perdor me asnje raport, komponente apo tabele */
IF EXISTS (SELECT 1 FROM T_MODULI WHERE IDMODULI = 19 AND MODULIKODI = 'M_BUSINESS_INTELL')
BEGIN
    SET @sql = N'';
    SELECT @sql = @sql + N'IF EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
         + N' WHERE ' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = 19) SET @ref = 1;' + CHAR(10)
    FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
    WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_MODULI');
    DECLARE @ref bit = 0;
    EXEC sp_executesql @sql, N'@ref bit OUTPUT', @ref OUTPUT;
    IF @ref = 0
        DELETE FROM T_MODULI WHERE IDMODULI = 19;
    ELSE
        PRINT 'T_MODULI 19 mbetet: ka ende rreshta qe i referohen.';
END

COMMIT;
