-- Rollback of 18-favoritet-e-kryefaqes.sql (the users' stars are lost).
DROP PROCEDURE IF EXISTS dbo.prc_T_PERDORUES_FAVORITE_ndrysho;
DROP PROCEDURE IF EXISTS dbo.prc_T_PERDORUES_FAVORITE_sel;
DROP TABLE IF EXISTS dbo.T_PERDORUES_FAVORITE;
