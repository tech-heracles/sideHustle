using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.SessionState;
using DbCore;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.Logging;
using DevExpress.Data;
using DevExpress.Web;
using DevExpress.Web.Data;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.Templates;

namespace PlatinumWeb.ApplicationUtils.ASPxControlExtensions
{
    public static class ASPxGridViewExtensions
    {
        /// <summary>
        /// konfiguron nje kolone qe mbart nje id ne nje kolon combobox ,psh idKonfig => kombo me konfigurimet.
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grida"></param>
        /// <param name="valueField"></param>
        /// <param name="descriptionField"></param>
        /// <param name="dataSource"></param>
        /// <param name="session"></param>
        /// <param name="komponente"></param>

        public static void KonfiguroCombo<T>(this ASPxGridView grida, string fieldName, string valueField, string textField, Func<T> funcDs, HttpSessionState session, string komponente, string guidString)
        {
            var sessionKey = SessionKeyUtils.MerrSessionKeyPerCmb(komponente, fieldName);
            var oldColumn = grida.Columns[fieldName];
            if (oldColumn == null) throw new MyException($"Kolona {fieldName} nuk gjendet ne griden {grida.ID} per komponentetn {komponente}");
            GridViewDataComboBoxColumn colNewCombo;
            T dataSource;
            if (typeof(GridViewDataComboBoxColumn) != oldColumn.GetType())
            {
                colNewCombo = GridUtil.KrijoGridViewDataComboBoxColumnSipasKolonesEkzistuese(oldColumn);
                grida.Columns.Remove(oldColumn);
                grida.Columns.Add(colNewCombo);

                colNewCombo.PropertiesComboBox.TextField = textField;
                colNewCombo.PropertiesComboBox.ValueField = valueField;
                colNewCombo.FieldName = fieldName;
                dataSource = funcDs.Invoke();
                colNewCombo.PropertiesComboBox.DataSource = dataSource;
                colNewCombo.PropertiesComboBox.AllowMouseWheel = true;
                colNewCombo.PropertiesComboBox.AllowNull = true;
                mySessionObjects.RuajNeSession(sessionKey, guidString, dataSource);
                return;
            }
            colNewCombo = ((GridViewDataComboBoxColumn)oldColumn);

            if (colNewCombo == null) throw new MyException($"Kolona {fieldName} ne griden {grida.ID} nuk mund te konvertohet ne GridViewDataComboBoxColumn");

            if (colNewCombo.PropertiesComboBox.DataSource != null) return;

            dataSource = mySessionObjects.MerrNgaSession<T>(session, sessionKey, guidString);
            if (dataSource == null)
            {
                dataSource = funcDs.Invoke();
                mySessionObjects.RuajNeSession(sessionKey, guidString, dataSource);
            }
            colNewCombo.PropertiesComboBox.DataSource = dataSource;
        }

        public static void KonfiguroComboMeItems(this ASPxGridView grid, string fieldName, Func<ListEditItemCollection> sourceItems)
        {
            var oldColumn = grid.Columns[fieldName];
            var colnew = GridUtil.KrijoGridViewDataComboBoxColumnSipasKolonesEkzistuese(oldColumn);
            if (typeof(GridViewDataComboBoxColumn) != oldColumn.GetType())
            {
                grid.Columns.Remove(oldColumn);
                grid.Columns.Add(colnew);
                colnew.PropertiesComboBox.Items.AddRange(sourceItems.Invoke());
                colnew.FieldName = fieldName;
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grid.Columns[fieldName];
                if (colnew.PropertiesComboBox.Items.Count != 0) return;
                colnew.PropertiesComboBox.Items.AddRange(sourceItems.Invoke());
            }
        }

        /// <summary>
        /// shton nje objekt clsMesazh i cili do lexohet ne endcallback te kesaj gride
        /// </summary>
        /// <param name="grida">grida te ciles do i shtohet si custom properties mesazhi</param>
        /// <param name="pergjigja">nje objekt clsMesazh</param>
        public static void ShtoMesazhBatchUpdate(this ASPxGridView grida, clsMesazh pergjigja)
        {
            grida.JSProperties["cpBatchUpdateMesazhi"] = JsonConvert.SerializeObject(pergjigja);
        }



        /// <summary>
        /// perdoret kur grid eshte ne callback
        /// </summary>
        /// <param name="grida"></param>
        /// <param name="pergjigja"></param>
        public static void ShtoMesazhNeGride(this ASPxGridView grida, clsMesazh pergjigja)
        {
            grida.JSProperties["cpMesazhNeGride"] = JsonConvert.SerializeObject(pergjigja);
        }

