-- 14: numrat automatike te dokumenteve per ndermarrjen AVEC ne DRAFT (2026-11-26)
-- Me pare DRAFT kishte formatet "Default" por asnje nuk ishte lidhur me dokumentat: cdo fature, hyrje, veprim
-- arke/banke dhe flete kontabel kerkonte numer te shkruar me dore. Tani cdo familje dokumentash ka numrin e vet,
-- nga 1, qe rifillon cdo vit (PERIUDHANRAUTOM = 3, si ne web_vg). Blerjet mbajne numrin e fatures se furnitorit.
-- Rollback: 14-rollback-numra-automatike.sql
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRAN;

DECLARE @nd INT = (SELECT IDNDERMARJE FROM T_NDERMARJE WHERE NDERMARJEKODI = 'AVEC');
DECLARE @perdoruesi INT = 14469;   -- konfig

DECLARE @formate TABLE (kodi VARCHAR(30), emri VARCHAR(200), kategoria INT, kontrolli INT, konfigurimet VARCHAR(400));
INSERT INTO @formate VALUES
 ('FSH',     'Numer automatik per faturat e shitjes',               1, 141, ',FSH,FSHmag,FST,FSHP,FSHPT,FSHZ,'),
 ('OSH',     'Numer automatik per ofertat e shitjes',               1, 141, ',OSH,'),
 ('USH',     'Numer automatik per porosite e shitjes',              1, 141, ',USH,USHmag,'),
 ('KSH',     'Numer automatik per kthimet e shitjes',               1, 141, ',KSH,'),
 ('Hyrje',   'Numer automatik per hyrjet ne magazine',              6, 208, ',FH,FH (SS),FH(NC),FH(NS),FHB,FHK,FHKTHIM,FHNV,FHPP,FHR,FHT,FHTK,FHTT,'),
 ('Dalje',   'Numer automatik per daljet nga magazina',             6, 208, ',FD,FDNV,FDPP,FDS,FDT,FDTK,FDTT,'),
 ('Banka',   'Numer automatik per derdhjet dhe terheqjet bankare',  4, 11,  ',A,P,'),
 ('Arka',    'Numer automatik per arketimet dhe pagesat e arkes',   3, 11,  ',ARKETIM,PAGESE,'),
 ('FK',      'Numer automatik per fletet kontabel',                 5, 95,  ',FleteKontabel,');

DECLARE @kodi VARCHAR(30), @emri VARCHAR(200), @kat INT, @ktr INT, @konf VARCHAR(400), @id INT;
DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT kodi, emri, kategoria, kontrolli, konfigurimet FROM @formate;
OPEN c;
FETCH NEXT FROM c INTO @kodi, @emri, @kat, @ktr, @konf;
WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @id = IDNRAUTOM FROM T_NRAUTOM WHERE IDNDERMARJE = @nd AND KODINRAUTOM = @kodi;
    IF @id IS NULL
    BEGIN
        INSERT INTO T_NRAUTOM (KODINRAUTOM, EMERTIMINRAUTOM, FILLONNRAUTOM, MBARONNRAUTOM, HAPINRAUTOM, DREJTIMINRAUTOM,
                               NGADATA, DERIME, MAJTASNRAUTOM, DJATHTASNRAUTOM, PERIUDHANRAUTOM, GJATESIANRAUTOM,
                               IDNDERMARJE, VITI, IDPERDORUESI, IDSTATUSDOK, DTKRIJIMI, KATEGORIA, LAJMEROPARANRFUNDIT, INTERVALI)
        VALUES (@kodi, @emri, 1, 0, 1, 0, '2013-01-01', '9999-12-31 23:59:59.997', '', '', 3, 0,
                @nd, 0, @perdoruesi, 1, GETDATE(), @kat, 0, 0);
        SET @id = SCOPE_IDENTITY();
    END

    -- lidh kontrollin e numrit te dokumentit te secilit konfigurim me formatin (vetem aty ku nuk ka numer te lidhur)
    UPDATE a SET a.IDNRAUTOMATIK = @id
    FROM T_ATRIBUTETRUPI a
    JOIN T_KONFIGAMBJENTE k ON k.IDKONFIGAMBJENTE = a.IDKONFIGAMBJENTE
    WHERE k.IDNDERMARJE = @nd AND a.IDKONTROLL = @ktr AND a.IDNRAUTOMATIK IS NULL
      AND CHARINDEX(',' + k.KODKONFIGAMBJENTE + ',', @konf) > 0;

    PRINT @kodi + ': format ' + CAST(@id AS VARCHAR) + ', konfigurime te lidhura ' + CAST(@@ROWCOUNT AS VARCHAR);
    SET @id = NULL;
    FETCH NEXT FROM c INTO @kodi, @emri, @kat, @ktr, @konf;
END
CLOSE c; DEALLOCATE c;

COMMIT;
