/*
  DRAFT 12 (2026-11-25): mbetjet e CRM/mobile dhe dy procedurat e licencave (Alkid: "mbaro pune").
    1. Modulet CRM (T_MODULI 24, M_CRM) dhe Mobile (T_MODULI 33, M_MOBILE): 07 dhe 08 lane 6 komponente
       (CRMDefault, CRMRouteAgjenti, CRMAnketa, CRMHistoriku, CRMDetyra dhe listeEntitetesh?lloji=shikoListShitjesh te mobile),
       sepse i perdornin kategorite e dokumenteve (T_KATEGORINIVELDOK 102-104, 110) dhe ambjentet CRM/Mobile (T_AMBJENTE).
       Faqet nuk jane me ne program (git grep: vetem tekste ne resx dhe nje krahasim emri ne clsFunksione).
       Hiqen bashke me gjithcka qe varet vetem prej tyre: te drejtat, menute, licencat, kategorite, nivelet, konfigurimet,
       lidhjet e kontrolleve, gridat, kontrollet e faqeve, ambjentet; pastaj modulet 24 dhe 33 nese nuk i perdor me asgje.
       Nje komponente mbetet (dhe raportohet) nese ndonje rresht jashte ketij grupi i referohet asaj apo dickaje qe varet prej saj
       (p.sh. nje dokument me ate konfigurim, nje perdorues me ate ambjent).
    2. SP_ModifikoLicencen kalon ne [hequr]: nuk e therret programi (git grep), therret prc_T_ROLI_ndryshollojlicence qe nuk ekziston,
       licencat tani menaxhohen nga Manager/Firebase.
    3. SP_FshiVeprime_SipasLicences: hiqen vetem blloqet e tabelave qe nuk ekzistojne me
       (T_USERTRACK_backup, T_IMPORTMAGAZINETRUPI, T_IMPORTMAGAZINEKOKA; shenjat "-----T_X-----" si ne 07/08).
       E vjetra kalon ne [hequr] me te njejtin emer, e rregulluara krijohet ne dbo; teksti i vjeter ruhet edhe ne hequr.T_DEFINICIONE_12.

  Si 05-11: asgje nuk fshihet perfundimisht. Rreshtat ruhen ne hequr.<TABELA>_12, objektet kalojne ne skemen [hequr].
    - kthimi mbrapsht:        12-rollback-mbetjet-crm-mobile-licenca.sql
    - fshirja perfundimtare:  12-pastrimi-perfundimtar.sql (pasi te jete provuar programi)
  Nese dicka nuk shkon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 12-mbetjet-crm-mobile-licenca.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_12') IS NOT NULL
BEGIN
    RAISERROR('Skripti 12 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_12 ekziston).', 16, 1);
    RETURN;
END

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_12 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);
CREATE TABLE hequr.T_OBJEKTE_12 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_MBETEN_12 (emri sysname PRIMARY KEY, arsye nvarchar(400) NOT NULL);   -- vetem per raport
CREATE TABLE hequr.T_DEFINICIONE_12 (emri sysname PRIMARY KEY, definicioni nvarchar(max) NOT NULL);

DECLARE @s nvarchar(max), @sql nvarchar(max), @n int;

/* ---------- 1. modulet dhe komponentet ---------- */
CREATE TABLE #mod (id numeric(18,0) PRIMARY KEY);
INSERT #mod SELECT IDMODULI FROM T_MODULI WHERE (IDMODULI = 24 AND MODULIKODI = 'M_CRM') OR (IDMODULI = 33 AND MODULIKODI = 'M_MOBILE');

CREATE TABLE #komp (id numeric(18,0) PRIMARY KEY);
INSERT #komp SELECT IDKOMPON FROM T_KOMPONENTE WHERE IDMODULI IN (SELECT id FROM #mod);

