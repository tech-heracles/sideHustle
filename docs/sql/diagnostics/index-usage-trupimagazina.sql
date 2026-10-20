-- Diagnostike (vetem lexim, nuk ndryshon asgje): perdorimi i indekseve te tabelave te magazines dhe shitjeve.
--
-- Ekzekutojeni ne databazen e klientit pasi serveri te kete punuar te pakten disa dite pa rinisje (statistikat e
-- sys.dm_db_index_usage_stats fshihen kur riniset SQL Server-i). Rezultati tregon per cdo indeks sa here eshte lexuar
-- (seek/scan/lookup) dhe sa here eshte perditesuar: indekset qe vetem perditesohen kushtojne ne cdo ruajtje dokumenti
-- pa ndihmuar asnje pyetje. Ruani rezultatin (p.sh. Results to File) dhe dergojeni per analize; asnje indeks nuk
-- fshihet pa kete.
SET NOCOUNT ON;

SELECT  CONVERT(varchar(19), sqlserver_start_time, 120) AS serveri_nisur_me,
        DATEDIFF(day, sqlserver_start_time, GETDATE()) AS dite_statistika
FROM    sys.dm_os_sys_info;

SELECT  OBJECT_NAME(i.object_id)                                  AS tabela,
        i.name                                                    AS indeksi,
        i.type_desc                                               AS lloji,
        ISNULL(u.user_seeks, 0)                                   AS seeks,
        ISNULL(u.user_scans, 0)                                   AS scans,
        ISNULL(u.user_lookups, 0)                                 AS lookups,
        ISNULL(u.user_updates, 0)                                 AS perditesime,
        CONVERT(varchar(19), u.last_user_seek, 120)               AS seek_fundit,
        CONVERT(varchar(19), u.last_user_scan, 120)               AS scan_fundit,
        (SELECT SUM(ps.used_page_count) * 8 / 1024
         FROM   sys.dm_db_partition_stats ps
         WHERE  ps.object_id = i.object_id AND ps.index_id = i.index_id) AS mb,
        STUFF((SELECT ', ' + c.name + CASE WHEN ic.is_descending_key = 1 THEN ' DESC' ELSE '' END
               FROM   sys.index_columns ic JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
               WHERE  ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 0
               ORDER BY ic.key_ordinal FOR XML PATH('')), 1, 2, '') AS celesat,
        STUFF((SELECT ', ' + c.name
               FROM   sys.index_columns ic JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
               WHERE  ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 1
               FOR XML PATH('')), 1, 2, '') AS te_perfshira,
        i.filter_definition                                       AS filtri
FROM    sys.indexes i
        LEFT JOIN sys.dm_db_index_usage_stats u
            ON u.object_id = i.object_id AND u.index_id = i.index_id AND u.database_id = DB_ID()
WHERE   i.object_id IN (OBJECT_ID('dbo.T_TRUPIMAGAZINA'), OBJECT_ID('dbo.T_KOKAMAGAZINA'),
                        OBJECT_ID('dbo.T_TRUPISHITJE'), OBJECT_ID('dbo.T_KOKASHITJE'),
                        OBJECT_ID('dbo.T_TRUPIFLETEKONTABEL'), OBJECT_ID('dbo.T_KOKAFLETEKONTABEL'))
        AND i.type > 0
ORDER BY tabela, ISNULL(u.user_seeks, 0) + ISNULL(u.user_scans, 0) + ISNULL(u.user_lookups, 0);
