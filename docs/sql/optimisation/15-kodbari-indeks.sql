-- 15 - Kerkimi i artikullit sipas kodbarit (T_KODBARI.PERSHKRIMI)
--
-- T_KODBARI nuk kishte indeks mbi vete kodbarin, keshtu qe cdo kerkim sipas kodbarit (kontrolli "ekziston kodbari" ne
-- importin e artikujve, gjetja e artikullit sipas kodbarit ne shitje/POS) lexonte te gjithe tabelen. Ne importin e
-- 13 882 artikujve: 8 983 kontrolle x ~1,3 ms. Indeks (PERSHKRIMI) me IDARTIKULLI: kerkim i drejtperdrejte.
-- Rikthimi: 15-rollback-kodbari-indeks.sql
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_KODBARI') AND name = N'IX_T_KODBARI_PERSHKRIMI')
    CREATE NONCLUSTERED INDEX IX_T_KODBARI_PERSHKRIMI ON dbo.T_KODBARI (PERSHKRIMI) INCLUDE (IDARTIKULLI);
GO
