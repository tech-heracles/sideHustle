-- 00 - Ruajtja e gjendjes para optimizimit (duhet ekzekutuar para skripteve 01-26; apply-optimisation.ps1 e ben vete)
--
-- Cdo skript optimizimi ruan ketu, para se te ndryshoje dicka, perkufizimin aktual te cdo procedure dhe indeksi qe prek
-- (ose faktin qe nuk ekzistonte). Skripti i rikthimit (NN-rollback-...sql) rikthen pikerisht ate qe u ruajt, edhe kur
-- databaza e klientit ka versione te tjera te procedurave ose indekse te tjera nga databaza ku u pergatiten skriptet.
-- Ruhet vetem ekzekutimi i pare: nje ekzekutim i dyte i te njejtit skript nuk e mbishkruan gjendjen e ruajtur.
-- Pas rikthimit rreshtat e skriptit fshihen.
--
-- Objektet: tabela dbo.T_OPTIMIZIM_RUAJTJE, funksioni dbo.fn_OPTIMIZIM_PerkufizimIndeksi dhe procedurat
-- dbo.prc_OPTIMIZIM_Ruaj, dbo.prc_OPTIMIZIM_Rikthe, dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar.
-- Rikthimi: 00-rollback-ruajtje.sql (vetem pasi jane rikthyer te gjitha skriptet e tjera).
-- I sigurt per t'u ekzekutuar disa here.
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'dbo.T_OPTIMIZIM_RUAJTJE', N'U') IS NULL
    CREATE TABLE dbo.T_OPTIMIZIM_RUAJTJE
    (
        ID          int IDENTITY(1, 1) NOT NULL,
        SKRIPTI     varchar(10)    NOT NULL,   -- '01'...'26'
        LLOJI       char(1)        NOT NULL,   -- 'M' procedure/funksion, 'I' indeks
        OBJEKTI     nvarchar(300)  NOT NULL,   -- 'dbo.emri' ose tabela per indeksin
        INDEKSI     sysname        NOT NULL,   -- emri i indeksit ('' per procedurat)
        EKZISTONTE  bit            NOT NULL,
        PERKUFIZIMI nvarchar(max)  NULL,
        QUOTED_IDENT bit           NULL,       -- opsionet me te cilat ishte krijuar procedura
        ANSI_NULLS_  bit           NULL,
        DATA        datetime       NOT NULL CONSTRAINT DF_T_OPTIMIZIM_RUAJTJE_DATA DEFAULT (GETDATE()),
        CONSTRAINT PK_T_OPTIMIZIM_RUAJTJE PRIMARY KEY (ID),
        CONSTRAINT UQ_T_OPTIMIZIM_RUAJTJE UNIQUE (SKRIPTI, LLOJI, OBJEKTI, INDEKSI)
    );
GO
-- CREATE INDEX i plote i nje indeksi ekzistues (me opsionet dhe filegroup-in), ose NULL nese indeksi nuk ekziston ose
-- eshte i nje lloji qe skriptet nuk e prekin (columnstore, i particionuar, PRIMARY KEY / UNIQUE constraint).
-- DROP_EXISTING = OFF zevendesohet me ON nga prc_OPTIMIZIM_Rikthe kur indeksi ekziston.
IF OBJECT_ID(N'dbo.fn_OPTIMIZIM_PerkufizimIndeksi', N'FN') IS NULL
    EXEC (N'CREATE FUNCTION dbo.fn_OPTIMIZIM_PerkufizimIndeksi (@tabela nvarchar(300), @indeksi sysname) RETURNS nvarchar(max) AS BEGIN RETURN NULL; END');
