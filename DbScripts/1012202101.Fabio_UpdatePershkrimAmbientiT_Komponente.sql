IF EXISTS ( 
   SELECT * FROM T_KOMPONENTE WHERE KOMPPERSHKRIMI_sq = 'Rregjistime Klient/Furnitor') 
BEGIN 
UPDATE T_KOMPONENTE SET KOMPPERSHKRIMI_sq = 'Regjistrime Klient/Furnitor' WHERE KOMPPERSHKRIMI_sq = 'Rregjistime Klient/Furnitor'
END