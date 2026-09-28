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
	/// Skedari eshte i njejte me ate te exporter-it (u krahasua qelize per qelize): XLSX me tekst si "@",
	/// data si date Excel "dd/MM/yyyy", numrat si numra "#,##0.00", kufij, rreshti i pare i ngrire dhe autofilter;
	/// CSV me "," (Windows-1252, CRLF), data dd/MM/yyyy dhe numrat #,##0.00.
	///
	/// Perdoret vetem kur rezultati eshte i njejte me ate te exporter-it: pa filter, renditje apo grupim ne gride
	/// (ne ato raste rreshtat/rradha i vendos gridi). Perndryshe kthen false dhe perdoret exporter-i si me pare.
	/// </summary>
	public static class EksportRapid
	{
		/// <summary>Nen kete numer rreshtash te zgjedhur exporter-i i DevExpress eshte mjaft i shpejte dhe perdoret si me pare.</summary>
		public const int PragRreshtash = 20000;

		private enum Lloji { Tekst, Date, Numer, Bool }

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
				.Select(c => new { Kolona = table.Columns[c.FieldName], Titulli = string.IsNullOrEmpty(c.Caption) ? c.FieldName : c.Caption })
				.ToList();
			if (kolonat.Count == 0)
				return false;

			var rreshtat = RreshtatEZgjedhur(grid, table);
			if (rreshtat.Count == 0)
				return false;

			string skedar = Path.Combine(Path.GetTempPath(), "avec-eksport-" + Guid.NewGuid().ToString("N"));
			try
			{
				var indekset = kolonat.Select(k => k.Kolona.Ordinal).ToArray();
				var llojet = kolonat.Select(k => LlojiKolones(k.Kolona.DataType)).ToArray();
				var titujt = kolonat.Select(k => k.Titulli).ToArray();
				if (tipi == "CSV")
					ShkruajCsv(skedar, rreshtat, indekset, llojet, titujt);
				else
					ShkruajXlsx(skedar, rreshtat, indekset, llojet, titujt, string.IsNullOrWhiteSpace(emerSheet) ? "Sheet1" : emerSheet);

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

		private static Lloji LlojiKolones(Type t)
		{
			if (t == typeof(DateTime))
				return Lloji.Date;
			if (t == typeof(bool))
				return Lloji.Bool;
			if (t == typeof(decimal) || t == typeof(double) || t == typeof(float) || t == typeof(int) || t == typeof(long)
				|| t == typeof(short) || t == typeof(byte))
				return Lloji.Numer;
			return Lloji.Tekst;
		}

		#region CSV

		private static void ShkruajCsv(string skedar, List<DataRow> rreshtat, int[] indekset, Lloji[] llojet, string[] titujt)
		{
			var kultura = CultureInfo.CurrentCulture;
			string ndares = kultura.TextInfo.ListSeparator;
			using (var w = new StreamWriter(skedar, false, Encoding.Default, 1 << 16))
			{
				w.Write(string.Join(ndares, titujt.Select(t => CsvFushe(t, ndares))));
				var rresht = new string[indekset.Length];
				foreach (var r in rreshtat)
				{
					for (int i = 0; i < indekset.Length; i++)
						rresht[i] = CsvFushe(TekstiCsv(r[indekset[i]], llojet[i], kultura), ndares);
					w.Write("\r\n");
					w.Write(string.Join(ndares, rresht));
				}
			}
		}

		private static string TekstiCsv(object vlera, Lloji lloji, CultureInfo kultura)
		{
			if (vlera == null || vlera == DBNull.Value)
				return "";
			switch (lloji)
			{
				case Lloji.Date:
					return ((DateTime)vlera).ToString("dd/MM/yyyy", kultura);
				case Lloji.Numer:
					return Convert.ToDecimal(vlera, CultureInfo.InvariantCulture).ToString("#,##0.00", kultura);
				default:
					return Convert.ToString(vlera, kultura);
			}
		}

		private static string CsvFushe(string s, string ndares)
		{
			if (s.IndexOf('"') >= 0 || s.Contains(ndares) || s.IndexOf('\n') >= 0 || s.IndexOf('\r') >= 0)
				return "\"" + s.Replace("\"", "\"\"") + "\"";
			return s;
		}

		#endregion

		#region XLSX

		// stilet (si ne skedarin e exporter-it): 1 = i pergjithshem me kufi, 2 = date dd/MM/yyyy,
		// 3 = numer #,##0.00, 4 = tekst "@" (titujt dhe vlerat tekst)
		private const string StiliBosh = "1", StiliDate = "2", StiliNumer = "3", StiliTekst = "4";

		private static void ShkruajXlsx(string skedar, List<DataRow> rreshtat, int[] indekset, Lloji[] llojet, string[] titujt, string emerSheet)
		{
			var fjalet = new Dictionary<string, int>(StringComparer.Ordinal);
			var listaFjaleve = new List<string>();
			int IndeksiFjales(string s)
			{
				if (!fjalet.TryGetValue(s, out int i))
				{
					i = listaFjaleve.Count;
					fjalet.Add(s, i);
					listaFjaleve.Add(s);
				}
				return i;
			}

			int nrKolonash = indekset.Length;
			int nrRreshtash = rreshtat.Count + 1;
			var shkronjat = Enumerable.Range(0, nrKolonash).Select(ShkronjaKolones).ToArray();
			string zona = "A1:" + shkronjat[nrKolonash - 1] + nrRreshtash.ToString(CultureInfo.InvariantCulture);

			var emri = emerSheet.Length > 31 ? emerSheet.Substring(0, 31) : emerSheet;
			foreach (var c in new[] { '\\', '/', '?', '*', '[', ']', ':' })
				emri = emri.Replace(c, ' ');

			using (var doc = SpreadsheetDocument.Create(skedar, SpreadsheetDocumentType.Workbook))
			{
				var wbPart = doc.AddWorkbookPart();

				var stilet = wbPart.AddNewPart<WorkbookStylesPart>();
				stilet.Stylesheet = Stilet();
				stilet.Stylesheet.Save();

				var wsPart = wbPart.AddNewPart<WorksheetPart>();
				using (var w = OpenXmlWriter.Create(wsPart))
				{
					w.WriteStartElement(new Worksheet());
					w.WriteElement(new SheetProperties(new OutlineProperties { SummaryBelow = false, SummaryRight = false }));
					w.WriteElement(new SheetViews(new SheetView(
						new Pane { TopLeftCell = "A2", VerticalSplit = 1D, ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen },
						new Selection { Pane = PaneValues.BottomLeft, ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue> { InnerText = "A1" } })
					{ WorkbookViewId = 0U }));
					var cols = new Columns();
					for (int i = 0; i < nrKolonash; i++)
						cols.Append(new Column
						{
							Min = (uint)(i + 1), Max = (uint)(i + 1), Width = 28.57D, CustomWidth = true,
							Style = uint.Parse(llojet[i] == Lloji.Date ? StiliDate : llojet[i] == Lloji.Numer ? StiliNumer : StiliBosh, CultureInfo.InvariantCulture)
						});
					w.WriteElement(cols);

					w.WriteStartElement(new SheetData());
					ShkruajRreshtTitujsh(w, 1, shkronjat, titujt, IndeksiFjales);
					int nr = 1;
					foreach (var r in rreshtat)
					{
						nr++;
						string nrTekst = nr.ToString(CultureInfo.InvariantCulture);
						w.WriteStartElement(new Row(), new[] { new OpenXmlAttribute("r", null, nrTekst) });
						for (int i = 0; i < nrKolonash; i++)
							ShkruajQelize(w, shkronjat[i] + nrTekst, r[indekset[i]], llojet[i], IndeksiFjales);
						w.WriteEndElement();
					}
					w.WriteEndElement();

					w.WriteElement(new AutoFilter { Reference = zona });
					w.WriteElement(new IgnoredErrors(new IgnoredError { SequenceOfReferences = new ListValue<StringValue> { InnerText = zona }, NumberStoredAsText = true }));
					w.WriteEndElement();
				}

				var sstPart = wbPart.AddNewPart<SharedStringTablePart>();
				using (var w = OpenXmlWriter.Create(sstPart))
				{
					w.WriteStartElement(new SharedStringTable(), new[]
					{
						new OpenXmlAttribute("count", null, listaFjaleve.Count.ToString(CultureInfo.InvariantCulture)),
						new OpenXmlAttribute("uniqueCount", null, listaFjaleve.Count.ToString(CultureInfo.InvariantCulture))
					});
					foreach (var s in listaFjaleve)
						w.WriteElement(new SharedStringItem(new Text(s) { Space = SpaceProcessingModeValues.Preserve }));
					w.WriteEndElement();
				}

				wbPart.Workbook = new Workbook(
					new Sheets(new Sheet { Id = wbPart.GetIdOfPart(wsPart), SheetId = 1U, Name = emri }),
					new DefinedNames(new DefinedName("'" + emri.Replace("'", "''") + "'!" + AbsoluteRef(zona)) { Name = "_xlnm._FilterDatabase", Hidden = true, LocalSheetId = 0U }));
				wbPart.Workbook.Save();
			}
		}

		private static void ShkruajRreshtTitujsh(OpenXmlWriter w, int nr, string[] shkronjat, string[] titujt, Func<string, int> indeksi)
		{
			string nrTekst = nr.ToString(CultureInfo.InvariantCulture);
			w.WriteStartElement(new Row(), new[] { new OpenXmlAttribute("r", null, nrTekst) });
			for (int i = 0; i < titujt.Length; i++)
				ShkruajTekst(w, shkronjat[i] + nrTekst, PaKaraktereKontrolli(titujt[i]), StiliTekst, indeksi);
			w.WriteEndElement();
		}

		private static void ShkruajQelize(OpenXmlWriter w, string ref_, object vlera, Lloji lloji, Func<string, int> indeksi)
		{
			if (vlera == null || vlera == DBNull.Value)
			{
				ShkruajTekst(w, ref_, "", StiliBosh, indeksi);
				return;
			}
			switch (lloji)
			{
				case Lloji.Date:
					ShkruajVlere(w, ref_, null, StiliDate, ((DateTime)vlera).ToOADate().ToString("R", CultureInfo.InvariantCulture));
					break;
				case Lloji.Numer:
					ShkruajVlere(w, ref_, null, StiliNumer, Convert.ToDouble(vlera, CultureInfo.InvariantCulture).ToString("R", CultureInfo.InvariantCulture));
					break;
				case Lloji.Bool:
					ShkruajVlere(w, ref_, "b", StiliBosh, (bool)vlera ? "1" : "0");
					break;
				default:
					var s = PaKaraktereKontrolli(Convert.ToString(vlera, CultureInfo.CurrentCulture));
					ShkruajTekst(w, ref_, s, s.Length == 0 ? StiliBosh : StiliTekst, indeksi);
					break;
			}
		}

		private static void ShkruajTekst(OpenXmlWriter w, string ref_, string s, string stili, Func<string, int> indeksi) =>
			ShkruajVlere(w, ref_, "s", stili, indeksi(s).ToString(CultureInfo.InvariantCulture));

		private static void ShkruajVlere(OpenXmlWriter w, string ref_, string tipi, string stili, string vlera)
		{
			var atr = new List<OpenXmlAttribute>(3) { new OpenXmlAttribute("r", null, ref_), new OpenXmlAttribute("s", null, stili) };
			if (tipi != null)
				atr.Add(new OpenXmlAttribute("t", null, tipi));
			w.WriteStartElement(new Cell(), atr);
			w.WriteElement(new CellValue(vlera));
			w.WriteEndElement();
		}

		private static Stylesheet Stilet()
		{
			var kufi = new DocumentFormat.OpenXml.Spreadsheet.Border(
				new LeftBorder(new Color { Rgb = "FF000000" }) { Style = BorderStyleValues.Thin },
				new RightBorder(new Color { Rgb = "FF000000" }) { Style = BorderStyleValues.Thin },
				new TopBorder(new Color { Rgb = "FF000000" }) { Style = BorderStyleValues.Thin },
				new BottomBorder(new Color { Rgb = "FF000000" }) { Style = BorderStyleValues.Thin },
				new DiagonalBorder());
			return new Stylesheet(
				new NumberingFormats(new NumberingFormat { NumberFormatId = 164U, FormatCode = "dd/MM/yyyy" }) { Count = 1U },
				new Fonts(
					new DocumentFormat.OpenXml.Spreadsheet.Font(new FontSize { Val = 11D }, new Color { Theme = 1U }, new FontName { Val = "Calibri" }, new FontFamilyNumbering { Val = 2 }, new FontScheme { Val = FontSchemeValues.Minor }),
					new DocumentFormat.OpenXml.Spreadsheet.Font(new FontSize { Val = 11D }, new Color { Rgb = "FF000000" }, new FontName { Val = "Calibri" }, new FontFamilyNumbering { Val = 0 }))
				{ Count = 2U },
				new Fills(new Fill(new PatternFill { PatternType = PatternValues.None }), new Fill(new PatternFill { PatternType = PatternValues.Gray125 })) { Count = 2U },
				new Borders(new DocumentFormat.OpenXml.Spreadsheet.Border(), kufi) { Count = 2U },
				new CellStyleFormats(new CellFormat { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U }) { Count = 1U },
				new CellFormats(
					new CellFormat { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U, FormatId = 0U },
					new CellFormat { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 1U, FormatId = 0U, ApplyFont = true, ApplyAlignment = true },
					new CellFormat { NumberFormatId = 164U, FontId = 0U, FillId = 0U, BorderId = 1U, FormatId = 0U, ApplyNumberFormat = true, ApplyFont = true, ApplyAlignment = true },
					new CellFormat { NumberFormatId = 4U, FontId = 0U, FillId = 0U, BorderId = 1U, FormatId = 0U, ApplyNumberFormat = true, ApplyFont = true, ApplyAlignment = true },
					new CellFormat { NumberFormatId = 49U, FontId = 1U, FillId = 0U, BorderId = 1U, FormatId = 0U, ApplyNumberFormat = true, ApplyFont = true })
				{ Count = 5U },
				new CellStyles(new CellStyle { Name = "Normal", FormatId = 0U, BuiltinId = 0U }) { Count = 1U });
		}

		private static string ShkronjaKolones(int i)
		{
			var s = "";
			for (i++; i > 0; i = (i - 1) / 26)
				s = (char)('A' + (i - 1) % 26) + s;
			return s;
		}

		private static string AbsoluteRef(string zona) =>
			string.Join(":", zona.Split(':').Select(r =>
			{
				int j = 0;
				while (j < r.Length && char.IsLetter(r[j])) j++;
				return "$" + r.Substring(0, j) + "$" + r.Substring(j);
			}));

		/// <summary>XML nuk lejon karakteret e kontrollit (pervec tab/rresht i ri); ato do prishnin skedarin.</summary>
		private static string PaKaraktereKontrolli(string s)
		{
			for (int i = 0; i < s.Length; i++)
				if (s[i] < 0x20 && s[i] != '\t' && s[i] != '\n' && s[i] != '\r')
					return new string(s.Where(c => c >= 0x20 || c == '\t' || c == '\n' || c == '\r').ToArray());
			return s;
		}

		#endregion
	}
}
