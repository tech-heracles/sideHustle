/*
  DRAFT 09 (2026-11-25): heqja e integrimit me OSHEE (Alkid: "OSHEE ... nuk na duhen, eleminoj"):
    - raportet "Lista e elementeve sipas rajoneve" dhe "Lista analitike e elementeve sipas rajoneve" (klasat *_OSHEE u hoqen nga kodi),
      me te drejtat, filtrat, formatet, procedurat e tyre (PRC_RAP_GIS_LISTA...ELEMENTEVESIPASRAJONEVE) dhe parametrat ne T_SP/T_PARAMXSP,
    - prc_Integrim_MerrDokumentaOSHETemp, prc_Integrim_ShkruajDokumentaOSHETemp dhe T_GIS_INTEGRIM_SAP_FBHISTORIKU,
    - tabelat GIS qe 08 i la vetem sepse i perdornin keto raporte.

  Mbeten: PRC_RAP_PermbledheseRealizimevedheParashikimeve (PBA; aty "OSHEE" eshte vetem etikete), Elemente per integrim (faqe e programit),
  POS/kasat (Alkid: "POS lere njehere") dhe cdo objekt qe e perdor ende dicka qe nuk hiqet (kontrollohet me poshte).

  Si 05-08: asgje nuk fshihet perfundimisht. Rreshtat ruhen ne hequr.<TABELA>_09, objektet kalojne ne skemen [hequr].
    - kthimi mbrapsht:        09-rollback-heqja-oshee.sql
    - fshirja perfundimtare:  09-pastrimi-perfundimtar.sql (pasi te jete provuar programi)
  Nese dicka nuk shkon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 09-heqja-oshee.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_09') IS NOT NULL
BEGIN
    RAISERROR('Skripti 09 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_09 ekziston).', 16, 1);
    RETURN;
END

-- teksti i procedurave/view-ve merret nje here, jashte transaksionit (shih 07); vetem objektet e skemes dbo
SELECT m.object_id, CASE WHEN o.type = 'TR' THEN o.parent_object_id END AS parent_id, m.definition
INTO #mod FROM sys.sql_modules m JOIN sys.objects o ON o.object_id = m.object_id
WHERE o.schema_id = SCHEMA_ID('dbo');

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_09 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);
CREATE TABLE hequr.T_OBJEKTE_09 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_MBETEN_09 (emri sysname PRIMARY KEY, arsye nvarchar(400) NOT NULL);   -- vetem per raport
CREATE TABLE hequr.T_DEFINICIONE_09 (emri sysname PRIMARY KEY, definicioni nvarchar(max) NOT NULL);

DECLARE @s nvarchar(max);

/* ---------- 1. raportet e OSHEE ("Lista e elementeve sipas rajoneve" dhe "Lista analitike e elementeve sipas rajoneve") ----------
   Gjenden sipas tekstit ne cdo kolone tekst te T_RAPORTI (emri, klasa e raportit ose procedura). */
CREATE TABLE #rap (id numeric(18,0) PRIMARY KEY);
SET @s = N'';
SELECT @s = @s + N' OR ' + QUOTENAME(c.name) + N' LIKE ''%ELEMENTEVESIPASRAJONEVE%'' OR ' + QUOTENAME(c.name) + N' LIKE ''%[_]OSHEE%'''
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.T_RAPORTI') AND TYPE_NAME(c.user_type_id) IN ('varchar', 'nvarchar', 'char', 'nchar');
IF @s <> N''
BEGIN
    SET @s = N'INSERT #rap SELECT IDRAPORTI FROM T_RAPORTI WHERE 1 = 0' + @s;
    EXEC (@s);
END
CREATE TABLE #design (id numeric(18,0) PRIMARY KEY);
INSERT #design SELECT IDRAPORTDESING FROM T_RAPORTDESING WHERE IDRAPORTI IN (SELECT id FROM #rap);
SET @s = N'';
SELECT @s = @s + N'DELETE d FROM #design d WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = d.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_RAPORTDESING') AND fk.parent_object_id <> OBJECT_ID('dbo.T_RAPXDESIGN');
EXEC (@s);
DELETE r FROM #rap r WHERE EXISTS (SELECT 1 FROM T_RAPORTDESING d WHERE d.IDRAPORTI = r.id AND d.IDRAPORTDESING NOT IN (SELECT id FROM #design));

