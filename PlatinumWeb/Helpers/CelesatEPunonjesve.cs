using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb.Helpers
{
	/// <summary>
	/// Celesat e enkriptimit te punonjesve (master key -> certifikata EncryptEmp -> Employee_key).
	/// Nje databaze e bere nga DRAFT dhe e rikthyer ne nje server tjeter ka master key qe ky server s'e hap vete,
	/// keshtu Punonjes, Vendndodhje dhe listepagesat bien. Ne nisje te aplikacionit, per cdo databaze te licencave,
	/// nese Employee_key nuk hapet, master key hapet nje here me fjalekalimin (AvecDbMasterKeyPassword ne
	/// avecLicense.config) dhe i shtohet enkriptimi me service master key te ketij serveri. Pastaj hapet vete.
	/// </summary>
	public static class CelesatEPunonjesve
	{
		private const string KontrolloSql =
			"IF NOT EXISTS (SELECT 1 FROM sys.symmetric_keys WHERE name = 'Employee_key') SELECT 1 ELSE " +
			"BEGIN BEGIN TRY OPEN SYMMETRIC KEY Employee_key DECRYPTION BY CERTIFICATE EncryptEmp; " +
			"CLOSE SYMMETRIC KEY Employee_key; SELECT 1; END TRY BEGIN CATCH SELECT 0; END CATCH END";

		public static void RregulloNeSfond(IEnumerable<string> lidhjet)
		{
			var fjalekalimi = ConfigurationManager.AppSettings["AvecDbMasterKeyPassword"];
			if (string.IsNullOrEmpty(fjalekalimi) || lidhjet == null)
				return;
			var lista = new List<string>(lidhjet);
			Task.Run(() =>
			{
				foreach (var lidhja in lista)
				{
					try { Rregullo(lidhja, fjalekalimi); }
					catch (Exception ex) { ImbLogger.Error(ex, "Celesat e punonjesve nuk u rregulluan"); }
				}
			});
		}

		private static void Rregullo(string lidhja, string fjalekalimi)
		{
			if (string.IsNullOrEmpty(lidhja))
				return;
			using (var conn = new SqlConnection(lidhja))
			{
				conn.Open();
				using (var cmd = new SqlCommand(KontrolloSql, conn))
				{
					if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
						return; // nuk ka celes ose hapet vete
				}
				// DDL nuk pranon parametra: fjalekalimi futet si literal i escape-uar
				var sql = "OPEN MASTER KEY DECRYPTION BY PASSWORD = N'" + fjalekalimi.Replace("'", "''") + "'; " +
						  "ALTER MASTER KEY ADD ENCRYPTION BY SERVICE MASTER KEY; CLOSE MASTER KEY;";
				using (var cmd = new SqlCommand(sql, conn))
					cmd.ExecuteNonQuery();
			}
		}
	}
}
