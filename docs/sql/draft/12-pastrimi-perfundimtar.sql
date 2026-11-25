/*
  Pastrimi perfundimtar pas 12-mbetjet-crm-mobile-licenca.sql, kur programi te jete provuar: fshin ate qe 12 vuri ne [hequr]
  (rreshtat e ruajtur, SP_ModifikoLicencen, versionin e vjeter te SP_FshiVeprime_SipasLicences) dhe tabelat e kontrollit.
  Nuk prek asgje qe skriptet e tjera kane vene ne [hequr].
  Pas kesaj, 12-rollback-mbetjet-crm-mobile-licenca.sql nuk punon me: vetem backup-i i databazes i kthen.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 12-pastrimi-perfundimtar.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('hequr.T_RENDI_12') IS NULL
BEGIN
    RAISERROR('hequr.T_RENDI_12 nuk ekziston: asgje per te pastruar nga skripti 12.', 16, 1);
    RETURN;
END

BEGIN TRAN;

DECLARE @sql nvarchar(max) = N'';
SELECT @sql = @sql + N'DROP ' + CASE o.type WHEN 'P' THEN N'PROCEDURE ' WHEN 'V' THEN N'VIEW ' WHEN 'U' THEN N'TABLE ' ELSE N'FUNCTION ' END
     + N'hequr.' + QUOTENAME(o.name) + N';' + CHAR(10)
FROM (SELECT emri FROM hequr.T_OBJEKTE_12
      UNION SELECT emri FROM hequr.T_DEFINICIONE_12            -- versionet e vjetra
      UNION SELECT tabela + '_12' FROM hequr.T_RENDI_12) x     -- rreshtat e ruajtur
JOIN sys.objects o ON o.name = x.emri AND o.schema_id = SCHEMA_ID('hequr');
EXEC (@sql);

DROP TABLE hequr.T_RENDI_12;
DROP TABLE hequr.T_OBJEKTE_12;
DROP TABLE hequr.T_MBETEN_12;
DROP TABLE hequr.T_DEFINICIONE_12;
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE schema_id = SCHEMA_ID('hequr'))
    EXEC ('DROP SCHEMA hequr');

COMMIT;
