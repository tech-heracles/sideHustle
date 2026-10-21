-- Rikthen kushtin IF(@KOSTOSASI = 1) te procedurave te lupes se artikujve (gjendja para 23-lupa-artikull-pa-gjendje-kur-bosh.sql).
-- Nese 23 u ekzekutua pasi ekzistonte 00-ruajtje.sql, rikthehet pikerisht gjendja e ruajtur atehere dhe pjesa
-- me poshte (rikthimi sipas databazes ku u pergatiten skriptet) anashkalohet.
SET NOEXEC OFF;
GO
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NOT NULL
BEGIN
    DECLARE @uRikthye bit;
    EXEC dbo.prc_OPTIMIZIM_Rikthe '23', @uRikthye OUTPUT;
    IF @uRikthye = 1 SET NOEXEC ON;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DECLARE @procedurat TABLE (emri sysname);
INSERT INTO @procedurat VALUES
    (N'prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliFilter'),
    (N'prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliJoPerbProdhimFilter'),
    (N'prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliPerbereFilter'),
    (N'prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliPerProdhimFilter'),
    (N'prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliProdhimiFilter'),
    (N'prc_T_ARTIKULLI_merrArtikullSipasNdermarjesAndAutorizimPerLupeArtikulliProdhimiFilterPlanifikim');

DECLARE @vjeter nvarchar(100) = N'IF(@KOSTOSASI = 1 AND @MERRARTIKUJ = 1)';
DECLARE @ri nvarchar(100) = N'IF(@KOSTOSASI = 1)';
DECLARE @emri sysname, @def nvarchar(max), @poz int;

DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM @procedurat;
OPEN c;
FETCH NEXT FROM c INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @def = OBJECT_DEFINITION(OBJECT_ID(N'dbo.' + @emri, N'P'));
    SET @poz = CHARINDEX(N'CREATE PROCEDURE', ISNULL(@def, N''));
    IF @def IS NOT NULL AND CHARINDEX(@vjeter, @def) > 0 AND @poz > 0
    BEGIN
        SET @def = STUFF(REPLACE(@def, @vjeter, @ri), @poz, LEN(N'CREATE'), N'ALTER');
        EXEC (@def);
        PRINT @emri + N': u rikthye.';
    END
    FETCH NEXT FROM c INTO @emri;
END
CLOSE c;
DEALLOCATE c;
GO
SET NOEXEC OFF;
GO
