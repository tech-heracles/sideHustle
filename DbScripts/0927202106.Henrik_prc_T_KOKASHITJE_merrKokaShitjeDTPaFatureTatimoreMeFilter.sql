IF EXISTS 
	(SELECT 1 FROM sys.objects WHERE [NAME] = 'prc_T_KOKASHITJE_merrKokaShitjeDTPaFatureTatimoreMeFilter' AND [TYPE] = 'P')
	DROP PROCEDURE prc_T_KOKASHITJE_merrKokaShitjeDTPaFatureTatimoreMeFilter
GO

CREATE PROCEDURE [dbo].[prc_T_KOKASHITJE_merrKokaShitjeDTPaFatureTatimoreMeFilter]
(
	@IDPERDORUES    varchar(10),
	@idndermarje    varchar(10),
	@merrTegjitha   varchar(5),
	@idndermarjevit varchar(10),
	@datanga        VARCHAR(30),
	@dataderi        VARCHAR(50),
	@top varchar(10),
	@filter varchar(max) 
)
AS
BEGIN
	DECLARE @Err INT;
	DECLARE @selekti varchar(8000)
	DECLARE @tabelat varchar(max);
	DECLARE @filterbaze varchar(max);
	DECLARE @order varchar(2000);
	DECLARE @query varchar(max);
	DECLARE @topQuery varchar(50)=''

	if(LEN(@top)>0)
		SET @topQuery=' TOP '+@top;
	ELSE 
		SET @topQuery='';
	    

	set @selekti=replicate(cast('	SELECT	'+@topQuery+' [T_KOKASHITJE].IdShitjeKoka, DtMbarimi, T_KOKASHITJE.DTFILLIMI, T_KOKASHITJE.DTFATURE AS DtFature, T_KOKASHITJE.IDNIVEL AS IdNivel, IDTEMPLATE AS IdTemplate,
											IDKONFIGAMBJENTE AS IdKonfigAmbjente, dbo.T_KOKASHITJE.IDKLIENTFURNITOR AS IdKlientFurnitor, EMERTIMIKF AS EmertimiKf, LLOJIKF AS LlojiKf, IDPROJEKT AS IdProjekt,
											NRPROJEKT AS NrProjekt, NRDOK AS NrDok, NRSERIAL AS NrSerial, DTDOK AS DtDok, DTMATURIMI AS DtMaturimi, IDMONEDHA AS IdMonedha, KURSI AS Kursi, IDMENYRETRANSPORT AS IdMenyreTransporti,
											DTTRANSPORTIMI AS DtTransportimi, IDKUSHTDERGIM AS IdKushtDergimi, IDAGJENT AS IdAgjent, IDMENYREPAGESE AS IdMenyrePagese, IDKUSHTPAGESE AS IdKushtPagese, t_kokashitje.ZBRITJE AS Zbritje, 
											TOTALI AS Totali, t_kokashitje.TVSH AS Tvsh, DTREGJISTRIMI AS DtRegjistrimi, T_KOKASHITJE.IDSTATUSDOK AS IdStatusDok, IDNDERM AS IdNdermarrje, IDNDERMVIT AS IdNdermarrjeVit, 	
											IDNIVELGJENERUES AS IdNivelGjenerues, IDKONFIGGJENERUES AS IdKonfigGjenerues, IDGJENERUES AS IdGjenerues, IDDOKNGA AS IdDokNga, T_KOKASHITJE.ADRESAFATURIMIT AS AdresaFaturimit, 
											T_KOKASHITJE.ADRESADERGIMIT AS AdresaDergimit, T_KOKASHITJE.PERSHKRIMI AS Pershkrimi, TOTALIMETVSHMEZBRITJE AS TotaliMeZbritjeMeTVSH, DOGANA AS Dogana,IDDEGEADMINISTRATIVE AS IdDegeAdministrative, 
											IDPIKESHITJEFURNIZIMI AS IdPikeShitjeFurnizimi, NULL AS OFleteKontabel, NULL AS OKokaMagazina, T_KOKASHITJE.dtkrijimi AS DtKrijimi,  T_KOKASHITJE.dtmodifikimi AS DtModifikimi, T_KOKASHITJE.IDPERDORUESI AS IdPerdoruesi, 
											IDRAPORTDESING AS IdRaportDesing, AFATIKOHOR AS AfatKohor, CASH AS Cash, T_KOKASHITJE.STATUSAPROVIMI AS StatusAprovimi,T_KOKASHITJE.IDKRIJUESI AS IdKrijuesi, PERDORUESUSERNAME AS Krijuesi, [T_KOKASHITJE].PERQINDJEAGJENTI AS PerqindjeAgjenti,
											idgrup1 AS IdGrup1, idgrup2 AS IdGrup2, idgrup3 AS IdGrup3,  idtransferimi AS IdTransferimi, idkonfigtransferimi AS IdKonfigTransferimi, STATUSTRANSFERIMI AS StatusTransferimi, 
											EMERKLIENTI EmerKlienti, KONTAKTI Kontakti, kase Kase, t_kokashitje.kupon Kupon, IDAUTOMJETI AS IdAutomjeti, KILOMETRAAUTO AS KilometraAuto, IDAGJENTI2 AS IdAgjenti2, 
											T_KOKASHITJE.PERQINDJEAGJENTI2 AS PerqindjeAgjenti2, IDAGJENTI3 AS IdAgjenti3, t_kokashitje.PERQINDJEAGJENTI3 AS PerqindjeAgjenti3, MARRESI AS Marresi, IDTRANSPORTUES AS IdTransportues, 
											FATUREPERMBLEDHESE AS FaturePermbledhese, SHPENZJOTEZBRITSHME AS ShpenzJoTeZbritshme, IDARKA AS IdArka, DTFATURE AS DtFature, GJENERUAR AS Gjeneruar, MuajRaportimi, IdVitRaportimi, 
											kf.KODKLIENTFURNITOR AS kodKlientFurnitor, T_KOKASHITJE.KOORDINATA.STAsText() AS Koordinata, dbo.T_KOKASHITJE.qytetik AS QytetiK,IDKATEGORISERIALI IdKategoriSeriali,
											ZBRITJENEVLERE AS ZbritjeNeVlere, PERQINDJEZBRITJE AS PerqindjeZbritje, DTKRIJIMIPAJISJE AS DtKrijimiPajisje, T_KOKASHITJE.SHENIME2 Shenime2, 
											KARTAPAPAGESE KartaPaPagese, IDDOKTRANSFERIMNGA IdDokTransferimNga, IDLLOJMARREVESHJE as IdLlojMarreveshje
											,IDMARREVESHJE as IdMarreveshje
											,KERKUARNGA AS KerkuarNga 
											,DATEKERKESE AS DateKerkese
											,CASE
												WHEN	STATUSMARREVESHJE = 1
												THEN	''Aktive''
												WHEN	STATUSMARREVESHJE = 2		
												THEN	''Inaktive''
												WHEN	STATUSMARREVESHJE = 3
												THEN	''Anulluar''
												ELSE	''''
											END AS StatusMarreveshje' AS varchar(max)),1)
											
	SET @tabelat = ' FROM	[T_KOKASHITJE] 
							INNER JOIN	( 
											SELECT	DISTINCT IDSHITJEKOKA
											FROM	(
														SELECT	IDSHITJEKOKA 
														FROM	[T_KOKASHITJE]
																INNER JOIN	(	
																				SELECT	IDLIDHESE  
																				FROM	T_LIDHJEAUTORIZIM			autoL
																						INNER JOIN T_LLOJBUXHETI	llojB	
																							ON autoL.IDLLOJI = llojB.idllojbuxheti
																						INNER JOIN T_AUTORIZIMTRUPI	autoK	
																							ON autoL.IDAUTORIZIMEKOKA = autoK.IDAUTORIZIMKOKA
																				WHERE	llojB.KODLLOJBUXHETI = ''KonfigurimDok'' 
																						AND IDPERDORUESI = ' + @IDPERDORUES + '
																						AND autoL.IDSTATUSDOK = 1 
																			)	eshteAuto 
																	ON  [T_KOKASHITJE].IDKONFIGAMBJENTE = eshteAuto.IDLIDHESE
														WHERE	IDNDERM = ' + @idndermarje + ' 
																AND dbo.T_KOKASHITJE.IDNDERMVIT = ' + @idndermarjevit + ' 
																AND dbo.T_KOKASHITJE.DTDOK BETWEEN convert(date,'''+@datanga+''',103) AND convert(date,'''+@dataderi+''',103) 
																AND T_KOKASHITJE.IDSTATUSDOK = 1 
																AND T_KOKASHITJE.KUPON = 0

														UNION ALL
			
														SELECT	IDSHITJEKOKA
														FROM	[T_KOKASHITJE]
																LEFT JOIN	(	
																				SELECT	IDLIDHESE  
																				FROM	T_LIDHJEAUTORIZIM			autoL
																						INNER JOIN T_LLOJBUXHETI	llojB	
																							ON autoL.IDLLOJI = llojB.idllojbuxheti
																				WHERE	llojB.KODLLOJBUXHETI = ''KonfigurimDok'' 
																						AND autoL.IDSTATUSDOK = 1 
																			)	nukEshteAuto 
																	ON  [T_KOKASHITJE].IDKONFIGAMBJENTE = nukEshteAuto.IDLIDHESE
														WHERE	nukEshteAuto.IDLIDHESE IS NULL 
																AND IDNDERM = ' + @idndermarje + '
																AND dbo.T_KOKASHITJE.IDNDERMVIT = ' + @idndermarjevit + '
																AND dbo.T_KOKASHITJE.DTDOK BETWEEN convert(date,'''+@datanga+''',103) AND convert(date,'''+@dataderi+''',103) 
																AND T_KOKASHITJE.IDSTATUSDOK = 1 
																AND T_KOKASHITJE.KUPON = 0
													)	a
										)	autoRiz 
								ON [T_KOKASHITJE].IDSHITJEKOKA = autoRiz.IDSHITJEKOKA
							INNER JOIN	T_NIVELREGJISTRIMI AS n 
								ON T_KOKASHITJE.IDNIVEL = n.IDNIVEL
							INNER JOIN	dbo.T_PERDORUESI 
								ON t_kokashitje.idkrijuesi = dbo.T_PERDORUESI.IDPERDORUES
							LEFT JOIN	dbo.T_KLIENTFURNITOR kf 
								ON dbo.T_KOKASHITJE.IDKLIENTFURNITOR = kf.IDKLIENTFURNITOR
							LEFT JOIN	(	
											SELECT	DISTINCT kon.IDSHITJEKOKA 
											FROM	dbo.T_TRUPISHITJE kon 
													INNER JOIN t_trupishitje t		
														ON t. idtrupikonvertimi=kon.IDSHITJETRUPI  
													INNER JOIN dbo.T_KOKASHITJE k	
														ON t.IDSHITJEKOKA = k.IDSHITJEKOKA 
											WHERE	IDSTATUSDOK <> 2  
													AND idnderm = ' + @idndermarje + '
										)	kon 
								ON kon.IDSHITJEKOKA = [T_KOKASHITJE].IDSHITJEKOKA '
			
	SET @filterbaze= ' 
					WHERE	IDNDERM = ' + @idndermarje + '
							AND dbo.T_KOKASHITJE.IDNDERMVIT = ' + @idndermarjevit + ' 
							AND IDKATDOK = 1 
							AND KASE = 1 
							AND t_kokashitje.KUPON = 0
							AND (
									CASE 
										WHEN	' + @merrTegjitha + ' = 0 
										THEN	dbo.T_KOKASHITJE.IDKRIJUESI 
										ELSE	' + @idperdorues + ' 
									END
								) = ' + @idperdorues + '
							AND T_KOKASHITJE.IDSTATUSDOK = 1
							AND kon.IDSHITJEKOKA IS NULL
							AND KODI = ''USH''
							AND dbo.T_KOKASHITJE.DTDOK BETWEEN convert(date,'''+@datanga+''',103) AND convert(date,'''+@dataderi+''',103)'
         
	SET @order='	ORDER BY dtdok asc, [T_KOKASHITJE].idshitjekoka asc '

	if(len(@filter) > 0)
		SET @filterbaze = @filterbaze + ' AND ';

	PRINT (@selekti + @tabelat + @filterbaze + @filter + @order)
	exec (@selekti + @tabelat + @filterbaze + @filter + @order)

    SET @Err = @@Error;
    RETURN @Err;
END