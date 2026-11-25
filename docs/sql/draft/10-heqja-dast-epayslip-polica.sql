/*
  DRAFT 10 (2026-11-25): heqja e integrimeve Dast, ePaySlip dhe Polica (Alkid: "8 7 9"):
    - Dast: procedurat prc_Integrim_*Dast (prodhim/magazine; nuk i therriste asgje ne program),
    - ePaySlip: faqja "Posto ne EPaySlip" dhe portali i punonjesve E-PaySlip (komponentet, te drejtat, licencat),
      procedurat e periudhave per ePaySlip, PIN-i i hyrjes (T_PIN_GJENERUAR dhe procedurat e tij), numri i telefonit per PIN,
    - Polica: "Transfero Fatura" ne import (prc_Integrim_lexoFSTNgaTabelaTempPolicash, T_TEMP_IMPORTPOLICA).

  Mbeten: kolonat POSTOEPAYSLIP/POSTOMEMOBONUS/POSTOANNUALDECLARATION te T_PERIUDHAT dhe TRANSFEROFATURA e konfigurimit te importit
  (i ruan programi), formatet e printimit te klientit DAST dhe cdo objekt qe e perdor ende dicka qe nuk hiqet.

  Si 05-09: asgje nuk fshihet perfundimisht. Rreshtat ruhen ne hequr.<TABELA>_10, objektet kalojne ne skemen [hequr].
    - kthimi mbrapsht:        10-rollback-heqja-dast-epayslip-polica.sql
    - fshirja perfundimtare:  10-pastrimi-perfundimtar.sql (pasi te jete provuar programi)
  Nese dicka nuk shkon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 10-heqja-dast-epayslip-polica.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_10') IS NOT NULL
BEGIN
    RAISERROR('Skripti 10 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_10 ekziston).', 16, 1);
    RETURN;
END

-- teksti i procedurave/view-ve merret nje here, jashte transaksionit (shih 07); vetem objektet e skemes dbo
SELECT m.object_id, CASE WHEN o.type = 'TR' THEN o.parent_object_id END AS parent_id, m.definition
INTO #mod FROM sys.sql_modules m JOIN sys.objects o ON o.object_id = m.object_id
WHERE o.schema_id = SCHEMA_ID('dbo');

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_10 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);
CREATE TABLE hequr.T_OBJEKTE_10 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_MBETEN_10 (emri sysname PRIMARY KEY, arsye nvarchar(400) NOT NULL);   -- vetem per raport
CREATE TABLE hequr.T_DEFINICIONE_10 (emri sysname PRIMARY KEY, definicioni nvarchar(max) NOT NULL);

DECLARE @s nvarchar(max);

/* ---------- 1. faqet e ePaySlip (Posto ne EPaySlip dhe portali i punonjesve E-PaySlip/...) ---------- */
CREATE TABLE #komp (id numeric(18,0) PRIMARY KEY);
INSERT #komp SELECT IDKOMPON FROM T_KOMPONENTE WHERE KOMPONEMRI LIKE '%PaySlip%';
DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM T_KATEGORINIVELDOK c WHERE c.IDKOMPONENTE = k.id);
SET @s = N'';
SELECT @s = @s + N'DELETE k FROM #komp k WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = k.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_KOMPONENTE')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_ROLDREJTATRUPI', 'T_ROLDREJTATRUPI_HISTORIK', 'T_MENUPERKOMPONENTE',
        'T_KOMPONENTEPERLLOJLICENCE', 'T_KOMPONENTE');
EXEC (@s);

/* ---------- 2. rreshtat: ruhen ne hequr.<TABELA>_10, pastaj fshihen (femijet para prinderve) ---------- */
CREATE TABLE #hap (nr int IDENTITY PRIMARY KEY, tabela sysname, kushti nvarchar(max));
INSERT #hap (tabela, kushti) VALUES
 ('T_ROLDREJTATRUPI',            'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_ROLDREJTATRUPI_HISTORIK',   'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_MENUPERKOMPONENTE',         'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_KOMPONENTEPERLLOJLICENCE',  'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_KOMPONENTE',                'IDKOMPON IN (SELECT id FROM #komp)');

