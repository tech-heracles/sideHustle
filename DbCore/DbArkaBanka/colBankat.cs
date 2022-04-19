using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbKontabiliteti;

namespace DbCore.DbArkaBanka
{
    public class colBankat : List<clsBanka>
    {

        #region Metoda Publike
      
        public static DataRow merrSipasABNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idllogari)
        {
            clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka();
            return dbartikuj.ktheABNdermarrjesAndAutorizimeDR(idnderm, idperdorues, idllogari);
        }

        public static DataTable merrSipasABNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka();
            return dbartikuj.ktheABNdermarrjesAndAutorizimeDT(idnderm, idperdorues, false);
        }

        public static DataTable merrSipasABNdermarrjesAndAutorizimeDTLupa(int idnderm, int idperdorues, bool MerrNdemarrjeBija, bool lupa)
        {
            clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka();
            if(!lupa)
            return dbartikuj.ktheABNdermarrjesAndAutorizimeDT(idnderm, idperdorues, MerrNdemarrjeBija);
            else
                return dbartikuj.ktheABNdermarrjesAndAutorizimeDTLupa (idnderm, idperdorues, MerrNdemarrjeBija);
        }

        public static DataTable merrSipasABNdermarrjesAndAutorizimeDTSipasFiltrit(int idnderm, int idperdorues, string filter, int startIndex, int endIndex)
        {
            clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka();
            return dbartikuj.ktheABNdermarrjesAndAutorizimeDT(idnderm, idperdorues, false);
        }
        public static DataTable merrSipasABNdermarrjesAndAutorizimeDTSipaMonedhes(int idnderm, int idperdorues, int idmonnderm, int idmonklienti)
        {
            clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka();
            return dbartikuj.ktheABNdermarrjesAndAutorizimeDTSipasMonedhes(idnderm, idperdorues, idmonnderm, idmonklienti);
        }
        public static DataTable merrSipasABNdermarrjesAndAutorizimeDTSipaMonedhesDheFiltrit(int idnderm, int idperdorues, int idmonnderm, int idmonklienti, string filter, int startIndex, int endIndex)
        {
            clsDatabaseArkaBanka dbartikuj = new clsDatabaseArkaBanka();
            return dbartikuj.ktheABNdermarrjesAndAutorizimeDTSipasMonedhesDheFiltrit(idnderm, idperdorues, idmonnderm, idmonklienti,filter,startIndex,endIndex);
        }
        public bool mbushGjitheBankatSipasAutorizimeve(int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            return mbushBanka(data.ktheGjitheBankatSipasAutorizimeve(idnderm, idperdorues));
        }

        public bool mbushGjitheBankatSipasAutorizimeveAktive(int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            return mbushBanka(data.ktheGjitheBankatSipasAutorizimeveAktive(idnderm, idperdorues));
        }

        public bool mbushGjitheBankatSipasAutorizimeveAktive(int idnderm, int idperdorues, clsDatabaseArkaBanka data)
        {
            return mbushBanka(data.ktheGjitheBankatSipasAutorizimeveAktive(idnderm, idperdorues));
        }

        public static DataTable ktheGjitheBankatSipasAutorizimeveSipasLlojit(int idnderm, int idperdorues, bool lloji, bool MerrNdemarrjeBija)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            return data.ktheGjitheBankatSipasAutorizimeveSipasLlojit(idnderm, idperdorues, lloji, MerrNdemarrjeBija);
        }

        public bool mbushGjitheBankatSipasAutorizimeveSipasLlojit(int idnderm, int idperdorues, bool lloji)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            bool sukses = mbushBanka(data.ktheGjitheArkatBankatAktiveSipasAutorizimeveDheLlojit(idnderm, idperdorues, lloji));
            data.Dispose();
            return sukses;
        }

        public bool mbushGjitheBankatSipasAutorizimeveSipasLlojit(int idnderm, int idperdorues, bool lloji, clsDatabaseArkaBanka data)
        {
            return mbushBanka(data.ktheGjitheArkatBankatAktiveSipasAutorizimeveDheLlojit(idnderm, idperdorues, lloji));            
        }

        public bool mbushGjitheBankatSipasAutorizimeveSipasLlojitAll(int idnderm, int idKategoria)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            return mbushBanka(data.ktheGjitheBankatSipasAutorizimeveSipasLlojitAll(idnderm, idKategoria==4));
        }

        public bool mbushGjitheBankatSipasAutorizimeveLupa(int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            return mbushBanka(data.ktheGjitheBankatSipasAutorizimeveLupa(idnderm, idperdorues));
        }

        public bool merrBankaPerNdermarrje(int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            return mbushBanka(data.merrBankaPerNdermarrje(idnderm, idperdorues));
        }
        public bool merrBankaPerNdermarrjePaTcr(int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            return mbushBankaPaTcr(data.merrBankaPerNdermarrje(idnderm, idperdorues));
        }

        public static DataTable mbushArkaBankaSipasFilter(string filter, long startIndex, long endIndex, bool lloji, int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            DataTable tabela = data.ktheArkaBankaMefilter(filter, startIndex, endIndex, lloji, idnderm, idperdorues);
            data.Dispose();
            return tabela;
        }

        public static List<(string EmerBanka, string NrLlogariBanka)> merrBankatENdermarrjes(int idPerdoruesi, int idNdermarje)
        {
            colBankat bankat;
            bankat = new colBankat();
            bankat.merrBankaPerNdermarrje(idNdermarje, idPerdoruesi);
            var list = new List<(string Emer, string NrLlogari)>();
            foreach (var banke in bankat)
                list.Add((banke.EmerBanka, banke.NrLlogariBanka));

            return list;
        }

        public static List<(string EmerBanka, bool ShfaqNeEinvoice, string NrLlogariBanka)> merrBankatENdermarrjesPerEinvoice(int idPerdoruesi, int idNdermarje)
        {
            colBankat bankat;
            bankat = new colBankat();
            bankat.merrBankaPerNdermarrjePaTcr(idNdermarje, idPerdoruesi);
            var list = new List<(string Emer, bool ShfaqNeEinvoice,string NrLlogari)>();
            foreach (var banke in bankat)
                list.Add((banke.EmerBanka, banke.ShfaqNeEinvoice,banke.NrLlogariBanka));

            return list;
        }
        #endregion

        #region Metoda Private

        private bool mbushBanka(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsBanka(rreshti));
            }
            return true;
        }
        private bool mbushBankaPaTcr(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsBanka(rreshti, true));
            }
            return true;
        }

        #endregion

    }
}