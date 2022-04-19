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
    public class ColBKomponenteLidhje : List<ClsBKomponenteLidhje>, IDataBaseReader
    {
        #region Constructors

        public ColBKomponenteLidhje() { }

        public ColBKomponenteLidhje(int idKomponente) {
            using (var db = new ClsDatabaseBuxheti())
                db.KtheLidhjeKomponenteSipasIdKomponente(idKomponente, this);
        }
       
        #endregion

        #region Public

        public void mbushAllTePalidhuraSipasBuxhetit(int idKomponente, int idLlojBuxheti)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.KtheLidhjeKomponenteAllTePalidhuraSipasBuxhetit(idKomponente, idLlojBuxheti, this);
        }

        public void mbushAllSipasNdermarrjes(int idNdermarrje)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.ktheLidhjeKomponenteAllSipasNdermarrjes(idNdermarrje, this);
        }

        public clsMesazh Ruaj(int idKomponente)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

                if (this.Count <= 0)
                {
                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
                    return new MesazhSuksesi(MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                }

                this.ForEach(lidhje => lidhje.IdKomponente = idKomponente);

                var mesazh = Valido(idKomponente);
                if (!mesazh)
                    return mesazh;

                mesazh = RuajLidhjeKomponente();
                if (!mesazh)
                    return mesazh;

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
                return new MesazhSuksesi(MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Modifiko(int idKomponente)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

                this.ForEach(lidhje => lidhje.IdKomponente = idKomponente);

                var mesazh = Valido(idKomponente);
                if (!mesazh)
                    return mesazh;

                mesazh = FshiLidhjeKomponente(idKomponente);
                if (!mesazh)
                    return mesazh;

                if (this.Count > 0)
                {
                    mesazh = RuajLidhjeKomponente();
                    if (!mesazh)
                        return mesazh;
                }

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
                return new MesazhSuksesi(MessagesResource.Messages["msgModifikimiMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Fshi(int idKomponente)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

                var mesazh = FshiLidhjeKomponente(idKomponente);
                if (!mesazh)
                    return mesazh;

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
                return new MesazhSuksesi(MessagesResource.Messages["msgFshirjeMeSukses"]);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public void Mbush(IDataRecord record) => Add(new ClsBKomponenteLidhje(record));

        #endregion

        #region Private

        private clsMesazh Valido(int idKomponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

            var gjitheLidhjet = MerrTeGjithaLidhjet(idKomponente);
            foreach(var item in this)
            {
                var kategoriLidhur = gjitheLidhjet.Find(x => x.IdKategoriBuxhetimi == item.IdKategoriBuxhetimi);
                if (kategoriLidhur != null)
                    return new MesazhGabimi($"Artikulli me kod {kategoriLidhur.KodKategoriBuxhetimi} per qendren shendetesore {kategoriLidhur.KodNdermarrje} eshte lidhur me nje komponente.");
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
            return new MesazhSuksesi();
        }

        private clsMesazh RuajLidhjeKomponente()
        {
            if (this.Count <= 0)
                return new MesazhSuksesi();

            var dt = this.ToDataTable("Id", "IdKomponente", "IdNdermarrje", "IdKategoriBuxhetimi");
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajLidhjeKomponente(dt);
        }

        private ColBKomponenteLidhje MerrTeGjithaLidhjet(int idKomponente)
        {
            var teGjithaLidhjet = new ColBKomponenteLidhje();
            using (var db = new ClsDatabaseBuxheti())
                db.MerrTeGjithaLidhjetKomponenteve(idKomponente, teGjithaLidhjet);
            return teGjithaLidhjet;
        }

        #endregion

        #region Internal

        internal clsMesazh FshiLidhjeKomponente(int idKomponente)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.FshiLidhjeKomponente(idKomponente);
        }

        #endregion
    }
}