DECLARE @nr int = 1, @tabela sysname, @kushti nvarchar(max), @n int, @sql nvarchar(max);
WHILE EXISTS (SELECT 1 FROM #hap WHERE nr = @nr)
BEGIN
    SELECT @tabela = tabela, @kushti = kushti FROM #hap WHERE nr = @nr;
    SET @nr += 1;
    IF OBJECT_ID('dbo.' + @tabela, 'U') IS NULL CONTINUE;
    SET @sql = N'SELECT @n = COUNT(*) FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti;
    EXEC sp_executesql @sql, N'@n int OUTPUT', @n OUTPUT;
    IF @n = 0 CONTINUE;
    SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@tabela + '_10') + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
             + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
    EXEC (@sql);
    INSERT hequr.T_RENDI_10 (tabela, rreshta) VALUES (@tabela, @n);
END

/* ---------- 3. objektet (tabela, procedura, view, funksione) ---------- */
CREATE TABLE #obj (id int PRIMARY KEY, emri sysname NOT NULL, lloji varchar(2) NOT NULL);
INSERT #obj (id, emri, lloji)
SELECT o.object_id, o.name, RTRIM(o.type) FROM sys.objects o
WHERE o.schema_id = SCHEMA_ID('dbo') AND o.type IN ('U', 'P', 'V', 'FN', 'IF', 'TF')
  AND (   o.name LIKE 'prc[_]Integrim[_]%Dast'                 -- Dast
       OR o.name LIKE 'prc[_]T[_]PERIUDHAT[_]selSipasVitiPostoEpaySlip%' OR o.name LIKE '%PIN[_]GJENERUAR%'   -- ePaySlip
       OR o.name IN ('prc_T_PUNONJES_merrNrTelSipasUsername',
                     'prc_Integrim_lexoFSTNgaTabelaTempPolicash', 'T_TEMP_IMPORTPOLICA'));   -- Polica

-- nje objekt mbetet nese e perdor (drejtperdrejt, me emer ne tekst apo me celes te huaj) dicka ne skemen dbo qe nuk hiqet;
-- perseritet derisa lista te mos ndryshoje. Objektet ne skemen [hequr] (te hequra nga 05-07) nuk llogariten.
DECLARE @hequr int = 1;
WHILE @hequr > 0
BEGIN
    INSERT hequr.T_MBETEN_10 (emri, arsye)
    SELECT o.emri, N'e perdor ' + MIN(OBJECT_NAME(d.referencing_id))
    FROM #obj o JOIN sys.sql_expression_dependencies d ON (d.referenced_id = o.id OR (d.referenced_id IS NULL AND d.referenced_entity_name = o.emri))
    JOIN sys.objects r ON r.object_id = d.referencing_id
    WHERE r.object_id NOT IN (SELECT id FROM #obj) AND r.type IN ('P', 'V', 'FN', 'IF', 'TF', 'TR') AND r.schema_id = SCHEMA_ID('dbo')
      AND NOT (r.type = 'TR' AND r.parent_object_id IN (SELECT id FROM #obj))
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_10)
    GROUP BY o.emri;
    SET @hequr = @@ROWCOUNT;

    INSERT hequr.T_MBETEN_10 (emri, arsye)
    SELECT o.emri, N'celes i huaj nga ' + MIN(OBJECT_NAME(fk.parent_object_id))
    FROM #obj o JOIN sys.foreign_keys fk ON fk.referenced_object_id = o.id
    WHERE fk.parent_object_id NOT IN (SELECT id FROM #obj) AND OBJECT_SCHEMA_NAME(fk.parent_object_id) = 'dbo'
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_10)
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    INSERT hequr.T_MBETEN_10 (emri, arsye)
    SELECT o.emri, N'emri ne tekstin e ' + MIN(OBJECT_NAME(m.object_id))
    FROM #obj o JOIN #mod m ON m.definition LIKE N'%' + REPLACE(o.emri, '_', '[_]') + N'%'
    WHERE m.object_id NOT IN (SELECT id FROM #obj) AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_10)
      AND (m.parent_id IS NULL OR m.parent_id NOT IN (SELECT id FROM #obj))
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    DELETE FROM #obj WHERE emri IN (SELECT emri FROM hequr.T_MBETEN_10);
END

INSERT hequr.T_OBJEKTE_10 (emri, lloji) SELECT emri, lloji FROM #obj;

DECLARE @emri sysname;
DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_10;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

COMMIT;

SELECT tabela, rreshta FROM hequr.T_RENDI_10 ORDER BY nr;
SELECT lloji, emri FROM hequr.T_OBJEKTE_10 ORDER BY lloji, emri;
SELECT emri, arsye FROM hequr.T_MBETEN_10 ORDER BY emri;
