/*
  DRAFT 08 (2026-11-25): heqja nga databaza e aplikacionit mobile (Alkid: "mobile nuk do perdoret fare"):
    - moduli Mobile (T_MODULI 33, M_MOBILE): komponentet 20000-20039, te drejtat, licencat, ambjenti mobile (T_AMBJENTE.AMBJENTPERMOBILE = 1),
    - kontrolli "caktimi i formatit te printimit te mobile" (btnCaktimiFormatitPrintimitMobile / lblEmertimiFormatitPrintimitMobile),
    - procedurat e aplikacionit mobile (prc_Integrim_Mob_*, raportet *MOB, login/regId/push notification, anketat, detyrat,
      formatet e printimit mobile, Vodafone shops),
    - tabelat e mobile (T_FORMATEPRINTIMIMOBILE, T_MOBILE_*, T_NRAUTOM_DERGUARMOBILE, T_RAPORTE_X_MOBILE, T_ImportMobile*,
      T_CRM_REGID, T_CRM_PUSHNOTIFICATION_LOG) dhe tabelat CRM/GIS qe 07 i la vetem sepse i perdornin procedurat e mobile.

  Mbeten:
    - kolonat mobile ne tabela qe ruhen nga programi (SHFAQMOBILE, RENDITJAMOBILE, KYCURMOBILE, IDAMBJENTMOBILE, KODIMOBILE ...),
    - procedurat prc_Integrim_* te sinkronizimit (*PasDatesSeFundit), te import/eksportit, OSHE, Dast, punesimeve (mund t'i perdore
      ndonje integrim tjeter; hiqen vetem nese konfirmohet),
    - prc_Integrim_fotoNgaArkiv dhe prc_Integrim_lexoFSTNgaTabelaTempPolicash (i therret programi),
    - cdo objekt qe e perdor ende nje procedure, view apo tabele qe nuk hiqet (kontrollohet me poshte).

  Si 05/06/07: asgje nuk fshihet perfundimisht. Rreshtat ruhen ne hequr.<TABELA>_08, objektet kalojne ne skemen [hequr].
    - kthimi mbrapsht:        08-rollback-heqja-mobile.sql
    - fshirja perfundimtare:  08-pastrimi-perfundimtar.sql (pasi te jete provuar programi)
  Nese dicka nuk shkon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 08-heqja-mobile.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_08') IS NOT NULL
BEGIN
    RAISERROR('Skripti 08 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_08 ekziston).', 16, 1);
    RETURN;
END

-- teksti i procedurave/view-ve merret nje here, jashte transaksionit (shih 07); vetem objektet e skemes dbo
SELECT m.object_id, CASE WHEN o.type = 'TR' THEN o.parent_object_id END AS parent_id, m.definition
INTO #mod FROM sys.sql_modules m JOIN sys.objects o ON o.object_id = m.object_id
WHERE o.schema_id = SCHEMA_ID('dbo');

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_08 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);
CREATE TABLE hequr.T_OBJEKTE_08 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_MBETEN_08 (emri sysname PRIMARY KEY, arsye nvarchar(400) NOT NULL);   -- vetem per raport
CREATE TABLE hequr.T_DEFINICIONE_08 (emri sysname PRIMARY KEY, definicioni nvarchar(max) NOT NULL);

DECLARE @s nvarchar(max);
DECLARE @mobile bit = CASE WHEN EXISTS (SELECT 1 FROM T_MODULI WHERE IDMODULI = 33 AND MODULIKODI = 'M_MOBILE') THEN 1 ELSE 0 END;

/* ---------- 1. ambjenti mobile ---------- */
CREATE TABLE #amb (id numeric(18,0) PRIMARY KEY);
IF COL_LENGTH('dbo.T_AMBJENTE', 'AMBJENTPERMOBILE') IS NOT NULL
    EXEC ('INSERT #amb SELECT ID FROM T_AMBJENTE WHERE AMBJENTPERMOBILE = 1');
