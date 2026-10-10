-- 20 - Periudha kontabel sipas dates (prc_T_PERIUDHAT_selIdSipasDateNderm dhe pyetjet e ngjashme)
--
-- Thirret per cdo dokument te ruajtur. T_PERIUDHAT dhe T_VITET kishin vetem celesat primare, keshtu qe lexoheshin te
-- gjitha periudhat e te gjitha ndermarrjeve (1 432) me krahasime teksti: ~1,1 ms CPU per thirrje.
-- Tani: vitet e ndermarrjes -> periudhat e atyre viteve.
-- Rikthimi: 20-rollback-periudha-indekse.sql
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_VITET') AND name = N'IX_T_VITET_IDNDERMARJE')
    CREATE NONCLUSTERED INDEX IX_T_VITET_IDNDERMARJE ON dbo.T_VITET (IDNDERMARJE) INCLUDE (IDSTATUSDOK);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.T_PERIUDHAT') AND name = N'IX_T_PERIUDHAT_IDVITI')
    CREATE NONCLUSTERED INDEX IX_T_PERIUDHAT_IDVITI ON dbo.T_PERIUDHAT (IDVITI, DATAFILLIMIT) INCLUDE (DATAMBARIMIT, EMERPERIUDHA, EKYCUR);
GO
