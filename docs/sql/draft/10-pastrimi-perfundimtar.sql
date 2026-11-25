/*
  Final cleanup after 10-heqja-dast-epayslip-polica.sql, once the program has been checked: drops what script 10 put in schema [hequr]
  (moved objects and the saved rows).
  Leaves alone anything that scripts 05-09 put there.
  After this, 10-rollback-heqja-dast-epayslip-polica.sql no longer works: only a database backup brings them back.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 10-pastrimi-perfundimtar.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_10') IS NULL
BEGIN
    RAISERROR('hequr.T_RENDI_10 nuk ekziston: asgje per te pastruar nga skripti 10.', 16, 1);
    RETURN;
END

BEGIN TRAN;

CREATE TABLE #obj (id int PRIMARY KEY, lloji varchar(2) NOT NULL);
INSERT #obj SELECT o.object_id, RTRIM(o.type) FROM hequr.T_OBJEKTE_10 x JOIN sys.objects o ON o.name = x.emri AND o.schema_id = SCHEMA_ID('hequr');
INSERT #obj SELECT o.object_id, 'U' FROM hequr.T_RENDI_10 x JOIN sys.objects o ON o.name = x.tabela + '_10' AND o.schema_id = SCHEMA_ID('hequr');

DECLARE @sql nvarchar(max) = N'';

/* celesat e huaj qe dalin nga tabelat qe fshihen ose qe hyjne ne to */
SELECT @sql = @sql + N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
     + N' DROP CONSTRAINT ' + QUOTENAME(fk.name) + N';' + CHAR(10)
FROM sys.foreign_keys fk
WHERE fk.parent_object_id IN (SELECT id FROM #obj) OR fk.referenced_object_id IN (SELECT id FROM #obj);
EXEC (@sql);

/* procedurat, view-t, funksionet, tabelat */
SET @sql = N'';
SELECT @sql = @sql + N'DROP ' + CASE o.type WHEN 'P' THEN N'PROCEDURE ' WHEN 'V' THEN N'VIEW ' WHEN 'U' THEN N'TABLE ' ELSE N'FUNCTION ' END
     + N'hequr.' + QUOTENAME(o.name) + N';' + CHAR(10)
FROM sys.objects o
WHERE o.object_id IN (SELECT id FROM #obj)
ORDER BY CASE o.type WHEN 'U' THEN 2 ELSE 1 END;
EXEC (@sql);

DROP TABLE hequr.T_RENDI_10;
DROP TABLE hequr.T_OBJEKTE_10;
DROP TABLE hequr.T_MBETEN_10;
DROP TABLE hequr.T_DEFINICIONE_10;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');

COMMIT;