        public static void ShtoMesazhNeGride(this ASPxGridView grida, List<clsMesazh> pergjigja)
        {
            grida.JSProperties["cpMesazhetNeGride"] = JsonConvert.SerializeObject(pergjigja);
        }
        public static void ShtoObjectNeGride<T>(this ASPxGridView grida, T pergjigja, string key)
        {
            grida.JSProperties[key] = JsonConvert.SerializeObject(pergjigja);
        }

        public static void ShtoMesazhErrori(this ASPxGridView grida, string errorDescr)
        {
            ShtoMesazhNeGride(grida, new clsMesazh(false, errorDescr));
        }

        public static void ShtoMesazhErrori(this ASPxGridView grida, string errorDescr, Exception ex)
        {
            var mesazh = new clsMesazh(false, $"{errorDescr}{ex.Message}");
            ImbLogger.Error(mesazh.PershkrimMesazhi);
            ShtoMesazhNeGride(grida, mesazh);
        }


        public static void ShtoTotalSummary(this ASPxGridView grida, string formatString, SummaryItemType summaryType, params string[] fushat)
        {
            for (int i = 0; i < fushat.Length; i++)
            {


                if (grida.TotalSummary.Find(x => x.FieldName == fushat[i]) == null)
                {
                    var summaryItem = new ASPxSummaryItem(fushat[i], summaryType)
                    {
                        DisplayFormat = String.IsNullOrEmpty(formatString) ? "{0}" : "{0:" + formatString + "}",
                        ValueDisplayFormat = formatString
                    };

                    grida.TotalSummary.Add(summaryItem);
                }
            }
        }
        /// <summary>
        /// update-on objektin qe kalohet si parameter,ne baze te rreshtit te grides
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateValues"></param>
        /// <param name="oldObj"></param>
        /// <returns></returns>
        public static bool UpdateCustomObjectByRow<T>(this ASPxDataUpdateValues updateValues, T oldObj)
        {
            // Type temp =typeof(T) ;
            //  T obj = New<T>.Instance();
            PropertyInfo[] properties = typeof(T).GetProperties();
            //  obj = oldObj;///me qellim qe te kopjohen gjithe vlerat e vjetra

            object[] keys = new object[updateValues.NewValues.Keys.Count];
            updateValues.NewValues.Keys.CopyTo(keys, 0);

            foreach (var key in keys)
            {
                var pro = properties.FirstOrDefault(x => x.Name.Equals(key.ToString(), StringComparison.OrdinalIgnoreCase));
                if (pro == null)
                    continue;
                if (updateValues.NewValues[key] is DBNull)
                    continue;
                if (pro.CanWrite)
                    pro.SetValue(oldObj, Convert.ChangeType(updateValues.NewValues[key], pro.PropertyType));
            }
            return true;
        }

        /// <summary>
        /// ben merge te objektit me rreshtin e updatuar te grides
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateValues"></param>
        /// <param name="oldObj"></param>
        /// <returns></returns>
        public static T MerrCustomUpdatedObject<T>(this ASPxDataUpdateValues updateValues, T oldObj)
        {
            // Type temp =typeof(T) ;
            //  T obj = New<T>.Instance();
            PropertyInfo[] properties = typeof(T).GetProperties();
            //  obj = oldObj;///me qellim qe te kopjohen gjithe vlerat e vjetra

            object[] keys = new object[updateValues.NewValues.Keys.Count];
            updateValues.NewValues.Keys.CopyTo(keys, 0);

            foreach (var key in keys)
            {
                var pro = properties.FirstOrDefault(x => x.Name.Equals(key.ToString(), StringComparison.OrdinalIgnoreCase));
                if (pro == null)
                    continue;
                if (updateValues.NewValues[key] == null || updateValues.NewValues[key] is DBNull)
                    continue;
                if (pro.CanWrite)
                    pro.SetValue(oldObj, Convert.ChangeType(updateValues.NewValues[key], pro.PropertyType));
            }
            return oldObj;
        }

