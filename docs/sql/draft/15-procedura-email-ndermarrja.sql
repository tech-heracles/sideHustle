-- 15: procedura qe mungonte ne DRAFT (2026-11-26). E therrasin email-et e perdoruesit te ri (fjalekalimi i gjeneruar,
-- linku i verifikimit): pa te keto email-e deshtonin. Kopjuar nga web_vg.
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
IF OBJECT_ID('dbo.prc_T_NDERMARJE_ktheIdNdermarrjenEPareNeListeTeKonfigurimeEmail') IS NOT NULL DROP PROCEDURE dbo.prc_T_NDERMARJE_ktheIdNdermarrjenEPareNeListeTeKonfigurimeEmail;
GO
CREATE PROCEDURE [dbo].[prc_T_NDERMARJE_ktheIdNdermarrjenEPareNeListeTeKonfigurimeEmail]
    (
      @IDPERDORUES T_FOR_KEY ,
      @EshtePunonjes T_BOOLEAN
    )
AS
    BEGIN

        DECLARE @Err INT
        DECLARE @IDLICENCA INT;
        IF ( @EshtePunonjes = 0 )
            BEGIN    
                        SELECT  TOP 1 ISNULL(ISNULL(tk.IDNDERMARJE, -1), 0) AS IDNDERMARJE
                        FROM    dbo.T_KONFIGURIMEMAIL tk 
								inner join dbo.T_NDERMARJE tn on tn.IDNDERMARJE = tk.IDNDERMARJE and tn.IDSTATUSDOK <> 2 AND tn.AKTIV = 1
								inner join (SELECT IDLICENCA FROM dbo.T_ROLPERDORUES
																INNER JOIN dbo.T_ROLI ON T_ROLI.IDROLI = T_ROLPERDORUES.IDROLI
															 WHERE IDPERDORUES = @IDPERDORUES AND T_ROLI.IDSTATUSDOK = 1 AND AKTIVROLI = 1) as Licenca
															 ON Licenca.IDLICENCA = tn.IDLICENCA
                        WHERE   tk.IDNDERMARJE > 0 
                ORDER BY tk.IDNDERMARJE
            END
        ELSE
            BEGIN
                SELECT  ISNULL(ISNULL(T_PUNONJES.IDNDERMARJE, -1), 0) AS IDNDERMARJE
                FROM    dbo.T_PUNONJES
					INNER JOIN  dbo.T_KONFIGURIMEMAIL ON T_PUNONJES.IDNDERMARJE = T_KONFIGURIMEMAIL.IDNDERMARJE AND T_KONFIGURIMEMAIL.IDNDERMARJE > 0
                WHERE   IDPUNONJES = @IDPERDORUES
            END 
        SET @Err = @@Error

        RETURN @Err

    END
GO
