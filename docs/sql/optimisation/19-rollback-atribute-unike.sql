-- Rikthen indeksin I_t_atribute_trupi_konfigAmbjenti vetem mbi IDKONFIGAMBJENTE (gjendja para 19-atribute-unike.sql).
-- Nese 19 u ekzekutua pasi ekzistonte 00-ruajtje.sql, rikthehet pikerisht gjendja e ruajtur atehere dhe pjesa
-- me poshte (rikthimi sipas databazes ku u pergatiten skriptet) anashkalohet.
SET NOEXEC OFF;
GO
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NOT NULL
BEGIN
    DECLARE @uRikthye bit;
    EXEC dbo.prc_OPTIMIZIM_Rikthe '19', @uRikthye OUTPUT;
    IF @uRikthye = 1 SET NOEXEC ON;
END
GO
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_ATRIBUTETRUPI') AND name = N'I_t_atribute_trupi_konfigAmbjenti')
    CREATE NONCLUSTERED INDEX I_t_atribute_trupi_konfigAmbjenti ON dbo.T_ATRIBUTETRUPI (IDKONFIGAMBJENTE)
        WITH (DROP_EXISTING = ON);
GO
SET NOEXEC OFF;
GO
