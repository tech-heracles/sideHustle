-- Rikthen gjendjen para 20-periudha-indekse.sql.
-- Nese 20 u ekzekutua pasi ekzistonte 00-ruajtje.sql, rikthehet pikerisht gjendja e ruajtur atehere dhe pjesa
-- me poshte (rikthimi sipas databazes ku u pergatiten skriptet) anashkalohet.
SET NOEXEC OFF;
GO
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NOT NULL
BEGIN
    DECLARE @uRikthye bit;
    EXEC dbo.prc_OPTIMIZIM_Rikthe '20', @uRikthye OUTPUT;
    IF @uRikthye = 1 SET NOEXEC ON;
END
GO
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_VITET') AND name = N'IX_T_VITET_IDNDERMARJE')
    DROP INDEX IX_T_VITET_IDNDERMARJE ON dbo.T_VITET;
GO
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_PERIUDHAT') AND name = N'IX_T_PERIUDHAT_IDVITI')
    DROP INDEX IX_T_PERIUDHAT_IDVITI ON dbo.T_PERIUDHAT;
GO
SET NOEXEC OFF;
GO
