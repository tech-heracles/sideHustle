-- 25 - Cmimi mesatar (prc_T_TRUPIMAGAZINA_llogaritCmimMesatarMeTotalHPD): me pak pune per cdo rresht dalje
--
-- Procedura therritet per cdo rresht dalje/kthimi te nje dokumenti te ri (p.sh. 9 400 here ne importin e 470 shitjeve),
-- ~8 ms secila. Ndryshimet, me te njejtin rezultat:
--  - kur numerohet e gjithe data (@idtrupimagazina = -1, rasti i dokumentit te ri) shuma e rreshtave te dates merret
--    direkt, pa tabelen e perkohshme #TRUPIMAG (ROW_NUMBER, dy indekse, rikompilime); rasti tjeter mbetet si me pare;
--  - shperndarjet e shpenzimeve (#shperndarjet) lexohen vetem kur artikulli ka te tilla (me pare per cdo rresht,
--    duke lexuar te gjitha levizjet e artikullit ne te gjitha magazinat);
--  - pa PRINT-et diagnostikuese (dhjete mesazhe per thirrje drejt aplikacionit).
-- Kerkon skriptin 17. Rikthimi: 25-rollback-cmim-mesatar-shuma-direkte.sql (versioni i skriptit 17)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[prc_T_TRUPIMAGAZINA_llogaritCmimMesatarMeTotalHPD]
(
    @CMIMIMESATAR       FLOAT = NULL OUTPUT,
    @IDARTIKULL         T_FOR_KEY,
    @IDMAG              T_FOR_KEY,
    @DATA               T_DATE,
    @IDRENDITJES        T_AUTO_NUM,
    @Cmimiartnjejte     FLOAT,
    @sasiaartnjejte     FLOAT,
    @idtrupimagazina    T_FOR_KEY
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Err            INT
    DECLARE @CM             FLOAT
    DECLARE @VLEFTA         FLOAT
    DECLARE @SASIA          FLOAT
    DECLARE @vlefta1        FLOAT = 0
    DECLARE @sasia1         FLOAT = 0
    DECLARE @id             T_FOR_KEY
    DECLARE @cmimshperndarje FLOAT

    DECLARE @TIMESTAMPFILLO DATETIME = GETDATE();
    DECLARE @EXECTIME       DATETIME = GETDATE();

    DECLARE @IDNDERMARRJE INT = (
        SELECT IDNDERMARJE
        FROM   T_ARTIKULLI
        WHERE  IDARTIKULLI = @IDARTIKULL AND IDSTATUSDOK = 1
    );

    DECLARE @IDNivelShSh NUMERIC(18, 0) = (
        SELECT TOP 1 IDNIVEL
        FROM   T_NIVELREGJISTRIMI
        WHERE  IDNDERMARRJE = @IDNDERMARRJE AND KODI = 'ShSh'
    );

    -- --------------------------------------------------------
    -- #KONFIGAMB: changed from @table variable to #temp so
    -- the optimizer gets accurate statistics for all 4 joins
    -- --------------------------------------------------------
    CREATE TABLE #KONFIGAMB (IDKONFIGAMBJENTE NUMERIC(18, 0) PRIMARY KEY);

    INSERT INTO #KONFIGAMB
    SELECT DISTINCT kt.IDKONFIGAMBJENTE
    FROM   T_KUSHTEMPLATE kt
           INNER JOIN T_KONFIGAMBJENTE konf
               ON konf.IDKONFIGAMBJENTE = kt.IDKONFIGAMBJENTE
           INNER JOIN T_KUSHTE k
               ON k.IDKUSHT = kt.IDKUSHT AND k.KODI = 'PM'
           INNER JOIN T_ALTERNATIVAKUSHTI ak
               ON ak.IDALTERNATIVEKUSHTI = kt.vlera AND ak.ALTERNATIVA = 'Po'
    WHERE  konf.IDKATDOK = 6
      AND  konf.IDNDERMARJE = @IDNDERMARRJE
      AND  konf.IDSTATUSDOK = 1;


    CREATE TABLE #KONFIGFHNS (IDKONFIGAMBJENTE NUMERIC(18, 0) PRIMARY KEY);

    INSERT INTO #KONFIGFHNS
    SELECT kt.VLERA
    FROM   T_KONFIGAMBJENTE konf
           JOIN T_KUSHTEMPLATE kt ON konf.IDKONFIGAMBJENTE = kt.IDKONFIGAMBJENTE
           JOIN T_KUSHTE k        ON kt.IDKUSHT = k.IDKUSHT
    WHERE  konf.IDKATDOK = 95
      AND  k.KODI = 'ZKDM'
      AND  konf.IDNDERMARJE = @IDNDERMARRJE;


    -- --------------------------------------------------------
    -- @VLEFTA / @SASIA calculation
    -- KEY CHANGES:
    --   1. DISTINCT + JOIN on @KONFIGAMB replaced with EXISTS on #KONFIGAMB
    --      -> eliminates row fan-out and the expensive Sort/Distinct operator
    --   2. LEFT JOIN KTHYER ... IS NULL replaced with NOT EXISTS
    --      -> cleaner anti-join, better plan on large tables
    --   3. LEFT JOIN #KONFIGFHNS ... IS NULL replaced with NOT EXISTS
    -- --------------------------------------------------------
    SELECT @VLEFTA = ISNULL(SUM(tm.Vlefta * tm.SHENJA), 0),
           @SASIA  = ISNULL(SUM(tm.SASIA  * tm.KOEFICENTI * tm.SHENJA), 0)
    FROM   T_TRUPIMAGAZINA tm
           JOIN T_KOKAMAGAZINA tk ON tm.IDKOKAMAGAZINA = tk.IDKOKAMAGAZINA
    WHERE  tk.IDNDERM    = @IDNDERMARRJE
      AND  tm.IDSTATUSDOK = 1
      AND  tm.IDARTIKULL  = @IDARTIKULL
      AND  tm.IDMAG       = @IDMAG
      AND  TM.IDTRUPIMAGAZINA <> @idtrupimagazina
      AND  EXISTS (SELECT 1 FROM #KONFIGAMB ka WHERE ka.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE)
      AND  (
               tm.DATA < CONVERT(datetime, @DATA, 103)
               OR (
                   tm.DATA = CONVERT(datetime, @DATA, 103)
                   AND tk.IDLLOJDOKUMENTIMAGAZINE = 1
                   AND tm.sasia > 0
                   AND NOT EXISTS (
                           SELECT 1 FROM T_TRUPIMAGAZINA kth
                           WHERE kth.IDKTHIMI = tm.IDTRUPIMAGAZINA
                       )
                   AND NOT EXISTS (
                           SELECT 1 FROM #KONFIGFHNS cf
                           WHERE cf.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE
                       )
               )
           )
    -- koka e dokumentit kerkohet per cdo levizje te artikullit ne magazine (disa qindra/mijera), jo te gjitha
    -- kokat e ndermarrjes me hash join (~1500 lexime dhe ~40 ms CPU ne cdo thirrje)
    OPTION (LOOP JOIN);


    -- --------------------------------------------------------
    -- #shperndarjet: unchanged logic, benefits from new indexes
    -- --------------------------------------------------------
    CREATE TABLE #shperndarjet (
        IDTRUPIMAGAZINA NUMERIC(18, 0),
        IDTRUPIGJEN     NUMERIC(18, 0),
        SASIA           INT
    );

    -- shperndarjet e shpenzimeve kerkojne nje rresht te artikullit ne T_SHPERNDARJESHPENZIMETRUPIFATURAT; shumica e
    -- artikujve nuk kane asnje, atehere pyetja (qe lexonte te gjitha levizjet e artikullit) anashkalohet
    IF EXISTS (SELECT 1 FROM T_SHPERNDARJESHPENZIMETRUPIFATURAT WHERE IDARTIKULL = @IDARTIKULL)
    INSERT INTO #shperndarjet
    SELECT tm.IDTRUPIMAGAZINA        AS idtrupifhss,
           TRUPIGJEN.IDTRUPIMAGAZINA AS idTrupiGjen,
           trupigjen.SASIA
    FROM   T_TRUPIMAGAZINA tm
           JOIN T_KOKAMAGAZINA tk
               ON tm.IDKOKAMAGAZINA = tk.IDKOKAMAGAZINA
               AND TK.IDNIVELGJENERUES = @IDNivelShSh
           JOIN T_ARTIKULLI ON TM.IDARTIKULL = T_ARTIKULLI.IDARTIKULLI
           LEFT JOIN T_SHPERNDARJESHPENZIMEKOKA shperndkoka
               ON tk.IDGJENERUES = shperndkoka.IDSHPERNDARJESHPENZ
           LEFT JOIN T_SHPERNDARJESHPENZIMETRUPI shperndtrupi
               ON shperndkoka.IDSHPERNDARJESHPENZ = shperndtrupi.IDSHPERNDARJESHPENZKOKA
           LEFT JOIN T_SHPERNDARJESHPENZIMETRUPIFATURAT SHPERNDTRUPIFATURAT
               ON SHPERNDTRUPIFATURAT.IDSHPERNDARJESHPENZ = shperndtrupi.IDSHPERNDARJESHPENZTRUPI
           LEFT JOIN T_TRUPIMAGAZINA TRUPIGJEN
               ON SHPERNDTRUPIFATURAT.IDTRUPISHITJE = TRUPIGJEN.IDTRUPIMAGAZINA
    WHERE  TM.IDARTIKULL = @IDARTIKULL
      AND  SHPERNDTRUPIFATURAT.VLERA = TM.VLEFTA
      AND  SHPERNDTRUPIFATURAT.IDARTIKULL = tm.IDARTIKULL;


    -- --------------------------------------------------------
    -- #DOKKTHYERA: unchanged logic
    -- --------------------------------------------------------
    CREATE TABLE #DOKKTHYERA (
        IDKOKAMAGAZINA   NUMERIC(18, 0),
        idtrupimagazina  NUMERIC(18, 0),
        KTHIM            BIT,
        idrenditjesprind INT,
        SASIA            FLOAT,
        PRIMARY KEY (IDKOKAMAGAZINA, idtrupimagazina)
    );

    -- Kthimet (IDKTHIMI) jane te rralla (52 nga 1,5 milion rreshta te klientit): kur artikulli s'ka asnje rresht
    -- te kthyer ne kete date, te dy INSERT-et me poshte nuk shtojne asgje, prandaj anashkalohen (~25 ms per thirrje).
    IF EXISTS (SELECT 1
               FROM   T_TRUPIMAGAZINA T
                      JOIN T_TRUPIMAGAZINA KTHIMET ON KTHIMET.IDKTHIMI = T.IDTRUPIMAGAZINA
               WHERE  T.IDARTIKULL = @IDARTIKULL
                 AND  T.DATA       = @DATA)
    BEGIN
        -- INSERT positive exits linked within the date
        INSERT INTO #DOKKTHYERA
        SELECT DISTINCT
               K.IDKOKAMAGAZINA,
               T.IDTRUPIMAGAZINA,
               0            AS KTHIM,
               t.IDRENDITJES,
               T.SASIA
        FROM   T_TRUPIMAGAZINA T
               JOIN T_KOKAMAGAZINA K
                   ON T.IDKOKAMAGAZINA = K.IDKOKAMAGAZINA
               JOIN T_TRUPIMAGAZINA KTHIMET
                   ON T.IDTRUPIMAGAZINA = KTHIMET.IDKTHIMI
               JOIN T_KOKAMAGAZINA KOKAKTHIMET
                   ON KTHIMET.IDKOKAMAGAZINA = KOKAKTHIMET.IDKOKAMAGAZINA
        WHERE  CONVERT(DATE, K.DTDOK)         = CONVERT(DATE, KOKAKTHIMET.DTDOK)
          AND  T.IDARTIKULL = @IDARTIKULL
          AND  T.DATA       = @DATA;


        -- INSERT returns linked within the date
        INSERT INTO #DOKKTHYERA
        SELECT DISTINCT
               dokkryesor.IDKOKAMAGAZINA,
               T.IDTRUPIMAGAZINA,
               1                        AS KTHIM,
               dokkryesor.idrenditjesprind,
               dokkryesor.SASIA
        FROM   T_TRUPIMAGAZINA T
               JOIN #DOKKTHYERA dokkryesor ON t.IDKTHIMI = dokkryesor.idtrupimagazina
        WHERE  T.IDARTIKULL = @IDARTIKULL
          AND  T.DATA       = @DATA;
    END


    -- --------------------------------------------------------
    -- #TRUPIMAG: unchanged logic
    -- Add indexes after INSERT so they don't slow the bulk load
    -- --------------------------------------------------------
    -- E gjithe data (@idtrupimagazina = -1, cdo rresht i ri dokumenti): shuma e rreshtave te dates direkt, pa #TRUPIMAG
    -- (ROW_NUMBER dhe dy indekse vetem per nje shume). DISTINCT mbi IDTRUPIMAGAZINA dhe fushat e llogaritura jep
    -- te njejtat rreshta si DISTINCT tm.* (tm.* varet nga IDTRUPIMAGAZINA).
    -- Shuma mblidhet me te njejtin rend si rreshtat e #TRUPIMAG (NR), qe rezultati me float te jete i njejti.
    IF (@idtrupimagazina = -1)
        BEGIN
            SELECT @vlefta1 = x.v, @sasia1 = x.s
            FROM (
                SELECT SUM(Vlefta * SHENJA) OVER (ORDER BY DATA, RENDITJE_BRENDADOK, CASE WHEN idrenditjesprind IS NULL THEN IDRENDITJES ELSE idrenditjesprind END, KTHIM, IDTRUPIMAGAZINA ROWS UNBOUNDED PRECEDING) AS v,
                       SUM(SASIA * KOEFICENTI * SHENJA) OVER (ORDER BY DATA, RENDITJE_BRENDADOK, CASE WHEN idrenditjesprind IS NULL THEN IDRENDITJES ELSE idrenditjesprind END, KTHIM, IDTRUPIMAGAZINA ROWS UNBOUNDED PRECEDING) AS s,
                       ROW_NUMBER() OVER (ORDER BY DATA DESC, RENDITJE_BRENDADOK DESC, CASE WHEN idrenditjesprind IS NULL THEN IDRENDITJES ELSE idrenditjesprind END DESC, KTHIM DESC, IDTRUPIMAGAZINA DESC) AS rn
                FROM (
        SELECT DISTINCT
               tm.IDTRUPIMAGAZINA, tm.DATA, tm.IDRENDITJES, tm.VLEFTA, tm.SASIA, tm.KOEFICENTI, tm.SHENJA,
               KTHYERA.idrenditjesprind,
               ISNULL(KTHYERA.KTHIM, 0) AS KTHIM,
               CASE
                   WHEN METODEKOSTOJEARTIKULLI = 1
                       THEN
                           CASE
                               WHEN TK.IDLLOJDOKUMENTIMAGAZINE = 1
                                   THEN
                                       CASE
                                           WHEN tm.SASIA = 0 AND shpernd.idTrupiGjen IS NOT NULL
                                               THEN CASE WHEN shpernd.sasia > 0 THEN 1 ELSE 2 END
                                           WHEN tm.SASIA > 0  THEN 1
                                           WHEN tm.SASIA < 0  THEN 2
                                           ELSE 1
                                       END
                               WHEN TK.IDLLOJDOKUMENTIMAGAZINE = 2
                                   THEN
                                       CASE
                                           WHEN tm.SASIA = 0 AND shpernd.idTrupiGjen IS NOT NULL
                                               THEN CASE WHEN shpernd.sasia < 0 THEN 3 ELSE 4 END
                                           WHEN KTHYERA.KTHIM = 1
                                               THEN CASE WHEN KTHYERA.SASIA < 0 THEN 3 ELSE 4 END
                                           WHEN tm.SASIA < 0  THEN 3
                                           WHEN tm.SASIA > 0  THEN 4
                                       END
                           END
                   ELSE 0
               END AS RENDITJE_BRENDADOK
        FROM   T_TRUPIMAGAZINA tm
               JOIN T_KOKAMAGAZINA tk  ON tm.IDKOKAMAGAZINA = tk.IDKOKAMAGAZINA
               JOIN T_ARTIKULLI A      ON A.IDARTIKULLI = TM.IDARTIKULL
               JOIN #KONFIGAMB konfig  ON konfig.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE
               LEFT JOIN #shperndarjet SHPERND  ON SHPERND.IDTRUPIMAGAZINA = TM.IDTRUPIMAGAZINA
               LEFT JOIN #DOKKTHYERA   KTHYERA  ON KTHYERA.idtrupimagazina = TM.IDTRUPIMAGAZINA
               LEFT JOIN T_TRUPIMAGAZINA KTHYER  ON TM.IDTRUPIMAGAZINA = KTHYER.IDKTHIMI
               LEFT JOIN #KONFIGFHNS KONFIGFHNS ON TK.IDKONFIGAMBJENTE = KONFIGFHNS.IDKONFIGAMBJENTE
        WHERE  tm.IDSTATUSDOK = 1
          AND  (
                   tk.IDLLOJDOKUMENTIMAGAZINE = 2
                   OR (tk.IDLLOJDOKUMENTIMAGAZINE = 1 AND tm.sasia <= 0)
                   OR (tk.IDLLOJDOKUMENTIMAGAZINE = 1 AND tm.sasia > 0 AND kthyer.idtrupimagazina IS NOT NULL)
                   OR (tk.IDLLOJDOKUMENTIMAGAZINE = 1 AND tm.sasia <> 0 AND KONFIGFHNS.IDKONFIGAMBJENTE IS NOT NULL)
               )
          AND  tm.IDARTIKULL = @IDARTIKULL
          AND  tm.IDMAG      = CASE WHEN @idmag = 0 THEN tm.idmag ELSE @IDMAG END
          AND  tm.DATA       = @DATA
                ) AS TAB
            ) x
            WHERE x.rn = 1;
        END
    ELSE
        BEGIN
    CREATE TABLE #TRUPIMAG (
            IDTRUPIMAGAZINA          NUMERIC(18, 0),
            IDKOKAMAGAZINA           NUMERIC(18, 0),
            IDLLOJVEPRIMI            NUMERIC(18, 0),
            IDARTIKULL               NUMERIC(18, 0),
            IDNJESIA                 NUMERIC(18, 0),
            SASIA                    FLOAT,
            CMIMI                    FLOAT,
            VLEFTA                   FLOAT,
            KOEFICENTI               FLOAT,
            SHENJA                   INT,
            SASIAPROGRESIVE          FLOAT,
            VLEFTAPROGRESIVE         FLOAT,
            IDMAG                    NUMERIC(18, 0),
            DATA                     DATETIME,
            IDSTATUSDOK              INT,
            IDRENDITJES              INT,
            IDDETAJIMI               NUMERIC(18, 0),
            SASIPROGRESIVEDETAJIMI   FLOAT,
            VLEFTEPROGRESIVEDETAJIMI FLOAT,
            IDDETAJIMI2              NUMERIC(18, 0),
            IDTRUPIREZERVIMI         NUMERIC(18, 0),
            IDTRUPIKONVERTIMFSH      NUMERIC(18, 0),
            IDTRUPIKONVERTIMUSH      NUMERIC(18, 0),
            IDTRUPIKONVERTIMUD       NUMERIC(18, 0),
            IDKTHIMI                 NUMERIC(18, 0),
            IDTRUPISHITJEGJENERIMI   NUMERIC(18, 0),
            SHENIME                  VARCHAR(MAX),
            IDTRUPIMAGAZINAOLD       NUMERIC(18, 0),
            IDARTIKULLSET            NUMERIC(18, 0),
            IDBARKODI                NUMERIC(18, 0),
            SASIAMBETUR              FLOAT,
            IDRENDITJESPRIND         INT,
            KTHIM                    INT,
            RENDITJE_BRENDADOK       INT,
            NR                       INT,
            -- indekset deklarohen ketu (jo me CREATE INDEX pas insert-it): DDL pas krijimit e pengonte ruajtjen e
            -- tabeles se perkohshme dhe rikompilonte pyetjet pasardhese ne cdo thirrje
            INDEX IX_TRUPIMAG_NR (NR),
            INDEX IX_TRUPIMAG_ID (IDTRUPIMAGAZINA, NR)
        );
    
        INSERT INTO #TRUPIMAG
        SELECT *,
               ROW_NUMBER() OVER (
                   ORDER BY DATA,
                            RENDITJE_BRENDADOK,
                            CASE WHEN idrenditjesprind IS NULL THEN IDRENDITJES ELSE idrenditjesprind END,
                            KTHIM,
                            IDTRUPIMAGAZINA
               ) NR
        FROM (
            SELECT DISTINCT
                   tm.*,
                   0                    AS sasiambetur,
                   KTHYERA.idrenditjesprind,
                   ISNULL(KTHYERA.KTHIM, 0) AS KTHIM,
                   CASE
                       WHEN METODEKOSTOJEARTIKULLI = 1
                           THEN
                               CASE
                                   WHEN TK.IDLLOJDOKUMENTIMAGAZINE = 1
                                       THEN
                                           CASE
                                               WHEN tm.SASIA = 0 AND shpernd.idTrupiGjen IS NOT NULL
                                                   THEN CASE WHEN shpernd.sasia > 0 THEN 1 ELSE 2 END
                                               WHEN tm.SASIA > 0  THEN 1
                                               WHEN tm.SASIA < 0  THEN 2
                                               ELSE 1
                                           END
                                   WHEN TK.IDLLOJDOKUMENTIMAGAZINE = 2
                                       THEN
                                           CASE
                                               WHEN tm.SASIA = 0 AND shpernd.idTrupiGjen IS NOT NULL
                                                   THEN CASE WHEN shpernd.sasia < 0 THEN 3 ELSE 4 END
                                               WHEN KTHYERA.KTHIM = 1
                                                   THEN CASE WHEN KTHYERA.SASIA < 0 THEN 3 ELSE 4 END
                                               WHEN tm.SASIA < 0  THEN 3
                                               WHEN tm.SASIA > 0  THEN 4
                                           END
                               END
                       ELSE 0
                   END AS RENDITJE_BRENDADOK
            FROM   T_TRUPIMAGAZINA tm
                   JOIN T_KOKAMAGAZINA tk  ON tm.IDKOKAMAGAZINA = tk.IDKOKAMAGAZINA
                   JOIN T_ARTIKULLI A      ON A.IDARTIKULLI = TM.IDARTIKULL
                   JOIN #KONFIGAMB konfig  ON konfig.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE
                   LEFT JOIN #shperndarjet SHPERND  ON SHPERND.IDTRUPIMAGAZINA = TM.IDTRUPIMAGAZINA
                   LEFT JOIN #DOKKTHYERA   KTHYERA  ON KTHYERA.idtrupimagazina = TM.IDTRUPIMAGAZINA
                   LEFT JOIN T_TRUPIMAGAZINA KTHYER  ON TM.IDTRUPIMAGAZINA = KTHYER.IDKTHIMI
                   LEFT JOIN #KONFIGFHNS KONFIGFHNS ON TK.IDKONFIGAMBJENTE = KONFIGFHNS.IDKONFIGAMBJENTE
            WHERE  tm.IDSTATUSDOK = 1
              AND  (
                       tk.IDLLOJDOKUMENTIMAGAZINE = 2
                       OR (tk.IDLLOJDOKUMENTIMAGAZINE = 1 AND tm.sasia <= 0)
                       OR (tk.IDLLOJDOKUMENTIMAGAZINE = 1 AND tm.sasia > 0 AND kthyer.idtrupimagazina IS NOT NULL)
                       OR (tk.IDLLOJDOKUMENTIMAGAZINE = 1 AND tm.sasia <> 0 AND KONFIGFHNS.IDKONFIGAMBJENTE IS NOT NULL)
                   )
              AND  tm.IDARTIKULL = @IDARTIKULL
              AND  tm.IDMAG      = CASE WHEN @idmag = 0 THEN tm.idmag ELSE @IDMAG END
              AND  tm.DATA       = @DATA
        ) AS TAB
        ORDER BY DATA,
                 RENDITJE_BRENDADOK,
                 CASE WHEN idrenditjesprind IS NULL THEN IDRENDITJES ELSE idrenditjesprind END,
                 KTHIM,
                 IDTRUPIMAGAZINA;
    
    
    
        -- --------------------------------------------------------
        -- Aggregate vlefta1 / sasia1 from #TRUPIMAG
        -- --------------------------------------------------------
            SELECT @vlefta1 = ISNULL(SUM(tm.Vlefta * tm.SHENJA),             0),
                   @sasia1  = ISNULL(SUM(tm.SASIA  * tm.KOEFICENTI * tm.SHENJA), 0)
            FROM   (SELECT NR FROM #TRUPIMAG WHERE IDTRUPIMAGAZINA = @idtrupimagazina) AS NUMRI
                   INNER JOIN #TRUPIMAG tm ON tm.NR < NUMRI.NR;
        END

    SET @VLEFTA = @VLEFTA + @vlefta1;
    SET @SASIA  = @SASIA  + @sasia1;

    -- --------------------------------------------------------
    -- Compute weighted average price
    -- --------------------------------------------------------
    IF (@SASIA <> 0 AND ABS(ROUND(@sasia, 7)) > 0)
        SET @CM = (@VLEFTA + @Cmimiartnjejte * @sasiaartnjejte) / (@SASIA + @sasiaartnjejte);
    ELSE
        SET @CM = 0;

    IF (@Cmimiartnjejte <> 0 AND @SASIA = 0)
        SET @CMIMIMESATAR = @Cmimiartnjejte;
    ELSE
    BEGIN
        IF (@sasia <= 0 OR @cm <= 0)
        BEGIN
            -- Negative stock: fall back to last entry price
            SELECT TOP 1
                   @CM  = CASE
                               WHEN T_ARTIKULLI.NJESI1ARTIKULLI <> TM.IDNJESIA
                                   THEN CMIMI / TM.KOEFICENTI
                               ELSE CMIMI
                           END,
                   @id  = tm.IDTRUPIMAGAZINA
            FROM   T_TRUPIMAGAZINA tm
                   JOIN T_KOKAMAGAZINA tk ON tm.IDKOKAMAGAZINA = tk.IDKOKAMAGAZINA
                   JOIN #KONFIGAMB konfig  ON tk.IDKONFIGAMBJENTE = konfig.IDKONFIGAMBJENTE
                   JOIN T_ARTIKULLI        ON T_ARTIKULLI.IDARTIKULLI = TM.IDARTIKULL
            WHERE  tm.IDSTATUSDOK = 1
              AND  tk.IDLLOJDOKUMENTIMAGAZINE = 1
              AND  tm.IDARTIKULL = @IDARTIKULL
              AND  tm.IDMAG     = @IDMAG
              AND  tm.cmimi    <> 0
              AND  (
                       tm.DATA < CONVERT(datetime, @DATA, 103)
                       OR (
                           tm.DATA = CONVERT(datetime, @DATA, 103)
                           AND (
                               tk.IDLLOJDOKUMENTIMAGAZINE = 1
                               OR (
                                   tk.IDLLOJDOKUMENTIMAGAZINE = 2
                                   AND tm.IDRENDITJES < @IDRENDITJES
                               )
                           )
                       )
                   )
            ORDER BY tm.DATA DESC, tm.IDRENDITJES DESC;

            SELECT TOP 1
                   @cmimshperndarje = ISNULL(
                       tm.VLEFTA / NULLIF(TRUPIGJEN.SASIA, 0),
                       0
                   )
            FROM   T_TRUPIMAGAZINA tm
                   INNER JOIN T_KOKAMAGAZINA tk
                       ON tm.IDKOKAMAGAZINA = tk.IDKOKAMAGAZINA
                   INNER JOIN T_SHPERNDARJESHPENZIMEKOKA shperndkoka
                       ON tk.IDGJENERUES = shperndkoka.IDSHPERNDARJESHPENZ
                       AND TK.IDNIVELGJENERUES = @IDNivelShSh
                   INNER JOIN T_SHPERNDARJESHPENZIMETRUPI shperndtrupi
                       ON shperndkoka.IDSHPERNDARJESHPENZ = shperndtrupi.IDSHPERNDARJESHPENZKOKA
                   INNER JOIN T_SHPERNDARJESHPENZIMETRUPIFATURAT SHPERNDTRUPIFATURAT
                       ON SHPERNDTRUPIFATURAT.IDSHPERNDARJESHPENZ = shperndtrupi.IDSHPERNDARJESHPENZTRUPI
                   INNER JOIN T_TRUPIMAGAZINA TRUPIGJEN
                       ON SHPERNDTRUPIFATURAT.IDTRUPISHITJE = TRUPIGJEN.IDTRUPIMAGAZINA
            WHERE  TRUPIGJEN.IDTRUPIMAGAZINA = @id
              AND  SHPERNDTRUPIFATURAT.VLERA    = TM.VLEFTA
              AND  SHPERNDTRUPIFATURAT.IDARTIKULL = tm.IDARTIKULL
              AND  shperndkoka.IDSTATUSDOK      = 1
            ORDER BY ABS(tm.IDRENDITJES - trupigjen.IDRENDITJES);

            SET @cm = @cm + ISNULL(@cmimshperndarje, 0);

            IF (@CM <= 0)
                SET @CM = 1;
        END

        SET @Err         = @@ERROR;
        SET @CMIMIMESATAR = @CM;

    END

    RETURN @Err;
END
GO
