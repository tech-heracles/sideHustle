using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;



using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaFormula : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerdoruesi;
            if (hfState.Count == 0)
            {
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
            }
            else
                idPerdoruesi = (int)hfState["idPerdoruesi"];
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }  
            int idNdermarrje;
            int idViti;
            int idGjuhe;
            if (!IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuhe = DbCore.mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idGjuhe", idGjuhe);
                percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuhe, idNdermarrje, "gvFormula", 1, "LupaFormula.aspx");
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaFormula.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                mbushGrideFormulash(idNdermarrje);
                konfiguroGride(idNdermarrje, idGjuhe, false);
            }
            else
            {
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idGjuhe = (int)hfState["idGjuhe"];                
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                {
                    percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                    clsToolbarConfig.mbushComboBoxFiltra(idGjuhe, idNdermarrje, "gvFormula",1, "LupaFormula.aspx");
                }
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvFormula")))
                {
                    mbushGrideFormulashNgaSesioni(idNdermarrje);
                    konfiguroGride(idNdermarrje, idGjuhe, false);
                }
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
        }

        private void mbushGrideFormulash(int idNdermarrje)
        {
            DataTable dt = DbCore.DbInventari.colFormulat.merrFormulatSipasNdermarrjes(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvFormula.DataSource = dt;
            gvFormula.DataBind();
            dt.Dispose();
        }

        private void mbushGrideFormulashNgaSesioni(int idNdermarrje)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushGrideFormulash(idNdermarrje);
            else
            {
                gvFormula.DataSource = tmpObject;
                gvFormula.DataBind();
            }
        }

        private void konfiguroGride(int idNdermarrje, int idGjuhe, bool visibleIndex)
        {            
            if (visibleIndex)
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuhe, idNdermarrje, gvFormula, "gvFormula", "LupaFormula.aspx");
            else 
                GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(idGjuhe, idNdermarrje, gvFormula, "gvFormula", "LupaFormula.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvFormula, "IdFormula");
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);
        }
        
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuhe"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaFormula.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }
        
        protected void gvFormula_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, (int)hfState["idViti"], "LupaFormula.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            
            string kodi = e.NewValues["KodFormula"].ToString();
            string pershkrimi = e.NewValues["PershkrimFormula"].ToString();
            int idFormula = 0;
            e.Cancel = true;
            gvFormula.CancelEdit();
            DbCore.DbInventari.clsFormula formulaRe = new DbCore.DbInventari.clsFormula(idFormula, kodi, pershkrimi, idNdermarrje, idPerdorues, 1);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = formulaRe.ruaj();
            if (!mesazh.Status == true)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
            mbushGrideFormulash(idNdermarrje);
        }

        protected void gvFormula_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {//funksioni qe theret javascriptin per kalimin e te dhenave nga grida tek textboxet e tabeve te tjera
            if (e.Editor.GetType().Name == "ASPxTextBox")
            {
                ASPxTextBox currentEditor = e.Editor as ASPxTextBox;
                currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextChanged('" + e.Column.FieldName + "',s.GetText());}";
                currentEditor.ClientInstanceName = e.Column.FieldName;
                if (e.Column == gvFormula.Columns["KodFormula"])
                {
                    if (hfRuaj.Value == "Modifiko")
                        e.Column.ReadOnly = true;
                }
            }
        }

        protected void gvFormula_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuhe"];
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvFormula.FilterExpression = "";
                else
                {

                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvFormula", "LupaFormula.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvFormula.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvFormula);
                    }
                }
            }
            konfiguroGride(idNdermarrje, idGjuha, false);
        }

        protected void gvFormula_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvFormula_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvFormula.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "")//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete bosh.";
                        return;
                    }
                }
            }
            int idNdermarrje = (int)hfState["idNdermarrje"];
            if (e.Keys.Count == 0) //shtim
            {
                if (DbCore.DbInventari.clsFormula.ekzistonFormuleSipasKodit(e.NewValues["KodFormula"].ToString(), idNdermarrje))
                {
                    e.RowError = "Ekziston nje formulë me këtë kod!";
                    return;
                }
            }
            string formula = e.NewValues["PershkrimFormula"].ToString();
            if (e.Keys.Count == 0 || (e.Keys.Count != 0 && e.NewValues["PershkrimFormula"].ToString() != e.OldValues["PershkrimFormula"].ToString()))
                if (DbCore.DbInventari.clsFormula.ekzistonFormuleSipasPershkrimit(formula, idNdermarrje))
                {
                    e.RowError = "Kjo formulë ekziston!";
                    return;
                }            
           
            formula = formula.Replace(" ", "");// heqim hapsirat nga formula
            string pattern = @"^[\*\+-\/]";//pattern per te kontrolluar qe formula fillon me operator aritmetik         

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(formula);
            if (!m.Success)
            {
                e.RowError = "Formula duhet të fillojë me një nga simbolet e mëposhtme: *, /, +, ose - !";
                return;
            }
            formula = "1" + formula; // shtojme nje numer perpara validimit per te kontrolluar nese formula eshte e vlefshme apo jo. kontrolli behet me ane te funksionit TestExpression(expression) 

            bool formVlefshme = TestExpression(formula); 
            if (!formVlefshme)
            {
                e.RowError = "Formula nuk është e vlefshme!";
                return;
            }
        }

        protected bool TestExpression(string expression)
        {
            try
            {
                double result = EvaluateExpression(expression);
                //Console.WriteLine("'" + expression + "' = " + result);
                return true;
            }
            catch (Exception err)
            {
                //Console.WriteLine("Expression is invalid: '" + expression + "'");
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return false;
            }
        }

        protected double EvaluateExpression(string expression)
        {
            string code = string.Format  // Note: Use "{{" to denote a single "{"
            (
                "public static class Func{{ public static double func(){{ return {0};}}}}",
                expression
            );
            CompilerResults compilerResults = CompileScript(code);
            if (compilerResults.Errors.HasErrors)
            {
                throw new InvalidOperationException("Expression has a syntax error.");
            } 
            Assembly assembly = compilerResults.CompiledAssembly;
            MethodInfo method = assembly.GetType("Func").GetMethod("func");
            return (double)method.Invoke(null, null);
        }
        
        protected CompilerResults CompileScript(string source)
        {
            CompilerParameters parms = new CompilerParameters();
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = false;
            CodeDomProvider compiler = CSharpCodeProvider.CreateProvider("CSharp");
            return compiler.CompileAssemblyFromSource(parms, source);
        }

        protected void gvFormula_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvFormula.IsNewRowEditing)
                {
                    gvFormula.DoRowValidation();
                }
        }

        protected void gvFormula_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (int)hfState["idViti"], "LupaFormula.aspx");
            if (!tedrejtaInfo.DMod)
            {   
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            int idNdemarrje = (int)hfState["idNdermarrje"];
            int idFormula = int.Parse(e.Keys["IdFormula"].ToString());
            DbCore.DbInventari.clsFormula formula = new DbCore.DbInventari.clsFormula(idFormula);
            formula.PershkrimFormula = e.NewValues["PershkrimFormula"].ToString();
            formula.IdNdermarrje = int.Parse(e.Keys["IdNdermarrje"].ToString());
            formula.IdPerdorues = (int)hfState["idPerdoruesi"];
            e.Cancel = true;            
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = formula.modifiko();
            if (!mesazh.Status == true)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
            gvFormula.CancelEdit();
            mbushGrideFormulash(idNdemarrje);
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNderm = (int)hfState["idNdermarrje"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuhe"], "gvFormula", "LupaFormula.aspx", (int)hfState["idNdermarrje"]);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, (int)hfState["idNdermarrje"], koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = (int)hfState["idPerdoruesi"];
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuhe"], idNderm, "gvFormula", 1, "LupaFormula.aspx");
                percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], idNderm, ASPxMenu1);
                if (mesazh.Status == true)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Green");
                else DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");

                cmbFiltra.Text = "";
                gvFormula.FilterExpression = String.Empty;
            }
        }
        
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            int idNderm = (int)hfState["idNdermarrje"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuhe"], "gvFormula", "LupaFormula.aspx", idNderm);
            filtri.GridaKokaId = koka.IdGridaKoka;

            filtri.FiltraVlera = gvFormula.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdFormula", gvFormula);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvFormula.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdFormula";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdNdermarje = idNderm;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField((int)hfState["idGjuhe"], idNderm, "gvFormula", "LupaFormula.aspx", "IdFiltra", "FiltraShenime", 1);
            clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuhe"], idNderm, "gvFormula", 1, "LupaFormula.aspx");
            percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], idNderm, ASPxMenu1);
            //if (mesazh.StatusMesazhi == true)
            //    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            //else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            if (mesazh.Status)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Green");
            else DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");

            cmbFiltra.Text = "";
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvFormula.FocusedRowIndex;
            gvFormula.Selection.SelectRow(a);
            List<object> rreshtat = gvFormula.GetSelectedFieldValues("IdFormula");            
            foreach (object id in rreshtat)
            {
                DbCore.DbInventari.clsFormula formula = new DbCore.DbInventari.clsFormula(int.Parse(id.ToString()));
                DbCore.clsMesazh mesazh = formula.fshi();
               if(mesazh.Status)
                   DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Green");
               else
                   DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
            }
            mbushGrideFormulash((int)hfState["idNdermarrje"]);
            pnlKryesor.Update();
        }
    }
}