/* ---------- 2. procedurat e ketyre raporteve ne T_SP dhe parametrat e tyre ne T_PARAMXSP ---------- */
CREATE TABLE #sp (id numeric(18,0) PRIMARY KEY);
DECLARE @spPk sysname = (SELECT TOP 1 COL_NAME(ic.object_id, ic.column_id) FROM sys.indexes i
                         JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
                         WHERE i.object_id = OBJECT_ID('dbo.T_SP') AND i.is_primary_key = 1);
DECLARE @paramFk sysname = (SELECT TOP 1 COL_NAME(fkc.parent_object_id, fkc.parent_column_id) FROM sys.foreign_keys fk
                            JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
                            WHERE fk.parent_object_id = OBJECT_ID('dbo.T_PARAMXSP') AND fk.referenced_object_id = OBJECT_ID('dbo.T_SP'));
-- pa celes te huaj: kolona me te njejtin emer si celesi i T_SP
IF @paramFk IS NULL AND @spPk IS NOT NULL AND COL_LENGTH('dbo.T_PARAMXSP', @spPk) IS NOT NULL SET @paramFk = @spPk;
IF @spPk IS NOT NULL
BEGIN
    SET @s = N'';
    SELECT @s = @s + N' OR ' + QUOTENAME(c.name) + N' LIKE ''%ELEMENTEVESIPASRAJONEVE%'''
    FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.T_SP') AND TYPE_NAME(c.user_type_id) IN ('varchar', 'nvarchar', 'char', 'nchar');
    IF @s <> N''
    BEGIN
        SET @s = N'INSERT #sp SELECT ' + QUOTENAME(@spPk) + N' FROM T_SP WHERE 1 = 0' + @s;
        EXEC (@s);
    END
    -- mbetet nese e perdor ndonje tabele tjeter (pervec parametrave te vet dhe raporteve qe hiqen)
    SET @s = N'';
    SELECT @s = @s + N'DELETE s FROM #sp s WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
         + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = s.id'
         + CASE WHEN fk.parent_object_id = OBJECT_ID('dbo.T_RAPORTI') THEN N' AND x.IDRAPORTI NOT IN (SELECT id FROM #rap)' ELSE N'' END + N');' + CHAR(10)
    FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
    WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_SP') AND fk.parent_object_id <> OBJECT_ID('dbo.T_PARAMXSP');
    EXEC (@s);
END

/* ---------- 3. rreshtat: ruhen ne hequr.<TABELA>_09, pastaj fshihen (femijet para prinderve) ---------- */
CREATE TABLE #hap (nr int IDENTITY PRIMARY KEY, tabela sysname, kushti nvarchar(max));
INSERT #hap (tabela, kushti) VALUES
 ('T_ROLDREJTATRUPI',            'IDRAPORTI IN (SELECT id FROM #rap)'),
 ('T_ROLDREJTATRUPI_HISTORIK',   'IDRAPORTI IN (SELECT id FROM #rap)'),
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
 ('T_RAPORTI',                   'IDRAPORTI IN (SELECT id FROM #rap)');
IF @paramFk IS NOT NULL INSERT #hap (tabela, kushti) VALUES ('T_PARAMXSP', QUOTENAME(@paramFk) + ' IN (SELECT id FROM #sp)');
IF @spPk IS NOT NULL    INSERT #hap (tabela, kushti) VALUES ('T_SP', QUOTENAME(@spPk) + ' IN (SELECT id FROM #sp)');

DECLARE @nr int = 1, @tabela sysname, @kushti nvarchar(max), @n int, @sql nvarchar(max);
WHILE EXISTS (SELECT 1 FROM #hap WHERE nr = @nr)
BEGIN
    SELECT @tabela = tabela, @kushti = kushti FROM #hap WHERE nr = @nr;
    SET @nr += 1;
    IF OBJECT_ID('dbo.' + @tabela, 'U') IS NULL CONTINUE;
    SET @sql = N'SELECT @n = COUNT(*) FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti;
    EXEC sp_executesql @sql, N'@n int OUTPUT', @n OUTPUT;
    IF @n = 0 CONTINUE;
    SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@tabela + '_09') + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
             + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
    EXEC (@sql);
    INSERT hequr.T_RENDI_09 (tabela, rreshta) VALUES (@tabela, @n);
