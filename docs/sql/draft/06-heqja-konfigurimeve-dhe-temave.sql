/*
  DRAFT 06 (2026-11-23): heqja e mbetjeve te tjera te pjeseve te hequra:
    - konfigurimet e dokumenteve Vodafone/Bazaar (VFONE, USHDD, POROSIDD, BAZAAR, POROSIBAZAAR, FBD, FHDealer),
      "Promocioni Plus" (AE) dhe "Trasfero ne ISKSH" (TFISKSH), bashke me kategorite dhe nivelet e tyre,
      faqet Shto_AlphawebEnhancments.aspx, TransferoNeISKSH.aspx dhe LupaSkemaKontabel.aspx (nuk ekzistojne);
    - temat per perdorues (T_THEME, T_THEMESAMBJENTE, T_THEMESDEVEXPRESSJQUERY, T_THEMEUSER dhe procedurat e tyre).
  Si 05: rreshtat dhe objektet kalojne ne skemen [hequr]; rollback: 06-rollback-...; fshirja: 06-pastrimi-perfundimtar.sql.
  Nje konfigurim, nivel apo kategori qe e perdor ndonje dokument nuk hiqet.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 06-heqja-konfigurimeve-dhe-temave.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_06') IS NOT NULL
BEGIN
    RAISERROR('Skripti 06 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_06 ekziston).', 16, 1);
    RETURN;
END

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_06 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);
CREATE TABLE hequr.T_OBJEKTE_06 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_DEFINICIONE_06 (emri sysname PRIMARY KEY, definicioni nvarchar(max) NOT NULL);

DECLARE @s nvarchar(max);

/* ---------- 1. cfare hiqet ---------- */
CREATE TABLE #konf (id numeric(18,0) PRIMARY KEY);
INSERT #konf SELECT IDKONFIGAMBJENTE FROM T_KONFIGAMBJENTE
WHERE KODKONFIGAMBJENTE IN ('VFONE', 'USHDD', 'POROSIDD', 'BAZAAR', 'POROSIBAZAAR', 'FBD', 'FHDealer')
   OR IDKATDOK IN (SELECT IDKATDOK FROM T_KATEGORINIVELDOK WHERE IDKOMPONENTE IN
        (SELECT IDKOMPON FROM T_KOMPONENTE WHERE KOMPONEMRI IN ('Shto_AlphawebEnhancments.aspx', 'TransferoNeISKSH.aspx')));
-- konfigurimi qe e perdor ndonje tabele tjeter (dokumente etj.) mbetet
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #konf k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.'
     + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KONFIGAMBJENTE')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_GRIDAKOKA', 'T_KUSHTEMPLATE', 'T_ATRIBUTETRUPI', 'T_LUPAMULTIPLE', 'T_KONFIGAMBJENTE');
EXEC (@s);

CREATE TABLE #kat (id numeric(18,0) PRIMARY KEY);          -- kategorite e faqeve qe nuk ekzistojne
INSERT #kat SELECT IDKATDOK FROM T_KATEGORINIVELDOK WHERE IDKOMPONENTE IN
    (SELECT IDKOMPON FROM T_KOMPONENTE WHERE KOMPONEMRI IN ('Shto_AlphawebEnhancments.aspx', 'TransferoNeISKSH.aspx'));
CREATE TABLE #niv (id numeric(18,0) PRIMARY KEY);
INSERT #niv SELECT IDNIVEL FROM T_NIVELREGJISTRIMI WHERE IDKATDOK IN (SELECT id FROM #kat);
-- nje nivel qe e perdor ndonje dokument (apo konfigurim qe mbetet) mbetet
SET @s = N'';
SELECT @s = @s + N'DELETE n FROM #niv n WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.'
     + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = n.id'
     + CASE WHEN OBJECT_NAME(fk.parent_object_id) = 'T_KONFIGAMBJENTE' THEN N' AND x.IDKONFIGAMBJENTE NOT IN (SELECT id FROM #konf)' ELSE N'' END + N');' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_NIVELREGJISTRIMI') AND OBJECT_NAME(fk.parent_object_id) <> 'T_ROLDREJTATRUPI';
