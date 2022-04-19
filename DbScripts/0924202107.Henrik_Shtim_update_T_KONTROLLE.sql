if EXISTS(select * from T_KONTROLLE where KODKONTROLL = 'cmbTipiId')
BEGIN
	UPDATE T_KONTROLLE
	set EMERIMPORTI = 'Tipi Id'
	where KODKONTROLL = 'cmbTipiId'
END
