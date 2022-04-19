using System.Data;

namespace DbCore.DbInventari
{
    public class colGjendjeArtikulli : System.Collections.Generic.List<clsGjendjeArtikulli>
    {
        public new clsGjendjeArtikulli this[int index]
        {
            get { return ((clsGjendjeArtikulli)base[index]); }
        }

        public colGjendjeArtikulli()
        {

        }

        public bool ktheGjendjeMinMaxArtikulli(int idArtikulli)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return mbushGjendjeArtikulli(db.ktheGjendjeMinMaxArtikulli(idArtikulli));
            }
        }

        public bool ktheGjendjeMinMaxArtikulliSipasMag(int idArtikulli, int idMagazina)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return mbushGjendjeArtikulli(db.ktheGjendjeMinMaxArtikulliSipasMag(idArtikulli, idMagazina));
            }
        }
        public bool ktheGjendjeMinMaxArtikulliSipasMag(int idArtikulli, string kodMagazina, int idNdermarrje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return mbushGjendjeArtikulli(db.ktheGjendjeMinMaxArtikulliSipasMag(idArtikulli, kodMagazina, idNdermarrje));
            }
        }
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// nepermjet kesaj metode thirret per nje metode qe ben mbushjen nga databaza
        /// <see cref="DbCore.DbInventari.clsGjendjeArtikulli"/> 
        /// </summary>
        private bool mbushGjendjeArtikulli(DataTable dt)
        {
            if (dt != null)
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    this.Add(new clsGjendjeArtikulli(rreshti));
                }
                return true;
            }
            return false;
        }
    }
}