        /// <summary>
        /// ben merge te objektit me rreshtin e ri te grides,i kalohet me qellim nje objekt per te mos u krijuar me reflection sepse kushton
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertedValues"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MerrCustomInsertedObject<T>(this ASPxDataInsertValues insertedValues, T obj)
        {
            //Type temp = typeof(T);

            //T obj = New<T>.Instance();//me mire ta marrim si parameter sepse behet instancimi me shpejt
            PropertyInfo[] properties = typeof(T).GetProperties();
            object[] keys = new object[insertedValues.NewValues.Keys.Count];
            insertedValues.NewValues.Keys.CopyTo(keys, 0);

            foreach (var key in keys)
            {
                var pro = properties.FirstOrDefault(x => x.Name.Equals(key.ToString(), StringComparison.OrdinalIgnoreCase));
                if (pro == null)
                    continue;
                if (insertedValues.NewValues[key] == null || insertedValues.NewValues[key] is DBNull)
                    continue;
                pro.SetValue(obj, Convert.ChangeType(insertedValues.NewValues[key], pro.PropertyType));
            }
            return obj;
        }

        public static T MerrKeyValue<T>(this ASPxDataUpdateValues updatedRow)
        {
            if (updatedRow.Keys.Count > 1)
                throw new NotImplementedException("nuk eshte implementuar rasti me disa keys");
            return (T)updatedRow.Keys[0];
        }

        public static T MerrKeyValue<T>(this ASPxDataDeleteValues deletedRow)
        {
            if (deletedRow.Keys.Count > 1)
                throw new NotImplementedException("nuk eshte implementuar rasti me disa keys");
            return (T)deletedRow.Keys[0]; //, typeof(T));
        }

        /// <summary>
        /// merr nje array me strings nga rreshti
        /// </summary>
        /// <param name="grida"></param>
        /// <param name="fushat"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string[] MerrVleratERreshtit(this ASPxGridView grida, int index, params string[] fushat)
        {
            string[] vlerat = new string[fushat.Length];
            object[] rowValues = grida.GetRowValues(index, fushat) as object[];
            for (int i = 0, count = fushat.Length; i < count; i++)
            {
                if (!Convert.IsDBNull(rowValues[i]))
                    vlerat[i] = rowValues[i].ToString();
                else
                    vlerat[i] = null;
            }
            return vlerat;
        }

