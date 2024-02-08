using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using AlphaWeb.Core.SharedKernel;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbAsete;
using DbCore.DbBuxheti;
using DbCore.DbImporte;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbProdhimi;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DevExpress.Data.Filtering;
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using NLog;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
	public partial class Eksport : MyPageBase
	{
		private const string Komponente = "Eksport.aspx";

		/// <summary>
		/// mbush te dhenat kur faqja ben loadim
		/// </summary>
		/// <param name="sender">derguesi</param>
		/// <param name="e">argumentat</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!mySessionObjects.isLogedIn(Session))
				clsFunksione.logout(Session, true, "FaqePaautorizuar");

			if (mySessionObjects.ktheKodNdermarrje(Session) == null)
				Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

			VendosPerkthimet();
			MbushHiddenFieldMePerkthime();

			if (!IsPostBack)
			{
				ShtoVleraTePergjithshmeNeHfState();
				int idKategoriaPerSel = 0;
				var idQueryString = string.Empty;

				if (Request.QueryString["idDok"] != null && !string.IsNullOrEmpty(Request.QueryString["idDok"]))
					idQueryString = Request.QueryString["idDok"];
				if (Request.QueryString["veprimi"] != null && !string.IsNullOrEmpty(Request.QueryString["veprimi"]))
					idKategoriaPerSel = KtheIdKategoriNgaQueryStringu(Request.QueryString["veprimi"]);

				EmrateButonave();

				hfState.Set("superuser", colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(IdPerdoruesi, "RSU") == -1);
				KonfiguroVleraFillestareShto(idKategoriaPerSel);
				PercaktoTemplateMenu();
				DbCore.mySessionObjects.ruajGrideNeSession(Komponente, Session, (object)new DataTable());
				hfState.Set("idQueryString", idQueryString);
				if (idKategoriaPerSel > 0)
					NgarkoGride(false);
			}
			else
			{
				if (!IsCallback || IsCallback && Request["__CALLBACKID"].Contains("ASPxMenu1"))
					PercaktoTemplateMenu();
			}

			MbushGrideNgaSesioni();
			GridUtil.konfigGrideListeEMadhePaTheme(gvExport, gvExport.KeyFieldName, false, false);
		}

		private static int KtheIdKategoriNgaQueryStringu(string veprimi)
		{
			int idKategoriaPerSel;
			switch (veprimi)
			{
				case "shitje":
					idKategoriaPerSel = 1;
					break;
				case "blerje":
					idKategoriaPerSel = 2;
					break;
				case "arketim":
				case "pagese":
					idKategoriaPerSel = 3;
					break;
				case "derdhje":
				case "terheqje":
					idKategoriaPerSel = 4;
					break;
				case "hyrje":
				case "dalje":
					idKategoriaPerSel = 6;
					break;
				case "veprimekf":
					idKategoriaPerSel = 20;
					break;
				case "ekzekutimProdhimi":
					idKategoriaPerSel = 45;
					break;
				case "amortizimi":
					idKategoriaPerSel = 90;
					break;
				case "inventarizimAsh":
					idKategoriaPerSel = 135;
					break;
				case "inventarizimAgj":
					idKategoriaPerSel = 136;
					break;
				default:
					idKategoriaPerSel = 0;
					break;
			}

			return idKategoriaPerSel;
		}

		/// <summary>
		/// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
		/// </summary>       
		private void EmrateButonave()
		{
			ButtonCancel.Text = MessagesResource.Messages["btnAdministrimiCancel"];
			btnSelectAll.Text = MessagesResource.Messages["btnZgjidhTeGjitha"];
			btnUnselectAll.Text = MessagesResource.Messages["btnHiqZgjedhjenTeGjitha"];
			btnSelectAllOnPage.Text = MessagesResource.Messages["zgjidhTeGjithaBtn"];
			btnUnselectAllOnPage.Text = MessagesResource.Messages["btnHiqZgjedhjenNeFaqe"];
		}

		/// <summary>
		/// Vendos emrat e hidden fiels ne baze te gjuhes se perdoruesit
		/// </summary>      
		private void MbushHiddenFieldMePerkthime()
		{
			hfState.Set("msgGabimeGjateTransferimit", MessagesResource.Messages["msgGabimeGjateTransferimit"]);
			hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", MessagesResource.Messages["msgPoTransferohetTeDhenatShtypniPerseriRuaj"]);
			hfState.Set("msgShenoEmrin", MessagesResource.Messages["msgShenoEmrin"]);
			hfState.Set("msgShenoKategori", MessagesResource.Messages["msgShenoKategori"]);
			hfState.Set("msgShenoFormatin", MessagesResource.Messages["msgShenoFormatin"]);
			hfState.Set("msgPlotesoniTabelatEksportit", MessagesResource.Messages["msgPlotesoniTabelatEksportit"]);
			hfState.Set("msgNukKaRreshtaPerEksport", MessagesResource.Messages["msgNukKaRreshtaPerEksport"]);
			hfState.Set("msgZgjidhEmerSkedar", MessagesResource.Messages["msgZgjidhEmerSkedar"]);
			hfState.Set("msgZgjidhFormatin", MessagesResource.Messages["msgZgjidhFormatin"]);
			hfState.Set("msgSkaRreshtaNeGrid", MessagesResource.Messages["msgSkaRreshtaNeGrid"]);
			hfState.Set("msgVendosDaten", MessagesResource.Messages["msgVendosDaten"]);
			hfState.Set("msgZgjidhKategorine", MessagesResource.Messages["msgZgjidhKategorine"]);
			hfState.Set("msgPlotesoniFushat", MessagesResource.Messages["msgPlotesoniFushat"]);
			hfState.Set("msgPlotesoniFushatETabelave", MessagesResource.Messages["msgPlotesoniFushatETabelave"]);
			hfState.Set("msgPlotesoniFushatETabelesKoke", MessagesResource.Messages["msgPlotesoniFushatETabelesKoke"]);
		}

		private void VendosPerkthimet()
		{
			lblLlojEksporti.Text = MessagesResource.Messages["lblLloji"];
			lblEmer.Text = MessagesResource.Messages["labelRaportEmri"];
			lblTipi.Text = MessagesResource.Messages["labelFilterAvancuarTipi"];
			lblKategoria.Text = MessagesResource.Messages["labelKategoria"];
			lblFormati.Text = MessagesResource.Messages["labelFilterAvancuarFormati"];
			lblEmerSheet.Text = MessagesResource.Messages["labelEmriExcel"];
			lblEmerSkedari.Text = MessagesResource.Messages["labelEmriISkedarit"];
			lblEmerTabKoka.Text = MessagesResource.Messages["lblEmriTabeleKoke"];
			lblEmerTabTrupi.Text = MessagesResource.Messages["lblEmriTabeleTrup"];
			lblEmerTabRec.Text = MessagesResource.Messages["lblEmriTabeleReceptura"];
			popFshi.HeaderText = MessagesResource.Messages["labelAdministrimiKujdes"];
			lblMsgbox.Text = MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"];
			ButtonCancel.Text = MessagesResource.Messages["labelAnullo"];
			lblFiltri.Text = MessagesResource.Messages["lblFiltri"];
		}

		/// <summary>
		/// mbush menune me buttonat perkates sipas faqes
		/// </summary>
		private void PercaktoTemplateMenu() =>
			clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, Komponente, this, MenuInfo, cmbEmer.SelectedIndex == -1, true, false, Meme);

		/// <summary>
		/// ndodh kur menuja ben bound
		/// </summary>
		/// <param name="sender">derguesi</param>
		/// <param name="e">argumentat</param>
		protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

		/// <summary>
		/// Vendos vlerat default kur po behet shtim
		/// </summary>
		private void KonfiguroVleraFillestareShto(int idKategoriaPerSel)
		{
			ConfigureAspxComboBox.KonfiguroComboBoxLlojEksporti(cmbLlojEksporti);
			ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimExporti(cmbEmer, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
			ConfigureAspxComboBox.KonfiguroComboBoxKategoriPerImport(cmbKategoria, false, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
			ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbFormati);

			cmbFiltri.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
			cmbFiltri.DropDownStyle = DropDownStyle.DropDown;
		}

		/// <summary>
		/// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
		/// </summary>
		protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
		{
			switch (e.Item.Name)
			{
				case "Ruaj":
					Page.Validate();
					RuajKonfigurim();
					break;
				case "Ngarko":
					NgarkoGride(cmbLlojEksporti.Text == "SQL");
					break;
				case "Shto":
					Pastrogride(true);
					break;
				case "Eksporto":
					EksportoGride();
					break;
			}
		}

		private void Pastrogride(bool pastro)
		{
			gvExport.Selection.UnselectAll();
			gvExport.Columns.Clear();
			gvExport.AutoGenerateColumns = true;
			var tb = new DataTable();
			gvExport.DataSource = tb;
			gvExport.DataBind();
			if (pastro)
			{
				DbCore.mySessionObjects.ruajGrideNeSession(Komponente, Session, (object)tb);
				status1.Value = "pastro";
				gvExport.FilterExpression = "";
			}
			tb.Dispose();
		}
		private void FiltroGride(int idFiltri, bool ndryshoFilterGride = false)
		{
			DataTable table;
			mySessionObjects.merrGrideNgaSessioni(Komponente, Session, out table);
			gvExport.DataSource = table;
			gvExport.DataBind();
			if (cmbFiltri.Value != null && (string.IsNullOrWhiteSpace(gvExport.FilterExpression) || ndryshoFilterGride))
				gvExport.FilterExpression = new clsFiltraExporti(idFiltri).Pershkrimi;
		}

		private void EksportoGride()
		{

			int idKategoria = (cmbKategoria.Value) != null ? int.Parse(cmbKategoria.Value.ToString()) : KtheIdKategoriNgaQueryStringu(Request.QueryString["veprimi"].ToString());
			if (cmbLlojEksporti.Text == "SQL")
			{
				EksportoGrideNeTabelaSql();
				return;
			}

			if (idKategoria.ToString() == "92")
			{
				EksportoFlexCube();
				return;
			}

			switch (rbTipi.Value.ToString())
			{
				case "CSV":
					{
						try
						{
							ASPxGridViewExporter1.WriteCsvToResponse(txtEmerSkedari.Text);
						}
						catch (ThreadAbortException ex)
						{
						}
						catch (Exception err)
						{
							LogManager.GetCurrentClassLogger().Error(err.Message);
							clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNdodhiNjeGabimGjateKrijimitTeDok"], pnlMesazhi);
						}
					}
					break;
				case "XLSX":
					{
						var o = new XlsxExportOptionsEx();
						if (txtEmerSheet.Text != "")
							o.SheetName = txtEmerSheet.Text;
						o.TextExportMode = TextExportMode.Text;
						o.CustomizeCell += o_customizeCell;
						try
						{
							ASPxGridViewExporter1.WriteXlsxToResponse(txtEmerSkedari.Text, o);
						}
						catch (ThreadAbortException ex)
						{
						}
						catch (Exception err)
						{
							LogManager.GetCurrentClassLogger().Error(err.Message);
							clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNdodhiNjeGabimGjateKrijimitTeDok"], pnlMesazhi);
						}
					}
					break;
				case "XLS":
					{
						var o = new XlsExportOptionsEx();
						if (txtEmerSheet.Text != "")
							o.SheetName = txtEmerSheet.Text;
						o.TextExportMode = TextExportMode.Text;
						o.CustomizeCell += o_customizeCell;
						try
						{
							ASPxGridViewExporter1.WriteXlsToResponse(txtEmerSkedari.Text, o);
						}
						catch (ThreadAbortException ex)
						{
						}
						catch (Exception ex)
						{
							LogManager.GetCurrentClassLogger().Error(ex.Message);
							clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNdodhiNjeGabimGjateKrijimitTeDok"], pnlMesazhi);
						}
					}
					break;
				default:
					clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukBehetEksportMeKeteTip"], pnlMesazhi);
					break;
			}
		}

		private void EksportoGrideNeTabelaSql()
		{
			if (cmbKategoria.Text == "Eksport Demesh")
			{
				ExportoDemeSql();
				return;
			}

			var idSuperKategori = clsKategoriNivelDok.mbushIDSuperKategoriNivDok(int.Parse(cmbKategoria.Value.ToString()));
			var mesazh = clsFunksione.kontrolloMosEkzistenceTabelashDheSP(txtEmerTabKoka.Text, txtEmerTabTrupi.Text, true, int.Parse(cmbKategoria.Value.ToString()), txtEmerTabRec.Text, idSuperKategori);
			if (!mesazh.Status)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
				return;
			}

			if (cmbKategoria.Value.ToString() == "45")
				RuajFaturaNeTabeleNdermjeteseEkzekutimProdhimi();
			else
				RuajFaturaNeTabeleNdermjetese(idSuperKategori);
		}

		private void EksportoFlexCube()
		{
			var rreshta = gvExport.GetSelectedFieldValues("BATCH_NO");
			if (rreshta.Count == 0)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNgarkoniGriden"], pnlMesazhi);
				return;
			}
			var msg = eksportoGriden(txtEmerSheet.Text, txtEmerSkedari.Text, ASPxGridViewExporter1, Session, Response, gvExport);
			if (!msg.Status)
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, msg.PershkrimMesazhi, pnlMesazhi);
		}

		private static void o_customizeCell(CustomizeCellEventArgs ea)
		{
			if (!(ea.Value is string)) return;
			ea.Formatting.NumberFormat = XlNumberFormat.Text;
			ea.Handled = true;
		}

		private void RuajFaturaNeTabeleNdermjeteseEkzekutimProdhimi()
		{
			try
			{
				var kontrolle = KontrolloFormatSelektuar(false, int.Parse(cmbKategoria.Value.ToString()));
				if (!kontrolle.Item1.Status)
				{
					clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolle.Item1.PershkrimMesazhi, pnlMesazhi);
					return;
				}

				var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(kontrolle.Item2);
				var primaryKeyEkzekutim = col.filtroFormatImportiPerPrimaryKey().KodKontrolli;
				var rreshtaKoka = gvExport.GetSelectedFieldValues(primaryKeyEkzekutim);
				var dt = (DataTable)gvExport.DataSource;
				var rreshtaUnike = rreshtaKoka.Distinct().ToList();
				var reshtatKokaFinal = new DataTable();

				foreach (var oo in rreshtaUnike)
				{
					var rreshtatKokesPerId = dt.Select("[" + primaryKeyEkzekutim + "]=" + oo).GetDataTable(dt);
					reshtatKokaFinal.Merge(rreshtatKokesPerId);//Mbushim datatable me rreshtat e perzgjedhur ne gride per eksport.
				}

				var koka = clsFunksione.ktheDataTableMeKokeDokumentesh(col, reshtatKokaFinal, mySessionObjects.ktheKodNdermarrje(Session));
				var primaryKeyProduktet = col.filtroFormatImportiPerPrimaryKeyProduktProdhimi().KodKontrolli;
				var rreshtaKokaProdukt = gvExport.GetSelectedFieldValues(primaryKeyProduktet);
				var rreshtaUnikeProdukt = rreshtaKokaProdukt.Distinct().ToList();
				var reshtatKokaFinalProdukt = new DataTable();

				foreach (var oo in rreshtaUnikeProdukt)
				{
					var rreshtatKokesPerIdProdukt = dt.Select("[" + primaryKeyProduktet + "]=" + oo).GetDataTable(dt);
					reshtatKokaFinalProdukt.Merge(rreshtatKokesPerIdProdukt);//Mbushim datatable me rreshtat e perzgjedhur ne gride per eksport.
				}

				var produktet = clsFunksione.ktheDataTableMeProdukteProdhimi(col, reshtatKokaFinalProdukt);
				var kodKontrolliTrupi = KtheEmraFushashIdSipasKategorie(int.Parse(cmbKategoria.Value.ToString()));
				var rreshtaTrupi = gvExport.GetSelectedFieldValues(kodKontrolliTrupi);
				var rreshtaTrupiFinal = new DataTable();
				dt = (DataTable)gvExport.DataSource;
				foreach (var oo in rreshtaTrupi)
				{
					var rreshtatTrupi = dt.Select(kodKontrolliTrupi + " = " + oo).GetDataTable(dt);
					rreshtaTrupiFinal.Merge(rreshtatTrupi);
				}

				var trupi = clsFunksione.ktheDataTableMeTrupaDokumentesh(col, rreshtaTrupiFinal, kodKontrolliTrupi, int.Parse(cmbKategoria.Value.ToString()));

				var mesazh = colEksportSQL.ruajDokumentaNeTabeleEksporti(koka, produktet, col, txtEmerTabKoka.Text, txtEmerTabTrupi.Text, int.Parse(cmbKategoria.Value.ToString()), txtEmerTabRec.Text, trupi, (int)SuperKategori.Regjistrime);
				if (mesazh.Status)
				{
					NgarkoGride(true);
					clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgEksportimiMeSukses"], pnlMesazhi);
				}
				else
					clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimGjateEksportit"], pnlMesazhi);
			}
			catch (Exception ex)
			{
				LogManager.GetCurrentClassLogger().Error(ex.Message);
				throw new MyException(MessagesResource.Messages["msgGabimGjateRuajtjesTabelatNdermjetese"], ex);
			}
		}

		private void RuajFaturaNeTabeleNdermjetese(int idSuperKategori)
		{
			try
			{
				var kontrolle = KontrolloFormatSelektuar(false, int.Parse(cmbKategoria.Value.ToString()));
				if (!kontrolle.Item1.Status)
				{
					clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolle.Item1.PershkrimMesazhi, pnlMesazhi);
					return;
				}

				var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(kontrolle.Item2);
				DataTable trupi = new DataTable(), reshtatKokaFinal = new DataTable();
				var dt = (DataTable)gvExport.DataSource;
				var primaryKey = MerrPrimaryKey(col, idSuperKategori);
				var rreshtaKoka = gvExport.GetSelectedFieldValues(primaryKey);
				var rreshtaUnike = rreshtaKoka.Distinct().ToList();
				foreach (var oo in rreshtaUnike)
				{
					var rreshtatKokesPerId = dt.Select(string.Format("[{0}]={1}", primaryKey, oo)).GetDataTable(dt);
					reshtatKokaFinal.Merge(rreshtatKokesPerId);//Mbushim datatable me rreshtat e perzgjedhur ne gride per eksport.
				}

				var koka = clsFunksione.ktheDataTableMeKokeDokumentesh(col, reshtatKokaFinal, mySessionObjects.ktheKodNdermarrje(Session));

				if (idSuperKategori == (int)SuperKategori.Regjistrime)
				{
					var kodKontrolliTrupi = KtheEmraFushashIdSipasKategorie(int.Parse(cmbKategoria.Value.ToString()));
					var rreshtaTrupi = gvExport.GetSelectedFieldValues(kodKontrolliTrupi);
					var rreshtaTrupiFinal = new DataTable();
					dt = (DataTable)gvExport.DataSource;
					foreach (var oo in rreshtaTrupi)
					{
						var rreshtatTrupi = dt.Select(string.Format("{0} = {1}", kodKontrolliTrupi, oo)).GetDataTable(dt);
						rreshtaTrupiFinal.Merge(rreshtatTrupi);
					}
					trupi = clsFunksione.ktheDataTableMeTrupaDokumentesh(col, rreshtaTrupiFinal, kodKontrolliTrupi, int.Parse(cmbKategoria.Value.ToString()));
				}

				var mesazh = colEksportSQL.ruajDokumentaNeTabeleEksporti(koka, trupi, col, txtEmerTabKoka.Text, txtEmerTabTrupi.Text, int.Parse(cmbKategoria.Value.ToString()), txtEmerTabRec.Text, new DataTable(), idSuperKategori);
				if (mesazh.Status)
				{
					NgarkoGride(true);
					clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgEksportimiMeSukses"], pnlMesazhi);
				}
				else
					clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimGjateEksportit"], pnlMesazhi);
			}
			catch (Exception e)
			{
				LogManager.GetCurrentClassLogger().Error(e.Message);
				throw new MyException(MessagesResource.Messages["msgGabimGjateRuajtjesTabelatNdermjetese"], e);
			}
		}

		private string MerrPrimaryKey(colTrupiFormatImporti col, int idSuperKategori)
		{
			return idSuperKategori == (int)SuperKategori.Regjistrime
				? col.filtroFormatImportiPerPrimaryKey().KodKontrolli
				: KtheEmraFushashIdSipasKategorie(int.Parse(cmbKategoria.Value.ToString()));
		}

		private void ExportoDemeSql()
		{
			var rreshta = gvExport.GetSelectedFieldValues("IDNIVELDOKUMENT", "IDDOKUMENTI", "NrDok", "DateDok", "NrCeshtje", "Monedha", "Vlefta", "Pershkrimi", "Furnitor", "IDDOKNGA");
			if (rreshta.Count == 0)
			{
				clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgNukKaRreshtaPerEksport"], pnlMesazhi);
				return;
			}

			var dtSelektuar = new DataTable("demet");
			dtSelektuar.Columns.Add("IDNIVELDOKUMENT", new decimal().GetType());
			dtSelektuar.Columns.Add("IDDOKUMENTI", new decimal().GetType());
			dtSelektuar.Columns.Add("NRDOK", typeof(string));
			dtSelektuar.Columns.Add("DATEDOK", new DateTime().GetType());
			dtSelektuar.Columns.Add("NRCESHTJE", typeof(string));
			dtSelektuar.Columns.Add("MONEDHA", typeof(string));
			dtSelektuar.Columns.Add("VLEFTAPAGUAR", new double().GetType());
			dtSelektuar.Columns.Add("PERSHKRIMVEPRIMI", typeof(string));
			dtSelektuar.Columns.Add("FURNITOR", typeof(string));
			dtSelektuar.Columns.Add("IDDOKNGA", new decimal().GetType());

			foreach (var oo in rreshta)
			{
				var rreshti = dtSelektuar.NewRow();
				rreshti["IDNIVELDOKUMENT"] = ((object[])(oo))[0];
				rreshti["IDDOKUMENTI"] = ((object[])(oo))[1];
				rreshti["NRDOK"] = ((object[])(oo))[2];
				rreshti["DATEDOK"] = ((object[])(oo))[3];
				rreshti["NRCESHTJE"] = ((object[])(oo))[4];
				rreshti["MONEDHA"] = ((object[])(oo))[5];
				rreshti["VLEFTAPAGUAR"] = ((object[])(oo))[6];
				rreshti["PERSHKRIMVEPRIMI"] = ((object[])(oo))[7];
				rreshti["FURNITOR"] = ((object[])(oo))[8];
				rreshti["IDDOKNGA"] = ((object[])(oo))[9];
				dtSelektuar.Rows.Add(rreshti);
			}

			var mesazh = clsKokaFormatImporti.ruajTeDhenaNeTabeleEksportiDt(dtSelektuar);
			if (mesazh.Status)
			{
				clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgEksportiDemeve"], pnlMesazhi);
				Pastrogride(true);
				status1.Value = "false";
			}
			else
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimGjateEksportDemeve"], pnlMesazhi);
				status1.Value = "false";
			}
		}

		private Tuple<clsMesazh, int> KontrolloFormatSelektuar(bool sql, int idKategoria)
		{
			int formati = 0;
			var formati1 = 0;
			int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

			if (cmbFormati.Value != null)
				int.TryParse(cmbFormati.Value.ToString(), out formati);
			else
			{
				DataView dv = new DataView(colKokaFormatImporti.MerrFormatetsipasNdermarjesDheKategorise(idNdermarrje, idKategoria));

				DataTable dt = dv.ToTable(true, "IDKOKA");
				if (dt.Rows.Count > 0)
				{
					var formatStandart = dt.Rows[0].ItemArray;
					if (Request.QueryString["veprimi"] != null && Request.QueryString["veprimi"].ToString() == "dalje")
						formatStandart = dt.Rows[1].ItemArray;
					int.TryParse(formatStandart[0].ToString(), out formati);
				}
			}
			if (formati == 0)
			{
				status1.Value = "false";
				return new Tuple<clsMesazh, int>(new clsMesazh(false, MessagesResource.Messages["msgImportKyFormatNukEkziston"]), formati);
			}

			if (new clsKokaFormatImporti(formati).IdKoka == 0)
			{
				status1.Value = "false";
				return new Tuple<clsMesazh, int>(new clsMesazh(false, MessagesResource.Messages["msgImportKyFormatNukEkziston"]), formati);
			}

			if (sql)
			{
				var mesazh = clsFunksione.kontrolloMosEkzistenceTabelashDheSP(txtEmerTabKoka.Text, txtEmerTabTrupi.Text, true, int.Parse(cmbKategoria.Value.ToString()), txtEmerTabRec.Text, clsKategoriNivelDok.mbushIDSuperKategoriNivDok(idKategoria));
				if (!mesazh.Status)
				{
					status1.Value = "false";
					return new Tuple<clsMesazh, int>(new clsMesazh(false, mesazh.PershkrimMesazhi), formati);
				}
			}

			return new Tuple<clsMesazh, int>(new clsMesazh(true, "Kontrollet u kaluan me sukses!"), formati);
		}

		private void NgarkoEksportDemesh()
		{
			var table = clsKokaFormatImporti.merrTeDhenatPerTabeleEksporti(IdNdermarrja);

			foreach (DataColumn d in table.Columns)
			{
				gvExport.Columns.Add(new GridViewDataColumn(d.ColumnName));
				if (d.ColumnName == "IDNIVELDOKUMENT" || d.ColumnName == "IDNIVELDOKUMENT" || d.ColumnName == "IDDOKUMENTI" || d.ColumnName == "IDDOKNGA")
					gvExport.Columns[d.ColumnName].Visible = false;
			}

			DbCore.mySessionObjects.ruajGrideNeSession(Komponente, Session, (object)table);
			gvExport.DataSource = table;
			gvExport.DataBind();
			table.Dispose();
			gvExport.KeyFieldName = "Nr";
			status1.Value = "export";
			gvExport.Columns["#"].VisibleIndex = 0;
		}

		private void NgarkoGride(bool sql, bool pastroFilterGride = false)
		{
			int idKategoriaQyeryString = KtheIdKategoriNgaQueryStringu(Request.QueryString["veprimi"]?.ToString());
			int idKategoria = (cmbKategoria.Value) != null ? int.Parse(cmbKategoria.Value.ToString()) : idKategoriaQyeryString;
			if (sql && idKategoria == 0)
			{
				NgarkoEksportDemesh();
				return;
			}

			Pastrogride(false);

			var kontrolle = KontrolloFormatSelektuar(sql, idKategoria);
			if (!kontrolle.Item1.Status)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolle.Item1.PershkrimMesazhi, pnlMesazhi);
				return;
			}

			var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(kontrolle.Item2);
			var primaryKey = col.filtroFormatImportiPerPrimaryKey();
			var emraFushash = KtheEmraFushashIdSipasKategorie(idKategoria);
			ShtoTrupIdNeCol(ref col, emraFushash);

			int lloji = sql ? 2 : 1;
			DataTable table = new DataTable();

			if (!pastroFilterGride)
				if (cmbFiltri.Value != null && string.IsNullOrWhiteSpace(gvExport.FilterExpression))
					gvExport.FilterExpression = new clsFiltraExporti(int.Parse(cmbFiltri.Value.ToString())).Pershkrimi;

			switch (idKategoria)
			{
				case 1:
				case 2:
					var op = CriteriaOperator.Parse(string.IsNullOrWhiteSpace(gvExport.FilterExpression) ? "" : gvExport.FilterExpression, 0);
					var filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
					var artikujSet = idKategoria.ToString().EqualsAnyIgnoreCase("1", "2", "6") && col.FirstOrDefault(x => x.KodKontrolli == "Artikulli Set").Visible;
					var serialeUnike = idKategoria.ToString().EqualsAnyIgnoreCase("1", "2", "6") && col.FirstOrDefault(x => x.KodKontrolli.EqualsAnyIgnoreCase("Seriali Unik Kryesor", "Seriali unik dytesor")).Visible;
					table = colKokaShitje.merrShitjePerEksport(IdNdermarrja, IdPerdoruesi, idKategoria, IdNdermarrjeVit, lloji, txtEmerTabKoka.Text, primaryKey.EmerImporti, hfState["idQueryString"].ToString(), cbMerrDokTeMod.Checked, cbMerrDokTeFshire.Checked, false, artikujSet, serialeUnike, filterString, false);
					break;
				case 3:
				case 4:
					table = colVeprimBankaKoka.kthedokVeprimeArkaBankaPerEksport(IdNdermarrja, IdPerdoruesi, IdNdermarrjeVit, idKategoria, lloji, txtEmerTabKoka.Text, primaryKey.EmerImporti, hfState["idQueryString"].ToString());
					break;
				case 5:
					table = colKokatFletetKontabel.MerrFleteKontabelExport(IdNdermarrja, IdPerdoruesi, IdNdermarrjeVit, lloji, txtEmerTabKoka.Text, primaryKey.EmerImporti, hfState["idQueryString"].ToString());
					break;
				case 6:
					var serialeUnik = idKategoria.ToString().EqualsAnyIgnoreCase("1", "2", "6") && col.FirstOrDefault(x => x.KodKontrolli.EqualsAnyIgnoreCase("Seriali Unik Kryesor", "Seriali unik dytesor")).Visible;
					table = colKokaMagazina.merrDokMagazinePerEksport(IdNdermarrja, IdPerdoruesi, IdNdermarrjeVit, lloji, txtEmerTabKoka.Text, primaryKey.EmerImporti, hfState["idQueryString"].ToString(), serialeUnik);
					break;
				case 7:
					table = colShperndarjeShpenzimeKoka.ktheDokShperndarjeShpenzimiPerEksport(IdNdermarrja, kontrolle.Item2);
					break;
				case 12:
					table = colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDTExport(IdNdermarrja, IdPerdoruesi, lloji, txtEmerTabKoka.Text, col.filtroFormatImportiSipasFushes("Kodi").EmerImporti);
					break;
				case 13:
					table = colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDTExport(IdNdermarrja, IdPerdoruesi, false, lloji, txtEmerTabKoka.Text, col.filtroFormatImportiSipasFushes("Kodi").EmerImporti);
					break;
				case 14:
					table = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeExport(IdNdermarrja, IdPerdoruesi, lloji, txtEmerTabKoka.Text, col.filtroFormatImportiSipasFushes("Numer").EmerImporti);
					break;
				case 17:
					table = colCmimeArtikujsh.ktheCmimArtikulliDtExport(IdNdermarrja);
					break;
				case 18:
					table = colZbritjetAnalitike.ktheZbritjeAnalitikeDtExport(IdNdermarrja);
					break;
				case 20:
					table = colVeprimeKFKoka.merrDokVeprimeKFPerEksport(IdNdermarrja, IdPerdoruesi, IdNdermarrjeVit, hfState["idQueryString"].ToString());
					break;
				case 21:
					table = colPerdoruesit.MerrSipasLicencesDTPerExport(IdPerdoruesi, clsLicenca.merrIdLicencePerdoruesi(IdPerdoruesi));
					break;
				case 23:
					table = colNjesiAdministrative.merrSipasNjesiNdermarrjesDTExport(IdNdermarrja);
					break;
				case 31:
					table = colPikaShitjeFurnizimi.merrSipasPikeNdermarrjesDTExport(IdNdermarrja);
					break;
				case 32:
					table = colDegeAdministrative.merrSipasDegeNdermarrjesDTExport(IdNdermarrja);
					break;
				case 37:
					table = colPunonjes.kthePunonjesNdermarrjesAndAutorizimeDTExport(IdNdermarrja);
					break;
				case 45:
					table = colKokaEkzekutim.merrEkzekutimePerEksport(IdNdermarrja, IdPerdoruesi, IdNdermarrjeVit, lloji, txtEmerTabKoka.Text, primaryKey.EmerImporti, hfState["idQueryString"].ToString());
					break;
				case 67:
					table = colKodifikimeArtikulli.merrKodifikimArtikulliExport(IdNdermarrja, lloji, txtEmerTabKoka.Text, col.filtroFormatImportiSipasFushes("Kodi").EmerImporti);
					break;
				case 71:
					table = colDetajimeArtikulli.merrDetajimePerEksport(IdNdermarrja);
					break;
				case 90:
					table = colAmortizimiKoka.ktheAmortizimFillestarPerEksport(IdNdermarrja, IdNdermarrjeVit, IdPerdoruesi, hfState["idQueryString"].ToString());
					break;
				case 92:
					var nrSerialBatchNo = clsFunksione.merrNumrinAutomatik(IdNdermarrja, Session);
					if (nrSerialBatchNo.Equals(""))
					{
						clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgCelNrAutomatike"], pnlMesazhi);
						return;
					}
					mySessionObjects.ruajNumrinAutomatikPerFlexCube(Session, nrSerialBatchNo);
					using (var admin = new clsDatabaseAdmin())
					{
						if (cmbFormati.Text == "Format Eksport Flex Cube")
							table = admin.merrTeDhenaPerExportFlexCubeAlphaBank(IdNdermarrja, nrSerialBatchNo, Convert.ToString(dtFiltriDates.Value), clsFunksione.teDrejtaGjitheDokSipasKomponentes(IdPerdoruesi, IdViti, IdNdermarrja, Komponente), IdPerdoruesi);
						else if (cmbFormati.Text == "Format Eksport Prepaid Expenses")
							table = admin.merrTeDhenaPerPrepaidExpensesAlphaBank(IdNdermarrja, nrSerialBatchNo, DateTime.Parse(Convert.ToString(dtFiltriDates.Value)));
					}
					break;
				case 93:
					table = colAseteNormaAmortizimi.ktheArtikujNormaAmortizimiDTExport(IdNdermarrja);
					break;
				case 98:///per list oraret tek formati ruhet data e fillimit dhe e mbarimit por ne file ruhen datat midis ketyre dy datave
					DateTime datefillimi;
					DateTime datembarimi;
					DateTime.TryParse(col.Find(x => x.KodKontrolli == "Fillim periudhe").VleraDefault, out datefillimi);//,arim daten e fillimit dhe te mbarimit
					DateTime.TryParse(col.Find(x => x.KodKontrolli == "Mbarim periudhe").VleraDefault, out datembarimi);
					col.Remove(col.Find(x => x.KodKontrolli == "Fillim periudhe"));//i heqim nga konfigurimi
					col.Remove(col.Find(x => x.KodKontrolli == "Mbarim periudhe"));
					int rendi = 5;
					for (var i = datefillimi; i <= datembarimi; i = i.AddDays(1))
					{
						var trdata = new clsTrupiFormatImporti
						{
							KodKontrolli = i.ToString("dddd, d MMMM yyyy", ci),
							EmerImporti = i.ToString("dddd, d MMMM yyyy", ci),
							Visible = true,
							Shfaq = true,
							Rendi = rendi
						}; //shtojme datat 1 nga 1 tek formati
						rendi++;
						col.Add(trdata);
					}
					///merren te dhenat nga databaza
					///duhet patur kujdes me sp sepse perdor pivot per te kthyer rreshtat ne kolona
					table = colListOrare.ktheGjitheListOrariExport(IdNdermarrja, datefillimi, datembarimi);
					break;
				case 99:
					table = colOreShtese.ktheGjitheOreShteseExport(IdNdermarrja);
					break;
				case 111:
					table = colKomponenteNr.ktheGjitheKomponenteNrExport(IdNdermarrja);
					break;
				case 112:
					table = colDiteLeje.ktheGjitheDiteLejeExport(IdNdermarrja);
					break;
				case 113:
					table = colKontrolliMjekesor.ktheGjitheKontrolliMjekesorExport(IdNdermarrja);
					break;
				case 114:
					table = colPagaShtesa.merrPagaDheShtesaPerExport(IdNdermarrja);
					break;
				case 115:
					table = colKomponenteListPagesePunonjesi.merrKomponenteListPagesePunonjesiPerExport(IdNdermarrja);
					break;
				case 117:
					table = colKarta.MerrKartaSipasNdermarrjesPerEksport(IdNdermarrja);
					break;
				case 133:
					table = colArtikulliPerberes.merrRecepturaPerExport(IdNdermarrja);
					break;
				case 135:
				case 136:
					table = colKokaInventarizim.merrDokInventarizimiPerEksport(IdNdermarrja, IdPerdoruesi, IdNdermarrjeVit, lloji, txtEmerTabKoka.Text, primaryKey.EmerImporti, idKategoria == 135 ? false : true, hfState["idQueryString"].ToString());
					var tt = new clsTrupiFormatImporti();
					tt.KodKontrolli = idKategoria == 135 ? "Seriali" : "Barkodi";
					tt.EmerImporti = idKategoria == 135 ? "Seriali" : "Barkodi";
					tt.Visible = false;
					col.Add(tt);
					break;
				case 138:
					table = colPunesim.kthePunesimNdermarrjesAndAutorizimeDTExport(IdNdermarrja);
					break;
				case 139:
					table = colQendraKostoPunonjes.ktheQendraKostoNdermarrjesAndAutorizimeDTExport(IdNdermarrja);
					break;
				case 147:
					table = colKlientMeKupon.MerrSipasNdermarrjesPerExport(IdNdermarrja);
					break;
				case 148:
					table = colKlientPerBazaar.MerrSipasNdermarrjesPerExport(IdNdermarrja);
					break;
				case 149:
					table = colNormaAmortizimiRezerva.ktheNormaperEkport(IdNdermarrja);
					break;
				case 150:
					table = colAmortizimiKoka.ktheAmortizimRezervaPerEksport(IdNdermarrja, IdNdermarrjeVit, hfState["idQueryString"].ToString());
					break;
				case 157:
					table = colQytetet.ktheQytetePerEksport(IdNdermarrja);
					break;
				case 163:
					table = colGrupeKF.KtheGrupimKlienteFurnitore(IdNdermarrja);
					break;
				case 164:
					table = colKodbare.ktheKodbarePerEksport(IdNdermarrja);
					break;
				case 170:
					table = ColBKokaBuxheti.ktheDokMiratimBuxhetiPerEksport(IdNdermarrja);
					break;
				case 172:
					int nrKol = col.Count();
					for (int i = 1; i <= 12; i++)
					{
						var muaji = ((Muajt)i).ToString();
						col.Add(new clsTrupiFormatImporti
						{
							KodKontrolli = muaji,
							EmerImporti = muaji,
							Visible = true,
							Shfaq = true,
							Rendi = nrKol++
						});
					}
					table = ColBKokaBuxheti.ktheDokAlokimBuxhetiPerEksport(IdNdermarrja);
					break;
				case 175:
				case 177:
				case 179:
				case 181:
					table = ColBKokaBuxheti.ktheDokumentBuxhetiPerEksport(IdNdermarrja, Convert.ToInt32(cmbKategoria.Value));
					break;
				case 155:
					table = colKategoriShpenzimi.MerrKategoriShpenzimiExport(IdNdermarrja);
					break;
			}

			foreach (DataColumn d in table.Columns)
				gvExport.Columns.Add(new GridViewDataColumn(d.ColumnName));

			if (idKategoria != 92 && idKategoria != 0)
				KonfiguroKolonaGridePasNgarkimit(sql, col, emraFushash, ref table, idKategoria);

			gvExport.KeyFieldName = emraFushash;

			DbCore.mySessionObjects.ruajGrideNeSession(Komponente, Session, (object)table);
			gvExport.DataSource = table;
			gvExport.DataBind();
			table.Dispose();
			status1.Value = "export";

			gvExport.Columns["#"].VisibleIndex = 0;
			gvExport.Columns["#"].Width = 20;
		}

		private void KonfiguroKolonaGridePasNgarkimit(bool sql, colTrupiFormatImporti col, string kodKontrolli, ref DataTable table, int idKategoria)
		{
			gvExport.VisibleColumns.ForEach(x => x.Visible = false);
			foreach (var trup in col)
			{
				try
				{
					gvExport.Columns[trup.KodKontrolli].VisibleIndex = trup.Rendi;
					((GridViewDataColumn)gvExport.Columns[trup.KodKontrolli]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
					gvExport.Columns[trup.KodKontrolli].Caption = trup.EmerImporti;
					gvExport.Columns[trup.KodKontrolli].Visible = trup.Shfaq && trup.Visible;
					if (sql && !(trup.Shfaq && trup.Visible))
					{
						if (trup.KodKontrolli != kodKontrolli &&
							(idKategoria.ToString().ContainsAnyIgnoreCase("5", "12", "13", "14", "37", "138", "139")
							|| (idKategoria.ToString().ContainsAnyIgnoreCase("1", "2", "3", "4", "6", "135", "136") && trup.FusheKokeApoTrupi != 3)
							|| (idKategoria == 45 && trup.FusheKokeApoTrupi != 3 && trup.FusheKokeApoTrupi != 5)))
						{
							foreach (DataRow dr in table.Rows)
							{
								dr[trup.KodKontrolli] = DBNull.Value;
							}
						}
					}
				}
				catch
				{
					continue;
				}


			}
		}

		/// <summary>
		/// fshin dokumentin
		/// </summary>
		/// <param name="sender">derguesi</param>
		/// <param name="e">argumentat</param>
		protected void ButtonOk_Click2(object sender, EventArgs e)
		{
			if (cmbEmer.SelectedIndex == -1 || cmbEmer.Text == "")
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgZgjidhKonfiguriminEksportit"], pnlMesazhi);
				return;
			}

			try
			{
				clsKonfigExporti.Fshi(int.Parse(cmbEmer.Value.ToString()), IdPerdoruesi);
			}
			catch (Exception ex)
			{
				LogManager.GetCurrentClassLogger().Error(ex.Message);
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nje gabim i papritur ka ndodhur!", pnlMesazhi);
				status1.Value = "false";
			}

			clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
			status1.Value = "true";
			cmbEmer.SelectedIndex = -1;
			ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimExporti(cmbEmer, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
			cmbEmer_pnlEmer.Update();
		}

		/// <summary>
		/// Ruan/Modifikon nje objekt clsKokaEkzekutim.
		/// </summary>
		private void RuajKonfigurim()
		{
			if (!Page.IsValid)
				return;

			try
			{
				var koka = KrijoKonfigurim();
				var tedrejtaInfo = new clsTeDrejtaRoli();
				tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);

				var idSuperKategori = clsKategoriNivelDok.mbushIDSuperKategoriNivDok(koka.Kategoria);

				if (cmbEmer.SelectedIndex == -1)
				{
					if (!tedrejtaInfo.DShtim)
					{
						clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
						status1.Value = "false";
						return;
					}

					koka.RuajKonfigurimExporti(idSuperKategori);
				}
				else
				{
					if (!tedrejtaInfo.DMod)
					{
						clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"],
							pnlMesazhi);
						status1.Value = "false";
						return;
					}

					koka.Id = int.Parse(cmbEmer.Value.ToString());
					koka.ModifikoKonfigurimExporti(new clsKonfigExporti(int.Parse(cmbEmer.Value.ToString())), idSuperKategori);
				}
			}
			catch (MyException myException)
			{
				LogManager.GetCurrentClassLogger().Error(myException.Message);
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myException.Message, pnlMesazhi, LoadingPanel);
				status1.Value = "false";
				return;
			}
			catch (Exception err)
			{
				LogManager.GetCurrentClassLogger().Error(err.Message);
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimRuajtje"], pnlMesazhi, LoadingPanel);
				status1.Value = "false";
				return;
			}

			clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
			ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimExporti(cmbEmer, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
			cmbEmer_pnlEmer.Update();
			status1.Value = "false";
			hfShtimModifikim.Value = "shtim";
		}

		/// <summary>
		/// Krijon nje objekt te tipit clsKokaEkzekutim 
		/// </summary>
		/// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
		private clsKonfigExporti KrijoKonfigurim()
		{
			int idkategori = 0, idfiltri = 0;
			if (cmbKategoria.Text != "")
			{
				idkategori = int.Parse(cmbKategoria.Value.ToString());
			}

			var kontrolli = KontrolloFormatSelektuar(false, int.Parse(cmbKategoria.Value.ToString()));
			if (!kontrolli.Item1.Status)
			{
				throw new MyException(kontrolli.Item1.PershkrimMesazhi);
			}

			if (cmbFiltri.Text != "")
			{
				int.TryParse(cmbFiltri.Value.ToString(), out idfiltri);
				if (idfiltri == 0)
				{
					throw new MyException(MessagesResource.Messages["msgImportKyFormatNukEkziston"]);
				}

				if (new clsFiltraExporti(idfiltri).Id == 0)
				{
					throw new MyException(MessagesResource.Messages["msgImportKyFilterNukEkziston"]);
				}
			}

			var llojEksporti = cmbLlojEksporti.Text == "File" ? 1 : 2;

			if (llojEksporti == 2)
			{
				txtEmerTabKoka.Text = txtEmerTabKoka.Text.Replace(" ", "");
				txtEmerTabTrupi.Text = txtEmerTabTrupi.Text.Replace(" ", "");
				txtEmerTabRec.Text = txtEmerTabRec.Text.Replace(" ", "");

				var mesazh = clsFunksione.kontrolloPerKaraktereSpeciale(txtEmerTabKoka.Text);

				if (!mesazh.Status)
				{
					mesazh.PershkrimMesazhi = "Tabela e kokes" + mesazh.PershkrimMesazhi;
					throw new MyException(mesazh.PershkrimMesazhi);
				}

				mesazh = clsFunksione.kontrolloPerKaraktereSpeciale(txtEmerTabTrupi.Text);

				if (!mesazh.Status)
				{
					if (cmbKategoria.Text == "Ekzekutim Prodhimi")
						mesazh.PershkrimMesazhi = "Tabela e produkteve" + mesazh.PershkrimMesazhi;
					else
						mesazh.PershkrimMesazhi = "Tabela e trupit" + mesazh.PershkrimMesazhi;

					throw new MyException(mesazh.PershkrimMesazhi);
				}

				if (cmbKategoria.Text == "Ekzekutim Prodhimi" && txtEmerTabRec.Text != "")
				{
					mesazh = clsFunksione.kontrolloPerKaraktereSpeciale(txtEmerTabRec.Text);
					if (!mesazh.Status)
					{
						mesazh.PershkrimMesazhi = "Tabela e recepturave" + mesazh.PershkrimMesazhi;
						throw new MyException(mesazh.PershkrimMesazhi);
					}
				}
			}

			return new clsKonfigExporti(0, cmbEmer.Text, rbTipi.Value.ToString(), idkategori, kontrolli.Item2, txtEmerSheet.Text, IdNdermarrja, IdPerdoruesi, 1, txtEmerSkedari.Text, idfiltri, Convert.ToDateTime(dtFiltriDates.Value), txtFormatDestinacion.Text, txtUrlDestinacion.Text, txtNdermDestinacion.Text, txtEmerTabKoka.Text, txtEmerTabTrupi.Text, llojEksporti, txtEmerTabRec.Text, cbMerrDokTeMod.Checked, cbMerrDokTeFshire.Checked);
		}

		private void MbushGrideNgaSesioni()
		{
			DataTable table;
			mySessionObjects.merrGrideNgaSessioni(Komponente, Session, out table);
			gvExport.DataSource = table;
			gvExport.DataBind();
			if (table != null && table.Rows.Count > 0 && cmbKategoria.Value != null)
				gvExport.KeyFieldName = KtheEmraFushashIdSipasKategorie(int.Parse(cmbKategoria.Value.ToString()));

			if (table?.Columns["Koordinata"] != null)
			{
				foreach (DataRow row in table.Rows)
				{
					if (row["Koordinata"] != DBNull.Value && !string.IsNullOrWhiteSpace((string)row["Koordinata"]) && ((string)row["Koordinata"]).Contains("POINT"))
						row["Koordinata"] = clsFunksione.formatoKoordinate((string)row["Koordinata"]);
				}
			}
		}

		protected void gvExport_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
		{
			if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "" && gvExport.FilterExpression == "")
			{
				NgarkoGride(cmbLlojEksporti.Text == "SQL", true);
				return;
			}
			DataTable table;
			mySessionObjects.merrGrideNgaSessioni(Komponente, Session, out table);
			gvExport.DataSource = table;
			gvExport.DataBind();
		}

		protected void gvExport_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
		{
			if (e.Parameters.Contains("Filter") || e.Parameters.Contains("NdryshoFiltrinSipasCombos"))
			{
				FiltroGride(int.Parse(e.Parameters.Split(';')[1]), e.Parameters.Contains("NdryshoFiltrinSipasCombos"));
				return;
			}

			if (e.Parameters == "pastro")
			{
				Pastrogride(true);
				return;
			}

			if (e.Parameters == "eksporto")
			{
				EksportoGride();
				return;
			}

			if (cmbLlojEksporti.Text == "SQL" && cmbKategoria.Value.ToString() == "0")
				return;

			var idKategoria = int.Parse(cmbKategoria.Value.ToString());
			var kontrolle = KontrolloFormatSelektuar(false, idKategoria);
			if (!kontrolle.Item1.Status)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolle.Item1.PershkrimMesazhi, pnlMesazhi);
				return;
			}

			var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(kontrolle.Item2);
			var emraFushash = KtheEmraFushashIdSipasKategorie(idKategoria);
			ShtoTrupIdNeCol(ref col, emraFushash);

			if (idKategoria == 98)
			{
				///per list orarin tek formati ruajme date fillimi dhe date mbarimi por duhet te shfaqen te gjitha datat
				DateTime datefillimi;
				DateTime datembarimi;
				DateTime.TryParse(col.Find(x => x.KodKontrolli == "Fillim periudhe").VleraDefault, out datefillimi);//marrim daten e fillimit dhe daten e mbarimit
				DateTime.TryParse(col.Find(x => x.KodKontrolli == "Mbarim periudhe").VleraDefault, out datembarimi);
				col.Remove(col.Find(x => x.KodKontrolli == "Fillim periudhe"));//i heqim nga konfigurimi
				col.Remove(col.Find(x => x.KodKontrolli == "Mbarim periudhe"));
				int rendi = 5;
				for (var i = datefillimi; i <= datembarimi; i = i.AddDays(1))
				{
					var trdata = new clsTrupiFormatImporti
					{
						KodKontrolli = i.ToString("dddd, d MMMM yyyy", ci),
						EmerImporti = i.ToString("dddd, d MMMM yyyy", ci),
						Visible = true,
						Shfaq = true,
						Rendi = rendi
					}; ///shtojme datat 1 nga 1 nga data e fillimit deri tek ajo e mbarimit
					rendi++;
					col.Add(trdata);
				}
			}

			if (idKategoria != 92)
			{
				var table = new DataTable();
				KonfiguroKolonaGridePasNgarkimit(false, col, emraFushash, ref table, idKategoria);
			}

			gvExport.Columns["#"].VisibleIndex = 0;
		}

		private static string KtheEmraFushashIdSipasKategorie(int idKategoria)
		{
			string kodKontrolli;
			switch (idKategoria)
			{
				case 0:
					kodKontrolli = "Nr";
					break;
				case 1:
				case 2:
					kodKontrolli = "IdRreshtiShitje";
					break;
				case 3:
				case 4:
					kodKontrolli = "IdRreshtiBanka";
					break;
				case 6:
					kodKontrolli = "IdRreshtiMagazina";
					break;
				case 7:
					kodKontrolli = "IdTrupiFatura";
					break;
				case 12:
					kodKontrolli = "IdKlientFurnitor";
					break;
				case 13:
					kodKontrolli = "IdArtikulli";
					break;
				case 14:
					kodKontrolli = "IdLlogari";
					break;
				case 17:
					kodKontrolli = "IdCmimArtikulli";
					break;
				case 18:
					kodKontrolli = "IdZbritjeAnalitike";
					break;
				case 20:
					kodKontrolli = "IdRreshtiTrupi";
					break;
				case 21:
					kodKontrolli = "IdPerdorues";
					break;
				case 23:
					kodKontrolli = "IdNjesiAdministrative";
					break;
				case 31:
					kodKontrolli = "IdPikeShitjeFurnizimi";
					break;
				case 32:
					kodKontrolli = "IdDegeAdministrative";
					break;
				case 37:
					kodKontrolli = "IdPunonjes";
					break;
				case 45:
					kodKontrolli = "IdReceptura";
					break;
				case 67:
					kodKontrolli = "IdKodifikimi";
					break;
				case 71:
					kodKontrolli = "IdDetajimi";
					break;
				case 90:
					kodKontrolli = "IdAmortizimi";
					break;
				case 92:
					kodKontrolli = "CURR_NO";
					break;
				case 93:
					kodKontrolli = "IdLidhjeArtikullLlojAmort";
					break;
				case 117:
					kodKontrolli = "IdKarta";
					break;
				case 135:
				case 136:
					kodKontrolli = "IdRreshtiInventarizim";
					break;
				case 138:
					kodKontrolli = "IdPunesim";
					break;
				case 147:
					kodKontrolli = "IdKlientKupon";
					break;
				case 148:
					kodKontrolli = "IdKlientPerBazaar";
					break;
				case 149:
					kodKontrolli = "IdNorma";
					break;
				case 150:
					kodKontrolli = "IdRivleresimi";
					break;
				case 157:
					kodKontrolli = "IdQyteti";
					break;
				case 163:
					kodKontrolli = "IdGrupi";
					break;
				case 164:
					kodKontrolli = "IdKodbari";
					break;
				case 175:
				case 177:
				case 181:
				case 179:
					kodKontrolli = "IdTrupiBuxheti";
					break;
				default:
					kodKontrolli = "Id";
					break;
			}
			return kodKontrolli;
		}

		private static void ShtoTrupIdNeCol(ref colTrupiFormatImporti col, string fusha)
		{
			col.Add(new clsTrupiFormatImporti
			{
				KodKontrolli = fusha,
				EmerImporti = "Id",
				Visible = false
			});
		}

		protected void gvExport_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
		{
			e.Properties["cpNoRows"] = gvExport.VisibleRowCount;
			e.Properties["cpNoPage"] = gvExport.PageIndex;
		}

		protected void gvExport_DataBound(object sender, EventArgs e)
		{
			if (gvExport.Columns["#"] != null) return;
			var check = new GridViewCommandColumn("#")
			{
				ShowSelectCheckbox = true,
				Width = Unit.Percentage(2)
			};
			gvExport.Settings.ShowFilterRow = true;
			gvExport.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
			gvExport.Settings.ShowFilterRowMenu = true;
			gvExport.Columns.Add(check);
			gvExport.SettingsBehavior.AllowSelectByRowClick = true;
			gvExport.SettingsBehavior.AllowFocusedRow = true;
		}

		protected void btnExporto_Click(object sender, EventArgs e)
		{
			EksportoGride();
		}

		protected void btnPastro_Click(object sender, EventArgs e)
		{
			Pastrogride(true);
		}

		private void Fshifilter()
		{
			if (cmbFiltri.SelectedIndex == -1)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGISKerkimiNotFilter"], pnlMesazhi);
				return;
			}

			var kontrolli = KontrolloFormatSelektuar(false, int.Parse(cmbKategoria.Value.ToString()));
			if (!kontrolli.Item1.Status)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolli.Item1.PershkrimMesazhi, pnlMesazhi);
				return;
			}
			var idfiltri = int.Parse(cmbFiltri.Value.ToString());

			if (clsFiltraExporti.kaVeprime(idfiltri))
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgFilteriKaVeprime"], pnlMesazhi);
				return;
			}
			var filtri = new clsFiltraExporti
			{
				Id = idfiltri,
				IdPerdoruesi = IdPerdoruesi
			};
			var mesazh = filtri.fshi();
			if (mesazh.Status)
			{
				clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFiltriUFshi"], pnlMesazhi);
				status1.Value = "filtri";
				cmbFiltri.Text = "";
				ConfigureAspxComboBox.KonfiguroComboBoxFiltra(cmbFiltri, kontrolli.Item2, IdNdermarrja);
				pnlfiltri.Update();
				gvExport.FilterExpression = "";
			}
			else
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
		}

		private void Ruajfilter()
		{
			if (cmbFiltri.Text == "")
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGISKerkimiAskFilterName"], pnlMesazhi);
				return;
			}
			if (cmbFormati.Text == "")
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgJepniFiltrinEksportit"], pnlMesazhi);
				return;
			}

			var kontrolli = KontrolloFormatSelektuar(false, int.Parse(cmbKategoria.Value.ToString()));
			if (!kontrolli.Item1.Status)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolli.Item1.PershkrimMesazhi, pnlMesazhi);
				return;
			}

			filterControlGvExport.FilterExpression = gvExport.FilterExpression;

			var filtri = new clsFiltraExporti(0, cmbFiltri.Text, kontrolli.Item2, IdPerdoruesi, IdNdermarrja, 1, gvExport.FilterExpression, filterControlGvExport.GetFilterExpressionForDataSet());
			var mesazh = filtri.ruaj();
			if (mesazh.Status)
			{
				clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgGISKerkimiRuajFilter"], pnlMesazhi);
				status1.Value = "filtri";
				ConfigureAspxComboBox.KonfiguroComboBoxFiltra(cmbFiltri, kontrolli.Item2, IdNdermarrja);
				pnlfiltri.Update();
				cmbFiltri.Text = "";
			}
			else
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
		}

		protected void btnRuaj_Click(object sender, EventArgs e)
		{
			Ruajfilter();
		}

		protected void btnFshi_Click(object sender, EventArgs e)
		{
			Fshifilter();
		}

		protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
		{
			var col = e.Column as GridViewDataColumn;
			if (e.RowType == GridViewRowType.Data && (col.FieldName == "BRANCH_CODE" || col.FieldName == "ACCOUNT_BRANCH"))
				e.TextValue = e.Value.ToString(); //behet kjo per rastin kur nje numer si 001 te mos eksportohet ne excel si 1 por sic eshte ne grid 001
			if (e.RowType == GridViewRowType.Data && (col.FieldName == "INITIATION_DATE" || col.FieldName == "VALUE_DATE"))
				e.TextValue = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
		}

		public static clsMesazh eksportoGriden(string sheetName, string emerFile, ASPxGridViewExporter ASPxGridViewExporter1, HttpSessionState Session, HttpResponse Response, ASPxGridView gvExport)
		{
			//@"c:\" + emerFile + DateTime.Now.ToFileTime() + ".xlsx"
			//string pathDir = @"C:\AlphaWebExport\";
			String filePathToOpen = "";
			DataTable dt = null;
			try
			{
				string pathDir = HttpContext.Current.Server.MapPath(null) + @"\AlphaWebExport\";
				DirectoryExtension.CreateDirIfNotExists(pathDir);
				filePathToOpen = pathDir + emerFile + "_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xlsx";
				FileStream stream = new FileStream(filePathToOpen, FileMode.OpenOrCreate);
				ASPxGridViewExporter1.WriteXlsx(stream);
				stream.Close();
				dt = clsFunksione.krijoDataTablePerMasterSheet(Session);
			}
			catch (Exception ex)
			{ return new clsMesazh(false, ex.Message); }
			clsFunksione.AddWorksheetToExcelWorkbook(dt, "master", sheetName, filePathToOpen, Response);
			return new clsMesazh(true);
		}
		protected void cmbFormati_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
		{
			if (IsCallback)
			{
				if (Request.Params["__CALLBACKID"].ToString().Contains("cmbFormati"))
				{
					ConfigureAspxComboBox.mbushComboFormat(cmbFormati, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(cmbKategoria.Value), e);
				}
			}
		}
		protected void cmbFormati_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
		{
			if (IsCallback)
			{
				if (Request.Params["__CALLBACKID"].ToString().Contains("cmbFormati"))
				{
					ConfigureAspxComboBox.mbushComboFormat(cmbFormati, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(cmbKategoria.Value), e);
				}
			}
		}
	}
}