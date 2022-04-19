using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Web.UI.WebControls;

namespace PlatinumWeb.E_PaySlip
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
            if (item!=null)
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemInfo");
            Control itemTemplate = page.LoadControl("MenuInfo.ascx");
            //itemTemplate.me
            item.BeginGroup = true;
            item.Template = itemTemplate as ITemplate;
            ASPxButton btnPo = ((PlatinumWeb.E_PaySlip.MenuInfo)(item.Template)).FindControl("btnPo") as ASPxButton;
            btnPo.Click += poHandler;
            //btnPo.ClientVisible = false;
            ASPxButton btnJo = ((PlatinumWeb.E_PaySlip.MenuInfo)(item.Template)).FindControl("btnJo") as ASPxButton;
            btnJo.Click += joHandler;
            // btnJo.ClientVisible = false;
        }

        public static void ShtoMenuItemInfo(Page page, ASPxMenu m)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemInfo");
            if (item != null)
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemInfo");
            Control itemTemplate = page.LoadControl("MenuInfo.ascx");
            //itemTemplate.me
            item.BeginGroup = true;
            item.Template = itemTemplate as ITemplate;
            ASPxButton btnPo = ((PlatinumWeb.E_PaySlip.MenuInfo)(item.Template)).FindControl("btnPo") as ASPxButton;
            btnPo.ClientVisible = false;
            ASPxButton btnJo = ((PlatinumWeb.E_PaySlip.MenuInfo)(item.Template)).FindControl("btnJo") as ASPxButton;
            btnJo.ClientVisible = false;
        }

        public static void ShtoMesazhGabimi(ASPxMenu m, String mesazh, UpdatePanel pnlMensazhi, DevExpress.Web.ASPxLoadingPanel loadPanel)
        { 
            ShtoMesazhGabimi(m, mesazh, pnlMensazhi);
            loadPanel.Visible = false;
        }
        public static void ShtoMesazh(ASPxMenu m, DbCore.clsMesazh mesazhi, UpdatePanel pnlMensazhi)
        {            
            switch (mesazhi.Tipi) { 
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
            DevExpress.Web.MenuItem itemButtonInfo = m.Items.FindByName("TemplatedItemInfo");
            //ASPxLabel hl = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhPage") as ASPxLabel;
            //hl.ForeColor = Color.Red;
            //hl.Text = mesazh;
            ASPxComboBox combo = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhList") as ASPxComboBox;
            DateTime tani = DateTime.Now;
            combo.Items.Insert(0, new ListEditItem(appendStringOfTime(tani, mesazh), getIntOfDate(tani), "images/info_error3.ico"));
            combo.SelectedIndex = 0;
            combo.Width = Unit.Percentage(100);
            pnlMesazhi.Update();
        }

        public static void ShtoMesazhSuksesi(ASPxMenu m, String mesazh, UpdatePanel pnlMensazhi)
        {
            DevExpress.Web.MenuItem itemButtonInfo = m.Items.FindByName("TemplatedItemInfo");
            //ASPxLabel hl = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhPage") as ASPxLabel;
            //hl.ForeColor = Color.Green;
            //hl.Text = mesazh;
            ASPxComboBox combo = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhList") as ASPxComboBox;
            DateTime tani = DateTime.Now;
            combo.Items.Insert(0, new ListEditItem(appendStringOfTime(tani, mesazh), getIntOfDate(tani), "images/info_sukses.ico"));
            combo.SelectedIndex = 0;
            combo.Width = Unit.Percentage(100);
            pnlMensazhi.Update();
        }

        public static void ShtoMesazhInformues(ASPxMenu m, String mesazh, UpdatePanel pnlMensazhi)
        {
            DevExpress.Web.MenuItem itemButtonInfo = m.Items.FindByName("TemplatedItemInfo");
            //ASPxLabel hl = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhPage") as ASPxLabel;
            //hl.ForeColor = Color.Green;
            //hl.Text = mesazh;
            ASPxComboBox combo = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhList") as ASPxComboBox;
            DateTime tani = DateTime.Now;
            combo.Items.Insert(0, new ListEditItem(appendStringOfTime(tani, mesazh), getIntOfDate(tani), "images/info_info.ico"));
            combo.SelectedIndex = 0;
            combo.Width = Unit.Percentage(100);
            pnlMensazhi.Update();
        }

        public static string MerrMesazhin(ASPxMenu m)
        {
            DevExpress.Web.MenuItem itemButtonInfo = m.Items.FindByName("TemplatedItemInfo");
            //ASPxLabel hl = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhPage") as ASPxLabel;
            //return hl.Text;
            ASPxComboBox combo = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhList") as ASPxComboBox;
            return combo.SelectedItem.Text;
        }

        public static void ShtoPyetje(ASPxMenu m, string pyetje, UpdatePanel pnlMesazhi)
        {
            DevExpress.Web.MenuItem itemButtonInfo = m.Items.FindByName("TemplatedItemInfo");
            //ASPxLabel hl = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhPage") as ASPxLabel;
            //hl.ForeColor = Color.Black;
            //hl.Text = pyetje;
            ASPxComboBox combo = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("mesazhList") as ASPxComboBox;
            DateTime tani = DateTime.Now;
            combo.Items.Insert(0, new ListEditItem(appendStringOfTime(tani, pyetje), getIntOfDate(tani), "images/info_pyetje.ico"));
            combo.SelectedIndex = 0;

            ASPxButton btnPo = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("btnPo") as ASPxButton;
            btnPo.ClientVisible = true;

            ASPxButton btnJo = ((PlatinumWeb.E_PaySlip.MenuInfo)(itemButtonInfo.Template)).FindControl("btnJo") as ASPxButton;
            btnJo.ClientVisible = true;
            combo.Width = Unit.Percentage(100);
            pnlMesazhi.Update();
        }

        private static Int32 getIntOfDate(DateTime date)
        {
            return date.Day * (Int32)Math.Pow(10, 6) + date.Hour * (Int32)Math.Pow(10, 4) + date.Minute * (Int32)Math.Pow(10, 2) + date.Second;
        }

        private static string appendStringOfTime(DateTime date, String mesazh)
        {
            return date.Hour + ":" + date.Minute + ":" + date.Second + " - " + mesazh;
        }
    }
}