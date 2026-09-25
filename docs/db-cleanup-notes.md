# Database cleanup notes (input for the DB draft)

The code no longer uses the items below, but the AVEC database still contains them. Nothing here has been
changed in the database yet. Each item says what the code does today, so the draft can be applied at any time.

## Vodafone

**Reports in `T_RAPORTI`**: their classes and special cases were removed from the code, so these rows should
be hidden (`RAPVISIBLE = 0`) or deleted:

| IDRAPORTI | RAPEMRIREAL |
|---|---|
| 129 | porosiVodafone |
| 144 | pagesaDealer |
| 150 | hyrjeVodafone |
| 151 | gjendjeArtikujtVodafone |
| 153 | veprimeTeAnulluaraVodafone |
| 155 | porosiDealerVodafone |
| 156 | shitjeAnalitikeVodafone |
| 158 | hyrjeVodafoneNdermarrjeBije |
| 159 | veprimeTeAnulluaraVodafoneNdermarrjeBije |
| 501 | statusPorosiVFONE |
| 502 | porosiBazaarDD |
| 504 | postPaidPayments |
| 505 | dailyGuaranteePayment |
| 506 | billPaymentsPerDay |
| 510 | shitjeVFONE |
| 674 | aparatetBleraNgaDealer |
| 675 | gjendjaArtikujveVodafoneExp |
| 731 | LibriShitjeveVodafone |
| 734 | PagesatEKryeraPerMPesa |
| 735 | PagesatPerMPesaSipasIntervaleve |
| 815 | listeArketimeAnullimePostpaid |

`hide-vodafone-reports.sql` (from the earlier session) covers part of this list; it also drops the
design variants 31353 and 10245.

**Toolbar actions**: `T_MENUITEM` 113 `KthimVod` ("Kthim Orderi") and 115 `Bli` ("Blerje Vodafone"), linked to
component 507 through `T_MENUPERKOMPONENTE` 2956 and 2958. The code now skips both, so the rows can go.

**Document types** (`T_KONFIGAMBJENTE`, present in about 91 companies each): `VFONE`, `USHDD`, `BAZAAR`,
`POROSIBAZAAR`, `POROSIDD`, `FBD`, `FBKTHIM`, `FHDealer`, `FHKTHIM`. Normal sales already hide
BAZAAR/POROSIBAZAAR/USHDD/POROSIDD (`clsFunksione.MerrKonfigurimShitje`); VFONE and the dealer types still appear.

**Document options** (`T_KUSHTE` codes), all off or unset in AVEC: `DEVFOne`, `ZDVFONE`, `KGJVFONE`, `ZBVFONE`, `VF_VM`.
The code no longer reads DEVFOne, ZDVFONE, KGJVFONE or ZBVFONE.

**Columns kept only because stored procedures still take them** (all empty in AVEC):

- `T_ARTIKULLI`: `KODVFONE`, `APARATBAZAAR`, `KODOFERTE`, `STOKUMAXVFONE` (0 of 4,501 articles use them).
  Their form fields are already hidden (`T_ATRIBUTETRUPI.VISIBLE = 0`).
- `T_PERDORUESI` and `T_PERDORUESI_HISTORIKU`: `SalesRep*`, `LeaveDateVod`, `LeaveDateShop`,
  `MaternityLeaveEndDate`, `CommentsRetail*`, `IDHIERARKILEAVEREASON` (0 of 675 users have values). Their form fields
  are already hidden.

Removing these columns needs the matching procedure parameters removed as well (e.g. `@APARATBAZAAR` in
`prc_T_ARTIKULLI_*`), followed by the data-layer arguments in `clsDatabaseInventari` / `clsDatabaseAdmin`.

## Tollona

- Options `RSHTT`, `RSHTTK`, `RSHTTKE`, `ZTK` (`T_KUSHTE`): "Jo" for every AVEC document config, and no longer read
  by the code. `ZT` is still used (automatic prices on replacement documents) and must stay.
- `T_NJESIADMINISTRATIVE.CELPERDORUESTOLLONASH`: the code always saves `false`. All AVEC rows are 0 or NULL. Drop the
  column together with the `@CELPERDORUESTOLLONASH` parameter of the insert/update procedures.
- Stored procedures that read the separate TOLLONA database are no longer called.

## Removed pages still registered as components (`T_KOMPONENTE`)

`Backup.aspx`, `Restore.aspx`, and the `ImportWK.aspx?lloji=importtollona*` entries. Rights rows referencing
them can be deleted.

## Client-specific print layouts (`T_RAPORTDESING`)

72 layouts made for individual former clients (SePDeFn saved designs, Redis, Sun Petrolium, Alpha Bank,
Procredit, Gethe, Kastrati, AutoElite, Dulac, Hromodhomia, IMB's own invoice, Delta, EVP). No AVEC document
and no print-format default references them, but every company sees them in the format lists.
Their report classes were kept on purpose, because deleting a class while its row exists makes that option fail.
Delete these rows first, then the classes can be removed from `AlphaWebReports.*`:

```
10249,10250,10251,10252,121,160,161,20275,20276,20278,20280,20281,20287,20291,20292,20293,20294,20295,
20296,20297,20298,20299,30353,30354,30574,30928,30936,30941,30942,30943,31038,31047,31176,31177,31186,
31201,31231,31305,31314,31315,31327,31329,31330,31336,31338,31339,31418,31419,31424,31425,31426,31427,
31429,31435,31437,31467,31471,31472,31479,31482,31538,31539,31542,31609,31612,31650,31651,31676,31677,
31680,31685,31687
```

(The full list with file names can be produced with `SELECT IDRAPORTDESING, FILENAME FROM T_RAPORTDESING WHERE IDRAPORTDESING IN (...)`.)

## Not a cleanup item, but blocking

The employees page needs the database master key: it is encrypted by the original IMB server's service master key.
Someone with the master key password must run
`OPEN MASTER KEY DECRYPTION BY PASSWORD = '...'; ALTER MASTER KEY ADD ENCRYPTION BY SERVICE MASTER KEY;`.