-- nje ambjent qe e perdor ndonje tabele tjeter mbetet
SET @s = N'';
SELECT @s = @s + N'DELETE a FROM #amb a WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = a.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_AMBJENTE');
EXEC (@s);
IF COL_LENGTH('dbo.T_PERDORUESI', 'IDAMBJENTMOBILE') IS NOT NULL
    EXEC ('DELETE a FROM #amb a WHERE EXISTS (SELECT 1 FROM T_PERDORUESI p WHERE p.IDAMBJENTMOBILE = a.id)');

/* ---------- 2. komponentet e modulit Mobile ---------- */
CREATE TABLE #komp (id numeric(18,0) PRIMARY KEY);
IF @mobile = 1
    INSERT #komp SELECT IDKOMPON FROM T_KOMPONENTE WHERE IDMODULI = 33;
DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM T_KATEGORINIVELDOK c WHERE c.IDKOMPONENTE = k.id);
IF OBJECT_ID('dbo.T_AMBJENTE', 'U') IS NOT NULL
    DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM T_AMBJENTE a WHERE a.IDKOMPONENTE = k.id AND a.ID NOT IN (SELECT id FROM #amb));
-- nje komponente qe e perdor ndonje tabele tjeter mbetet
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KOMPONENTE')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_ROLDREJTATRUPI', 'T_ROLDREJTATRUPI_HISTORIK', 'T_MENUPERKOMPONENTE',
        'T_KOMPONENTEPERLLOJLICENCE', 'T_KOMPONENTE', 'T_AMBJENTE');
EXEC (@s);

/* ---------- 3. raportet e modulit Mobile (nese ka) ---------- */
CREATE TABLE #rap (id numeric(18,0) PRIMARY KEY);
IF @mobile = 1
    INSERT #rap SELECT IDRAPORTI FROM T_RAPORTI WHERE IDMODULI = 33;
CREATE TABLE #design (id numeric(18,0) PRIMARY KEY);
INSERT #design SELECT IDRAPORTDESING FROM T_RAPORTDESING WHERE IDRAPORTI IN (SELECT id FROM #rap);
SET @s = N'';
SELECT @s = @s + N'DELETE d FROM #design d WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = d.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_RAPORTDESING') AND fk.parent_object_id <> OBJECT_ID('dbo.T_RAPXDESIGN');
EXEC (@s);
DELETE r FROM #rap r WHERE EXISTS (SELECT 1 FROM T_RAPORTDESING d WHERE d.IDRAPORTI = r.id AND d.IDRAPORTDESING NOT IN (SELECT id FROM #design));

/* ---------- 4. kontrolli i formatit te printimit per mobile ---------- */
CREATE TABLE #kontr (id numeric(18,0) PRIMARY KEY);
INSERT #kontr SELECT IDKONTROLL FROM T_KONTROLLE
WHERE KODKONTROLL IN ('btnCaktimiFormatitPrintimitMobile', 'lblEmertimiFormatitPrintimitMobile');
-- mbetet nese e perdor ndonje tabele tjeter, ose nese ndonje tabele i referohet rreshtave te tij ne T_ATRIBUTETRUPI
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #kontr k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KONTROLLE') AND fk.parent_object_id <> OBJECT_ID('dbo.T_ATRIBUTETRUPI');
SELECT @s = @s + N'DELETE k FROM #kontr k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x JOIN dbo.T_ATRIBUTETRUPI a ON a.' + QUOTENAME(COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id))
     + N' = x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' WHERE a.IDKONTROLL = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_ATRIBUTETRUPI');
EXEC (@s);

