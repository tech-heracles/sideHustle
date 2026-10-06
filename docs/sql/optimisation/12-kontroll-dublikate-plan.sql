-- 12 - Kontrolli i dokumentit te dublikuar ne ruajtje (trigeret INSTEAD OF INSERT te T_KOKASHITJE / T_KOKAMAGAZINA)
--
-- Skriptet 07 dhe 08 i vune OPTION (RECOMPILE) pyetjes se numerimit, qe te kerkonte me indeks ne vend te skanimit.
-- Por kompilimi i saj kushton ~13 ms CPU ne cdo thirrje (ekzekutimi ~0 ms), dhe trigeret e therrasin per cdo
-- dokument qe ruhet: ~15 ms shitje/blerje + ~15 ms magazine per dokument.
-- Tani pyetja ndertohet vetem me filtrat e fushave identifikuese aktive (sp_executesql me parametra): nje plan i
-- ruajtur per cdo kombinim fushash, me kerkim ne indeks, pa kompilim ne cdo thirrje.
-- Kushtet jane te njejta me me pare, edhe per vlerat NULL:
--   shitje:   NIVEL/MODEL/DATE/NR/NRSERIAL: aktive dhe vlera jo NULL -> kolona = vlera; ndryshe -> kolona IS NOT NULL
--             (si "kolona = ISNULL(@x, kolona)"); fushat e tjera: aktive -> kolona = vlera; joaktive -> pa filter.
--   magazine: NIVEL/KONFIGURIM/DATE/NR: aktive -> kolona = vlera; joaktive -> kolona IS NOT NULL
--             (si "kolona = CASE ... ELSE kolona END"); fushat e tjera: aktive -> kolona = vlera; joaktive -> pa filter.
-- Rikthimi: 12-rollback-kontroll-dublikate-plan.sql (versionet e 07/08)
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

    -- fushat identifikuese (unike) te llojit te dokumentit
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
        @HasIdent = 1
    FROM dbo.T_ATRIBUTETRUPI  ta
    INNER JOIN dbo.T_KONTROLLE tk ON tk.IDKONTROLL = ta.IDKONTROLL
    WHERE ta.IDKONFIGAMBJENTE = @IDKONFIGAMBJENTE
      AND tk.TIPI            <> 0
      AND ta.UNIKE            = 1;

    IF @HasIdent IS NULL
    BEGIN
        SELECT COUNT(*) FROM dbo.T_KOKASHITJE WHERE IDSTATUSDOK = 77;
        RETURN 0;
    END

    -- vetem filtrat e fushave aktive; teksti varet vetem nga kombinimi i fushave, prandaj plani riperdoret
    DECLARE @w nvarchar(max) = N'IDNDERM = @IDNDERMARJE AND IDSHITJEKOKA <> @IDSHITJEKOKA';
    SET @w += CASE WHEN @EXISTS1 = 1 AND @IDNIVEL          IS NOT NULL THEN N' AND IDNIVEL = @IDNIVEL'                   ELSE N' AND IDNIVEL IS NOT NULL' END;
    SET @w += CASE WHEN @EXISTS2 = 1 AND @IDKONFIGAMBJENTE IS NOT NULL THEN N' AND IDKONFIGAMBJENTE = @IDKONFIGAMBJENTE' ELSE N' AND IDKONFIGAMBJENTE IS NOT NULL' END;
    SET @w += CASE WHEN @EXISTS3 = 1 AND @DTDOK            IS NOT NULL THEN N' AND DTDOK = @DTDOK'                       ELSE N' AND DTDOK IS NOT NULL' END;
    SET @w += CASE WHEN @EXISTS4 = 1 AND @NRDOK            IS NOT NULL THEN N' AND NRDOK = @NRDOK'                       ELSE N' AND NRDOK IS NOT NULL' END;
    SET @w += CASE WHEN @EXISTS6 = 1 AND @NRSERIAL         IS NOT NULL THEN N' AND NRSERIAL = @NRSERIAL'                 ELSE N' AND NRSERIAL IS NOT NULL' END;
    IF @EXISTS5  = 1 SET @w += N' AND IDKLIENTFURNITOR = @IDKLIENTFURNITOR';
    IF @EXISTS7  = 1 SET @w += N' AND IDMENYREPAGESE = @IDMENYREPAGESE';
    IF @EXISTS8  = 1 SET @w += N' AND IDPIKESHITJEFURNIZIMI = @IDPIKESHITJEFURNIZIMI';
    IF @EXISTS9  = 1 SET @w += N' AND IDDEGEADMINISTRATIVE = @IDDEGEADMINISTRATIVE';
    IF @EXISTS10 = 1 SET @w += N' AND IDRAPORTDESING = @IDRAPORTDESING';
    IF @EXISTS11 = 1 SET @w += N' AND IDGRUP1 = @IDGRUP1';
    IF @EXISTS12 = 1 SET @w += N' AND IDGRUP2 = @IDGRUP2';
    IF @EXISTS13 = 1 SET @w += N' AND IDGRUP3 = @IDGRUP3';
    IF @EXISTS14 = 1 SET @w += N' AND IDAGJENT = @IDAGJENT';

    DECLARE @stmt nvarchar(max) = N'
        SELECT SUM(NR) AS DuplicateCount
        FROM (
            SELECT COUNT(*) AS NR FROM dbo.T_KOKASHITJE WITH (NOLOCK)
            WHERE IDSTATUSDOK IN (0, 1, 4) AND ' + @w + N'
            UNION ALL
            SELECT COUNT(*) AS NR FROM dbo.T_KOKASHITJEHISTORIK WITH (NOLOCK)
            WHERE LIDHUR = 0 AND IDSTATUSDOK = 6 AND ' + @w + N'
        ) AS A';

    EXEC sp_executesql @stmt,
        N'@NRDOK varchar(100), @IDNIVEL numeric(18,0), @IDNDERMARJE numeric(18,0), @DTDOK datetime, @IDKONFIGAMBJENTE numeric(18,0),
          @IDKLIENTFURNITOR numeric(18,0), @NRSERIAL varchar(100), @IDMENYREPAGESE numeric(18,0), @IDPIKESHITJEFURNIZIMI numeric(18,0),
          @IDDEGEADMINISTRATIVE numeric(18,0), @IDRAPORTDESING numeric(18,0), @IDGRUP1 numeric(18,0), @IDGRUP2 numeric(18,0),
          @IDGRUP3 numeric(18,0), @IDAGJENT numeric(18,0), @IDSHITJEKOKA numeric(18,0)',
        @NRDOK, @IDNIVEL, @IDNDERMARJE, @DTDOK, @IDKONFIGAMBJENTE, @IDKLIENTFURNITOR, @NRSERIAL, @IDMENYREPAGESE,
        @IDPIKESHITJEFURNIZIMI, @IDDEGEADMINISTRATIVE, @IDRAPORTDESING, @IDGRUP1, @IDGRUP2, @IDGRUP3, @IDAGJENT, @IDSHITJEKOKA;
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
	BEGIN
	-- vetem filtrat e fushave aktive; teksti varet vetem nga kombinimi i fushave, prandaj plani riperdoret
	DECLARE @w nvarchar(max) = N'IDSTATUSDOK in (1,0) AND IDNDERM = @IDNDERMARJE AND IDKOKAMAGAZINA <> @IDKOKAMAGAZINA'
	SET @w += CASE WHEN @EXISTS1 > 0 THEN N' AND IDNIVEL = @idnivel'                   ELSE N' AND IDNIVEL IS NOT NULL' END
	SET @w += CASE WHEN @EXISTS2 > 0 THEN N' AND IDKONFIGAMBJENTE = @IDKONFIGAMBJENTE' ELSE N' AND IDKONFIGAMBJENTE IS NOT NULL' END
	SET @w += CASE WHEN @EXISTS3 > 0 THEN N' AND DTDOK = @DTDOK'                       ELSE N' AND DTDOK IS NOT NULL' END
	SET @w += CASE WHEN @EXISTS4 > 0 THEN N' AND NRDOK = @NRDOK'                       ELSE N' AND NRDOK IS NOT NULL' END
	IF @EXISTS5  > 0 SET @w += N' AND IDKLIENTFURNITOR = @IDKLIENTFURNITOR'
	IF @EXISTS6  > 0 SET @w += N' AND IDMAG = @IDMAG'
	IF @EXISTS7  > 0 SET @w += N' AND IDDEGEADMINISTRATIVE = @IDDEGEADMINISTRATIVE'
	IF @EXISTS8  > 0 SET @w += N' AND IDRAPORTDESING = @IDRAPORTDESING'
	IF @EXISTS9  > 0 SET @w += N' AND IDGRUP1 = @IDGRUP1'
	IF @EXISTS10 > 0 SET @w += N' AND IDGRUP2 = @IDGRUP2'
	IF @EXISTS11 > 0 SET @w += N' AND IDGRUP3 = @IDGRUP3'

	DECLARE @stmt nvarchar(max) = N'select count(*) from T_KOKAMAGAZINA WHERE ' + @w
	EXEC sp_executesql @stmt,
		N'@IDKONFIGAMBJENTE numeric(18,0), @NRDOK varchar(100), @IDMAG numeric(18,0), @DTDOK datetime, @IDNDERMARJE numeric(18,0),
		  @idnivel numeric(18,0), @IDKLIENTFURNITOR numeric(18,0), @IDDEGEADMINISTRATIVE numeric(18,0), @IDRAPORTDESING numeric(18,0),
		  @IDGRUP1 numeric(18,0), @IDGRUP2 numeric(18,0), @IDGRUP3 numeric(18,0), @IDKOKAMAGAZINA numeric(18,0)',
		@IDKONFIGAMBJENTE, @NRDOK, @IDMAG, @DTDOK, @IDNDERMARJE, @idnivel, @IDKLIENTFURNITOR, @IDDEGEADMINISTRATIVE,
		@IDRAPORTDESING, @IDGRUP1, @IDGRUP2, @IDGRUP3, @IDKOKAMAGAZINA
	END

	Set @Err = @@Error
	RETURN @Err
END
GO
