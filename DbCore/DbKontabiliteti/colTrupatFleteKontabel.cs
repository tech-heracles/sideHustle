using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Script.Serialization;
using DbCore.DbAdmin;
using DbCore.DbQendraKosto;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiFleteKontabel
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupatFletetKontabel : List<clsTrupiFleteKontabel>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colTrupatFletetKontabel()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idKoka">id e kokes</param>
        public colTrupatFletetKontabel(int idKoka)
        {
            using (var dbTrupFleteKont = new clsDatabaseKontabilitet())
                MbushTrupFleteveKontabel(dbTrupFleteKont.ktheTrupatFleteKontabelSipasKokes(idKoka));
        }

        public colTrupatFletetKontabel(int idKoka, clsDatabaseKontabilitet dbTrupFleteKont)
        {
            MbushTrupFleteveKontabel(dbTrupFleteKont.ktheTrupatFleteKontabelSipasKokes(idKoka));
        }

        public colLlogarite ktheColLlogari()
        {
            var colLlog = new colLlogarite();
            colLlog.AddRange(this.Select(trup => new clsLlogari(trup.IdLlogari)));
            return colLlog;
        }

        public DbAdmin.colMonedhat KtheColMonedha()
        {
            var colMon = new DbAdmin.colMonedhat();
            colMon.AddRange(this.Select(trup => new DbAdmin.clsMonedha(trup.IdMonedha)));
            return colMon;
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsTrupiFleteKontabel"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiFleteKontabel this[int index]
        {
            get { return ((clsTrupiFleteKontabel)base[index]); }
        }
        
        public void VendosIdKokeNeTrup(int idKoka)
        {
            ForEach(x => x.IdKokaFleteKontabel = idKoka);
        }
        
        public bool mbushTrupiSipasKokesPerQK(int idkoka)
        {
            using (var dbTrupFleteKont = new clsDatabaseKontabilitet())
                return MbushTrupFleteveKontabel(dbTrupFleteKont.ktheTrupatFleteKontabelSipasKokesPerQK(idkoka));
        }

        public bool mbushTrupiSipasKokesPerQK(int idkoka, clsDatabaseKontabilitet dbTrupFleteKont) =>
            MbushTrupFleteveKontabel(dbTrupFleteKont.ktheTrupatFleteKontabelSipasKokesPerQK(idkoka));

        public static Tuple<colTrupatFletetKontabel, bool> KrijoTrup(int idndermarje, 
            out colObjektivaKosto objektivat, 
            out List<double> vleratobjektiva, 
            out List<double> vleratobjektivamonbaze, 
            out List<int> idllogobj,
            object[] dokumenti,
            bool azhornim,
            DateTime data)
        {
            objektivat = new colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var trupat = new colTrupatFletetKontabel();
            var ndryshimKursi = false;
            
            foreach (Dictionary<string, object> t in dokumenti)
            {
                var nrllog = t["txtNrllogarie"].ToString();
                var emerllog = t["txtEmertimi"].ToString();
                var pershkrimi = t["txtPershkrimi"].ToString();
                var mon = t["txtMonedha"].ToString();
                var kursi = t["txtKursi"].ToString();
                var debi = t["txtVldebi"].ToString();
                var kredi = t["txtVlkredi"].ToString();
                var debimon = t["txtVlmondebi"].ToString();
                var kredimon = t["txtVlmonkredi"].ToString();
                var trup = new clsTrupiFleteKontabel(idndermarje, azhornim, clsNdermarrje.ktheIdMonedheNdermSipasID(idndermarje), nrllog, emerllog, pershkrimi, mon,
                    kursi, debi, kredi, debimon, kredimon, data);
                if (trup.IdLlogari == -1) continue;
                var kursiifundit = new clsKurset(trup.IdMonedha, data);
                var diferenca = Math.Abs(kursiifundit.VleraKursi - trup.Kursi);
                ndryshimKursi = diferenca / kursiifundit.VleraKursi > 0.2;
                trupat.Add(trup);
            }
            return new Tuple<colTrupatFletetKontabel, bool>(trupat, ndryshimKursi);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbKontabiliteti.clsTrupiFleteKontabel"/> 
        /// </summary>
        private bool MbushTrupFleteveKontabel(DataTable dt)
        {
            
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiFleteKontabel(rreshti));
            }
            return true;
        }

        #endregion
    }
}