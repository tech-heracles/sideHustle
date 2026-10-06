-- Rikthen versionet para 12-kontroll-dublikate-plan.sql (ato te skripteve 07 dhe 08).
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[prc_T_KOKASHITJE_ekzistonRegjistrimShitjeSipasIdentifikuese]
(
    @NRDOK                  VARCHAR(100),
    @IDNIVEL                T_FOR_KEY,
    @IDNDERMARJE            T_FOR_KEY,
    @DTDOK                  DATETIME,
    @IDKONFIGAMBJENTE       T_FOR_KEY,
    @IDKLIENTFURNITOR       T_FOR_KEY,
    @NRSERIAL               VARCHAR(100),
    @IDMENYREPAGESE         T_FOR_KEY,
    @IDPIKESHITJEFURNIZIMI  T_FOR_KEY,
    @IDDEGEADMINISTRATIVE   T_FOR_KEY,
    @IDRAPORTDESING         T_FOR_KEY,
    @IDGRUP1                T_FOR_KEY,
    @IDGRUP2                T_FOR_KEY,
    @IDGRUP3                T_FOR_KEY,
    @IDAGJENT               T_FOR_KEY,
    @IDSHITJEKOKA           T_FOR_KEY
)
AS
BEGIN
    SET NOCOUNT ON;

    -- ----------------------------------------------------------------
    -- 1. Resolve all unique-field flags in ONE pass over the two tables
    --    instead of 14 separate subqueries against @ident.
    -- ----------------------------------------------------------------
    DECLARE
        @EXISTS1  BIT, @EXISTS2  BIT, @EXISTS3  BIT, @EXISTS4  BIT,
        @EXISTS5  BIT, @EXISTS6  BIT, @EXISTS7  BIT, @EXISTS8  BIT,
        @EXISTS9  BIT, @EXISTS10 BIT, @EXISTS11 BIT, @EXISTS12 BIT,
        @EXISTS13 BIT, @EXISTS14 BIT,
        @HasIdent BIT;

    SELECT
        @EXISTS1  = MAX(CASE WHEN tk.KODKONTROLL = 'cmbNiveli'               THEN 1 ELSE 0 END),
        @EXISTS2  = MAX(CASE WHEN tk.KODKONTROLL = 'cmbModeli'               THEN 1 ELSE 0 END),
        @EXISTS3  = MAX(CASE WHEN tk.KODKONTROLL = 'data_DateEdit'           THEN 1 ELSE 0 END),
        @EXISTS4  = MAX(CASE WHEN tk.KODKONTROLL = 'txtNumer'                THEN 1 ELSE 0 END),
        @EXISTS5  = MAX(CASE WHEN tk.KODKONTROLL = 'btnKlienti'              THEN 1 ELSE 0 END),
        @EXISTS6  = MAX(CASE WHEN tk.KODKONTROLL = 'txtNumerSerial'          THEN 1 ELSE 0 END),
        @EXISTS7  = MAX(CASE WHEN tk.KODKONTROLL = 'cmbMenyrePagese'         THEN 1 ELSE 0 END),
        @EXISTS8  = MAX(CASE WHEN tk.KODKONTROLL = 'cmbPikeShitjeFurnizimi'  THEN 1 ELSE 0 END),
        @EXISTS9  = MAX(CASE WHEN tk.KODKONTROLL = 'cmbDegeAdministrative'   THEN 1 ELSE 0 END),
        @EXISTS10 = MAX(CASE WHEN tk.KODKONTROLL = 'cmbFormatiPrintimit'     THEN 1 ELSE 0 END),
        @EXISTS11 = MAX(CASE WHEN tk.KODKONTROLL = 'cmbGrup1'                THEN 1 ELSE 0 END),
        @EXISTS12 = MAX(CASE WHEN tk.KODKONTROLL = 'cmbGrup2'                THEN 1 ELSE 0 END),
        @EXISTS13 = MAX(CASE WHEN tk.KODKONTROLL = 'cmbGrup3'                THEN 1 ELSE 0 END),
        @EXISTS14 = MAX(CASE WHEN tk.KODKONTROLL = 'btnAgjenti'              THEN 1 ELSE 0 END),
        @HasIdent = 1                          -- will be NULL if no rows match
    FROM dbo.T_ATRIBUTETRUPI  ta
    INNER JOIN dbo.T_KONTROLLE tk ON tk.IDKONTROLL = ta.IDKONTROLL
    WHERE ta.IDKONFIGAMBJENTE = @IDKONFIGAMBJENTE
      AND tk.TIPI            <> 0
      AND ta.UNIKE            = 1;

    -- ----------------------------------------------------------------
    -- 2. Fast-path: no unique controls configured  legacy count
    -- ----------------------------------------------------------------
    IF @HasIdent IS NULL
    BEGIN
        SELECT COUNT(*) FROM dbo.T_KOKASHITJE WHERE IDSTATUSDOK = 77;
        RETURN 0;
    END

    -- ----------------------------------------------------------------
    -- 3. Normalize NULLs produced by MAX() over an empty set
    -- ----------------------------------------------------------------
    SELECT
        @EXISTS1  = ISNULL(@EXISTS1,  0), @EXISTS2  = ISNULL(@EXISTS2,  0),
        @EXISTS3  = ISNULL(@EXISTS3,  0), @EXISTS4  = ISNULL(@EXISTS4,  0),
        @EXISTS5  = ISNULL(@EXISTS5,  0), @EXISTS6  = ISNULL(@EXISTS6,  0),
        @EXISTS7  = ISNULL(@EXISTS7,  0), @EXISTS8  = ISNULL(@EXISTS8,  0),
        @EXISTS9  = ISNULL(@EXISTS9,  0), @EXISTS10 = ISNULL(@EXISTS10, 0),
        @EXISTS11 = ISNULL(@EXISTS11, 0), @EXISTS12 = ISNULL(@EXISTS12, 0),
        @EXISTS13 = ISNULL(@EXISTS13, 0), @EXISTS14 = ISNULL(@EXISTS14, 0);

    -- ----------------------------------------------------------------
    -- 4. Pre-compute effective filter values once.
    --    When the flag is OFF we pass NULL, which the WHERE clause
    --    below treats as "match anything" via the OR col IS NOT NULL trick.
    --    This keeps predicates sargable.
    -- ----------------------------------------------------------------
    DECLARE
        @F_IDNIVEL               T_FOR_KEY  = CASE WHEN @EXISTS1  = 1 THEN @IDNIVEL               END,
        @F_IDKONFIGAMBJENTE      T_FOR_KEY  = CASE WHEN @EXISTS2  = 1 THEN @IDKONFIGAMBJENTE      END,
        @F_DTDOK                 DATETIME   = CASE WHEN @EXISTS3  = 1 THEN @DTDOK                 END,
        @F_NRDOK                 VARCHAR(100)= CASE WHEN @EXISTS4 = 1 THEN @NRDOK                 END,
        @F_IDKLIENTFURNITOR      T_FOR_KEY  = CASE WHEN @EXISTS5  = 1 THEN @IDKLIENTFURNITOR      END,
        @F_NRSERIAL              VARCHAR(100)= CASE WHEN @EXISTS6 = 1 THEN @NRSERIAL              END,
        @F_IDMENYREPAGESE        T_FOR_KEY  = CASE WHEN @EXISTS7  = 1 THEN @IDMENYREPAGESE        END,
        @F_IDPIKESHITJEFURNIZIMI T_FOR_KEY  = CASE WHEN @EXISTS8  = 1 THEN @IDPIKESHITJEFURNIZIMI END,
        @F_IDDEGEADMINISTRATIVE  T_FOR_KEY  = CASE WHEN @EXISTS9  = 1 THEN @IDDEGEADMINISTRATIVE  END,
        @F_IDRAPORTDESING        T_FOR_KEY  = CASE WHEN @EXISTS10 = 1 THEN @IDRAPORTDESING        END,
        @F_IDGRUP1               T_FOR_KEY  = CASE WHEN @EXISTS11 = 1 THEN @IDGRUP1               END,
        @F_IDGRUP2               T_FOR_KEY  = CASE WHEN @EXISTS12 = 1 THEN @IDGRUP2               END,
        @F_IDGRUP3               T_FOR_KEY  = CASE WHEN @EXISTS13 = 1 THEN @IDGRUP3               END,
        @F_IDAGJENT              T_FOR_KEY  = CASE WHEN @EXISTS14 = 1 THEN @IDAGJENT              END;

    -- ----------------------------------------------------------------
    -- 5. Main duplicate-detection query
    --    Pattern: (@F_col IS NULL OR col = @F_col)
    --       when flag = 1   @F_col has the real value   equality filter (sargable)
    --       when flag = 0   @F_col IS NULL              condition is always TRUE (no filter)
    --    Nullable columns additionally need the original NULL-match arm.
    -- ----------------------------------------------------------------
    BEGIN TRY

        SELECT SUM(NR) AS DuplicateCount
        FROM (
            -- Active sales header
            SELECT COUNT(*) AS NR
            FROM dbo.T_KOKASHITJE WITH (NOLOCK)
            WHERE IDSTATUSDOK              IN (0, 1, 4)
              AND IDNDERM                   = @IDNDERMARJE
              AND IDSHITJEKOKA             <> @IDSHITJEKOKA
              AND IDNIVEL                   = ISNULL(@F_IDNIVEL,          IDNIVEL)
              AND IDKONFIGAMBJENTE          = ISNULL(@F_IDKONFIGAMBJENTE,  IDKONFIGAMBJENTE)
              AND DTDOK                     = ISNULL(@F_DTDOK,             DTDOK)
              AND NRDOK                     = ISNULL(@F_NRDOK,             NRDOK)
              AND NRSERIAL                  = ISNULL(@F_NRSERIAL,          NRSERIAL)
              AND (IDKLIENTFURNITOR         = @F_IDKLIENTFURNITOR      OR @EXISTS5  = 0)
              AND (IDMENYREPAGESE           = @F_IDMENYREPAGESE         OR @EXISTS7  = 0)
              AND (IDPIKESHITJEFURNIZIMI    = @F_IDPIKESHITJEFURNIZIMI  OR @EXISTS8  = 0)
              AND (IDDEGEADMINISTRATIVE     = @F_IDDEGEADMINISTRATIVE   OR @EXISTS9  = 0)
              AND (IDRAPORTDESING           = @F_IDRAPORTDESING         OR @EXISTS10 = 0)
              AND (IDGRUP1                  = @F_IDGRUP1                OR @EXISTS11 = 0)
              AND (IDGRUP2                  = @F_IDGRUP2                OR @EXISTS12 = 0)
              AND (IDGRUP3                  = @F_IDGRUP3                OR @EXISTS13 = 0)
              AND (IDAGJENT                 = @F_IDAGJENT               OR @EXISTS14 = 0)

            UNION ALL

            -- Historical (unlinked, cancelled) sales header
            SELECT COUNT(*) AS NR
            FROM dbo.T_KOKASHITJEHISTORIK WITH (NOLOCK)
            WHERE LIDHUR                    = 0
              AND IDSTATUSDOK               = 6
              AND IDNDERM                   = @IDNDERMARJE
              AND IDSHITJEKOKA             <> @IDSHITJEKOKA
              AND IDNIVEL                   = ISNULL(@F_IDNIVEL,          IDNIVEL)
              AND IDKONFIGAMBJENTE          = ISNULL(@F_IDKONFIGAMBJENTE,  IDKONFIGAMBJENTE)
              AND DTDOK                     = ISNULL(@F_DTDOK,             DTDOK)
              AND NRDOK                     = ISNULL(@F_NRDOK,             NRDOK)
              AND NRSERIAL                  = ISNULL(@F_NRSERIAL,          NRSERIAL)
              AND (IDKLIENTFURNITOR         = @F_IDKLIENTFURNITOR      OR @EXISTS5  = 0)
              AND (IDMENYREPAGESE           = @F_IDMENYREPAGESE         OR @EXISTS7  = 0)
              AND (IDPIKESHITJEFURNIZIMI    = @F_IDPIKESHITJEFURNIZIMI  OR @EXISTS8  = 0)
              AND (IDDEGEADMINISTRATIVE     = @F_IDDEGEADMINISTRATIVE   OR @EXISTS9  = 0)
              AND (IDRAPORTDESING           = @F_IDRAPORTDESING         OR @EXISTS10 = 0)
              AND (IDGRUP1                  = @F_IDGRUP1                OR @EXISTS11 = 0)
              AND (IDGRUP2                  = @F_IDGRUP2                OR @EXISTS12 = 0)
              AND (IDGRUP3                  = @F_IDGRUP3                OR @EXISTS13 = 0)
              AND (IDAGJENT                 = @F_IDAGJENT               OR @EXISTS14 = 0)
        ) AS A
        -- plani per vlerat konkrete: pa te, filtri "kolona = ISNULL(@x, kolona)" skanonte T_KOKASHITJE ne cdo thirrje
        OPTION (RECOMPILE);

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH

