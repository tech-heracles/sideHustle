using DbCore;
using DbCore.DbAnalizeBuxheti;

namespace RestApi.WebAPI.Models
{
    public class AnalizeBuxhetiRepository
    {
        internal static object KaVeprimeMeKeteFushe(int idAmbjenti, int idRreshti)
        {
            return new { KaVeprime = AnalizeBuxheti.KaVeprimeRreshti(idAmbjenti, idRreshti) };
        }

        internal static clsMesazh KontrolloNeseMundTeFshihetShpenzimiKonfig(int shokID)
        {
            if (clsShpenzimeOperativeKonfig.KaRegjistrimeMeKeteShpenzimOperativ(shokID))
                return new clsMesazh(false, "Me kete shpenzim operativ jane bere regjistrime!");
            if (clsShpenzimeOperativeKonfig.KaFemij(shokID))
                return new clsMesazh(false, "Ky shpenzim operativ sherben si prind per shpenzime te tjera operative!");
            //TODO nese ka femij te pytet perdoruesi nese deshiron te vazhdoj me fshirjen nese asnje nga femijet nuk eshte perdorur ne regjistrime

            return new clsMesazh(true);
        }

        internal static int MerrNivelinEShpenzimitOperativ(int idPrindi)
        {

            if (idPrindi == 0)
                return 1;
            else return clsShpenzimeOperativeKonfig.MerrNivelinSipasID(idPrindi) + 1;
        }

        internal static object KaVeprimeMeKeteFusheParashikimi(int idRreshti)
        {
            return new { KaVeprime = AnalizeBuxheti.KaVeprimeParaShikimiIShpenzimeve(idRreshti) };
        }

        internal static object EshtePerdorurPrindiParashikimShpenzimesh(int idRreshti)
        {
            return new { KaFemije = AnalizeBuxheti.EshtePerdorurKyZePrindParashikimShpenzimesh(idRreshti) };
        }

        internal static clsMesazh KontrolloNeseMundTeFshihetZeriIProkurimit(int rpkId)
        {
            if (clsRealizimProkurimesh.KaRegjistrimeMeKeteRealizimProkurimesh(rpkId))
                return new clsMesazh(false, "Me kete ze jane bere regjistrime!");
            if (clsRealizimProkurimesh.KaFemij(rpkId))
                return new clsMesazh(false, "Ky ze sherben si prind per zera te tjere prokurimesh publike!");
            
            return new clsMesazh(true);
        }

        internal static int MerrNivelinEZeritTeProkurimeve(int idPrindi)
        {

            if (idPrindi == 0)
                return 1;
            else return clsRealizimProkurimesh.MerrNivelinSipasID(idPrindi) + 1;
        }

    }
}