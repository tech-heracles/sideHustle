/*
  DRAFT 07 (2026-11-25): heqja nga databaza e pjeseve qe u hoqen nga programi:
    - CRM (anketat, detyrat, takimet, raportet e modulit CRM),
    - GIS (hapesirat e punes, layer-at, objektet, ngarkimet),
    - Dashboard ("Treguesit kryesore", T_DASHBOARD*),
    - Harta (GoogleHarte, LupaAseteJoNeHarte, LupaLayerElementi),
    - Dergo Mesazh, Historik email dhe Konfigurim email (vetem faqet; tabelat e email-it mbeten).

  Mbeten (i perdorin pjese te tjera te programit):
    - T_GIS_App_FILTRAGRIDA dhe procedurat e saj (Kerkim me atribute te klientet),
    - T_GIS_App_LLOJLAYERMAGAZINE dhe prc_..._merrGjitheLlojLayerMagazine (fusha "Lloj layeri" e magazinave),
    - prc_GIS_Combo_* (filtrat e raporteve), tabelat e email-it (dergimi i email-eve per aprovime etj.),
    - cdo objekt qe e perdor ende nje procedure, view apo tabele qe nuk hiqet (kontrollohet me poshte).

  Si 05/06: asgje nuk fshihet perfundimisht. Rreshtat ruhen ne hequr.<TABELA>_07, objektet kalojne ne skemen [hequr].
    - kthimi mbrapsht:        07-rollback-heqja-crm-gis-dashboard-harta.sql
    - fshirja perfundimtare:  07-pastrimi-perfundimtar.sql (pasi te jete provuar programi)
  Nese dicka nuk shkon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 07-heqja-crm-gis-dashboard-harta.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_07') IS NOT NULL
BEGIN
    RAISERROR('Skripti 07 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_07 ekziston).', 16, 1);
    RETURN;
END

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_07 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);
CREATE TABLE hequr.T_OBJEKTE_07 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_MBETEN_07 (emri sysname PRIMARY KEY, arsye nvarchar(400) NOT NULL);   -- vetem per raport

DECLARE @s nvarchar(max);

/* ---------- 1. komponentet (faqet dhe menute) ---------- */
CREATE TABLE #komp (id numeric(18,0) PRIMARY KEY);
INSERT #komp SELECT IDKOMPON FROM T_KOMPONENTE
WHERE KOMPONEMRI LIKE 'CRM%.aspx%' OR KOMPONEMRI LIKE 'GIS%.aspx%' OR KOMPONEMRI LIKE 'GIS%.ashx%'
   OR KOMPONEMRI LIKE 'Dashboard.aspx%' OR KOMPONEMRI LIKE 'Dashboard[_]%' OR KOMPONEMRI LIKE 'GoogleHarte.aspx%'
   OR KOMPONEMRI IN ('GisDefault', 'RaportetAllNew.aspx')
   OR KOMPONEMRI LIKE 'LupaAseteJoNeHarte.aspx%' OR KOMPONEMRI LIKE 'LupaLayerElementi.aspx%'
   OR KOMPONEMRI LIKE 'HistorikuEmail.aspx%' OR KOMPONEMRI LIKE 'KonfigurimeEmail.aspx%' OR KOMPONEMRI LIKE 'MessageToAll.html%'
   OR KOMPONEMRI LIKE 'FooterPanelInfo.aspx%'
   OR KOMPONEMRI IN ('Raportet.aspx?idmod=24', 'konfig-gis', 'crm', 'gis', 'map', 'dashboard');

/* kategorite, nivelet dhe konfigurimet e ketyre faqeve (si ne 06); ato qe i perdor ndonje dokument mbeten */
CREATE TABLE #kat (id numeric(18,0) PRIMARY KEY);
INSERT #kat SELECT IDKATDOK FROM T_KATEGORINIVELDOK WHERE IDKOMPONENTE IN (SELECT id FROM #komp);
CREATE TABLE #konf (id numeric(18,0) PRIMARY KEY);
INSERT #konf SELECT IDKONFIGAMBJENTE FROM T_KONFIGAMBJENTE
WHERE IDKATDOK IN (SELECT id FROM #kat)
   OR KODKONFIGAMBJENTE IN ('GIS/LO', 'GIS/WS', 'Lp/Gis', 'LP/KerkoGIS', 'LP/KerkoGIS2');
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #konf k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.'
     + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KONFIGAMBJENTE')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_GRIDAKOKA', 'T_KUSHTEMPLATE', 'T_ATRIBUTETRUPI', 'T_LUPAMULTIPLE', 'T_KONFIGAMBJENTE');
