/*
  DRAFT 05 (2026-11-23): heqja e Raporteve Inteligjente (BI: kubet / Raport_PivotGrid) dhe e mbetjeve te pjeseve
  qe jane hequr nga programi (Vodafone, Tollona, faqe qe nuk ekzistojne me).

  Asgje nuk fshihet perfundimisht ketu: rreshtat ruhen ne skemen [hequr] (tabelat hequr.<TABELA>_20261123) dhe
  objektet (tabela, procedura, view) kalojne ne skemen [hequr]. Keshtu:
    - kthimi mbrapsht:        05-rollback-heqja-bi-dhe-mbetjeve.sql
    - fshirja perfundimtare:  05-pastrimi-perfundimtar.sql (pasi te jete provuar programi)
  Ne nje databaze klienti hiqen vetem rreshtat qe nuk perdoren nga dokumentet (p.sh. nje format printimi qe e ka
  ndonje fature mbetet). Nese ndonje tabele tjeter i referon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 05-heqja-bi-dhe-mbetjeve.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_20261123') IS NOT NULL
BEGIN
    RAISERROR('Skripti 05 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_20261123 ekziston).', 16, 1);
    RETURN;
END

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_20261123 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);
CREATE TABLE hequr.T_OBJEKTE_20261123 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_DEFINICIONE_20261123 (emri sysname PRIMARY KEY, definicioni nvarchar(max) NOT NULL);

/* ---------- 1. cfare hiqet ---------- */
CREATE TABLE #rap (id numeric(18,0) PRIMARY KEY);      -- raporte: kubet BI (moduli 19) + raportet Vodafone
INSERT #rap SELECT IDRAPORTI FROM T_RAPORTI WHERE IDMODULI = 19;
INSERT #rap SELECT IDRAPORTI FROM T_RAPORTI WHERE IDMODULI <> 19 AND IDRAPORTI IN
    (129,144,150,151,153,155,156,158,159,501,502,504,505,506,510,674,675,731,734,735,815);

CREATE TABLE #komp (id numeric(18,0) PRIMARY KEY);     -- komponente: faqet BI + faqe qe nuk ekzistojne me
INSERT #komp SELECT IDKOMPON FROM T_KOMPONENTE
WHERE KOMPONEMRI LIKE 'Raport[_]PivotGrid.aspx%' OR KOMPONEMRI = 'Raportet.aspx?idmod=19'
   OR KOMPONEMRI IN ('UserThemes.aspx', 'PreviewThemeAmbjente.aspx', 'ImportWK.aspx?lloji=importwk', 'ImportWK.aspx?lloji=importfk',
                     'Shto_Makro.aspx', 'Modifiko_Makro.aspx', 'NdryshimCmimi.aspx', 'LupaLlogariaKf.aspx', 'LupaNrAutomatik.aspx',
                     'LupaQendraKosto.aspx', 'LupaValidim.aspx', 'LupaQendraKostoPlote.aspx')
   OR KOMPONEMRI LIKE 'ImportWK.aspx?lloji=importtollona%' OR KOMPONEMRI IN ('Backup.aspx', 'Restore.aspx');
-- komponentet qe kane kategori dokumentesh (konfigurime dokumentash) nuk hiqen
DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM T_KATEGORINIVELDOK c WHERE c.IDKOMPONENTE = k.id);

