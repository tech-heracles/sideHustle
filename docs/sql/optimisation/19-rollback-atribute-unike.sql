-- Rikthen indeksin I_t_atribute_trupi_konfigAmbjenti vetem mbi IDKONFIGAMBJENTE (gjendja para 19-atribute-unike.sql).
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_ATRIBUTETRUPI') AND name = N'I_t_atribute_trupi_konfigAmbjenti')
    CREATE NONCLUSTERED INDEX I_t_atribute_trupi_konfigAmbjenti ON dbo.T_ATRIBUTETRUPI (IDKONFIGAMBJENTE)
        WITH (DROP_EXISTING = ON);
GO
