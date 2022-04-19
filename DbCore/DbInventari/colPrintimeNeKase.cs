using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbInventari
{
    public class colPrintimeNeKase : System.Collections.Generic.List<clsPrintimeKase>
    {
        #region Metoda Private

        /// <summary>
        /// Metode qe perdoret per te mbushur collectionin me te dhenat e databazes
        /// </summary>
        /// <param name="dt">DataTable qe kthehet nga query i databazes</param>
        /// <returns>Kthen true nese mbushja kryhet me sukses, dhe anasjelltas</returns>
        private bool mbushPrintimeKasa(DataTable dt)
        {
           
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsPrintimeKase printime = new clsPrintimeKase();
                    printime.mbushPrintimNeKase(rreshti);
                    this.Add(printime);
                }
            return true;
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Merr te gjitha regjistrimet qe kane ndodhur ne tabelen e ruajtjes se statusit te dokumentit ne kase
        /// </summary>
        /// <returns>Kthen True nese mbushja kryhet me sukses dhe anasjelltas</returns>
        public bool merrTeGjithaRegjistrimetPerKase()
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataTable tabela = dbPrintime.ktheTeGjithaRegjistrimetPerKase();
            dbPrintime.Dispose();
            return mbushPrintimeKasa(tabela);
        }

        /// <summary>
        /// Merr te gjitha regjistrimet ne kase sipas nje dyqani te caktuar dhe useri te caktuar
        /// </summary>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="idUser">Id e userit</param>
        /// <returns>Kthen True nese mbushja kryhet me sukses dhe anasjelltas</returns>
        public bool merrDergimeKaseSipasIdShop(int idShop, int idUser)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataTable tabela = dbPrintime.ktheDergimeKaseSipasIdShop(idShop, idUser);
            dbPrintime.Dispose();
            return mbushPrintimeKasa(tabela);
        }

        /// <summary>
        /// Kthen te gjitha regjistrimet e nje dyqani nga nje user i caktuar me nje status te caktuar
        /// </summary>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <param name="idUser">Id e userit</param>
        /// <returns></returns>
        public bool merrDergimeKaseSipasIdShopDheStatus(int idShop, bool statusiPrintimi, int idUser)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataTable tabela = dbPrintime.ktheDergimeKaseSipasIdShopDheStatus(idShop, statusiPrintimi, idUser);
            dbPrintime.Dispose();
            return mbushPrintimeKasa(tabela);
        }
        /// <summary>
        /// Kthen te gjitha regjistrimet e nje dyqani nga nje user i caktuar me nje status te caktuar
        /// </summary>
        /// <param name="idShop">Id e dyqanit</param>
        /// <param name="statusiPrintimi">Nese kerkojme te printuar(true), ose nese kerkojme te paprintuarat(false) ne kase</param>
        /// <param name="idUser">Id e userit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <returns></returns>
        public bool ktheDergimeKaseSipasIdShopIdNdermDheStatus(int idShop, bool statusiPrintimi, int idUser, int idndermarje, int idnivel)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataTable tabela = dbPrintime.ktheDergimeKaseSipasIdShopIdNdermDheStatus(idShop, statusiPrintimi, idUser, idndermarje, idnivel);
            dbPrintime.Dispose();
            return mbushPrintimeKasa(tabela);
        }
        public bool ktheDergimeKaseSipasIdShopIdNdermDheStatusBanka(int idShop, bool statusiPrintimi, int idUser, int idndermarje)
        {
            clsDatabaseInventari dbPrintime = new clsDatabaseInventari();
            DataTable tabela = dbPrintime.ktheDergimeKaseSipasIdShopIdNdermDheStatusBanka(idShop, statusiPrintimi, idUser, idndermarje);
            dbPrintime.Dispose();
            return mbushPrintimeKasa(tabela);
        }

        #endregion
    }
}