/* ---------- 5. rreshtat: ruhen ne hequr.<TABELA>_08, pastaj fshihen (femijet para prinderve) ---------- */
CREATE TABLE #hap (nr int IDENTITY PRIMARY KEY, tabela sysname, kushti nvarchar(max));
INSERT #hap (tabela, kushti) VALUES
 ('T_ROLDREJTATRUPI',            'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDRAPORTI IN (SELECT id FROM #rap)'),
 ('T_ROLDREJTATRUPI_HISTORIK',   'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDRAPORTI IN (SELECT id FROM #rap)'),
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
 ('T_AMBJENTE',                  'ID IN (SELECT id FROM #amb)'),
 ('T_KOMPONENTE',                'IDKOMPON IN (SELECT id FROM #komp)'),
 ('T_ATRIBUTETRUPI',             'IDKONTROLL IN (SELECT id FROM #kontr)'),
 ('T_KONTROLLE',                 'IDKONTROLL IN (SELECT id FROM #kontr)'),
 -- moduli 33 Mobile
 ('T_AMBJENTIMODULI',            'IDMODULI = 33 AND EXISTS (SELECT 1 FROM T_MODULI WHERE IDMODULI = 33 AND MODULIKODI = ''M_MOBILE'')'),
 ('T_LISTAKONFIGURIMEVERAP',     'IDMODULI = 33 AND EXISTS (SELECT 1 FROM T_MODULI WHERE IDMODULI = 33 AND MODULIKODI = ''M_MOBILE'')');

DECLARE @nr int = 1, @tabela sysname, @kushti nvarchar(max), @n int, @sql nvarchar(max);
WHILE EXISTS (SELECT 1 FROM #hap WHERE nr = @nr)
BEGIN
    SELECT @tabela = tabela, @kushti = kushti FROM #hap WHERE nr = @nr;
    SET @nr += 1;
    IF OBJECT_ID('dbo.' + @tabela, 'U') IS NULL CONTINUE;
    SET @sql = N'SELECT @n = COUNT(*) FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti;
    EXEC sp_executesql @sql, N'@n int OUTPUT', @n OUTPUT;
    IF @n = 0 CONTINUE;
    SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@tabela + '_08') + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
             + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
    EXEC (@sql);
    INSERT hequr.T_RENDI_08 (tabela, rreshta) VALUES (@tabela, @n);
END

/* ---------- 5b. procedura e pastrimit sipas licences nuk i prek me tabelat e mobile ----------
   Cdo bllok fillon me nje rresht shenje "-----T_<TABELA>-----" dhe mbaron para rreshtit te shenjes tjeter ("-----").
   Nese nje bllok nuk gjendet, tabela e tij mbetet (e mban kontrolli i varesive me poshte). */
DECLARE @def nvarchar(max) = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_FshiVeprime_SipasLicences')), @def0 nvarchar(max);
SET @def0 = @def;
DECLARE @shenja TABLE (nr int IDENTITY, modeli nvarchar(200));
INSERT @shenja (modeli) VALUES (N'%--T[_]FORMATEPRINTIMIMOBILE%'), (N'%--T[_]MOBILE[_]TEDREJTA[^A-Z]%'), (N'%--T[_]NRAUTOM[_]DERGUARMOBILE%'),
                               (N'%--T[_]MOBILE[_]KOMPONENTE[^A-Z]%'), (N'%--T[_]RAPORTE[_]X[_]MOBILE%'), (N'%--T[_]CRM[_]%');
DECLARE @modeli nvarchar(200), @i int = 1, @p int, @a int, @eol int, @m int, @b int, @rrotullime int = 0;
WHILE @def IS NOT NULL AND EXISTS (SELECT 1 FROM @shenja WHERE nr = @i)
BEGIN
    SELECT @modeli = modeli FROM @shenja WHERE nr = @i;
    SET @i += 1;
    WHILE 1 = 1
    BEGIN
        SET @p = PATINDEX(@modeli, @def);
        IF @p = 0 BREAK;
        SET @rrotullime += 1;
        IF @rrotullime > 30 THROW 50008, 'SP_FshiVeprime_SipasLicences: shume blloqe per t''u hequr, skripti ndalet.', 1;
        SET @a = @p - CHARINDEX(CHAR(10), REVERSE(LEFT(@def, @p))) + 1;      -- fillimi i rreshtit te shenjes
        SET @eol = CHARINDEX(CHAR(10), @def, @p);                               -- fundi i rreshtit te shenjes
        SET @m = CHARINDEX('-----', @def, @eol);                                -- shenja e bllokut pasardhes
        IF @eol = 0 OR @m = 0 THROW 50008, 'SP_FshiVeprime_SipasLicences: blloku nuk ka shenje pas tij.', 1;
        SET @b = @m - CHARINDEX(CHAR(10), REVERSE(LEFT(@def, @m))) + 1;      -- fillimi i rreshtit te shenjes pasardhese
        SET @def = STUFF(@def, @a, @b - @a, N'-- (DRAFT 08) blloku i nje tabele te mobile u hoq bashke me mobile-n' + CHAR(13) + CHAR(10));
    END
