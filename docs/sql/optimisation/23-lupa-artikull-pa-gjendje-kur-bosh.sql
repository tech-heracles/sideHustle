-- 23 - Lupa e artikujve: pa llogaritje gjendjeje kur lista kthehet bosh
--
-- Procedurat "...PerLupeArtikulli...Filter" (lupa e artikujve me filter ne databaze) llogarisnin gjendjen e te gjithe
-- artikujve te ndermarrjes (#gjendje, nga te gjitha rreshtat e T_TRUPIMAGAZINA) edhe kur @MERRARTIKUJ = 0, d.m.th. kur
-- lupa hapet bosh dhe pret nje filter. Me shume levizje magazine kjo zgjaste minuta. Kur @MERRARTIKUJ = 0 asnje artikull
-- nuk kthehet, keshtu qe gjendja nuk perdoret: llogaritja anashkalohet (#gjendje merr vetem rreshtin bosh si kur
-- @KOSTOSASI = 0). Rezultati mbetet i njejte (asnje rresht).
--
-- Skripti ndryshon vetem kushtin IF(@KOSTOSASI = 1) ne definicionin ekzistues te seciles procedure ne kete databaze;
-- procedurat qe nuk e kane ate tekst sakte nje here (ose jane ndryshuar tashme) lihen sic jane.
-- Rikthimi: 23-rollback-lupa-artikull-pa-gjendje-kur-bosh.sql
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

DECLARE @vjeter nvarchar(100) = N'IF(@KOSTOSASI = 1)';
DECLARE @ri nvarchar(100) = N'IF(@KOSTOSASI = 1 AND @MERRARTIKUJ = 1)';
DECLARE @emri sysname, @def nvarchar(max), @poz int, @n int;

DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT emri FROM @procedurat;
OPEN c;
FETCH NEXT FROM c INTO @emri;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @def = OBJECT_DEFINITION(OBJECT_ID(N'dbo.' + @emri, N'P'));
    SET @n = CASE WHEN @def IS NULL THEN 0
                  ELSE (DATALENGTH(@def) - DATALENGTH(REPLACE(@def, @vjeter, N''))) / DATALENGTH(@vjeter) END;
    SET @poz = CHARINDEX(N'CREATE PROCEDURE', ISNULL(@def, N''));
    IF @def IS NULL
        PRINT @emri + N': nuk ekziston, u la.';
    ELSE IF CHARINDEX(@ri, @def) > 0
        PRINT @emri + N': e ndryshuar me pare.';
    ELSE IF @n <> 1 OR @poz = 0
        PRINT @emri + N': teksti i pritur nuk u gjet, u la sic eshte.';
    ELSE
    BEGIN
        SET @def = STUFF(REPLACE(@def, @vjeter, @ri), @poz, LEN(N'CREATE'), N'ALTER');
        EXEC (@def);
        PRINT @emri + N': u ndryshua.';
    END
    FETCH NEXT FROM c INTO @emri;
END
CLOSE c;
DEALLOCATE c;
GO
