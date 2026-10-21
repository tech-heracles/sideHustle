-- Rikthen indeksin para 10-nr-reference.sql (NRREFERENCE si kolone INCLUDE).
-- Nese 10 u ekzekutua pasi ekzistonte 00-ruajtje.sql, rikthehet pikerisht gjendja e ruajtur atehere dhe pjesa
-- me poshte (rikthimi sipas databazes ku u pergatiten skriptet) anashkalohet.
SET NOEXEC OFF;
GO
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NOT NULL
BEGIN
    DECLARE @uRikthye bit;
    EXEC dbo.prc_OPTIMIZIM_Rikthe '10', @uRikthye OUTPUT;
    IF @uRikthye = 1 SET NOEXEC ON;
END
GO
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_KOKAFLETEKONTABEL') AND name = N'T_KOKAFLETEKONTABEL_23072018')
    CREATE NONCLUSTERED INDEX T_KOKAFLETEKONTABEL_23072018
        ON dbo.T_KOKAFLETEKONTABEL (IDNDERVITI)
        INCLUDE (NRREFERENCEKOKAFLETEKONTABEL)
        WITH (DROP_EXISTING = ON);
GO
SET NOEXEC OFF;
GO
