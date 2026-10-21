-- Heq objektet e 00-ruajtje.sql. Ekzekutohet vetem pasi jane rikthyer te gjitha skriptet 01-26: nese tabela ka ende
-- gjendje te ruajtur, nuk heq asgje (pa to rikthimi i sakte nuk do te ishte me i mundur).
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'dbo.T_OPTIMIZIM_RUAJTJE', N'U') IS NOT NULL
BEGIN
    -- IF i ndare: tabela lexohet vetem kur ekziston
    IF EXISTS (SELECT 1 FROM dbo.T_OPTIMIZIM_RUAJTJE)
    BEGIN
        RAISERROR (N'T_OPTIMIZIM_RUAJTJE ka ende gjendje te ruajtur: riktheni fillimisht skriptet 26...01.', 16, 1);
        SET NOEXEC ON;
    END
END
GO
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar', N'P') IS NOT NULL DROP PROCEDURE dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar;
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NOT NULL DROP PROCEDURE dbo.prc_OPTIMIZIM_Rikthe;
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Ruaj', N'P') IS NOT NULL DROP PROCEDURE dbo.prc_OPTIMIZIM_Ruaj;
IF OBJECT_ID(N'dbo.fn_OPTIMIZIM_PerkufizimIndeksi', N'FN') IS NOT NULL DROP FUNCTION dbo.fn_OPTIMIZIM_PerkufizimIndeksi;
IF OBJECT_ID(N'dbo.T_OPTIMIZIM_RUAJTJE', N'U') IS NOT NULL DROP TABLE dbo.T_OPTIMIZIM_RUAJTJE;
GO
SET NOEXEC OFF;
GO
