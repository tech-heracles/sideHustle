IF EXISTS 
	(SELECT 1 FROM sys.objects WHERE [NAME] = 'prc_T_LLOGARI_merrLlogariSipasIdVe' AND [TYPE] = 'P')
	DROP PROCEDURE prc_T_LLOGARI_merrLlogariSipasIdVe
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[prc_T_LLOGARI_merrLlogariSipasIdVe]
    (
      @idLlogarish VARCHAR(MAX)
    )
AS
    BEGIN

        DECLARE @Err INT
		DECLARE @IDS AS TABLE( IDLLOGARI INT );
		INSERT INTO @IDS(IDLLOGARI)
		SELECT *
		FROM DBO.SPLIT_STRING( @idLlogarish, ',' );

        SELECT  tp.* ,
                tgp.PERSHKGRUPILLOGARIA_sq,
                tq.PERSHKNENGRUPILLOGARIA_sq,
                tm.MONEDHAKOD ,
                tm.MONEDHAPERSHK ,
                tk1.KODIKPF AS KodiKPF1 ,
                tk2.KODIKPF AS KodiKPF2 ,
                tk3.KODIKPF AS KodiKPF3 ,
                taksa.NORMAPERQINDJE AS PERSHKRIMTAKSA ,
                o.KODI AS Objektiva ,
                COALESCE(q.KODI, sk.KODI) AS Qendra ,
                ks.KODI AS KategoriShpenzimi
        FROM    T_LLOGARI tp
				INNER JOIN @IDS id ON id.IDLLOGARI = tp.IDLLOGARI
                LEFT OUTER JOIN T_GRUPILLOGARIA tgp ON tp.GRUP = tgp.IDGRUPILLOGARIA
                LEFT OUTER JOIN T_NENGRUPILLOGARIA tq ON tp.NENGRUP = tq.IDNENGRUPILLOGARIA
                LEFT OUTER JOIN T_MONEDHA AS tm ON tm.IDMONEDHA = tp.MONEDHA
                LEFT OUTER JOIN T_KPF AS tk1 ON tp.KPF1 = tk1.IDKPF
                LEFT OUTER JOIN T_KPF AS tk2 ON tp.KPF2 = tk2.IDKPF
                LEFT OUTER JOIN T_KPF AS tk3 ON tp.KPF3 = tk3.IDKPF
                LEFT OUTER JOIN T_TAKSAT AS taksa ON taksa.IDTAKSA = tp.NIVEL_TAKSE
                LEFT OUTER JOIN dbo.T_OBJEKTIVAKOSTO o ON o.ID = tp.IDOBJEKTIVAKOSTO
                LEFT OUTER JOIN dbo.T_QENDRAKOSTO q ON q.ID = tp.QENDRA_KOSTOS
                LEFT OUTER JOIN dbo.T_KOKASKEMAQK sk ON sk.IDKOKA = tp.IDSKEMAQENDRAKOSTO
                LEFT OUTER JOIN T_KATEGORISHPENZIMI ks ON ks.ID = tp.IDKATEGORISHPENZIMI
                            
        SET @Err = @@Error

        RETURN @Err
	
    END