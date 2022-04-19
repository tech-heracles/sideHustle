IF OBJECT_ID('PRC_RAP_LIBERBLERJE2019') IS NOT NULL
	DROP PROCEDURE PRC_RAP_LIBERBLERJE2019
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_RAP_LIBERBLERJE2019]
(
	@filterDtDok VARCHAR(max),
	@filterNumerDokumenti VARCHAR(max),
	@filterFurnitor VARCHAR(max),
	@filterNumerSerial VARCHAR(max),
	@filterLlojDokumenti VARCHAR(max),
	@filterQyteti VARCHAR(max),
	@filterPikeshitjeFurnizmi VARCHAR(max),
	@idndermarje varchar(20),
	@idPerdoruesi VARCHAR(100),
	@IdRaport VARCHAR(100),
	@filterDtDokFLD varchar(max),
	@filterNrDokFLD  varchar(max),
	@filterGrupimPKF VARCHAR(max),
	@filterGrupimDKF VARCHAR(max),
	@filterGrupimTKF VARCHAR(max),
	@filterDegeAdministrative VARCHAR(max),
	@shikoGjitheDokumentat VARCHAR(20),
	@filterMuajRaportimi VARCHAR(max),
	@filterVitRaportimi VARCHAR(max),
	@filterMagazina VARCHAR(max),
	@filterGrupimDokP VARCHAR(max),
	@filterGrupimDokD VARCHAR(max),
	@filterGrupimDokT VARCHAR(max)
)
AS
SET QUOTED_IDENTIFIER OFF

declare @STRFUSHAT varchar(max)
declare @SQTQUERY varchar(MAX)
declare @SQTQUERY2 varchar(MAX)
declare @strquery3 varchar(MAX)
declare @SQTQUERY4 varchar(MAX)
declare @SQTQUERY5 varchar(MAX)
declare @SQTQUERY6 varchar(MAX)
declare @STRFILTER VARCHAR(MAX)
declare @STRFILTER2 VARCHAR(MAX)
DECLARE @vleftaXImporte VARCHAR(max)
DECLARE @vleftaXImporte1 VARCHAR(max)
--shtuar variabli @norows qe do perdoret tek ndermarjet ne rastin ku eshte nderamarje e vetme ose edhe raportuese te meret per te gjithe ndermarjet raportues veprimet 
declare @norows INT
declare @ndermarjet varchar (MAX)

set @ndermarjet = ' CREATE TABLE #ndermarjet(id int primary key) '

if (@filterGrupimPKF = '')
	set @filterGrupimPKF = '' 
else
	set @filterGrupimPKF = ' T_KLIENTFURNITOR.GRUPIM1KF IN	(
																SELECT	IDGRUPI
																FROM	T_GRUPEKF
																WHERE	' + @filterGrupimPKF + ' IDSTATUSDOK <> 2 
																		AND LLOJKODIFIKIMI = 1
															)	AND '
		
if (@filterGrupimDKF = '')
	set @filterGrupimDKF = '' 
else
	set @filterGrupimDKF = ' T_KLIENTFURNITOR.GRUPIM2KF IN	(
																SELECT	IDGRUPI
																FROM	T_GRUPEKF
																WHERE	' + @filterGrupimDKF + ' IDSTATUSDOK <> 2 
																		AND LLOJKODIFIKIMI = 2
															)	AND '

if (@filterGrupimTKF = '')
	set @filterGrupimTKF = '' 
else
	set @filterGrupimTKF = ' T_KLIENTFURNITOR.GRUPIM3KF IN	(
																SELECT	IDGRUPI
																FROM	T_GRUPEKF
																WHERE	' + @filterGrupimTKF + ' IDSTATUSDOK <> 2 
																		AND LLOJKODIFIKIMI = 3
															)	AND '		
		
SET @STRFILTER = ' 1 = 1 AND ' + @filterDtDok + @filterFurnitor + @filterNumerDokumenti + @filterNumerSerial + @filterLlojDokumenti + @filterQyteti + @filterPikeshitjeFurnizmi + @filterGrupimPKF +@filterGrupimDKF +@filterGrupimTKF
+@filterGrupimDokP+@filterGrupimDokD+@filterGrupimDokT + @filterDegeAdministrative +@filterMagazina+ @filterMuajRaportimi + @filterVitRaportimi + ' 1=1'

set @STRFILTER2 = '1 = 1 AND '+ @filterDtDokFLD + @filterFurnitor + @filterNrDokFLD + @filterNumerSerial + @filterLlojDokumenti + @filterQyteti + @filterPikeshitjeFurnizmi +  @filterGrupimPKF +@filterGrupimDKF +@filterGrupimTKF
+@filterGrupimDokP+@filterGrupimDokD+@filterGrupimDokT + @filterDegeAdministrative + @filterMuajRaportimi + @filterVitRaportimi + @filterMagazina+' 1=1'

set @STRFUSHAT = (SELECT kolona = CAST(
(SELECT T_EMERLOGJIKTABELE.EMERREALTABELE  +'.'+T_EMERLOGJIKKOLONE.EMERREALKOLONE +','   FROM (select *  from T_RAPTRUPI where T_RAPTRUPI.IDRAP = @IdRaport ) as T_RAPTRUPI   inner join T_EMERLOGJIKKOLONE  on T_RAPTRUPI.IDKOLONE = T_EMERLOGJIKKOLONE.IDKOLONE
inner join T_EMERLOGJIKTABELE on T_EMERLOGJIKKOLONE.IDTABELE = T_EMERLOGJIKTABELE.IDTABELE 
 FOR XML PATH('') ) AS VARCHAR(MAX)) )

 --shtuar per te mare ndermarjet raportuse