END
IF @def <> @def0
BEGIN
    INSERT hequr.T_DEFINICIONE_08 VALUES ('SP_FshiVeprime_SipasLicences', @def0);
    SET @def = STUFF(@def, PATINDEX('%CREATE%PROC%', @def), LEN('CREATE'), 'ALTER');
    EXEC (@def);
    UPDATE #mod SET definition = @def WHERE object_id = OBJECT_ID('dbo.SP_FshiVeprime_SipasLicences');
END

/* ---------- 6. objektet (tabela, procedura, view, funksione) ---------- */
CREATE TABLE #obj (id int PRIMARY KEY, emri sysname NOT NULL, lloji varchar(2) NOT NULL);
INSERT #obj (id, emri, lloji)
SELECT o.object_id, o.name, RTRIM(o.type) FROM sys.objects o
WHERE o.schema_id = SCHEMA_ID('dbo') AND o.type IN ('U', 'P', 'V', 'FN', 'IF', 'TF')
  AND (   o.name LIKE 'prc[_]Integrim[_]Mob[_]%'
       OR o.name IN (
            -- procedurat e aplikacionit mobile
            'Kristi_shtoKomponenteTeRe_Mobile_RaporteShitje', 'PRC_RAP_MARZHISHITJES_GRAFIK_MOB', 'PRC_RAP_ShitjeDitoreMOB',
            'prc_Integrim_lexoFormatePrintimiMobile', 'prc_Integrim_perditesoPerdoruesDheDateLoginMobile', 'prc_Integrim_UpdateKlientNgaMobile',
            'prc_Integrim_lexoRaportePerMobile', 'prc_Integrim_merrAgjentinSipasPerdoruesit', 'prc_Integrim_lexoKlientetDateFunditPerAgjent',
            'prc_Integrim_updateStatusRangeNrAutomatikDerguar', 'prc_Integrim_kontrolloEkzistenceRegId', 'prc_Integrim_merrRegIdNgaDatabaza',
            'prc_Integrim_shkruajRegID', 'prc_Integrim_shkruajDateDergimiPushNotificationNeLog', 'prc_Integrim_shkruajDatePergjigjePushNotificationNeLog',
            'prc_Integrim_shkruajPushNotificationNeLog', 'prc_Integrim_shkruajKokaAnkete', 'prc_Integrim_KontrolloEkzistenceDetyreKlientiNeWeb',
            'prc_Integrim_T_TEMP_PERDORUES_VF_SHOP_WEB', 'prc_Integrim_VodPerdoruesView_T_TEMP_PERDORUES_VF_SHOP',
            'prc_Integrimi_T_GIS_App_LIDHJE_OBJEKTE_WEB_ins',
            'prc_T_AGJENTSHITJE_ktheAgjentSHitjeSipasIdPErdoruesMobile', 'prc_T_FORMATEPRINTIMIMOBILE_merrDesingSipasKategorisePerMobile',
            'prc_T_NRAUTOM_EshteILidhurMeAtributeTrupiPerMobile', 'prc_T_PERDORUESI_kthePerdoruesKycurMobile',
            'prc_T_PERDORUESI_merrNdermarrjetPerdoruesitPerMobile', 'prc_T_RAPORTE_X_MOBILE',
            -- tabelat e mobile
            'T_CRM_PUSHNOTIFICATION_LOG', 'T_CRM_REGID', 'T_FORMATEPRINTIMIMOBILE', 'T_ImportMobileKokaArketime', 'T_ImportMobileTrupiArketime',
            'T_MOBILE_KOMPONENTE', 'T_MOBILE_TEDREJTA', 'T_NRAUTOM_DERGUARMOBILE', 'T_RAPORTE_X_MOBILE', 'T_PERDORUES_CRM_VFSHOPS',
            -- tabelat CRM/GIS qe 07 i la per shkak te procedurave te mobile
            'T_CRM_DETYRE_KLIENT', 'T_CRM_DETYRE_KLIENT_AGJENT', 'T_CRM_KLIENT_ANKETA', 'T_CRM_KOKAANKETA', 'T_CRM_KOKAKLIENT_ANKETA_AGJENT',
            'T_CRM_SKEDULER', 'T_GIS_App_LAYERS', 'T_GIS_App_LAYERSFOLDER', 'T_GIS_App_LAYERSKOKA', 'T_GIS_App_LAYERSTYPE',
            'T_GIS_App_LAYERSXWORKSPACE', 'T_GIS_App_LIDHJE_OBJEKTE_ARKIVA', 'T_GIS_App_LIDHJE_OBJEKTE_CONN', 'T_GIS_App_LIDHJE_OBJEKTE_WEB',
            'T_GIS_App_WORKSPACE', 'T_GIS_INTEGRIM_SAP_FBHISTORIKU', 'T_GIS_Layer_LANDFILLS', 'T_GIS_Layer_LANDFILLSEXTENSION',
            'T_GIS_Layer_NEWALBANIALGUDIVISION', 'T_GIS_Layer_NEWALBANIAMUNICIPALITIES'));

