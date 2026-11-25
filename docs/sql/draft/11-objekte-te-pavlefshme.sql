/*
  DRAFT 11 (2026-11-25): objektet e pavlefshme (kontrolli objekte-te-pavlefshme.txt), Alkid: "vazhdo me te dyja".
    1. Kalojne ne skemen [hequr] (ALTER SCHEMA TRANSFER):
       - objektet e grupit (ii): nuk i therret programi, asnje raport (T_RAPORTI) apo T_SP,
       - objektet "DEAD" te grupit (i): tabelat e tyre nuk ekzistojne me (T_AMBJENT, T_GRUPAUDITIMI*, T_LLOGARIKONTABILITETI)
         dhe metoda C# qe i therret nuk arrihet nga asnje faqe; funksionet Get_gjendja*LlogFill* (shfaqen vetem ne nje koment).
       SP_FshiVeprime_SipasLicences NUK levizet (mbetet, vetem raportohet).
       Nje objekt mbetet nese e perdor ende dicka ne dbo qe nuk levizet (si ne 10); emri ne tekst kerkohet si fjale e plote.
    2. 18 procedura qe i therret programi rregullohen (kolonat _sq/_en/_fr te perkthimeve, kolona e paqarte,
       numri i kolonave te @a, IDNDERVITI -> IDNDERMARJE e kokes): e vjetra kalon ne [hequr] me te njejtin emer
       (teksti i saj nuk kompilohet me, prandaj nuk mjafton ta ruash si tekst), e rregulluara krijohet ne dbo.
       Teksti i vjeter ruhet edhe ne hequr.T_DEFINICIONE_11.
       Nese teksti aktual i nje procedure nuk eshte ai qe u analizua (hash), skripti ndalon pa ndryshuar asgje.

  Asgje nuk fshihet perfundimisht.
    - kthimi mbrapsht:        11-rollback-objekte-te-pavlefshme.sql
    - fshirja perfundimtare:  11-pastrimi-perfundimtar.sql (pasi te jete provuar programi)
  Nese dicka nuk shkon, i gjithe skripti kthehet mbrapsht (XACT_ABORT).

  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 11-objekte-te-pavlefshme.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF SCHEMA_ID('hequr') IS NULL EXEC('CREATE SCHEMA hequr');
IF OBJECT_ID('hequr.T_RENDI_11') IS NOT NULL
BEGIN
    RAISERROR('Skripti 11 eshte ekzekutuar tashme ne kete databaze (hequr.T_RENDI_11 ekziston).', 16, 1);
    RETURN;
END

-- teksti i procedurave/view-ve merret nje here, jashte transaksionit (shih 07); vetem objektet e skemes dbo
SELECT m.object_id, CASE WHEN o.type = 'TR' THEN o.parent_object_id END AS parent_id, m.definition
INTO #mod FROM sys.sql_modules m JOIN sys.objects o ON o.object_id = m.object_id
WHERE o.schema_id = SCHEMA_ID('dbo');

/* procedurat qe rregullohen: emri, hash SHA2_256 i tekstit aktual (ai qe u analizua), teksti i ri (ALTER) */
CREATE TABLE #fix (emri sysname PRIMARY KEY, hash varbinary(32) NOT NULL, teksti nvarchar(max) NOT NULL);
INSERT #fix (emri, hash, teksti) VALUES
(N'prc_T_ATRIBUTETRUPI_merrVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL', 0xB6175229D86EFE44626442A18346F858F89F66F333AF55D44293C9491A75CF6E,
N'-- ============================================================================================
-- Author:				EGI ALUSHI
-- Create date:			09/02/2018
-- Description:			Kthen te gjithe kontrollet me vlerat default per nje konfigurim
-- ============================================================================================

CREATE procedure [dbo].[prc_T_ATRIBUTETRUPI_merrVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL]
(
@IDKONFIGAMBJENTE T_FOR_KEY
)
AS
BEGIN

	DECLARE @Err Int

	SELECT	KODKONTROLL AS KodKontrolli, VLEREDEFAULT_sq AS VleraDefault
	FROM	T_ATRIBUTETRUPI 
			INNER JOIN T_KONTROLLE on T_ATRIBUTETRUPI.IDKONTROLL=T_KONTROLLE.IDKONTROLL 
	WHERE	IDKONFIGAMBJENTE = @IDKONFIGAMBJENTE

	SET @Err = @@Error
	RETURN @Err
	
END'),
(N'prc_T_ATRIBUTETRUPI_updVleredefault', 0xB568CC1D719794F8BDEC04932494C2BCA043F7DCE5D63F67859DE955A21CFB4E,
N'create procedure [dbo].[prc_T_ATRIBUTETRUPI_updVleredefault]
	
(
	@IDKONTROLL T_FOR_KEY,
	@IDKONFIGAMBJENTE T_FOR_KEY,
	@VLEREDEFAULT VARCHAR(50)
	
)
AS
BEGIN
DECLARE @Err Int	

	update
	 T_ATRIBUTETRUPI set
	
		
		
		VLEREDEFAULT_sq=@VLEREDEFAULT
		
		where IDKONTROLL =@IDKONTROLL and IDKONFIGAMBJENTE =@IDKONFIGAMBJENTE
		
		
	
	Set @Err = @@Error
	RETURN @Err
END
'),
(N'prc_T_BURIME_merrGjitheBurimeAktive', 0xD32CC8356FE2014A6E37EFFCA871DECF017BCE96B5FC9862DCD4ABB1F6BD3D1D,
N'CREATE procedure [dbo].[prc_T_BURIME_merrGjitheBurimeAktive]
(
	@IDNDERMARJE T_FOR_KEY
)
AS
BEGIN

DECLARE @Err Int

	select t_burime.*, NRLLOGARI 
	from dbo.T_BURIME  LEFT outer JOIN dbo.T_LLOGARI ON dbo.T_BURIME.IDLLOGARI = dbo.T_LLOGARI.IDLLOGARI
	
                   where dbo.T_BURIME.IDNDERMARJE = @IDNDERMARJE  and dbo.T_BURIME.AKTIV =''true'' AND dbo.T_BURIME.IDSTATUSDOK<>2
	Set @Err = @@Error

	RETURN @Err
	
END
'),
(N'prc_T_ETAPAAPROVIMI_merrEtapaDTSipasKokaShitjeDheNrEtape', 0x8BAC9A12602A808550D1AFDE59992AD1269B8969C3A476C1B4F9564D7131F995,
N'CREATE procedure prc_T_ETAPAAPROVIMI_merrEtapaDTSipasKokaShitjeDheNrEtape 
(
@IDKOKA T_FOR_KEY,
@NRETAPE T_FOR_KEY
)
AS
BEGIN
SET NOCOUNT ON 	
DECLARE @Err Int

SELECT IDETAPA AS IdEtapa,idskema AS IdSkema,sk.KODI AS Skema, et.idkoka AS IdKokaShitje, kat.IDKATDOK as IdKategoria,kat.PERSHKRIMI_sq AS Kategoria,niv.KODI AS Nenkategoria,konf.KODKONFIGAMBJENTE AS Lloji,ks.NRDOK AS NrDokumenti,mon.MONEDHAKOD AS Monedha, ks.TOTALIMETVSHMEZBRITJE AS Vlefta,ks.TOTALIMETVSHMEZBRITJE*ks.KURSI AS VleftaMonBaze, nrprocesi AS NrProcesi,nretape AS Etapa,et.idkrijuesi AS IdKrijuesi, krijues.PERDORUESUSERNAME Krijuesi,dtdergimi AS DtDergimi,
idaprovuesi AS IdAprovuesi ,COALESCE(aprovues.PERDORUESUSERNAME,rol.KODIROLI) AS Aprovuesi,  et.idmodifikuesi AS IdModifikuesi,
modifikuesi.PERDORUESUSERNAME AS Modifikuesi,et.dtmodifikimi AS DtModifikimi, dtkujtese AS DtKujtese,et.idndermarje AS IdNdermarje,CASE when(SELECT COUNT(*) FROM t_komenteaprovimi WHERE idetape=et.idetapa)>0 THEN ''Po'' ELSE ''Jo'' END  AS Komente, CASE WHEN et.statusaprovimi=0 THEN '''' WHEN et.statusaprovimi=1 THEN ''Per Aprovim'' WHEN et.statusaprovimi=2 THEN ''Aprovuar'' WHEN et.statusaprovimi=''3'' THEN ''Refuzuar'' ELSE ''Deleguar'' END AS Statusi,
 CASE WHEN ks.statusaprovimi=2 THEN CASE WHEN et.niveli=(SELECT MAX(niveli) FROM dbo.T_ETAPAAPROVIMI WHERE IDKOKA=ks.idshitjekoka) THEN 100 ELSE et.NIVELI*100/(SELECT MAX (NIVELI) FROM dbo.T_TRUPISKEMAWORKFLOW WHERE IDKOKA=SK.IDKOKA ) END  WHEN et.statusaprovimi=3 THEN 0 ELSE 
et.NIVELI*100/(SELECT MAX (NIVELI) FROM dbo.T_TRUPISKEMAWORKFLOW WHERE IDKOKA=SK.IDKOKA ) END  AS Progresi, niveli AS Niveli,et.STATUSAPROVIMI StatusAprovimi, LLOJAPROVUESI AS LlojAprovuesi, kf.EMERTIMIKF as Klienti
 FROM T_ETAPAAPROVIMI ET 
 INNER JOIN dbo.T_KOKASKEMAWORKFLOW sk ON et.idskema=sk.IDKOKA
 INNER JOIN (	SELECT IDSHITJEKOKA, NRDOK, STATUSAPROVIMI, IDMONEDHA, IDKLIENTFURNITOR,  IDKONFIGAMBJENTE, TOTALIMETVSHMEZBRITJE, KURSI
				FROM T_KOKASHITJE
				UNION ALL 
				SELECT IDSHITJEKOKA, NRDOK, STATUSAPROVIMI, IDMONEDHA, IDKLIENTFURNITOR,  IDKONFIGAMBJENTE, TOTALIMETVSHMEZBRITJE, KURSI 
				FROM T_KOKASHITJEHISTORIK WHERE LIDHUR = 0 
			) ks ON et.idkoka=ks.IDSHITJEKOKA 
INNER JOIN  T_KONFIGAMBJENTE konf ON ks.IDKONFIGAMBJENTE = konf.IDKONFIGAMBJENTE 
INNER JOIN	T_KATEGORINIVELDOK kat ON konf.IDKATDOK = kat.IDKATDOK AND kat.IDKATDOK = ET.IDKATEGORIA
INNER JOIN	T_NIVELREGJISTRIMI niv ON konf.IDNIVEL=niv.IDNIVEL 
INNER JOIN	T_MONEDHA mon ON mon.IDMONEDHA=ks.IDMONEDHA  
INNER JOIN	T_PERDORUESI AS krijues ON krijues.IDPERDORUES=et.idkrijuesi
inner JOIN T_PERDORUESI AS modifikuesi ON modifikuesi.IDPERDORUES=et.idmodifikuesi
LEFT JOIN T_ROLI AS rol ON et.IDAPROVUESI=rol.IDROLI AND llojaprovuesi=2
LEFT JOIN T_PERDORUESI AS aprovues ON aprovues.IDPERDORUES=et.idaprovuesi AND llojaprovuesi=1
LEFT JOIN T_KLIENTFURNITOR kf on kf.IDKLIENTFURNITOR=ks.IDKLIENTFURNITOR

WHERE  IDSHITJEKOKA=@IDKOKA AND NRETAPE=@NRETAPE
	Set @Err = @@Error

	RETURN @Err
END'),
(N'prc_T_ETAPAAPROVIMI_merrEtapaDTSipasNrProcesi', 0x9D0CE47D26326C9A3060382C2066FE4406002054046BD26849C61D4A57C3DE4B,
N'CREATE procedure prc_T_ETAPAAPROVIMI_merrEtapaDTSipasNrProcesi
(

@NRPROCESI T_FOR_KEY,
@IDNDERMARJE T_FOR_KEY
)
AS
BEGIN
	SET NOCOUNT ON 
DECLARE @Err Int


SELECT IDETAPA AS IdEtapa,idskema AS IdSkema,sk.KODI AS Skema, et.idkoka AS IdKokaShitje,kat.PERSHKRIMI_sq AS Kategoria,niv.KODI AS Nenkategoria,konf.KODKONFIGAMBJENTE AS Lloji,ks.NRDOK AS NrDokumenti,mon.MONEDHAKOD AS Monedha, ks.TOTALIMETVSHMEZBRITJE AS Vlefta,ks.TOTALIMETVSHMEZBRITJE*ks.KURSI AS VleftaMonBaze, nrprocesi AS NrProcesi,nretape AS Etapa,et.idkrijuesi AS IdKrijuesi, krijues.PERDORUESUSERNAME Krijuesi,dtdergimi AS DtDergimi,
idaprovuesi AS IdAprovuesi ,COALESCE(aprovues.PERDORUESUSERNAME,rol.KODIROLI) AS Aprovuesi, et.idmodifikuesi AS IdModifikuesi,
modifikuesi.PERDORUESUSERNAME AS Modifikuesi,et.dtmodifikimi AS DtModifikimi, dtkujtese AS DtKujtese,et.idndermarje AS IdNdermarje,CASE when(SELECT COUNT(*) FROM t_komenteaprovimi WHERE idetape=et.idetapa)>0 THEN ''Po'' ELSE ''Jo'' END  AS Komente, CASE WHEN et.statusaprovimi=0 THEN '''' WHEN et.statusaprovimi=1 THEN ''Per Aprovim'' WHEN et.statusaprovimi=2 THEN ''Aprovuar'' WHEN et.statusaprovimi=''3'' THEN ''Refuzuar'' ELSE ''Deleguar'' END AS Statusi,
CASE WHEN ks.statusaprovimi=2 THEN CASE WHEN et.niveli=(SELECT MAX(niveli) FROM T_ETAPAAPROVIMI WHERE IDKOKA=ks.idshitjekoka) THEN 100 ELSE et.NIVELI*100/(SELECT MAX (NIVELI) FROM T_TRUPISKEMAWORKFLOW WHERE IDKOKA=SK.IDKOKA ) END  WHEN et.statusaprovimi=3 THEN 0 ELSE 
et.NIVELI*100/(SELECT MAX (NIVELI) FROM T_TRUPISKEMAWORKFLOW WHERE IDKOKA=SK.IDKOKA ) END AS Progresi, niveli AS Niveli,et.STATUSAPROVIMI StatusAprovimi, LLOJAPROVUESI AS LlojAprovuesi, kf.EMERTIMIKF as Klienti
 FROM T_ETAPAAPROVIMI ET INNER JOIN T_KOKASKEMAWORKFLOW sk ON et.idskema=sk.IDKOKA
 INNER JOIN (	SELECT IDSHITJEKOKA, NRDOK, STATUSAPROVIMI, IDMONEDHA, IDKLIENTFURNITOR,  IDKONFIGAMBJENTE, TOTALIMETVSHMEZBRITJE, KURSI
				FROM T_KOKASHITJE
				UNION ALL 
				SELECT IDSHITJEKOKA, NRDOK, STATUSAPROVIMI, IDMONEDHA, IDKLIENTFURNITOR,  IDKONFIGAMBJENTE, TOTALIMETVSHMEZBRITJE, KURSI 
				FROM T_KOKASHITJEHISTORIK WHERE LIDHUR = 0 
			) ks ON et.idkoka=ks.IDSHITJEKOKA 
			INNER JOIN  T_KONFIGAMBJENTE konf ON ks.IDKONFIGAMBJENTE = konf.IDKONFIGAMBJENTE 
			INNER JOIN T_KATEGORINIVELDOK kat ON konf.IDKATDOK = kat.IDKATDOK AND kat.IDKATDOK = ET.IDKATEGORIA
INNER JOIN T_NIVELREGJISTRIMI niv ON konf.IDNIVEL=niv.IDNIVEL 
INNER JOIN T_MONEDHA mon ON mon.IDMONEDHA=ks.IDMONEDHA  
INNER JOIN T_PERDORUESI AS krijues ON krijues.IDPERDORUES=et.idkrijuesi
inner JOIN T_PERDORUESI AS modifikuesi ON modifikuesi.IDPERDORUES=et.idmodifikuesi
LEFT OUTER JOIN T_ROLI AS rol ON et.IDAPROVUESI=rol.IDROLI AND llojaprovuesi=2
LEFT OUTER  JOIN T_PERDORUESI AS aprovues ON aprovues.IDPERDORUES=et.idaprovuesi AND llojaprovuesi=1
LEFT OUTER  JOIN T_KLIENTFURNITOR kf on kf.IDKLIENTFURNITOR=ks.IDKLIENTFURNITOR
WHERE NRPROCESI=@NRPROCESI AND et.IDNDERMARJE=@IDNDERMARJE
	Set @Err = @@Error

	RETURN @Err
END'),
(N'prc_T_ETAPAAPROVIMI_merrEtapaFunditSipasBuxhetitDhePerdoruesit', 0x8430E918F73E05A25379DDC78B1A55C1C156989575CF051127843A16A25F3665,
N'CREATE PROCEDURE [dbo].[prc_T_ETAPAAPROVIMI_merrEtapaFunditSipasBuxhetitDhePerdoruesit]
(
@IDKOKA T_FOR_KEY,
@IDPERDORUES T_FOR_KEY
)
AS
BEGIN
	
DECLARE @Err Int

SET NOCOUNT ON 

SELECT TOP(1) IDETAPA AS IdEtapa, idskema AS IdSkema, sk.KODI AS Skema, et.idkoka AS IdKokaShitje, kat.IDKATDOK as IdKategoria,kat.PERSHKRIMI_sq AS Kategoria, niv.KODI AS Nenkategoria, 
	konf.KODKONFIGAMBJENTE AS Lloji, kb.NRDOK AS NrDokumenti, ''LEK'' AS Monedha, kb.TOTALI AS Vlefta, kb.TOTALI AS VleftaMonBaze, nrprocesi AS NrProcesi, 
	nretape AS Etapa, et.idkrijuesi AS IdKrijuesi, krijues.PERDORUESUSERNAME Krijuesi, dtdergimi AS DtDergimi, idaprovuesi AS IdAprovuesi, COALESCE(aprovues.PERDORUESUSERNAME,rol.KODIROLI) AS Aprovuesi,  
	et.idmodifikuesi AS IdModifikuesi, modifikuesi.PERDORUESUSERNAME AS Modifikuesi, et.dtmodifikimi AS DtModifikimi, dtkujtese AS DtKujtese, et.idndermarje AS IdNdermarje, 
	CASE when(SELECT COUNT(*) FROM t_komenteaprovimi WHERE idetape=et.idetapa)>0 THEN ''Po'' ELSE ''Jo'' END  AS Komente, 
	CASE WHEN et.statusaprovimi=0 THEN '''' WHEN et.statusaprovimi=1 THEN ''Per Aprovim'' WHEN et.statusaprovimi=2 THEN ''Aprovuar'' WHEN et.statusaprovimi=''3'' THEN ''Refuzuar'' ELSE ''Deleguar'' END AS Statusi,
	CASE WHEN kb.statusaprovimi=2 THEN 
			CASE WHEN et.niveli=(SELECT MAX(niveli) FROM dbo.T_ETAPAAPROVIMI WHERE IDKOKA=kb.IDBUXHETIKOKA) THEN 100 ELSE et.NIVELI*100/(SELECT MAX (NIVELI) FROM dbo.T_TRUPISKEMAWORKFLOW WHERE IDKOKA=SK.IDKOKA ) END			 WHEN et.statusaprovimi=3 THEN 0 
		 ELSE et.NIVELI*100/(SELECT MAX (NIVELI) FROM dbo.T_TRUPISKEMAWORKFLOW WHERE IDKOKA=SK.IDKOKA ) END  AS Progresi, 
	niveli AS Niveli, et.STATUSAPROVIMI StatusAprovimi, LLOJAPROVUESI AS LlojAprovuesi, '''' as Klienti

FROM T_ETAPAAPROVIMI ET 
	INNER JOIN dbo.T_KOKASKEMAWORKFLOW sk ON et.idskema=sk.IDKOKA
	INNER JOIN (SELECT NRDOK, TOTALI, STATUSAPROVIMI, IDBUXHETIKOKA, IDKONFIGAMBJENTE FROM  dbo.T_B_KOKABUXHETI 
							UNION SELECT NRDOK, TOTALI, STATUSAPROVIMI, IDBUXHETIKOKA, IDKONFIGAMBJENTE FROM dbo.T_B_KOKABUXHETIHISTORIK) kb ON et.idkoka=kb.IDBUXHETIKOKA  
	INNER JOIN  dbo.T_KONFIGAMBJENTE konf ON kb.IDKONFIGAMBJENTE = konf.IDKONFIGAMBJENTE 
	INNER JOIN dbo.T_KATEGORINIVELDOK kat ON konf.IDKATDOK = kat.IDKATDOK AND kat.IDKATDOK = ET.IDKATEGORIA
	INNER JOIN dbo.T_NIVELREGJISTRIMI niv ON konf.IDNIVEL=niv.IDNIVEL 
	INNER JOIN dbo.T_PERDORUESI AS krijues ON krijues.IDPERDORUES=et.idkrijuesi
	inner JOIN dbo.T_PERDORUESI AS modifikuesi ON modifikuesi.IDPERDORUES=et.idmodifikuesi
	LEFT OUTER JOIN dbo.T_ROLI AS rol ON et.IDAPROVUESI=rol.IDROLI AND llojaprovuesi=2
	LEFT OUTER  JOIN dbo.T_PERDORUESI AS aprovues ON aprovues.IDPERDORUES=et.idaprovuesi AND llojaprovuesi=1

WHERE  IDBUXHETIKOKA=@idkoka 
	  AND ((et.STATUSAPROVIMI=2 OR et.STATUSAPROVIMI=3) 
			OR (et.STATUSAPROVIMI=1 AND ((idaprovuesi=@IDPERDORUES AND llojaprovuesi=1) 
			OR (llojaprovuesi=2 AND IDAPROVUESI IN (SELECT idroli FROM dbo.T_ROLPERDORUES WHERE IDPERDORUES=@IDPERDORUES)))) 
			OR (et.STATUSAPROVIMI=1 AND @idperdorues NOT IN (SELECT IDAPROVUESI FROM dbo.T_ETAPAAPROVIMI WHERE IDKOKA=@idkoka AND STATUSAPROVIMI=1 AND llojaprovuesi=1 ) 
				AND @idperdorues NOT IN (SELECT idperdorues FROM dbo.T_ETAPAAPROVIMI INNER JOIN dbo.T_ROLPERDORUES ON IDAPROVUESI=IDROLI AND llojaprovuesi=2 WHERE IDKOKA=@idkoka AND STATUSAPROVIMI=1 )
			   )
		  )  

ORDER BY NRETAPE desc
	
	Set @Err = @@Error

	RETURN @Err
END'),
(N'prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentesSipasGjuhesEmerKomp', 0x941E3CF4D8C2A64608EF98F322F011DA2F278D90470464FD15E14470CB62AD6D,
N'CREATE PROCEDURE prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentesSipasGjuhesEmerKomp
(
@EMERKOMPONENTE T_PERSHKRIM,
@IDKONFIG T_AUTO_NUM,
@IDGJUHA T_FOR_KEY
)
AS
set nocount on
BEGIN
DECLARE @Err Int
	select  t.GRIDATRUPIID, t.GRIDAKOKAID, t.GRIDATRUPIINDEX, 
			t.GRIDATRUPIVISIBLE, t.GRIDATRUPIKODI, CASE WHEN @IDGJUHA=0 THEN t.GRIDATRUPIPERSHKRIMI_sq WHEN @IDGJUHA=1 THEN t.GRIDATRUPIPERSHKRIMI_en ELSE t.GRIDATRUPIPERSHKRIMI_fr END AS GRIDATRUPIPERSHKRIMI,
			t.GRIDATRUPIREADONLY, t.GRIDATRUPIWIDTH,isnull(t.GRIDATRUPIINDEXORIGJINAL,0)as GRIDATRUPIINDEXORIGJINAL, t.IDKONFIGLUPA, VISIBLECOSTUMIZE, t.GRIDATRUPIPERSHKRIMI_en, t.GRIDATRUPIPERSHKRIMI_fr,
			''KODKONFIGLUPA'' = CASE 
			WHEN t.IDKONFIGLUPA = 0 THEN ''0'' 
			WHEN t.IDKONFIGLUPA = 1 THEN 
			ISNULL(REVERSE(STUFF(REVERSE((
				SELECT konfig.KODKONFIGAMBJENTE + ''-''
				FROM T_LUPAMULTIPLE lupa LEFT OUTER JOIN T_KONFIGAMBJENTE AS konfig ON lupa.IDKONFIGAMBJENTELUPA = konfig.IDKONFIGAMBJENTE  
				WHERE lupa.GRIDATRUPIID = t.GRIDATRUPIID 
				FOR XML PATH ('''')
				)), 1, 1, '''')),0) 
			ELSE KODKONFIGAMBJENTE 
			END,
			''IDKONFIGLUPA1'' = CASE 
			WHEN t.IDKONFIGLUPA = 0 THEN ''0'' 
			WHEN t.IDKONFIGLUPA = 1 THEN 
			ISNULL(REVERSE(STUFF(REVERSE((
				SELECT CAST(IDKONFIGAMBJENTELUPA AS VARCHAR) + ''-''
				FROM T_LUPAMULTIPLE lupa   
				WHERE lupa.GRIDATRUPIID = t.GRIDATRUPIID 
				FOR XML PATH ('''')
				)), 1, 1, '''')),0) 
			ELSE CAST (t.IDKONFIGLUPA AS VARCHAR) 
			END,
			TIPI, SHFAQMOBILE, RENDITJAMOBILE, gk.GRIDKOKAEMRI, LLOJFORMATFUSHE
	from  T_GRIDATRUPI t 
		inner join T_GRIDAKOKA gk on t.GRIDAKOKAID = gk.GRIDKOKAID
		left join T_KONFIGAMBJENTE k on k.IDKONFIGAMBJENTE = t.IDKONFIGLUPA
	where gk.GRIDKOMPID = (SELECT IDKOMPON FROM T_KOMPONENTE WHERE KOMPONEMRI = @EMERKOMPONENTE) and gk.IDKONFIGURIM = @IDKONFIG

    ORDER BY GRIDATRUPIINDEX

	Set @Err = @@Error

	RETURN @Err
	
END
set nocount off'),
(N'prc_T_LUPAMULTIPLE_selSipasIdGridaTrupi', 0xEB65779CDEB43AA5865A839D573A5E0AF1745918285F578D4BDBDA904A81902B,
N'CREATE PROCEDURE prc_T_LUPAMULTIPLE_selSipasIdGridaTrupi
(
	@GRIDATRUPIID T_FOR_KEY,
	@IDKONFIGAMBJENTE T_FOR_KEY,
	@IDGJUHA T_FOR_KEY
)
AS
BEGIN

DECLARE @Err Int

	select g.GRIDATRUPIID, g.GRIDAKOKAID, g.GRIDATRUPIINDEX, g.GRIDATRUPIVISIBLE, g.GRIDATRUPIKODI,
	CASE WHEN @IDGJUHA=0 THEN g.GRIDATRUPIPERSHKRIMI_sq WHEN @IDGJUHA=1 THEN g.GRIDATRUPIPERSHKRIMI_en ELSE g.GRIDATRUPIPERSHKRIMI_fr END AS GRIDATRUPIPERSHKRIMI, g.GRIDATRUPIREADONLY, g.GRIDATRUPIWIDTH, g.GRIDATRUPIINDEXORIGJINAL, 
	a.IDKONFIGAMBJENTE, k.KODKONFIGAMBJENTE,
	''IDKONFIGLUPA1'' = CASE 
			WHEN g.IDKONFIGLUPA = 0 THEN ''0'' 
			WHEN g.IDKONFIGLUPA = 1 THEN 
			ISNULL(REVERSE(STUFF(REVERSE((
				SELECT CAST(IDKONFIGAMBJENTELUPA AS VARCHAR) + ''-''
				FROM T_LUPAMULTIPLE lupa   
				WHERE lupa.GRIDATRUPIID = g.GRIDATRUPIID 
				FOR XML PATH ('''')
				)), 1, 1, '''')),0) 
			ELSE CAST (g.IDKONFIGLUPA AS VARCHAR) 
			END,
			g.SHFAQMOBILE,            
	        g.RENDITJAMOBILE,
			koka.GRIDKOKAEMRI,
			g.LLOJFORMATFUSHE		

	from T_LUPAMULTIPLE  a 
	left join T_GRIDATRUPI g on a.GRIDATRUPIID = g.GRIDATRUPIID 
	left join T_KONFIGAMBJENTE k on  a.IDKONFIGAMBJENTE = k.IDKONFIGAMBJENTE
	INNER JOIN T_GRIDAKOKA koka ON g.GRIDAKOKAID = koka.GRIDKOKAID
	 			
	where a.GRIDATRUPIID = @GRIDATRUPIID
	and a.IDKONFIGAMBJENTE = @IDKONFIGAMBJENTE
                            
	Set @Err = @@Error

	RETURN @Err
	
END'),
(N'prc_T_KOKAFORMATIMPORTI_ktheFormatImportiNdermarrjes', 0x19EB556873FEBD75AD5C777341CA6AA348A6A11902098C3AEAAF3BA69E87350A,
N'
CREATE PROCEDURE [dbo].[prc_T_KOKAFORMATIMPORTI_ktheFormatImportiNdermarrjes]
(
	@IDNDERMARJE T_FOR_KEY
)
AS
BEGIN

DECLARE @Err Int

	SELECT IDKOKA, KODI,T_KOKAFORMATIMPORTI. PERSHKRIMI, IDKATEGORI, IDNDERMARJE, IDPERDORUESI, IDSTATUSDOK, DTKRIJIMI, DTMODIFIKIMI, T_KATEGORINIVELDOK.PERSHKRIMI_sq AS KATEGORI,IDSUPERKATEGORI
	FROM  T_KOKAFORMATIMPORTI INNER JOIN dbo.T_KATEGORINIVELDOK ON IDKATDOK=IDKATEGORI
    WHERE IDNDERMARJE=@IDNDERMARJE AND IDSTATUSDOK<>2
                            
	Set @Err = @@Error

	RETURN @Err
	
END


'),
(N'prc_T_KOKAFORMATIMPORTI_ktheSipasKoditDheNdermarrjes', 0x8852AEB05C818FD8F8AB218BD6F66D6786861880974B8328B635ED8B885D09DA,
N'
CREATE PROCEDURE [dbo].[prc_T_KOKAFORMATIMPORTI_ktheSipasKoditDheNdermarrjes]
(
@KODI T_PERSHKRIM,
@IDNDERMARJE T_FOR_KEY
)
AS
BEGIN

DECLARE @Err Int

		SELECT IDKOKA, KODI,T_KOKAFORMATIMPORTI. PERSHKRIMI, IDKATEGORI, IDNDERMARJE, IDPERDORUESI, IDSTATUSDOK, DTKRIJIMI, DTMODIFIKIMI, T_KATEGORINIVELDOK.PERSHKRIMI_sq AS KATEGORI,IDSUPERKATEGORI
	FROM  T_KOKAFORMATIMPORTI INNER JOIN dbo.T_KATEGORINIVELDOK ON IDKATDOK=IDKATEGORI
    WHERE KODI= @KODI  AND IDNDERMARJE=@IDNDERMARJE AND IDSTATUSDOK<>2
                            
	Set @Err = @@Error

	RETURN @Err
	
END



'),
(N'prc_T_KOKASHITJE_merrGjitheRegjistrimDokumentashDokKonvertimSipasKrijuesit', 0xB9A10CFC7976188719A89F76A5B05452D14D78B1858B891ADE071140FA6A8EEF,
N'CREATE procedure [dbo].[prc_T_KOKASHITJE_merrGjitheRegjistrimDokumentashDokKonvertimSipasKrijuesit] 
(
@IDNDERmarje T_FOR_KEY,
@DATANGA VARCHAR(15),
@DATADERI VARCHAR(15),
@IDPERDORUES T_FOR_KEY,
@IDNIVEL T_FOR_KEY,
@DGJITHEDOK T_BOOLEAN
)
AS
BEGIN
	
DECLARE @Err INT


DECLARE @a AS TABLE (kodi VARCHAR(100), lloji INT, gjatesi DECIMAL(18,2), gjeresi DECIMAL(18,2), sasipermase DECIMAL(18,2), iddet1 INT, iddet2 INT, fsh DECIMAL(18,2), ofer DECIMAL(18,2), id T_FOR_KEY,idkonf T_FOR_KEY, DATA DATETIME,idsh T_FOR_KEY,idkonfsh T_FOR_KEY, dateshitje DATETIME, rowcoutn int)-- fsh dhe ofer jane ne njesine baze te artikullit

INSERT INTO @a        SELECT * FROM  konvertimet( @IDNDERmarje)
DECLARE @b AS TABLE (sasimbetur DECIMAL(18,2),id T_FOR_KEY, idkonf T_FOR_KEY)
INSERT INTO @b 
SELECT MAX(ofer)-SUM (fsh) sasimbetur,id,idkonf FROM @a GROUP BY id,kodi,lloji,idkonf,data

SELECT * FROM (
		SELECT --- dokumentat e shitjes  te pakonvertuar

b.IDSHITJEKOKA as IdDokumenti, b.IDNIVEL as IdNiveli, b.IDKONFIGAMBJENTE AS IdKonfigAmbjente,
			b.NRDOK as NrDokumenti,b.IDKLIENTFURNITOR as IdKlientFurnitori, KODKLIENTFURNITOR AS KodKlientFurnitor, B.PERSHKRIMI as Pershkrimi,b.IDMONEDHA as IdMonedha,
			b.TOTALIMETVSHMEZBRITJE as Vlefta,  b.IDSTATUSDOK as Status, DTDOK AS DtDokumenti,0 AS VleftaPaLikujduar,0 AS Kursi,''01/01/1900'' AS DtMaturimi, ''01/01/1900''AS DtAzhornimi, 0 AS Zbritja, 0 AS KursAzhornimi, 0 AS IdKushtPagese,0 AS VleftaLikuiduar, 0 AS VleftaLikuiduarMon,''gri'' Ngjyra,''RaportiShpejte.aspx?idraporti=82&printo=0&Sesioni=false&kodfatura=''+b.NRDOK+''&kodkonf=''+KODKONFIGAMBJENTE+''&dtdok=''+CONVERT (VARCHAR(20),b.DTDOK,101) AS Raporti, EMERTIMIKF AS EmertimiKf
	FROM [T_KOKASHITJE]  as b INNER JOIN  T_NIVELREGJKONVERTO as k ON b.IDNIVEL =k.IDNIVEL     
	INNER JOIN dbo.T_KONFIGAMBJENTE  ON b.IDKONFIGAMBJENTE=dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE
	LEFT OUTER JOIN dbo.T_KLIENTFURNITOR ON b.IDKLIENTFURNITOR = dbo.T_KLIENTFURNITOR.IDKLIENTFURNITOR
	WHERE b.IDNDERM = @IDNDERmarje   and k.IDNIVELKONVERT=@IDNIVEL AND B.DTDOK BETWEEN CONVERT(DATE, @DATANGA,103) AND CONVERT(DATE, @DATADERI,103) AND b.IDSTATUSDOK<>2 and (b.IDKONFIGAMBJENTE  in( select IDLIDHESE  from  T_LIDHJEAUTORIZIM  where IDAUTORIZIMEKOKA  
            in( select IDAUTORIZIMKOKA  from T_AUTORIZIMTRUPI  where IDPERDORUESI =@IDPERDORUES ) 
            and IDLLOJI in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))
            or b.IDKONFIGAMBJENTE  not in (select idlidhese from T_LIDHJEAUTORIZIM where IDLLOJI 
            in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))) 
            AND b.IDSHITJEKOKA NOT IN (SELECT IDDOKKONVERTUAR FROM T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE= T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR WHERE IDKATDOK=1 OR IDKATDOK=2) 
           
            AND CASE WHEN @DGJITHEDOK = 0 THEN B.IDKRIJUESI
					 ELSE @IDPERDORUES
					 END = @IDPERDORUES --Pati: kur gjithedok eshte true (ose 1), atehere perdoruesi mund te shohe te gjithe dokumentet, edhe ato qe s''i ka krijuar vet, prandaj vendoset kushti @IDPERDORUES=@IDPERDORUES (e njejta gje si 1=1), ndersa kur gjithedok eshte false (ose 0), atehere perdoruesi mund te shohe vetem dokumentet e krijuara vet, prandaj vendoset kushti IDKRIJUESI=@IDPERDORUES. Ketu: @IDPERDORUES eshte id e perdoruesit te loguar.
    
            UNION all
      SELECT-- dokumentat e shitjes te konvertuar

b.IDSHITJEKOKA as IdDokumenti, b.IDNIVEL as IdNiveli, b.IDKONFIGAMBJENTE AS IdKonfigAmbjente,
			b.NRDOK as NrDokumenti,b.IDKLIENTFURNITOR as IdKlientFurnitori, KODKLIENTFURNITOR AS KodKlientFurnitor, B.PERSHKRIMI as Pershkrimi,b.IDMONEDHA as IdMonedha,
			b.TOTALIMETVSHMEZBRITJE as Vlefta,  b.IDSTATUSDOK as Status, DTDOK AS DtDokumenti,0 AS VleftaPaLikujduar,0 AS Kursi,''01/01/1900'' AS DtMaturimi, ''01/01/1900''AS DtAzhornimi, 0 AS Zbritja, 0 AS KursAzhornimi, 0 AS IdKushtPagese,0 AS VleftaLikuiduar, 0 AS VleftaLikuiduarMon,(CASE   WHEN (SELECT COUNT (*) FROM  @b c WHERE  c.id=b.IDSHITJEKOKA AND c.idkonf=b.idkonfigambjente AND  sasimbetur>0)>0 THEN ''verdhe'' WHEN (SELECT COUNT (*) FROM  @b c WHERE  c.id=b.IDSHITJEKOKA AND c.idkonf=b.idkonfigambjente AND ( sasimbetur>0 OR sasimbetur<0))=0 THEN ''gjelber'' else	''kuqe'' END) Ngjyra,''RaportiShpejte.aspx?idraporti=82&printo=0&Sesioni=false&kodfatura=''+b.NRDOK+''&kodkonf=''+KODKONFIGAMBJENTE+''&dtdok=''+CONVERT (VARCHAR(20),b.DTDOK,101) AS Raporti, EMERTIMIKF AS EmertimiKf
	FROM [T_KOKASHITJE]  as b INNER JOIN  T_NIVELREGJKONVERTO as k ON b.IDNIVEL =k.IDNIVEL     
	INNER JOIN dbo.T_KONFIGAMBJENTE  ON b.IDKONFIGAMBJENTE=dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE  
	LEFT OUTER JOIN dbo.T_KLIENTFURNITOR ON b.IDKLIENTFURNITOR = dbo.T_KLIENTFURNITOR.IDKLIENTFURNITOR 
	WHERE b.IDNDERM = @IDNDERmarje   and k.IDNIVELKONVERT=@IDNIVEL AND B.DTDOK BETWEEN CONVERT(DATE, @DATANGA,103) AND CONVERT(DATE, @DATADERI,103) AND b.IDSTATUSDOK<>2 and (b.IDKONFIGAMBJENTE  in( select IDLIDHESE  from  T_LIDHJEAUTORIZIM  where IDAUTORIZIMEKOKA  
            in( select IDAUTORIZIMKOKA  from T_AUTORIZIMTRUPI  where IDPERDORUESI =@IDPERDORUES ) 
            and IDLLOJI in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))
            or b.IDKONFIGAMBJENTE  not in (select idlidhese from T_LIDHJEAUTORIZIM where IDLLOJI 
            in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))) 
            AND b.IDSHITJEKOKA  IN (SELECT IDDOKKONVERTUAR FROM T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE= T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR WHERE IDKATDOK=1 OR IDKATDOK=2) 
             AND CASE WHEN @DGJITHEDOK = 0 THEN B.IDKRIJUESI
					 ELSE @IDPERDORUES
					 END = @IDPERDORUES
	
	UNION ALL --dokumentat e magazines te pakonvertuar
	
	SELECT k.IDKOKAMAGAZINA AS IdDokumenti, k.IDNIVEL AS IdNiveli,k.IDKONFIGAMBJENTE AS IdKonfigAmbjente, k.NRDOK AS NrDokumenti,k.IDKLIENTFURNITOR AS IdKlientFurnitori, KODKLIENTFURNITOR AS KodKlientFurnitor,k.SHENIME AS Pershkrimi,(SELECT idmonedha FROM t_monedha WHERE MONEDHAKOD = (SELECT monedhakod FROM dbo.T_MONEDHA WHERE IDMONEDHA = (SELECT NDERMARJEMON FROM dbo.T_NDERMARJE WHERE IDNDERMARJE=k.IDNDERM))AND dbo.T_MONEDHA.IDNDERMARJE=k.IDNDERM) AS IdMonedha, k.VLEFTA AS Vlefta, k.IDSTATUSDOK AS Status, k.DTDOK AS DtDokumenti ,0 AS VleftaPaLikujduar,0 AS Kursi,''01/01/1900'' AS DtMaturimi, ''01/01/1900''AS DtAzhornimi, 0 AS Zbritja, 0 AS KursAzhornimi, 0 AS IdKushtPagese,0 AS VleftaLikuiduar, 0 AS VleftaLikuiduarMon,''gri'' Ngjyra,''RaportiShpejte.aspx?idraporti=82&printo=0&Sesioni=false&kodfatura=''+k.NRDOK+''&kodkonf=''+KODKONFIGAMBJENTE+''&dtdok=''+CONVERT (VARCHAR(20),k.DTDOK,101)  AS Raporti, EMERTIMIKF AS EmertimiKf
	FROM dbo.T_KOKAMAGAZINA AS k 
	INNER JOIN dbo.T_NIVELREGJKONVERTO AS n ON k.IDNIVEL=n.IDNIVEL 	
	INNER JOIN dbo.T_KONFIGAMBJENTE  ON k.IDKONFIGAMBJENTE=dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE
	LEFT OUTER JOIN dbo.T_KLIENTFURNITOR ON k.IDKLIENTFURNITOR = dbo.T_KLIENTFURNITOR.IDKLIENTFURNITOR
	WHERE k.IDNDERM=@IDNDERmarje  AND n.IDNIVELKONVERT=@IDNIVEL AND k.DTDOK BETWEEN CONVERT(DATE, @DATANGA,103) AND CONVERT(DATE, @DATADERI,103) AND k.IDSTATUSDOK<>2
and (k.IDKONFIGAMBJENTE  in( select IDLIDHESE  from  T_LIDHJEAUTORIZIM  where IDAUTORIZIMEKOKA  
            in( select IDAUTORIZIMKOKA  from T_AUTORIZIMTRUPI  where IDPERDORUESI =@IDPERDORUES ) 
            and IDLLOJI in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))
            or k.IDKONFIGAMBJENTE  not in (select idlidhese from T_LIDHJEAUTORIZIM where IDLLOJI 
            in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))) 
	 AND k.IDGJENERUES IS NULL 
	 AND k.IDKOKAMAGAZINA NOT IN (SELECT IDDOKKONVERTUAR FROM T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE= T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR WHERE IDKATDOK=6) --AND B.IDKRIJUESI = @IDPERDORUES

UNION ALL-- dokumentat e magazines te konvertuar

SELECT k.IDKOKAMAGAZINA AS IdDokumenti, k.IDNIVEL AS IdNiveli, k.IDKONFIGAMBJENTE AS IdKonfigAmbjente, k.NRDOK AS NrDokumenti,k.IDKLIENTFURNITOR AS IdKlientFurnitori, KODKLIENTFURNITOR AS KodKlientFurnitor,k.SHENIME AS Pershkrimi,(SELECT idmonedha FROM t_monedha WHERE MONEDHAKOD = (SELECT monedhakod FROM dbo.T_MONEDHA WHERE IDMONEDHA = (SELECT NDERMARJEMON FROM dbo.T_NDERMARJE WHERE IDNDERMARJE=k.IDNDERM))AND dbo.T_MONEDHA.IDNDERMARJE=k.IDNDERM) AS IdMonedha, k.VLEFTA AS Vlefta, k.IDSTATUSDOK AS Status, k.DTDOK AS DtDokumenti ,0 AS VleftaPaLikujduar,0 AS Kursi,''01/01/1900'' AS DtMaturimi, ''01/01/1900''AS DtAzhornimi, 0 AS Zbritja, 0 AS KursAzhornimi, 0 AS IdKushtPagese,0 AS VleftaLikuiduar, 0 AS VleftaLikuiduarMon,(CASE   WHEN (SELECT COUNT (*) FROM  @b c WHERE  c.id=k.IDKOKAMAGAZINA AND c.idkonf=k.idkonfigambjente AND  sasimbetur>0)>0 THEN ''verdhe'' WHEN (SELECT COUNT (*) FROM  @b c WHERE  c.id=k.IDKOKAMAGAZINA AND c.idkonf=k.idkonfigambjente AND ( sasimbetur>0 OR sasimbetur<0))=0 THEN ''gjelber'' else	''kuqe'' END) Ngjyra,''RaportiShpejte.aspx?idraporti=82&printo=0&Sesioni=false&kodfatura=''+k.NRDOK+''&kodkonf=''+KODKONFIGAMBJENTE+''&dtdok=''+CONVERT (VARCHAR(20),k.DTDOK,101) AS Raporti, EMERTIMIKF AS EmertimiKf
	FROM dbo.T_KOKAMAGAZINA AS k 
	INNER JOIN dbo.T_NIVELREGJKONVERTO AS n ON k.IDNIVEL=n.IDNIVEL 	
	INNER JOIN dbo.T_KONFIGAMBJENTE  ON k.IDKONFIGAMBJENTE=dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE
	LEFT OUTER JOIN dbo.T_KLIENTFURNITOR ON k.IDKLIENTFURNITOR = dbo.T_KLIENTFURNITOR.IDKLIENTFURNITOR    
	WHERE k.IDNDERM=@IDNDERmarje  AND n.IDNIVELKONVERT=@IDNIVEL AND k.DTDOK BETWEEN CONVERT(DATE, @DATANGA,103) AND CONVERT(DATE, @DATADERI,103) AND k.IDSTATUSDOK<>2
and (k.IDKONFIGAMBJENTE  in( select IDLIDHESE  from  T_LIDHJEAUTORIZIM  where IDAUTORIZIMEKOKA  
            in( select IDAUTORIZIMKOKA  from T_AUTORIZIMTRUPI  where IDPERDORUESI =@IDPERDORUES ) 
            and IDLLOJI in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))
            or k.IDKONFIGAMBJENTE  not in (select idlidhese from T_LIDHJEAUTORIZIM where IDLLOJI 
            in (select idllojbuxheti from T_LLOJBUXHETI  where KODLLOJBUXHETI=''KonfigurimDok''))) 
	 AND k.IDGJENERUES IS NULL -- AND B.IDKRIJUESI = @IDPERDORUES
	 AND k.IDKOKAMAGAZINA  IN (SELECT IDDOKKONVERTUAR FROM T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE= T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR WHERE IDKATDOK=6))
	 
	AS dok ORDER BY  DtDokumenti,IdDokumenti

	Set @Err = @@Error

	RETURN @Err
END
'),
(N'prc_T_KPF_merrGjitheKPFteSipasGrupitAndAutorizimeLike', 0x6331A8064A7448DECB0C0E8D7C963696BFDFC5DDABAA333487E063E4586DCBED,
N'CREATE procedure [dbo].[prc_T_KPF_merrGjitheKPFteSipasGrupitAndAutorizimeLike]
(
	@GRUPIKPF T_KOD_NUM,
	@IDNDERmarje T_FOR_KEY,
	@IDPERDORUES T_FOR_KEY,
	@KODIKPF T_KOD_VAR
)
AS
BEGIN

DECLARE @Err Int
DECLARE @a AS TABLE (kodi VARCHAR(20), niveli INT)
INSERT INTO @a 
SELECT kodikpf, NIVELIKPF FROM t_kpf WHERE IDNDERMARJE=@IDNDERmarje AND IDSTATUSDOK=1 ORDER BY KODIKPF desc
SELECT TOP (10)	KODIKPF AS label,IDKPF as value,EMERTIMIKPF_sq as des,(case when NIVELIKPF=1 then 1 else 0 end)as niveli1 ,
			(case when NIVELIKPF=2 then 1 else 0 end)as niveli2,
			(case when NIVELIKPF=3 then 1 else 0 end)as niveli3,
			(case when NIVELIKPF=4 then 1 else 0 end)as niveli4,
			(case when NIVELIKPF=5 then 1 else 0 end)as niveli5,
			(case when NIVELIKPF=6 then 1 else 0 end)as niveli6,
			(case when NIVELIKPF=7 then 1 else 0 end)as niveli7, 0 as gjendja ,
			(select top(1)KODI  from @a WHERE  NIVELI =(k.NIVELIKPF-1 )  AND koDI <k.KODIKPF order by kodi desc  )as Prind,
		(CASE 
			WHEN k.KODIKPF = @KODIKPF THEN 1			
			WHEN k.KODIKPF LIKE (@KODIKPF+''%'') THEN 2			
			ELSE 3 END) AS PRIORITETI			
FROM T_KPF k
WHERE (IDKPF IN (SELECT IDLIDHESE  
				 FROM T_LIDHJEAUTORIZIM 
				 WHERE IDSTATUSDOK=1 and IDAUTORIZIMEKOKA IN (SELECT IDAUTORIZIMKOKA 
											FROM T_AUTORIZIMTRUPI 
											WHERE IDPERDORUESI =@IDPERDORUES) 
					AND IDLLOJI IN (SELECT idllojbuxheti 
									FROM T_LLOJBUXHETI 
									WHERE KODLLOJBUXHETI=''KPF''))
					OR IDKPF NOT IN (SELECT idlidhese 
									 FROM T_LIDHJEAUTORIZIM 
									 WHERE IDSTATUSDOK=1 and IDLLOJI IN (SELECT idllojbuxheti 
													   FROM T_LLOJBUXHETI 
													   WHERE KODLLOJBUXHETI=''KPF'')))
					AND grupiKPF = @GRUPIKPF AND IDKPF >= 0 AND IDNDERmarje = @IDNDERmarje AND IDNDERMARJE>0 
					AND (k.KODIKPF = @KODIKPF OR KODIKPF like ''%'' + @KODIKPF + ''%'')
					AND INAKTIV =''false'' AND idstatusdok<>2
ORDER BY PRIORITETI
                            
	Set @Err = @@Error

	RETURN @Err
	
END'),
(N'prc_T_LLOGARISHPERNDARJEQK_merrSipasIdKpf', 0x77D3BFCD049467FE1C57D8C5FD0ED6ACA38483CECB46B769D7DE9C1C9D6419BE,
N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[prc_T_LLOGARISHPERNDARJEQK_merrSipasIdKpf] 
(@IDKPF T_FOR_KEY	
)
AS
 BEGIN    SET NOCOUNT ON 

DECLARE @Err Int	

SELECT ll.*,KODIKPF,EMERTIMIKPF_sq AS EMERTIMIKPF,NIVELIKPF,(select top(1)k.EMERTIMIKPF_sq  from t_kpf k WHERE  k.NIVELIkpf =(T_KPF.NIVELIKPF-1 )  AND k.KODIKPF <T_KPF.KODIKPF  AND k.IDNDERMARJE=dbo.T_KPF.IDNDERMARJE  )AS prind FROM dbo.T_LLOGARISHPERNDARJEQK AS ll INNER JOIN dbo.T_KPF ON ll.IDKPF = dbo.T_KPF.IDKPF WHERE
(ll.IDKPF   = @idkpf)



	Set @Err = @@Error

	RETURN @Err
END
'),
(N'prc_T_LLOGARISHPERNDARJEQK_sel', 0x748BF1AC4186AAAED617A61EA628C04F9E7ADB7ECC6A8F183770CF0F9DE632A6,
N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[prc_T_LLOGARISHPERNDARJEQK_sel] 

	(@IDLLOGSHPERNDARJE T_AUTO_NUM
	)
AS
 BEGIN    SET NOCOUNT ON 
	
DECLARE @Err Int


SELECT ll.*,KODIKPF,EMERTIMIKPF_sq AS EMERTIMIKPF,NIVELIKPF,(select top(1)k.EMERTIMIKPF_sq  from t_kpf k WHERE  k.NIVELIkpf =(T_KPF.NIVELIKPF-1 )  AND k.KODIKPF <T_KPF.KODIKPF  AND k.IDNDERMARJE=dbo.T_KPF.IDNDERMARJE  ) prind FROM dbo.T_LLOGARISHPERNDARJEQK AS ll INNER JOIN dbo.T_KPF ON ll.IDKPF = dbo.T_KPF.IDKPF WHERE
(IDLLOGSHPERNDARJE   = @IDLLOGSHPERNDARJE  OR @IDLLOGSHPERNDARJE IS NULL)

	Set @Err = @@Error

	RETURN @Err
END
'),
(N'prc_T_NENGRUPILLOGARIA_ins', 0xEF3E879CFE5319DE10697D190CB41FE60A4A1FB78C98CB52C5A126539DDFDF4A,
N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[prc_T_NENGRUPILLOGARIA_ins] 
	
(
@IDNENGRUPILLOGARIA T_AUTO_NUM = null output,
@NRNENGRUPILLOGARIA T_KOD_NUM,
@PERSHKNENGRUPILLOGARIA T_PERSHKRIM,
@IDGRUPILLOGARIA  T_FOR_KEY,
@IDNDERMARJE T_FOR_KEY
)
AS
BEGIN

DECLARE @Err Int	

	INSERT
	INTO [T_NENGRUPILLOGARIA]
	(
[NRNENGRUPILLOGARIA],
[PERSHKNENGRUPILLOGARIA_sq],
[IDGRUPILLOGARIA],
IDNDERMARJE
	)
	VALUES
	(
@NRNENGRUPILLOGARIA ,
@PERSHKNENGRUPILLOGARIA ,
@IDGRUPILLOGARIA,
@IDNDERMARJE
	)

	Set @Err = @@Error
set @IDNENGRUPILLOGARIA  =@@IDENTITY 
	RETURN @Err
END
'),
(N'prc_T_NENGRUPILLOGARIA_upd', 0xBB8D4168D3D4C4AB5F524D62678C2B89C0E7FFBC716718E4C9CEDBE3C7F846A3,
N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[prc_T_NENGRUPILLOGARIA_upd]
 (
@IDNENGRUPILLOGARIA T_AUTO_NUM ,
@NRNENGRUPILLOGARIA T_KOD_NUM,
@PERSHKNENGRUPILLOGARIA T_PERSHKRIM,
@IDGRUPILLOGARIA T_FOR_KEY,
@IDNDERMARJE T_FOR_KEY
)
AS
BEGIN
DECLARE @Err Int	
		UPDATE [T_NENGRUPILLOGARIA] 
	Set
[NRNENGRUPILLOGARIA]=@NRNENGRUPILLOGARIA ,
[PERSHKNENGRUPILLOGARIA_sq]=@PERSHKNENGRUPILLOGARIA,
[IDGRUPILLOGARIA]=@IDGRUPILLOGARIA,
IDNDERMARJE = @IDNDERMARJE
	WHERE
[IDNENGRUPILLOGARIA]=@IDNENGRUPILLOGARIA

	Set @Err = @@Error

	RETURN @Err
	end
'),
(N'prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogariaPozitive', 0x7F5CF9FFE38618DAAFD6A6A237C7EB7CE5F9D054983AC53CBFB1CAB90274AC9B,
N'CREATE procedure [dbo].[prc_T_NENGRUPILLOGARIA_merrGjitheNenGrupetLlogariaPozitive]
(
	@IDNDERMARJE T_FOR_KEY
)
AS
BEGIN

DECLARE @Err Int

	select IDNENGRUPILLOGARIA, NRNENGRUPILLOGARIA, PERSHKNENGRUPILLOGARIA_sq AS PERSHKNENGRUPILLOGARIA, IDGRUPILLOGARIA, IDNDERMARJE
	from T_NENGRUPILLOGARIA where IDNENGRUPILLOGARIA >=0 and
	IDNDERMARJE = @IDNDERMARJE
                           
                            
	Set @Err = @@Error

	RETURN @Err
	
END
'),
(N'prc_T_TRUPIKATEGORIZBRITJE_merrSipasNdermarrjes', 0x7DF0E4B58E67205318EF17F315FD6BC6C2CD82B068C2894E32DEC5B179ADAED9,
N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[prc_T_TRUPIKATEGORIZBRITJE_merrSipasNdermarrjes] 

	(@IDNDERVITI T_AUTO_NUM
	)
AS
BEGIN
	
DECLARE @Err Int
		Select


t.*
	FROM [T_TRUPIKATEGORIZBRITJE] t INNER JOIN T_KOKAKATEGORIZBRITJE k ON k.IDKOKAKATEGORIZBRITJE = t.IDKOKAKATEGORIZBRITJE   
	WHERE
(k.IDNDERMARJE=@IDNDERVITI )

	Set @Err = @@Error

	RETURN @Err
END
');

BEGIN TRAN;

CREATE TABLE hequr.T_RENDI_11 (nr int IDENTITY PRIMARY KEY, tabela sysname NOT NULL, rreshta int NOT NULL);   -- 11 nuk fshin rreshta; mbahet si shenje se skripti eshte ekzekutuar
CREATE TABLE hequr.T_OBJEKTE_11 (emri sysname PRIMARY KEY, lloji varchar(2) NOT NULL);
CREATE TABLE hequr.T_MBETEN_11 (emri sysname PRIMARY KEY, arsye nvarchar(400) NOT NULL);   -- vetem per raport
CREATE TABLE hequr.T_DEFINICIONE_11 (emri sysname PRIMARY KEY, definicioni nvarchar(max) NOT NULL);

DECLARE @sql nvarchar(max), @emri sysname;

/* ---------- 1. procedurat qe rregullohen (kontrolli i tekstit para cdo ndryshimi) ---------- */
SELECT @emri = MIN(f.emri) FROM #fix f
WHERE OBJECT_ID('dbo.' + QUOTENAME(f.emri), 'P') IS NULL
   OR HASHBYTES('SHA2_256', OBJECT_DEFINITION(OBJECT_ID('dbo.' + QUOTENAME(f.emri)))) <> f.hash;
IF @emri IS NOT NULL
BEGIN
    ROLLBACK;
    RAISERROR('Procedura %s mungon ose teksti i saj nuk eshte ai qe u analizua: asgje nuk u ndryshua.', 16, 1, @emri);
    RETURN;
END

INSERT hequr.T_DEFINICIONE_11 (emri, definicioni)
SELECT f.emri, OBJECT_DEFINITION(OBJECT_ID('dbo.' + QUOTENAME(f.emri))) FROM #fix f;

-- procedura e vjeter kalon ne [hequr] me te njejtin emer (nuk mund te rikrijohet nga teksti: kolonat e saj nuk ekzistojne me),
-- pastaj krijohet versioni i rregulluar ne dbo; rollback-u fshin te rejen dhe kthen te vjetren
DECLARE @t nvarchar(max);
DECLARE f CURSOR LOCAL FAST_FORWARD FOR SELECT emri, teksti FROM #fix;
OPEN f; FETCH NEXT FROM f INTO @emri, @t;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    EXEC (@t);
    FETCH NEXT FROM f INTO @emri, @t;
END
CLOSE f; DEALLOCATE f;

/* ---------- 2. objektet qe kalojne ne [hequr] ---------- */
CREATE TABLE #emra (emri sysname PRIMARY KEY);
INSERT #emra (emri) VALUES   -- grupi (ii): pa asnje reference ne program
 (N'HelpCoder_shtoDesignPerFormatPrintimi'), (N'HelpCoder_shtoRaportTeRi'), (N'PRC_T_ASETE_AQTSERIALE_UPDATE_AMORTIZIMIFILLESTAR'),
 (N'prc_T_B_KOKABUXHETI_merrMiratimPerVit'), (N'prc_T_FORMATNRKONFIG_delSipasKatMon'), (N'prc_T_FORMATNRKONFIG_delSipasKatMonNew'),
 (N'prc_T_GRIDATRUPI_ktheGridenKonfigurimit'), (N'prc_T_GRUPENDERMARRJE_merrGrupArkeBankeSipasNdermarjes'), (N'prc_T_KLIENTFURNITOR_merrKlienteOseFurnitoreMeFilter_12012016'),
 (N'prc_T_KPF_insrestore'), (N'prc_T_LLOGARI_insrestore'), (N'prc_T_NIVELREGJISTRIMI_shtoDokumentatDefault_Old'),
 (N'prc_T_PUNONJES_insrestore'), (N'prc_T_VENDNDODHJET_insrestore'), (N'prc_WINLINE_EVO_T_MBUSHTABELAT_INS'),
 (N'SUPPORT_Shfaqja_E_Modulit_Te_Raporteve_Tek_Te_Drejtat_Shtimi_Te_Rolet_Default'), (N'PRC_T_AMORTIZIM_IMPORT'), (N'prc_T_KLIENTMEKUPON_validoKuponTenure'),
 (N'prcKontrolloRaport'), (N'prc_T_TRUPIMAGAZINA_riVleresimProgresivi'), (N'prc_T_TRUPIMAGAZINA_riVleresimTotaleHPD_old'),
 (N'prc_T_TRUPIMAGAZINA_riVleresimTotaleSipasRadhes'), (N'T_MP_PROCESEANALITIKE_HISTORIK_ins'), (N'T_MP_PROCESEANALITIKE_ListaMbylljeve'),
 (N'sp_upgraddiagrams');
INSERT #emra (emri) VALUES   -- grupi (i), DEAD
 (N'prc_T_AMBJENT_sel'), (N'prc_T_GRUPAUDITIMI_ins'), (N'prc_T_GRUPAUDITIMI_sel'),
 (N'prc_T_GRUPAUDITIMITRUPI_ins'), (N'prc_T_GRUPAUDITIMITRUPI_merrTrupinGrupitAuditimit'), (N'prc_T_BUXHETI_merrBuxhetetSipasIdLidheseDheDateAktivizimi'),
 (N'prc_T_TRUPIMAGAZINA_riVleresimTotaleHPD'), (N'Get_gjendjaDebiLlogFillKF'), (N'Get_gjendjaKrediLlogFillFK');

CREATE TABLE #obj (id int PRIMARY KEY, emri sysname NOT NULL, lloji varchar(2) NOT NULL);
INSERT #obj (id, emri, lloji)
SELECT o.object_id, o.name, RTRIM(o.type) FROM sys.objects o JOIN #emra e ON e.emri = o.name
WHERE o.schema_id = SCHEMA_ID('dbo') AND o.type IN ('P', 'V', 'FN', 'IF', 'TF');

INSERT hequr.T_MBETEN_11 (emri, arsye) VALUES
 (N'SP_FshiVeprime_SipasLicences', N'mbahet me kerkese (i pavlefshem: tabela qe nuk ekzistojne); vetem raport');

-- referenca te njohura qe jane vetem brenda komenteve (nuk llogariten)
CREATE TABLE #koment (emri sysname, referues sysname);
INSERT #koment VALUES (N'Get_gjendjaDebiLlogFillKF', N'PRC_RAP_KARTELAKLIENT_MeAutorizim'),
                      (N'Get_gjendjaKrediLlogFillFK', N'PRC_RAP_KARTELAKLIENT_MeAutorizim');

-- si ne 10: mbetet cdo objekt qe e perdor dicka ne dbo qe nuk levizet; perseritet derisa lista te mos ndryshoje
DECLARE @hequr int = 1;
WHILE @hequr > 0
BEGIN
    INSERT hequr.T_MBETEN_11 (emri, arsye)
    SELECT o.emri, N'e perdor ' + MIN(OBJECT_NAME(d.referencing_id))
    FROM #obj o JOIN sys.sql_expression_dependencies d ON (d.referenced_id = o.id OR (d.referenced_id IS NULL AND d.referenced_entity_name = o.emri))
    JOIN sys.objects r ON r.object_id = d.referencing_id
    WHERE r.object_id NOT IN (SELECT id FROM #obj) AND r.type IN ('P', 'V', 'FN', 'IF', 'TF', 'TR') AND r.schema_id = SCHEMA_ID('dbo')
      AND NOT (r.type = 'TR' AND r.parent_object_id IN (SELECT id FROM #obj))
      AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_11)
    GROUP BY o.emri;
    SET @hequr = @@ROWCOUNT;

    -- emri ne tekst si fjale e plote (jo pjese e nje emri me te gjate, p.sh. prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentes)
    INSERT hequr.T_MBETEN_11 (emri, arsye)
    SELECT o.emri, N'emri ne tekstin e ' + MIN(OBJECT_NAME(m.object_id))
    FROM #obj o JOIN #mod m ON (m.definition + N' ') LIKE N'%' + REPLACE(REPLACE(o.emri, '[', '[[]'), '_', '[_]') + N'[^A-Za-z0-9_]%'
    WHERE m.object_id NOT IN (SELECT id FROM #obj) AND o.emri NOT IN (SELECT emri FROM hequr.T_MBETEN_11)
      AND (m.parent_id IS NULL OR m.parent_id NOT IN (SELECT id FROM #obj))
      AND NOT EXISTS (SELECT 1 FROM #koment k WHERE k.emri = o.emri AND k.referues = OBJECT_NAME(m.object_id))
    GROUP BY o.emri;
    SET @hequr += @@ROWCOUNT;

    DELETE FROM #obj WHERE emri IN (SELECT emri FROM hequr.T_MBETEN_11);
END

INSERT hequr.T_OBJEKTE_11 (emri, lloji) SELECT emri, lloji FROM #obj;

DECLARE k CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM hequr.T_OBJEKTE_11;
OPEN k; FETCH NEXT FROM k INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = N'ALTER SCHEMA hequr TRANSFER dbo.' + QUOTENAME(@emri);
    EXEC (@sql);
    FETCH NEXT FROM k INTO @emri;
END
CLOSE k; DEALLOCATE k;

COMMIT;

SELECT lloji, emri FROM hequr.T_OBJEKTE_11 ORDER BY lloji, emri;
SELECT emri, arsye FROM hequr.T_MBETEN_11 ORDER BY emri;
SELECT emri AS rregulluar FROM hequr.T_DEFINICIONE_11 ORDER BY emri;
