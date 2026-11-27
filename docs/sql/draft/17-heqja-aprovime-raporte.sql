/*
  DRAFT 17 (2026-11-27): heqje sipas Alkid-it (thread "Ridizajno kryefaqen dhe hiq modulet"):
    1. Aprovime dokumentash: faqja ListeAprovimi.aspx (Aprovimet / Kerkese per aprovim),
    2. Administrim: Grupim ndermarrjesh (LupaGrupNdermarrje.aspx), Politika (PolitikaFjalekalimi.aspx),
       Skema Pune (Shto_SkemaWorkFlow.aspx dhe LupaSkemaWorkFlow.aspx),
    3. Raportet e Arkes: Arketimet Ditore (511), Arketimet Orare (727),
       Ditari Klasik Sipas Autorizimeve (134), Lista e arketimeve dhe anullimeve (503),
    4. Raportet e Bankes: Ditari Klasik Sipas Autorizimeve (135), Ditari Total ne nivel raportues (582),
       Gjendja e permbledhur e bankes ne nivel raportues (311).
  (2026-11-27: raporti "Arketimet" (220) mbetet, Alkid: "Arketimet ktheje". Ne DRAFT u kthye me rollback + rerun.)

  Komponentet dhe raportet fshihen bashke me te gjitha rreshtat qe varen prej tyre (te drejtat, menute, gridat, dizajnet,
  filtrat ...), te gjetura nga celesat e huaj deri ne 4 nivele, plus tabelat pa celes te huaj me kolone IDKOMPONENTE/IDRAPORTI.
  Mbeten: tabelat dhe procedurat e aprovimit/skemes se punes (bosh ne DRAFT, por i therrasin ruajtjet e dokumenteve),
  T_KONFIGURIMEFJALEKALIMI (e lexon hyrja ne program me vlerat ekzistuese), grupet e ndermarrjeve ne T_NDERMARJE.

  Si 05-12: asgje nuk fshihet perfundimisht. Rreshtat ruhen ne hequr.<TABELA>_17_<nr>.
    - kthimi mbrapsht:        17-rollback-heqja-aprovime-raporte.sql
    - fshirja perfundimtare:  17-pastrimi-perfundimtar.sql
  Nese dicka nuk shkon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 17-heqja-aprovime-raporte.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_17') IS NOT NULL
BEGIN
    RAISERROR('Skripti 17 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_17 ekziston).', 16, 1);
    RETURN;
END

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_17 (nr int IDENTITY PRIMARY KEY, niveli int NOT NULL, tabela sysname NOT NULL, kopja sysname NOT NULL, rreshta int NOT NULL);

CREATE TABLE #komp (id numeric(18,0) PRIMARY KEY);
INSERT #komp SELECT IDKOMPON FROM T_KOMPONENTE
WHERE KOMPONEMRI IN ('ListeAprovimi.aspx?status=aprovim', 'ListeAprovimi.aspx?status=kerkese', 'LupaGrupNdermarrje.aspx',
                     'PolitikaFjalekalimi.aspx', 'Shto_SkemaWorkFlow.aspx', 'LupaSkemaWorkFlow.aspx');

CREATE TABLE #rap (id numeric(18,0) PRIMARY KEY);
INSERT #rap SELECT IDRAPORTI FROM T_RAPORTI
WHERE (IDMODULI = 2 AND RAPEMRIREAL IN ('arketimeDitore', 'ArketimetOrare', 'ditariKlasikAutorizimeArka', 'listeArketimeAnullime'))
   OR (IDMODULI = 6 AND RAPEMRIREAL IN ('ditariKlasikAutorizimeBanka', 'ditariTotalBankaRaportues', 'gjendjaPermbledhurBankesNivelRaportues'));

SELECT 'komponente' AS cfare, COUNT(*) AS n FROM #komp UNION ALL SELECT 'raporte', COUNT(*) FROM #rap;

/* ---------- plani: rrenjet + femijet sipas celesave te huaj (deri ne 4 nivele) ---------- */
CREATE TABLE #plan (nr int IDENTITY PRIMARY KEY, niveli int NOT NULL, tabela sysname NOT NULL, kushti nvarchar(max) NOT NULL);
INSERT #plan (niveli, tabela, kushti) VALUES
 (0, 'T_KOMPONENTE', N'IDKOMPON IN (SELECT id FROM #komp)'),
 (0, 'T_RAPORTI',    N'IDRAPORTI IN (SELECT id FROM #rap)');

