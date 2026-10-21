-- Rikthen gjendjen para 15-kodbari-indeks.sql.
-- Nese 15 u ekzekutua pasi ekzistonte 00-ruajtje.sql, rikthehet pikerisht gjendja e ruajtur atehere dhe pjesa
-- me poshte (rikthimi sipas databazes ku u pergatiten skriptet) anashkalohet.
SET NOEXEC OFF;
GO
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NOT NULL
BEGIN
    DECLARE @uRikthye bit;
    EXEC dbo.prc_OPTIMIZIM_Rikthe '15', @uRikthye OUTPUT;
    IF @uRikthye = 1 SET NOEXEC ON;
END
GO
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_KODBARI') AND name = N'IX_T_KODBARI_PERSHKRIMI')
    DROP INDEX IX_T_KODBARI_PERSHKRIMI ON dbo.T_KODBARI;
GO
SET NOEXEC OFF;
GO
