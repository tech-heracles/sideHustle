using System;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.IMBUtils.Cache
{
    /// <summary>
    /// Cache per konfigurimin e gridave (T_GRIDATRUPI e tabelat qe lidhen me te). Keto lexohen disa here per cdo faqe dhe
    /// T_GRIDATRUPI ka mbi nje milion rreshta pa indeks per GRIDAKOKAID, keshtu qe cdo lexim kushtonte 100-250 ms.
    /// Si invalidohet: shih <see cref="TabelaCache"/>.
    /// </summary>
    public static class KonfigGrideCache
    {
        private static readonly TabelaCache cache = new TabelaCache("KonfigGrideCache",
            "T_GRIDATRUPI", "T_GRIDAKOKA", "T_LUPAMULTIPLE", "T_KONFIGAMBJENTE", "T_KOMPONENTE");

        /// <param name="dbManager">lidhja me te cilen lexohet (dhe databaza, qe hyn ne celes)</param>
        /// <param name="celesi">procedura dhe parametrat</param>
        /// <param name="lexo">leximi nga databaza; vendos vete parametrat e veta</param>
        public static DataTable Merr(IDbManager dbManager, string celesi, Func<DataTable> lexo)
        {
            return cache.Merr(dbManager, celesi, lexo);
        }
    }

    /// <summary>
    /// Cache per te drejtat e roleve (T_ROLDREJTAKOKA/TRUPI, T_ROLPERDORUES dhe tabelat e komponenteve, moduleve e
    /// raporteve qe hyjne ne procedurat e te drejtave). Celesi permban perdoruesin, ndermarrjen, vitin dhe komponenten,
    /// keshtu qe secili perdorues merr vetem te drejtat e veta. Cdo ndryshim ne keto tabela (nga faqet e roleve, nga
    /// procedurat apo direkt ne databaze) e ben te pavlefshem cache-in brenda <see cref="TabelaCache.IntervaliStampes"/>.
    /// <para>Kujdes: mbi kete cache, te drejtat e nje komponente ruhen edhe ne sesionin e perdoruesit
    /// (GlobalCacheManager.MySessionCache, si me pare), pra perdoruesi qe eshte i loguar i sheh ndryshimet pas login-it te ri.</para>
    /// </summary>
    public static class TeDrejtaCache
    {
        private static readonly TabelaCache cache = new TabelaCache("TeDrejtaCache",
            "T_ROLDREJTAKOKA", "T_ROLDREJTATRUPI", "T_ROLPERDORUES", "T_KOMPONENTE", "T_MODULI", "T_RAPORTI");

        public static DataTable Merr(IDbManager dbManager, string celesi, Func<DataTable> lexo)
        {
            return cache.Merr(dbManager, celesi, lexo);
        }
    }

    /// <summary>
    /// Cache per butonat e menuse se faqes sipas te drejtave (prc_T_MENUITEM_SipasKomponentes...DheTeDrejta*,
    /// prc_T_MENUITEM_SipasRaportit). Lexohen disa here per cdo hapje faqeje; njera prej tyre ndertohet si SQL dinamik qe
    /// kompilohet nga e para ne cdo thirrje (~15 ms). Celesi permban perdoruesin, ndermarrjen, vitin, komponenten dhe
    /// parametrat e tjere; invalidohet si <see cref="TeDrejtaCache"/>, edhe kur ndryshojne menute ose ndermarrja.
    /// </summary>
    public static class MenuTeDrejtaCache
    {
        private static readonly TabelaCache cache = new TabelaCache("MenuTeDrejtaCache",
            "T_ROLDREJTAKOKA", "T_ROLDREJTATRUPI", "T_ROLPERDORUES", "T_KOMPONENTE", "T_MENUPERKOMPONENTE", "T_MENUITEM",
            "T_NDERMARJE", "T_RAPORTI");

        public static DataTable Merr(IDbManager dbManager, string celesi, Func<DataTable> lexo)
        {
            return cache.Merr(dbManager, celesi, lexo);
        }
    }
}
