using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaWebReports
{
    /// <summary>
    /// Raportet aktualisht ngarkohen duke kapur dll-ne projektit qe gjendet ne bin
    /// Projektet e moduleve te vecanta te raporteve jane pjese e varesive te Alphawebreports, por jo te PlatinumWeb
    /// Per kete arsye ato kopjohen tek bin-i i AlphaWebReports, por neqoftese nuk perdoren gjekundi nuk kane per tu kopjuar tek projektet qe kane
    /// varesi nga AlphaWebReports. Kjo klase sherben vetem per ti patur te pakten njehere si reference ketu per tu kopjuar me tej tek PlatinumWeb
    /// </summary>
    public class AssemblyReference
    {
        public AssemblyReference()
        {
            var unused = new RaportetDs.ListPagesat.Raportet.Rap_Amendament();
            var rap_Amortizimi = new RaportetDs.Amortizimi.Rap_Amortizimi();
            var rAP_ARKA_ARKETIMETDITORE = new RaportetDs.Arka.Raporte.RAP_ARKA_ARKETIMETDITORE();
            var rAP_ARKABANKA_DITARIPERMBLEDHES = new RaportetDs.Banka.Raporte.RAP_ARKABANKA_DITARIPERMBLEDHES();
            var rapArtikujTeBlereMeSeriale = new RaportetDs.Blerje.RapArtikujTeBlereMeSeriale();
            var rapmagazina = new RaportetDs.Magazina.Ds_ArtikujGjendjeMinMaks();
            var rapkf = new RaportetDs.KlientFurnitor.DataSource.ds_MaturimFurnitori();
            var rapkont = new RaportetDs.Ds_FleteKontabel();
        }
    }
}
