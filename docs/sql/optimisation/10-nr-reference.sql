-- 10 - Numri i references se fletes kontabel (trg_NrReference -> Get_T_KOKAFLETEKONTABEL_gjeneroNrReference)
--
-- Per cdo flete kontabel qe krijohet (cdo dokument shitje/blerje/magazine i kontabilizuar, edhe ne import) trigeri
-- llogarit MAX(NRREFERENCEKOKAFLETEKONTABEL) + 1 per vitin (IDNDERVITI). Indeksi ekzistues kishte NRREFERENCE vetem si
-- kolone "INCLUDE", keshtu qe MAX lexonte te gjitha rreshtat e vitit (~190 mije, ~50 ms per dokument; ne importin e
-- 1000 blerjeve: 92 s nga 179 s SQL). Me NRREFERENCE si kolone celesi MAX eshte nje kerkim i vetem.
-- I njejti indeks (emer, kolona), vetem renditja: cdo pyetje qe e perdorte me pare e perdor njesoj.
-- Rikthimi: 10-rollback-nr-reference.sql
CREATE NONCLUSTERED INDEX T_KOKAFLETEKONTABEL_23072018
    ON dbo.T_KOKAFLETEKONTABEL (IDNDERVITI, NRREFERENCEKOKAFLETEKONTABEL)
    WITH (DROP_EXISTING = ON);
GO
