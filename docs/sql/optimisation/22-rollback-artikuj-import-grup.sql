-- Heq prc_T_ARTIKULLI_ruajImport (gjendja para 22-artikuj-import-grup.sql).
-- Aplikacioni e kontrollon vete nese procedura ekziston dhe perndryshe i ruan artikujt nje nga nje si me pare.
-- Nese 22 u ekzekutua pasi ekzistonte 00-ruajtje.sql, rikthehet pikerisht gjendja e ruajtur atehere dhe pjesa
-- me poshte (rikthimi sipas databazes ku u pergatiten skriptet) anashkalohet.
SET NOEXEC OFF;
GO
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NOT NULL
BEGIN
    DECLARE @uRikthye bit;
    EXEC dbo.prc_OPTIMIZIM_Rikthe '22', @uRikthye OUTPUT;
    IF @uRikthye = 1 SET NOEXEC ON;
END
GO
IF OBJECT_ID(N'dbo.prc_T_ARTIKULLI_ruajImport', N'P') IS NOT NULL
    DROP PROCEDURE dbo.prc_T_ARTIKULLI_ruajImport;
GO
SET NOEXEC OFF;
GO
