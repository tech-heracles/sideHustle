-- 02 - Rritja e skedareve te databazes
--
-- Databaza e klientit rritej me 1 MB (data) dhe 10% (log). Cdo import/ruajtje qe mbush skedarin ndalon
-- per te zgjeruar skedarin, dhe 23 GB te rritura me hapa 1 MB jane te copezuara ne disk.
-- Rritje fikse: data 256 MB, log 128 MB (rritja me % e log-ut krijon VLF te medha e te parregullta).
-- Nuk ndryshon madhesine aktuale te asnje skedari. I sigurt per t'u ekzekutuar disa here.
--
-- Rekomandim per serverin e klientit (jo pjese e skriptit): aktivizoni "Perform volume maintenance tasks"
-- (Instant File Initialization) per llogarine e sherbimit te SQL Server, qe rritja e data file-it te jete e menjehershme.
SET NOCOUNT ON;
DECLARE @sql nvarchar(max) = N'';
SELECT @sql += N'ALTER DATABASE CURRENT MODIFY FILE (NAME = ' + QUOTENAME(name) + N', FILEGROWTH = '
    + CASE type WHEN 0 THEN N'256MB' ELSE N'128MB' END + N');' + CHAR(10)
FROM sys.database_files
WHERE type IN (0, 1)
  AND NOT (is_percent_growth = 0 AND growth * 8 / 1024 >= CASE type WHEN 0 THEN 256 ELSE 128 END);
PRINT @sql;
EXEC sp_executesql @sql;
SELECT name, type_desc, size * 8 / 1024 AS size_mb,
       CASE WHEN is_percent_growth = 1 THEN CAST(growth AS varchar) + '%' ELSE CAST(growth * 8 / 1024 AS varchar) + ' MB' END AS growth
FROM sys.database_files;
