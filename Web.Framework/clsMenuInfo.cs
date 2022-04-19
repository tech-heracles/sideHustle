using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{    ///per te shtuar informacionin duhet te shtoni menune tek faqja aspx 
     ///te therisni metoden shtoMenuItemInfo per ta mbushur menune
     ///per te shfaqur nje mesazh perdorni metodat shtoMesazhGabimi kur mesazhi eshte per gabim dhe shtoMesazhSuksesi kur mesazh eshte per suksesin e kryerjes se veprimit
     ///ne pjesen e javascriptit duhet te shtohet endRequestTimeri per te aktivizuar timerin pasi shfaqet mesazhi.
    public class clsMenuInfo
    {
        /// <summary>
        /// ne menune kryesore krijohet dhe shtohet nje item me informacion
        ///<param name="page"></param>
        /// <param name="m"></param>
        /// <param name="emriGrida"></param>
        public static void ShtoMenuItemInfo(Page page, ASPxMenu m, EventHandler poHandler, EventHandler joHandler)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemInfo");
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemInfo");
            Control itemTemplate = page.LoadControl("MenuInfo.ascx");
            //itemTemplate.me
            item.BeginGroup = true;
            item.Template = itemTemplate as ITemplate;
            ASPxButton btnPo = ((UserControl)(item.Template)).FindControl("btnPo") as ASPxButton;
            btnPo.Click += poHandler;
            //btnPo.ClientVisible = false;
            ASPxButton btnJo = ((UserControl)(item.Template)).FindControl("btnJo") as ASPxButton;
            btnJo.Click += joHandler;
            // btnJo.ClientVisible = false;
        }

        public static void ShtoMenuItemInfo(Page page, ASPxMenu m)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemInfo");
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemInfo");
            Control itemTemplate = page.LoadControl("MenuInfo.ascx");
            //itemTemplate.me
            item.BeginGroup = true;
            item.Template = itemTemplate as ITemplate;
            ASPxButton btnPo = ((UserControl)(item.Template)).FindControl("btnPo") as ASPxButton;
            btnPo.ClientVisible = false;
            ASPxButton btnJo = ((UserControl)(item.Template)).FindControl("btnJo") as ASPxButton;
            btnJo.ClientVisible = false;
        }

        public static void ShtoMesazhGabimi(ASPxMenu m, String mesazh, UpdatePanel pnlMensazhi, DevExpress.Web.ASPxLoadingPanel loadPanel)
        {
            ShtoMesazhGabimi(m, mesazh, pnlMensazhi);
            loadPanel.Visible = false;
        }
        public static void ShtoMesazh(ASPxMenu m, DbCore.clsMesazh mesazhi, UpdatePanel pnlMensazhi)
        {
            switch (mesazhi.Tipi)
            {
                case DbCore.TipMesazhi.Gabim:
                    ShtoMesazhGabimi(m, mesazhi.PershkrimMesazhi, pnlMensazhi);
                    return;
                case DbCore.TipMesazhi.Sukses:
                    ShtoMesazhSuksesi(m, mesazhi.PershkrimMesazhi, pnlMensazhi);
                    return;
                case DbCore.TipMesazhi.Informim:
                    ShtoMesazhInformues(m, mesazhi.PershkrimMesazhi, pnlMensazhi);
                    return;
                case DbCore.TipMesazhi.undefined:
                    throw new DbCore.MyException("Unknown tip mesazhi");
            }
        }
        
        public static void ShtoMesazhGabimi(ASPxMenu m, String mesazh, UpdatePanel pnlMesazhi)
        {
            shtoMesazh("error", mesazh, pnlMesazhi);
            pnlMesazhi.Update();
            return;
        }
        public static void ShtoMesazhGabimi(ASPxMenu m, List<String> mesazh, UpdatePanel pnlMesazhi)
        {
            shtoMesazh("error", mesazh, pnlMesazhi);
            pnlMesazhi.Update();
            return;
        }
        public static void ShtoMesazhGabimi(Action<string> logger, ASPxMenu m, String mesazh, UpdatePanel pnlMesazhi)
        {
            logger(mesazh);
            shtoMesazh("error", mesazh, pnlMesazhi);
            pnlMesazhi.Update();
            return;
        }
        public static void ShtoMesazhSuksesi(ASPxMenu m, String mesazh, UpdatePanel pnlMesazhi)
        {
            shtoMesazh("success", mesazh, pnlMesazhi);
            pnlMesazhi.Update();
            return;
            
        }

        public static void ShtoMesazhInformues(ASPxMenu m, String mesazh, UpdatePanel pnlMesazhi)
        {
            shtoMesazh("information", mesazh, pnlMesazhi);
            pnlMesazhi.Update();
            return;           
        }

        public static string MerrMesazhin(ASPxMenu m)
        {
            DevExpress.Web.MenuItem itemButtonInfo = m.Items.FindByName("TemplatedItemInfo");
            //ASPxLabel hl = ((UserControl)(itemButtonInfo.Template)).FindControl("mesazhPage") as ASPxLabel;
            //return hl.Text;
            ASPxComboBox combo = ((UserControl)(itemButtonInfo.Template)).FindControl("mesazhList") as ASPxComboBox;
            return combo.SelectedItem.Text;
        }
      
        public static void ShtoPyetje(ASPxMenu m, string pyetje, UpdatePanel pnlMesazhi, int idGjuha, bool server = true)
        {
            shtoMesazh("confirm", pyetje, server, pnlMesazhi, idGjuha);
            pnlMesazhi.Update();
            return;
        }

        private static Int32 getIntOfDate(DateTime date)
        {
            return date.Day * (Int32)Math.Pow(10, 6) + date.Hour * (Int32)Math.Pow(10, 4) + date.Minute * (Int32)Math.Pow(10, 2) + date.Second;
        }

        private static string appendStringOfTime(DateTime date, String mesazh)
        {
            return date.Hour + ":" + date.Minute + ":" + date.Second + " - " + mesazh;
        }
        private static void shtoMesazh(string type, string mesazh, UpdatePanel pnlMesazhi)
        {
            shtoMesazh(type,mesazh,false,pnlMesazhi);
        }
        private static void shtoMesazh(string type, List<string> mesazh, UpdatePanel pnlMesazhi)
        {
            shtoMesazh(type, mesazh, false, pnlMesazhi);
        }
        private static void shtoMesazh(string type, string mesazh, bool serverSide, UpdatePanel pnlMesazhi, int idGjuha = 0)
        {
            string mesazhFormServerID = "mesazhFromServer";
            var mesazhFromServer = pnlMesazhi.FindControl(mesazhFormServerID) as System.Web.UI.HtmlControls.HtmlInputHidden;
            if (mesazhFromServer == null)
            {
                mesazhFromServer = new System.Web.UI.HtmlControls.HtmlInputHidden();
                mesazhFromServer.ID = mesazhFormServerID;
                pnlMesazhi.ContentTemplateContainer.Controls.Add(mesazhFromServer);
            }
            List<string> myMsgList = new List<string>();
            if (mesazhFromServer.Value != "")
            {
                mesazhFromServer.Value = mesazhFromServer.Value.Replace("[", "").Replace("]", "");
                myMsgList = mesazhFromServer.Value.Split(',').ToList();
            }                         
            myMsgList.Add(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                type = type,
                text = mesazh,
                serverSide = serverSide,
                idGjuha = idGjuha
            }));
            mesazhFromServer.Value = "[" + String.Join(",",myMsgList) + "]";
        }
        private static void shtoMesazh(string type, List<string> mesazhe, bool serverSide, UpdatePanel pnlMesazhi, int idGjuha = 0)
        {
            foreach (string m in mesazhe)
            {
                shtoMesazh(type, m, serverSide, pnlMesazhi, idGjuha);
            }
        }
    }
}