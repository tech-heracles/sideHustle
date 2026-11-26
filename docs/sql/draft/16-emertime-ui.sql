-- 16: emertime qe dilnin gabim ne UI (2026-11-26)
-- Konfigurimet bankare A dhe P quheshin "Arketim"/"Pagese" (si ato te arkes), ndaj faqja "Derdhje e re" shfaqte
-- "Koke Dokumenti: Arketim". Ne menu dhe ne lista quhen Derdhje / Terheqje.
-- Rollback: vendosni perseri 'Arketim'/'Receipt' dhe 'Pagese'/'Payment'.
SET NOCOUNT ON;
UPDATE T_KONFIGAMBJENTE SET PERSHKRIMKONFIGAMBJENTE_sq = N'Derdhje', PERSHKRIMKONFIGAMBJENTE_en = N'Deposit'
WHERE KODKONFIGAMBJENTE = 'A' AND PERSHKRIMKONFIGAMBJENTE_sq = 'Arketim';
PRINT 'A: ' + CAST(@@ROWCOUNT AS VARCHAR);
UPDATE T_KONFIGAMBJENTE SET PERSHKRIMKONFIGAMBJENTE_sq = N'T' + NCHAR(235) + N'rheqje', PERSHKRIMKONFIGAMBJENTE_en = N'Withdrawal'
WHERE KODKONFIGAMBJENTE = 'P' AND PERSHKRIMKONFIGAMBJENTE_sq = 'Pagese';
PRINT 'P: ' + CAST(@@ROWCOUNT AS VARCHAR);

-- Ikona e ngarkimit ishte logoja e vjeter jeshile "W" (images/GIFWEB.svg): tani rrethi portokalli i AVEC.
-- Rollback: UPDATE T_SERVER_CONFIGURATION SET [VALUE] = 'images/GIFWEB.svg' WHERE [KEY] = 'LOADING_URL';
UPDATE T_SERVER_CONFIGURATION SET [VALUE] = 'images/brand/avec-loading.svg' WHERE [KEY] = 'LOADING_URL' AND [VALUE] = 'images/GIFWEB.svg';
PRINT 'LOADING_URL: ' + CAST(@@ROWCOUNT AS VARCHAR);

-- Klienti/Furnitori i ri nga forma e plote nuk merrte llogarine (411/401) si shtimi i shpejte nga fatura:
-- kopjohet vlera default e fushes "Nr. Llogari" (txtNr2) nga konfigurimet e shpejta KLSH -> K dhe FRSH -> F.
UPDATE a SET a.VLEREDEFAULT_sq = s.VLEREDEFAULT_sq, a.VLEREDEFAULT_en = s.VLEREDEFAULT_en, a.VLEREDEFAULT_fr = s.VLEREDEFAULT_fr
FROM T_ATRIBUTETRUPI a
JOIN T_KONFIGAMBJENTE k ON k.IDKONFIGAMBJENTE = a.IDKONFIGAMBJENTE AND k.KODKONFIGAMBJENTE IN ('K','F')
JOIN T_KONFIGAMBJENTE ks ON ks.IDNDERMARJE = k.IDNDERMARJE AND ks.KODKONFIGAMBJENTE = CASE k.KODKONFIGAMBJENTE WHEN 'K' THEN 'KLSH' ELSE 'FRSH' END
JOIN T_ATRIBUTETRUPI s ON s.IDKONFIGAMBJENTE = ks.IDKONFIGAMBJENTE AND s.IDKONTROLL = 2113
WHERE a.IDKONTROLL = 638 AND ISNULL(CAST(a.VLEREDEFAULT_sq AS NVARCHAR(100)), '') = '' AND ISNULL(CAST(s.VLEREDEFAULT_sq AS NVARCHAR(100)), '') <> '';
PRINT 'txtNr2 default: ' + CAST(@@ROWCOUNT AS VARCHAR);

-- Klienti: fusha "Agjent shitjesh" ishte e dukshme pa etiketen e saj (etiketa VISIBLE = 0): shfaqet etiketa.
UPDATE a SET a.VISIBLE = 1
FROM T_ATRIBUTETRUPI a
JOIN T_KONTROLLE ko ON ko.IDKONTROLL = a.IDKONTROLL AND ko.KODKONTROLL = 'lblAgjentShitje'
JOIN T_ATRIBUTETRUPI c ON c.IDKONFIGAMBJENTE = a.IDKONFIGAMBJENTE
JOIN T_KONTROLLE kc ON kc.IDKONTROLL = c.IDKONTROLL AND kc.KODKONTROLL = 'cmbAgjentShitjesh'
WHERE a.VISIBLE = 0 AND c.VISIBLE = 1;
PRINT 'lblAgjentShitje: ' + CAST(@@ROWCOUNT AS VARCHAR);
