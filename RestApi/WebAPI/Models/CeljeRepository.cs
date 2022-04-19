using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using System;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DbCore.DbQendraKosto;
using DbCore;

namespace RestApi.WebAPI.Models
{
    public class CeljeRepository
    {
        public static bool kontrolloEkzistonKodObjekti(string kodi, int idObjekti, int idndermarje, int idPerdorues, int kategoria)
        {
            if (idObjekti != 0)//nqs jemi ne modifikim kontrollojme ne ka ndryshuar kodi i objektit. nqs po kontrollojme ne ekziston ky kod
            {
                string kodiNeDataBaze = "";
                switch (kategoria)
                {
                    case 13:
                        kodiNeDataBaze = clsArtikulli.ktheKodArtikulliSipasId(idObjekti);
                        break;
                    case 23:
                        kodiNeDataBaze = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(idObjekti, idPerdorues);
                        break;
                }

                if (kodiNeDataBaze != kodi)
                {
                    bool ekziston = true;
                    switch (kategoria)
                    {
                        case 13:
                            ekziston = clsArtikulli.ekziston(kodi, idndermarje);
                            break;
                        case 23:
                            ekziston = clsNjesiAdministrative.ekziston(kodi, idndermarje);
                            break;
                        default:
                            ekziston = true;
                            break;
                    }
                    return ekziston;
                }
                else return false;
            }
            //ne raste te shtimit kontrollojme nese ekziston apo jo kodi i objektit
            bool ekzistonShtim = true;
            switch (kategoria)
            {
                case 13:
                    ekzistonShtim = clsArtikulli.ekziston(kodi, idndermarje);
                    break;
                case 23:
                    ekzistonShtim = clsNjesiAdministrative.ekziston(kodi, idndermarje);
                    break;
                default:
                    ekzistonShtim = true;
                    break;
            }
            return ekzistonShtim;
        }

        public static object ktheKFSipasKodit(String kodi, int idNdermarrje)
        {
            clsKlientFurnitor kf = new clsKlientFurnitor();
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(DateTime.Now);
            kf.mbushKlientFurnitorSipasKodit(kodi, idNdermarrje);
            return new { kf= kf, data= data};
        }
        public static object merrBuxhete(int id, int idNderViti,  int idllojbuxheti, int idviti, bool eshteprojekt)
        {

            return colBuxhetet.ktheDataBuxhetetPerQendratEKostos(id, idNderViti,idllojbuxheti,idviti,eshteprojekt);
        }

        public static object merrQkPrind(int id)
        {

            clsQendraKosto qk = new clsQendraKosto(id);
            return new { prindi = qk};
        }


        public static string ktheImazhPerdoruesi(int idPerdorues)
        {
            return clsArkiva.ktheImazhPerdoruesi(idPerdorues);
        }

        
        public static string vendosThumbnailDefaultArkiva(int iddok, int idKategoria, string ext, string path, string name, int idPerdoruesi, string connection)
        {
            
            clsArkiva arkivaDok = new clsArkiva(0, iddok, idKategoria, ext, path, name, "", (int)StatusDokumenti.Ruajtur, idPerdoruesi, idPerdoruesi, 0, connection, true);
            arkivaDok.ruaj();
            return "";
           
        }
    }
}
