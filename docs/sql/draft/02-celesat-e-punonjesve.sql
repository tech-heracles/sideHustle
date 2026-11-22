/*
  DRAFT: new encryption keys for employee data (Burime Njerezore: Punonjes, Vendndodhje, payroll).

  Why: DRAFT was copied from a database whose master key belonged to IMB's old server, so every page that
  opens Employee_key failed with "Please create a master key in the database or open the master key".
  DRAFT has no employees, so the keys are simply recreated under the same names (the ~190 procedures that
  use them stay unchanged).

  The master key is protected twice:
    - by this server's service master key, so SQL Server opens it by itself (nobody runs OPEN MASTER KEY);
    - by a password, kept in the git-ignored avecLicense.config as AvecDbMasterKeyPassword. When a database made
      from DRAFT is restored on another server, the program uses that password once at start-up to add that
      server's service master key (Global.asax -> CelesatEPunonjesve.Rregullo), so nothing is run by hand there either.

  Run (sqlcmd variable, the password never goes into this file):
      sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -v MK_PASSWORD="..." -i 02-celesat-e-punonjesve.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF EXISTS (SELECT 1 FROM T_PUNONJES)
BEGIN
    RAISERROR('DRAFT has employees: their encrypted fields would be lost. Stopped.', 16, 1);
    RETURN;
END

IF EXISTS (SELECT 1 FROM sys.symmetric_keys WHERE name = 'Employee_key') DROP SYMMETRIC KEY Employee_key;
IF EXISTS (SELECT 1 FROM sys.certificates WHERE name = 'EncryptEmp') DROP CERTIFICATE EncryptEmp;
IF EXISTS (SELECT 1 FROM sys.symmetric_keys WHERE name = '##MS_DatabaseMasterKey##') DROP MASTER KEY;

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '$(MK_PASSWORD)';   -- also encrypted by this server's service master key
CREATE CERTIFICATE EncryptEmp WITH SUBJECT = 'Employee', EXPIRY_DATE = '2099-12-31';
CREATE SYMMETRIC KEY Employee_key WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE EncryptEmp;
GO

-- Check: must print "ok".
OPEN SYMMETRIC KEY Employee_key DECRYPTION BY CERTIFICATE EncryptEmp;
SELECT CONVERT(varchar(10), DecryptByKey(EncryptByKey(Key_GUID('Employee_key'), 'ok'))) AS result;
CLOSE SYMMETRIC KEY Employee_key;
GO
