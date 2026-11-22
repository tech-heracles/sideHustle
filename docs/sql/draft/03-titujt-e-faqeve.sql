/*
  DRAFT: page titles shown in the header (T_KOMPONENTE.KOMPPERSHKRIMI_sq) without typos and named like the menu.
  Old values are kept in T_KOMPONENTE_TITUJ_BACKUP_20261122; rollback: 03-rollback-titujt-e-faqeve.sql.
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 03-titujt-e-faqeve.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('dbo.T_KOMPONENTE_TITUJ_BACKUP_20261122') IS NULL
    SELECT IDKOMPON, KOMPPERSHKRIMI_sq INTO dbo.T_KOMPONENTE_TITUJ_BACKUP_20261122 FROM T_KOMPONENTE;

DECLARE @t TABLE (id int PRIMARY KEY, titull nvarchar(200));
INSERT @t VALUES
 (100, N'Përdorues'), (129, N'Ndërmarrje'), (160, N'Role'), (162, N'Ndryshim fjalëkalimi'), (180, N'Grupim ndërmarrjesh'),
 (231, N'Politika e fjalëkalimit'),
 (110, N'Struktura e llogarive'), (116, N'Fletë kontabël e re'), (117, N'Fletë kontabël'), (230, N'Kategori shpenzimi'),
 (517, N'Shpërndarje shpenzimesh'), (518, N'Shpërndarje shpenzimesh e re'),
 (225, N'Konfigurimet e bilancit'), (226, N'Konfigurimet e cash flow'), (227, N'Konfigurimet e P.A.SH'),
 (232, N'Konfigurimet e formateve buxhetore'), (3090, N'Konfigurimet e bilancit OJF'), (3091, N'Konfigurimet e cash flow OJF'),
 (3092, N'Konfigurimet e P.A.SH OJF'),
 (402, N'Artikuj'), (403, N'Artikuj afatgjatë'), (413, N'Njësi matëse'), (414, N'Grupe artikujsh'), (415, N'Grupe artikujsh afatgjatë'),
 (513, N'Hyrje'), (510, N'Hyrje e re'), (519, N'Dalje'), (511, N'Dalje e re'),
 (547, N'Inventarizim për artikujt afatgjatë'), (549, N'Inventarizim për artikujt afatgjatë – i ri'),
 (548, N'Inventarizim për artikujt afatshkurtër'), (550, N'Inventarizim për artikujt afatshkurtër – i ri'),
 (544, N'Ndryshim sasi/çmim'), (545, N'Ndryshim sasi/çmim i ri'), (532, N'Rivlerësim inventari'),
 (537, N'Rezervime hyrje'), (539, N'Rezervim hyrje i ri'), (538, N'Rezervime dalje'), (540, N'Rezervim dalje i ri'),
 (123, N'Klientë'), (124, N'Furnitorë'), (505, N'Shitje'), (506, N'Shitje e re'), (507, N'Blerje'), (508, N'Blerje e re'),
 (149, N'Agjentë shitje'), (417, N'Kategori zbritje'), (144, N'Lloje transporti'), (146, N'Kusht dërgimi'), (147, N'Kusht dërgimi i ri'),
 (220, N'Kusht pagese'), (221, N'Kusht pagese i ri'), (166, N'Lloj transporti i ri'),
 (523, N'Fletë doganore import'), (522, N'Fletë doganore import e re'), (527, N'Fletë doganore eksport'), (528, N'Fletë doganore eksport e re'),
 (229, N'Lidhje dokumentesh'), (217, N'Lidhje dokumentesh e re'),
 (652, N'Veprime me klient/furnitor'), (653, N'Veprim me klient/furnitor i ri'),
 (651, N'Azhornime klient/furnitor'), (650, N'Azhornim klient/furnitor i ri'),
 (678, N'Mbyllje klient/furnitor'), (679, N'Mbyllje klient/furnitor e re'), (423, N'Përcaktim zbritje'),
 (301, N'Derdhje e re'), (306, N'Tërheqje e re'), (307, N'Arkëtim i ri'), (308, N'Pagesë e re'),
 (122, N'Çelje arka'), (121, N'Çelje banka'),
 (701, N'Departamente'), (709, N'Listëpagesa'), (710, N'Listëpagesë e re'), (704, N'Komponente listëpagese'), (703, N'Komponente page'),
 (309, N'Grupe'), (310, N'Tituj'), (311, N'Kapituj'), (312, N'Urdhër pagesa'), (313, N'Urdhër pagesë i ri'),
 (803, N'Planifikime prodhimi'), (804, N'Planifikim prodhimi i ri'), (805, N'Ekzekutime prodhimi'), (806, N'Ekzekutim prodhimi i ri'),
 (807, N'Gjenero planifikim prodhimi'), (808, N'Skedulime prodhimi'), (809, N'Skedulim prodhimi i ri'),
 (903, N'Qendra kosto'), (904, N'Skema e qendrave të kostos'), (905, N'Objektiva kosto'), (907, N'Regjistrime në qendra kosto'),
 (908, N'Regjistrim në qendra kosto i ri'), (901, N'Llogari për shpërndarje në qendra kosto'),
 (1001, N'Standarde amortizimi'), (1002, N'Rregulla amortizimi'), (1003, N'Amortizim i ri'), (1004, N'Amortizime'),
 (1005, N'Amortizime fillestare'), (1006, N'Rivlerësime'), (1007, N'Amortizim fillestar i ri'), (1008, N'Rivlerësim i ri'),
 (535, N'Aprovime'), (536, N'Kërkesa aprovimi'),
 (3066, N'Miratime buxheti'), (3065, N'Miratim buxheti i ri'), (3067, N'Planifikime buxheti'), (3068, N'Planifikim buxheti i ri'),
 (3070, N'Alokime buxheti'), (3069, N'Alokim buxheti i ri'), (3071, N'Rialokime buxheti'), (3072, N'Rialokim buxheti i ri'),
 (3073, N'Përfitime buxheti'), (3074, N'Përfitim buxheti i ri'), (3075, N'Planifikime ekzekutimi buxheti'),
 (3076, N'Planifikim ekzekutimi buxheti i ri'), (3077, N'Ekzekutime buxheti'), (3078, N'Ekzekutim buxheti i ri'),
 (3095, N'Hedhja e të dhënave të planifikimit'),
 (174, N'Formate importi/eksporti'), (175, N'Format importi/eksporti i ri'), (177, N'Eksport'), (207, N'Fusha shtesë'),
 (128, N'Numra automatikë'), (133, N'Monedha'), (215, N'Vite'), (429, N'Makro'), (431, N'Makro e re'), (432, N'Modifiko makron'),
 (520, N'Taksa'), (102, N'Qytete'),
 (443, N'Lloje defekti'), (444, N'Statuse riparimi'), (541, N'Regjistrime riparimi'), (542, N'Regjistrim riparimi i ri'),
 (3059, N'Receta optike'), (3058, N'Recetë optike e re'),
 (2001, N'Route i agjentëve'), (2002, N'Konfiguro anketë'), (2005, N'Përcakto veprimtari'), (2007, N'Anketa'),
 (1016, N'Amortizimi i aseteve në përqindje'), (1013, N'Njësi administrative'), (1014, N'Shitje sipas magazinave'),
 (1017, N'Shitje sipas klientëve'), (1018, N'Blerje sipas furnitorëve'), (1015, N'Gjendja e magazinave'), (1020, N'Shitje sipas pikave të shitjes'),
 (1019, N'Marzhi i shitjes sipas klientëve'), (1021, N'Harta e klientëve');

UPDATE k SET KOMPPERSHKRIMI_sq = t.titull
FROM T_KOMPONENTE k JOIN @t t ON t.id = k.IDKOMPON;

SELECT @@ROWCOUNT AS titujt_e_ndryshuar;
