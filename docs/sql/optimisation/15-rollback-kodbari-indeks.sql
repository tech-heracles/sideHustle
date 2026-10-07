-- Rikthen gjendjen para 15-kodbari-indeks.sql.
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_KODBARI') AND name = N'IX_T_KODBARI_PERSHKRIMI')
    DROP INDEX IX_T_KODBARI_PERSHKRIMI ON dbo.T_KODBARI;
GO