END
GO
ALTER PROCEDURE [dbo].[prc_T_KOKAMAGAZINA_ekzistonRegjistrimMagazineSipasIdentifikuesi]
(
@IDKONFIGAMBJENTE T_FOR_KEY,
@NRDOK VARCHAR(100),
@IDMAG T_FOR_KEY,
@DTDOK T_DATE,
@IDNDERMARJE T_FOR_KEY,
@idnivel t_for_key,
@IDKLIENTFURNITOR t_for_key,
@IDDEGEADMINISTRATIVE t_for_key,
@IDRAPORTDESING t_for_key,
@IDGRUP1 t_for_key,
@IDGRUP2 t_for_key,
@IDGRUP3 t_for_key,
@IDKOKAMAGAZINA T_FOR_KEY

)
AS
BEGIN

DECLARE @Err Int
DECLARE @ident AS TABLE (kodkontroll varchar(100))
INSERT @ident

SELECT KODKONTROLL FROM dbo.T_ATRIBUTETRUPI ta INNER JOIN dbo.T_KONTROLLE tk ON tk.IDKONTROLL = ta.IDKONTROLL WHERE ta.IDKONFIGAMBJENTE=@IDKONFIGAMBJENTE AND tk.TIPI<>0  AND ta.UNIKE=1

	DECLARE @EXISTS1  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='cmbLloji'),0)
	DECLARE @EXISTS2  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='cmbKonfigurimi'),0)
	DECLARE @EXISTS3  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='dteDtDok'),0)
	DECLARE @EXISTS4  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='txtNrDok'),0)
	DECLARE @EXISTS5  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='btneKlientFurnitori'),0)
	DECLARE @EXISTS6  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='btneMagazina'),0)
	DECLARE @EXISTS7  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='cmbDegeAdministrative'),0)
	DECLARE @EXISTS8  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='cmbFormatiPrintimit'),0)
	DECLARE @EXISTS9  AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='cmbGrup1'),0)
	DECLARE @EXISTS10 AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='cmbGrup2'),0)
	DECLARE @EXISTS11 AS BIT = ISNULL((SELECT 1 FROM @ident i WHERE i.kodkontroll='cmbGrup3'),0)