set @ndermarjet = @ndermarjet + 'Insert into #ndermarjet select * from dbo.getLeafsNdermarjeRaportuese ('+ @IdNdermarje+' )'
	
set @strquery3 =  @ndermarjet + '
	SELECT	nrdok
			,nrserial
			,dtdok
			,EMERTIMIKF
			,EMERTIMFATURE		
			,QYTETIEMRI
			,NIPTKF
			,(round(sum(BLERJEPERJASHTUAR),0)+round(sum(BLERJEAQTPERJASHTUAR),0)+round(sum(importeaqtperjashtuar),0)+round(sum(importeperjashtuar),0)+ round(sum(vlefta20importe),0)+ round(sum(tvsh20importe),0)+ round(sum(vlefta10importe),0)+ round(sum(tvsh10importe),0)+ round(sum(vlefta6importe),0)+ round(sum(tvsh6importe),0)+round(sum(VLEFTAAQTIMPORTE),0)+ round(sum(TVSHAQTIMPORTE),0)+round(sum(VLEFTA20),0)+ round(sum(TVSH20),0)+round(sum(VLEFTA10),0)+ round(sum(TVSH10),0)+round(sum(VLEFTA6),0)+ round(sum(TVSH6),0)+ round(sum(VLEFTAAQT20),0)+round( sum(TVSHAQT20),0)+round(sum(VLEFTAFERMER20),0)+ round(sum(TVSHFERMER20),0)+round(sum(VLEFTAAUTONGARKESE),0)+ round(sum(TVSHAUTONGARKESE),0))  totalimonbaze
			,round(sum(BLERJEPERJASHTUAR),0) blerjeperjashtuar
			,round(sum(BLERJEAQTPERJASHTUAR),0) blerjeaqtperjashtuar
			,round( sum(importeaqtperjashtuar),0) importeaqtperjashtuar
			,round(sum(importeperjashtuar),0) importeperjashtuar
			,round(sum(vlefta20importe), 0) vlefta20importe
			,round(sum(tvsh20importe), 0) tvsh20importe
			,round(sum(vlefta10importe), 0) vlefta10importe
			,round(sum(tvsh10importe), 0) tvsh10importe
			,round(sum(vlefta6importe), 0) vlefta6importe
			,round(sum(tvsh6importe), 0) tvsh6importe
			,round(sum(VLEFTAAQTIMPORTE), 0) VLEFTAAQTIMPORTE
			,round(sum(TVSHAQTIMPORTE),0) TVSHAQTIMPORTE
			,round(sum(VLEFTA20),0) VLEFTA20
			,round(sum(TVSH20),0) TVSH20
			,round(sum(VLEFTA10),0) VLEFTA10
			,round(sum(TVSH10),0) TVSH10
			,round(sum(VLEFTA6),0) VLEFTA6
			,round(sum(TVSH6),0) TVSH6
			,round(sum(VLEFTAAQT20),0) VLEFTAAQT20
			,round(sum(TVSHAQT20),0) TVSHAQT20
			,round(sum(VLEFTAFERMER20),0) VLEFTAFERMER20
			,round(sum(TVSHFERMER20),0) TVSHFERMER20 
			,round(sum(VLEFTAAUTONGARKESE),0) VLEFTAAUTONGARKESE
			,round(sum(TVSHAUTONGARKESE),0) TVSHAUTONGARKESE 
			,sum(TOTALIRREGULLIME) TOTALIRREGULLIME	
			,sum(TVSHRREGULLIME) TVSHRREGULLIME
			,sum(TOTALIBORXHI) TOTALIBORXHI
			,sum(TVSHBORXHI) TVSHBORXHI 
			,max(NDERMARJEPERSHK)  NDERMARJEPERSHK
			,MAX(PERSHKRIMI) PERSHKRIMI
			,NRREF
	FROM	( ' 

set @SQTQUERY ='
				SELECT	t_kokashitje.idshitjekoka
						,T_KOKASHITJE.NRDOK
						,case when T_KOKASHITJE.NIVF<>'''' THEN T_KOKASHITJE.NIVF ELSE  T_KOKASHITJE.NRSERIAL END NRSERIAL
						,T_KOKASHITJE.DTDOK
						,T_KOKASHITJE.TOTALIMETVSHMEZBRITJE
						,T_KLIENTFURNITOR.EMERTIMIKF
						,T_KLIENTFURNITOR.EMERTIMFATURE
						,T_KLIENTFURNITOR.NIPTKF
						,T_KLIENTFURNITOR.QYTETIKF
						,T_QYTETI.QYTETIEMRI
						,T_KOKASHITJE.TOTALIMETVSHMEZBRITJE * T_KOKASHITJE.KURSI AS TOTALIMONBAZE
						,MONTH(T_KOKASHITJE.DTDOK) AS PERIUDHA
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) = 0 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.FERMER = 0 
															AND AUTONGARKESE = 0  
															AND (
																	T_ARTIKULLI.LLOJIART = 0 
																	OR T_LLOGARI.NRLLOGARI NOT LIKE ''2%''
																)  
													THEN	T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	BLERJEPERJASHTUAR
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) = 0 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.FERMER = 0 
															AND AUTONGARKESE = 0  
															AND (
																	T_ARTIKULLI.LLOJIART = 1 
																	OR T_LLOGARI.NRLLOGARI  LIKE ''2%''
																)  
													THEN	T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	BLERJEAQTPERJASHTUAR
						,0 as importeaqtperjashtuar
						,0 as importeperjashtuar
						,0 AS VLEFTA20IMPORTE
						,0 AS TVSH20IMPORTE
						,0 AS VLEFTA10IMPORTE
						,0 AS TVSH10IMPORTE
						,0 AS VLEFTA6IMPORTE
						,0 AS TVSH6IMPORTE
						,0 AS VLEFTAAQTIMPORTE
						,0 AS TVSHAQTIMPORTE
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) > 0 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) >= 0.2 
															AND T_KOKASHITJE.DOGANA = 0 
															AND (
																	T_ARTIKULLI.LLOJIART = 0 
																	OR T_LLOGARI.NRLLOGARI NOT LIKE ''2%''
																) 
															and T_KLIENTFURNITOR.FERMER = 0  
															AND AUTONGARKESE = 0 
													THEN	T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	VLEFTA20
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) > 0 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH),2) >= 0.2 
															AND T_KOKASHITJE.DOGANA = 0 
															AND (
																	T_ARTIKULLI.LLOJIART = 0 
																	OR T_LLOGARI.NRLLOGARI NOT LIKE ''2%''
																)
															and T_KLIENTFURNITOR.FERMER = 0 
															AND AUTONGARKESE = 0 
													THEN	(T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI -	CASE
																																					WHEN	T_KOKASHITJE.TOTALI <> 0
																																					THEN	(Round(T_KOKASHITJE.ZBRITJE / T_KOKASHITJE.TOTALI, 4) * ((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI)) 
																																					ELSE	0
																																				END
													ELSE	0 
												END
											)
								END
							)	TVSH20
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) > 0 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) >= 0.1 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) < 0.2 
															AND T_KOKASHITJE.DOGANA = 0 
															AND (
																	T_ARTIKULLI.LLOJIART = 0 
																	OR T_LLOGARI.NRLLOGARI NOT LIKE ''2%''
																) 
															and T_KLIENTFURNITOR.FERMER = 0  
															AND AUTONGARKESE = 0 
													THEN	T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	VLEFTA10
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) > 0 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) >= 0.1 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) < 0.2 
															AND T_KOKASHITJE.DOGANA = 0 
															AND (
																	T_ARTIKULLI.LLOJIART = 0 
																	OR T_LLOGARI.NRLLOGARI NOT LIKE ''2%''
																)
															and T_KLIENTFURNITOR.FERMER = 0 
															AND AUTONGARKESE = 0 
													THEN	(T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI -	CASE
																																					WHEN	T_KOKASHITJE.TOTALI <> 0
																																					THEN	(Round(T_KOKASHITJE.ZBRITJE / T_KOKASHITJE.TOTALI, 4) * ((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI)) 
																																					ELSE	0
																																				END
													ELSE	0 
												END
											)
								END
							)	TVSH10 '
	set @SQTQUERY2 = '
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) > 0 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) > 0.01 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) < 0.1 
															AND T_KOKASHITJE.DOGANA = 0 
															AND (
																	T_ARTIKULLI.LLOJIART = 0 
																	OR T_LLOGARI.NRLLOGARI NOT LIKE ''2%''
																) 
															and T_KLIENTFURNITOR.FERMER = 0  
															AND AUTONGARKESE = 0 
													THEN	T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	VLEFTA6
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH) > 0 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH),2) > 0.01 
															AND round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) < 0.1 
															AND T_KOKASHITJE.DOGANA = 0 
															AND (
																	T_ARTIKULLI.LLOJIART = 0 
																	OR T_LLOGARI.NRLLOGARI NOT LIKE ''2%''
																)
															and T_KLIENTFURNITOR.FERMER = 0 
															AND AUTONGARKESE = 0 
													THEN	(T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI -	CASE
																																					WHEN	T_KOKASHITJE.TOTALI <> 0
																																					THEN	(Round(T_KOKASHITJE.ZBRITJE / T_KOKASHITJE.TOTALI, 4) * ((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI)) 
																																					ELSE	0
																																				END
													ELSE	0 
												END
											)
								END
							)	TVSH6
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) > 0.01 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.FERMER = 0 
															AND AUTONGARKESE = 0 
															AND	(
																	T_ARTIKULLI.LLOJIART = 1 
																	OR T_LLOGARI.NRLLOGARI  LIKE ''2%''
																) 
													THEN	(T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI) -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	VLEFTAAQT20
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) > 0.01 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.FERMER = 0 
															AND AUTONGARKESE = 0 
															AND (
																	T_ARTIKULLI.LLOJIART = 1 
																	OR T_LLOGARI.NRLLOGARI LIKE ''2%''
																) 
													THEN	(T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI -	CASE
																																					WHEN	T_KOKASHITJE.TOTALI <> 0
																																					THEN	(Round(T_KOKASHITJE.ZBRITJE / T_KOKASHITJE.TOTALI, 4) * ((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI)) 
																																					ELSE	0
																																				END
													ELSE	0 
												END
											)
								END
							)	TVSHAQT20
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) > 0.01 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.FERMER = 1 
															AND AUTONGARKESE = 0 
													THEN	(T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI) -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	VLEFTAFERMER20
						,SUM(
								CASE	
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH), 2) > 0.01 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.FERMER = 1 
															AND AUTONGARKESE = 0 
													THEN	(T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI -	CASE
																																					WHEN	T_KOKASHITJE.TOTALI <> 0
																																					THEN	(Round(T_KOKASHITJE.ZBRITJE / T_KOKASHITJE.TOTALI, 4) * ((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI)) 
																																					ELSE	0
																																				END
													ELSE	0 
												END
											)
								END
							)	TVSHFERMER20
						,SUM(	
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH),2) > 0.01 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.AUTONGARKESE = 1 
													THEN	(T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI) -	CASE
																													WHEN	T_KOKASHITJE.TOTALI <> 0
																													THEN	(Round(T_KOKASHITJE.ZBRITJE/T_KOKASHITJE.TOTALI, 4) * (T_TRUPISHITJE.VLEFTAPATVSH * T_KOKASHITJE.KURSI))
																													ELSE	0
																												END
													ELSE	0 
												END
											)
								END
							)	VLEFTAAUTONGARKESE
						,SUM(
								CASE 
									WHEN	T_TRUPISHITJE.VLEFTAPATVSH = 0 
									THEN	0 
									ELSE	(
												CASE 
													WHEN	round(((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) / T_TRUPISHITJE.VLEFTAPATVSH),2) > 0.01 
															AND T_KOKASHITJE.DOGANA = 0 
															and T_KLIENTFURNITOR.AUTONGARKESE = 1  
													THEN	(T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI -	CASE
																																					WHEN	T_KOKASHITJE.TOTALI <> 0
																																					THEN	(Round(T_KOKASHITJE.ZBRITJE / T_KOKASHITJE.TOTALI, 4) * ((T_TRUPISHITJE.VLEFTAMETVSH - T_TRUPISHITJE.VLEFTAPATVSH) * T_KOKASHITJE.KURSI)) 
																																					ELSE	0
																																				END
													ELSE	0 
												END
											)
								END
							)	TVSHAUTONGARKESE
						,0 AS TOTALIRREGULLIME
						,0 AS TVSHRREGULLIME
						,0 AS TOTALIBORXHI
						,0 AS TVSHBORXHI  
						,T_DEGEADMINISTRATIVE.PERSHKRIMI as degaadministrative 
						,T_NDERMARJE.NDERMARJEPERSHK
						,T_KOKASHITJE.PERSHKRIMI
						,CASE T_KOKAFLETEKONTABEL.IDSTATUSDOK 
							WHEN	0 
							THEN	'''' 
							ELSE	cast(NRREFERENCEKOKAFLETEKONTABEL as varchar(20)) 
						END AS NRREF '
   		
set @SQTQUERY4 = '
				FROM	T_TRUPISHITJE 
						INNER JOIN T_KOKASHITJE 
							ON T_TRUPISHITJE.IDSHITJEKOKA = T_KOKASHITJE.IDSHITJEKOKA
						INNER JOIN T_KLIENTFURNITOR 
							ON T_KOKASHITJE.IDKLIENTFURNITOR = T_KLIENTFURNITOR.IDKLIENTFURNITOR
						INNER JOIN T_KONFIGAMBJENTE 
							ON T_KOKASHITJE.IDKONFIGAMBJENTE = T_KONFIGAMBJENTE.IDKONFIGAMBJENTE
						INNER JOIN	(
										SELECT	DISTINCT T_KOKASHITJE.IDSHITJEKOKA 
										FROM	T_KOKASHITJE 
												join #ndermarjet 
													on  dbo.T_KOKASHITJE.IDNDERM=#ndermarjet.id
												INNER JOIN	(
																SELECT	DISTINCT IDNIVEL 
																FROM	T_NIVELREGJISTRIMI 
																		INNER JOIN T_KATEGORINIVELDOK 
																			ON T_NIVELREGJISTRIMI.IDKATDOK = T_KATEGORINIVELDOK.IDKATDOK
																		join #ndermarjet 
																			on T_NIVELREGJISTRIMI.IDNDERMARRJE=#ndermarjet.id
																WHERE	T_KATEGORINIVELDOK.PERSHKRIMI_sq = ''Blerje'' 
																		AND T_NIVELREGJISTRIMI.KODI = ''FB''
															)	niveli 
													ON T_KOKASHITJE.IDNIVEL = niveli.IDNIVEL
												
									
									)	existT 
							ON  T_TRUPISHITJE.IDSHITJEKOKA= existT.IDSHITJEKOKA
						left join	t_grupimdokumentikoka as gd1 
							on gd1.idgrupimkoka=t_kokashitje.idgrup1
						left join	t_grupimdokumentikoka as gd2 
							on gd2.idgrupimkoka=t_kokashitje.idgrup2
						left join	t_grupimdokumentikoka as gd3 
							on gd3.idgrupimkoka=t_kokashitje.idgrup3
						LEFT JOIN	T_QYTETI 
							ON T_QYTETI.IDQYTETI = CONVERT(INT,T_KLIENTFURNITOR.QYTETIKF)
						left join	T_DEGEADMINISTRATIVE 
							ON T_DEGEADMINISTRATIVE.IDDEGEADMINISTRATIVE = T_KOKASHITJE.IDDEGEADMINISTRATIVE
						left join	T_NJESIADMINISTRATIVE 
							on T_trupishitje.idmagazina=T_NJESIADMINISTRATIVE.IDNJESIADM
						left join	T_PIKESHITJEFURNIZIMI 
							ON T_PIKESHITJEFURNIZIMI.IDPIKESHITJEFURNIZIMI = T_KOKASHITJE.IDPIKESHITJEFURNIZIMI
 						LEFT JOIN	T_ARTIKULLI 
							ON	T_ARTIKULLI.IDARTIKULLI=T_TRUPISHITJE.IDKODI 
								AND T_TRUPISHITJE.IDLLOJVEPRIMI=1
						LEFT JOIN	T_LLOGARI 
							ON	T_LLOGARI.IDLLOGARI=T_TRUPISHITJE.IDKODI 
								AND T_TRUPISHITJE.IDLLOJVEPRIMI=3
						LEFT JOIN	T_KOKAFLETEKONTABEL 
							ON	T_KOKASHITJE.IDSHITJEKOKA = T_KOKAFLETEKONTABEL.IDGJENERUES 
								AND T_KOKASHITJE.IDKONFIGAMBJENTE = T_KOKAFLETEKONTABEL.IDKONFIGGJENERUES
						--shtuar left join per te kapur ndermarjepershk 
						left join	T_NDERMARJE 
							ON T_NDERMARJE.IDNDERMARJE =  T_KOKASHITJE.IDNDERM
				WHERE	t_kokashitje.idstatusdok=1 
 						AND (CASE WHEN ' + @shikoGjitheDokumentat + '= 0 THEN T_KOKASHITJE.IDKRIJUESI ELSE ' + @idPerdoruesi + ' END) = ' + @idPerdoruesi + '
						AND t_kokashitje.dogana=0 
						AND T_KOKASHITJE.IDSTATUSDOK = 1 AND ' + @STRFILTER + ' 
				GROUP BY t_kokashitje.idshitjekoka,T_KOKASHITJE.NRDOK, T_KOKASHITJE.NRSERIAL, T_KOKASHITJE.DTDOK, T_KLIENTFURNITOR.EMERTIMIKF, T_KLIENTFURNITOR.EMERTIMFATURE, T_KLIENTFURNITOR.QYTETIKF, T_KLIENTFURNITOR.NIPTKF, T_KOKASHITJE.TOTALIMETVSHMEZBRITJE, T_KONFIGAMBJENTE.KODKONFIGAMBJENTE, T_KOKASHITJE.KURSI, T_QYTETI.QYTETIEMRI,T_DEGEADMINISTRATIVE.PERSHKRIMI , T_ARTIKULLI.LLOJIART ,T_NDERMARJE.NDERMARJEPERSHK ,T_KOKASHITJE.PERSHKRIMI, NRREFERENCEKOKAFLETEKONTABEL,T_KOKAFLETEKONTABEL.IDSTATUSDOK, NIVF '

SET @vleftaXImporte = ' *max(fk.kursi),0)+(max(fk. VLTRANSPORT)+max( fk.VLSIGURACION)+max(fk. VLTJERA)+(ISNULL(MAX(FLETDOGANORETAKSA.vlefta),0)))* (CASE WHEN((ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0) )=0) '						
SET @vleftaXImporte1 = ' *max(fk.kursi),0)+(max(fk. VLTRANSPORT)+max( fk.VLSIGURACION)+max(fk. VLTJERA)+(ISNULL(MAX(FLETDOGANORETAKSATOT.vlefta),0)))* (CASE WHEN((ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0) )=0) '

SET @SQTQUERY5 = '
				UNION ALL

				SELECT	MAX(t_kokashitje.idshitjekoka)
						,max(fk.nrdok)
						,(max(fk.nrdok)) as nrserial
						,max( fk.dtdok)
						,max(fk.VLDOGANIM)
						,max(T_KLIENTFURNITOR.EMERTIMIKF)
						,max(T_KLIENTFURNITOR.EMERTIMFATURE)
						,max(T_KLIENTFURNITOR.NIPTKF)
						,MAX(T_KLIENTFURNITOR.QYTETIKF)
						,MAX(T_QYTETI.QYTETIEMRI)
						,max(fk.VLDOGANIM) as totalimonbaze
						,MONTH(max(fk.DTDOK)) AS PERIUDHA 
						,0 BLERJEPERJASHTUAR
						,0 BLERJEAQTPERJASHTUAR
						,case 
							when	(max(t.normaperqindje) = 0 and ft.aqt = 1) 
							then	isnull(max(SHUMAVLEFTA0AQT.VLEFTAFATURUAR) ' + @vleftaXImporte1 + ' THEN	0 
																										ELSE	isnull(max(SHUMAVLEFTA0AQT.VLEFTAFATURUAR),0) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar), 0)) 
																									end) 
							else	0 
						end +	case 
									when	max (t.normaperqindje) > 0 
											and ft.aqt = 1 
									then	isnull((
														CASE 
															WHEN	(ISNULL(MAX(FLETDOGANORETVSH.vfaturuar), 0)) = 0 
															THEN	0 
															ELSE	((ISNULL(MAX(FLETDOGANORETAKSA2.VLEFTA2),0)) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) * max(SHUMAVLEFTA20AQT.VLEFTAFATURUAR))
														end
													), 0) 
									else	0 
								end as importeaqtperjashtuar 
						,case 
								when	(max (t.normaperqindje) = 0 and ft.aqt=0) 
								then	isnull(max(SHUMAVLEFTA0.VLEFTAFATURUAR) ' + @vleftaXImporte1 + '	THEN	0 
																								ELSE	isnull(max(SHUMAVLEFTA0.VLEFTAFATURUAR),0) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0))
																									end) 
							else	0 
						end +		case 
									when	(max (t.normaperqindje) >=20 
											and ft.aqt=0) 
									then	isnull((
														CASE	
															WHEN	(ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0) ) = 0 
															THEN	0 
															ELSE	((ISNULL(MAX(FLETDOGANORETAKSA2.VLEFTA2),0)) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) * max(shumavlefta20) )
														end 
													), 0)
									else 0
									 end +	    case 
												when	(max (t.normaperqindje) >=10 and max (t.normaperqindje)<20
														and ft.aqt=0) 
												then	isnull((
																	CASE	
																		WHEN	(ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0) ) = 0 
																		THEN	0 
																		ELSE	((ISNULL(MAX(FLETDOGANORETAKSA2.VLEFTA2),0)) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) * max(shumavlefta10) )
																	end 
																), 0)
							        else 0 
									end +   case 
											when	(max (t.normaperqindje) >=6 and max (t.normaperqindje)<10
													and ft.aqt=0) 
											then	isnull((
																CASE	
																	WHEN	(ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0) ) = 0 
																	THEN	0 
																	ELSE	((ISNULL(MAX(FLETDOGANORETAKSA2.VLEFTA2),0)) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) * max(shumavlefta6) )
																end 
															), 0)
										else 0
								end as importeperjashtuar
					
						,case	
							when	(max(t.normaperqindje) >= 20 and ft.aqt=0 ) 
							then	isnull(max(FLETDOGANORETVSHsum.shumavlefta20) ' + @vleftaXImporte + '	THEN	0 
																											ELSE	isnull(max(FLETDOGANORETVSHsum.shumavlefta20),0) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) 
																										end) 
							else	0 
						end as vlefta20importe
						,case 
							when	(max(t.normaperqindje) >= 20 and ft.aqt=0) 
							then	isnull(max(FLETDOGANORETVSHsum.shumatvsh20),0) 
							else	0 
						end as tvsh20importe
						,case	
							when	(max(t.normaperqindje)  >= 10 and max(t.normaperqindje) < 20 and ft.aqt=0 ) 
							then	isnull(max(FLETDOGANORETVSHsum.shumavlefta10) ' + @vleftaXImporte + '	THEN	0 
																											ELSE	isnull(max(FLETDOGANORETVSHsum.shumavlefta10),0) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) 
																										end) 
							else	0 
						end as vlefta10importe
						,case 
							when	(max(t.normaperqindje) >= 10 and max(t.normaperqindje) < 20 and ft.aqt=0) 
							then	isnull(max(FLETDOGANORETVSHsum.shumatvsh10),0) 
							else	0 
						end as tvsh10importe
						,case	
							when	(max(t.normaperqindje) >= 6 and max(t.normaperqindje) < 10 and ft.aqt=0 ) 
							then	isnull(max(FLETDOGANORETVSHsum.shumavlefta6) ' + @vleftaXImporte + '	THEN	0 
																											ELSE	isnull(max(FLETDOGANORETVSHsum.shumavlefta6),0) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) 
																										end) 
							else	0 
						end as vlefta6importe
						,case 
							when	(max(t.normaperqindje) >= 6 and max(t.normaperqindje) < 10 and ft.aqt=0) 
							then	isnull(max(FLETDOGANORETVSHsum.shumatvsh6),0) 
							else	0 
						end as tvsh6importe
						,case 
							when	(max(t.normaperqindje)>0  and ft.aqt=1) 
							then	isnull(max(FLETDOGANORETVSHsum.shumavlefta20aqt) ' + @vleftaXImporte + '	THEN	0 
																												ELSE	isnull(max(FLETDOGANORETVSHsum.shumavlefta20aqt),0) / (ISNULL(MAX(FLETDOGANORETVSH.vfaturuar),0)) 
																											end) 
							else	0 
						end as vleftaAQTimporte
						,case 
							when	(max(t.normaperqindje) > 0 and ft.aqt = 1 ) 
							then	isnull(max(FLETDOGANORETVSHsum.shumatvshaqt),0)
							else	0 
						end as tvshAQTimporte
						,0 VLEFTA20
						,0 TVSH20
						,0 VLEFTA10
						,0 TVSH10
						,0 VLEFTA6
						,0 TVSH6
						,0 VLEFTAAQT20
						,0 TVSHAQT20
						,0 VLEFTAFERMER20
						,0 TVSHFERMER20
						,0 VLEFTAAUTONGARKESE
						,0 TVSHAUTONGARKESE
						,0 AS TOTALIRREGULLIME
						,0 AS TVSHRREGULLIME
						,0 AS TOTALIBORXHI
						,0 AS TVSHBORXHI 
						,MAX(T_DEGEADMINISTRATIVE.PERSHKRIMI) as degaadministrative 
						,MAX(NDERMARJEPERSHK) 
						,'''' AS PERSHKRIMI
						,CASE T_KOKAFLETEKONTABEL.IDSTATUSDOK WHEN 0 THEN '''' ELSE cast(NRREFERENCEKOKAFLETEKONTABEL as varchar(20)) END AS NRREF '

	set @SQTQUERY6 = '
				FROM	t_fletedoganorekoka as fk  
						join #ndermarjet 
							on fk.IDNDERM=#ndermarjet.id
						JOIN t_fletedoganoretrupi as tr 
							on fk.idfletedoganore=tr.idfletedoganore  
						JOIN t_fletedoganoreTVSH as ft 
							on fk.idfletedoganore= ft.idfletedoganore 
						JOIN t_taksat as t 
							on t.idtaksa=ft.idtaksa 
						JOIN t_kokashitje  
							on t_kokashitje.idshitjekoka=tr.idfatura
						JOIN T_TRUPISHITJE 
							ON T_KOKASHITJE.idshitjekoka=T_TRUPISHITJE.IDSHITJEKOKA
						JOIN T_KLIENTFURNITOR 
							ON t_kokashitje.IDKLIENTFURNITOR = T_KLIENTFURNITOR.IDKLIENTFURNITOR
						JOIN T_KONFIGAMBJENTE 
							ON fk.IDKONFIGAMBJENTE = T_KONFIGAMBJENTE.IDKONFIGAMBJENTE
						LEFT JOIN	(
										select	IDFLETEDOGANORE
												,sum(VLEFTAFATURUAR) as vl
												,case when (aqt=0 and max(NORMAPERQINDJE) = 0) then (sum(VLEFTAFATURUAR)) else 0 end as shumavlefta0
												,case when (aqt=1 and max(NORMAPERQINDJE) = 0) then (sum(VLEFTAFATURUAR)) else 0 end as shumavlefta0aqt
												,case when (aqt=1 and max(NORMAPERQINDJE) >= 20) then (sum(VLEFTAFATURUAR)) else 0 end as shumavlefta20aqt
												,case when (aqt=1 and max(NORMAPERQINDJE) >= 20) then (sum(VLEFTATVSH)) else 0 end as shumatvshaqt
												,case when (aqt=0 and max(NORMAPERQINDJE) >= 20) then (sum(VLEFTAFATURUAR)) else 0 end as shumavlefta20
												,case when (aqt=0 and max(NORMAPERQINDJE) >= 20) then (sum(VLEFTATVSH)) else 0 end as shumatvsh20
												,case when (aqt=0 and max(NORMAPERQINDJE) >= 10 and max(NORMAPERQINDJE) < 20) then (sum(VLEFTAFATURUAR)) else 0 end as shumavlefta10
												,case when (aqt=0 and max(NORMAPERQINDJE) >= 10 and max(NORMAPERQINDJE) < 20) then (sum(VLEFTATVSH)) else 0 end as shumatvsh10
												,case when (aqt=0 and max(NORMAPERQINDJE) > 0 and max(NORMAPERQINDJE) < 10) then (sum(VLEFTAFATURUAR)) else 0 end as shumavlefta6
												,case when (aqt=0 and max(NORMAPERQINDJE) > 0 and max(NORMAPERQINDJE) < 10) then (sum(VLEFTATVSH)) else 0 end as shumatvsh6
										from	T_FLETEDOGANORETVSH ftv
												inner join T_TAKSAT t 
													on t.IDTAKSA=ftv.IDTAKSA 
												join #ndermarjet 
													on #ndermarjet.id=t.IDNDERM					
										group by IDFLETEDOGANORE, t.IDTAKSA, AQT
									)	FLETDOGANORETVSHsum  
							on FLETDOGANORETVSHsum.idfletedoganore=fk.idfletedoganore
						LEFT JOIN	(
										SELECT	idfletedoganore, SUM(vlefta) AS vlefta 
										FROM	dbo.T_FLETEDOGANORETAKSA 
										WHERE  T_FLETEDOGANORETAKSA.TVSH=1
										GROUP BY T_FLETEDOGANORETAKSA.idfletedoganore 
									)	FLETDOGANORETAKSA 
							ON FLETDOGANORETAKSA.idfletedoganore = fk.idfletedoganore
						LEFT JOIN	(
										SELECT	idfletedoganore, vfaturuar = SUM(vleftafaturuar)
										FROM	dbo.t_fletedoganoretvsh
										GROUP BY idfletedoganore
									)	FLETDOGANORETVSH 
							ON FLETDOGANORETVSH.idfletedoganore = fk.idfletedoganore
						LEFT JOIN	(
										SELECT	idfletedoganore, SUM(VLEFTA) AS VLEFTA2 
										FROM	dbo.T_FLETEDOGANORETAKSA 
											INNER JOIN dbo.T_TAKSAT ON T_TAKSAT.IDTAKSA = T_FLETEDOGANORETAKSA.IDTAKSA   
										WHERE  TVSH = 0 AND EPERJASHTUAR = 0
										GROUP BY T_FLETEDOGANORETAKSA.idfletedoganore  
									)	FLETDOGANORETAKSA2 
							ON FLETDOGANORETAKSA2.idfletedoganore = fk.idfletedoganore
						LEFT JOIN t_grupimdokumentikoka as gd1 
							on gd1.idgrupimkoka=t_kokashitje.idgrup1
						LEFT JOIN t_grupimdokumentikoka as gd2 
							on gd2.idgrupimkoka=t_kokashitje.idgrup2
						LEFT JOIN t_grupimdokumentikoka as gd3 
							on gd3.idgrupimkoka=t_kokashitje.idgrup3
						LEFT JOIN T_QYTETI 
							ON T_QYTETI.IDQYTETI = CONVERT(INT,T_KLIENTFURNITOR.QYTETIKF)
						LEFT JOIN	T_DEGEADMINISTRATIVE 
							ON T_DEGEADMINISTRATIVE.IDDEGEADMINISTRATIVE = T_KOKASHITJE.IDDEGEADMINISTRATIVE
						LEFT JOIN	T_NJESIADMINISTRATIVE  
							on T_trupishitje.idmagazina=T_NJESIADMINISTRATIVE.IDNJESIADM
						LEFT JOIN T_PIKESHITJEFURNIZIMI 
							ON T_PIKESHITJEFURNIZIMI.IDPIKESHITJEFURNIZIMI = T_KOKASHITJE.IDPIKESHITJEFURNIZIMI
						LEFT JOIN T_NDERMARJE 
							ON T_NDERMARJE.IDNDERMARJE =  fk.IDNDERM
						LEFT JOIN T_KOKAFLETEKONTABEL 
							ON	fk.IDFLETEDOGANORE = T_KOKAFLETEKONTABEL.IDGJENERUES 
								AND fk.IDKONFIGAMBJENTE = T_KOKAFLETEKONTABEL.IDKONFIGGJENERUES
						LEFT JOIN	(
										SELECT	idfletedoganore, SUM(VLEFTA) AS VLEFTA
										FROM	dbo.T_FLETEDOGANORETAKSA 
												INNER JOIN dbo.T_TAKSAT 
													ON T_TAKSAT.IDTAKSA = T_FLETEDOGANORETAKSA.IDTAKSA   
										WHERE  ((TVSH = 0 AND EPERJASHTUAR = 0) OR TVSH = 1)
										GROUP BY T_FLETEDOGANORETAKSA.idfletedoganore  
									)	FLETDOGANORETAKSATOT
							ON	FLETDOGANORETAKSATOT.idfletedoganore = fk.idfletedoganore
						LEFT JOIN	(
										SELECT	IDFLETEDOGANORE
												,SUM(VLEFTAFATURUAR) VLEFTAFATURUAR
										from	T_FLETEDOGANORETVSH ftv
												inner join T_TAKSAT t 
													on t.IDTAKSA=ftv.IDTAKSA
												join #ndermarjet 
													on #ndermarjet.id=t.IDNDERM		
										where	NORMAPERQINDJE = 0
												AND AQT = 0
										group by IDFLETEDOGANORE
									)	SHUMAVLEFTA0
							ON SHUMAVLEFTA0.IDFLETEDOGANORE = fk.idfletedoganore
						LEFT JOIN	(
										SELECT	IDFLETEDOGANORE
												,SUM(VLEFTAFATURUAR) VLEFTAFATURUAR
										from	T_FLETEDOGANORETVSH ftv
												inner join T_TAKSAT t 
													on t.IDTAKSA=ftv.IDTAKSA
												join #ndermarjet 
													on #ndermarjet.id=t.IDNDERM		
										where	NORMAPERQINDJE > 0
												AND AQT = 0
										group by IDFLETEDOGANORE
									)	SHUMAVLEFTA20
							ON SHUMAVLEFTA20.IDFLETEDOGANORE = fk.idfletedoganore
						LEFT JOIN	(
										SELECT	IDFLETEDOGANORE
												,SUM(VLEFTAFATURUAR) VLEFTAFATURUAR
										from	T_FLETEDOGANORETVSH ftv
												inner join T_TAKSAT t 
													on t.IDTAKSA=ftv.IDTAKSA 
												join #ndermarjet 
													on #ndermarjet.id=t.IDNDERM		
										where	NORMAPERQINDJE = 0
												AND AQT = 1
										group by IDFLETEDOGANORE
									)	SHUMAVLEFTA0AQT
							ON SHUMAVLEFTA0AQT.IDFLETEDOGANORE = fk.idfletedoganore
						LEFT JOIN	(
										SELECT	IDFLETEDOGANORE
												,SUM(VLEFTAFATURUAR) VLEFTAFATURUAR
										from	T_FLETEDOGANORETVSH ftv
												inner join T_TAKSAT t 
													on t.IDTAKSA=ftv.IDTAKSA
												join #ndermarjet 
													on #ndermarjet.id=t.IDNDERM		
										where	NORMAPERQINDJE > 0
												AND AQT = 1
										group by IDFLETEDOGANORE
									)	SHUMAVLEFTA20AQT
							ON SHUMAVLEFTA20AQT.IDFLETEDOGANORE = fk.idfletedoganore
				WHERE	fk.idstatusdok = 1 
						AND T_KOKASHITJE.IDSTATUSDOK = 1 
						AND fk.IDSTATUSDOK = 1 
						AND fk.IMPORTEXPORT = 1
						AND ' + @STRFILTER2 +'
				GROUP by fk.idfletedoganore, ft.idtaksa, ft.aqt, NRREFERENCEKOKAFLETEKONTABEL,T_KOKAFLETEKONTABEL.IDSTATUSDOK

			)	AS BLERJELIBRI 
	group by nrdok, nrserial, dtdok, EMERTIMIKF, EMERTIMFATURE, QYTETIEMRI, NIPTKF, NRREF
	ORDER BY DTDOK, NRDOK'

print(@strquery3)
print(@SQTQUERY)
print(@SQTQUERY2)
print(@SQTQUERY4)
print(@SQTQUERY5)
print(@SQTQUERY6)
exec (@strquery3+@SQTQUERY+@SQTQUERY2+@SQTQUERY4+@SQTQUERY5+@SQTQUERY6)