        /// <summary>
        /// heq nga sessioni datasource e kesaj gride per te gjitha periudhat
        /// </summary>
        /// <param name="grida"></param>
        /// <param name="emerKomponente"></param>
        /// <param name="idViti"></param>
        /// <param name="sessioni"></param>
        public static void PastroDataSourceNgaSessioni(this ASPxGridView grida, string emerKomponente, int idViti, HttpSessionState sessioni)
        {
            foreach (var periudhe in Enum.GetNames(typeof(LlojPeriudhe)))
            {
                mySessionObjects.ruajGrideNeSession(emerKomponente, idViti, periudhe, sessioni, null);
            }
        }
        public static void RuajDataSourceMePeriduheNeSession<TDataSource>(this ASPxGridView grida, HttpSessionState sessioni, string komponente,TitlePeriudha periudha, TDataSource dataSource, string guidString)
        {
            mySessionObjects.RuajNeSession<TDataSource>(SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, periudha.IdViti, periudha.PeriudhaDok), guidString, dataSource);
        }
        //vendosur dhe konfigurimi per disa lista (shitja per momentin)
        public static void RuajDataSourceMePeriduheNeSession<TDataSource>(this ASPxGridView grida, HttpSessionState sessioni, string komponente,  TitlePeriudha periudha, string kodKonfigambjente, TDataSource dataSource, string guidString)
        {
            mySessionObjects.RuajNeSession<TDataSource>(SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, periudha.IdViti, periudha.PeriudhaDok,kodKonfigambjente), guidString, dataSource);
        }
        public static TDataSource MerrDataSourceMePeriduheNeSession<TDataSource>(this ASPxGridView grida, HttpSessionState sessioni, string komponente, TitlePeriudha periudha, string guidString)
        {
            return mySessionObjects.MerrNgaSession<TDataSource>(sessioni, SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, periudha.IdViti, periudha.PeriudhaDok), guidString);
        }
        //shtuar per listat qe kan dhe filter periudha dhe filter topRows
        public static void RuajDataSourceMePeriudheDheTopRowsNeSession<TDataSource>(this ASPxGridView grida, HttpSessionState sessioni, string komponente, TitlePeriudha periudha, TDataSource dataSource, string guidString)
        {
            mySessionObjects.RuajNeSession<TDataSource>(SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, periudha.IdViti, periudha.PeriudhaDok, periudha.TopRowsControl.TopRows), guidString, dataSource);
        }
        public static TDataSource MerrDataSourceMePeriudheDheTopRowsNeSession<TDataSource>(this ASPxGridView grida, HttpSessionState sessioni, string komponente, TitlePeriudha periudha, string guidString)
        {
            return mySessionObjects.MerrNgaSession<TDataSource>(sessioni, SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, periudha.IdViti, periudha.PeriudhaDok, periudha.TopRowsControl.TopRows), guidString);
        }
        //vendosur dhe konfigurimi per disa lista (shitja per momentin)
        public static TDataSource MerrDataSourceMePeriduheNeSession<TDataSource>(this ASPxGridView grida, HttpSessionState sessioni, string komponente, TitlePeriudha periudha, string kodKonfigambjente, string guidString)
        {
            return mySessionObjects.MerrNgaSession<TDataSource>(sessioni, SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, periudha.IdViti, periudha.PeriudhaDok, kodKonfigambjente), guidString);
        }

        /// <summary>
        /// per dt e ruajtur ne session per te gjitha periudhat e kesaj gride hiqen rreshtat e dhene
        /// </summary>
        /// <param name="grida"></param>
        /// <param name="emerKomponente"></param>
        /// <param name="idViti"></param>
        /// <param name="session"></param>
        /// <param name="drs"></param>
        public static void HiqRreshtaNgaSessioniPerGjithePeriudhat(this ASPxGridView grida, string emerKomponente, int idViti, HttpSessionState session, int id, string guidString)
        {
           GridUtil.HiqRreshtaNgaDataSourceGridesNeSession(emerKomponente, guidString, idViti, session, grida.KeyFieldName, id);
        }
        public static void HiqRreshtaTeFshireNgaSessioni(this ASPxGridView grida, string emerKomponente, int idViti, HttpSessionState session, int ID, string guidString) {

        }
        public static void PercaktoTemplateGroupSummaryFooter(this ASPxGridView grida, string formatString, params string[] fushat)
        {
            foreach (string fusha in fushat)
                grida.Columns[fusha].GroupFooterTemplate = new MySummaryGroupFooterTemplate(fusha, formatString);
        }


        public static void PercaktoEmerPerKolonen(this ASPxGridView grida, string caption, string kolona)
        {
            grida.AllColumns[kolona].Caption = caption;
        }
        public static void ShtoGroupSummary(this ASPxGridView grida, string formatString, DevExpress.Data.SummaryItemType summaryType = DevExpress.Data.SummaryItemType.Sum, params string[] fushat)
        {
            foreach (string fusha in fushat)
            {
                var summaryItem = new ASPxSummaryItem(fusha, summaryType)
                {
                    DisplayFormat = String.IsNullOrEmpty(formatString) ? "{0}" : "{0:" + formatString + "}",
                    ValueDisplayFormat = formatString,
                    ShowInGroupFooterColumn = fusha
                };
                summaryItem.Visible = false;
                if (grida.GroupSummary.Find(x => x.FieldName == summaryItem.FieldName) == null)
                    grida.GroupSummary.Add(summaryItem);
            }
        }

        public static void MbushGride(this ASPxGridView grida ,object objekt, HttpSessionState Session, string guidString, string komponente)
        {
            //mySessionObjects.RuajNeSession<DateTime>(Session, DateTime.Now, komponente + "_" + grida.ClientInstanceName, guidString);
            grida.DataSource = objekt;
            grida.DataBind();
        }


        public static bool AplikoFilterDefault(this ASPxGridView grida, ASPxGridViewCustomCallbackEventArgs e, int idKonfig)
        {
            if(e.Parameters == GridUtil.APLIKOFILTERDEFAULT)
            {
                GridUtil.AplikoFilterDefault(grida, idKonfig);
                return true;
            }
            return false;
        }
        public static void SaveFilter(this ASPxGridView grida, int idNdermarrje, string ambjenti)
        {
            mySessionObjects.SaveFilter(idNdermarrje, grida.FilterExpression, grida.ID, ambjenti);
        }

        public static void SaveFilter(this ASPxGridView grida, int idNdermarrje)
        {
            mySessionObjects.SaveFilter(idNdermarrje, grida.FilterExpression, grida.ID, "");
        }
        public static void RestoreFilter(this ASPxGridView grida, int idNdermarrje)
        {
            grida.RestoreFilter(idNdermarrje,"", string.Empty);
        }

        public static void RestoreFilter(this ASPxGridView grida, int idNdermarrje, string ambjenti, string filterDefault)
        {
            string filterExpression = mySessionObjects.RestoreFilter(idNdermarrje, grida.ID, ambjenti);
            grida.FilterExpression = filterExpression != null ? filterExpression : filterDefault;
        }
    }
}