CREATE TABLE #design (id numeric(18,0) PRIMARY KEY);   -- formate printimi: te raporteve te mesiperme + 72 formate klientesh te vjeter
INSERT #design SELECT IDRAPORTDESING FROM T_RAPORTDESING
WHERE IDRAPORTI IN (SELECT id FROM #rap)
   OR IDRAPORTDESING IN (10249,10250,10251,10252,121,160,161,20275,20276,20278,20280,20281,20287,20291,20292,20293,20294,20295,
       20296,20297,20298,20299,30353,30354,30574,30928,30936,30941,30942,30943,31038,31047,31176,31177,31186,31201,31231,31305,
       31314,31315,31327,31329,31330,31336,31338,31339,31418,31419,31424,31425,31426,31427,31429,31435,31437,31467,31471,31472,
       31479,31482,31538,31539,31542,31609,31612,31650,31651,31676,31677,31680,31685,31687);
-- nje format qe e perdor ndonje dokument (cdo FK tjeter perveç T_RAPXDESIGN) mbetet, bashke me raportin e tij
DECLARE @s nvarchar(max) = N'';
SELECT @s = @s + N'DELETE d FROM #design d WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = d.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_RAPORTDESING') AND fk.parent_object_id <> OBJECT_ID('dbo.T_RAPXDESIGN');
EXEC (@s);
DELETE r FROM #rap r WHERE EXISTS (SELECT 1 FROM T_RAPORTDESING d WHERE d.IDRAPORTI = r.id AND d.IDRAPORTDESING NOT IN (SELECT id FROM #design));
-- nje raport qe mbetet nuk e humb formatin e tij te fundit (perndryshe zhduket nga lista e raporteve)
DELETE d FROM #design d JOIN T_RAPORTDESING x ON x.IDRAPORTDESING = d.id
WHERE x.IDRAPORTI NOT IN (SELECT id FROM #rap)
  AND NOT EXISTS (SELECT 1 FROM T_RAPORTDESING y WHERE y.IDRAPORTI = x.IDRAPORTI AND y.IDRAPORTDESING NOT IN (SELECT id FROM #design));

CREATE TABLE #menu (id numeric(18,0) PRIMARY KEY);     -- butona toolbar-i Vodafone: Kthim Orderi, Blerje Vodafone
INSERT #menu SELECT IDMENUITEM FROM T_MENUITEM WHERE IDMENUITEM IN (113, 115) AND NAME IN ('KthimVod', 'Bli');

CREATE TABLE #kusht (id numeric(18,0) PRIMARY KEY);    -- opsione dokumentash qe programi nuk i lexon me (Vodafone, Tollona)
INSERT #kusht SELECT IDKUSHT FROM T_KUSHTE WHERE KODI IN ('DEVFOne', 'ZDVFONE', 'KGJVFONE', 'ZBVFONE', 'RSHTT', 'RSHTTK', 'RSHTTKE', 'ZTK');

/* ---------- 2. rreshtat: ruhen ne hequr.<TABELA>_20261123, pastaj fshihen (femijet para prinderve) ---------- */
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
 ('T_MENUPERKOMPONENTE',         'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDMENUITEM IN (SELECT id FROM #menu)'),
 ('T_MENUITEM',                  'IDMENUITEM IN (SELECT id FROM #menu)'),
 ('T_KOMPONENTEPERLLOJLICENCE',  'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_LUPAMULTIPLE',              'GRIDATRUPIID IN (SELECT GRIDATRUPIID FROM T_GRIDATRUPI WHERE GRIDAKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp)))'),
 ('T_GRIDATRUPI',                'GRIDAKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp))'),
 ('T_FILTRAGRIDA',               'GRIDKOKAID IN (SELECT GRIDKOKAID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp))'),
 ('T_GRIDAKOKA',                 'GRIDKOMPID IN (SELECT id FROM #komp)'),
 ('T_KOMPONENTE',                'IDKOMPON IN (SELECT id FROM #komp)'),
 ('T_KONFLLOJRRESHTI',           'IDKUSHTEMPLATE IN (SELECT IDKUSHTEMPLATE FROM T_KUSHTEMPLATE WHERE IDKUSHT IN (SELECT id FROM #kusht))'),
 ('T_KUSHTEMPLATE',              'IDKUSHT IN (SELECT id FROM #kusht)'),
 ('T_ALTERNATIVAKUSHTI',         'IDKUSHTI IN (SELECT id FROM #kusht)'),
 ('T_KUSHTE',                    'IDKUSHT IN (SELECT id FROM #kusht)'),
 ('T_AMBJENTIMODULI',            'IDMODULI = 19'),
 ('T_LISTAKONFIGURIMEVERAP',     'IDMODULI = 19');

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
    SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@tabela + '_20261123') + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
             + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
    EXEC (@sql);
    INSERT hequr.T_RENDI_20261123 (tabela, rreshta) VALUES (@tabela, @n);
END

/* ---------- 3. procedura e pastrimit sipas licences nuk i prek me tabelat e pivot-it ---------- */
DECLARE @def nvarchar(max) = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_FshiVeprime_SipasLicences'));
DECLARE @a int = CHARINDEX('T_KONFIG_PIVOTGRIDA_KOKA---', @def), @b int = CHARINDEX('DROP TABLE KONFIGPIVOTGRIDATRUPI;', @def);
IF @def IS NOT NULL AND @a > 0 AND @b > @a
BEGIN
    INSERT hequr.T_DEFINICIONE_20261123 VALUES ('SP_FshiVeprime_SipasLicences', @def);
    SET @a = @a - CHARINDEX(CHAR(10), REVERSE(LEFT(@def, @a))) + 1;     -- nga fillimi i rreshtit me shenjen
    SET @def = STUFF(@def, @a, @b + LEN('DROP TABLE KONFIGPIVOTGRIDATRUPI;') - @a,
                     '-- tabelat T_KONFIG_PIVOTGRIDA_* u hoqen bashke me Raportet Inteligjente (DRAFT 05)');
    SET @def = STUFF(@def, PATINDEX('%CREATE%PROC%', @def), LEN('CREATE'), 'ALTER');
    EXEC (@def);
END

/* ---------- 4. objektet: kalojne ne skemen [hequr] ---------- */
INSERT hequr.T_OBJEKTE_20261123 (emri, lloji)
SELECT o.name, o.type FROM sys.objects o
WHERE o.schema_id = SCHEMA_ID('dbo') AND o.type IN ('U', 'P', 'V') AND o.name IN (
 -- BI / Raport_PivotGrid
 'T_KONFIG_PIVOTGRIDA_TRUPI', 'T_KONFIG_PIVOTGRIDA_KOKA', 'T_KOLONAT_PIVOTGRID', 'T_GRUPIMKOLONE_MODUL_PIVOTGRID', 'T_GRUPIMKOLONE_PIVOTGRID',
 'PivotGrid', 'PivotGrid_Blerje', 'PivotGrid_CRM', 'PivotGrid_Kontabiliteti', 'PivotGrid_Listpagesa', 'PivotGrid_Listpagesa_vleratEFundit',
 'PivotGrid_Magazina', 'PivotGrid_Organika', 'PivotGrid_Organika_raportuese', 'prc_T_GRUPIMKOLONE_PIVOTGRID_ktheSipasIDModulit',
 'prc_T_KONFIG_PIVOTGRIDA_KOKA_del', 'prc_T_KONFIG_PIVOTGRIDA_KOKA_ins', 'prc_T_KONFIG_PIVOTGRIDA_KOKA_insrestore',
 'prc_T_KONFIG_PIVOTGRIDA_KOKA_ktheKonfigPivotGridNdermarrjes', 'prc_T_KONFIG_PIVOTGRIDA_KOKA_ktheKonfigPivotGridNdermarrjesModulit',
 'prc_T_KONFIG_PIVOTGRIDA_KOKA_ktheKonfigPivotGridNdermarrjesPerGride', 'prc_T_KONFIG_PIVOTGRIDA_KOKA_ktheSipasID',
 'prc_T_KONFIG_PIVOTGRIDA_KOKA_ktheSipasKoditDheNdermarrjes', 'prc_T_KONFIG_PIVOTGRIDA_KOKA_upd', 'prc_T_KONFIG_PIVOTGRIDA_KOKA_upddel',
 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_del', 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_delSipasIdKonfigPGKoka', 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_ins',
 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_insrestore', 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_ktheKonfigDefaultShtim',
 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_ktheKonfigPGTrupiSipasId', 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_ktheKonfigPGTrupiSipasIdKoka',
 'prc_T_KONFIG_PIVOTGRIDA_TRUPI_upd', 'prc_T_RAPORTI_selIdRapPivotGridSipasModulit',
 -- Vodafone / Tollona: as programi, as procedura te tjera nuk i perdorin
 'prc_Integrim_TollonatLexoFaturatMeParapagim', 'prc_Integrim_TollonatShkruajNgaParapagimetDaljet', 'PRC_RAP_APARATETeBLERAngaDEALER',
 'PRC_RAP_ARKA_PAGESADEALER', 'PRC_RAP_GJENDJEARTIKUJ_VODAFONE', 'PRC_RAP_GJENDJEARTIKUJ_VODAFONE_EXP', 'PRC_RAP_LIBERSHITJE_VODAFONE',
 'PRC_RAP_MAGAZINAHYRJEVODAFONE', 'PRC_RAP_MAGAZINAHYRJEVODAFONENdemarjeBije', 'PRC_RAP_MARZHISHITJES_TOLLONA',
 'PRC_RAP_MARZHISHITJES_TOLLONA_27092016', 'PRC_RAP_PagesatEKryeraPerMPesa', 'PRC_RAP_PagesatPerMPesaSipasIntervaleve',
 'PRC_RAP_POROSI_BAZAAR_DISCOUNTED_DEVICES', 'PRC_RAP_PorosiDealer', 'PRC_RAP_SHITJE_VFONE', 'PRC_RAP_STATUSI_POROSIVE_VFONE',
 'PRC_RAP_TOLLONA_ANALITIK', 'PRC_RAP_TOLLONA_PERMBLEDHES', 'PRC_RAP_VEPRIMETEANULLUARAVODAFONE', 'PRC_RAP_VEPRIMETEANULLUARAVODAFONENdermarjeBije',
 'prc_T_ARIKULLITOLLONA_ins', 'prc_T_KLIENTPERBAZAAR_existsMsisdn', 'prc_T_KLIENTPERBAZAAR_ins', 'prc_T_KLIENTPERBAZAAR_merrSipasMsisdn',
 'prc_T_KLIENTPERBAZAAR_merrSipasNdermarrjes', 'prc_T_KLIENTPERBAZAAR_updateStatusPErdorur', 'prc_T_KLIENTPERBAZAAR_validoKupon',
 'prc_T_KOKASHITJE_merrPiketNeModifikimTeVFONE', 'prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimePerImport',
 'prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimePerImportSpecifik', 'prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimeTrupiPerImport',
 'prc_T_KONSUMIMTOLLONAELEKTRONIK_merrKonsumimeTrupiPerImportSpecifik', 'prc_T_KONSUMIMTOLLONAELEKTRONIK_ndryshoStatus',
 'prc_T_KONSUMIMTOLLONAELEKTRONIK_ndryshoStatusSpecifik', 'prc_T_KONSUMIMTOLLONALETER_eshteKonsumuarTolloni', 'prc_T_KONSUMIMTOLLONALETER_ins',
 'prc_T_KONSUMIMTOLLONALETER_merrKonsumimePerImport', 'prc_T_KONSUMIMTOLLONALETER_merrKonsumimeTrupiPerImport',
 'prc_T_KONSUMIMTOLLONALETER_ndryshoStatus', 'prc_T_PERDORUESITOLLONA_ekzistonPerdorues', 'prc_T_SHITJEMESERIALTOLLONA_merrKonsumimePerImport',
 'prc_T_SHITJEMESERIALTOLLONA_merrKonsumimeTrupiPerImport', 'prc_T_SHITJEMESERIALTOLLONA_raportiPerTollonat', 'prc_T_tollonaELEKTRONIK_ins',
 'prc_T_TOLLONALETER_ekzistonSerialSipasArtikullit', 'prc_T_tollonaleter_ins', 'prc_T_TOLLONALETER_raportiPerTollonat',
 'prc_T_TRUPISHITJETOLLONA_kaTollonaShitjaKastrati', 'prc_T_TRUPISHITJETOLLONAELEKTRONIK_kaTollonaShitjaKastrati', 'T_MAPPING_BAZAAR', 'VodafoneView');

DECLARE @emri sysname;
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_20261123;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

COMMIT;

SELECT tabela, rreshta FROM hequr.T_RENDI_20261123 ORDER BY nr;
SELECT lloji, COUNT(*) objekte FROM hequr.T_OBJEKTE_20261123 GROUP BY lloji;
