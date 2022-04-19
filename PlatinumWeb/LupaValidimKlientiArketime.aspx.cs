using DbCore.DbRegjistrim;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using DbCore.DbArkaBanka;
using DbCore;
using DbCore.BRMAdapterServices;
using DbCore.Integrime;
using DbCore.IMBUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaValidimKlientiArketime : MyPageBase
    {
        private string lloji;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
            lloji = Request.QueryString["lloji"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int idPerdoruesi;
                int idGjuha;
                int idNdermarrje;
                int idViti;
                int idNdermarrjeVit;
                bool eshteOwn, eshteMeme;

                if (!IsPostBack)
                {
                    if (!DbCore.mySessionObjects.isLogedIn(Session))
                    {
                        Response.Redirect($"{Paths.defaultLoginPath}arsye=FaqePaautorizuar");
                    }
                    idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                    idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                    idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                    eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
                    eshteMeme = DbCore.mySessionObjects.merrEshteMemeSesioni(Session);
                    hfState.Set("idPerdoruesi", idPerdoruesi);
                    hfState.Set("idGjuha", idGjuha);
                    hfState.Set("idNdermarrje", idNdermarrje);
                    hfState.Set("idViti", idViti);
                    hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                    hfState.Set("OwnShop", eshteOwn);
                    hfState.Set("Meme", eshteMeme);

                    txtNrLlogarie.ClientEnabled = (Request.QueryString["lloji"] == "arketimLlogariKlienti");
                    txtNrKontakti.ClientEnabled = (Request.QueryString["lloji"] == "arketimAbonent");
                    //txtEmriKlientit.ClientEnabled = (Request.QueryString["lloji"] == "arketimAbonent");
                    percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, idGjuha, eshteMeme);

                }
                else
                {
                    idPerdoruesi = (int)hfState["idPerdoruesi"];
                    idGjuha = (int)hfState["idGjuha"];
                    idNdermarrje = (int)hfState["idNdermarrje"];
                    idViti = (int)hfState["idViti"];
                    idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                    //eshteOwn = (bool)hfState["OwnShop"];
                    eshteMeme = (bool)hfState["Meme"];
                    if (!IsCallback || (IsCallback && Request["__CALLBACKID"].Contains("ASPxMenu1")))
                        percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, idGjuha, eshteMeme);
                }

                //DbCore.clsFunksione.konfiguroGrideListeMadhePopupiPaTheme(gvLupaArtikull, "IdArtikulli");
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, int idGjuha, bool meme)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaValidimKlientiArketime.aspx", this, MenuInfo, null, null, true, false, false, meme);
        }

        protected void btnValido_Click(object sender, EventArgs e)
        {
            try
            {
                string msisdn = "";
                if (lloji == "arketimAbonent")
                {
                    if (string.IsNullOrWhiteSpace(txtNrKontakti.Text))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "MSISDN nuk duhet te jete bosh!", pnlMesazhi);
                        hfStatus.Value = bool.FalseString;
                        return;
                    }
                    msisdn = "3556" + txtNrKontakti.Text;
                }
                if (lloji == "arketimLlogariKlienti")
                {
                    if (string.IsNullOrEmpty(txtNrLlogarie.Text))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Numri i llogarise nuk duhet te jet bosh! ", pnlMesazhi);
                        hfStatus.Value = bool.FalseString;
                        return;
                    }

                }

                //per siguri
                if (!string.IsNullOrEmpty(txtNrKontakti.Text) && !string.IsNullOrEmpty(txtNrLlogarie.Text))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te plotesoni numrin e llogarise dhe msisdn e klientit njekohesisht!", pnlMesazhi);
                    hfStatus.Value = bool.FalseString;
                    return;
                }
                if (string.IsNullOrEmpty(txtEmriKlientit.Text) || txtEmriKlientit.Text.Length < 3)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Emri i klientit nuk duhet te permbaj me pak se 3 karaktere", pnlMesazhi);
                    hfStatus.Value = bool.FalseString;
                    return;
                }


                (clsMesazh mesazh, var vlera, string accountNo) = new BrmAdapter().MerrBalancenBashkeMeFaturatNgaBrm(msisdn, txtEmriKlientit.Text, txtNrLlogarie.Text, string.Empty, mySessionObjects.merrIdNdermarrjeSesioni(Session));



                if (vlera != null)
                {
                    bool merrKlient = Request.QueryString["MerrKlient"] != null && bool.Parse(Request.QueryString["MerrKlient"]);
                    DbCore.DbKontabiliteti.clsKlientFurnitor klient = merrKlient ? new DbCore.DbKontabiliteti.clsKlientFurnitor(string.IsNullOrWhiteSpace(txtNrLlogarie.Text) ? accountNo : txtNrLlogarie.Text, IdNdermarrja, IdPerdoruesi) : null;

                    var teDhenatEKlientit = new
                    {
                        MSISDN = msisdn,
                        ACCOUNTNO = string.IsNullOrWhiteSpace(txtNrLlogarie.Text) ? accountNo : txtNrLlogarie.Text,
                        EMRIKLIENTIT = txtEmriKlientit.Text,
                        MBIPAGESA = vlera.Item4,//suspendedAmount
                        KLIENTI = klient
                    };
                    //dergojme ne client
                    hfTeDhenaKlienti.Value = Newtonsoft.Json.JsonConvert.SerializeObject(teDhenatEKlientit);


                    mySessionObjects.RuajFaturatNgaBrmNeSession(Session, vlera);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Te dhenat e klientit jane te sakta,Mund te vazhdoni me arketimin!", pnlMesazhi);
                    hfStatus.Value = bool.TrueString;

                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format("{0}", mesazh.PershkrimMesazhi), pnlMesazhi);
                    hfStatus.Value = bool.FalseString;
                }

            }

            catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format("Ndodhi nje gabim gjate validimit te klientit {0}", ex.Message), pnlMesazhi);
                NLog.LogManager.GetCurrentClassLogger().Error("Ndodhi nje gabim gjate validimit te klientit {0}", ex.Message);
            }
        }
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            try
            {
                percaktoTemplateMenu(ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (int)hfState["idGjuha"], (bool)hfState["Meme"]);
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }
    }
}