-- rreshtat qe varen nga komponentet: (tabela, celesi primar, komponentja)
CREATE TABLE #rr (tabela sysname NOT NULL, id numeric(18,0) NOT NULL, komp numeric(18,0) NOT NULL, PRIMARY KEY (tabela, id));
-- tabelat qe trajtohen ketu: referencat nga keto tabela fshihen bashke me grupin, cdo reference tjeter e mban komponenten
DECLARE @trajtuara TABLE (tabela sysname PRIMARY KEY);
INSERT @trajtuara VALUES ('T_KOMPONENTE'), ('T_ROLDREJTATRUPI'), ('T_ROLDREJTATRUPI_HISTORIK'), ('T_MENUPERKOMPONENTE'),
    ('T_KOMPONENTEPERLLOJLICENCE'), ('T_ATRIBUTETRUPI'), ('T_LIDHESPERKONTROLL'), ('T_KONFIGAMBJENTE'), ('T_NIVELREGJISTRIMI'),
    ('T_KATEGORINIVELDOK'), ('T_FILTRAGRIDA'), ('T_GRIDATRUPI'), ('T_GRIDAKOKA'), ('T_KONTROLLE'), ('T_AMBJENTE');

CREATE TABLE #mban (komp numeric(18,0), arsye nvarchar(400));
DECLARE @mbajtur int = 1, @rrotullime int = 0;
WHILE @mbajtur > 0
BEGIN
    SET @rrotullime += 1;
    IF @rrotullime > 20 THROW 50012, 'Kontrolli i komponenteve nuk u stabilizua, skripti ndalet.', 1;
    TRUNCATE TABLE #rr;
    INSERT #rr SELECT 'T_KOMPONENTE', id, id FROM #komp;
    INSERT #rr SELECT 'T_KATEGORINIVELDOK', IDKATDOK, IDKOMPONENTE FROM T_KATEGORINIVELDOK WHERE IDKOMPONENTE IN (SELECT id FROM #komp);
    INSERT #rr SELECT 'T_NIVELREGJISTRIMI', n.IDNIVEL, r.komp FROM T_NIVELREGJISTRIMI n JOIN #rr r ON r.tabela = 'T_KATEGORINIVELDOK' AND r.id = n.IDKATDOK;
    INSERT #rr SELECT DISTINCT 'T_KONFIGAMBJENTE', k.IDKONFIGAMBJENTE, r.komp FROM T_KONFIGAMBJENTE k
        JOIN #rr r ON (r.tabela = 'T_NIVELREGJISTRIMI' AND r.id = k.IDNIVEL) OR (r.tabela = 'T_KATEGORINIVELDOK' AND r.id = k.IDKATDOK);
    INSERT #rr SELECT 'T_GRIDAKOKA', GRIDKOKAID, GRIDKOMPID FROM T_GRIDAKOKA WHERE GRIDKOMPID IN (SELECT id FROM #komp);
    INSERT #rr SELECT 'T_KONTROLLE', IDKONTROLL, IDKOMPONENTE FROM T_KONTROLLE WHERE IDKOMPONENTE IN (SELECT id FROM #komp);
    INSERT #rr SELECT 'T_AMBJENTE', ID, IDKOMPONENTE FROM T_AMBJENTE WHERE IDKOMPONENTE IN (SELECT id FROM #komp);

    TRUNCATE TABLE #mban;
    -- celesat e huaj nga tabela qe nuk trajtohen ketu
    SET @s = N'';
    SELECT @s = @s + N'INSERT #mban SELECT r.komp, N''' + OBJECT_NAME(fk.parent_object_id) + N'.' + COL_NAME(fkc.parent_object_id, fkc.parent_column_id)
         + N' -> ' + OBJECT_NAME(fk.referenced_object_id) + N''' FROM #rr r WHERE r.tabela = ''' + OBJECT_NAME(fk.referenced_object_id)
         + N''' AND EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
         + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = r.id);' + CHAR(10)
    FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
    WHERE OBJECT_SCHEMA_NAME(fk.referenced_object_id) = 'dbo' AND OBJECT_NAME(fk.referenced_object_id) IN (SELECT tabela FROM @trajtuara)
      AND OBJECT_NAME(fk.parent_object_id) NOT IN (SELECT tabela FROM @trajtuara);
    EXEC (@s);
    -- kolonat pa celes te huaj: dokumentet me keto konfigurime, perdoruesit me keto ambjente
    SET @s = N'';
    SELECT @s = @s + N'INSERT #mban SELECT r.komp, N''' + t.name + N'.' + c.name + N''' FROM #rr r WHERE r.tabela = ''T_KONFIGAMBJENTE'''
         + N' AND EXISTS (SELECT 1 FROM dbo.' + QUOTENAME(t.name) + N' x WHERE x.' + QUOTENAME(c.name) + N' = r.id);' + CHAR(10)
    FROM sys.columns c JOIN sys.tables t ON t.object_id = c.object_id
    WHERE t.schema_id = SCHEMA_ID('dbo') AND c.name = 'IDKONFIGAMBJENTE' AND t.name NOT IN (SELECT tabela FROM @trajtuara);
    SELECT @s = @s + N'INSERT #mban SELECT r.komp, N''T_PERDORUESI.' + c.name + N''' FROM #rr r WHERE r.tabela = ''T_AMBJENTE'''
         + N' AND EXISTS (SELECT 1 FROM dbo.T_PERDORUESI x WHERE x.' + QUOTENAME(c.name) + N' = r.id);' + CHAR(10)
    FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.T_PERDORUESI') AND c.name IN ('IDAMBJENT', 'IDAMBJENTMOBILE');
    EXEC (@s);

    INSERT hequr.T_MBETEN_12 (emri, arsye)
    SELECT k.KOMPONEMRI, N'komponentja ' + CAST(m.komp AS nvarchar(20)) + N' mbetet: ' + MIN(m.arsye)
    FROM #mban m JOIN T_KOMPONENTE k ON k.IDKOMPON = m.komp
    WHERE k.KOMPONEMRI NOT IN (SELECT emri FROM hequr.T_MBETEN_12)
    GROUP BY m.komp, k.KOMPONEMRI;
    DELETE FROM #komp WHERE id IN (SELECT komp FROM #mban);
    SET @mbajtur = @@ROWCOUNT;