-- nje objekt mbetet nese e perdor (drejtperdrejt, me emer ne tekst apo me celes te huaj) dicka ne skemen dbo qe nuk hiqet;
-- perseritet derisa lista te mos ndryshoje. Objektet ne skemen [hequr] (te hequra nga 05-07) nuk llogariten.
DECLARE @hequr int = 1;
WHILE @hequr > 0
BEGIN
    INSERT hequr.T_MBETEN_08 (emri, arsye)
    SELECT o.emri, N'e perdor ' + MIN(OBJECT_NAME(d.referencing_id))
    FROM #obj o JOIN sys.sql_expression_dependencies d ON (d.referenced_id = o.id OR (d.referenced_id IS NULL AND d.referenced_entity_name = o.emri))
    JOIN sys.objects r ON r.object_id = d.referencing_id
    WHERE r.object_id NOT IN (SELECT id FROM #obj) AND r.type IN ('P', 'V', 'FN', 'IF', 'TF', 'TR') AND r.schema_id = SCHEMA_ID('dbo')
      AND NOT (r.type = 'TR' AND r.parent_object_id IN (SELECT id FROM #obj))
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_08)
    GROUP BY o.emri;
    SET @hequr = @@ROWCOUNT;

    INSERT hequr.T_MBETEN_08 (emri, arsye)
    SELECT o.emri, N'celes i huaj nga ' + MIN(OBJECT_NAME(fk.parent_object_id))
    FROM #obj o JOIN sys.foreign_keys fk ON fk.referenced_object_id = o.id
    WHERE fk.parent_object_id NOT IN (SELECT id FROM #obj) AND OBJECT_SCHEMA_NAME(fk.parent_object_id) = 'dbo'
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_08)
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    INSERT hequr.T_MBETEN_08 (emri, arsye)
    SELECT o.emri, N'emri ne tekstin e ' + MIN(OBJECT_NAME(m.object_id))
    FROM #obj o JOIN #mod m ON m.definition LIKE N'%' + REPLACE(o.emri, '_', '[_]') + N'%'
    WHERE m.object_id NOT IN (SELECT id FROM #obj) AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_08)
      AND (m.parent_id IS NULL OR m.parent_id NOT IN (SELECT id FROM #obj))
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    DELETE FROM #obj WHERE emri IN (SELECT emri FROM hequr.T_MBETEN_08);
END

INSERT hequr.T_OBJEKTE_08 (emri, lloji) SELECT emri, lloji FROM #obj;

DECLARE @emri sysname;
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_08;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

COMMIT;

SELECT tabela, rreshta FROM hequr.T_RENDI_08 ORDER BY nr;
SELECT lloji, emri FROM hequr.T_OBJEKTE_08 ORDER BY lloji, emri;
SELECT emri, arsye FROM hequr.T_MBETEN_08 ORDER BY emri;
