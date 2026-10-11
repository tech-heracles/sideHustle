-- Heq prc_T_ARTIKULLI_ruajImport (gjendja para 22-artikuj-import-grup.sql).
-- Aplikacioni e kontrollon vete nese procedura ekziston dhe perndryshe i ruan artikujt nje nga nje si me pare.
IF OBJECT_ID(N'dbo.prc_T_ARTIKULLI_ruajImport', N'P') IS NOT NULL
    DROP PROCEDURE dbo.prc_T_ARTIKULLI_ruajImport;
GO
