using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAnalizeBuxheti
{
    public class AnalizeBuxheti
    {
        /// <summary>
        /// metode e cila rivendos konfigurimin per rreshtat e ambjenteve te analizes se buxhetit
        /// </summary>
        /// <param name="idAmbjenti"></param>
        /// <param name="idNdermarrjeRaportuese"></param>
        /// <returns></returns>
        public static clsMesazh RuajKonfigurimRreshtash(int idAmbjenti, int idNdermarrjeRaportuese)
        {
            clsMesazh mesazh = null;





            switch (idAmbjenti)
            {
                case 1://PROJEKT BUXHETI I VITIT PASARDHES
                    mesazh = colPBuxhetPermbledhes.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;

                case 2://PARASHIKIMI I TE ARDHURAVE TE VETA TE MINISTRIVE DHE INSTITUCIONEVE BUXHETORE
                    mesazh = colParashikimiTeArdhura.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;

                case 3://PARASHIKIMI I SHPENZIMEVE PER PERSONELIN PER VITIN PASARDHES
                    mesazh = colParashikimShpenzPersoneliConfig.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;

                case 4://PARASHIKIMI I SHPENZIMEVE PER PERSONELIN 

                    break;
                case 5://PROJEKT-BUXHETI PER VITIN PASARDHES (SHPENZIME OPERATIVE)

                    break;
                case 6://PARASHIKIMET PER SHPENZIME KAPITALE PER 3 VITET E ARDHSHME
                    mesazh = colShpenzimeKapitale.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;

                case 7://PROJEKT BUXHETI PER 3 VITET E ARDHSHME
                    mesazh = colPBuxheti3Vjecar.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;

                case 8://PLANIFIKIMI I PRODUKTEVE TE PROGRAMIT NE SASI DHE VLERE
                    mesazh = colPlanifikimiProdukteve.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;
                case 9://INVENTARI SIPAS PERDORUESVE
                    mesazh = colInventariPerdorues.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;
                case 10://INVENTARI SIPAS VITEVE
                    mesazh = colInventariVite.KrijoDokumentDefault(idNdermarrjeRaportuese);
                    break;
                case 11://EVIDENCA STATISTIKORE
                    mesazh = new clsMesazh(true);
                    break;
                default:
                    mesazh = new clsMesazh(false, "Ky ambjent nuk ekziston!");
                    break;
            }

            return mesazh;
        }

        public static bool KaVeprimeRreshti(int idAmbjenti, int idRreshti)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KaVeprimeMeKeteRresht(idAmbjenti, idRreshti);
            }
        }

       

        internal static clsMesazh FshiRreshtinNgaAmbjenti(int rreshtiID, int idAmbjenti, clsDatabaseAnalizeBuxheti dbAB)
        {
            clsMesazh mesazh = null;

            switch (idAmbjenti)
            {
                case 1://PROJEKT BUXHETI I VITIT PASARDHES
                    mesazh = clsPBuxhetPermbledhes.Fshi(rreshtiID, dbAB);
                    break;

                case 2://PARASHIKIMI I TE ARDHURAVE TE VETA TE MINISTRIVE DHE INSTITUCIONEVE BUXHETORE
                    mesazh = clsParashikimiTeArdhura.Fshi(rreshtiID, dbAB);
                    break;

                case 3://PARASHIKIMI I SHPENZIMEVE PER PERSONELIN PER VITIN PASARDHES
                    mesazh = clsParashikimShpenzPersoneliConfig.Fshi(rreshtiID, dbAB);
                    break;

                case 4://PARASHIKIMI I SHPENZIMEVE PER PERSONELIN 

                    break;

                case 5://PROJEKT-BUXHETI PER VITIN PASARDHES (SHPENZIME OPERATIVE)
                    mesazh = clsShpenzimeOperative.Fshi(rreshtiID, dbAB);
                    break;

                case 6://PARASHIKIMET PER SHPENZIME KAPITALE PER 3 VITET E ARDHSHME
                    mesazh = clsShpenzimeKapitale.Fshi(rreshtiID, dbAB);
                    break;

                case 7://PROJEKT BUXHETI PER 3 VITET E ARDHSHME
                    mesazh = clsPBuxheti3Vjecar.Fshi(rreshtiID, dbAB);
                    break;

                case 8://PLANIFIKIMI I PRODUKTEVE TE PROGRAMIT NE SASI DHE VLERE
                    mesazh = clsPlanifikimiProdukteve.Fshi(rreshtiID, dbAB);
                    break;

                case 9://INVENTARI SIPAS PERDORUESVE
                    mesazh = clsInventariPerdorues.Fshi(rreshtiID, dbAB);
                    break;
                case 10:
                    mesazh = clsInventariVite.Fshi(rreshtiID, dbAB);
                    break;
                case 11:
                    mesazh = clsKokaEvidencaStatistikore.Fshi(rreshtiID, dbAB);
                    break;
                default:
                    mesazh = new clsMesazh(false, "Ky ambjent nuk ekziston!");
                    break;
            }

            return mesazh;
        }

        /// <summary>
        /// shton automatikisht nivelet e reja te shpenzimeve 
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static clsMesazh RuajShpenzimeOperative(int idNdermarrje)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KrijoDokumentDefaultShpenzimeOperative(idNdermarrje);
            }
        }

        /// <summary>
        /// shton automatikisht nivelet e reja te shpenzimeve 
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static clsMesazh RuajZeraProkurimesh(int idNdermarrje)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KrijoDokumentDefaultRealizimProkurimesh(idNdermarrje);
            }
        }
        /// <summary>
        /// merr si parameter colectionin me e konfigurimeve dhe nje objekt shepenzimeKofig dhe percakton nivelin ne varesi te prinidit qe ka objekti
        /// </summary>
        /// <param name="colShpenzimeKonfig"></param>
        /// <param name="shpenzKonfig"></param>
        /// <returns></returns>
        public static int ktheNivelShpenzimiOperativ(colShpenzimeOperativeKonfig colShpenzimeKonfig, clsShpenzimeOperativeKonfig shpenzKonfig)
        {
            if (shpenzKonfig == null || shpenzKonfig.IdPrindi == 0)
                return 1;
            clsShpenzimeOperativeKonfig prindi = colShpenzimeKonfig.FirstOrDefault(x => x.ShokId == shpenzKonfig.IdPrindi);
            if (prindi != null)
                return 1 + ktheNivelShpenzimiOperativ(colShpenzimeKonfig, prindi);
            return 1;
        }

        /// merr si parameter colectionin me shpenzimet operative ,shpenzimin te cilit do i caktohet nje prind,dhe prindin
        /// me pas kontrollon nese prindi potencial ka si prind ndonje nga femijet e shpenzimit korrent
        /// </summary>
        /// <param name="colShpenzimeKonfig"></param>
        /// <param name="shpenzimeKonfig"></param>
        /// <param name="prindiPotencial"></param>
        /// <returns></returns>
        public static bool FormohetCikel(IEnumerable<clsShpenzimeOperativeKonfig> colShpenzimeKonfig, clsShpenzimeOperativeKonfig shpenzimeKonfig, clsShpenzimeOperativeKonfig prindiPotencial)
        {
            if (prindiPotencial == null)
                return false;
            var colFemijet = colShpenzimeKonfig.Where(x => x.IdPrindi == prindiPotencial.ShokId);///merr gjithe femijet e prindit potencial

            foreach (var node in colFemijet)
            {
                if (node.IdPrindi == shpenzimeKonfig.ShokId)//kontrollohet nese ndonjeri nga femijet e prindit potencial ka si prind shepnzimin qe po do i caktohet prindi i ri
                    return true;
                else return FormohetCikel(colShpenzimeKonfig, shpenzimeKonfig, node);
            }

            return false;
        }

        public static bool FormohetCikel(IEnumerable<clsRealizimProkurimesh> colRealizimeKonfig, clsRealizimProkurimesh realizimeKonfig, clsRealizimProkurimesh prindiPotencial)
        {
            if (prindiPotencial == null)
                return false;
            var colFemijet = colRealizimeKonfig.Where(x => x.IdPrindi == prindiPotencial.RpkId);///merr gjithe femijet e prindit potencial

            foreach (var node in colFemijet)
            {
                if (node.IdPrindi == realizimeKonfig.RpkId)//kontrollohet nese ndonjeri nga femijet e prindit potencial ka si prind shepnzimin qe po do i caktohet prindi i ri
                    return true;
                else return FormohetCikel(colRealizimeKonfig, realizimeKonfig, node);
            }

            return false;
        }

        public static DataTable MerrTeDhenatERaportiPerGride( string raport,Dictionary<string, object> parametra, bool raportuese)
        {

            string sp = string.Empty;


            string suffix = raportuese ? "_raportuese" : string.Empty;
            switch (raport)
            {
                case "BuxhetPermbledhes":
                    sp = "prc_RAP_AB_PBUXHET_PERMBLEDHES" + suffix;
                    break;

                case "ParashikimiTeArdhura":
                    sp = "prc_RAP_AB_PARASHIKIMI_TE_ARDHURA" + suffix;
                    break;

                case "ParashikimShpenzPersoneli":
                    sp = "prc_RAP_AB_PARASHIKIM_SHPENZ_PERSONELI" + suffix;
                    break;

                case "ShpenzimeOperative":
                    sp = "prc_RAP_AB_SHPENZIME_OPERATIVE" + suffix;
                    break;
                case "ShpenzimeOperative3Vjecar":
                    sp = "prc_RAP_AB_SHPENZIME_OPERATIVE_3Vjecar" + suffix;
                    break;
                case "PBuxheti3Vjecar":
                    sp = "prc_RAP_AB_PBUXHETI_3VJECAR" + suffix;
                    break;

                case "PlanifikimiIProdukteve":
                    sp = "prc_RAP_AB_PLANIFIKIMI_PRODUKTEVE" + suffix;
                    break;

                case "ShpenzimeKapitale":
                    sp = "prc_RAP_AB_SHPENZIME_KAPITALE" + suffix;
                    break;
                case "ParashikimShpenzimeshRaportuese":
                    sp = "prc_RAP_AB_PARASHIKIM_SHPENZ_PERSONELI_VIT_PASARDHES_raportues";
                    break;
                case "ShpenzimeOperativePermbledhese":
                    sp = "prc_RAP_AB_PERMBLEDHESE_SHPENZIME_OPERATIVE";
                    break;
                case "ShpenzimeOperativeMujore":
                    sp = "prc_RAP_AB_SHPENZIME_OPERATIVE_mujore" + suffix;
                    break;
                case "RegjistriIRealizimitTeProkurimevePublike":
                    sp = "prc_RAP_AB_REALIZIM_PROKURIMESH_PUBLIKE" + suffix;
                    break;
                    
                default:
                    throw new Exception("Ky raport nuk ekziston!!");
            }

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.MerrDataTablePerRaportin(sp, parametra);
            }
        }
        public static DataTable MerrTeDhenaRaportiPivotGrid(int idNdermarrje, int idKomponente, int idNdermVit)
        {

            string sp = string.Empty;
            bool eshteRaportuese=clsNdermarrje.EshteRaportuese(idNdermarrje);

            string suffix = eshteRaportuese ? "_raportuese" : string.Empty;

            switch (idKomponente)
            {

                case 3026:
                    if (!eshteRaportuese)
                        throw new Exception("Ju Nuk keni te drejta te shikoni kete raport!");
                    sp = "prc_rap_T_AB_INVENTARI_VITE" + suffix;
                    break;
                case 3027:
                    if (!eshteRaportuese)
                        throw new Exception("Ju Nuk keni te drejta te shikoni kete raport!");
                    sp = "prc_rap_T_AB_INVENTARI_PERDORUES" + suffix;
                    break;
                case 3028:
                    if (!eshteRaportuese)
                        throw new Exception("Ju Nuk keni te drejta te shikoni kete raport!");
                    sp = "prc_rap_T_AB_EVIDENCA_STATISTIKORE" + suffix;
                    break;
                case 3041:
                    if (!eshteRaportuese)
                        throw new Exception("Ju Nuk keni te drejta te shikoni kete raport!");
                    sp = "prc_rap_T_AB_PLANIFIKIM_REALIZIM" + suffix;
                    break;
                default:
                    throw new Exception("Ky raport nuk ekziston!");


            }

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.MerrDataTablePerRaportin(sp, idNdermarrje, idNdermVit);
            }

        }

    

        public static clsMesazh RuajKonfigurimPerShpenzimePersoneli(int idNdermarrje)
        {
           using(clsDatabaseAnalizeBuxheti dbAB=new clsDatabaseAnalizeBuxheti())
           {
              return dbAB.KrijoDokumentDefaultParashikimShpenzPersoneli(idNdermarrje);
           }
        }

        public static bool KaVeprimeShpenzimi(int shokId)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KaVeprimeMeKeteShpenzim(shokId);
            }
        }

        public static bool KaVeprimeParaShikimiIShpenzimeve(int rreshtiId)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KaVeprimeMeKeteParashikimShpenzimi(rreshtiId);
            }
        }


        public static bool EshtePerdorurKyZePrindParashikimShpenzimesh(int rreshtiId)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.EshtePerdorurKyZePrind(rreshtiId);
            }
        }


        public static DataTable MerrTeDhenatERaportitParashikimProkurimeshPublike(int idNdermarrje, int idNdermVit)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.MerrTeDhenatERaportitParashikimProkurimeshPublike(idNdermarrje, idNdermVit);
            }
        }

        /// <summary>
        /// Kontrollon rastet kur komponentja mund te hapet vetem nga ndermarrja raportuese
        /// Konfigurimi i zerave, dhe raportet e inventarit, evidences,tabeles permbledhese planifikim/realizim
        /// </summary>
        /// <param name="idKomponente"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static bool KaTeDrejteTeHapeAmbjentin(int idKomponente, int idNdermarrje)
        {
            if ((idKomponente == 3001 || idKomponente == 3026 || idKomponente == 3027 || idKomponente == 3028 || idKomponente == 3041))
                return clsNdermarrje.EshteRaportuese(idNdermarrje);
            else return true;
        }
    }
}