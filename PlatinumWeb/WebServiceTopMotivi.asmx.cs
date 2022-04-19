using DbCore.WebAutoFear.Pergjigjet;
using DbCore.WebAutoFear.Klasat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using DbCore.DbImporte;

namespace PlatinumWeb
{
    [System.Web.Services.WebServiceBindingAttribute(Name = "IServiceV23", Namespace = "DVSE")]
    public interface IServiceV23
    {

        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("DVSE/IServiceV23/GetArticleInformation", RequestNamespace = "DVSE", ResponseNamespace = "DVSE")]
        GetBackItems GetArticleInformation(User User, Item[] Items, ResponseLimit responseLimit);

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("DVSE/IServiceV23/SendOrder", RequestNamespace = "DVSE", ResponseNamespace = "DVSE")]
        GetBackOrder SendOrder(User User, Order Order, Item[] Items);
    }
    /// <summary>
    /// GetArticleInformation -> Request of stock information, prices, etc.
    /// SendOrder -> Transfer of an order
    /// SignalDownload -> Optional function if the information system should give an information that a shopping basked need to be transfered
    /// GetMaxAllowedArticle -> Function which gives the information which content of article information can be transfered
    /// </summary>
    [WebService(Name = "IServiceV23", Namespace = "DVSE")]
    /*[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]*/
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceTopMotivi : System.Web.Services.WebService, IServiceV23
    {
        /// <summary>
        /// The function GetArticleInformation delivers information to a list of articles depending on the user, p.e.stock information, prices, etc.
        /// </summary>
        /// <param name="user">Object Type User</param>
        /// <param name="items">Object Type Items</param>
        /// <returns>Object Type GetBackItems</returns>
        [System.Web.Services.WebMethod(EnableSession = false)]
        public GetBackItems GetArticleInformation(User User, Item[] Items, ResponseLimit responseLimit)
        {
            GetBackItems pergjigja;
            Boolean meGabime = false;
            int rreshti = 0;
            for (rreshti = 0; rreshti < Items.Length; rreshti++)
            {
                try
                {
                    Items[rreshti].ktheInfoArtikuj(User.CustomerId);
                }
                catch (Exception)
                {
                    meGabime = true;
                }
            }
            if (!meGabime)
                pergjigja = new GetBackItems(0, "", Items, new ResponseLimit(false, Items.Length));
            else
                pergjigja = new GetBackItems(-1, "An error has occurred during article transfer", Items, new ResponseLimit(false, Items.Length));
            return pergjigja;
        }

        /// <summary>
        /// The function SendOrder sends an order to the suppliers.
        /// </summary>
        /// <param name="user">Object Type User</param>
        /// <param name="order">Object Type Order</param>
        /// <param name="items">Object Type Items</param>
        /// <returns>Object Type GetBackOrder</returns>
        [System.Web.Services.WebMethod(EnableSession = false)]
        public GetBackOrder SendOrder(User User, Order Order, Item[] Items)
        {
            DbCore.clsMesazh mesazh;
            GetBackOrder pergjigja;
            string ndermarrja = "FEAR";
            DbCore.DbAdmin.colTrupiFormatImporti col = new DbCore.DbAdmin.colTrupiFormatImporti();
            DataTable dtKoka = new DataTable("Fear");
            DataTable dtTrupi = new DataTable("Fear");
            clsKonfigImporti importi = new clsKonfigImporti("ShitjeFear", DbCore.DbAdmin.clsNdermarrje.ktheIdNdermarrje(ndermarrja));
            importi.KrijoInsertSipasTemplate(ref dtKoka, (int)clsKonfigImporti.Dokumenta.Koka, ref col);
            importi.KrijoInsertSipasTemplate(ref dtTrupi, (int)clsKonfigImporti.Dokumenta.Trupi, ref col);
            if (krijoObjektinNeDataTable(ref dtKoka, ref dtTrupi, User.CustomerId, Order, Items, ndermarrja))
            {
                mesazh = DbCore.DbImporte.colImportSQL.ruajDokumentaNeTabeleImporti(dtKoka, dtTrupi, col, importi.EmerTabKoka, importi.EmerTabTrupi, importi.Kategoria);
                if (!mesazh.Status)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error("Ndodhi nje gabim gjate ruajtjes se dokumentave! " + mesazh.PershkrimMesazhi);
                    pergjigja = new GetBackOrder(-1, mesazh.PershkrimMesazhi, Order, Items);
                }
                else
                    pergjigja = new GetBackOrder(0, "", Order, Items);
                return pergjigja;
            }
            NLog.LogManager.GetCurrentClassLogger().Error("Ndodhi nje gabim gjate komunikimit per dergimin e orderave! ");
            pergjigja = new GetBackOrder(-1, "An error has occurred!", Order, Items);
            return pergjigja;
        }

        private Boolean krijoObjektinNeDataTable(ref DataTable dtKoka, ref DataTable dtTrupi, string CustomerId, Order order, Item[] items, string ndermarrja)
        {
            try
            {
                DataRow rreshtKoka = dtKoka.NewRow();
                long idTick = DateTime.Now.Ticks;
                string idShitjeKoka = idTick.ToString() + " - " + order.OrderId;
                rreshtKoka["Klient/Furnitori"] = !string.IsNullOrWhiteSpace(CustomerId) ? CustomerId : (object)DBNull.Value;
                rreshtKoka["Date Dokumenti"] = DateTime.Now;
                rreshtKoka["Kod Ndermarrje"] = ndermarrja;
                rreshtKoka["Id Shitje Koka"] = !string.IsNullOrWhiteSpace(idShitjeKoka) ? idShitjeKoka : (object)DBNull.Value;
                rreshtKoka["Nr Dokumenti"] = !string.IsNullOrWhiteSpace(order.OrderId) ? order.OrderId : (object)DBNull.Value;
                rreshtKoka["Emer Klienti"] = order.DeliveryAddress != null ? order.DeliveryAddress.CompanyName : (object)DBNull.Value;
                rreshtKoka["Kontakti"] = order.DeliveryAddress != null ? order.DeliveryAddress.Contact : (object)DBNull.Value;
                dtKoka.Rows.Add(rreshtKoka);
                for (int i = 0; i < items.Length; i++)
                {
                    DataRow rreshtTrupi = dtTrupi.NewRow();
                    rreshtTrupi["Id Shitje Koka"] = !string.IsNullOrWhiteSpace(idShitjeKoka) ? idShitjeKoka : (object)DBNull.Value;
                    rreshtTrupi["Kodi"] = !string.IsNullOrWhiteSpace(items[i].WholesalerArticleNumber) ? items[i].WholesalerArticleNumber : (object)DBNull.Value;
                    rreshtTrupi["Magazina"] = items[i].Warehouses != null && items[i].Warehouses.Length > 0 ? (items[i].Warehouses[0] != null ? items[i].Warehouses[0].Description : (object)DBNull.Value) : (object)DBNull.Value;
                    rreshtTrupi["Vlefta me TVSH"] = items[i].RequestedQuantity != null ? (items[i].Prices != null && items[i].Prices.Length > 0 ? items[i].Prices[0].Value * items[i].RequestedQuantity.Value : 0) : 0;
                    rreshtTrupi["TVSH"] = items[i].Prices != null ? (items[i].Prices != null && items[i].Prices.Length > 0 ? items[i].Prices[0].VAT : 0) : 0;
                    rreshtTrupi["Vlefta pa TVSH"] = items[i].RequestedQuantity != null ? (items[i].Prices != null && items[i].Prices.Length > 0 ? (items[i].Prices[0].Value * items[i].RequestedQuantity.Value) / (1 + items[i].Prices[0].VAT / 100) : 0) : 0;
                    rreshtTrupi["Cmimi me Tvsh"] = items[i].Prices != null ? items[i].Prices[0].Value : 0;
                    rreshtTrupi["Sasia"] = items[i].RequestedQuantity != null ? items[i].RequestedQuantity.Value : 0;
                    rreshtTrupi["Njesia"] = items[i].RequestedQuantity != null ? items[i].RequestedQuantity.QuantityUnit : (object)DBNull.Value;
                    dtTrupi.Rows.Add(rreshtTrupi);
                }
                return true;
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error("Ndodhi nje gabim gjate perkatesimit te vlerave! " + e);
                return false;
            }
        }

    }


}
