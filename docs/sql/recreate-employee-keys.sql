/*
  Makes "Burime Njerezore" (employees / payroll) work without IMB's master key password.

  Why: employee data is encrypted with the symmetric key Employee_key -> certificate EncryptEmp -> database
  master key. The master key was encrypted by IMB's old server, so this server cannot open it and every payroll
  procedure fails at OPEN SYMMETRIC KEY.

  What this does: replaces the three keys with new ones under the SAME names, so the ~200 payroll procedures
  keep working unchanged. The new master key is also encrypted by this server's service master key, so nothing
  needs to be opened manually afterwards.

  Side effect: the 28 existing employee records (all in IMB demo/trial companies: Demo IMB, Pasticeri Tirana,
  Ndermarrje demo, Instituti i modelimeve ne biznes, ...) can no longer be decrypted and show empty fields.
  A full backup taken before this change keeps them recoverable if IMB ever supplies the old password:
      C:\Program Files\Microsoft SQL Server\MSSQL17.HERACLES\MSSQL\Backup\AVEC_before_new_employee_keys_2026-09-25.bak

  How to run: replace BOTH occurrences of <choose-a-strong-password> below, keep that password in a password
  manager, then run in SSMS against localhost\HERACLES, or:
      sqlcmd -S localhost\HERACLES -d AVEC -E -C -b -i recreate-employee-keys.sql
*/
USE AVEC;
SET XACT_ABORT ON;

DROP SYMMETRIC KEY Employee_key;
DROP CERTIFICATE EncryptEmp;
DROP MASTER KEY;

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<choose-a-strong-password>';
CREATE CERTIFICATE EncryptEmp WITH SUBJECT = 'Employee', EXPIRY_DATE = '2099-12-31';
CREATE SYMMETRIC KEY Employee_key WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE EncryptEmp;

-- Key backups: needed to move the database to another server (restore, then
-- OPEN MASTER KEY DECRYPTION BY PASSWORD = '...'; ALTER MASTER KEY ADD ENCRYPTION BY SERVICE MASTER KEY;).
BACKUP MASTER KEY TO FILE = 'C:\Program Files\Microsoft SQL Server\MSSQL17.HERACLES\MSSQL\Backup\AVEC_master_key.key'
    ENCRYPTION BY PASSWORD = '<choose-a-strong-password>';
GO

-- Check: must print "ok".
OPEN SYMMETRIC KEY Employee_key DECRYPTION BY CERTIFICATE EncryptEmp;
SELECT CONVERT(varchar(10), DecryptByKey(EncryptByKey(Key_GUID('Employee_key'), 'ok', 1, HashBytes('SHA1', 0x01)),
                                         1, HashBytes('SHA1', 0x01))) AS result;
CLOSE SYMMETRIC KEY Employee_key;
