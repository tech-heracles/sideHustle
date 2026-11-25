/*
  Pastrimi perfundimtar pas 11-objekte-te-pavlefshme.sql, kur programi te jete provuar: fshin objektet qe 11 kaloi ne [hequr]
  dhe tabelat e kontrollit (perfshi tekstet e vjetra te 18 procedurave te rregulluara, qe mbeten te rregulluara).
  Nuk prek asgje qe skriptet e tjera kane vene ne [hequr].
  Pas kesaj, 11-rollback-objekte-te-pavlefshme.sql nuk punon me: vetem backup-i i databazes i kthen.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 11-pastrimi-perfundimtar.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_11') IS NULL
BEGIN
    RAISERROR('hequr.T_RENDI_11 nuk ekziston: asgje per te pastruar nga skripti 11.', 16, 1);
    RETURN;
END

BEGIN TRAN;

DECLARE @sql nvarchar(max) = N'';
SELECT @sql = @sql + N'DROP ' + CASE o.type WHEN 'P' THEN N'PROCEDURE ' WHEN 'V' THEN N'VIEW ' ELSE N'FUNCTION ' END
     + N'hequr.' + QUOTENAME(o.name) + N';' + CHAR(10)
FROM (SELECT emri FROM hequr.T_OBJEKTE_11 UNION SELECT emri FROM hequr.T_DEFINICIONE_11) x   -- + versionet e vjetra te 18 procedurave
JOIN sys.objects o ON o.name = x.emri AND o.schema_id = SCHEMA_ID('hequr');
EXEC (@sql);

DROP TABLE hequr.T_RENDI_11;
DROP TABLE hequr.T_OBJEKTE_11;
DROP TABLE hequr.T_MBETEN_11;
DROP TABLE hequr.T_DEFINICIONE_11;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');

COMMIT;
