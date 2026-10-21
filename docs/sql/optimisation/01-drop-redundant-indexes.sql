-- 01 - Heq indekset e teperta (redundant)
--
-- Nje indeks eshte i teperte kur nje indeks tjeter ne te njejten tabele ka te njejtat kolona celes ne fillim
-- (ne te njejten rradhe) dhe permban te gjitha kolonat e tij te perfshira (INCLUDE): cdo kerkim qe perdor
-- indeksin e hequr mund te perdore ate qe mbetet. Indekset UNIQUE, PRIMARY KEY dhe me filter nuk preken.
-- Perfitimi: cdo INSERT/UPDATE/DELETE (importet, ruajtja e dokumenteve) mirembante keto indekse kot.
--
-- U kontrollua qe asnje procedure/view/funksion dhe asnje rresht kodi i AVEC nuk i permend me emer.
-- Rikthimi: 01-rollback-redundant-indexes.sql
-- Cdo indeks hiqet vetem nese ne kete databaze ekziston nje indeks tjeter qe e mbulon (kontrollohet nga
-- prc_OPTIMIZIM_HiqIndeksTeMbuluar, 00-ruajtje.sql); perkufizimi ruhet para heqjes, rikthimi e krijon perseri.
-- I sigurt per t'u ekzekutuar disa here.
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
-- T_ARTIKULLI.idx_tartikulli_IDARTIKULLI (0 MB): i mbuluar nga PK_T_ARTIKULLI
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_ARTIKULLI', N'idx_tartikulli_IDARTIKULLI';

-- T_ARTIKULLI.IX_T_ARTIKULLI_IDARTIKULLI (0 MB): i mbuluar nga idx_tartikulli_IDARTIKULLI
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_ARTIKULLI', N'IX_T_ARTIKULLI_IDARTIKULLI';

-- T_ARTIKULLI.IX_T_ARTIKULLI_IDNDERMARJE (0 MB): i mbuluar nga autokomplete
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_ARTIKULLI', N'IX_T_ARTIKULLI_IDNDERMARJE';

-- T_ARTIKULLI.IX_T_ARTIKULLI_IDSTATUSDOK (0 MB): i mbuluar nga 20160208
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_ARTIKULLI', N'IX_T_ARTIKULLI_IDSTATUSDOK';

-- T_KLIENTFURNITOR.INDEX_T_KLIENTFURNITOR_LLOJIKF (0 MB): i mbuluar nga IX_KLIENTFURNITOR_Lloji_Aktiv_Nderm_Status
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KLIENTFURNITOR', N'INDEX_T_KLIENTFURNITOR_LLOJIKF';

-- T_KOKAMAGAZINA.idx_tkokamagazina_IDKOKAMAGAZINA (4 MB): i mbuluar nga IX_KOKAMAGAZINA_KONFIG
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKAMAGAZINA', N'idx_tkokamagazina_IDKOKAMAGAZINA';

-- T_KOKAMAGAZINA.idx_tkokamagazina_IDSTATUSDOK (6 MB): i mbuluar nga T_KOKAMAGAZINA_IDKONFIGAMBJENTE_04122018
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKAMAGAZINA', N'idx_tkokamagazina_IDSTATUSDOK';

-- T_KOKAMAGAZINA.IX_KOKAMAGAZINA_KONFIG (7 MB): i mbuluar nga PK_T_KOKAMAGAZINA
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKAMAGAZINA', N'IX_KOKAMAGAZINA_KONFIG';

-- T_KOKAMAGAZINA.IX_T_KOKAMAGAZINA_IDKOKAMAGAZINA (4 MB): i mbuluar nga IX_KOKAMAGAZINA_KONFIG
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKAMAGAZINA', N'IX_T_KOKAMAGAZINA_IDKOKAMAGAZINA';

-- T_KOKAMAGAZINA.IX_T_KOKAMAGAZINA_IDKONFIGAMBJENTE (6 MB): i mbuluar nga IX_TK_KonfigNderm_Optimized
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKAMAGAZINA', N'IX_T_KOKAMAGAZINA_IDKONFIGAMBJENTE';

-- T_KOKAMAGAZINA.IX_T_KOKAMAGAZINA_IDSTATUSDOK (5 MB): i mbuluar nga T_KOKAMAGAZINA_IDKONFIGAMBJENTE_04122018
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKAMAGAZINA', N'IX_T_KOKAMAGAZINA_IDSTATUSDOK';

-- T_KOKAMAGAZINA.T_KOKAMAGAZINA_04122018_IDKONFIGAMBJENTE (9 MB): i mbuluar nga T_KOKAMAGAZINA_IDKONFIGAMBJENTE_04122018
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKAMAGAZINA', N'T_KOKAMAGAZINA_04122018_IDKONFIGAMBJENTE';

-- T_KOKASHITJEHISTORIK.index_T_KOKASHITJEHISTORIK_nonclustered_05062018 (1 MB): i mbuluar nga index_T_KOKASHITJEHISTORIK_nonclustered_080620182
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KOKASHITJEHISTORIK', N'index_T_KOKASHITJEHISTORIK_nonclustered_05062018';