END

-- moduli hiqet vetem nese nuk mbetet asnje komponente e tij dhe asgje tjeter nuk i referohet (pervec T_AMBJENTIMODULI / T_LISTAKONFIGURIMEVERAP)
DELETE FROM #mod WHERE EXISTS (SELECT 1 FROM T_KOMPONENTE k WHERE k.IDMODULI = #mod.id AND k.IDKOMPON NOT IN (SELECT id FROM #komp));
SET @s = N'';
SELECT @s = @s + N'DELETE m FROM #mod m WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x WHERE x.' + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' = m.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_MODULI')
  AND OBJECT_NAME(fk.parent_object_id) NOT IN ('T_KOMPONENTE', 'T_AMBJENTIMODULI', 'T_LISTAKONFIGURIMEVERAP');
EXEC (@s);
SET @s = N'';
SELECT @s = @s + N'DELETE m FROM #mod m WHERE EXISTS (SELECT 1 FROM ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
     + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) + N' x JOIN dbo.T_AMBJENTIMODULI a ON a.IDAMBJMODULI = x.'
     + QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' WHERE a.IDMODULI = m.id);' + CHAR(10)
FROM sys.foreign_keys fk JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
WHERE fk.referenced_object_id = OBJECT_ID('dbo.T_AMBJENTIMODULI');
EXEC (@s);
INSERT hequr.T_MBETEN_12 (emri, arsye)
SELECT MODULIKODI, N'moduli mbetet: ka ende komponente ose rreshta qe i referohen'
FROM T_MODULI WHERE ((IDMODULI = 24 AND MODULIKODI = 'M_CRM') OR (IDMODULI = 33 AND MODULIKODI = 'M_MOBILE')) AND IDMODULI NOT IN (SELECT id FROM #mod);

/* ---------- 2. rreshtat: ruhen ne hequr.<TABELA>_12, pastaj fshihen (femijet para prinderve) ---------- */
CREATE TABLE #hap (nr int IDENTITY PRIMARY KEY, tabela sysname, kushti nvarchar(max));
INSERT #hap (tabela, kushti) VALUES
 ('T_ROLDREJTATRUPI',           'IDKOMPONENTE IN (SELECT id FROM #komp) OR IDKATEGORIA IN (SELECT id FROM #rr WHERE tabela = ''T_KATEGORINIVELDOK'')'),
 ('T_ROLDREJTATRUPI_HISTORIK',  'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_MENUPERKOMPONENTE',        'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_KOMPONENTEPERLLOJLICENCE', 'IDKOMPONENTE IN (SELECT id FROM #komp)'),
 ('T_ATRIBUTETRUPI',            'IDKONFIGAMBJENTE IN (SELECT id FROM #rr WHERE tabela = ''T_KONFIGAMBJENTE'') OR IDKONTROLL IN (SELECT id FROM #rr WHERE tabela = ''T_KONTROLLE'')'),
 ('T_LIDHESPERKONTROLL',        'IDNIVEL IN (SELECT id FROM #rr WHERE tabela = ''T_NIVELREGJISTRIMI'')'),
 ('T_KONFIGAMBJENTE',           'IDKONFIGAMBJENTE IN (SELECT id FROM #rr WHERE tabela = ''T_KONFIGAMBJENTE'')'),
 ('T_NIVELREGJISTRIMI',         'IDNIVEL IN (SELECT id FROM #rr WHERE tabela = ''T_NIVELREGJISTRIMI'')'),
 ('T_KATEGORINIVELDOK',         'IDKATDOK IN (SELECT id FROM #rr WHERE tabela = ''T_KATEGORINIVELDOK'')'),
 ('T_FILTRAGRIDA',              'GRIDKOKAID IN (SELECT id FROM #rr WHERE tabela = ''T_GRIDAKOKA'')'),
 ('T_GRIDATRUPI',               'GRIDAKOKAID IN (SELECT id FROM #rr WHERE tabela = ''T_GRIDAKOKA'')'),
 ('T_GRIDAKOKA',                'GRIDKOKAID IN (SELECT id FROM #rr WHERE tabela = ''T_GRIDAKOKA'')'),
 ('T_KONTROLLE',                'IDKONTROLL IN (SELECT id FROM #rr WHERE tabela = ''T_KONTROLLE'')'),
 ('T_AMBJENTE',                 'ID IN (SELECT id FROM #rr WHERE tabela = ''T_AMBJENTE'')'),
 ('T_KOMPONENTE',               'IDKOMPON IN (SELECT id FROM #komp)'),
 ('T_AMBJENTIMODULI',           'IDMODULI IN (SELECT id FROM #mod)'),
 ('T_LISTAKONFIGURIMEVERAP',    'IDMODULI IN (SELECT id FROM #mod)'),
 ('T_MODULI',                   'IDMODULI IN (SELECT id FROM #mod)');

DECLARE @nr int = 1, @tabela sysname, @kushti nvarchar(max);
WHILE EXISTS (SELECT 1 FROM #hap WHERE nr = @nr)
BEGIN
    SELECT @tabela = tabela, @kushti = kushti FROM #hap WHERE nr = @nr;
    SET @nr += 1;
    IF OBJECT_ID('dbo.' + @tabela, 'U') IS NULL CONTINUE;
    SET @sql = N'SELECT @n = COUNT(*) FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti;
    EXEC sp_executesql @sql, N'@n int OUTPUT', @n OUTPUT;
    IF @n = 0 CONTINUE;
    SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@tabela + '_12') + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
             + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
    EXEC (@sql);
    INSERT hequr.T_RENDI_12 (tabela, rreshta) VALUES (@tabela, @n);
END

/* ---------- 3. SP_ModifikoLicencen kalon ne [hequr] (nese nuk e perdor asgje tjeter ne dbo) ---------- */
IF OBJECT_ID('dbo.SP_ModifikoLicencen', 'P') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.sql_modules m JOIN sys.objects o ON o.object_id = m.object_id
               WHERE o.schema_id = SCHEMA_ID('dbo') AND o.name <> 'SP_ModifikoLicencen' AND m.definition LIKE N'%SP[_]ModifikoLicencen%')
        INSERT hequr.T_MBETEN_12 (emri, arsye) VALUES ('SP_ModifikoLicencen', N'e perdor nje objekt tjeter ne dbo');
    ELSE
    BEGIN
        ALTER SCHEMA hequr TRANSFER dbo.SP_ModifikoLicencen;
        INSERT hequr.T_OBJEKTE_12 (emri, lloji) VALUES ('SP_ModifikoLicencen', 'P');
    END
