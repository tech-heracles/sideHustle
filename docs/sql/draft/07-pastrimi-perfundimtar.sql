/*
  Final cleanup after 07-heqja-crm-gis-dashboard-harta.sql, once the program has been checked: drops what script 07 put in
  schema [hequr] (moved objects and the saved rows) and the CRM report module (T_MODULI 24) if nothing uses it any more.
  Leaves alone anything that scripts 05/06 put there.
  After this, 07-rollback-heqja-crm-gis-dashboard-harta.sql no longer works: only a database backup brings them back.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 07-pastrimi-perfundimtar.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_07') IS NULL
BEGIN
    RAISERROR('hequr.T_RENDI_07 nuk ekziston: asgje per te pastruar nga skripti 07.', 16, 1);
    RETURN;
END

BEGIN TRAN;

CREATE TABLE #obj (id int PRIMARY KEY, lloji varchar(2) NOT NULL);
INSERT #obj SELECT o.object_id, RTRIM(o.type) FROM hequr.T_OBJEKTE_07 x JOIN sys.objects o ON o.name = x.emri AND o.schema_id = SCHEMA_ID('hequr');
INSERT #obj SELECT o.object_id, 'U' FROM hequr.T_RENDI_07 x JOIN sys.objects o ON o.name = x.tabela + '_07' AND o.schema_id = SCHEMA_ID('hequr');

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

DROP TABLE hequr.T_RENDI_07;
DROP TABLE hequr.T_OBJEKTE_07;
DROP TABLE hequr.T_MBETEN_07;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');

/* moduli i raporteve CRM: hiqet vetem kur nuk i referohet me asgje */
IF EXISTS (SELECT 1 FROM T_MODULI WHERE IDMODULI = 24 AND MODULIKODI = 'M_CRM')
BEGIN
    SET @sql = N'';
    SELECT @sql = @sql + N'IF EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
         + N' WHERE ' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = 24) SET @ref = 1;' + CHAR(10)
    FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
    WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_MODULI');
    DECLARE @ref bit = 0;
    EXEC sp_executesql @sql, N'@ref bit OUTPUT', @ref OUTPUT;
    IF @ref = 0
        DELETE FROM T_MODULI WHERE IDMODULI = 24;
    ELSE
        PRINT 'T_MODULI 24 mbetet: ka ende rreshta qe i referohen.';
END

COMMIT;