END

/* ---------- 4. objektet (tabela, procedura, view, funksione) ---------- */
CREATE TABLE #obj (id int PRIMARY KEY, emri sysname NOT NULL, lloji varchar(2) NOT NULL);
INSERT #obj (id, emri, lloji)
SELECT o.object_id, o.name, RTRIM(o.type) FROM sys.objects o
WHERE o.schema_id = SCHEMA_ID('dbo') AND o.type IN ('U', 'P', 'V', 'FN', 'IF', 'TF')
  AND (   o.name LIKE 'PRC[_]RAP[_]GIS[_]LISTA%ELEMENTEVESIPASRAJONEVE'
       OR o.name IN (
            -- integrimi me OSHEE
            'prc_Integrim_MerrDokumentaOSHETemp', 'prc_Integrim_ShkruajDokumentaOSHETemp', 'T_GIS_INTEGRIM_SAP_FBHISTORIKU',
            -- tabelat GIS qe 08 i la vetem per raportet e OSHEE
            'T_GIS_App_LAYERS', 'T_GIS_App_LAYERSFOLDER', 'T_GIS_App_LAYERSKOKA', 'T_GIS_App_LAYERSTYPE', 'T_GIS_App_LAYERSXWORKSPACE',
            'T_GIS_App_LIDHJE_OBJEKTE_ARKIVA', 'T_GIS_App_LIDHJE_OBJEKTE_WEB', 'T_GIS_App_WORKSPACE'));

-- nje objekt mbetet nese e perdor (drejtperdrejt, me emer ne tekst apo me celes te huaj) dicka ne skemen dbo qe nuk hiqet;
-- perseritet derisa lista te mos ndryshoje. Objektet ne skemen [hequr] (te hequra nga 05-07) nuk llogariten.
DECLARE @hequr int = 1;
WHILE @hequr > 0
BEGIN
    INSERT hequr.T_MBETEN_09 (emri, arsye)
    SELECT o.emri, N'e perdor ' + MIN(OBJECT_NAME(d.referencing_id))
    FROM #obj o JOIN sys.sql_expression_dependencies d ON (d.referenced_id = o.id OR (d.referenced_id IS NULL AND d.referenced_entity_name = o.emri))
    JOIN sys.objects r ON r.object_id = d.referencing_id
    WHERE r.object_id NOT IN (SELECT id FROM #obj) AND r.type IN ('P', 'V', 'FN', 'IF', 'TF', 'TR') AND r.schema_id = SCHEMA_ID('dbo')
      AND NOT (r.type = 'TR' AND r.parent_object_id IN (SELECT id FROM #obj))
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_09)
    GROUP BY o.emri;
    SET @hequr = @@ROWCOUNT;

    INSERT hequr.T_MBETEN_09 (emri, arsye)
    SELECT o.emri, N'celes i huaj nga ' + MIN(OBJECT_NAME(fk.parent_object_id))
    FROM #obj o JOIN sys.foreign_keys fk ON fk.referenced_object_id = o.id
    WHERE fk.parent_object_id NOT IN (SELECT id FROM #obj) AND OBJECT_SCHEMA_NAME(fk.parent_object_id) = 'dbo'
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_09)
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    INSERT hequr.T_MBETEN_09 (emri, arsye)
    SELECT o.emri, N'emri ne tekstin e ' + MIN(OBJECT_NAME(m.object_id))
    FROM #obj o JOIN #mod m ON m.definition LIKE N'%' + REPLACE(o.emri, '_', '[_]') + N'%'
    WHERE m.object_id NOT IN (SELECT id FROM #obj) AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_09)
      AND (m.parent_id IS NULL OR m.parent_id NOT IN (SELECT id FROM #obj))
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    DELETE FROM #obj WHERE emri IN (SELECT emri FROM hequr.T_MBETEN_09);
END

INSERT hequr.T_OBJEKTE_09 (emri, lloji) SELECT emri, lloji FROM #obj;

DECLARE @emri sysname;
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_09;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

COMMIT;

SELECT tabela, rreshta FROM hequr.T_RENDI_09 ORDER BY nr;
SELECT lloji, emri FROM hequr.T_OBJEKTE_09 ORDER BY lloji, emri;
SELECT emri, arsye FROM hequr.T_MBETEN_09 ORDER BY emri;
