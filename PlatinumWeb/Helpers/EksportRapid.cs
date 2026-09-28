using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using DevExpress.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PlatinumWeb
{
	/// <summary>
	/// Eksport i shpejte i grides se Eksport.aspx per CSV dhe XLSX: shkruan rreshtat e zgjedhur direkt nga
	/// DataTable-i (qe gridi e mban ne sesion) ne skedar, rresht pas rreshti.
	///
	/// ASPxGridViewExporter-i ndertonte te gjithe dokumentin ne memorie dhe per nje vit shitjesh
	/// (~550 mije rreshta) nuk mbaronte as pas 30 minutash. Ketu memoria mbetet e vogel dhe koha lineare.
	///
	/// Perdoret vetem kur rezultati eshte i njejte me ate te exporter-it: pa filter, renditje apo grupim ne gride
	/// (ne ato raste rreshtat/rradha i vendos gridi). Perndryshe kthen false dhe perdoret exporter-i si me pare.
	/// Vlerat shkruhen si tekst (si TextExportMode.Text i exporter-it), kolonat jane ato te dukshme te grides,
	/// me titullin dhe rradhen e tyre.
	/// </summary>
	public static class EksportRapid
	{
		/// <summary>Nen kete numer rreshtash te zgjedhur exporter-i i DevExpress eshte mjaft i shpejte dhe perdoret si me pare.</summary>
		public const int PragRreshtash = 20000;

		public static bool Provo(ASPxGridView grid, DataTable table, string tipi, string emerSkedari, string emerSheet, HttpResponse response)
		{
			if (table == null || table.Rows.Count < PragRreshtash || grid.Selection.Count < PragRreshtash)
				return false;
			if (!string.IsNullOrWhiteSpace(grid.FilterExpression) || grid.SortCount > 0 || grid.GroupCount > 0)
				return false;
			if (tipi != "CSV" && tipi != "XLSX")
				return false;

			var kolonat = grid.VisibleColumns.OfType<GridViewDataColumn>()
				.Where(c => !string.IsNullOrEmpty(c.FieldName) && table.Columns.Contains(c.FieldName))
				.OrderBy(c => c.VisibleIndex)
				.Select(c => new { Indeksi = table.Columns.IndexOf(c.FieldName), Titulli = string.IsNullOrEmpty(c.Caption) ? c.FieldName : c.Caption })
				.ToList();
			if (kolonat.Count == 0)
				return false;

			var rreshtat = RreshtatEZgjedhur(grid, table);
			if (rreshtat.Count == 0)
				return false;

			string skedar = Path.Combine(Path.GetTempPath(), "avec-eksport-" + Guid.NewGuid().ToString("N"));
			try
			{
				var indekset = kolonat.Select(k => k.Indeksi).ToArray();
				var titujt = kolonat.Select(k => k.Titulli).ToArray();
				if (tipi == "CSV")
					ShkruajCsv(skedar, table, rreshtat, indekset, titujt);
				else
					ShkruajXlsx(skedar, table, rreshtat, indekset, titujt, string.IsNullOrWhiteSpace(emerSheet) ? "Sheet1" : emerSheet);

				string prapashtesa = tipi == "CSV" ? ".csv" : ".xlsx";
				response.Clear();
				response.ContentType = tipi == "CSV" ? "text/csv" : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				response.AddHeader("Content-Disposition", "attachment; filename=\"" + (emerSkedari ?? "eksport").Replace("\"", "") + prapashtesa + "\"");
				response.BufferOutput = false;
				using (var fs = File.OpenRead(skedar))
				{
					response.AddHeader("Content-Length", fs.Length.ToString(CultureInfo.InvariantCulture));
					fs.CopyTo(response.OutputStream, 1 << 16);
				}
				response.Flush();
				return true;
			}
			finally
			{
				try { File.Delete(skedar); } catch { /* skedari i perkohshem hiqet ne rastin tjeter */ }
			}
		}

		/// <summary>
		/// Rreshtat e zgjedhur ne rradhen e DataTable-it. Kur jane zgjedhur te gjithe (butoni "Zgjidh te gjitha"),
		/// nuk kontrollohet celes per celes.
		/// </summary>
		private static List<DataRow> RreshtatEZgjedhur(ASPxGridView grid, DataTable table)
		{
			int zgjedhur = grid.Selection.Count;
			if (zgjedhur >= table.Rows.Count)
				return table.Rows.Cast<DataRow>().ToList();
			if (zgjedhur == 0 || string.IsNullOrEmpty(grid.KeyFieldName) || !table.Columns.Contains(grid.KeyFieldName))
				return new List<DataRow>();

			int celesi = table.Columns.IndexOf(grid.KeyFieldName);
			var rezultati = new List<DataRow>(zgjedhur);
			foreach (DataRow r in table.Rows)
				if (grid.Selection.IsRowSelectedByKey(r[celesi]))
					rezultati.Add(r);
			return rezultati;
		}

		private static string Teksti(object vlera)
		{
			if (vlera == null || vlera == DBNull.Value)
				return "";
			return Convert.ToString(vlera, CultureInfo.CurrentCulture);
		}

		private static void ShkruajCsv(string skedar, DataTable table, List<DataRow> rreshtat, int[] indekset, string[] titujt)
		{
			string ndares = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
			using (var w = new StreamWriter(skedar, false, new UTF8Encoding(true), 1 << 16))
			{
				w.WriteLine(string.Join(ndares, titujt.Select(t => CsvFushe(t, ndares))));
				var rresht = new string[indekset.Length];
				foreach (var r in rreshtat)
				{
					for (int i = 0; i < indekset.Length; i++)
						rresht[i] = CsvFushe(Teksti(r[indekset[i]]), ndares);
					w.WriteLine(string.Join(ndares, rresht));
				}
			}
		}

		private static string CsvFushe(string s, string ndares)
		{
			if (s.IndexOf('"') >= 0 || s.Contains(ndares) || s.IndexOf('\n') >= 0 || s.IndexOf('\r') >= 0)
				return "\"" + s.Replace("\"", "\"\"") + "\"";
			return s;
		}

		private static void ShkruajXlsx(string skedar, DataTable table, List<DataRow> rreshtat, int[] indekset, string[] titujt, string emerSheet)
		{
			using (var doc = SpreadsheetDocument.Create(skedar, SpreadsheetDocumentType.Workbook))
			{
				var wbPart = doc.AddWorkbookPart();
				wbPart.Workbook = new Workbook();

				// stili 1: tekst (@) si o_customizeCell; stili 2: tekst i trashe per titujt
				var stilet = wbPart.AddNewPart<WorkbookStylesPart>();
				stilet.Stylesheet = new Stylesheet(
					new Fonts(new DocumentFormat.OpenXml.Spreadsheet.Font(), new DocumentFormat.OpenXml.Spreadsheet.Font(new Bold())) { Count = 2 },
					new Fills(new Fill(new PatternFill { PatternType = PatternValues.None }), new Fill(new PatternFill { PatternType = PatternValues.Gray125 })) { Count = 2 },
					new Borders(new DocumentFormat.OpenXml.Spreadsheet.Border()) { Count = 1 },
					new CellFormats(
						new CellFormat(),
						new CellFormat { NumberFormatId = 49, ApplyNumberFormat = true },
						new CellFormat { NumberFormatId = 49, FontId = 1, ApplyNumberFormat = true, ApplyFont = true }) { Count = 3 });
				stilet.Stylesheet.Save();

				var wsPart = wbPart.AddNewPart<WorksheetPart>();
				using (var w = OpenXmlWriter.Create(wsPart))
				{
					w.WriteStartElement(new Worksheet());
					w.WriteStartElement(new SheetData());
					ShkruajRresht(w, titujt, 2);
					var vlerat = new string[indekset.Length];
					foreach (var r in rreshtat)
					{
						for (int i = 0; i < indekset.Length; i++)
							vlerat[i] = PaKaraktereKontrolli(Teksti(r[indekset[i]]));
						ShkruajRresht(w, vlerat, 1);
					}
					w.WriteEndElement();
					w.WriteEndElement();
				}

				var emri = emerSheet.Length > 31 ? emerSheet.Substring(0, 31) : emerSheet;
				foreach (var c in new[] { '\\', '/', '?', '*', '[', ']', ':' })
					emri = emri.Replace(c, ' ');
				wbPart.Workbook.AppendChild(new Sheets(new Sheet { Id = wbPart.GetIdOfPart(wsPart), SheetId = 1, Name = emri }));
				wbPart.Workbook.Save();
			}
		}

		/// <summary>XML nuk lejon karakteret e kontrollit (pervec tab/rresht i ri); ato do prishnin skedarin.</summary>
		private static string PaKaraktereKontrolli(string s)
		{
			for (int i = 0; i < s.Length; i++)
				if (s[i] < 0x20 && s[i] != '\t' && s[i] != '\n' && s[i] != '\r')
					return new string(s.Where(c => c >= 0x20 || c == '\t' || c == '\n' || c == '\r').ToArray());
			return s;
		}

		private static void ShkruajRresht(OpenXmlWriter w, string[] vlerat, uint stili)
		{
			w.WriteStartElement(new Row());
			var atributet = new List<OpenXmlAttribute> { new OpenXmlAttribute("t", null, "inlineStr"), new OpenXmlAttribute("s", null, stili.ToString(CultureInfo.InvariantCulture)) };
			foreach (var v in vlerat)
			{
				w.WriteStartElement(new Cell(), atributet);
				w.WriteStartElement(new InlineString());
				w.WriteElement(new Text(v) { Space = SpaceProcessingModeValues.Preserve });
				w.WriteEndElement();
				w.WriteEndElement();
			}
			w.WriteEndElement();
		}
	}
}
