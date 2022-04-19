using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbBuxheti
{
    public class ColBKomponenteVlere : List<ClsBKomponenteVlere>, IDataBaseReader
    {
        #region Constructors

        public ColBKomponenteVlere() { }

        public ColBKomponenteVlere(int idKoka, bool ngaGjenerimi)
        {
            this.Add(new ClsBKomponenteVlere(0, idKoka, 0, 0, 0, null, null, 0, 0, 0, null, null, null, ngaGjenerimi, String.Empty, String.Empty));
        }

        public ColBKomponenteVlere(int idNdermarrje, int idPerdoruesi, int idNjesia)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.merrKomponenteBuxhetiVlereSipasNdermarrjes(idNdermarrje, idPerdoruesi, idNjesia, this);
        }

        #endregion

        #region Public

        public clsMesazh Ruaj()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

                using (var scope = new MyTransactionScope())
                {
                    var mesazh = Valido();
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajVlereKomponente();
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajVlereKomponenteNeHistorik(false);
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();
                }
                return new MesazhSuksesi("Ruajtja perfundoi me sukses");
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Modifiko()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

                using (var scope = new MyTransactionScope())
                {
                    var mesazh = Valido();
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajVlereKomponente();
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajVlereKomponenteNeHistorik(false);
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();
                }

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi("Modifikimi perfundoi me sukses");
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Fshi()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

                var firstItem = this.First();
                if (firstItem == null)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ska asnje vlere per te fshire.");

                var mesazh = RuajVlereKomponenteNeHistorik(firstItem.NgaGjenerimi);
                if (!mesazh)
                    return mesazh;

                mesazh = FshiVlereKomponente(firstItem.NgaGjenerimi);
                if (!mesazh)
                    return mesazh;


                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi("Fshirja perfundoi me sukses");
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh FshiDheRiruaj(bool ngaGjenerimi)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

                using (var scope = new MyTransactionScope())
                {
                    var mesazh = FshiVlereKomponente(ngaGjenerimi);
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajVlereKomponente();
                    if (!mesazh)
                        return mesazh;

                    mesazh = RuajVlereKomponenteNeHistorik(ngaGjenerimi);
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();
                }

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi("Ruajtja perfundoi me sukses");
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public void Mbush(IDataRecord record) => Add(new ClsBKomponenteVlere(record));

        #endregion

        #region Private

        private clsMesazh Valido()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ClsBKomponenteVlere first = this.First(); ClsBKomponenteVlere ekzistuese;
            ColBKomponenteVlere komponenteEkzistuese = new ColBKomponenteVlere(first.IdNdermarrje, first.IdModifikuesi > 0 ? first.IdModifikuesi : first.IdKrijuesi, 3);
            foreach(var rresht in this)
            {
                ekzistuese = komponenteEkzistuese.FirstOrDefault(x => x.IdKomponente == rresht.IdKomponente && x.IdQendraShendetesore == rresht.IdQendraShendetesore);
                if (ekzistuese != null && ekzistuese.Id > 0 && ((rresht.DtKrijimi == null && ekzistuese.DtKrijimi != null) || (rresht.DtModifikimi != ekzistuese.DtModifikimi)))
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Vlerat e komponenteve jane ndryshuar. Ju lutem rihapeni dhe njehere ambientin!");
                if ((rresht.VleraMin != null || rresht.VleraMax != null) && rresht.Tipi == null)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgPlotesoLlojKufizimiPerKomponente"] + rresht.Komponente + ", " + MessagesResource.Messages["qenderShendetesore"] + ": " + rresht.QendraShendetesore);
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi();
        }

        private clsMesazh RuajVlereKomponente()
        {
            var dt = this.ToDataTable("Id", "IdKoka", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi", "IdKomponente", "IdQendraShendetesore", "Vlera", "VleraMin", "VleraMax", "Tipi", "NgaGjenerimi");
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajVlereKomponente(dt);
        }

        private clsMesazh RuajVlereKomponenteNeHistorik(bool ngaGjenerimi)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajVlereKomponenteNeHistorik(this.First(), ngaGjenerimi);
        }

        private clsMesazh FshiVlereKomponente(bool ngaGjenerimi)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.FshiVlereKomponente(this.First(), ngaGjenerimi);
        }

        #endregion
    }
}