GO
ALTER FUNCTION dbo.fn_OPTIMIZIM_PerkufizimIndeksi (@tabela nvarchar(300), @indeksi sysname)
RETURNS nvarchar(max)
AS
BEGIN
    DECLARE @oid int = OBJECT_ID(@tabela), @iid int, @celesat nvarchar(max), @perfshira nvarchar(max), @s nvarchar(max);
    SELECT @iid = i.index_id
    FROM sys.indexes i JOIN sys.data_spaces ds ON ds.data_space_id = i.data_space_id
    WHERE i.object_id = @oid AND i.name = @indeksi AND i.type IN (1, 2) AND i.is_primary_key = 0
          AND i.is_unique_constraint = 0 AND ds.type = 'FG';
    IF @iid IS NULL
        RETURN NULL;

    SET @celesat = STUFF((SELECT N', ' + QUOTENAME(c.name) + CASE WHEN ic.is_descending_key = 1 THEN N' DESC' ELSE N' ASC' END
                          FROM sys.index_columns ic
                               JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
                          WHERE ic.object_id = @oid AND ic.index_id = @iid AND ic.key_ordinal > 0
                          ORDER BY ic.key_ordinal
                          FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N'');
    SET @perfshira = STUFF((SELECT N', ' + QUOTENAME(c.name)
                            FROM sys.index_columns ic
                                 JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
                            WHERE ic.object_id = @oid AND ic.index_id = @iid AND ic.is_included_column = 1
                            ORDER BY ic.index_column_id
                            FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N'');

    SELECT @s = N'CREATE ' + CASE WHEN i.is_unique = 1 THEN N'UNIQUE ' ELSE N'' END
              + CASE i.type WHEN 1 THEN N'CLUSTERED' ELSE N'NONCLUSTERED' END + N' INDEX ' + QUOTENAME(i.name)
              + N' ON ' + QUOTENAME(OBJECT_SCHEMA_NAME(@oid)) + N'.' + QUOTENAME(OBJECT_NAME(@oid)) + N' (' + @celesat + N')'
              + ISNULL(N' INCLUDE (' + @perfshira + N')', N'')
              + ISNULL(N' WHERE ' + i.filter_definition, N'')
              + N' WITH (DROP_EXISTING = OFF'
              + N', PAD_INDEX = ' + CASE WHEN i.is_padded = 1 THEN N'ON' ELSE N'OFF' END
              + CASE WHEN i.fill_factor > 0 THEN N', FILLFACTOR = ' + CONVERT(nvarchar(3), i.fill_factor) ELSE N'' END
              + N', IGNORE_DUP_KEY = ' + CASE WHEN i.ignore_dup_key = 1 THEN N'ON' ELSE N'OFF' END
              + N', ALLOW_ROW_LOCKS = ' + CASE WHEN i.allow_row_locks = 1 THEN N'ON' ELSE N'OFF' END
              + N', ALLOW_PAGE_LOCKS = ' + CASE WHEN i.allow_page_locks = 1 THEN N'ON' ELSE N'OFF' END
              + ISNULL((SELECT TOP (1) N', DATA_COMPRESSION = ' + p.data_compression_desc COLLATE DATABASE_DEFAULT
                        FROM sys.partitions p
                        WHERE p.object_id = @oid AND p.index_id = @iid AND p.data_compression <> 0), N'')
              + N') ON ' + QUOTENAME(ds.name)
    FROM sys.indexes i JOIN sys.data_spaces ds ON ds.data_space_id = i.data_space_id
    WHERE i.object_id = @oid AND i.index_id = @iid;
    RETURN @s;
END
GO
-- Ruan gjendjen aktuale te nje procedure/funksioni (@indeksi = NULL) ose te nje indeksi, nese skripti nuk e ka ruajtur me pare.
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Ruaj', N'P') IS NULL
    EXEC (N'CREATE PROCEDURE dbo.prc_OPTIMIZIM_Ruaj AS RETURN 0;');
GO
ALTER PROCEDURE dbo.prc_OPTIMIZIM_Ruaj
    @skripti varchar(10),
    @objekti nvarchar(300),
    @indeksi sysname = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @lloji char(1) = CASE WHEN @indeksi IS NULL THEN 'M' ELSE 'I' END, @def nvarchar(max), @ekziston bit,
            @qi bit, @an bit;
    IF EXISTS (SELECT 1 FROM dbo.T_OPTIMIZIM_RUAJTJE
               WHERE SKRIPTI = @skripti AND LLOJI = @lloji AND OBJEKTI = @objekti AND INDEKSI = ISNULL(@indeksi, N''))
        RETURN 0;

    IF @lloji = 'M'
    BEGIN
        SET @ekziston = CASE WHEN OBJECT_ID(@objekti) IS NULL THEN 0 ELSE 1 END;
        SELECT @def = definition, @qi = uses_quoted_identifier, @an = uses_ansi_nulls
        FROM sys.sql_modules WHERE object_id = OBJECT_ID(@objekti);
    END
    ELSE
    BEGIN
        SET @ekziston = CASE WHEN EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(@objekti) AND name = @indeksi)
                             THEN 1 ELSE 0 END;
        SET @def = dbo.fn_OPTIMIZIM_PerkufizimIndeksi(@objekti, @indeksi);
    END
    IF @ekziston = 1 AND @def IS NULL
    BEGIN
        RAISERROR (N'%s %s: perkufizimi nuk mund te ruhet (i enkriptuar ose lloj indeksi i pambeshtetur). Asgje nuk u ndryshua.',
                   16, 1, @objekti, @indeksi);
        RETURN 1;
    END
    INSERT INTO dbo.T_OPTIMIZIM_RUAJTJE (SKRIPTI, LLOJI, OBJEKTI, INDEKSI, EKZISTONTE, PERKUFIZIMI, QUOTED_IDENT, ANSI_NULLS_)
    VALUES (@skripti, @lloji, @objekti, ISNULL(@indeksi, N''), @ekziston, @def, @qi, @an);
    RETURN 0;
END
GO
-- Rikthen gjithcka qe ka ruajtur nje skript (ne rradhe te kundert) dhe fshin rreshtat e tij.
-- @uRikthye = 1 kur kishte dicka te ruajtur; perndryshe skripti i rikthimit vazhdon me rikthimin e tij te vjeter.
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_Rikthe', N'P') IS NULL
    EXEC (N'CREATE PROCEDURE dbo.prc_OPTIMIZIM_Rikthe AS RETURN 0;');
GO
ALTER PROCEDURE dbo.prc_OPTIMIZIM_Rikthe
    @skripti varchar(10),
    @uRikthye bit = 0 OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @uRikthye = 0;
    IF NOT EXISTS (SELECT 1 FROM dbo.T_OPTIMIZIM_RUAJTJE WHERE SKRIPTI = @skripti)
        RETURN 0;

    DECLARE @id int, @lloji char(1), @objekti nvarchar(300), @indeksi sysname, @ekzistonte bit, @def nvarchar(max),
            @sql nvarchar(max), @i int, @n int, @tipi char(2), @qi bit, @an bit, @mbeshtjellese nvarchar(200);
    DECLARE c CURSOR LOCAL FAST_FORWARD FOR
        SELECT ID, LLOJI, OBJEKTI, INDEKSI, EKZISTONTE, PERKUFIZIMI, QUOTED_IDENT, ANSI_NULLS_
        FROM dbo.T_OPTIMIZIM_RUAJTJE WHERE SKRIPTI = @skripti ORDER BY ID DESC;

    BEGIN TRANSACTION;
    OPEN c;
    FETCH NEXT FROM c INTO @id, @lloji, @objekti, @indeksi, @ekzistonte, @def, @qi, @an;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF @lloji = 'I'
        BEGIN
            IF @ekzistonte = 0
            BEGIN
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(@objekti) AND name = @indeksi)
                BEGIN
                    SET @sql = N'DROP INDEX ' + QUOTENAME(@indeksi) + N' ON ' + @objekti;
                    EXEC (@sql);
                    PRINT @skripti + N': u hoq indeksi ' + @objekti + N'.' + @indeksi + N' (nuk ekzistonte me pare).';
                END
            END
            ELSE
            BEGIN
                SET @sql = @def;
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(@objekti) AND name = @indeksi)
                BEGIN
                    IF dbo.fn_OPTIMIZIM_PerkufizimIndeksi(@objekti, @indeksi) = @def
                        SET @sql = NULL;   -- eshte tashme si me pare
                    ELSE
                        SET @sql = REPLACE(@def, N'WITH (DROP_EXISTING = OFF', N'WITH (DROP_EXISTING = ON');
                END
                IF @sql IS NOT NULL
                BEGIN
                    EXEC (@sql);
                    PRINT @skripti + N': u rikthye indeksi ' + @objekti + N'.' + @indeksi + N'.';
                END
            END
        END
        ELSE
        BEGIN
            SET @tipi = (SELECT type FROM sys.objects WHERE object_id = OBJECT_ID(@objekti));
            -- QUOTED_IDENTIFIER dhe ANSI_NULLS lexohen kur analizohet teksti, prandaj vendosen ne nje batch te jashtem
            SET @mbeshtjellese = N'SET QUOTED_IDENTIFIER ' + CASE WHEN ISNULL(@qi, 1) = 1 THEN N'ON' ELSE N'OFF' END
                               + N'; SET ANSI_NULLS ' + CASE WHEN ISNULL(@an, 1) = 1 THEN N'ON' ELSE N'OFF' END + N'; EXEC (@s);';
            IF @ekzistonte = 0
            BEGIN
                IF @tipi IS NOT NULL
                BEGIN
                    SET @sql = CASE WHEN @tipi = 'P' THEN N'DROP PROCEDURE ' ELSE N'DROP FUNCTION ' END + @objekti;
                    EXEC (@sql);
                    PRINT @skripti + N': u hoq ' + @objekti + N' (nuk ekzistonte me pare).';
                END
            END
            ELSE IF @tipi IS NULL
            BEGIN
                EXEC sp_executesql @mbeshtjellese, N'@s nvarchar(max)', @s = @def;
                PRINT @skripti + N': u krijua perseri ' + @objekti + N'.';
            END
            ELSE IF ISNULL(OBJECT_DEFINITION(OBJECT_ID(@objekti)), N'') <> @def
                    OR OBJECTPROPERTY(OBJECT_ID(@objekti), 'ExecIsQuotedIdentOn') <> ISNULL(@qi, 1)
                    OR OBJECTPROPERTY(OBJECT_ID(@objekti), 'ExecIsAnsiNullsOn') <> ISNULL(@an, 1)
            BEGIN
                -- CREATE -> ALTER (ruhen te drejtat dhe object_id): kapercehen hapesirat dhe komentet ne fillim
                SET @i = 1; SET @n = LEN(@def + N'x') - 1;
                WHILE @i <= @n
                BEGIN
                    IF SUBSTRING(@def, @i, 1) IN (N' ', NCHAR(9), NCHAR(10), NCHAR(13))
                        SET @i += 1;
                    ELSE IF SUBSTRING(@def, @i, 2) = N'--'
                        SET @i = CASE WHEN CHARINDEX(NCHAR(10), @def, @i) = 0 THEN @n + 1 ELSE CHARINDEX(NCHAR(10), @def, @i) + 1 END;
                    ELSE IF SUBSTRING(@def, @i, 2) = N'/*'
                        SET @i = CASE WHEN CHARINDEX(N'*/', @def, @i + 2) = 0 THEN @n + 1 ELSE CHARINDEX(N'*/', @def, @i + 2) + 2 END;
                    ELSE
                        BREAK;
                END
                IF UPPER(SUBSTRING(@def, @i, 6)) <> N'CREATE'
                BEGIN
                    ROLLBACK TRANSACTION;
                    RAISERROR (N'%s: fillimi i perkufizimit te ruajtur nuk u njoh; asgje nuk u rikthye.', 16, 1, @objekti);
                    RETURN 1;
                END
                SET @sql = STUFF(@def, @i, 6, N'ALTER');
                EXEC sp_executesql @mbeshtjellese, N'@s nvarchar(max)', @s = @sql;
                PRINT @skripti + N': u rikthye ' + @objekti + N'.';
            END
        END
        FETCH NEXT FROM c INTO @id, @lloji, @objekti, @indeksi, @ekzistonte, @def, @qi, @an;
    END
    CLOSE c;
    DEALLOCATE c;
    DELETE FROM dbo.T_OPTIMIZIM_RUAJTJE WHERE SKRIPTI = @skripti;
    COMMIT TRANSACTION;
    SET @uRikthye = 1;
    RETURN 0;
