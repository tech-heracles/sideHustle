IF OBJECT_ID('PRC_T_FATURAEINVOICE_KRIJOTABELEINVOICE') IS NOT NULL
	DROP PROCEDURE PRC_T_FATURAEINVOICE_KRIJOTABELEINVOICE
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[PRC_T_FATURAEINVOICE_KRIJOTABELEINVOICE](
@XML varchar(max)
)
AS

SET QUOTED_IDENTIFIER off

if EXISTS(
       SELECT * FROM information_schema.tables 
       WHERE table_name = 'T_FATURAEINVOICE'
)
DROP TABLE T_FATURAEINVOICE
CREATE  table T_FATURAEINVOICE(Amount varchar(100) null,
DocNumber varchar(100) null,
DocType varchar(100) null,
DueDateTime T_DATE  null,
EIC varchar(100)  null,
PartyType varchar(10)  null,
RecDateTime T_DATE  null,
Status varchar(20)  null)declare @handle intexec sp_xml_preparedocument @handle out, @XMLinsert into T_FATURAEINVOICEselect Amount,DocNumber,DocType,DueDateTime,EIC,PartyType,RecDateTime,[Status] from openxml(@handle, '/Einvoices/Einvoice', 1) with (Amount varchar(100),
DocNumber varchar(100),
DocType varchar(100),
DueDateTime T_DATE,
EIC varchar(100),
PartyType varchar(100),
RecDateTime T_DATE,
Status varchar(100))