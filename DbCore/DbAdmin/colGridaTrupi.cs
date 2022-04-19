using System.Linq;
using System.Data;
using DbCore.DbShare;

namespace DbCore.DbAdmin
{
    public class colGridaTrupi : System.Collections.Generic.List<clsGridaTrupi>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colGridaTrupi()
        {
        }

        public colGridaTrupi(int idKonfigurim, int idGjuha)
        {
            using (var data = new clsDatabaseAdmin())
                MbushGridaTrupa(data.merrGridenKonfigurimit(idKonfigurim, idGjuha));
        }

        public colGridaTrupi(int idKomponente, int idKonfigurim, int idGjuha)
        {
            using (var data = new clsDatabaseAdmin())
                MbushGridaTrupa(data.merrGridenKonfigurimitKomponentesSipasGjuhes(idKomponente, idKonfigurim, idGjuha));
        }
        
        public colGridaTrupi(string emriGrides, int idKomponente, int idKonfigurim, int idGjuha) : base(new clsDatabaseAdmin().merrGridenKonfigurimitKomponentesSipasGridesNew(emriGrides, idKomponente, idKonfigurim, idGjuha))
        {
        }

        #endregion

        #region Metoda Publike

        public new clsGridaTrupi this[int index] => base[index];

        /// <summary>
        /// mbush trupin sipas id kokes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idGrida">id koka e grides</param>
        /// <returns>kthen true nese mbushja kryhet me sukses</returns>
        public bool mbushTrupin(int idGjuha, int idGrida)
        {
            using (var data = new clsDatabaseAdmin())
                return MbushGridaTrupa(data.ktheTrupin(idGjuha, idGrida));
        }

        /// <summary>
        /// mbush trupin sipas id kokes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idGrida">id koka e grides</param>
        /// <returns>kthen true nese mbushja kryhet me sukses</returns>
        public void mbushTrupinEng(int idGjuha, int idGrida)
        {
            using (var data = new clsDatabaseAdmin())
                MbushGridaTrupaEng(data.ktheTrupin(idGjuha, idGrida));
        }

        public void mbushGrideTrupin(int idKomponente, int idKonfigurim, int idGjuha)
        {
            using (var data = new clsDatabaseAdmin())
                MbushGridaTrupaEng(data.merrGridenKonfigurimitKomponentes(idKomponente, idKonfigurim, idGjuha));
        }

        public void mbushGrideTrupinSipasEmerGride(int idKonfigurim, string emriGrides, int idGjuha)
        {
            using (var data = new clsDatabaseAdmin())
                MbushGridaTrupaEng(data.merrGrideTrupinSipasEmerGride(idKonfigurim, emriGrides, idGjuha));
        }

        public void mbushGrideTrupinSipasEmerGrideDheKomponente(int idKonfigurim, string emriGrides, int idGjuha, string emerKomponente)
        {
            using (var data = new clsDatabaseAdmin())
                MbushGridaTrupaEng(data.merrGrideTrupinSipasEmerGrideDheKomponente(idKonfigurim, emriGrides, idGjuha, emerKomponente));
        }

        /// <summary>
        /// metode per updatetimin e fushave te grides
        /// </summary>
        /// <returns>kthen true ose false nqs ruajtja ka perfunduar me sukses</returns>
        public bool Update(int idGjuha)
        {
            var dbAdmin = new clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            
            if (!update(dbAdmin, idGjuha))
            {
                dbAdmin.rollbackTransaksion();
                return false;
            }

            dbAdmin.commitTransaksion();
            return true;
        }

        /// <summary>
        /// metode per updatimin e fushave te grides si pjese e transaksionit
        /// </summary>
        /// <param name="dbAdmin">clsDatabaseAdmin per transaksion</param>
        /// <returns>kthen true ose false nqs ruajtja ka perfunduar me sukses</returns>
        public bool update(clsDatabaseAdmin dbAdmin, int idGjuha) => 
            this.All(gridElem => gridElem.ruajTrup(dbAdmin, idGjuha).Status);

        public static colGridaTrupi KriColGridaTrupi(string kode, string kodiTrupi, int idNdermarrja)
        {
            var oColTrupiLupat = new colGridaTrupi();
            var lupat = kode.Split('-');
            foreach (var lupe in lupat)
            {
                if (lupe == "")
                    continue;
                var idKonfigAmb = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(lupe, idNdermarrja);

                if (idKonfigAmb <= 0)
                    throw new MyException("Lloji i konfigurimit " + lupe + " nuk ekziston!");

                if (oColTrupiLupat.FirstOrDefault(x => x.KodLupa == lupe && x.IdKonfigAmbjenteLupa == idKonfigAmb && x.KodiTrupi == kodiTrupi) != null)
                    continue; // nqs ekziston njehere lupa me keto te dhena ekzistuese te mos shtohet serish

                var oGrTrupi = new clsGridaTrupi
                {
                    KodLupa = lupe,
                    IdKonfigAmbjenteLupa = idKonfigAmb,
                    KodiTrupi = kodiTrupi
                };

                oColTrupiLupat.Add(oGrTrupi);
            }

            return oColTrupiLupat;
        }

        #endregion

        #region Metoda Private

        private bool MbushGridaTrupa(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add(new clsGridaTrupi(rreshti));

            return true;
        }

        private void MbushGridaTrupaEng(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                var trupi = new clsGridaTrupi();
                trupi.mbushGridTrupiEng(rreshti);
                Add(trupi);
            }
        }

        public void RregulloRenditje()
        {
            var col = this.OrderBy(x => x.IndexTrupi).ToList();
            for (int i = 0; i < col.Count; i++)
                col[i].IndexTrupi = i;
        }

        #endregion
    }
}