EXEC (@s);
CREATE TABLE #niv (id numeric(18,0) PRIMARY KEY);
INSERT #niv SELECT IDNIVEL FROM T_NIVELREGJISTRIMI WHERE IDKATDOK IN (SELECT id FROM #kat);
SET @s = N'';
SELECT @s = @s + N'DELETE n FROM #niv n WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.'
     + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = n.id'
     + CASE WHEN OBJECT_NAME(fk.parent_object_id) = 'T_KONFIGAMBJENTE' THEN N' AND x.IDKONFIGAMBJENTE NOT IN (SELECT id FROM #konf)' ELSE N'' END + N');' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_NIVELREGJISTRIMI') AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_ROLDREJTATRUPI', 'T_ROLDREJTATRUPI_HISTORIK');
EXEC (@s);
DELETE k FROM #kat k WHERE EXISTS (SELECT 1 FROM T_NIVELREGJISTRIMI n WHERE n.IDKATDOK = k.id AND n.IDNIVEL NOT IN (SELECT id FROM #niv))
                        OR EXISTS (SELECT 1 FROM T_KONFIGAMBJENTE a WHERE a.IDKATDOK = k.id AND a.IDKONFIGAMBJENTE NOT IN (SELECT id FROM #konf));
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #kat k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.'
     + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KATEGORINIVELDOK')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_NIVELREGJISTRIMI', 'T_KONFIGAMBJENTE', 'T_ROLDREJTATRUPI', 'T_ROLDREJTATRUPI_HISTORIK');
EXEC (@s);
-- nje faqe qe i ka mbetur ndonje kategori dokumentesh nuk hiqet
DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM T_KATEGORINIVELDOK c WHERE c.IDKOMPONENTE = k.id AND c.IDKATDOK NOT IN (SELECT id FROM #kat));
-- nje komponente qe e perdor ndonje tabele tjeter (p.sh. si faqe fillestare e nje ambjenti) mbetet
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KOMPONENTE')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_ROLDREJTATRUPI', 'T_ROLDREJTATRUPI_HISTORIK', 'T_MENUPERKOMPONENTE',
        'T_KOMPONENTEPERLLOJLICENCE', 'T_GRIDAKOKA', 'T_KOMPONENTE', 'T_KATEGORINIVELDOK', 'T_DASHBOARD_KOMPONENTE_X_SP');
EXEC (@s);

/* ---------- 2. raportet e CRM (moduli 24) ---------- */
CREATE TABLE #rap (id numeric(18,0) PRIMARY KEY);
IF EXISTS (SELECT 1 FROM T_MODULI WHERE IDMODULI = 24 AND (MODULIKODI LIKE '%CRM%' OR MODULIEMRI LIKE '%CRM%'))
    INSERT #rap SELECT IDRAPORTI FROM T_RAPORTI WHERE IDMODULI = 24;

CREATE TABLE #design (id numeric(18,0) PRIMARY KEY);
INSERT #design SELECT IDRAPORTDESING FROM T_RAPORTDESING WHERE IDRAPORTI IN (SELECT id FROM #rap);
-- nje format qe e perdor ndonje dokument mbetet, bashke me raportin e tij
SET @s = N'';
SELECT @s = @s + N'DELETE d FROM #design d WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = d.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_RAPORTDESING') AND fk.parent_object_id <> OBJECT_ID('dbo.T_RAPXDESIGN');
EXEC (@s);
DELETE r FROM #rap r WHERE EXISTS (SELECT 1 FROM T_RAPORTDESING d WHERE d.IDRAPORTI = r.id AND d.IDRAPORTDESING NOT IN (SELECT id FROM #design));