END

/* ---------- 4. SP_FshiVeprime_SipasLicences: blloqet e tabelave qe nuk ekzistojne ----------
   Cdo bllok fillon me nje rresht shenje "-----T_<TABELA>-----" dhe mbaron para rreshtit te shenjes tjeter ("-----").
   Blloku hiqet vetem nese tabela e tij nuk ekziston. */
DECLARE @def nvarchar(max) = OBJECT_DEFINITION(OBJECT_ID('dbo.SP_FshiVeprime_SipasLicences')), @def0 nvarchar(max);
SET @def0 = @def;
DECLARE @shenja TABLE (nr int IDENTITY, tabela sysname, modeli nvarchar(200));
INSERT @shenja (tabela, modeli) VALUES
    ('T_USERTRACK_backup',    N'%--T[_]USERTRACK[_]backup--%'),
    ('T_IMPORTMAGAZINETRUPI', N'%--T[_]IMPORTMAGAZINETRUPI--%'),
    ('T_IMPORTMAGAZINEKOKA',  N'%--T[_]IMPORTMAGAZINEKOKA--%');
DECLARE @modeli nvarchar(200), @i int = 1, @p int, @a int, @eol int, @m int, @b int;
SET @rrotullime = 0;
WHILE @def IS NOT NULL AND EXISTS (SELECT 1 FROM @shenja WHERE nr = @i)
BEGIN
    SELECT @modeli = modeli, @tabela = tabela FROM @shenja WHERE nr = @i;
    SET @i += 1;
    IF OBJECT_ID('dbo.' + @tabela) IS NOT NULL CONTINUE;
    WHILE 1 = 1
    BEGIN
        SET @p = PATINDEX(@modeli, @def);
        IF @p = 0 BREAK;
        SET @rrotullime += 1;
        IF @rrotullime > 10 THROW 50012, 'SP_FshiVeprime_SipasLicences: shume blloqe per t''u hequr, skripti ndalet.', 1;
        SET @a = @p - CHARINDEX(CHAR(10), REVERSE(LEFT(@def, @p))) + 1;      -- fillimi i rreshtit te shenjes
        SET @eol = CHARINDEX(CHAR(10), @def, @p);                               -- fundi i rreshtit te shenjes
        SET @m = CHARINDEX('-----', @def, @eol);                                -- shenja e bllokut pasardhes
        IF @eol = 0 OR @m = 0 THROW 50012, 'SP_FshiVeprime_SipasLicences: blloku nuk ka shenje pas tij.', 1;
        SET @b = @m - CHARINDEX(CHAR(10), REVERSE(LEFT(@def, @m))) + 1;      -- fillimi i rreshtit te shenjes pasardhese
        SET @def = STUFF(@def, @a, @b - @a, N'-- (DRAFT 12) u hoq blloku i ' + @tabela + N' (tabela nuk ekziston)' + CHAR(13) + CHAR(10));
    END
END
IF @def <> @def0
BEGIN
    INSERT hequr.T_DEFINICIONE_12 VALUES ('SP_FshiVeprime_SipasLicences', @def0);
    ALTER SCHEMA hequr TRANSFER dbo.SP_FshiVeprime_SipasLicences;   -- e vjetra, me te njejtin emer
    EXEC (@def);                                                     -- e rregulluara (CREATE) ne dbo
    EXEC sp_refreshsqlmodule 'dbo.SP_FshiVeprime_SipasLicences';
END

COMMIT;

SELECT tabela, rreshta FROM hequr.T_RENDI_12 ORDER BY nr;
SELECT lloji, emri FROM hequr.T_OBJEKTE_12 ORDER BY lloji, emri;
SELECT emri AS [ndryshuar (i vjetri ne hequr)] FROM hequr.T_DEFINICIONE_12;
SELECT emri, arsye FROM hequr.T_MBETEN_12 ORDER BY emri;
