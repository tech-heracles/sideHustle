if EXISTS(select * from T_KONTROLLE where KODKONTROLL = 'cbIRimbursueshem')
BEGIN
	UPDATE T_KONTROLLE
	set EMERIMPORTI = 'I rimbursueshem'
	where KODKONTROLL = 'cbIRimbursueshem'
END