/* ---------- 3. rreshtat: ruhen ne hequr.<TABELA>_07, pastaj fshihen (femijet para prinderve) ---------- */
CREATE TABLE #hap (nr int IDENTITY PRIMARY KEY, tabela sysname, kushti nvarchar(max));
INSERT #hap (tabela, kushti) VALUES
 ('T_ROLDREJTATRUPI',            'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDRAPORTI IN (SELECT id FROM #rap) OR IDNIVELREGJISTRIMI IN (SELECT id FROM #niv) OR IDKATEGORIA IN (SELECT id FROM #kat)'),
 ('T_ROLDREJTATRUPI_HISTORIK',   'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDRAPORTI IN (SELECT id FROM #rap) OR IDNIVELREGJISTRIMI IN (SELECT id FROM #niv) OR IDKATEGORIA IN (SELECT id FROM #kat)'),
 ('T_ROLDREJTARAPORTE',          'IDRAPORTI IN (SELECT id FROM #rap)'),
 ('T_RAPORTET_SITEMAP',          'IDRAP IN (SELECT id FROM #rap)'),
 ('T_RAPORTIPERPARAMETER',       'IDRAPORTI IN (SELECT id FROM #rap)'),
 ('T_RAPORT_FILTER_RELATIONSHIP','idraport IN (SELECT id FROM #rap)'),
 ('T_RAPORTXKATEGORI',           'IDRAPORT IN (SELECT id FROM #rap)'),
 ('T_LIDHJERAPORTI',             'IDSUPERRAPORT IN (SELECT id FROM #rap) OR IDSUBRAPORT IN (SELECT id FROM #rap)'),
 ('T_RAPFILTER',                 'IDRAP IN (SELECT id FROM #rap) OR IDFILTERKOKA IN (SELECT IDKOKAFILTER FROM T_FILTERKOKA WHERE IDRAP IN (SELECT id FROM #rap))'),
 ('T_FILTERTRUPI',               'IDKOKAFILTER IN (SELECT IDKOKAFILTER FROM T_FILTERKOKA WHERE IDRAP IN (SELECT id FROM #rap))'),
 ('T_FILTERKOKA',                'IDRAP IN (SELECT id FROM #rap)'),
 ('T_RAPXDESIGN',                'IDDESIGN IN (SELECT id FROM #design)'),
 ('T_RAPORTDESING',              'IDRAPORTDESING IN (SELECT id FROM #design)'),
 ('T_RAPORTI',                   'IDRAPORTI IN (SELECT id FROM #rap)'),
 ('T_MENUPERKOMPONENTE',         'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_KOMPONENTEPERLLOJLICENCE',  'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_DASHBOARD_KOMPONENTE_X_SP', 'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_LUPAMULTIPLE',              'IDKONFIGAMBJENTE IN (SELECT id FROM #konf) OR IDKONFIGAMBJENTELUPA IN (SELECT id FROM #konf) OR GRIDATRUPIID IN (SELECT GRIDATRUPIID FROM T_GRIDATRUPI WHERE GRIDAKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf)))'),
 ('T_GRIDATRUPI',                'GRIDAKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf))'),
 ('T_FILTRAGRIDA',               'GRIDKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf))'),
 ('T_GRIDAKOKA',                 'GRIDKOMPID IN (SELECT id FROM #komp) OR IDKONFIGURIM IN (SELECT id FROM #konf)'),
 ('T_KONFLLOJRRESHTI',           'IDKUSHTEMPLATE IN (SELECT IDKUSHTEMPLATE FROM T_KUSHTEMPLATE WHERE IDKONFIGAMBJENTE IN (SELECT id FROM #konf))'),
 ('T_KUSHTEMPLATE',              'IDKONFIGAMBJENTE IN (SELECT id FROM #konf)'),
 ('T_ATRIBUTETRUPI',             'IDKONFIGAMBJENTE IN (SELECT id FROM #konf)'),
 ('T_KONFIGAMBJENTE',            'IDKONFIGAMBJENTE IN (SELECT id FROM #konf)'),
 ('T_NIVELREGJISTRIMI',          'IDNIVEL IN (SELECT id FROM #niv)'),
 ('T_KATEGORINIVELDOK',          'IDKATDOK IN (SELECT id FROM #kat)'),
 ('T_KOMPONENTE',                'IDKOMPON IN (SELECT id FROM #komp)'),
 -- modulet 23 Harta (M_RAP_GIS), 24 CRM, 25 GIS, 58 Dashboard
 ('T_AMBJENTIMODULI',            'IDMODULI IN (23, 24, 25, 58)'),
 ('T_LISTAKONFIGURIMEVERAP',     'IDMODULI IN (23, 24, 25, 58)');

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
    SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@tabela + '_07') + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
             + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
    EXEC (@sql);
    INSERT hequr.T_RENDI_07 (tabela, rreshta) VALUES (@tabela, @n);
END