if(SELECT count(*) FROM @ident i)=0
select 1;
	ELSE 

	select count(*)
	from T_KOKAMAGAZINA
	 WHERE IDSTATUSDOK in (1,0) AND dbo.T_KOKAMAGAZINA.IDNDERM=@IDNDERMARJE and IDKOKAMAGAZINA <> @IDKOKAMAGAZINA      
	AND dbo.T_KOKAMAGAZINA.IDNIVEL					=( CASE WHEN @EXISTS1 > 0 THEN @idnivel else idnivel END )       
	AND dbo.T_KOKAMAGAZINA.IDKONFIGAMBJENTE			=( CASE WHEN @EXISTS2 > 0 THEN @IDKONFIGAMBJENTE else IDKONFIGAMBJENTE END ) 
	AND dbo.T_KOKAMAGAZINA.DTDOK					=( CASE WHEN @EXISTS3 > 0 THEN @DTDOK else DTDOK END ) 
	AND dbo.T_KOKAMAGAZINA.NRDOK					=( CASE WHEN @EXISTS4 > 0  THEN @NRDOK else NRDOK END )
	AND (dbo.T_KOKAMAGAZINA.IDKLIENTFURNITOR		=( CASE WHEN @EXISTS5 > 0 THEN @IDKLIENTFURNITOR else IDKLIENTFURNITOR END ) OR ( dbo.T_KOKAMAGAZINA.IDKLIENTFURNITOR IS NULL AND @EXISTS5 = 0 ))
	AND (dbo.T_KOKAMAGAZINA.IDMAG					=( CASE WHEN @EXISTS6 > 0 THEN @IDMAG else IDMAG END ) OR ( dbo.T_KOKAMAGAZINA.IDMAG IS NULL AND @EXISTS6 = 0 ))
	AND (dbo.T_KOKAMAGAZINA.IDDEGEADMINISTRATIVE	=( CASE WHEN @EXISTS7 > 0 THEN @IDDEGEADMINISTRATIVE else IDDEGEADMINISTRATIVE END )OR (dbo.T_KOKAMAGAZINA.IDDEGEADMINISTRATIVE IS NULL AND @EXISTS7 = 0 ))
	AND( dbo.T_KOKAMAGAZINA.IDRAPORTDESING			=( CASE WHEN @EXISTS8 > 0 THEN @IDRAPORTDESING else IDRAPORTDESING END )OR (dbo.T_KOKAMAGAZINA.IDRAPORTDESING IS NULL AND @EXISTS8 = 0 ))
	AND (dbo.T_KOKAMAGAZINA.IDGRUP1					=( CASE WHEN @EXISTS9 > 0 THEN @IDGRUP1 else IDGRUP1 END ) OR (dbo.T_KOKAMAGAZINA.IDGRUP1 IS NULL AND @EXISTS9 = 0 ))
	AND (dbo.T_KOKAMAGAZINA.IDGRUP2					=( CASE WHEN @EXISTS10 > 0 THEN @IDGRUP2 else IDGRUP2 END ) OR (dbo.T_KOKAMAGAZINA.IDGRUP2 IS NULL AND @EXISTS10 = 0 ))
	AND (dbo.T_KOKAMAGAZINA.IDGRUP3					=( CASE WHEN @EXISTS11 > 0 THEN @IDGRUP3 else IDGRUP3 END ) OR (dbo.T_KOKAMAGAZINA.IDGRUP3 IS NULL AND @EXISTS11 = 0 ))
	-- plani per vlerat konkrete: pa te, filtrat "kolona = CASE ... ELSE kolona END" skanonin T_KOKAMAGAZINA ne cdo ruajtje
	OPTION (RECOMPILE)
               
	Set @Err = @@Error

	RETURN @Err
	
END
GO