-- tabelat me kolone IDKOMPONENTE / IDRAPORTI pa celes te huaj (historiku i te drejtave, etj.)
INSERT #plan (niveli, tabela, kushti)
SELECT 1, t.name, QUOTENAME(c.name) + CASE WHEN c.name = 'IDRAPORTI' THEN N' IN (SELECT id FROM #rap)' ELSE N' IN (SELECT id FROM #komp)' END
FROM sys.tables t JOIN sys.columns c ON c.object_id = t.object_id
WHERE t.schema_id = SCHEMA_ID('dbo') AND c.name IN ('IDKOMPONENTE', 'IDRAPORTI')
  AND t.name NOT IN ('T_KOMPONENTE', 'T_RAPORTI')
  AND NOT EXISTS (SELECT 1 FROM sys.foreign_key_columns fkc WHERE fkc.parent_object_id = t.object_id AND fkc.parent_column_id = c.column_id
                  AND fkc.referenced_object_id IN (OBJECT_ID('dbo.T_KOMPONENTE'), OBJECT_ID('dbo.T_RAPORTI')));

DECLARE @niveli int = 0;
WHILE @niveli < 4
BEGIN
    INSERT #plan (niveli, tabela, kushti)
    SELECT @niveli + 1, OBJECT_NAME(fk.parent_object_id),
           QUOTENAME(COL_NAME(fkc.parent_object_id, fkc.parent_column_id)) + N' IN (SELECT ' + QUOTENAME(COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id))
           + N' FROM dbo.' + QUOTENAME(p.tabela) + N' WHERE ' + p.kushti + N')'
    FROM #plan p
    JOIN sys.foreign_keys fk ON fk.referenced_object_id = OBJECT_ID('dbo.' + p.tabela)
    JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
    WHERE p.niveli = @niveli AND fk.parent_object_id <> fk.referenced_object_id AND OBJECT_SCHEMA_NAME(fk.parent_object_id) = 'dbo'
      AND (SELECT COUNT(*) FROM sys.foreign_key_columns x WHERE x.constraint_object_id = fk.object_id) = 1;
    SET @niveli += 1;
END

/* ---------- rreshtat: ruhen ne hequr, pastaj fshihen (niveli me i thelle se pari) ---------- */
DECLARE @nr int, @tabela sysname, @kushti nvarchar(max), @n int, @sql nvarchar(max), @kopja sysname;
DECLARE p CURSOR LOCAL STATIC FOR SELECT nr, niveli, tabela, kushti FROM #plan ORDER BY niveli DESC, nr DESC;
OPEN p; FETCH NEXT FROM p INTO @nr, @niveli, @tabela, @kushti;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'SELECT @n = COUNT(*) FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti;
    EXEC sp_executesql @sql, N'@n int OUTPUT', @n OUTPUT;
    IF @n > 0
    BEGIN
        SET @kopja = @tabela + '_17_' + CAST(@nr AS varchar(10));
        SET @sql = N'SELECT * INTO hequr.' + QUOTENAME(@kopja) + N' FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';'
                 + N'DELETE FROM dbo.' + QUOTENAME(@tabela) + N' WHERE ' + @kushti + N';';
        EXEC (@sql);
        INSERT hequr.T_RENDI_17 (niveli, tabela, kopja, rreshta) VALUES (@niveli, @tabela, @kopja, @n);
    END
    FETCH NEXT FROM p INTO @nr, @niveli, @tabela, @kushti;
END
CLOSE p; DEALLOCATE p;

COMMIT;

SELECT niveli, tabela, kopja, rreshta FROM hequr.T_RENDI_17 ORDER BY nr;
