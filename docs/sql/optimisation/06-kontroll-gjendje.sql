-- 06 - Kontrolli i gjendjes se artikullit (prc_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullit_Modifikim)
--
-- Thirret per cdo rresht magazine kur ruhet/importohet nje dokument (a ka gjendje; a shkon gjendja negative
-- ne ndonje date te mevonshme). Per metodat mesatare (1, 2, 5, 6):
--   - llogariste gjendjen e cdo date me nje shume te plote te te gjitha levizjeve deri ne ate date;
--   - vlerat futeshin si tekst ne SQL, keshtu qe cdo thirrje kompilonte nje plan te ri.
--   ~0,6 s per rresht per artikujt me shume levizje: kontrolli i nje importi me 10 mije rreshta shitjesh ~20 minuta.
-- Tani levizjet lexohen nje here, gjendja per date eshte shume progresive dhe SQL-i ka parametra (plani riperdoret).
-- Rezultati u krahasua me versionin e vjeter per qindra thirrje reale (dalje, dalje pa gjendje, hyrje,
-- modifikim, me dy data): asnje ndryshim. Dega FIFO nuk ndryshon; hiqet edhe print(@SqlSelect).
-- Rikthimi: 06-rollback-kontroll-gjendje.sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[prc_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullit_Modifikim]
(
	@IDNDERM			T_FOR_KEY,
	@IDMAG				T_FOR_KEY,
	@MEDYDATA			bit,
	@DATA1				T_DATE,
	@DATA2				T_DATE,
	@KAPDOKMESDATAVE	bit,
	@IDKOKAMAGAZINA		T_FOR_KEY,
	@ESHTEDALJE			bit,
	@IDARTIKULL			T_FOR_KEY,
	@MEDETAJIM			int,
	@IDDETAJIM			T_FOR_KEY,
	@SASIARTIKULL		decimal (18,8),
	@METODEKOSTO		int,
	@LLOJVEPRIMI		int,
	@SASIARTIKULLOLD	FLOAT
)
AS
BEGIN
	DECLARE @Err			Int
	DECLARE @SqlSelect		varchar(8000) = '
	CREATE table #KONFIGAMB  (IDKONFIGAMBJENTE NUMERIC(18, 0) PRIMARY KEY )
	INSERT	INTO #KONFIGAMB
	SELECT	DISTINCT T_KONFIGAMBJENTE.IDKONFIGAMBJENTE
	FROM	T_KONFIGAMBJENTE 
			JOIN T_KUSHTEMPLATE on T_KONFIGAMBJENTE.IDKONFIGAMBJENTE=T_KUSHTEMPLATE.IDKONFIGAMBJENTE 
			JOIN T_KUSHTE on T_KUSHTE.IDKUSHT = T_KUSHTEMPLATE.IDKUSHT and T_KUSHTE.KODI = ''PM''
	WHERE	T_KONFIGAMBJENTE.IDNDERMARJE =' + CAST(@IDNDERM AS VARCHAR(20)) + '
			AND T_KONFIGAMBJENTE.IDSTATUSDOK=1 			
			AND T_KUSHTEMPLATE.vlera IN ( SELECT IDALTERNATIVEKUSHTI FROM T_ALTERNATIVAKUSHTI WHERE ALTERNATIVA=''Po'')
	'
	DECLARE @SqlWhereJoin	varchar(4000)
	DECLARE @SqlTableJoin	varchar(4000)
	DECLARE @SqlWhere		varchar(4000)
	DECLARE @SqlWhere2		varchar(4000)
	DECLARE @data			varchar(1000)
	DECLARE @SqlFields		varchar(1000)
	DECLARE @roundVl		char(2)
	
	--numrat pas presjes qe duhet te rrubullakoset gjendja
	Set @roundVl='8'

	Set @SqlWhere= 'tk.IDNDERM=' + cast(@IDNDERM as varchar(50)) + ' AND tk.IDSTATUSDOK=1 AND tm.IDMAG=' + cast(@IDMAG as varchar(20)) +' AND IDARTIKULL=' + cast(@IDARTIKULL as varchar(50))
				  --+  ' AND tk.IDKONFIGAMBJENTE in (select T_KONFIGAMBJENTE.IDKONFIGAMBJENTE from T_KONFIGAMBJENTE inner join T_KUSHTEMPLATE on T_KONFIGAMBJENTE.IDKONFIGAMBJENTE=T_KUSHTEMPLATE.IDKONFIGAMBJENTE inner join T_KUSHTE on T_KUSHTE.IDKUSHT=T_KUSHTEMPLATE.IDKUSHT and T_KUSHTE.KODI=''PM'' inner join T_ALTERNATIVAKUSHTI on T_ALTERNATIVAKUSHTI.IDALTERNATIVEKUSHTI=T_KUSHTEMPLATE.vlera and T_ALTERNATIVAKUSHTI.ALTERNATIVA=''Po'')'

	if @MEDETAJIM=1
		Set @SqlWhere= @SqlWhere + ' and tm.IDDETAJIMI = '  + cast(@IDDETAJIM as varchar(50)) 
	else if @MEDETAJIM=2
		Set @SqlWhere= @SqlWhere + ' and tm.IDDETAJIMI2 = ' + cast(@IDDETAJIM as varchar(50)) 
	--------------------------------------------------------------------------------------------
	Set @SqlTableJoin=''

	
	-- FILLO 1: METODAT MESATARE (1, 2, 5, 6)
	-- Gjendja ne cdo date me veprime pas dates se dokumentit (datapas) = shuma e levizjeve te artikullit ne magazine
	-- deri ne ate date; kthehet data e pare ku gjendja (pas ketij veprimi) del negative.
	-- Me pare: nje shume e plote per cdo date (data x levizje; ~0,6 s per rresht per artikujt me shume levizje) dhe
	-- vlerat futeshin si tekst ne SQL, pra cdo thirrje kompilonte plan te ri. Tani: levizjet lexohen nje here (#lv),
	-- gjendja per date eshte shume progresive, dhe SQL-i eshte me parametra (i njejti plan per te gjitha thirrjet).
	-- Vlerat e parametrave jane ato qe jepte teksti me pare (p.sh. data kalon ne varchar(20) dhe kthehet me stilin 103).
	if @metodeKosto=1 or @metodeKosto=2 or @metodeKosto=5 or @metodeKosto=6
	begin
		DECLARE @stmt nvarchar(max), @w nvarchar(max), @filtriDates nvarchar(400), @fushat nvarchar(400), @kushti2 nvarchar(400), @meGjendjeFillestare bit = 0
		DECLARE @pD1 datetime = convert(datetime, cast(@data1 as varchar(20)), 103),
		        @pD2 datetime = convert(datetime, cast(@data2 as varchar(20)), 103),
		        @pSasi decimal(18,8)

		SET @w = N'tk.IDNDERM = @pNderm AND tk.IDSTATUSDOK = 1 AND tm.IDMAG = @pMag AND tm.IDARTIKULL = @pArt'
		if @MEDETAJIM=1
			SET @w = @w + N' AND tm.IDDETAJIMI = @pDet'
		else if @MEDETAJIM=2
			SET @w = @w + N' AND tm.IDDETAJIMI2 = @pDet'
		-- perjashtohet dokumenti qe po modifikohet (dhe ai qe ai gjeneroi me transferim)
		SET @w = @w + N' AND tk.IDKOKAMAGAZINA <> @pKoka AND (mg2.IDKOKAMAGAZINA <> @pKoka OR tk2.IDKONFIGAMBJENTE IS NULL)'

		-- datat e kontrollit: veprimet me date dokumenti nga data e ketij veprimi (ose midis dy datave kur ndryshon data)
		if @MEDYDATA=0
			SET @filtriDates = N' AND lv.DTDOK >= @pD1'
		else if @KAPDOKMESDATAVE=1
		begin
			if @data1<@data2
				SET @filtriDates = N' AND lv.DTDOK >= @pD1 AND lv.DTDOK < @pD2'
			else
				SET @filtriDates = N' AND lv.DTDOK >= @pD2 AND lv.DTDOK < @pD1'
		end
		else
			SET @filtriDates = N' AND lv.DTDOK >= @pD2'

		if ( @ESHTEDALJE=1 and @SASIARTIKULL>0 ) Or ( @ESHTEDALJE=0 and @SASIARTIKULL<0 ) -- DALJE
		begin
			SET @pSasi = Abs(@SASIARTIKULL)
			SET @fushat = N'GJENDJE = ROUND((GJENDJE - @pSasi), 8)'
			SET @kushti2 = N'ROUND(GJENDJE - @pSasi, 8) < 0 OR GJENDJE <= 0'
			SET @meGjendjeFillestare = 1   -- dalja kerkon edhe gjendjen deri ne daten e veprimit
		end
		else -- HYRJE
		begin
			SET @pSasi = Abs(@SASIARTIKULL)
			if (@KAPDOKMESDATAVE=1 or @LLOJVEPRIMI=2)
				SET @pSasi = 0
			SET @fushat = N'GJENDJE = ROUND((@pSasi + GJENDJE), 8)'
			SET @kushti2 = N'ROUND(@pSasi + GJENDJE, 8) < 0'
		end

		SET @stmt = N'
		CREATE TABLE #KONFIGAMB (IDKONFIGAMBJENTE NUMERIC(18, 0) PRIMARY KEY)
		INSERT INTO #KONFIGAMB
		SELECT	DISTINCT T_KONFIGAMBJENTE.IDKONFIGAMBJENTE
		FROM	T_KONFIGAMBJENTE
				JOIN T_KUSHTEMPLATE on T_KONFIGAMBJENTE.IDKONFIGAMBJENTE = T_KUSHTEMPLATE.IDKONFIGAMBJENTE
				JOIN T_KUSHTE on T_KUSHTE.IDKUSHT = T_KUSHTEMPLATE.IDKUSHT and T_KUSHTE.KODI = ''PM''
		WHERE	T_KONFIGAMBJENTE.IDNDERMARJE = @pNderm
				AND T_KONFIGAMBJENTE.IDSTATUSDOK = 1
				AND T_KUSHTEMPLATE.vlera IN (SELECT IDALTERNATIVEKUSHTI FROM T_ALTERNATIVAKUSHTI WHERE ALTERNATIVA = ''Po'')

		SELECT	tm.DATA, tk.DTDOK, SASIA*KOEFICENTI*SHENJA AS q
		INTO	#lv
		FROM	T_TRUPIMAGAZINA tm
				INNER JOIN T_KOKAMAGAZINA tk ON tm.IDKOKAMAGAZINA = tk.IDKOKAMAGAZINA
				INNER JOIN #KONFIGAMB KONFIGAMB ON KONFIGAMB.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE
				LEFT OUTER JOIN t_kokamagazina mg2 ON mg2.idkokamagazina = tk.IDGJENERUES AND mg2.IDKONFIGAMBJENTE = tk.IDKONFIGGJENERUES
				LEFT OUTER JOIN t_konfigambjente tk2 ON tk.IDKONFIGGJENERUES = tk2.IDKONFIGAMBJENTE AND tk2.IDKATDOK = 6
		WHERE	' + @w + N'
		OPTION (LOOP JOIN)

		;WITH mv AS (
				SELECT DATA, SUM(q) AS q FROM #lv GROUP BY DATA
			), cum AS (
				SELECT DATA, SUM(q) OVER (ORDER BY DATA ROWS UNBOUNDED PRECEDING) AS g FROM mv
			)
		SELECT TOP(1) ' + @fushat + N', DATADOK = (CASE WHEN DATADOK = ''01/01/1900'' THEN @pData ELSE DATADOK END)
		FROM (
				SELECT	ROUND(ISNULL(cum.g, 0), 8) AS GJENDJE, datapas.myData AS DATADOK
				FROM	(SELECT DISTINCT lv.DATA AS myData FROM #lv lv WHERE 1=1' + @filtriDates + N') datapas
						INNER JOIN cum ON cum.DATA = datapas.myData
				' + CASE WHEN @meGjendjeFillestare = 1 THEN N'
				UNION ALL
				SELECT	ROUND(ISNULL(SUM(q), 0), 8) AS GJENDJE, ''01/01/1900'' AS DATADOK
				FROM	#lv
				WHERE	DATA <= @pData' ELSE N'' END + N'
			) gjendjet
		WHERE ' + @kushti2 + N'
		ORDER BY DATADOK ASC'

		EXEC sp_executesql @stmt,
			N'@pNderm numeric(18,0), @pMag numeric(18,0), @pArt numeric(18,0), @pDet numeric(18,0), @pKoka numeric(18,0), @pD1 datetime, @pD2 datetime, @pData datetime, @pSasi decimal(18,8)',
			@pNderm = @IDNDERM, @pMag = @IDMAG, @pArt = @IDARTIKULL, @pDet = @IDDETAJIM, @pKoka = @IDKOKAMAGAZINA,
			@pD1 = @pD1, @pD2 = @pD2, @pData = @pD1, @pSasi = @pSasi

		Set @Err = @@Error
		RETURN @Err
	end
	--FUND 1





	-- FILLO 2 : METODA FIFO
	else
	begin
		-- @data sherben qe te kapet gjendja deri ne daten e veprimit, qe nese nuk ka gjendje te mos behet dalje
		if @MEDYDATA=0
		begin
			Set @SqlWhereJoin = ' AND ' + @SqlWhere + ' AND tk.DTDOK>=convert(datetime, ''' + cast(@data1 as varchar(20)) + ''', 103) '  
			Set @data= 'convert(datetime, ''' + cast(@data1 as varchar(20)) + ''', 103)'
		end
		else
		begin
			if @data1<@data2
			begin
				Set @SqlWhereJoin = ' AND ' +  @SqlWhere + ' AND tk.DTDOK>=convert(datetime, ''' + cast(@data1 as varchar(20)) + ''', 103) '
				Set @data= 'convert(datetime, ''' + cast(@data1 as varchar(20)) + ''', 103)'
			end
			else
			begin
				Set @SqlWhereJoin = ' AND ' +  @SqlWhere + ' AND tk.DTDOK>=convert(datetime, ''' + cast(@data2 as varchar(20)) + ''', 103) '	
				Set @data= 'convert(datetime, ''' + cast(@data2 as varchar(20)) + ''', 103)' 
			end		
		end
		--------------------------------------------------------------------------------------------

		if ( @ESHTEDALJE=1 and @SASIARTIKULL>0 ) Or ( @ESHTEDALJE=0 and @SASIARTIKULL<0 ) 
		begin
			if @LLOJVEPRIMI=0
				begin
					set @SASIARTIKULL=Abs(@SASIARTIKULL)
					set @SqlFields = ' GJENDJE = ROUND(( GJENDJE - cast(' + cast( @SASIARTIKULL as varchar(100)) + ' as float) ),' + @roundVl + ') '
					set @SqlWhere2=  ' ROUND(GJENDJE - cast(' + cast( @SASIARTIKULL as varchar(100)) + ' as float),' + @roundVl + ') < 0 OR GJENDJE <= 0 '
				end
				if @LLOJVEPRIMI=1
				begin
					if @MEDYDATA=0
					begin
						set @SASIARTIKULL=@SASIARTIKULL - @SASIARTIKULLOLD
						if @SASIARTIKULL<0
						begin
							set @SASIARTIKULL=Abs(@SASIARTIKULL)
							set @SqlFields = 'GJENDJE=ROUND((GJENDJE + cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float)),' + @roundVl + ')'
							set @SqlWhere2= 'ROUND(GJENDJE + cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float),' + @roundVl + ') < 0 ' 
						end
						else
						begin
							set @SASIARTIKULL=Abs(@SASIARTIKULL)
							set @SqlFields = ' GJENDJE = ROUND(( GJENDJE - cast(' + cast( @SASIARTIKULL as varchar(100)) + ' as float) ),' + @roundVl + ') '
							set @SqlWhere2=  ' ROUND(GJENDJE - cast(' + cast( @SASIARTIKULL as varchar(100)) + ' as float),' + @roundVl + ') < 0 OR GJENDJE <= 0 '
						end 
					end
					else
					begin
						if @data1<@data2
						begin
							set @SqlFields = 'GJENDJE= ROUND((case when DATADOK < convert(datetime, ''' + cast(@data2 as varchar(20)) + ''', 103) then ( GJENDJE - cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)) else (GJENDJE + cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) - cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)) end),' + @roundVl + ')'
							set @SqlWhere2= ' (  DATADOK < convert(datetime, ''' + cast(@data2 as varchar(20)) + ''', 103) and (( GJENDJE - cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)) < 0 OR GJENDJE <= 0 )) or
												 (DATADOK >= convert(datetime, ''' + cast(@data2 as varchar(20)) + ''', 103) and (GJENDJE + cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) - cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float))  < 0 ) ' 
						end
						else
						begin
							set @SqlFields = 'GJENDJE= ROUND((GJENDJE + cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) - cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)),' + @roundVl + ')'
							set @SqlWhere2= ' ROUND((GJENDJE + cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) - cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)),' + @roundVl + ') < 0 ' 
						end
					end
				end
				if @LLOJVEPRIMI=2
				begin
					set @SASIARTIKULL=Abs(@SASIARTIKULL)
					set @SqlFields = 'GJENDJE=ROUND((GJENDJE + cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float)),' + @roundVl + ')'
					set @SqlWhere2= ' ROUND(GJENDJE + cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float),' + @roundVl + ') < 0 ' 
				end


			Set @SqlTableJoin='
				UNION ALL 

				SELECT	GJENDJE=SUM(GJENDJE), DATADOK=''01/01/1900''
				FROM
				(
					SELECT	GJENDJE=0
					UNION ALL
					SELECT	Top 1 ROUND(tm.SASIAPROGRESIVE, ' + @roundVl + ') as GJENDJE
					FROM	T_TRUPIMAGAZINA tm
							inner join 
							(	
								SELECT dokumentiFunditRresht.*
								FROM
								(
									SELECT	max(tm.data) as myData
									FROM	[T_KOKAMAGAZINA] tk INNER JOIN dbo.T_TRUPIMAGAZINA tm ON tk.IDKOKAMAGAZINA = tm.IDKOKAMAGAZINA						
											INNER JOIN #KONFIGAMB KONFIGAMB ON KONFIGAMB.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE 
											LEFT OUTER JOIN t_kokamagazina mg2 ON mg2.idkokamagazina=tk.IDGJENERUES AND mg2.IDKONFIGAMBJENTE=tk.IDKONFIGGJENERUES
											LEFT OUTER JOIN t_konfigambjente tk2 on tk.IDKONFIGGJENERUES=tk2.IDKONFIGAMBJENTE AND tk2.IDKATDOK=6
									WHERE	1=1 AND '  + @SqlWhere + '	and (tm.DATA <= ' + @data + ' ) 
								) dataVeprimit 

								inner Join
								(
									SELECT	distinct tm.data as myData, IDRENDITJES=max(tm.IDRENDITJES)
									FROM	[T_KOKAMAGAZINA] tk INNER JOIN dbo.T_TRUPIMAGAZINA tm ON tk.IDKOKAMAGAZINA = tm.IDKOKAMAGAZINA						
											INNER JOIN #KONFIGAMB KONFIGAMB ON KONFIGAMB.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE 
											LEFT OUTER JOIN t_kokamagazina mg2 ON mg2.idkokamagazina=tk.IDGJENERUES AND mg2.IDKONFIGAMBJENTE=tk.IDKONFIGGJENERUES
											LEFT OUTER JOIN t_konfigambjente tk2 on tk.IDKONFIGGJENERUES=tk2.IDKONFIGAMBJENTE AND tk2.IDKATDOK=6
									WHERE	1=1 AND '  + @SqlWhere + '	and (tm.DATA <= ' + @data + ' ) 
									GROUP BY tm.data
								) dokumentiFundit on dataVeprimit.myData=dokumentiFundit.myData

								inner Join
								(
									SELECT	distinct tm.data as myData, tm.IDRENDITJES,IDTRUPIMAGAZINA=max(tm.IDTRUPIMAGAZINA)
									FROM	[T_KOKAMAGAZINA] tk INNER JOIN dbo.T_TRUPIMAGAZINA tm ON tk.IDKOKAMAGAZINA = tm.IDKOKAMAGAZINA						
											INNER JOIN #KONFIGAMB KONFIGAMB ON KONFIGAMB.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE 
											LEFT OUTER JOIN t_kokamagazina mg2 ON mg2.idkokamagazina=tk.IDGJENERUES AND mg2.IDKONFIGAMBJENTE=tk.IDKONFIGGJENERUES
											LEFT OUTER JOIN t_konfigambjente tk2 on tk.IDKONFIGGJENERUES=tk2.IDKONFIGAMBJENTE AND tk2.IDKATDOK=6
									WHERE	1=1 AND '  + @SqlWhere + '	and (tm.DATA <= ' + @data + ' ) 
									GROUP BY tm.data,tm.IDRENDITJES
								) dokumentiFunditRresht on dataVeprimit.myData=dokumentiFunditRresht.myData and dokumentiFundit.IDRENDITJES=dokumentiFunditRresht.IDRENDITJES
							) datapas  on datapas.myData=tm.DATA and datapas.IDRENDITJES=tm.IDRENDITJES  and datapas.IDTRUPIMAGAZINA=tm.IDTRUPIMAGAZINA 
				)TotFill'
		end
		else
		begin
			if @LLOJVEPRIMI=0
			begin
				set @SASIARTIKULL=Abs(@SASIARTIKULL)
				set @SqlFields = 'GJENDJE=ROUND((GJENDJE + cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float)),' + @roundVl + ')'
				set @SqlWhere2= ' GJENDJE<0 ' 
			end
			if @LLOJVEPRIMI=1
			begin
				if @MEDYDATA=0
				begin
					set @SASIARTIKULL=@SASIARTIKULL - @SASIARTIKULLOLD
					if @SASIARTIKULL<=0
					begin
						set @SASIARTIKULL=Abs(@SASIARTIKULL)
						set @SqlFields = 'GJENDJE=ROUND((GJENDJE - cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float)),' + @roundVl + ')'
						set @SqlWhere2= ' GJENDJE - cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float) < 0 '
					end
					else
					begin
						set @SASIARTIKULL=Abs(@SASIARTIKULL)
						set @SqlFields = 'GJENDJE=ROUND((GJENDJE + cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float)),' + @roundVl + ')'
						set @SqlWhere2= ' GJENDJE<0 '
					end 
				end
				else
				begin
					if @data1>@data2
					begin
						set @SqlFields = 'GJENDJE= ROUND((case when DATADOK < convert(datetime, ''' + cast(@data1 as varchar(20)) + ''', 103) then (GJENDJE - cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float)) else (GJENDJE - cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) + cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)) end ),' + @roundVl + ')'
						set @SqlWhere2= ' (DATADOK < convert(datetime, ''' + cast(@data1 as varchar(20)) + ''', 103) and (GJENDJE - cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float)) <0 ) or
											(DATADOK >= convert(datetime, ''' + cast(@data1 as varchar(20)) + ''', 103) and (GJENDJE - cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) + cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float) < 0)) '
					end
					else
					begin
						set @SqlFields = 'GJENDJE= ROUND((GJENDJE - cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) + cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)),' + @roundVl + ')'
						set @SqlWhere2= '  ROUND((GJENDJE - cast(' + cast(Abs(@SASIARTIKULLOLD) as varchar(100)) + ' as float) + cast(' + cast(Abs(@SASIARTIKULL) as varchar(100)) + ' as float)),' + @roundVl + ') < 0 ' 
					end

				end --End me dy data
			end
			if @LLOJVEPRIMI=2
			begin
				set @SASIARTIKULL=Abs(@SASIARTIKULL)
				set @SqlFields = 'GJENDJE=ROUND((GJENDJE - cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float)),' + @roundVl + ')'
				set @SqlWhere2= ' ROUND(GJENDJE - cast(' + cast(@SASIARTIKULL as varchar(100)) + ' as float),' + @roundVl + ') < 0 ' 
			end
		end	

		--1. 'dataVeprimit'				kap te gjithe datat e dokumentave per te cilat artikulli ka veprime te mevonshme ; myData
		--2. 'dokumentiFundit'			kap veprimin e fundit te dites, per cdo date qe ka veprime ; IDRENDITJES
		--3. 'dokumentiFunditRresht'	kap rreshtin e fundit te dokumentit te fundit (nese artikulli eshte vendosur me shume se nje here ne trup) ; IDTRUPIMAGAZINA
		Set @SqlSelect=@SqlSelect+'
		SELECT TOP(1) ' + @SqlFields + ',DATADOK=(case when DATADOK=''01/01/1900'' then ' + @data + ' else DATADOK end)
		FROM ( 
				SELECT	ROUND(tm.SASIAPROGRESIVE,' + @roundVl + ') as GJENDJE, datapas.myData as DATADOK
				FROM	T_TRUPIMAGAZINA tm
						inner join 
						(	
							SELECT dokumentiFunditRresht.*
							FROM
							(
								SELECT	distinct tm.data as myData
								FROM	[T_KOKAMAGAZINA] tk INNER JOIN dbo.T_TRUPIMAGAZINA tm ON tk.IDKOKAMAGAZINA = tm.IDKOKAMAGAZINA						
										INNER JOIN #KONFIGAMB KONFIGAMB ON KONFIGAMB.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE 
										LEFT OUTER JOIN t_kokamagazina mg2 ON mg2.idkokamagazina=tk.IDGJENERUES AND mg2.IDKONFIGAMBJENTE=tk.IDKONFIGGJENERUES
										LEFT OUTER JOIN t_konfigambjente tk2 on tk.IDKONFIGGJENERUES=tk2.IDKONFIGAMBJENTE AND tk2.IDKATDOK=6
								WHERE	1=1 ' + @SqlWhereJoin + '
							) dataVeprimit 

							inner Join
							(
								SELECT	distinct tm.data as myData, IDRENDITJES=max(tm.IDRENDITJES)
								FROM	[T_KOKAMAGAZINA] tk INNER JOIN dbo.T_TRUPIMAGAZINA tm ON tk.IDKOKAMAGAZINA = tm.IDKOKAMAGAZINA					
										INNER JOIN #KONFIGAMB KONFIGAMB ON KONFIGAMB.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE 
										LEFT OUTER JOIN t_kokamagazina mg2 ON mg2.idkokamagazina=tk.IDGJENERUES AND mg2.IDKONFIGAMBJENTE=tk.IDKONFIGGJENERUES
										LEFT OUTER JOIN t_konfigambjente tk2 on tk.IDKONFIGGJENERUES=tk2.IDKONFIGAMBJENTE AND tk2.IDKATDOK=6
								WHERE	1=1 ' + @SqlWhereJoin + '
								GROUP BY tm.data
							) dokumentiFundit on dataVeprimit.myData=dokumentiFundit.myData

							inner Join
							(
								SELECT	distinct tm.data as myData, tm.IDRENDITJES,IDTRUPIMAGAZINA=max(tm.IDTRUPIMAGAZINA)
								FROM	[T_KOKAMAGAZINA] tk INNER JOIN dbo.T_TRUPIMAGAZINA tm ON tk.IDKOKAMAGAZINA = tm.IDKOKAMAGAZINA					
										INNER JOIN #KONFIGAMB KONFIGAMB ON KONFIGAMB.IDKONFIGAMBJENTE = tk.IDKONFIGAMBJENTE 
										LEFT OUTER JOIN t_kokamagazina mg2 ON mg2.idkokamagazina=tk.IDGJENERUES AND mg2.IDKONFIGAMBJENTE=tk.IDKONFIGGJENERUES
										LEFT OUTER JOIN t_konfigambjente tk2 on tk.IDKONFIGGJENERUES=tk2.IDKONFIGAMBJENTE AND tk2.IDKATDOK=6
								WHERE	1=1 ' + @SqlWhereJoin + '
								GROUP BY tm.data,tm.IDRENDITJES
							) dokumentiFunditRresht on dataVeprimit.myData=dokumentiFunditRresht.myData and dokumentiFundit.IDRENDITJES=dokumentiFunditRresht.IDRENDITJES

						) datapas  on datapas.myData=tm.DATA and datapas.IDRENDITJES=tm.IDRENDITJES  and datapas.IDTRUPIMAGAZINA=tm.IDTRUPIMAGAZINA 
				'+ @SqlTableJoin + '
				
			) gjendjet 

		WHERE ' + @SqlWhere2 + '
		ORDER BY DATADOK ASC '
	end
	-- FUND 2

	exec(@SqlSelect)

	Set @Err = @@Error
	RETURN @Err
End
GO