END
GO
-- Heq nje indeks jo-unik pa filter vetem nese nje indeks tjeter ekzistues e mbulon: celesat e tij jane fillimi i celesave
-- te tjetrit (ne te njejten rradhe dhe drejtim) dhe kolonat e perfshira jane ne tjetrin (ose tjetri eshte clustered).
-- Ruan perkufizimin para heqjes. Nese nuk ekziston ose nuk mbulohet, lihet sic eshte.
IF OBJECT_ID(N'dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar', N'P') IS NULL
    EXEC (N'CREATE PROCEDURE dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar AS RETURN 0;');
GO
ALTER PROCEDURE dbo.prc_OPTIMIZIM_HiqIndeksTeMbuluar
    @skripti varchar(10),
    @tabela nvarchar(300),
    @indeksi sysname
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @oid int = OBJECT_ID(@tabela), @iid int, @mbulues sysname, @sql nvarchar(max);
    SELECT @iid = index_id FROM sys.indexes
    WHERE object_id = @oid AND name = @indeksi AND type = 2 AND is_unique = 0 AND is_primary_key = 0
          AND is_unique_constraint = 0 AND has_filter = 0 AND is_hypothetical = 0;
    IF @iid IS NULL
    BEGIN
        IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = @oid AND name = @indeksi)
            PRINT @skripti + N': ' + @tabela + N'.' + @indeksi + N' u la (unik, me filter ose i nje lloji tjeter).';
        RETURN 0;
    END

    SELECT TOP (1) @mbulues = y.name
    FROM sys.indexes y
    WHERE y.object_id = @oid AND y.index_id <> @iid AND y.type IN (1, 2) AND y.has_filter = 0
          AND y.is_disabled = 0 AND y.is_hypothetical = 0
          -- cdo celes i indeksit ne te njejtin pozicion dhe drejtim te y
          AND NOT EXISTS (SELECT 1 FROM sys.index_columns x
                          WHERE x.object_id = @oid AND x.index_id = @iid AND x.key_ordinal > 0
                                AND NOT EXISTS (SELECT 1 FROM sys.index_columns k
                                                WHERE k.object_id = @oid AND k.index_id = y.index_id
                                                      AND k.key_ordinal = x.key_ordinal AND k.column_id = x.column_id
                                                      AND k.is_descending_key = x.is_descending_key))
          -- cdo kolone e perfshire gjendet ne y (ose y eshte clustered dhe i ka te gjitha)
          AND (y.type = 1 OR NOT EXISTS (SELECT 1 FROM sys.index_columns x
                                         WHERE x.object_id = @oid AND x.index_id = @iid AND x.is_included_column = 1
                                               AND NOT EXISTS (SELECT 1 FROM sys.index_columns k
                                                               WHERE k.object_id = @oid AND k.index_id = y.index_id
                                                                     AND k.column_id = x.column_id)))
    ORDER BY y.type, y.index_id;

    IF @mbulues IS NULL
    BEGIN
        PRINT @skripti + N': ' + @tabela + N'.' + @indeksi + N' u la: asnje indeks tjeter nuk e mbulon ne kete databaze.';
        RETURN 0;
    END
    EXEC dbo.prc_OPTIMIZIM_Ruaj @skripti, @tabela, @indeksi;
    SET @sql = N'DROP INDEX ' + QUOTENAME(@indeksi) + N' ON ' + @tabela;
    EXEC (@sql);
    PRINT @skripti + N': u hoq ' + @tabela + N'.' + @indeksi + N' (e mbulon ' + @mbulues + N').';
    RETURN 0;
END
GO