EXEC (@s);
-- nje kategori mbetet nese i ka mbetur ndonje nivel, konfigurim apo rresht tjeter
DELETE k FROM #kat k WHERE EXISTS (SELECT 1 FROM T_NIVELREGJISTRIMI n WHERE n.IDKATDOK = k.id AND n.IDNIVEL NOT IN (SELECT id FROM #niv))
                        OR EXISTS (SELECT 1 FROM T_KONFIGAMBJENTE a WHERE a.IDKATDOK = k.id AND a.IDKONFIGAMBJENTE NOT IN (SELECT id FROM #konf));
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #kat k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.'
     + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KATEGORINIVELDOK')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_NIVELREGJISTRIMI', 'T_KONFIGAMBJENTE', 'T_ROLDREJTATRUPI');
EXEC (@s);

CREATE TABLE #komp (id numeric(18,0) PRIMARY KEY);
INSERT #komp SELECT IDKOMPON FROM T_KOMPONENTE WHERE KOMPONEMRI = 'LupaSkemaKontabel.aspx'
   OR (KOMPONEMRI IN ('Shto_AlphawebEnhancments.aspx', 'TransferoNeISKSH.aspx')
       AND NOT EXISTS (SELECT 1 FROM T_KATEGORINIVELDOK c WHERE c.IDKOMPONENTE = IDKOMPON AND c.IDKATDOK NOT IN (SELECT id FROM #kat)));

/* ---------- 2. rreshtat: ruhen ne hequr.<TABELA>_06, pastaj fshihen (femijet para prinderve) ---------- */
CREATE TABLE #hap (nr int IDENTITY PRIMARY KEY, tabela sysname, kushti nvarchar(max));
INSERT #hap (tabela, kushti) VALUES
 ('T_ROLDREJTATRUPI',          'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDNIVELREGJISTRIMI IN (SELECT id FROM #niv) OR IDKATEGORIA IN (SELECT id FROM #kat)'),
 ('T_ROLDREJTATRUPI_HISTORIK', 'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDNIVELREGJISTRIMI IN (SELECT id FROM #niv) OR IDKATEGORIA IN (SELECT id FROM #kat)'),
 ('T_MENUPERKOMPONENTE',       'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_KOMPONENTEPERLLOJLICENCE','IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_LUPAMULTIPLE',            'IDKONFIGAMBJENTE IN (SELECT id FROM #konf) OR IDKONFIGAMBJENTELUPA IN (SELECT id FROM #konf) OR GRIDATRUPIID IN (SELECT GRIDATRUPIID FROM T_GRIDATRUPI WHERE GRIDAKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf)))'),
 ('T_GRIDATRUPI',              'GRIDAKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf))'),
 ('T_FILTRAGRIDA',             'GRIDKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf))'),
 ('T_GRIDAKOKA',               'GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf)'),
 ('T_KONFLLOJRRESHTI',         'IDKUSHTEMPLATE IN (SELECT IDKUSHTEMPLATE FROM T_KUSHTEMPLATE WHERE IDKONFIGAMBJENTE IN (SELECT id FROM #konf))'),
 ('T_KUSHTEMPLATE',            'IDKONFIGAMBJENTE IN (SELECT id FROM #konf)'),
 ('T_ATRIBUTETRUPI',           'IDKONFIGAMBJENTE IN (SELECT id FROM #konf)'),
 ('T_KONFIGAMBJENTE',          'IDKONFIGAMBJENTE IN (SELECT id FROM #konf)'),
 ('T_NIVELREGJISTRIMI',        'IDNIVEL IN (SELECT id FROM #niv)'),
 ('T_KATEGORINIVELDOK',        'IDKATDOK IN (SELECT id FROM #kat)'),
 ('T_KOMPONENTE',              'IDKOMPON IN (SELECT id FROM #komp)');

DECLARE @nr int = 1, @tabela sysname, @kushti nvarchar(max), @n int, @sql nvarchar(max);
WHILE EXISTS (SELECT 1 FROM #hap WHERE nr = @nr)
BEGIN
    SELECT @tabela = tabela, @kushti = kushti FROM #hap WHERE nr = @nr;
    SET @nr += 1;
    IF OBJECT_ID('dbo.' + @tabela, 'U') IS NULL CONTINUE;
    -- nese ne nje databaze tjeter tabela nuk ka kolonen e pritur, gabimi e kthen mbrapsht te gjithe skriptin
    SET @sql = N'SELECT @n = COUNT(*) FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti;
    EXEC sp_executesql @sql, N'@n int OUTPUT', @n OUTPUT;
    IF @n = 0 CONTINUE;
    SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@tabela + '_06') + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
             + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
    EXEC (@sql);
    INSERT hequr.T_RENDI_06 (tabela, rreshta) VALUES (@tabela, @n);
END

/* ---------- 3. procedura e pastrimit sipas licences nuk e prek me T_THEMEUSER ---------- */
DECLARE @def nvarchar(max) = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_FshiVeprime_SipasLicences'));
DECLARE @a int = CHARINDEX('T_THEMEUSER---', @def), @b int = CHARINDEX('TABELA THEMEUSER U FSHI'';', @def);
IF @def IS NOT NULL AND @a > 0 AND @b > @a
BEGIN
    INSERT hequr.T_DEFINICIONE_06 VALUES ('SP_FshiVeprime_SipasLicences', @def);
    SET @a = @a - CHARINDEX(CHAR(10), REVERSE(LEFT(@def, @a))) + 1;
    SET @def = STUFF(@def, @a, @b + LEN('TABELA THEMEUSER U FSHI'';') - @a, '-- T_THEMEUSER u hoq bashke me temat per perdorues (DRAFT 06)');
    SET @def = STUFF(@def, PATINDEX('%CREATE%PROC%', @def), LEN('CREATE'), 'ALTER');
    EXEC (@def);
END

/* ---------- 4. objektet e temave kalojne ne skemen [hequr] ---------- */
INSERT hequr.T_OBJEKTE_06 (emri, lloji)
SELECT o.name, o.type FROM sys.objects o
WHERE o.schema_id = SCHEMA_ID('dbo') AND o.type IN ('U', 'P')
  AND (o.name IN ('T_THEMEUSER', 'T_THEMESAMBJENTE', 'T_THEMESDEVEXPRESSJQUERY', 'T_THEME')
       OR o.name LIKE 'prc[_]T[_]THEME[_]%' OR o.name LIKE 'prc[_]T[_]THEMESAMBJENTE[_]%'
       OR o.name LIKE 'prc[_]T[_]THEMESDEVEXPRESSJQUERY[_]%' OR o.name LIKE 'prc[_]T[_]THEMEUSER[_]%');

DECLARE @emri sysname;
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_06;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

COMMIT;

SELECT tabela, rreshta FROM hequr.T_RENDI_06 ORDER BY nr;
SELECT lloji, COUNT(*) objekte FROM hequr.T_OBJEKTE_06 GROUP BY lloji;