-- T_kokashitjelogical.IX_kokashitjelogical_IdImport (1 MB): i mbuluar nga IX_kokashitjelogical_IdImport_KodNderm
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_kokashitjelogical', N'IX_kokashitjelogical_IdImport';

-- T_KONFIGAMBJENTE.index_T_KONFIGAMBJENTE_20181203_03 (0 MB): i mbuluar nga index_T_NIVELREGJISTRIMI_20181203_02
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KONFIGAMBJENTE', N'index_T_KONFIGAMBJENTE_20181203_03';

-- T_KONFIGAMBJENTE.IX_T_KONFIGAMBJENTE_IDKONFIGAMBJENTE (0 MB): i mbuluar nga PK_T_KONFIGAMBJENTE
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KONFIGAMBJENTE', N'IX_T_KONFIGAMBJENTE_IDKONFIGAMBJENTE';

-- T_KONFIGAMBJENTE.IX_T_KONFIGAMBJENTE_IDNDERMARJE (0 MB): i mbuluar nga IND_T_KONFIGAMBJENTE_24_01_17_1
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KONFIGAMBJENTE', N'IX_T_KONFIGAMBJENTE_IDNDERMARJE';

-- T_KPF.T_KPF_KODIKPF (0 MB): i mbuluar nga index_T_KPF_24052018_02
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KPF', N'T_KPF_KODIKPF';

-- T_KUSHTEMPLATE.indexidkonvlera (5 MB): i mbuluar nga IX_KushTemplate_KonfigKusht
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_KUSHTEMPLATE', N'indexidkonvlera';

-- T_LLOGARI.index_T_LLOGARI_24052018_01 (1 MB): i mbuluar nga T_LLOGARI_NRLLOGari_IDSTATUSDOK
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_LLOGARI', N'index_T_LLOGARI_24052018_01';

-- T_NIVELREGJISTRIMI.index_T_NIVELREGJISTRIMI_nonclustered_08062018 (0 MB): i mbuluar nga index_T_NIVELREGJISTRIMI_20181203_01
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_NIVELREGJISTRIMI', N'index_T_NIVELREGJISTRIMI_nonclustered_08062018';

-- T_NJESIADMINISTRATIVE.IX_T_NJESIADMINISTRATIVE_IDNJESIADM (0 MB): i mbuluar nga PK_T_NJESIADMINISTRATIVE
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_NJESIADMINISTRATIVE', N'IX_T_NJESIADMINISTRATIVE_IDNJESIADM';

-- T_TRUPIMAGAZINA.index_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullit_Modifikim_10012019 (310 MB): i mbuluar nga IX_TM_ArtMagData_Optimized
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPIMAGAZINA', N'index_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullit_Modifikim_10012019';

-- T_TRUPIMAGAZINA.IX_T_TRUPIMAGAZINA_IDARTIKULL (102 MB): i mbuluar nga IX_TM_ArtMagData_Optimized
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPIMAGAZINA', N'IX_T_TRUPIMAGAZINA_IDARTIKULL';

-- T_TRUPIMAGAZINA.IX_T_TRUPIMAGAZINA_IDARTIKULL_DATA (131 MB): i mbuluar nga IX_T_TRUPIMAGAZINA_IDARTIKULL_DATA_IDKTHIMI
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPIMAGAZINA', N'IX_T_TRUPIMAGAZINA_IDARTIKULL_DATA';

-- T_TRUPIMAGAZINA.IX_T_TRUPIMAGAZINA_IDKOKAMAGAZINA (73 MB): i mbuluar nga IX_T_TRUPIMAGAZINA_IDKOKAMAGAZINA_IDARTIKULL_IDMAG_IDSTATUSDOK
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPIMAGAZINA', N'IX_T_TRUPIMAGAZINA_IDKOKAMAGAZINA';

-- T_TRUPIMAGAZINA.IX_TRUPIMAGAZINA_ART_MAG_DATA (329 MB): i mbuluar nga IX_TM_ArtMagData_Optimized
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPIMAGAZINA', N'IX_TRUPIMAGAZINA_ART_MAG_DATA';

-- T_TRUPIMAGAZINA.IX_TRUPIMAGAZINA_IDARTIKULL_IDMAG_DATA (307 MB): i mbuluar nga IX_TM_ArtMagData_Optimized
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPIMAGAZINA', N'IX_TRUPIMAGAZINA_IDARTIKULL_IDMAG_DATA';

-- T_TRUPIMAGAZINA.IX_TrupiMagazina_Koka_Art_Mag (151 MB): i mbuluar nga IX_T_TRUPIMAGAZINA_IDKOKAMAGAZINA_IDARTIKULL_IDMAG_IDSTATUSDOK
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPIMAGAZINA', N'IX_TrupiMagazina_Koka_Art_Mag';

-- T_TRUPISHITJE.idx_ttrupishitje_IDSHITJEKOKA (46 MB): i mbuluar nga ind20160324_1
EXEC dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar '01', N'dbo.T_TRUPISHITJE', N'idx_ttrupishitje_IDSHITJEKOKA';
