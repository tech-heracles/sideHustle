using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbInventari;

namespace DbCore.DbProdhimi
{
     /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsProjektProdhimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colProjektProdhimi: List<clsProjektProdhimi>
    {
        #region konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colProjektProdhimi()
        {

        }



        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsProjektProdhimi</param>
        public colProjektProdhimi(IEnumerable<clsProjektProdhimi> collection)
            : base(collection)
        {

        }
        /// <summary>
        /// konstruktori me nje parameter qe implementon klasen baze
        /// </summary>
        /// <param name="capacity"> sasia </param>
        public colProjektProdhimi(int capacity)
            : base(capacity)
        {
            
        }
         
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsProjektProdhimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsProjektProdhimi this[int index]
        {
            get { return ((clsProjektProdhimi)base[index]); }
        }
        /// <summary>
        /// kjo metode perdoret per te krijuar projektprodhimet e nenproduketeve te artikullit kryesor ne menyre rekursive
        /// </summary>
        /// <param name="prod">merr projekt prodhimin e artikullit kryesor per te cilin do te gjenerohen projekt prodhimet e nenprodukteve</param>
        /// <returns>kthen projekt prodhimet e nenproduketeve te artikullit kryesor</returns>
        public static colProjektProdhimi krijoProjektProdhimiTeNenProdukte(clsProjektProdhimi prod)
        {
            //marrim artikullin dhe recepturat e tij
            colProjektProdhimi colproj = new colProjektProdhimi();
            DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(prod.IdArtikulli);
            DbCore.DbInventari.colArtikulliPerberes colartper = new colArtikulliPerberes();
            colartper.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfertProdhim(prod.IdArtikulli, prod.Data);
            DbCore.DbInventari.colArtikujt artperberes = new DbCore.DbInventari.colArtikujt();
            artperberes.merrArtikujtPerberesSipasIdArtKryesordheDate(prod.IdArtikulli, prod.Data);
            // per cdo recepture shikojme nqs eshte artikull dhe i perket klases 5 ose 6 ( prodhim ose prodhim ne proces) dhe eshte artikull prodhim me porosi.
            // Nqs plotesohen keto kushte gjenerojme projekt prodhimin e receptures duke shumezuar sasine me koeficientin e receptures dhe ne fund therrasim perseri kete metode per te mare nenproduktet e receptures ne menyre rekursive
            foreach (clsArtikulliPerberes artp in colartper)
            {
                if (artp.Lloji == 1)
                {
                    clsArtikulli ar = artperberes.Find(x => x.IdArtikulli == artp.IdLidheseArt);
                    if ((ar.Klasa == 5 || ar.Klasa == 6) && ar.ProdhimMePorosi)
                    {
                        clsProjektProdhimi proda;
                        //nqs artikulli kryesor eshte ne njesine e dyte tek tek projekt prodhimi i gjeneruar per te recepturat e tij koeficientin e kane ne baze te njesise se pare te artikullit, per kete i shumezojme edhe me koeficientin e artikullit
                        double sasiakt = DbRegjistrim.clsKokaShitje.merrSasiTePaGjeneruarArtikulli(ar.IdArtikulli, prod.IdTrupiShitje);//kjo nuk funksionon ne raste kur recepturat jane te klases prodhim 
                        double sasiaporositur = (art.Njesi1Artikulli == prod.IdNjesi) ? prod.SasiaPorositur * double.Parse(artp.Koeficienti.ToString()) : prod.SasiaPorositur * double.Parse((artp.Koeficienti * art.KoeficientArtikulli).ToString());
                        proda = new clsProjektProdhimi(prod.Id + colproj.Count + 1, 
                                                       prod.IdKoka, 
                                                       ar.IdArtikulli, 
                                                       prod.NrProjekti.Split('-')[0] + '-' + prod.NrProjekti.Split('-')[1] + '/' + ar.KodArtikulli + '-' + new Random().Next(100000) + prod.Id, 
                                                       ar.PershkrimArtikulli, 
                                                       (art.Njesi1Artikulli == prod.IdNjesi) ? prod.SasiAktuale * double.Parse(artp.Koeficienti.ToString()) : prod.SasiAktuale * double.Parse((artp.Koeficienti * art.KoeficientArtikulli).ToString()),//vetem keshtu mund te funksionoje nese recepturat jane te klases prodhim
                                                       sasiaporositur, 
                                                       prod.Data, 
                                                       prod.IdKlienti, 
                                                       prod.EmerKlienti, 
                                                       prod.NrUrdherShitje, 
                                                       prod.IdMag, 
                                                       ar.Njesi1Artikulli, 
                                                       ar.KodArtikulli, 
                                                       prod.KodKlienti, 
                                                       prod.GjeresiPorositur, 
                                                       prod.GjatesiPorositur, 
                                                       prod.SasiPermase, 
                                                       prod.Shenime, 
                                                       prod.IdTrupiShitje,
                                                       prod.KodDetajim1,
                                                       prod.KodDetajim2);

                        colproj.Add(proda);
                        colproj.AddRange(colProjektProdhimi.krijoProjektProdhimiTeNenProdukte(proda));
                    }
                }
            }
            return colproj;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit trupi planifikim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushTrupatPlanifikim(DataTable dt)
        {
            try
            {

                //foreach (DataRow rreshti in dt.Rows)
                //{
                //    clsProjektProdhimi trupi = new clsProjektProdhimi();
                //    trupi.(rreshti);
                //    Add(trupi);
                //}

            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion
    }
}