/* ---------- 4. objektet (tabela, procedura, view, funksione) ---------- */
CREATE TABLE #obj (id int PRIMARY KEY, emri sysname NOT NULL, lloji varchar(2) NOT NULL);
INSERT #obj (id, emri, lloji)
SELECT o.object_id, o.name, RTRIM(o.type) FROM sys.objects o
WHERE o.schema_id = SCHEMA_ID('dbo') AND o.type IN ('U', 'P', 'V', 'FN', 'IF', 'TF')
  AND (   o.name LIKE 'T[_]CRM[_]%'       OR o.name LIKE 'prc[_]T[_]CRM[_]%'
       OR o.name LIKE 'T[_]GIS[_]%'       OR o.name LIKE 'prc[_]T[_]GIS[_]%'   OR o.name LIKE 'prc[_]GIS[_]%'
       OR o.name LIKE 'GIS[_]%'           OR o.name LIKE 'V[_]GIS[_]%'         OR o.name LIKE 'prc[_]Integrim[_]GIS[_]%'
       OR o.name LIKE 'T[_]DASHBOARD%'    OR o.name LIKE 'prc[_]T[_]DASHBOARD%'
       OR o.name LIKE 'F[_]GIS[_]%'       OR o.name = 'GISEXPORTLAYERS'
       OR (o.name LIKE 'prc[_]Integrim[_]%' AND (o.name LIKE '%CRM%' OR o.name LIKE '%Takim%' OR o.name LIKE '%Anketa%'
                                               OR o.name LIKE '%Detyra%' OR o.name LIKE '%GIS%' OR o.name LIKE '%Layer%'))
       OR o.name LIKE 'prc[_]Intergrim[_]%WEBGIS%' OR o.name LIKE 'prc[_]Asistenti[_]Anketa%'
       OR o.name IN ('T_PERDORUES_CRM_VFSHOPS', 'prc_T_NDERMARJE_ktheIdNdermarrjenEPareNeListeTeKonfigurimeEmail')
       OR o.name IN ('prc_T_Email_merrHistorikun', 'prc_T_ASETE_LIDHJE_MAGAZINE_SERIAL_perNdermarrje',
                     'prc_T_KLIENTFURNITOR_ktheKFNdermarrjesAndAutorizimeDTPerCRM', 'prc_T_KONFIGAMBJENTE_ktheIdKonfigAmbjenteSipasLidhjesGISWEB',
                     'prc_T_NDERMARJE_merrSipasLicensesGIS', 'prc_T_ROLDREJTA_merrTeDrejtaRoliPerCRM', 'prc_T_ROLDREJTA_merrTeDrejtaRoliPerGIS',
                     'Prc_Rap_AnalitikAnketa', 'Prc_Rap_EvidencaDetyrave', 'PivotGrid_CRM'));

-- i perdor ende programi
DELETE FROM #obj WHERE emri IN ('T_GIS_App_FILTRAGRIDA', 'T_GIS_App_LLOJLAYERMAGAZINE', 'prc_T_GIS_App_LLOJLAYERMAGAZINE_merrGjitheLlojLayerMagazine',
    'prc_GIS_Combo_ElementeWEBGISNeNje_sel', 'prc_GIS_Combo_StatusObjekteshWEBGIS')
    OR emri LIKE 'prc[_]T[_]GIS[_]App[_]FILTRAGRIDA[_]%';

-- nje objekt mbetet nese e perdor (drejtperdrejt, me emer ne tekst apo me celes te huaj) dicka qe nuk hiqet; perseritet
-- derisa lista te mos ndryshoje
DECLARE @hequr int = 1;
WHILE @hequr > 0
BEGIN
    INSERT hequr.T_MBETEN_07 (emri, arsye)
    SELECT DISTINCT o.emri, N'e perdor ' + OBJECT_NAME(d.referencing_id)
    FROM #obj o JOIN sys.sql_expression_dependencies d ON (d.referenced_id = o.id OR (d.referenced_id IS NULL AND d.referenced_entity_name = o.emri))
    JOIN sys.objects r ON r.object_id = d.referencing_id
    WHERE r.object_id NOT IN (SELECT id FROM #obj) AND r.type IN ('P', 'V', 'FN', 'IF', 'TF', 'TR')
      AND NOT (r.type = 'TR' AND r.parent_object_id IN (SELECT id FROM #obj))
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_07);
    SET @hequr = @@ROWCOUNT;

    INSERT hequr.T_MBETEN_07 (emri, arsye)
    SELECT o.emri, N'celes i huaj nga ' + MIN(OBJECT_NAME(fk.parent_object_id))
    FROM #obj o JOIN sys.foreign_keys fk ON fk.referenced_object_id = o.id
    WHERE fk.parent_object_id NOT IN (SELECT id FROM #obj) AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_07)
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    -- emri ne tekstin e nje procedure/view qe mbetet (p.sh. SQL dinamik)
    INSERT hequr.T_MBETEN_07 (emri, arsye)
    SELECT o.emri, N'emri ne tekstin e ' + MIN(OBJECT_NAME(m.object_id))
    FROM #obj o JOIN sys.sql_modules m ON m.definition LIKE N'%' + REPLACE(o.emri, '_', '[_]') + N'%'
    WHERE m.object_id NOT IN (SELECT id FROM #obj) AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_07)
      AND NOT EXISTS (SELECT 1 FROM sys.triggers t WHERE t.object_id = m.object_id AND t.parent_id IN (SELECT id FROM #obj))
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    DELETE FROM #obj WHERE emri IN (SELECT emri FROM hequr.T_MBETEN_07);
END

INSERT hequr.T_OBJEKTE_07 (emri, lloji) SELECT emri, lloji FROM #obj;

DECLARE @emri sysname;
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_07;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

COMMIT;

SELECT tabela, rreshta FROM hequr.T_RENDI_07 ORDER BY nr;
SELECT lloji, COUNT(*) objekte FROM hequr.T_OBJEKTE_07 GROUP BY lloji;
SELECT emri, arsye FROM hequr.T_MBETEN_07 ORDER BY emri;
