using System;
using System.Globalization;
using System.Resources;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DevExpress.Web.ASPxTreeList;

namespace PlatinumWeb.ApplicationUtils.ASPxControlUtils
{
    public static class TreeListUtil
    {

        public static void KonfiguroCombo_TreeListe<T>(this ASPxTreeList grida, string fieldName, string valueField, string textField, Func<T> funcDs, HttpSessionState session, string komponente, string guidString)
        {
            var sessionKey = SessionKeyUtils.MerrSessionKeyPerCmb(komponente, fieldName);
            var oldColumn = grida.Columns[fieldName];
            if (oldColumn == null) throw new MyException($"Kolona {fieldName} nuk gjendet ne griden {grida.ID} per komponentetn {komponente}");
            TreeListComboBoxColumn colNewCombo;
            T dataSource;
            if (typeof(TreeListComboBoxColumn) != oldColumn.GetType())
            {
                colNewCombo = KrijoTreeListComboBoxColumnSipasKolonesEkzistuese(oldColumn);
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
            colNewCombo = ((TreeListComboBoxColumn)oldColumn);

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



        public static TreeListComboBoxColumn KrijoTreeListComboBoxColumnSipasKolonesEkzistuese(TreeListColumn oldColumn)
        {
            return new TreeListComboBoxColumn
            {
                HeaderCaptionTemplate = oldColumn.HeaderCaptionTemplate,
                Caption = oldColumn.Caption,
                Name = oldColumn.Name,
                Index = oldColumn.Index,
                VisibleIndex = oldColumn.VisibleIndex,
                Width = oldColumn.Width,
                //AllowTextTruncationInAdaptiveMode = oldColumn.AllowTextTruncationInAdaptiveMode,
                Visible = oldColumn.Visible,
                ToolTip = oldColumn.ToolTip
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="KeyFieldName"></param>
        /// <param name="parentFieldName"></param>
        public static void konfiguroTreeListeEvogelPaTheme(ASPxTreeList tree, String KeyFieldName, string parentFieldName, bool allowSelection)
        {
            tree.KeyFieldName = KeyFieldName;
            tree.SettingsPager.PageSize = 20;
            tree.Settings.ShowFilterRow = true;
            tree.Settings.ShowFilterRowMenu = true;
            tree.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;
            tree.SettingsBehavior.AllowFocusedNode = true;
            tree.SettingsBehavior.AllowDragDrop = true;
            tree.SettingsBehavior.AllowSort = true;
            tree.SettingsBehavior.AutoExpandAllNodes = true;
            tree.SettingsBehavior.ExpandCollapseAction = TreeListExpandCollapseAction.Button;
            tree.SettingsEditing.AllowNodeDragDrop = allowSelection;
            tree.SettingsSelection.Enabled = allowSelection;
            tree.SettingsSelection.Recursive = allowSelection;
            tree.ParentFieldName = parentFieldName;
            tree.SettingsEditing.Mode = TreeListEditMode.EditForm;


            TreeListCommandColumn commandCol;
            if (tree.Columns["Action"] == null)
            {
                commandCol = new TreeListCommandColumn("Action");
            }
            else commandCol = tree.Columns["Action"] as TreeListCommandColumn;

            commandCol.Name = "Action";
            commandCol.Visible = false;
            commandCol.ButtonType = ButtonType.Image;
            commandCol.CancelButton.Image.Url = "~/images/cancel1.png";
            commandCol.UpdateButton.Image.Url = "~/images/save_green.png";
            commandCol.UpdateButton.Image.Height = 20;
            commandCol.UpdateButton.Image.Width = 20;
            commandCol.CancelButton.Image.Height = 20;
            commandCol.CancelButton.Image.Width = 20;
            commandCol.CancelButton.Text = MessagesResource.Messages["labelAnullo"];
            commandCol.UpdateButton.Text = MessagesResource.Messages["buttonRuaj"];
            commandCol.UpdateButton.Image.AlternateText = MessagesResource.Messages["buttonRuaj"];
            commandCol.CancelButton.Image.AlternateText = MessagesResource.Messages["labelAnullo"];
            commandCol.UpdateButton.Image.ToolTip = MessagesResource.Messages["buttonRuaj"];
            commandCol.CancelButton.Image.ToolTip = MessagesResource.Messages["labelAnullo"];
            commandCol.Width = 10;
            if (tree.Columns["Action"] == null)
                tree.Columns.Add(commandCol);
            tree.Settings.ShowRoot = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="tree"></param>
        /// <param name="emriTree"></param>
        /// <param name="emriKomponentes"></param>
        public static void percaktoVisibleColumns(int idGjuha, int idNdermarrje, ASPxTreeList tree, String emriTree, String emriKomponentes)
        {
            clsGridaKoka koka = new clsGridaKoka(idGjuha, emriTree, emriKomponentes, idNdermarrje);
            if (koka.IdGridaKoka == 0)
            {
                ImbLogger.Error(String.Format("percaktoVisibleColumns({0}, {1}, {2}, {3}, {4}) - koka.IdGridaKoka == 0", idGjuha, idNdermarrje, tree, emriTree, emriKomponentes));
                return;
            }
            koka.OColGridaTrupi.mbushTrupin(idGjuha, koka.IdGridaKoka);
            foreach (clsGridaTrupi gridaKolone in koka.OColGridaTrupi)
            {
                var treeListColumn = tree.Columns[gridaKolone.KodiTrupi];
                if (treeListColumn == null)
                {
                    ImbLogger.Error(String.Format("percaktoVisibleColumns({0}, {1}, {2}, {3}, {4}) - treeListColumn == null", idGjuha, idNdermarrje, tree, emriTree, emriKomponentes));
                    return;
                }
                treeListColumn.Caption = gridaKolone.PershkrimiTrupi;
                treeListColumn.VisibleIndex = gridaKolone.IndexTrupi;
                treeListColumn.Visible = gridaKolone.VisibleTrupi;
                TreeListDataColumn col = treeListColumn as TreeListDataColumn;
                col.ReadOnly = gridaKolone.ReadonlyTrupi;
                col.PropertiesEdit.ClientInstanceName = gridaKolone.KodiTrupi;
                col.Width = Unit.Percentage(gridaKolone.WidthTrupi);
            }
        }

        public static void percaktoVisibleColumnsKonf(int idGjuha, int idNdermarrje, ASPxTreeList tree, string paramKodKonfigurimi, string paramIdKomponente)
        {
            string idkomponente = "";
            int idKonfigurim = -1;

            if (paramKodKonfigurimi != "")
            {
                idkomponente = paramIdKomponente;
                string kodkonfigurimi = paramKodKonfigurimi;
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(kodkonfigurimi, idNdermarrje, idGjuha);
                //DbCore.DbShare.colKonfigurimAmbjenti colKonf = share.ktheKonfiguriminMeKod(kodkonfigurimi, DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
                idKonfigurim = clsKonf.IdKonfigAmbjente;
            }
            else
            {
                idkomponente = paramIdKomponente;
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(int.Parse(idkomponente), idNdermarrje);
                //DbCore.DbShare.colKonfigurimAmbjenti colKonf = share.ktheKonfigDefaultKomponentes(int.Parse(idkomponente), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                idKonfigurim = clsKonf.IdKonfigAmbjente;
            }
            colGridaTrupi colGrida = new colGridaTrupi(idKonfigurim, idGjuha);
            foreach (clsGridaTrupi gridaKolone in colGrida)
            {
                var treeListColumn = tree.Columns[gridaKolone.KodiTrupi];
                if (treeListColumn == null)
                {
                    ImbLogger.Error(String.Format("percaktoVisibleColumnsKonf({0}, {1}, {2}, {3}, {4}) - treeListColumn == null", idGjuha, idNdermarrje, tree, paramKodKonfigurimi, paramIdKomponente));
                    return;
                }
                treeListColumn.Caption = gridaKolone.PershkrimiTrupi;
                treeListColumn.VisibleIndex = gridaKolone.IndexTrupi;
                treeListColumn.Visible = gridaKolone.VisibleTrupi;
                TreeListDataColumn col = treeListColumn as TreeListDataColumn;
                col.ReadOnly = gridaKolone.ReadonlyTrupi;
                col.PropertiesEdit.ClientInstanceName = gridaKolone.KodiTrupi;
                col.Width = Unit.Percentage(gridaKolone.WidthTrupi);
            }
        }
    }

}