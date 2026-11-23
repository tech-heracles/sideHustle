/*
  Final cleanup after 06-heqja-konfigurimeve-dhe-temave.sql, once the program has been checked: drops everything kept in schema
  [hequr] (moved objects and the saved rows).
  After this, 06-rollback-heqja-konfigurimeve-dhe-temave.sql no longer works: only a database backup brings them back.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 06-pastrimi-perfundimtar.sql
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

COMMIT;
