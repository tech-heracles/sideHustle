using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbArkaBanka
{
    public class colVeprimBankaTrupi : System.Collections.Generic.List<clsVeprimBankaTrupi>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colVeprimBankaTrupi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        public colVeprimBankaTrupi(int idkoka)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            mbushVeprimeBankaTrupi(data.ktheTrupinSipasVeprimBankeKoka(idkoka));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsVeprimBankaTrupi this[int index]
        {
            get { return ((clsVeprimBankaTrupi)base[index]); }
        }
        public DbCore.DbKontabiliteti.colKlienteFurnitore ktheColKF()
        {
            DbCore.DbKontabiliteti.colKlienteFurnitore colKF = new DbCore.DbKontabiliteti.colKlienteFurnitore();
            foreach (clsVeprimBankaTrupi trupMag in this)
            {
                if (trupMag.Lloji == "Klient" || trupMag.Lloji == "Furnitor")
                {
                    DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(trupMag.IdSubjekti);
                    colKF.Add(kf);
                }
                else colKF.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor());
            }
            return colKF;
        }
        public DbCore.DbKontabiliteti.colLlogarite ktheColLLogari()
        {
            DbCore.DbKontabiliteti.colLlogarite colLLogari = new DbCore.DbKontabiliteti.colLlogarite();
            foreach (clsVeprimBankaTrupi trupMag in this)
            {
                if (trupMag.Lloji == "Llogari")
                {
                    DbCore.DbKontabiliteti.clsLlogari llog = new DbCore.DbKontabiliteti.clsLlogari(trupMag.IdSubjekti);
                    colLLogari.Add(llog);
                }
                else colLLogari.Add(new DbCore.DbKontabiliteti.clsLlogari());
            }
            return colLLogari;
        }


        public DbCore.DbListPagesat.colPunonjes ktheColPunonjes()
        {
            DbCore.DbListPagesat.colPunonjes colPunonjes = new DbCore.DbListPagesat.colPunonjes();
            foreach (clsVeprimBankaTrupi trupMag in this)
            {
                if (trupMag.Lloji == "Punonjes")
                {
                    DbCore.DbListPagesat.clsPunonjes pun = new DbCore.DbListPagesat.clsPunonjes(trupMag.IdSubjekti);
                    colPunonjes.Add(pun);
                }
                else colPunonjes.Add(new DbCore.DbListPagesat.clsPunonjes());
            }
            return colPunonjes;
        }



        public DbCore.DbRegjistrim.colKokaShitje ktheColShitje(int idndermarje)
        {
            DbCore.DbRegjistrim.colKokaShitje col = new DbCore.DbRegjistrim.colKokaShitje();
            foreach (clsVeprimBankaTrupi trupBanke in this)
            {
                if (trupBanke.IdFatura != 0)
                {
                    //DbCore.DbRegjistrim.clsNivelRegjistrimi niveli = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                    // niveli.mbushNivelRegjistrimiSipasID(trupMag.IdNivel);
                     int idKategori = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(trupBanke.IdNivel);
                     if (idKategori != 20)
                    {
                        DbCore.DbRegjistrim.clsKokaShitje fature = new DbCore.DbRegjistrim.clsKokaShitje();
                        fature.mbushKokaShitjeSipasIDPaTrup(trupBanke.IdFatura);
                        col.Add(fature);

                    }
                    else
                    {
                        col.Add(new DbCore.DbRegjistrim.clsKokaShitje());

                    }

                }
                else col.Add(new DbCore.DbRegjistrim.clsKokaShitje());
            }
            return col;
        }
        public DbCore.DbRegjistrim.colVeprimeKFKoka ktheColVeprimekf(int idndermarje)
        {
            DbCore.DbRegjistrim.colVeprimeKFKoka col = new DbCore.DbRegjistrim.colVeprimeKFKoka();
            DbCore.DbRegjistrim.clsVeprimeKFKoka newv = new DbCore.DbRegjistrim.clsVeprimeKFKoka();
            newv.ColVeprimeKFTrupi = new DbCore.DbRegjistrim.colVeprimeKFTrupi();
            foreach (clsVeprimBankaTrupi trupMag in this)
            {
                if (trupMag.IdFatura != 0)
                {

                    //DbCore.DbRegjistrim.clsNivelRegjistrimi niveli = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                    //niveli.mbushNivelRegjistrimiSipasID(trupMag.IdNivel);
                    int idKategori = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(trupMag.IdNivel);
                    if (idKategori == 20)
                    {
                        DbCore.DbRegjistrim.clsVeprimeKFKoka fature = new DbCore.DbRegjistrim.clsVeprimeKFKoka(trupMag.IdFatura);
                      
                        col.Add(fature);

                    }
                    else
                    {
                        col.Add(newv);

                    }

                }
                else col.Add(newv);
            }
            return col;
        }
        public List<int> ktheIdFature()
        {
            List<int> idte = new List<int>();
            idte.Add(0);//shtohet sepse grida e jquerit fillon nga 1
            foreach (clsVeprimBankaTrupi trupMag in this)
            {
                idte.Add(trupMag.IdFatura);
            }
            return idte;
        }
        public List<int> ktheNivele()
        {
            List<int> idte = new List<int>();
            idte.Add(0);//shtohet sepse grida e jquerit fillon nga 1
            foreach (clsVeprimBankaTrupi trupVeprimBanka in this)
            {
                idte.Add(trupVeprimBanka.IdNivel);
            }
            return idte;
        }
        public List<double[]> ktheKMK()
        {
            List<double[]> idte = new List<double[]>();
          
            foreach (clsVeprimBankaTrupi trupMag in this)
            {
                double[] vlerat = { trupMag.IdFatura, trupMag.IdNivel, trupMag.KMK };
                idte.Add(vlerat);
            }
            return idte;
        }

        #endregion

        #region Metoda Private

        private bool mbushVeprimeBankaTrupi(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsVeprimBankaTrupi(rreshti));
            }
            return true;
        }

        #endregion       
    }
}