using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using DbCore.DbQendraKosto;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.DataBase;
using System.Data.SqlClient;
using DbCore.DbKontabiliteti;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsAmortizimiKoka (objekte per ruajtjen e kokave te dokumentave te amortizimit) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_AMORTIZIMI_KOKA.
    /// </summary>
    public class colAmortizimiKoka : List<clsAmortizimiKoka>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsAmortizimiKoka per kokat e dokumentave te amortizimit.
        /// </summary>
        public colAmortizimiKoka()
        {
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruajListAmortizimeTrans(int iddokumentigjenerues, int meKontabilizim, out string shfaqmesazhapolupe, colAmortizimiKoka colAmortizimetEVjetra, int idPeriudha, int idkategoria, colSerialetMagazine colSerialetMagazine, bool ngarivleresimi, colAmortizimiFillestar colaqt, IDictionary<string, object> hfregjistrime, ResourceManager rm, CultureInfo ci, bool modifikim,out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
               clsMesazh pergjigja;
            using (var scope = new MyTransactionScope())
            {
                if (Count == 1)
                {
                    int idamortizimiparaardhes = 0;
                    shfaqmesazhapolupe = "Jo";
                    pergjigja = ruajAmortiziminNgaLista(this[0], 0, iddokumentigjenerues, meKontabilizim, ref idamortizimiparaardhes, 0, colAmortizimetEVjetra, idPeriudha, idkategoria, colSerialetMagazine, ngarivleresimi, hfregjistrime, modifikim, ref shfaqmesazhapolupe, out mesazhmevonshem);
                    if (pergjigja.Status)
                    {
                        foreach (clsAmortizimiFillestar ser in colaqt)
                        {
                            ser.IdDokNga = idamortizimiparaardhes;
                            pergjigja = ser.modifiko();
                            if (!pergjigja.Status)
                                return pergjigja;
                        }
                    }
                }
                else
                    pergjigja = ruajListAmortizime(iddokumentigjenerues, meKontabilizim, out shfaqmesazhapolupe, colAmortizimetEVjetra, idPeriudha, idkategoria, colSerialetMagazine, ngarivleresimi, hfregjistrime, modifikim, out mesazhmevonshem);
                if (!pergjigja.Status)
                    return pergjigja;
                scope.Complete();

            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan te gjitha objektet e koleksionit te amortizimeve.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="iddokumentigjenerues">(int) Merr id e dokumentit gjenerues.</param>
        /// <param name="meKontabilizim">(int) Nese ruajtja ka kontabilizim apo jo.</param>
        /// <param name="shfaqmesazhapolupe">(string) Mesazhin qe duhet te shfaqe.</param>
        /// <param name="colAmortizimetEVjetra">(colAmortizimiKoka) Merr koleksionin e kokave te vjetra te amortizimeve.</param>
        /// <param name="idPeriudha">(int) Merr id e periudhes.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        /// <param name="idkategoria"></param>
        /// <param name="colSerialetMagazine"></param>
        /// <param name="ngarivleresimi"></param>
        public clsMesazh ruajListAmortizime(int iddokumentigjenerues, int meKontabilizim, out string shfaqmesazhapolupe, colAmortizimiKoka colAmortizimetEVjetra, int idPeriudha, int idkategoria, colSerialetMagazine colSerialetMagazine, bool ngarivleresimi, IDictionary<string, object> hfregjistrime, bool modifikim, out string mesazhmevonshem)
        {
            string tmpmevonshem=   mesazhmevonshem = "";
            int mekont = 0;
            string tmpShfaqmesazhapolupe = shfaqmesazhapolupe = "Jo";
            if (Count != 0)
            {
                int idamortizimiparaardhes = 0;//duhet per rastet e transferimit
                clsMesazh pergjigja = new clsMesazh(true, MessagesResource.Messages["msgRegjistrimiIListesSeAmortizimeveUKryeMeSukses"]);
                int rreshti = 0;
                foreach (clsAmortizimiKoka amortizimi in this)
                {
                    pergjigja = ruajAmortiziminNgaLista(amortizimi, mekont, iddokumentigjenerues, meKontabilizim, ref idamortizimiparaardhes, rreshti, colAmortizimetEVjetra, idPeriudha, idkategoria, colSerialetMagazine, ngarivleresimi, hfregjistrime, modifikim, ref tmpShfaqmesazhapolupe, out tmpmevonshem);
                    if (shfaqmesazhapolupe == "Jo")
                        shfaqmesazhapolupe = tmpShfaqmesazhapolupe;
                    if (mesazhmevonshem == "")
                        mesazhmevonshem = tmpmevonshem;
                    if (!pergjigja.Status) {
                        shfaqmesazhapolupe = "Jo";
                        return pergjigja;
                    }
                    rreshti++;
                }
                return pergjigja;
            }
            return new clsMesazh(false, MessagesResource.Messages["msgNukEkzistonAsnjeAmortizimPerTuRegjistruar"]);
        }

        public clsMesazh ruajAmortiziminNgaLista(clsAmortizimiKoka amortizimi, int mekont, int iddokumentigjenerues, int meKontabilizim, ref int idamortizimiparaardhes, int rreshti, colAmortizimiKoka colAmortizimetEVjetra, int idPeriudha, int idkategoria, colSerialetMagazine colSerialetMagazine, bool ngarivleresimi, IDictionary<string, object> hfregjistrime, bool modifikim, ref string shfaqmesazhapolupe, out string  mesazhmevonshem)
        {
            mesazhmevonshem = "";
               clsMesazh pergjigja = new clsMesazh(true, MessagesResource.Messages["msgRegjistrimiIListesSeAmortizimeveUKryeMeSukses"]);
            mekont = meKontabilizim;//ia kalojme nje varibli tjeter qe te mos humbim rastin e kontabilizimit per standartet e tjera
            amortizimi.IdDokGjenerues = iddokumentigjenerues;
            if (amortizimi.IdNivelGjenerues == amortizimi.IdNiveli)
            {
                amortizimi.IdDokGjenerues = idamortizimiparaardhes;//per hyrjet nuk ka kontabilizim
                mekont = 0;
            }
            if (rreshti != 0)
            {
                amortizimi.OKokaMagazina = new clsKokaMagazina();
            }
            if (colAmortizimetEVjetra.Count > rreshti)
            {
                amortizimi.IdDokNga = colAmortizimetEVjetra[rreshti].IdAmortizimi;
                amortizimi.NrRenditje = colAmortizimetEVjetra[rreshti].NrRenditje;
                pergjigja = amortizimi.ruaj(mekont, colAmortizimetEVjetra[rreshti].OFleteKontabel.IdKokaFleteKontabel, out shfaqmesazhapolupe, colAmortizimetEVjetra[rreshti].OFleteKontabel.KokaQendraKosto.IdKoka, colAmortizimetEVjetra[rreshti].OFleteKontabel.KokaQendraKosto.ColTrupi, idPeriudha, idkategoria, colSerialetMagazine, ngarivleresimi, hfregjistrime, modifikim, out mesazhmevonshem);
            }
            else pergjigja = amortizimi.ruaj(mekont, 0, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), idPeriudha, idkategoria, colSerialetMagazine, ngarivleresimi, hfregjistrime, modifikim, out mesazhmevonshem);
            idamortizimiparaardhes = amortizimi.IdAmortizimi;
            return pergjigja;
        }

        public clsMesazh modifikoList(int meKontabilizim, bool lidhur, int idperiudha, int idkategoria, out string shfaqmesazhapolupe, colSerialetMagazine seriale, bool ngarivleresimi, bool gjeneromagazine, DbShare.clsKonfigurimAmbjenti konfmag, ResourceManager rm, CultureInfo ci, int idLlojStandarti, out string  mesazhmevonshem)
        {
            mesazhmevonshem = "";
            clsMesazh u_modifikua = new clsMesazh();
            bool rivleresimXStandart = false;
            shfaqmesazhapolupe = "jo";
            if (lidhur == false)
            {
                using (var scope = new MyTransactionScope())
                {
                    try
                    {
                        colAmortizimiKoka kokavjeter = new colAmortizimiKoka();
                        clsAmortizimiKoka amortizimiiPareIvjeter = new clsAmortizimiKoka();
                        amortizimiiPareIvjeter.ktheAmortizimKokaSipasId(this[0].IdAmortizimi);
                        kokavjeter.ktheAmortizimKokaSipasMagDateNenKatDheNrDok(amortizimiiPareIvjeter.DateDokumenti, amortizimiiPareIvjeter.IdNjesiAdministrative, amortizimiiPareIvjeter.NrDok, amortizimiiPareIvjeter.IdNdermarrje, amortizimiiPareIvjeter.IdKonfigurimAmbjenti);
                        foreach (clsAmortizimiKoka kok in kokavjeter)
                        {
                            if (idLlojStandarti == -1 || idLlojStandarti == kok.IdLlojStandarti)
                            {
                                if (idLlojStandarti == kok.IdLlojStandarti)
                                {
                                    gjeneromagazine = false;
                                    rivleresimXStandart = true;
                                }
                                u_modifikua = kok.fshiAmortizimKokaTransaksion(this[0].IdPerdoruesi, idkategoria, this[0].ColTrupi, rivleresimXStandart);
                                if (!u_modifikua.Status)
                                    return u_modifikua;
                                rivleresimXStandart = false;
                            }
                        }
                        bool ndryshovleraseriale = true;
                        for (int i = 0; i < this.Count; i++)
                        {
                            clsAmortizimiKoka kok = this[i];
                            if (idLlojStandarti == -1 || idLlojStandarti == kok.IdLlojStandarti)
                            {
                                if (idLlojStandarti == kok.IdLlojStandarti)
                                {
                                    gjeneromagazine = false;
                                    rivleresimXStandart = true;
                                }
                                kok.krijoTrupiDokumentAmortizimiPerRivleresim(gjeneromagazine, konfmag, seriale, ndryshovleraseriale, rivleresimXStandart, out mesazhmevonshem);
                                if (rivleresimXStandart)
                                    kok.RivleresimKoka.IdDokNga = kokavjeter[i].RivleresimKoka.IdDokNga;
                                ndryshovleraseriale = false;
                                rivleresimXStandart = false;
                            }
                        }
                        this[0].OKokaMagazina.IdDokNga = kokavjeter[0].OKokaMagazina.IdKokaMagazina;//dokumenti i magazines lidhet me dokumentin e pare te amortizimit

                        u_modifikua = ruajListAmortizime(0, meKontabilizim, out shfaqmesazhapolupe, kokavjeter, idperiudha, idkategoria, seriale, ngarivleresimi, null, true, out mesazhmevonshem);

                        if (!u_modifikua.Status)
                            return u_modifikua;

                        scope.Complete();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return u_modifikua;
            }
            using (var scope = new MyTransactionScope())
            {
                clsDatabazeAsete data = new clsDatabazeAsete();
                foreach (clsAmortizimiKoka kok in this)
                {
                    u_modifikua = data.modifikoAmortizimiKoka(kok.IdAmortizimi, kok.NrDok, kok.DateDokumenti, kok.DateAmortizimi, kok.DateRegjistrimi, kok.IdNjesiAdministrative, kok.Shenime, kok.AmortizimiShteseTotal, kok.IdLlojStandarti, kok.IdStatusDokumenti, kok.IdPerdoruesi);
                    if (!u_modifikua.Status)
                        return u_modifikua;
                }
                scope.Complete();
            }
            return u_modifikua;
        }

        public bool ktheAmortizimKokaSipasMagDateNenKatDheNrDok(DateTime datedok, int idNjesiAdministrative, string nrdok, int idNdermarrje, int idkonfigambjente)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAmortizimiKokaList(moduliAsete.ktheAmortizimKokaSipasMagDateNenKatDheNrDok(idkonfigambjente, datedok, idNjesiAdministrative, nrdok, idNdermarrje));
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen te gjitha amortizimet sipas id se dokumentit gjenerues.
        /// </summary>
        /// <param name="idgjenerues">(int) Id e dokumentit gjenerues.</param>
        /// <param name="idkonfiggjenerues">(int) Id e konfigurimit gjenerues.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifikoKokatPerRillogaritje(ResourceManager rm, CultureInfo ci,int idNdermarrje, List<clsAmortizimiTrupiAbstract> lista, int maxRetry)
        {
            DbData dbData = new DbData();
            clsMesazh pergjigja = new clsMesazh(true, "Modifikimi u krye me sukses!");
            foreach (clsAmortizimiKoka kokaModifikuar in this)
            {
                clsRetryTrans retryTrans = new clsRetryTrans("ModifikimAmortizimi", maxRetry);
                do
                {
                    using (MyTransactionScope scope = new MyTransactionScope(dbData, 0))
                    {
                        try
                        {
                            //Ruan trupat e modifikuar nga rillogaritja.
                            List<clsAmortizimiTrupiAbstract> colTrupaPerKoke = lista.FindAll(x => x.IdAmortizimKoka == kokaModifikuar.IdAmortizimi);
                            foreach (clsAmortizimiTrupiAbstract trupiPerModfifikim in colTrupaPerKoke)
                            {
                                pergjigja = trupiPerModfifikim.modifikoTrupPerRillogaritje(dbData);
                                if (!pergjigja.Status)
                                    return pergjigja;
                            }

                            pergjigja = kokaModifikuar.modifikoKokaPerRillogaritje(rm, ci, dbData);
                            if (!pergjigja.Status)
                                return pergjigja;

                            scope.Complete(out dbData);

                            retryTrans.stopRetrying(); //dil se e bone
                        }
                        catch (SqlException sqlEx)
                        {
                            if (!retryTrans.checkRetry(sqlEx, "Rillogaritje amortzimi - modifikim dokumenti", maxRetry, sqlEx.Message))
                                throw sqlEx;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                } while (retryTrans.isRetrying());
            }
            return pergjigja;
        }

        
        /// <summary>
        /// MODULI ASETE:
        /// Kthen dokumentat e kokes se amortizimit ne DataTable.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit.</param>
        /// <returns>Kthen DataTable te dokumetave te kokes se amortizimit.</returns>
        public static DataTable ktheAmortizimKokaSipasDt(int idnderviti, int idperdoruesi)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAmortizimKokaDT(idnderviti, idperdoruesi);
            }

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen dokumentat e kokes se amortizimit ne DataTable sipas amortizimit fillestar.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit.</param>
        /// <returns>Kthen DataTable te dokumetave te kokes se amortizimit.</returns>
        public static DataTable ktheAmortizimKokaSipasDtAmortizimFillestar(int idnderviti, int idperdoruesi)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAmortizimKokaSipasDtAmortizimFillestar(idnderviti, idperdoruesi);
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen dokumentat e kokes se amortizimit ne DataTable sipas amortizimit fillestar.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit.</param>
        /// <returns>Kthen DataTable te dokumetave te kokes se amortizimit.</returns>
        public static DataTable ktheAmortizimFillestarPerEksport(int idNdermarrje, int idnderviti, int idperdoruesi, string idPerEksport)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAmortizimFillestarPerEksport(idNdermarrje, idnderviti, idperdoruesi, idPerEksport);
            }
        }

        public static DataTable ktheAmortizimRezervaPerEksport(int idNdermarrje, int idNdermViti, string idPerEksport)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAmortizimRezervaPerEksport(idNdermarrje, idNdermViti, idPerEksport);
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen dokumentat e kokes se amortizimit ne DataTable sipas rivleresimit.
        /// </summary>
        /// <param name="idnderviti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit.</param>
        /// <returns>Kthen DataTable te dokumetave te kokes se amortizimit.</returns>
        public static DataTable ktheAmortizimKokaSipasDtRivleresim(int idnderviti, int idperdoruesi)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAmortizimKokaSipasDtRivleresim(idnderviti, idperdoruesi);
            }

        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe kokat e dokumenteve te amortizimit ne nje magazine sipas date se amortizimit ne trupin e dokumentit.
        /// </summary>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se kokave te dokumentit ose False ne te kundert.</returns>
        public bool merrAmortizimKokaSipasMagDateAmortSipasTrupi(DateTime dateAmortizimi, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAmortizimiKokaList(moduliAsete.ktheAmortizimKokaSipasMagDateAmortSipasTrupi(dateAmortizimi, idNjesiAdministrative, idLlojStandarti, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe kokat e dokumenteve te amortizimit ne nje magazine sipas date se amortizimit.
        /// </summary>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen po behet regjistrimi i dokumentit te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public bool merrAmortizimKoka(DateTime dateAmortizimi, int idNjesiAdministrative, int idLlojStandarti, int idNdermarrje)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return mbushAmortizimiKokaList(moduliAsete.ktheAmortizimKoka(dateAmortizimi, idNjesiAdministrative, idLlojStandarti, idNdermarrje));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit per te cilen po behet llogaritja e amortizimit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public bool merrAmortizimKoka(DateTime dateAmortizimi, int idLlojStandarti, int idNdermarrje)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return mbushAmortizimiKokaList(moduliAsete.ktheAmortizimKoka(dateAmortizimi, idLlojStandarti, idNdermarrje));
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen te gjitha amortizimet sipas id se dokumentit gjenerues.
        /// </summary>
        /// <param name="idgjenerues">(int) Id e dokumentit gjenerues.</param>
        /// <param name="idkonfiggjenerues">(int) Id e konfigurimit gjenerues.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje problem gjate marrjes se amortizimit, ne te kundert False.</returns>
        public bool ktheAmortizimKokaSipasIdGjeneruesi(int idgjenerues, int idkonfiggjenerues)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAmortizimiKokaList(moduliAsete.ktheAmortizimKokaSipasIdGjeneruesi(idgjenerues, idkonfiggjenerues));
            return pergjigja;
        }

        #region Krijimi i dokumentave te amortizimit sipas standarteve

        /// <summary>
        /// Krijimi i amortizimeve per te tre standartet automatikisht per ndryshimin e statusit te magazines.
        /// </summary>
        /// <param name="historikMagazine">(clsHistorikStatusMagazine) Historiku i magazines qe po ndryshohet.</param>
        /// <param name="njesiadm">(DbRegjistrim.clsNjesiAdministrative) Objekti i njesise administrative.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="idNderViti">(int) Id e ndermarrjes qe lidhet me vitin fiskal.</param>
        /// <param name="nrRreshti">(int) Numri i rreshtit qe po kryhet veprimi.</param>
        /// <param name="konfigamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="idnivelgjenerues">(int) Id llojit te nivelit qe e ka gjeneruar.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e amortizimeve per te tre standartet kryhet me sukses, ne te kundert False.</returns>
        public bool krijoAmortizimeKokaNdryshimStatusi(clsHistorikStatusMagazine historikMagazine, DbRegjistrim.clsNjesiAdministrative njesiadm, int idNdermarrje, int idNderViti, out int nrRreshti, DbShare.clsKonfigurimAmbjenti konfigamortizimi, int idnivelgjenerues, clsDatabazeAsete dbasete)
        {
            nrRreshti = 0;
            colStandarteAmortizimi standarte = new colStandarteAmortizimi(idNdermarrje);
            foreach (clsStandarteAmortizim standarti in standarte)
            {
                clsAmortizimiKoka amortizimiAktualKoka = new clsAmortizimiKoka();

                bool krijimiAmort = amortizimiAktualKoka.krijoDokumentAmortizimiNgaNdryshimiStatusit(historikMagazine.DataStatusit, njesiadm, standarti.IdStandarti, idNdermarrje, idNderViti, historikMagazine.IdPerdoruesi, out nrRreshti, konfigamortizimi, idnivelgjenerues, dbasete);
                if (!krijimiAmort)
                    return krijimiAmort;
                if (amortizimiAktualKoka.ColTrupi.Count > 0)
                    Add(amortizimiAktualKoka);

            }
            return true;
        }

        /// <summary>
        /// Krijimi i amortizimeve per te tre standartet automatikisht per dokumentat e blerjes.
        /// </summary>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="dokBlerjes">(DbRegjistrim.clsKokaShitje) Dokumenti i blerjes qe po gjeneron amortizimin.</param>
        /// <param name="konfigamortizimi">(DbShare.clsKonfigurimAmbjenti) Konfigurimi i ambjentit te amortizimit.</param>
        /// <param name="colAmortizimetEVjetra">(colAmortizimiKoka) Koleksioni i amortizimeve te vjetra.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
        public bool krijoAmortizimeKokaNgaBlerja(colSerialetMagazine serialet, DbRegjistrim.clsKokaShitje dokBlerjes, DbShare.clsKonfigurimAmbjenti konfigamortizimi, colAmortizimiKoka colAmortizimetEVjetra, clsDatabazeAsete dbasete)
        {
            bool pergjigja = true;
            colStandarteAmortizimi standarte = new colStandarteAmortizimi(dokBlerjes.IdNdermarrje);
            int i = 0;
            foreach (clsStandarteAmortizim standarti in standarte)
            {
                clsAmortizimiKoka amortizimiAktualKoka = new clsAmortizimiKoka();
                int iddoknga;
                int nrRendor;
                if (colAmortizimetEVjetra.Count > i)
                {
                    iddoknga = colAmortizimetEVjetra[i].IdAmortizimi;
                    nrRendor = colAmortizimetEVjetra[i].NrRenditje;
                }
                else
                {
                    iddoknga = 0;
                    nrRendor = 0;
                }
                pergjigja = amortizimiAktualKoka.krijoDokumentAmortizimiNgaBlerjaMagazine(serialet, dokBlerjes, standarti.IdStandarti, konfigamortizimi, iddoknga, nrRendor, dbasete);

                if (!pergjigja)
                    return pergjigja;

                Add(amortizimiAktualKoka);
                i++;
            }

            return pergjigja;
        }

        /// <summary>
        /// Krijimi i amortizimeve per te tre standartet automatikisht per transferimet nga Magazina
        /// </summary>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="serialeERinjTeNdashem">(colHistorikAQTSeriale) Lista e serialeve te rinj te krijuar te ndare nga serialet prind.</param>
        /// <param name="dokMagazine">(DbRegjistrim.clsKokaMagazina) Dokumenti i magazines qe po gjeneron amortizimin.</param>
        /// <param name="konfigamortizimi">(DbShare.clsKonfigurimAmbjenti) Objekti i konfigurimit te amoritizmit.</param>
        /// <param name="konfamortizimihyrje">(DbShare.clsKonfigurimAmbjenti) Objekti i konfigurimit te hyrjes se amortizimit.</param>
        /// <param name="colAmortizimetEVjetra">(colAmortizimiKoka) Objektii amortizimeve te vjetra te kokes.</param>
        /// <param name="colAmortizimetEVjetraHyrje">(colAmortizimiKoka) Objekti i hyrjeve te amortizimeve te vjetra te kokes.</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e amortizimeve per te tre standartet kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoAmortizimeKokaTransferimMagazine(colSerialetMagazine serialet, colHistorikAQTSeriale serialeERinjTeNdashem, DbRegjistrim.clsKokaMagazina dokMagazine, DbShare.clsKonfigurimAmbjenti konfigamortizimi, DbShare.clsKonfigurimAmbjenti konfamortizimihyrje, colAmortizimiKoka colAmortizimetEVjetra, colAmortizimiKoka colAmortizimetEVjetraHyrje, bool mosLlogaritAmortizimShtese, int idMagKoka, out string  mesazhmevonshem)
        {
            mesazhmevonshem = "";
            int i = 0;
            colStandarteAmortizimi standarte = new colStandarteAmortizimi(dokMagazine.IdNdermarrje);
            foreach (clsStandarteAmortizim standarti in standarte)
            {
                clsAmortizimiKoka amortizimiAktualKokaDalje = new clsAmortizimiKoka();
                int iddoknga = 0;
                int iddokngahyrje = 0;
                int nrRenditje = 0;
                int nrRenditjeHyrje = 0;
                if (colAmortizimetEVjetra.Count > i)
                {
                    iddoknga = colAmortizimetEVjetra[i].IdAmortizimi;
                    nrRenditje = colAmortizimetEVjetra[i].NrRenditje;
                    if (colAmortizimetEVjetraHyrje.Count > i)
                    {
                        iddokngahyrje = colAmortizimetEVjetraHyrje[i].IdAmortizimi;
                        nrRenditjeHyrje = colAmortizimetEVjetraHyrje[i].NrRenditje;
                    }
                }
                clsAmortizimiKoka amortizimiKokaPerHyrje = new clsAmortizimiKoka();
                clsMesazh krijimiAmortDalje = amortizimiAktualKokaDalje.krijoDokumentAmortizimiNgaDaljaMagazinePerTransferim(ref amortizimiKokaPerHyrje, serialet, serialeERinjTeNdashem, dokMagazine, standarti.IdStandarti, konfigamortizimi, iddoknga, nrRenditje, mosLlogaritAmortizimShtese,idMagKoka, out mesazhmevonshem);
                if (!krijimiAmortDalje.Status)
                    return krijimiAmortDalje;
                Add(amortizimiAktualKokaDalje);
                clsAmortizimiKoka amortizimiAktualKokaHyrje = new clsAmortizimiKoka();
                bool krijimiAmortHyrje = amortizimiAktualKokaHyrje.krijoDokumentAmortizimiNgaHyrjaMagazinePerTransferim(amortizimiKokaPerHyrje, dokMagazine, konfamortizimihyrje, iddokngahyrje, nrRenditjeHyrje, serialeERinjTeNdashem);
                if (!krijimiAmortHyrje)
                    return new clsMesazh(krijimiAmortHyrje);
                Add(amortizimiAktualKokaHyrje);
                i++;
            }
            return new clsMesazh(true);
        }

        /// <summary>
        /// Krijimi i amortizimeve per te tre standartet automatikisht per daljen nga magazina
        /// </summary>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="serialeERinjTeNdashem">(colHistorikAQTSeriale) Lista e serialeve te rinj te krijuar te ndare nga serialet prind.</param>
        /// <param name="dokMagazine">(DbRegjistrim.clsKokaMagazina) Dokumenti i magazines qe po gjeneron amortizimin.</param>
        /// <param name="konfigamortizimi">(DbShare.clsKonfigurimAmbjenti) Objekti i konfigurimit te amoritizmit.</param>
        /// <param name="colAmortizimetEVjetra">(colAmortizimiKoka) Objektii amortizimeve te vjetra te kokes.</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e amortizimeve per te tre standartet kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoAmortizimeKokaDaljaMagazine(colSerialetMagazine serialet, colHistorikAQTSeriale serialeERinjTeNdashem, DbRegjistrim.clsKokaMagazina dokMagazine, DbShare.clsKonfigurimAmbjenti konfigamortizimi, colAmortizimiKoka colAmortizimetEVjetra, bool mosLlogaritAmortizimShtese,int idMagKoka, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
               colStandarteAmortizimi standarte = new colStandarteAmortizimi(dokMagazine.IdNdermarrje);
            int i = 0;
            foreach (clsStandarteAmortizim standarti in standarte)
            {
                clsAmortizimiKoka amortizimiAktualKokaDalje = new clsAmortizimiKoka();
                int iddoknga;
                int nrRenditje;
                if (colAmortizimetEVjetra.Count > i)
                {
                    iddoknga = colAmortizimetEVjetra[i].IdAmortizimi;
                    nrRenditje = colAmortizimetEVjetra[i].NrRenditje;
                }
                else
                {
                    iddoknga = 0;
                    nrRenditje = 0;
                }
                clsMesazh krijimiAmortDalje = amortizimiAktualKokaDalje.krijoDokumentAmortizimiNgaDaljaMagazine(serialet, serialeERinjTeNdashem, dokMagazine, standarti.IdStandarti, konfigamortizimi, iddoknga, nrRenditje, mosLlogaritAmortizimShtese,idMagKoka, out mesazhmevonshem);
                if (!krijimiAmortDalje.Status)
                    return krijimiAmortDalje;
                Add(amortizimiAktualKokaDalje);
                i++;
            }
            return new clsMesazh(true);
        }

        /// <summary>
        /// Krijimi i amortizimeve per te tre standartet automatikisht per shitjen nga magazina
        /// </summary>
        /// <param name="serialet">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
        /// <param name="serialeERinjTeNdashem">(colHistorikAQTSeriale) Lista e serialeve te rinj te krijuar te ndare nga serialet prind.</param>
        /// <param name="dokShitje">(DbRegjistrim.clsKokaShitje) Dokumenti i shitjes qe po gjeneron amortizimin.</param>
        /// <param name="konfigamortizimi">(DbShare.clsKonfigurimAmbjenti) Objekti i konfigurimit te amoritizmit.</param>
        /// <param name="colAmortizimetEVjetra">(colAmortizimiKoka) Objektii amortizimeve te vjetra te kokes.</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese mbushja e amortizimeve per te tre standartet kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoAmortizimeKokaShitjeMagazine(colSerialetMagazine serialet, colHistorikAQTSeriale serialeERinjTeNdashem, DbRegjistrim.clsKokaShitje dokShitje, DbShare.clsKonfigurimAmbjenti konfigamortizimi, colAmortizimiKoka colAmortizimetEVjetra, bool mosLlogaritAmortizimShtese,int idMagKoka, out string mesazhmevonshem)
        {
            mesazhmevonshem = "";
            string tmpmesazh = "";
               colStandarteAmortizimi standarte = new colStandarteAmortizimi(dokShitje.IdNdermarrje);
            int i = 0;
            foreach (clsStandarteAmortizim standarti in standarte)
            {
                clsAmortizimiKoka amortizimiAktualKokaShitje = new clsAmortizimiKoka();
                int iddoknga;
                int nrRenditja;
                if (colAmortizimetEVjetra.Count > i)
                {
                    iddoknga = colAmortizimetEVjetra[i].IdAmortizimi;
                    nrRenditja = colAmortizimetEVjetra[i].NrRenditje;
                }
                else
                {
                    iddoknga = 0;
                    nrRenditja = 0;
                }
                clsMesazh krijimiAmortShitje = amortizimiAktualKokaShitje.krijoDokumentAmortizimiNgaShitjaMagazine(serialet, serialeERinjTeNdashem, dokShitje, standarti.IdStandarti, konfigamortizimi, iddoknga, nrRenditja, mosLlogaritAmortizimShtese,idMagKoka, out tmpmesazh);
                if (tmpmesazh != "")
                    mesazhmevonshem = tmpmesazh;
                if (!krijimiAmortShitje.Status)
                    return krijimiAmortShitje;
                Add(amortizimiAktualKokaShitje);
                i++;
            }
            return new clsMesazh(true);
        }

        /// <summary>
        /// Krijimi i amortizimeve per te tre standartet automatikisht per rivleresimin.
        /// </summary>
        /// <param name="nrDok">(string) Numri i dokumentit qe po krijohet.</param>
        /// <param name="idNiveli">(int) Id e nekategorise se dokumentit.</param>
        /// <param name="idKonfigurimAmbjenti">(int) Id e llojit te dokumentit.</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te krijuar te amortizimit.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po behet regjistrimi i amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
        /// <param name="shenime">(string) Shenime te ndryshme qe vendosen ne ambjentit e ruajtjes te amortizimit.</param>
        /// <param name="idNderViti">(int) Id e lidhjes se ndemarrjes me vitin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ku po kryhet amortizimi.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryhen amortizimin.</param>
        /// <param name="colTrupiKrijuar">(colAmortizimiTrupi) Lista e trupit te krijuar ne griden e amortizimit fillestar.</param>
        /// <param name="krijoTrupTani">perdoret per rastet e modifikimit qe trupi i dokumentit te krijohet vetem pasi te fshihet dokumenti i vjeter</param>
        /// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoAmortizimeKokaRivleresim(string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateAmortizimi, int idNjesiAdministrative,
            string shenime, int idNderViti, int idNdermarrje, int idPerdoruesi, DateTime dtregjistrimi, int idstatusdok, int idllogari, colAmortizimiTrupiAbstract colTrupiKrijuar, DbShare.clsKonfigurimAmbjenti konfmag, bool gjeneromagazine, colSerialetMagazine colseriale, bool krijoTrupTani, int idLlojStandarti, out string mesazhmevonshem)
        {
         string tmpmesazhi=   mesazhmevonshem = "";
               colStandarteAmortizimi standarte = new colStandarteAmortizimi(idNdermarrje);
            clsDatabazeAsete dbasete = new clsDatabazeAsete();
            bool ndryshovleraseriale = true;
            bool rivleresimXStandart = false;
            if (!krijoTrupTani) ndryshovleraseriale = false;
            foreach (clsStandarteAmortizim standarti in standarte)
            {
                if (standarti.IdStandarti == idLlojStandarti || idLlojStandarti == -1)
                {
                    //rastet kur specifikohet standarti qe do amortizohet
                    if (standarti.IdStandarti == idLlojStandarti)
                    {
                        gjeneromagazine = false;
                        rivleresimXStandart = true;
                    }
                    clsAmortizimiKoka amortizimiAktualRivleresim = new clsAmortizimiKoka();
                    colAmortizimiTrupiAbstract coltrupi;
                    if (colTrupiKrijuar.objektiKod == enumObjekteAmortizimi.ASETE) coltrupi = new colAmortizimiTrupi();
                    else coltrupi = new colAmortizimiTrupiRezerva();
                    coltrupi.AddRange(colTrupiKrijuar);
                    clsMesazh krijimiAmortRivleresim = amortizimiAktualRivleresim.krijoDokumentAmortizimiPerRivleresim(nrDok, idNiveli, idKonfigurimAmbjenti, dateDokumenti, dateAmortizimi, idNjesiAdministrative, standarti.IdStandarti,
                        shenime, idNderViti, idNdermarrje, idPerdoruesi, dtregjistrimi, idstatusdok, idllogari, coltrupi, konfmag, gjeneromagazine, colseriale, ndryshovleraseriale, krijoTrupTani, rivleresimXStandart, out tmpmesazhi);
                    if (tmpmesazhi != "")
                        mesazhmevonshem = tmpmesazhi;
                    if (!krijimiAmortRivleresim.Status)
                        return krijimiAmortRivleresim;
                    Add(amortizimiAktualRivleresim);
                    ndryshovleraseriale = false;
                    rivleresimXStandart = false;
                }
            }
            return new clsMesazh(true);
        }

        /// <summary>
        /// Krijimi i amortizimeve per te tre standartet automatikisht per shperndarjen e shpenzimeve.
        /// </summary>
        /// <param name="kokamag">(DbRegjistrim.clsShperndarjeShpenzimeKoka) Dokumenti i shperndarjes se shpenzimeve.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
        public clsMesazh krijoAmortizimeKokaShperndarjeShpenzimesh(DbRegjistrim.clsKokaMagazina kokamag, DbShare.clsKonfigurimAmbjenti konfigamortizimi, List<clsShperndarjeShpenzimeTrupi> trupiPerKetedokmag, colAmortizimiKoka colAmortizimetEVjetra, colSerialetMagazine colseriale)
        {
            colStandarteAmortizimi standarte = new colStandarteAmortizimi(kokamag.IdNdermarrje);
            int i = 0;
            foreach (clsStandarteAmortizim standarti in standarte)
            {
                clsAmortizimiKoka amortizimiAktualShperndarjeShpenzime = new clsAmortizimiKoka();
                int iddoknga;
                int nrRenditja;
                if (colAmortizimetEVjetra.Count > i)
                {
                    iddoknga = colAmortizimetEVjetra[i].IdAmortizimi;
                    nrRenditja = colAmortizimetEVjetra[i].NrRenditje;
                }
                else
                {
                    iddoknga = 0;
                    nrRenditja = 0;
                }
                clsMesazh krijimiAmortShperndarjeShpenzimesh = amortizimiAktualShperndarjeShpenzime.krijoDokumentAmortizimiPerShperndarjeShpezime(kokamag, standarti.IdStandarti, konfigamortizimi, iddoknga, nrRenditja, trupiPerKetedokmag, colseriale);
                if (!krijimiAmortShperndarjeShpenzimesh.Status)
                    return krijimiAmortShperndarjeShpenzimesh;
                if (amortizimiAktualShperndarjeShpenzime.ColTrupi.Count > 0)
                    Add(amortizimiAktualShperndarjeShpenzime);
                i++;
            }
            return new clsMesazh(true);
        }

        #endregion

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_AMORTIZIMI_KOKA ne nje list objektesh clsAmortizimiKoka.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushAmortizimiKokaList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAmortizimiKoka amortizimiKoka = new clsAmortizimiKoka();
                    //amortizimiKoka.mbushAmortizimKokaObjekt(rreshti);
                    Add(new clsAmortizimiKoka(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
