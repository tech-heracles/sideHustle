using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AlphaWeb.Core.SharedKernel;

namespace DbCore.DbAdmin
{
    public class colTeDrejtat : System.Collections.Generic.List<clsTeDrejtat>
    {
        #region Metoda Publike

        public new clsTeDrejtat this[int index]
        {
            get { return ((clsTeDrejtat)base[index]); }
        }

        public bool shtoTeDrejte(clsTeDrejtat eDrejta)
        {
            base.Add(eDrejta);
            if (base.Contains(eDrejta))
                return true;
            else return false;
        }

        public bool fshiTeDrejte(clsTeDrejtat eDrejta)
        {            
            base.Remove(eDrejta);
            if (base.Contains(eDrejta))
                return false;
            else return true;
        }

        public bool fshiGjitheTeDrejtat()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteTeDrejte(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoTeDrejteNeIndeksin(int index, clsTeDrejtat eDrejta)
        {
            base.Insert(index, eDrejta);
        }

        public int indeksiTeDrejtes(clsTeDrejtat eDrejta)
        {
            return base.IndexOf(eDrejta);
        }

        public bool ekzistonEDrejta(clsTeDrejtat eDrejta)
        {
            if (base.Contains(eDrejta))
                return true;
            else return false;
        }

        public int numriTeDrejtave()
        {
            return base.Count;
        }

        public clsMesazh kaTeDrejtaTePlota(int idkomponente)
        {
            IEnumerable<clsTeDrejtat> oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                                                       where oTeDrejtat.IdKomponente.Equals(idkomponente)
                                                       select oTeDrejtat);
            oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                             where oTeDrejtat.IdDrejtaVeprim.Equals((int)LlojVeprimeshTeDrejta.Kontroll_i_Plote) && (oTeDrejtat.IdKomponente.Equals(idkomponente))
                             select oTeDrejtat);
            if (oColTeDrejtat.Count() > 0)
            {
                return new clsMesazh(true);
            }
            else
            {
                return new clsMesazh(false);
            }
        }

        public clsMesazh kaTeDrejtaTeLexoje(int idkomponente)
        {
            IEnumerable<clsTeDrejtat> oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                                                       where oTeDrejtat.IdKomponente.Equals(idkomponente)
                                                       select oTeDrejtat);
            oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                             where oTeDrejtat.IdDrejtaVeprim.Equals((int)LlojVeprimeshTeDrejta.Lexim) && (oTeDrejtat.IdKomponente.Equals(idkomponente))
                             select oTeDrejtat);
            if (oColTeDrejtat.Count() > 0)
            {
                return new clsMesazh(true);
            }
            else
            {
                return new clsMesazh(false);
            }
        }

        public clsMesazh kaTeDrejtaTeModifikoje(int idkomponente)
        {
            IEnumerable<clsTeDrejtat> oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                                                       where oTeDrejtat.IdKomponente.Equals(idkomponente)
                                                       select oTeDrejtat);
            oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                             where oTeDrejtat.IdDrejtaVeprim.Equals((int)LlojVeprimeshTeDrejta.Modifikim) && (oTeDrejtat.IdKomponente.Equals(idkomponente))
                             select oTeDrejtat);
            if (oColTeDrejtat.Count() > 0)
            {
                return new clsMesazh(true);
            }
            else
            {
                return new clsMesazh(false);
            }
        }

        public clsMesazh kaTeDrejtaTeFshije(int idkomponente)
        {
            IEnumerable<clsTeDrejtat> oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                                                       where oTeDrejtat.IdKomponente.Equals(idkomponente)
                                                       select oTeDrejtat);
            oColTeDrejtat = (from clsTeDrejtat oTeDrejtat in this
                             where oTeDrejtat.IdDrejtaVeprim.Equals((int)LlojVeprimeshTeDrejta.Fshirje) && (oTeDrejtat.IdKomponente.Equals(idkomponente))
                             select oTeDrejtat);
            if (oColTeDrejtat.Count() > 0)
            {
                return new clsMesazh(true);
            }
            else
            {
                return new clsMesazh(false);
            }
        }

        public bool mbushTeDrejtePerdorues(int IdPerdorues, int idnermarrjeviti)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushTeDrejtat(data.ktheTeDrejtePerdorues(IdPerdorues, idnermarrjeviti));
            data.Dispose();
            return sukses;
        }

        public bool mbushTeDrejtePerdoruesGjitheNdermarrjet(int IdPerdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushTeDrejtat(data.ktheTeDrejtePerdoruesGjitheNdermarrjet(IdPerdorues));
            data.Dispose();
            return sukses;
        }

        public bool mbushTeDrejteGrupPerdoruesGjitheNdermarrjet(int idGrupiPerdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushTeDrejtat(data.ktheTeDrejteGrupPerdoruesGjitheNdermarrjet(idGrupiPerdorues));
            data.Dispose();
            return sukses;
        }

        public bool mbushTeDrejteGrupPerdorues(int idGrupiPerdorues, int idndermarrjeviti)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushTeDrejtat(data.ktheTeDrejteGrupPerdorues(idGrupiPerdorues, idndermarrjeviti));
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushTeDrejtat(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTeDrejtat eDrejta = new clsTeDrejtat();
                    //eDrejta.mbushTeDrejten(rreshti);
                    Add(new clsTeDrejtat(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushTeDrejtat(DataTable dt)", true)]
        public colTeDrejtat mbushArrayListTeDrejtat(DataSet ds)
        {
            colTeDrejtat teDrejtat = new colTeDrejtat();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTeDrejtat eDrejta = new clsTeDrejtat();

                eDrejta.IdDrejta = int.Parse(rreshti[0].ToString());
                eDrejta.IdNderViti = int.Parse(rreshti[1].ToString());
                eDrejta.IdKomponente= int.Parse(rreshti[2].ToString());
                eDrejta.IdModul = int.Parse(rreshti[3].ToString());
                eDrejta.IdPerdorues = int.Parse(rreshti[4].ToString());
                eDrejta.IdDrejtaVeprim = int.Parse(rreshti[5].ToString());
                eDrejta.IdAmbjentiModuli = int.Parse(rreshti[6].ToString());
                eDrejta.PerdoruesApoGrup = int.Parse(rreshti[7].ToString());
                //eDrejta.AmbjentiModuli = rreshti[7].ToString();
                //eDrejta.Komponente = rreshti[8].ToString();

                teDrejtat.Add(eDrejta);
            }
            return teDrejtat;
        }

    }
}
