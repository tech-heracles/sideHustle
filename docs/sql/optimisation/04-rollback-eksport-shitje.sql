-- Rikthen versionin e meparshem te procedures (para 04-eksport-shitje.sql).
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[prc_T_KOKASHITJE_merrShitjeBlerjeSipasNdermarrjePerEksport]
(
    @IDNDERMARJE       VARCHAR(100),
    @IDPERDORUES       VARCHAR(100),
    @IDKATDOK          VARCHAR(100),
    @IDNDERVITI        VARCHAR(100),
    @LLOJI             INT,
    @EMERTABKOKA       VARCHAR(100),
    @EMERFUSHEID       VARCHAR(100),
    @IDPEREKSPORT      VARCHAR(MAX),
    @MERRDOKTEMODIFIKUAR BIT,
    @MERRDOKTEFSHIRE   BIT,
    @HIQRRESHTAKOMISIONI BIT,
    @SERIALEUNIKE      BIT,
    @ARTIKUJSET        BIT,
    @FILTERSTRING      VARCHAR(MAX),
    @IMPORTAUTOMATIK   BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Err   INT
    DECLARE @SQL   VARCHAR(MAX)

    -- --------------------------------------------------------
    -- 0.  Resolve @FILTERSTRING
    -- --------------------------------------------------------
    IF @IMPORTAUTOMATIK = 1
        SET @FILTERSTRING = (
            SELECT ISNULL(filterEksp.FILTERPERDATASET, '1 = 1')
            FROM   T_KOKAFILTRAEXPORTI filterEksp
                   INNER JOIN T_KONFIGEXPORTI konfigEksp
                           ON konfigEksp.FILTRI = filterEksp.ID
            WHERE  konfigEksp.EMERTABELEKOKA = @EMERTABKOKA
              AND  konfigEksp.KATEGORIA      = @IDKATDOK
        )
    ELSE
        IF @FILTERSTRING = '' SET @FILTERSTRING = '1 = 1'

    IF @HIQRRESHTAKOMISIONI = 1
    BEGIN
        SET @SERIALEUNIKE = 0
        SET @ARTIKUJSET   = 0
    END

    -- --------------------------------------------------------
    -- 1.  Pre-materialise NKAKNKA config IDs  (unchanged)
    -- --------------------------------------------------------
    IF OBJECT_ID('tempdb..#KONFIGNKAKNKA') IS NOT NULL
        DROP TABLE #KONFIGNKAKNKA

    CREATE TABLE #KONFIGNKAKNKA (idkonfig INT PRIMARY KEY)

    INSERT INTO #KONFIGNKAKNKA
    SELECT DISTINCT t_konfigambjente.idkonfigambjente
    FROM   t_konfigambjente
           INNER JOIN t_kushtemplate   ON t_konfigambjente.idkonfigambjente = t_kushtemplate.idkonfigambjente
           INNER JOIN t_kushte         ON t_kushte.idkusht = t_kushtemplate.idkusht
           INNER JOIN t_alternativakushti
                   ON t_alternativakushti.IDALTERNATIVEKUSHTI = t_kushtemplate.vlera
                  AND t_alternativakushti.idkushti            = t_kushte.idkusht
    WHERE  t_kushte.kodi       = 'NKAKNKA'
      AND  idndermarje         = @IDNDERMARJE
      AND  alternativa         = 'Po'
      AND  IDSTATUSDOK         = 1

    -- --------------------------------------------------------
    -- 2.  Pre-materialise authorised configs for this user
    --     KEY FIX: replaces OUTER APPLY per row with one scan
    -- --------------------------------------------------------
    IF OBJECT_ID('tempdb..#AUTHORIZED') IS NOT NULL
        DROP TABLE #AUTHORIZED

    CREATE TABLE #AUTHORIZED (idkonfigambjente INT PRIMARY KEY)

    -- We call the function once per distinct IDKONFIGAMBJENTE
    -- that exists for this company/year/category combination,
    -- rather than once per document row.
    INSERT INTO #AUTHORIZED (idkonfigambjente)
    SELECT DISTINCT ka.IDKONFIGAMBJENTE
    FROM   T_KONFIGAMBJENTE ka
    WHERE  EXISTS (
               SELECT 1
               FROM   eshteIAutorizuar(ka.IDKONFIGAMBJENTE, 'KonfigurimDok', @IDPERDORUES)
               WHERE  AUTORIZUAR = 1
           )

    -- --------------------------------------------------------
    -- 3.  Build the main dynamic SQL
    --     The expensive bits (correlated subqueries, kf1) are
    --     now fixed; only truly dynamic parts remain dynamic.
    -- --------------------------------------------------------

    DECLARE @COLS_COMMON VARCHAR(MAX)
    DECLARE @JOINS_COMMON VARCHAR(MAX)
    DECLARE @JOINS_SERIAL VARCHAR(MAX)
    DECLARE @WHERE_COMMON VARCHAR(MAX)
    DECLARE @FILTER_ID    VARCHAR(MAX) = ''
    DECLARE @FILTER_EXCL  VARCHAR(MAX) = ''
    DECLARE @KOMISIONI_FILTER VARCHAR(MAX) = ''

    IF @HIQRRESHTAKOMISIONI <> 0
        SET @KOMISIONI_FILTER = ' AND T_TRUPISHITJE.LLOGARITKOMISION = 0 '

    IF @IDPEREKSPORT <> ''
        SET @FILTER_ID = ' AND T_KOKASHITJE.IDSHITJEKOKA IN (' + @IDPEREKSPORT + ') '

    -- Column list shared by main query + komisioni branch
    SET @COLS_COMMON = '
        T_KOKASHITJE.IDSHITJEKOKA                                                   AS [Id Shitje Koka],
        T_KOKASHITJE.DTMBARIMI                                                       AS [Date Mbarimi],
        T_KOKASHITJE.DTFILLIMI                                                       AS [Date Fillimi],
        N.KODI                                                                       AS Nenkategoria,
        T_KONFIGAMBJENTE.KODKONFIGAMBJENTE                                           AS [Lloj Dokumenti],
        KF.KODKLIENTFURNITOR                                                         AS [Klient/Furnitori],
        T_KOKASHITJE.NRPROJEKT                                                       AS [Numer Projekti],
        T_KOKASHITJE.NRDOK                                                           AS [Nr Dokumenti],
        T_KOKASHITJE.NRSERIAL                                                        AS [Numer Serial],
        T_KOKASHITJE.DTDOK                                                           AS [Date Dokumenti],
        T_KOKASHITJE.DTMODIFIKIMI                                                    AS [Date Modifikimi],
        T_KOKASHITJE.DTMATURIMI                                                      AS [Date Maturimi],
        T_MONEDHA.MONEDHAKOD                                                         AS Monedha,
        T_KOKASHITJE.KURSI                                                           AS Kursi,
        AGJ1.KODIAGJENTSHITJE                                                        AS [Agjent Shitje 1],
        CASE T_KOKASHITJE.IDMENYREPAGESE
            WHEN -1 THEN ''''   WHEN 0 THEN ''Me mirebesim''
            WHEN  4 THEN ''Pagese''   WHEN 5 THEN ''Pagese Automatike''
            WHEN  7 THEN ''Me parapagim''   WHEN 8 THEN ''Arke''
            WHEN  9 THEN ''Karte krediti''
        END                                                                          AS [Menyre Pagese],
        CASE T_KOKASHITJE.ZBRITJENEVLERE
            WHEN 0 THEN T_KOKASHITJE.PERQINDJEZBRITJE
            WHEN 1 THEN T_KOKASHITJE.ZBRITJE
        END                                                                          AS [Total Zbritje],
        T_KOKASHITJE.DTREGJISTRIMI                                                   AS [Date Regjistrimi],
        T_KOKASHITJE.ADRESAFATURIMIT                                                 AS [Adresa e Faturimit],
        T_KOKASHITJE.ADRESADERGIMIT                                                  AS [Adresa e Dergimit],
        T_KOKASHITJE.PERSHKRIMI                                                      AS Pershkrimi,
        CASE WHEN T_KOKASHITJE.DOGANA = 1 THEN ''po'' ELSE ''jo'' END               AS Dogana,
        T_DEGEADMINISTRATIVE.KODI                                                    AS [Dege Administrative],
        T_PIKESHITJEFURNIZIMI.KODI                                                   AS [Pike Shitje/Furnizimi],
        T_KOKASHITJE.AFATIKOHOR                                                      AS [Afati Kohor],
        T_KOKASHITJE.PERQINDJEAGJENTI                                                AS [Perqindje Agjenti 1],
        GR1.KODI                                                                     AS [Grupim Dokumenti 1],
        GR2.KODI                                                                     AS [Grupim Dokumenti 2],
        GR3.KODI                                                                     AS [Grupim Dokumenti 3],
        T_KOKASHITJE.EMERKLIENTI                                                     AS [Emer Klienti],
        T_KOKASHITJE.KONTAKTI                                                        AS Kontakti,
        T_KOKASHITJE.KASE                                                            AS Kase,
        CASE WHEN T_KOKASHITJE.KUPON = 1 THEN ''po'' ELSE ''jo'' END                AS Kupon,
        autom.NRSHASIE                                                               AS Automjeti,
        T_KOKASHITJE.KILOMETRAAUTO                                                   AS Kilometra,
        AGJ2.KODIAGJENTSHITJE                                                        AS [Agjent Shitje 2],
        T_KOKASHITJE.PERQINDJEAGJENTI2                                               AS [Perqindje Agjenti 2],
        AGJ3.KODIAGJENTSHITJE                                                        AS [Agjent Shitje 3],
        T_KOKASHITJE.PERQINDJEAGJENTI3                                               AS [Perqindje Agjenti 3],
        T_KOKASHITJE.MARRESI                                                         AS Marresi,
        T_TRANSPORTUES.EMERTIMI                                                      AS Transportues,
        T_KOKASHITJE.SHPENZJOTEZBRITSHME                                             AS [Shpenzime Jo Te Zbritshme],
        T_PERDORUESI.PERDORUESUSERNAME                                               AS Krijuesi,
        T_KOKASHITJE.IDNIVELGJENERUES                                                AS [Id Nivel Gjenerues],
        T_KOKASHITJE.IDDOKNGA                                                        AS [Id Dok Nga],
        T_KOKASHITJE.DTTRANSPORTIMI                                                  AS [Dt Transporti],
        T_KOKASHITJE.CASH                                                            AS Cash,
        T_KUSHTEDERGIMI.KODIKUSHTDERGIMI                                             AS [Kusht Dergimi],
        T_KUSHTPAGESEKOKA.KODIKUSHTPAGESE                                            AS [Kusht Pagese],
        T_KOKASHITJE.IDKONFIGGJENERUES                                               AS [Id Konfig Gjenerues],
        T_KOKASHITJE.IDGJENERUES                                                     AS [Id Gjenerues],
        T_KOKASHITJE.TOTALI                                                          AS Totali,
        T_KOKASHITJE.TVSH                                                            AS [Tvsh Koka],
        T_KOKASHITJE.IDSTATUSDOK                                                     AS [Id Status Dok],
        T_KOKASHITJE.TOTALIMETVSHMEZBRITJE                                           AS [Totali Me Tvsh Me Zbritje],
        T_KOKASHITJE.DTKRIJIMI                                                       AS [Dt Krijimi],
        T_BANKA.KODIBANKA                                                            AS [Arka],
        T_KOKASHITJE.FATUREPERMBLEDHESE                                              AS [Fature Permbledhese],
        T_NDERMARJE.NDERMARJEKODI                                                    AS [Kod Ndermarrje],
        T_KOKASHITJE.DTFATURE                                                        AS [Dt Fature],
        autom.TARGA                                                                  AS Targa,
        kf1.KODKLIENTFURNITOR                                                        AS [Klient/Furnitori vartes],
        T_KOKASHITJE.Targa                                                           AS [Targa e shoferit],
        T_KOKASHITJE.Shoferi                                                         AS Shoferi,
        T_KARTA.KODI                                                                 AS Karta,
        T_KATEGORISHPENZIMI.KODI                                                     AS [Kategori Shpenzimi],
        T_TRUPISHITJE.IDSHITJETRUPI                                                  AS IdRreshtiShitje,
        CASE T_TRUPISHITJE.IDLLOJVEPRIMI
            WHEN 1 THEN ''Artikull'' WHEN 2 THEN ''Makro'' WHEN 3 THEN ''Llogari''
        END                                                                          AS Lloji,
        T_TRUPISHITJE.KODI                                                           AS Kodi,
        T_TRUPISHITJE.PERSHKRIMI                                                     AS [Pershkrim Trupi],
        T_NJESIARTIKULLI.KODNJESIA                                                   AS Njesia,
        T_TRUPISHITJE.SASIA                                                          AS Sasia,
        T_TRUPISHITJE.CMIMI                                                          AS Cmimi,
        T_TRUPISHITJE.ZBRITJE                                                        AS [Zbritje Analitike],
        T_TRUPISHITJE.ZBRITJEVlere                                                   AS [Zbritje vlere],
        CASE WHEN T_TRUPISHITJE.LLOJZBRITJE = 2 THEN ''Vlere'' ELSE ''Perqindje'' END AS [Lloj zbritje],
        T_TRUPISHITJE.CMIMI * (1 + CASE WHEN T_TRUPISHITJE.TVSH = 0 THEN 0
                                         ELSE T_TAKSAT.normaperqindje END / 100)     AS [Cmimi me Tvsh],
        T_TRUPISHITJE.VLEFTAPATVSH                                                   AS [Vlefta pa TVSH],
        CASE WHEN T_TRUPISHITJE.TVSH = 0 OR T_TRUPISHITJE.TVSH IS NULL
             THEN ''Pa TVSH'' ELSE T_TAKSAT.KODI END                                 AS TVSH,
        T_TRUPISHITJE.VLEFTAMETVSH                                                   AS [Vlefta me TVSH],
        T_NJESIADMINISTRATIVE.KODI                                                   AS Magazina,
        T_TRUPISHITJE.GJERESI                                                        AS Gjeresi,
        T_TRUPISHITJE.GJATESI                                                        AS Gjatesi,
        T_TRUPISHITJE.SASIPERMASE                                                    AS [Sasi Permase],
        DETAJIM1.KODDETAJIMARTIKULLI                                                 AS [Detajimi 1],
        DETAJIM2.KODDETAJIMARTIKULLI                                                 AS [Detajimi 2],
        T_TRUPISHITJE.SHENIME                                                        AS Shenime,
        LLOGARISHPENZ.NRLLOGARI                                                      AS [Llogari Shpenzimi],
        T_TRUPISHITJE.DTFILLIMI                                                      AS [Date Fillimi Trupi],
        T_TRUPISHITJE.DTMBARIMI                                                      AS [Date Mbarimi Trupi],
        T_TRUPISHITJE.IDTRUPIKONVERTIMI                                              AS [Id Trupi Konvertimi],
        T_TRUPISHITJE.SASIREZ                                                        AS [Sasi Rezervimi],
        ''''                                                                          AS Seriali,
        CASE WHEN idkonf_merrSP.idkonfig IS NULL THEN T_KODBARI.PERSHKRIMI
             ELSE ISNULL(T_KODBARI.PERSHKRIMI, T_KODBARIPAREARTIKULLIT.PERSHKRIMI)
        END                                                                          AS Kodbari,
        kf.EMERTIMIKF                                                                AS [Emertimi],
        GR1.PERSHKRIMI                                                               AS [Pershkrim Gr. Dok. 1],
        GR2.PERSHKRIMI                                                               AS [Pershkrim Gr. Dok. 2],
        GR3.PERSHKRIMI                                                               AS [Pershkrim Gr. Dok. 3],
        T_KOKASHITJE.KOORDINATA.STAsText()                                           AS Koordinata,
        T_KOKASHITJE.NIPTK                                                           AS NiptK,
        T_KOKASHITJE.qytetik                                                         AS QytetiK,
        T_KOKASHITJE.SHENIME2                                                        AS [Shenime 2],
        T_KOKASHITJE.KARTAPAPAGESE                                                   AS [Karta pa pagese],
        CASE WHEN T_KOKASHITJE.IDDOKTRANSFERIMNGA IS NULL
                  OR T_KOKASHITJE.IDDOKTRANSFERIMNGA = 0
             THEN T_KOKASHITJE.IDSHITJEKOKA
             ELSE T_KOKASHITJE.IDDOKTRANSFERIMNGA
        END                                                                          AS [ID Dok Transferim Nga],
        CASE WHEN T_TRUPISHITJE.IDTRUPITRANSFERIMNGA IS NULL
                  OR T_TRUPISHITJE.IDTRUPITRANSFERIMNGA = 0
             THEN T_TRUPISHITJE.IDSHITJETRUPI
             ELSE T_TRUPISHITJE.IDTRUPITRANSFERIMNGA
        END                                                                          AS [ID Trupi Transferim Nga]'

    -- Serial columns (conditional)
    DECLARE @COLS_SERIAL VARCHAR(MAX)
    IF @SERIALEUNIKE = 1
        SET @COLS_SERIAL = ',slm.SERIALI_KRYESOR AS [Seriali Unik Kryesor], slm.SERIALI_DYTESOR AS [Seriali Unik Dytesor]'
    ELSE
        SET @COLS_SERIAL = ','''' AS [Seriali Unik Kryesor], '''' AS [Seriali Unik Dytesor]'

    DECLARE @COLS_TAIL VARCHAR(MAX) = '
        ,'''' AS [Artikulli Set],
        CASE T_KOKASHITJE.ZBRITJENEVLERE
            WHEN -1 THEN ''''  WHEN 0 THEN ''Perqindje''  WHEN 1 THEN ''Vlere''
        END                                                                          AS [Lloj Zbritje Totale],
        T_TRUPISHITJE.SHENIME2                                                       AS [Shenime 2 Trupi],
        CASE WHEN T_KOKASHITJE.IDLLOJMARREVESHJE IN (0,-1) THEN ''''
             WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 1 THEN ''Retention''
             WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 2 THEN ''Acquisition''
             WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 3 THEN ''EBU Benefit''
             WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 4 THEN ''Agreement Tenure Reward''
        END                                                                          AS [Lloji i marreveshjes],
        T_KOKASHITJE.IDMARREVESHJE                                                   AS [ID e marreveshjes],
        T_KOKASHITJE.KERKUARNGA                                                      AS KerkuarNga,
        T_KOKASHITJE.DATEKERKESE                                                     AS DateKerkese,
        kf.KodIntegrimi                                                              AS [Kod Klienti Integrimi],
        T_KOKASHITJE.IIC,
        T_KOKASHITJE.NIVF,
        -- FIX: was correlated subquery per row, now LEFT JOIN (see joins below)
        op.KODI                                                                      AS Operatori,
        T_KOKASHITJE.EINVOICE,
        T_KOKASHITJE.EIC,
        T_KOKASHITJE.EinStatus,
        -- FIX: was correlated subquery per row, now LEFT JOIN
        proc_.KODI                                                                   AS Procesi,
        -- FIX: was correlated subquery per row, now LEFT JOIN
        tei.KODI                                                                     AS [E-invoice Type],
        T_KOKASHITJE.NIVFKTHIM                                                       AS [NIVF kthim],
        T_KOKASHITJE.TIPIIVETEFATURIMIT                                              AS [Tipi i vetefaturimit]'

    -- --------------------------------------------------------
    -- Common JOIN block for main query
    -- --------------------------------------------------------
    SET @JOINS_COMMON = '
    FROM  T_KOKASHITJE
          INNER JOIN T_TRUPISHITJE
                  ON T_TRUPISHITJE.IDSHITJEKOKA    = T_KOKASHITJE.IDSHITJEKOKA
          INNER JOIN T_NIVELREGJISTRIMI N
                  ON N.IDNIVEL                      = T_KOKASHITJE.IDNIVEL
          INNER JOIN T_KONFIGAMBJENTE
                  ON T_KONFIGAMBJENTE.IDKONFIGAMBJENTE = T_KOKASHITJE.IDKONFIGAMBJENTE
          -- FIX: authorisation via pre-built #AUTHORIZED (not per-row APPLY)
          INNER JOIN #AUTHORIZED auth_
                  ON auth_.idkonfigambjente         = T_KOKASHITJE.IDKONFIGAMBJENTE
          LEFT  JOIN #KONFIGNKAKNKA idkonf_merrSP
                  ON idkonf_merrSP.idkonfig         = T_KOKASHITJE.IDKONFIGAMBJENTE
          LEFT  JOIN T_KLIENTFURNITOR kf
                  ON kf.IDKLIENTFURNITOR            = T_KOKASHITJE.IDKLIENTFURNITOR
          -- FIX: kf1 scoped to current company only, not full table scan
          LEFT  JOIN (
                    SELECT  kv.IDSHITJEKOKA,
                            STUFF((
                                SELECT DISTINCT '', '' + kf2.KODKLIENTFURNITOR
                                FROM   T_KOKASHITJE_KLIENTVARTES kv2
                                       INNER JOIN T_KLIENTFURNITOR kf2
                                               ON kf2.IDKLIENTFURNITOR = kv2.IDKLIENTFURNITOR
                                WHERE  kv2.IDSHITJEKOKA = kv.IDSHITJEKOKA
                                FOR XML PATH(''''), TYPE
                            ).value(''.'', ''NVARCHAR(MAX)''), 1, 1, '''') AS KODKLIENTFURNITOR
                    FROM    T_KOKASHITJE_KLIENTVARTES kv
                    WHERE   EXISTS (
                                SELECT 1 FROM T_KOKASHITJE k
                                WHERE  k.IDSHITJEKOKA = kv.IDSHITJEKOKA
                                  AND  k.IDNDERM      = ' + @IDNDERMARJE + '
                                  AND  k.IDNDERMVIT   = ' + @IDNDERVITI + '
                            )
                    GROUP BY kv.IDSHITJEKOKA
                ) kf1  ON kf1.IDSHITJEKOKA = T_KOKASHITJE.IDSHITJEKOKA
          INNER JOIN T_LLOGARI
                  ON T_LLOGARI.IDLLOGARI             = kf.IDLLOGARI
          INNER JOIN T_MONEDHA
                  ON T_MONEDHA.IDMONEDHA             = T_LLOGARI.MONEDHA
          LEFT  JOIN T_ARTIKULLI
                  ON T_ARTIKULLI.IDARTIKULLI         = T_TRUPISHITJE.IDKODI
          LEFT  JOIN T_KODBARI
                  ON T_KODBARI.IDKODBARI             = T_TRUPISHITJE.IDBARKODI
          LEFT  JOIN T_AUTOMJETE autom
                  ON autom.IDAUTOMJETI               = T_KOKASHITJE.IDAUTOMJETI
          LEFT  JOIN T_AGJENTSHITJE AGJ1
                  ON AGJ1.IDAGJENTSHITJE             = T_KOKASHITJE.IDAGJENT
          LEFT  JOIN T_AGJENTSHITJE AGJ2
                  ON AGJ2.IDAGJENTSHITJE             = T_KOKASHITJE.IDAGJENTI2
          LEFT  JOIN T_AGJENTSHITJE AGJ3
                  ON AGJ3.IDAGJENTSHITJE             = T_KOKASHITJE.IDAGJENTI3
          LEFT  JOIN T_DEGEADMINISTRATIVE
                  ON T_DEGEADMINISTRATIVE.IDDEGEADMINISTRATIVE = T_KOKASHITJE.IDDEGEADMINISTRATIVE
          LEFT  JOIN T_PIKESHITJEFURNIZIMI
                  ON T_PIKESHITJEFURNIZIMI.IDPIKESHITJEFURNIZIMI = T_KOKASHITJE.IDPIKESHITJEFURNIZIMI
          LEFT  JOIN T_GRUPIMDOKUMENTIKOKA GR1 ON GR1.IDGRUPIMKOKA = T_KOKASHITJE.IDGRUP1
          LEFT  JOIN T_GRUPIMDOKUMENTIKOKA GR2 ON GR2.IDGRUPIMKOKA = T_KOKASHITJE.IDGRUP2
          LEFT  JOIN T_GRUPIMDOKUMENTIKOKA GR3 ON GR3.IDGRUPIMKOKA = T_KOKASHITJE.IDGRUP3
          LEFT  JOIN T_TRANSPORTUES
                  ON T_TRANSPORTUES.IDTRANSPORTUES   = T_KOKASHITJE.IDTRANSPORTUES
          LEFT  JOIN T_NJESIARTIKULLI
                  ON T_NJESIARTIKULLI.IDNJESIA        = T_TRUPISHITJE.IDNJESIA
          LEFT  JOIN T_TAKSAT
                  ON T_TAKSAT.IDTAKSA                 = T_TRUPISHITJE.TVSH
          LEFT  JOIN T_NJESIADMINISTRATIVE
                  ON T_NJESIADMINISTRATIVE.IDNJESIADM = T_TRUPISHITJE.IDMAGAZINA
          LEFT  JOIN T_DETAJIMARTIKULLI DETAJIM1
                  ON DETAJIM1.IDDETAJIMARTIKULLI      = T_TRUPISHITJE.IDDETAJIMART
          LEFT  JOIN T_DETAJIMARTIKULLI DETAJIM2
                  ON DETAJIM2.IDDETAJIMARTIKULLI      = T_TRUPISHITJE.IDDETAJIMART2
          LEFT  JOIN T_LLOGARI LLOGARISHPENZ
                  ON LLOGARISHPENZ.IDLLOGARI          = T_TRUPISHITJE.IDLLOGSHPENZIMI
          LEFT  JOIN T_PERDORUESI
                  ON T_PERDORUESI.IDPERDORUES         = T_KOKASHITJE.IDKRIJUESI
          LEFT  JOIN T_KUSHTEDERGIMI
                  ON T_KUSHTEDERGIMI.IDKUSHTDERGIMI   = T_KOKASHITJE.IDKUSHTDERGIM
          LEFT  JOIN T_KUSHTPAGESEKOKA
                  ON T_KUSHTPAGESEKOKA.IDKOKA         = T_KOKASHITJE.IDKUSHTPAGESE
          LEFT  JOIN T_BANKA
                  ON T_BANKA.IDBANKA                  = T_KOKASHITJE.IDARKA
          LEFT  JOIN T_NDERMARJE
                  ON T_NDERMARJE.IDNDERMARJE          = T_KOKASHITJE.IDNDERM
          LEFT  JOIN T_KATEGORISHPENZIMI
                  ON T_KATEGORISHPENZIMI.ID           = T_TRUPISHITJE.IDKATEGORISHPENZIMI
          LEFT  JOIN T_KARTA
                  ON T_KARTA.IDKARTA                  = T_KOKASHITJE.IDKARTA
          -- MMDT kusht joins
          LEFT  JOIN T_KUSHTEMPLATE
                  ON T_KUSHTEMPLATE.idkONFIGAMBJENTE  = T_KOKASHITJE.IDKONFIGAMBJENTE
          LEFT  JOIN T_KUSHTE
                  ON T_KUSHTE.IDKUSHT                 = T_KUSHTEMPLATE.IDKUSHT
          LEFT  JOIN T_ALTERNATIVAKUSHTI
                  ON T_ALTERNATIVAKUSHTI.IDALTERNATIVEKUSHTI = T_KUSHTEMPLATE.VLERA
                 AND T_ALTERNATIVAKUSHTI.IDKUSHTI            = T_KUSHTE.IDKUSHT
          -- FIX: correlated subqueries replaced with LEFT JOINs
          LEFT  JOIN T_OPERATORE op
                  ON op.IDOPERATOR                    = T_KOKASHITJE.IDOPERATOR
          LEFT  JOIN T_PROCESI proc_
                  ON proc_.ID                         = T_KOKASHITJE.Procesi
          LEFT  JOIN T_TIPIEINVOICE tei
                  ON tei.ID                           = T_KOKASHITJE.eInvoiceType
          OUTER APPLY (
                    SELECT TOP(1) PERSHKRIMI
                    FROM   T_KODBARI
                    WHERE  IDARTIKULLI = T_ARTIKULLI.IDARTIKULLI
                      AND  NJESIA      = CASE WHEN T_TRUPISHITJE.IDNJESIA = T_ARTIKULLI.NJESI1ARTIKULLI THEN 1 ELSE 2 END
                    ORDER BY IDKODBARI ASC
                ) T_KODBARIPAREARTIKULLIT'

    -- Optional serial join
    IF @SERIALEUNIKE = 1
        SET @JOINS_SERIAL = '
          LEFT  JOIN T_TRUPIMAGAZINA
                  ON T_TRUPIMAGAZINA.IDTRUPISHITJEGJENERIMI = T_TRUPISHITJE.IDSHITJETRUPI
          LEFT  JOIN T_SERIALEUNIKE_LIDHJE_MAGAZINE slm
                  ON slm.id_trupi_magazine             = T_TRUPIMAGAZINA.idtrupimagazina'
    ELSE
        SET @JOINS_SERIAL = ''

    SET @WHERE_COMMON = '
    WHERE N.IDKATDOK              = ' + @IDKATDOK + '
      AND T_KOKASHITJE.IDSTATUSDOK <> 2
      AND T_KUSHTE.KODI            = ''MMDT''
      AND T_KOKASHITJE.IDNDERM     = ' + @IDNDERMARJE + '
      AND T_KOKASHITJE.IDNDERMVIT  = ' + @IDNDERVITI

    -- --------------------------------------------------------
    -- 4.  Komisioni branch  (HIQRRESHTAKOMISIONI=1)
    --     Original used TOP 0 trick; kept semantically but
    --     now as a clean separate CTE-style temp.
    -- --------------------------------------------------------
    DECLARE @SQL_KOMISIONI VARCHAR(MAX) = ''
    IF @HIQRRESHTAKOMISIONI = 1
    BEGIN
        SET @SQL_KOMISIONI = '
        IF OBJECT_ID(''tempdb..#SHITJE_K'') IS NOT NULL DROP TABLE #SHITJE_K

        SELECT ' + @COLS_COMMON + '
               ,sm.Seriali_Kryesor                                                   AS [Seriali Unik Kryesor]
               ,CONVERT(VARCHAR(100),'''')                                            AS [Seriali Unik Dytesor]
               ,'''' AS [Artikulli Set]
               ,CASE T_KOKASHITJE.ZBRITJENEVLERE
                    WHEN -1 THEN ''''  WHEN 0 THEN ''Perqindje''  WHEN 1 THEN ''Vlere''
                END                                                                   AS [Lloj Zbritje Totale]
               ,T_TRUPISHITJE.SHENIME2                                                AS [Shenime 2 Trupi]
               ,CASE WHEN T_KOKASHITJE.IDLLOJMARREVESHJE IN (0,-1) THEN ''''
                     WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 1 THEN ''Retention''
                     WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 2 THEN ''Acquisition''
                     WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 3 THEN ''EBU Benefit''
                     WHEN T_KOKASHITJE.IDLLOJMARREVESHJE = 4 THEN ''Agreement Tenure Reward''
                END                                                                   AS [Lloji i marreveshjes]
               ,T_KOKASHITJE.IDMARREVESHJE  AS [ID e marreveshjes]
               ,T_KOKASHITJE.KERKUARNGA     AS KerkuarNga
               ,T_KOKASHITJE.DATEKERKESE    AS DateKerkese
               ,kf.KodIntegrimi             AS [Kod Klienti Integrimi]
               ,T_KOKASHITJE.IIC, T_KOKASHITJE.NIVF
               ,op.KODI                     AS Operatori
               ,T_KOKASHITJE.EINVOICE, T_KOKASHITJE.EIC, T_KOKASHITJE.EinStatus
               ,proc_.KODI                  AS Procesi
               ,tei.KODI                    AS [E-invoice Type]
               ,T_KOKASHITJE.NIVFKTHIM      AS [NIVF kthim]
               ,T_KOKASHITJE.TIPIIVETEFATURIMIT AS [Tipi i vetefaturimit]
        INTO   #SHITJE_K
        ' + @JOINS_COMMON + '
          LEFT JOIN T_TRUPIMAGAZINA tm
                 ON tm.idtrupishitjegjenerimi = T_TRUPISHITJE.IDSHITJETRUPI
          LEFT JOIN T_SERIALEUNIKE_LIDHJE_MAGAZINE sm
                 ON sm.id_trupi_magazine      = tm.idtrupimagazina
          LEFT JOIN T_SERIALEUNIKE_KATEGORI sk
                 ON sk.ID                     = sm.id_kategori_seriali
        ' + @WHERE_COMMON + @FILTER_ID + '
          AND sk.KATEGORI                 = ''APARATE''
          AND sm.SERIALI_KRYESOR IS NOT NULL
          AND T_TRUPISHITJE.LLOGARITKOMISION = 0
        OPTION (RECOMPILE)
        '
    END

    -- --------------------------------------------------------
    -- 5.  Main query  (#SHITJE)
    -- --------------------------------------------------------
    -- Exclusion filter for @LLOJI<>1
    IF @LLOJI <> 1
    BEGIN
        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @EMERTABKOKA)
            SET @FILTER_EXCL = '
              AND T_KOKASHITJE.IDSHITJEKOKA NOT IN (SELECT [' + @EMERFUSHEID + '] FROM ' + @EMERTABKOKA + ')
              AND ((T_KOKASHITJE.IDDOKNGA IS NULL
                    OR T_KOKASHITJE.IDDOKNGA NOT IN (SELECT [' + @EMERFUSHEID + '] FROM ' + @EMERTABKOKA + '))
                   OR T_ALTERNATIVAKUSHTI.ALTERNATIVA <> ''DEFAULT'')'
    END

    DECLARE @SQL_MAIN VARCHAR(MAX)

    SET @SQL_MAIN = '
    IF OBJECT_ID(''tempdb..#SHITJE'') IS NOT NULL DROP TABLE #SHITJE

    SELECT ' + @COLS_COMMON + @COLS_SERIAL + @COLS_TAIL + '
    INTO   #SHITJE
    ' + @JOINS_COMMON + @JOINS_SERIAL + '
    ' + @WHERE_COMMON + @FILTER_ID + @FILTER_EXCL + @KOMISIONI_FILTER + '
    OPTION (RECOMPILE)
    '

    -- --------------------------------------------------------
    -- 6.  Historik branch  (MERRDOKTEMODIFIKUAR / MERRDOKTEFSHIRE)
    -- --------------------------------------------------------
    DECLARE @SQL_HISTORIK VARCHAR(MAX) = ''

    IF @MERRDOKTEMODIFIKUAR = 1 OR @MERRDOKTEFSHIRE = 1
    BEGIN
        -- Source CTE for historik rows
        DECLARE @HISTORIK_SOURCE VARCHAR(MAX)

        IF @MERRDOKTEMODIFIKUAR = 1 AND @MERRDOKTEFSHIRE = 1
            SET @HISTORIK_SOURCE = 'T_KOKASHITJEHISTORIK AS K2'
        ELSE IF @MERRDOKTEFSHIRE = 1
            SET @HISTORIK_SOURCE = '(
                SELECT a.*
                FROM   T_KOKASHITJEHISTORIK a
                WHERE  a.IDSTATUSDOK = 2
                  AND  a.idnderm     = ' + @IDNDERMARJE + '
                  AND  NOT EXISTS (SELECT 1 FROM T_KOKASHITJE       WHERE IDDOKNGA = a.idshitjekoka)
                  AND  NOT EXISTS (SELECT 1 FROM T_KOKASHITJEHISTORIK WHERE IDDOKNGA = a.idshitjekoka AND LIDHUR = 0)
            ) AS K2'
        ELSE  -- MERRDOKTEMODIFIKUAR = 1 only
            SET @HISTORIK_SOURCE = '(
                -- first modification version
                SELECT k.* FROM T_KOKASHITJEHISTORIK k
                WHERE  k.idnderm     = ' + @IDNDERMARJE + '
                  AND  k.IDSTATUSDOK = 2
                  AND  k.IDDOKNGA IS NOT NULL
                  AND  k.LIDHUR      = 0
                UNION ALL
                -- original when modified multiple times
                SELECT c.* FROM T_KOKASHITJEHISTORIK c
                INNER JOIN T_KOKASHITJEHISTORIK d ON c.IDSHITJEKOKA = d.IDDOKNGA
                WHERE  c.IDDOKNGA IS NULL AND c.IDNDERM = ' + @IDNDERMARJE + '
                  AND  c.IDSTATUSDOK = 2 AND c.LIDHUR = 0 AND d.LIDHUR = 0
                UNION ALL
                -- original modified once
                SELECT e.* FROM T_KOKASHITJEHISTORIK e
                INNER JOIN T_KOKASHITJE f ON e.IDSHITJEKOKA = f.IDDOKNGA
                WHERE  e.IDDOKNGA IS NULL AND e.IDNDERM = ' + @IDNDERMARJE + '
                  AND  e.IDSTATUSDOK = 2 AND e.LIDHUR = 0
            ) AS K2'

        SET @SQL_HISTORIK = '
        INSERT INTO #SHITJE
        SELECT
            K2.IDSHITJEKOKA                                                          AS [Id Shitje Koka],
            K2.DTMBARIMI                                                             AS [Date Mbarimi],
            K2.DTFILLIMI                                                             AS [Date Fillimi],
            N.KODI                                                                   AS Nenkategoria,
            T_KONFIGAMBJENTE.KODKONFIGAMBJENTE                                       AS [Lloj Dokumenti],
            KF.KODKLIENTFURNITOR                                                     AS [Klient/Furnitori],
            K2.NRPROJEKT                                                             AS [Numer Projekti],
            K2.NRDOK                                                                 AS [Nr Dokumenti],
            K2.NRSERIAL                                                              AS [Numer Serial],
            K2.DTDOK                                                                 AS [Date Dokumenti],
            K2.DTMODIFIKIMI                                                          AS [Date Modifikimi],
            K2.DTMATURIMI                                                            AS [Date Maturimi],
            T_MONEDHA.MONEDHAKOD                                                     AS Monedha,
            K2.KURSI                                                                 AS Kursi,
            AGJ1.KODIAGJENTSHITJE                                                    AS [Agjent Shitje 1],
            CASE K2.IDMENYREPAGESE
                WHEN -1 THEN ''''   WHEN 0 THEN ''Me mirebesim''
                WHEN  4 THEN ''Pagese''   WHEN 5 THEN ''Pagese Automatike''
                WHEN  7 THEN ''Me parapagim''   WHEN 8 THEN ''Arke''
                WHEN  9 THEN ''Karte krediti''
            END                                                                      AS [Menyre Pagese],
            CASE K2.ZBRITJENEVLERE WHEN 0 THEN K2.PERQINDJEZBRITJE WHEN 1 THEN K2.ZBRITJE END AS [Total Zbritje],
            K2.DTREGJISTRIMI                                                         AS [Date Regjistrimi],
            K2.ADRESAFATURIMIT                                                       AS [Adresa e Faturimit],
            K2.ADRESADERGIMIT                                                        AS [Adresa e Dergimit],
            K2.PERSHKRIMI                                                            AS Pershkrimi,
            CASE WHEN K2.DOGANA = 1 THEN ''po'' ELSE ''jo'' END                     AS Dogana,
            T_DEGEADMINISTRATIVE.KODI                                                AS [Dege Administrative],
            T_PIKESHITJEFURNIZIMI.KODI                                               AS [Pike Shitje/Furnizimi],
            K2.AFATIKOHOR                                                            AS [Afati Kohor],
            K2.PERQINDJEAGJENTI                                                      AS [Perqindje Agjenti 1],
            GR1.KODI                                                                 AS [Grupim Dokumenti 1],
            GR2.KODI                                                                 AS [Grupim Dokumenti 2],
            GR3.KODI                                                                 AS [Grupim Dokumenti 3],
            K2.EMERKLIENTI                                                           AS [Emer Klienti],
            K2.KONTAKTI                                                              AS Kontakti,
            K2.KASE                                                                  AS Kase,
            CASE WHEN K2.KUPON = 1 THEN ''po'' ELSE ''jo'' END                      AS Kupon,
            autom.NRSHASIE                                                           AS Automjeti,
            K2.KILOMETRAAUTO                                                         AS Kilometra,
            AGJ2.KODIAGJENTSHITJE                                                    AS [Agjent Shitje 2],
            K2.PERQINDJEAGJENTI2                                                     AS [Perqindje Agjenti 2],
            AGJ3.KODIAGJENTSHITJE                                                    AS [Agjent Shitje 3],
            K2.PERQINDJEAGJENTI3                                                     AS [Perqindje Agjenti 3],
            K2.MARRESI                                                               AS Marresi,
            T_TRANSPORTUES.EMERTIMI                                                  AS Transportues,
            K2.SHPENZJOTEZBRITSHME                                                   AS [Shpenzime Jo Te Zbritshme],
            T_PERDORUESI.PERDORUESUSERNAME                                           AS Krijuesi,
            K2.IDNIVELGJENERUES                                                      AS [Id Nivel Gjenerues],
            K2.IDDOKNGA                                                              AS [Id Dok Nga],
            K2.DTTRANSPORTIMI                                                        AS [Dt Transporti],
            K2.CASH                                                                  AS Cash,
            T_KUSHTEDERGIMI.KODIKUSHTDERGIMI                                         AS [Kusht Dergimi],
            T_KUSHTPAGESEKOKA.KODIKUSHTPAGESE                                        AS [Kusht Pagese],
            K2.IDKONFIGGJENERUES                                                     AS [Id Konfig Gjenerues],
            K2.IDGJENERUES                                                           AS [Id Gjenerues],
            K2.TOTALI                                                                AS Totali,
            K2.TVSH                                                                  AS [Tvsh Koka],
            K2.IDSTATUSDOK                                                           AS [Id Status Dok],
            K2.TOTALIMETVSHMEZBRITJE                                                 AS [Totali Me Tvsh Me Zbritje],
            K2.DTKRIJIMI                                                             AS [Dt Krijimi],
            T_BANKA.KODIBANKA                                                        AS [Arka],
            K2.FATUREPERMBLEDHESE                                                    AS [Fature Permbledhese],
            T_NDERMARJE.NDERMARJEKODI                                                AS [Kod Ndermarrje],
            K2.DTFATURE                                                              AS [Dt Fature],
            autom.TARGA                                                              AS Targa,
            kf1.KODKLIENTFURNITOR                                                    AS [Klient/Furnitori vartes],
            K2.Targa                                                                 AS [Targa e shoferit],
            K2.Shoferi                                                               AS Shoferi,
            T_KARTA.KODI                                                             AS Karta,
            T_KATEGORISHPENZIMI.KODI                                                 AS [Kategori Shpenzimi],
            H.IDSHITJETRUPI                                                          AS IdRreshtiShitje,
            CASE H.IDLLOJVEPRIMI WHEN 1 THEN ''Artikull'' WHEN 2 THEN ''Makro'' WHEN 3 THEN ''Llogari'' END AS Lloji,
            H.KODI                                                                   AS Kodi,
            H.PERSHKRIMI                                                             AS [Pershkrim Trupi],
            T_NJESIARTIKULLI.KODNJESIA                                               AS Njesia,
            H.SASIA, H.CMIMI,
            H.ZBRITJE                                                                AS [Zbritje Analitike],
            H.ZBRITJEVlere                                                           AS [Zbritje vlere],
            CASE WHEN H.LLOJZBRITJE = 0 THEN ''Perqindje'' ELSE ''Vlere'' END       AS [Lloj zbritje],
            H.CMIMI*(1+CASE WHEN H.TVSH=0 THEN 0 ELSE T_TAKSAT.normaperqindje END/100) AS [Cmimi me Tvsh],
            H.VLEFTAPATVSH                                                           AS [Vlefta pa TVSH],
            CASE WHEN H.TVSH=0 OR H.TVSH IS NULL THEN ''Pa TVSH'' ELSE T_TAKSAT.KODI END AS TVSH,
            H.VLEFTAMETVSH                                                           AS [Vlefta me TVSH],
            T_NJESIADMINISTRATIVE.KODI                                               AS Magazina,
            H.GJERESI, H.GJATESI,
            H.SASIPERMASE                                                            AS [Sasi Permase],
            DETAJIM1.KODDETAJIMARTIKULLI                                             AS [Detajimi 1],
            DETAJIM2.KODDETAJIMARTIKULLI                                             AS [Detajimi 2],
            H.SHENIME,
            LLOGARISHPENZ.NRLLOGARI                                                  AS [Llogari Shpenzimi],
            H.DTFILLIMI                                                              AS [Date Fillimi Trupi],
            H.DTMBARIMI                                                              AS [Date Mbarimi Trupi],
            H.IDTRUPIKONVERTIMI                                                      AS [Id Trupi Konvertimi],
            H.SASIREZ                                                                AS [Sasi Rezervimi],
            ''''                                                                      AS Seriali,
            CASE WHEN idkonf_merrSP.idkonfig IS NULL THEN T_KODBARI.PERSHKRIMI
                 ELSE ISNULL(T_KODBARI.PERSHKRIMI, T_KODBARIPAREARTIKULLIT.PERSHKRIMI)
            END                                                                      AS Kodbari,
            kf.EMERTIMIKF                                                            AS [Emertimi],
            GR1.PERSHKRIMI                                                           AS [Pershkrim Gr. Dok. 1],
            GR2.PERSHKRIMI                                                           AS [Pershkrim Gr. Dok. 2],
            GR3.PERSHKRIMI                                                           AS [Pershkrim Gr. Dok. 3],
            K2.KOORDINATA.STAsText()                                                 AS Koordinata,
            K2.NIPTK, K2.qytetik AS QytetiK, K2.SHENIME2 AS [Shenime 2],
            K2.KARTAPAPAGESE                                                         AS [Karta pa pagese],
            CASE WHEN K2.IDDOKTRANSFERIMNGA IS NULL OR K2.IDDOKTRANSFERIMNGA=0
                 THEN K2.IDSHITJEKOKA ELSE K2.IDDOKTRANSFERIMNGA END                AS [ID Dok Transferim Nga],
            CASE WHEN H.IDTRUPITRANSFERIMNGA IS NULL OR H.IDTRUPITRANSFERIMNGA=0
                 THEN H.IDSHITJETRUPI ELSE H.IDTRUPITRANSFERIMNGA END               AS [ID Trupi Transferim Nga],
            ''''  AS [Seriali Unik Kryesor],
            ''''  AS [Seriali Unik Dytesor],
            ''''  AS [Artikulli Set],
            CASE K2.ZBRITJENEVLERE WHEN -1 THEN '''' WHEN 0 THEN ''Perqindje'' WHEN 1 THEN ''Vlere'' END AS [Lloj Zbritje Totale],
            H.SHENIME2                                                               AS [Shenime 2 Trupi],
            CASE WHEN K2.IDLLOJMARREVESHJE IN (0,-1) THEN ''''
                 WHEN K2.IDLLOJMARREVESHJE=1 THEN ''Retention''
                 WHEN K2.IDLLOJMARREVESHJE=2 THEN ''Acquisition''
                 WHEN K2.IDLLOJMARREVESHJE=3 THEN ''EBU Benefit''
                 WHEN K2.IDLLOJMARREVESHJE=4 THEN ''Agreement Tenure Reward''
            END                                                                      AS [Lloji i marreveshjes],
            K2.IDMARREVESHJE  AS [ID e marreveshjes],
            K2.KERKUARNGA     AS KerkuarNga,
            K2.DATEKERKESE    AS DateKerkese,
            kf.KodIntegrimi   AS [Kod Klienti Integrimi],
            K2.IIC, K2.NIVF,
            op.KODI           AS Operatori,
            K2.EINVOICE, K2.EIC, K2.EinStatus,
            proc_.KODI        AS Procesi,
            tei.KODI          AS [E-invoice Type],
            K2.NIVFKTHIM      AS [NIVF kthim],
            K2.TIPIIVETEFATURIMIT AS [Tipi i vetefaturimit]
        FROM ' + @HISTORIK_SOURCE + '
             INNER JOIN T_TRUPISHITJEHISTORIK H
                     ON H.IDSHITJEKOKA = K2.IDSHITJEKOKA AND H.LIDHUR = 0
             INNER JOIN T_NIVELREGJISTRIMI N
                     ON N.IDNIVEL      = K2.IDNIVEL
             INNER JOIN T_KONFIGAMBJENTE
                     ON T_KONFIGAMBJENTE.IDKONFIGAMBJENTE = K2.IDKONFIGAMBJENTE
             INNER JOIN #AUTHORIZED auth_
                     ON auth_.idkonfigambjente            = K2.IDKONFIGAMBJENTE
             LEFT  JOIN #KONFIGNKAKNKA idkonf_merrSP
                     ON idkonf_merrSP.idkonfig            = K2.IDKONFIGAMBJENTE
             LEFT  JOIN T_KLIENTFURNITOR kf
                     ON kf.IDKLIENTFURNITOR               = K2.IDKLIENTFURNITOR
             LEFT  JOIN (
                        SELECT  kv.IDSHITJEKOKA,
                                STUFF((
                                    SELECT DISTINCT '', '' + kf2.KODKLIENTFURNITOR
                                    FROM   T_KOKASHITJE_KLIENTVARTES kv2
                                           INNER JOIN T_KLIENTFURNITOR kf2
                                                   ON kf2.IDKLIENTFURNITOR = kv2.IDKLIENTFURNITOR
                                    WHERE  kv2.IDSHITJEKOKA = kv.IDSHITJEKOKA
                                    FOR XML PATH(''''), TYPE
                                ).value(''.'', ''NVARCHAR(MAX)''), 1, 1, '''') AS KODKLIENTFURNITOR
                        FROM    T_KOKASHITJE_KLIENTVARTES kv
                        WHERE   EXISTS (
                                    SELECT 1 FROM T_KOKASHITJEHISTORIK k
                                    WHERE  k.IDSHITJEKOKA = kv.IDSHITJEKOKA
                                      AND  k.IDNDERM      = ' + @IDNDERMARJE + '
                                )
                        GROUP BY kv.IDSHITJEKOKA
                    ) kf1 ON kf1.IDSHITJEKOKA = K2.IDSHITJEKOKA
             INNER JOIN T_LLOGARI   ON T_LLOGARI.IDLLOGARI  = kf.IDLLOGARI
             INNER JOIN T_MONEDHA   ON T_MONEDHA.IDMONEDHA  = T_LLOGARI.MONEDHA
             LEFT  JOIN T_ARTIKULLI ON T_ARTIKULLI.IDARTIKULLI = H.IDKODI
             LEFT  JOIN T_KODBARI   ON T_KODBARI.IDKODBARI  = H.IDBARKODI
             LEFT  JOIN T_AUTOMJETE autom ON autom.IDAUTOMJETI = K2.IDAUTOMJETI
             LEFT  JOIN T_AGJENTSHITJE AGJ1 ON AGJ1.IDAGJENTSHITJE = K2.IDAGJENT
             LEFT  JOIN T_AGJENTSHITJE AGJ2 ON AGJ2.IDAGJENTSHITJE = K2.IDAGJENTI2
             LEFT  JOIN T_AGJENTSHITJE AGJ3 ON AGJ3.IDAGJENTSHITJE = K2.IDAGJENTI3
             LEFT  JOIN T_DEGEADMINISTRATIVE    ON T_DEGEADMINISTRATIVE.IDDEGEADMINISTRATIVE = K2.IDDEGEADMINISTRATIVE
             LEFT  JOIN T_PIKESHITJEFURNIZIMI   ON T_PIKESHITJEFURNIZIMI.IDPIKESHITJEFURNIZIMI = K2.IDPIKESHITJEFURNIZIMI
             LEFT  JOIN T_GRUPIMDOKUMENTIKOKA GR1 ON GR1.IDGRUPIMKOKA = K2.IDGRUP1
             LEFT  JOIN T_GRUPIMDOKUMENTIKOKA GR2 ON GR2.IDGRUPIMKOKA = K2.IDGRUP2
             LEFT  JOIN T_GRUPIMDOKUMENTIKOKA GR3 ON GR3.IDGRUPIMKOKA = K2.IDGRUP3
             LEFT  JOIN T_TRANSPORTUES  ON T_TRANSPORTUES.IDTRANSPORTUES  = K2.IDTRANSPORTUES
             LEFT  JOIN T_NJESIARTIKULLI ON T_NJESIARTIKULLI.IDNJESIA     = H.IDNJESIA
             LEFT  JOIN T_TAKSAT         ON T_TAKSAT.IDTAKSA              = H.TVSH
             LEFT  JOIN T_NJESIADMINISTRATIVE ON T_NJESIADMINISTRATIVE.IDNJESIADM = H.IDMAGAZINA
             LEFT  JOIN T_DETAJIMARTIKULLI DETAJIM1 ON DETAJIM1.IDDETAJIMARTIKULLI = H.IDDETAJIMART
             LEFT  JOIN T_DETAJIMARTIKULLI DETAJIM2 ON DETAJIM2.IDDETAJIMARTIKULLI = H.IDDETAJIMART2
             LEFT  JOIN T_LLOGARI LLOGARISHPENZ ON LLOGARISHPENZ.IDLLOGARI = H.IDLLOGSHPENZIMI
             LEFT  JOIN T_PERDORUESI ON T_PERDORUESI.IDPERDORUES           = K2.IDKRIJUESI
             LEFT  JOIN T_KUSHTEDERGIMI   ON T_KUSHTEDERGIMI.IDKUSHTDERGIMI   = K2.IDKUSHTDERGIM
             LEFT  JOIN T_KUSHTPAGESEKOKA ON T_KUSHTPAGESEKOKA.IDKOKA         = K2.IDKUSHTPAGESE
             LEFT  JOIN T_BANKA           ON T_BANKA.IDBANKA                  = K2.IDARKA
             LEFT  JOIN T_NDERMARJE       ON T_NDERMARJE.IDNDERMARJE          = K2.IDNDERM
             LEFT  JOIN T_KATEGORISHPENZIMI ON T_KATEGORISHPENZIMI.ID         = H.IDKATEGORISHPENZIMI
             LEFT  JOIN T_KARTA           ON T_KARTA.IDKARTA                  = K2.IDKARTA
             LEFT  JOIN T_KUSHTEMPLATE    ON T_KUSHTEMPLATE.idkONFIGAMBJENTE  = K2.IDKONFIGAMBJENTE
             LEFT  JOIN T_KUSHTE          ON T_KUSHTE.IDKUSHT                 = T_KUSHTEMPLATE.IDKUSHT
             LEFT  JOIN T_ALTERNATIVAKUSHTI
                     ON T_ALTERNATIVAKUSHTI.IDALTERNATIVEKUSHTI = T_KUSHTEMPLATE.VLERA
                    AND T_ALTERNATIVAKUSHTI.IDKUSHTI            = T_KUSHTE.IDKUSHT
             LEFT  JOIN T_OPERATORE op   ON op.IDOPERATOR = K2.IDOPERATOR
             LEFT  JOIN T_PROCESI proc_  ON proc_.ID      = K2.Procesi
             LEFT  JOIN T_TIPIEINVOICE tei ON tei.ID      = K2.eInvoiceType
             LEFT  JOIN #SHITJE existing ON existing.IdRreshtiShitje = H.IDSHITJETRUPI
             OUTER APPLY (
                        SELECT TOP(1) PERSHKRIMI FROM T_KODBARI
                        WHERE  IDARTIKULLI = T_ARTIKULLI.IDARTIKULLI
                          AND  NJESIA = CASE WHEN H.IDNJESIA = T_ARTIKULLI.NJESI1ARTIKULLI THEN 1 ELSE 2 END
                        ORDER BY IDKODBARI ASC
                    ) T_KODBARIPAREARTIKULLIT
        WHERE  N.IDKATDOK           = ' + @IDKATDOK + '
          AND  T_KUSHTE.KODI        = ''MMDT''
          AND  K2.IDNDERM           = ' + @IDNDERMARJE + '
          AND  K2.IDNDERMVIT        = ' + @IDNDERVITI + '
          AND  existing.IdRreshtiShitje IS NULL
          AND  K2.LIDHUR            = 0 '

        IF @HIQRRESHTAKOMISIONI <> 0
            SET @SQL_HISTORIK = @SQL_HISTORIK + '  AND H.LLOGARITKOMISION = 0 '

        SET @SQL_HISTORIK = @SQL_HISTORIK + ' OPTION (RECOMPILE) '
    END

    -- --------------------------------------------------------
    -- 7.  Final SELECT / INSERT into target table
    -- --------------------------------------------------------
    DECLARE @SQL_FINAL VARCHAR(MAX)

    IF @EMERTABKOKA = 'T_TEMP_MAG_FSH_KOKA'
        SET @SQL_FINAL = '
        INSERT INTO eksport_fshmag_temp
        SELECT *, ''' + @EMERTABKOKA + ''', GETDATE()
        FROM   #SHITJE
        WHERE  [Lloj Dokumenti] = ''FSHmag'' AND [Id Status Dok] = 1

        SELECT  s.*
        FROM    #SHITJE s
        WHERE   NOT EXISTS (
                    SELECT 1
                    FROM   #SHITJE s2
                           JOIN T_ARTIKULLI art
                                ON art.KODARTIKULLI = s2.Kodi
                               AND art.IDNDERMARJE  = ' + @IDNDERMARJE + '
                           JOIN T_SERIALEUNIKE_FORMATE formate
                                ON formate.IDFORMATSERIALESH = art.IDKATEGORISERIALI
                           JOIN T_SERIALEUNIKE_KATEGORI kser
                                ON kser.ID = formate.IDKATEGORISERIALESH
                    WHERE  kser.KATEGORI          = ''APARATE''
                      AND  s2.[Seriali Unik Kryesor] = ''''
                      AND  s2.[Id Shitje Koka]    = s.[Id Shitje Koka]
                      AND  s2.[Lloj Dokumenti]   LIKE ''%FSHmag%''
                      AND  s2.[Id Status Dok]     = 1
                )'
    ELSE
        SET @SQL_FINAL = '
        SELECT *
        FROM   #SHITJE
        ORDER  BY [Date Dokumenti] DESC, [Id Shitje Koka] DESC'

    -- --------------------------------------------------------
    -- 8.  Komisioni merge back if needed
    -- --------------------------------------------------------
    DECLARE @SQL_KOMISIONI_MERGE VARCHAR(MAX) = ''
    IF @HIQRRESHTAKOMISIONI = 1
        SET @SQL_KOMISiONI_MERGE = '
        INSERT INTO #SHITJE SELECT * FROM #SHITJE_K WHERE IdRreshtiShitje NOT IN (SELECT IdRreshtiShitje FROM #SHITJE) '

    -- --------------------------------------------------------
    -- 9.  Execute everything
    -- --------------------------------------------------------
    SET @SQL = @SQL_KOMISIONI + @SQL_MAIN + @SQL_HISTORIK + @SQL_KOMISIONI_MERGE + @SQL_FINAL

    EXEC (@SQL)

    SET @Err = @@ERROR
    RETURN @Err
END
GO
