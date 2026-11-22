/*
  DRAFT: captions of the default dashboard (T_DASHBOARD.DASHBOARDXML) in Albanian. Only display names change
  (Name="..." of items, measures and series); data members and data sources stay the same.
  The old XML is kept in T_DASHBOARD_BACKUP_20261122. Rollback:
      UPDATE d SET DASHBOARDXML = b.DASHBOARDXML FROM T_DASHBOARD d JOIN T_DASHBOARD_BACKUP_20261122 b ON b.IDDASHBOARD = d.IDDASHBOARD;
  Run: sqlcmd -S localhost\HERACLES -d DRAFT -E -C -b -f 65001 -i 04-dashboard-shqip.sql
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID('dbo.T_DASHBOARD_BACKUP_20261122') IS NULL
    SELECT IDDASHBOARD, DASHBOARDXML INTO dbo.T_DASHBOARD_BACKUP_20261122 FROM T_DASHBOARD;

DECLARE @x nvarchar(max);
DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT IDDASHBOARD FROM T_DASHBOARD;
DECLARE @id int;
OPEN c;
FETCH NEXT FROM c INTO @id;
WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @x = CAST(DASHBOARDXML AS nvarchar(max)) FROM T_DASHBOARD WHERE IDDASHBOARD = @id;
    SET @x = REPLACE(@x, N'Name="Monthly Sales"', N'Name="Shitjet mujore"');
    SET @x = REPLACE(@x, N'Name="Sales per month"', N'Name="Shitjet sipas muajve"');
    SET @x = REPLACE(@x, N'Name="Daily Cash Entries"', N'Name="Arkëtimet ditore"');
    SET @x = REPLACE(@x, N'Name="Top Monthly Customer"', N'Name="Klientët kryesorë të muajit"');
    SET @x = REPLACE(@x, N'Name="Monthly Net Sales"', N'Name="Shitje neto mujore"');
    SET @x = REPLACE(@x, N'Name="Net Sale Amount BC"', N'Name="Shitje neto (monedha bazë)"');
    SET @x = REPLACE(@x, N'Name="Net sale amount BC"', N'Name="Shitje neto (monedha bazë)"');
    SET @x = REPLACE(@x, N'Name="Sale net amount BC"', N'Name="Shitje neto (monedha bazë)"');
    SET @x = REPLACE(@x, N'Name="Cash Amount"', N'Name="Vlera e arkëtimit"');
    SET @x = REPLACE(@x, N'Name="Month"', N'Name="Muaji"');
    SET @x = REPLACE(@x, N'Text="Dashboard"', N'Text="Treguesit kryesorë"');
    UPDATE T_DASHBOARD SET DASHBOARDXML = CAST(@x AS xml) WHERE IDDASHBOARD = @id;
    FETCH NEXT FROM c INTO @id;
END
CLOSE c;
DEALLOCATE c;
