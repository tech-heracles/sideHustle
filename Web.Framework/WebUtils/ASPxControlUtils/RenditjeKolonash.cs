using System;
using System.Collections.Generic;
using System.Reflection;
using DevExpress.Web;

namespace PlatinumWeb
{
    /// <summary>
    /// Vendos VisibleIndex/Visible per shume kolona te nje gride pa koston e DevExpress-it.
    /// <para>Ne DevExpress 18.2 cdo "col.VisibleIndex = x" rillogarit indekset e te gjitha kolonave disa here
    /// (WebColumnsOwnerDefaultImplementation.SetColumnVisibleIndex: dy cikle mbi kolonat, dhe cdo lexim i VisibleIndex
    /// brenda tyre therret perseri EnsureVisibleIndices mbi te gjitha kolonat), pra kushton O(n^2) per kolone dhe O(n^3)
    /// per griden. Te Artikujt (124 kolona) kjo merrte ~200 ms ne cdo hapje faqeje.</para>
    /// <para>Kjo klase ben pikerisht te njejtat hapa (te njejtin algoritem, me te njejtin rend) mbi nje kopje te
    /// indekseve ne memorie, dhe ne fund i shkruan vlerat perfundimtare ne kolona me nje kalim. Rezultati eshte identik
    /// me thirrjet e njepasnjeshme te "col.VisibleIndex = x" / "col.Visible = v".</para>
    /// <para>Mes thirrjeve te kesaj klase nuk duhet lexuar VisibleIndex/Visible nga kolonat e grides (do te ishin te vjetra)
    /// dhe nuk duhen shtuar/hequr kolona. Nese DevExpress ndryshon dhe leximi i brendshem nuk gjendet, ose grida ka kolona
    /// te grupuara (band), klasa thjesht therret setter-at e zakonshem.</para>
    /// </summary>
    public sealed class RenditjeKolonash : IDisposable
    {
        private static readonly Func<WebColumnBase, int> lexoIndeksinERuajtur = KrijoLexuesin();

        /// <summary>Vetem per testim: ben edhe thirrjet reale te DevExpress-it dhe ne fund shkruan ne log (Error) cdo kolone ku
        /// modeli ndryshon nga rezultati real. U kontrollua keshtu ne 36 faqe dhe lupat e tyre (83 grida): asnje ndryshim.</summary>
        public static volatile bool Kontrollo;

        private readonly ASPxGridView grida;
        private readonly bool aktiv;
        private readonly List<WebColumnBase> kolonat = new List<WebColumnBase>();
        private readonly Dictionary<WebColumnBase, int> pozicioni = new Dictionary<WebColumnBase, int>();
        private int[] indeksi;
        private bool[] dukshme;
        private int[] indeksiFillestar;
        private bool[] dukshmeFillestare;
        private bool perfundoi;
        private bool mundTeKeteBosh;

        public RenditjeKolonash(ASPxGridView grida)
        {
            this.grida = grida;
            aktiv = lexoIndeksinERuajtur != null && grida != null && LexoModelin();
        }

        private static Func<WebColumnBase, int> KrijoLexuesin()
        {
            try
            {
                MethodInfo m = typeof(WebColumnBase).GetMethod("GetColVisibleIndex", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, Type.EmptyTypes, null);
                if (m == null || m.ReturnType != typeof(int))
                    return null;
                return (Func<WebColumnBase, int>)Delegate.CreateDelegate(typeof(Func<WebColumnBase, int>), m);
            }
            catch
            {
                return null;
            }
        }

        private bool LexoModelin()
        {
            kolonat.Clear();
            pozicioni.Clear();
            foreach (GridViewColumn c in grida.Columns)
            {
                if (c is GridViewBandColumn)
                    return false;
                pozicioni[c] = kolonat.Count;
                kolonat.Add(c);
            }
            int n = kolonat.Count;
            indeksi = new int[n];
            dukshme = new bool[n];
            for (int i = 0; i < n; i++)
            {
                indeksi[i] = lexoIndeksinERuajtur(kolonat[i]);
                dukshme[i] = kolonat[i].Visible;
            }
            indeksiFillestar = (int[])indeksi.Clone();
            dukshmeFillestare = (bool[])dukshme.Clone();
            mundTeKeteBosh = true;
            return true;
        }

        /// <summary>Si "kolona.VisibleIndex = vlera".</summary>
        public void VendosIndeksin(WebColumnBase kolona, int vlera)
        {
            int i;
            if (!aktiv)
                kolona.VisibleIndex = vlera;
            else if (!pozicioni.TryGetValue(kolona, out i))
                JashteModelit(() => kolona.VisibleIndex = vlera);
            else
            {
                ModelIndeksi(i, vlera);
                if (Kontrollo)
                    kolona.VisibleIndex = vlera;
            }
        }

        /// <summary>Si "kolona.Visible = vlera".</summary>
        public void VendosDukshmerine(WebColumnBase kolona, bool vlera)
        {
            int i;
            if (!aktiv)
                kolona.Visible = vlera;
            else if (!pozicioni.TryGetValue(kolona, out i))
                JashteModelit(() => kolona.Visible = vlera);
            else
            {
                if (vlera && !dukshme[i])
                    mundTeKeteBosh = true;
                dukshme[i] = vlera;
                if (Kontrollo)
                    kolona.Visible = vlera;
            }
        }

        // kolone qe nuk eshte direkt ne grida.Columns: shkruhet modeli, behet thirrja e zakonshme dhe modeli rilexohet
        private void JashteModelit(Action veprimi)
        {
            if (!Kontrollo)
                Shkruaj();
            veprimi();
            LexoModelin();
        }

        // WebColumnsOwnerDefaultImplementation.EnsureVisibleIndices: kolonat e dukshme pa indeks marrin indekset e lira me te vogla, sipas rendit ne koleksion
        private void Siguro()
        {
            if (!mundTeKeteBosh)
                return;
            mundTeKeteBosh = false;
            HashSet<int> zene = null;
            int k = 0;
            for (int i = 0; i < indeksi.Length; i++)
            {
                if (indeksi[i] >= 0 || !dukshme[i])
                    continue;
                if (zene == null)
                {
                    zene = new HashSet<int>();
                    for (int j = 0; j < indeksi.Length; j++)
                        if (indeksi[j] >= 0) zene.Add(indeksi[j]);
                }
                while (zene.Contains(k)) k++;
                indeksi[i] = k;
                k++;
            }
        }

        // WebColumnsOwnerDefaultImplementation.SetColumnVisibleIndex
        private void ModelIndeksi(int c, int vlera)
        {
            Siguro();
            if (dukshme[c] && vlera == indeksi[c])
                return;
            if (vlera < 0)
            {
                dukshme[c] = false;
                Siguro();
                vlera = indeksi[c];
            }
            else
            {
                if (!dukshme[c])
                    mundTeKeteBosh = true;
                dukshme[c] = true;
                Siguro();
                for (int j = 0; j < indeksi.Length; j++)
                {
                    Siguro();
                    if (indeksi[j] > indeksi[c])
                        indeksi[j] = indeksi[j] - 1;
                }
                for (int j = 0; j < indeksi.Length; j++)
                {
                    Siguro();
                    if (indeksi[j] >= vlera)
                        indeksi[j] = indeksi[j] + 1;
                }
            }
            indeksi[c] = vlera;
            if (vlera < 0)
                mundTeKeteBosh = true;
        }

        // shkruan gjendjen perfundimtare ne kolona dhe pastron cache-t e grides si do te benin setter-at publike
        private void Shkruaj()
        {
            WebColumnBase endryshuar = null;
            for (int i = 0; i < kolonat.Count; i++)
            {
                if (indeksi[i] != indeksiFillestar[i])
                {
                    kolonat[i].SetColVisibleIndex(indeksi[i]);
                    endryshuar = kolonat[i];
                }
                if (dukshme[i] != dukshmeFillestare[i])
                {
                    kolonat[i].SetColVisible(dukshme[i]);
                    endryshuar = kolonat[i];
                }
            }
            if (endryshuar != null)
            {
                // "Visible = !v; Visible = v" nuk ndryshon asgje, por kalon neper IWebColumnsOwner.SetColumnVisible dhe OnColumnChanged:
                // pastron listen e kolonave te dukshme, ColumnHelper-in dhe njofton LayoutChanged, si cdo setter publik.
                bool v = endryshuar.Visible;
                endryshuar.Visible = !v;
                endryshuar.Visible = v;
            }
            indeksiFillestar = (int[])indeksi.Clone();
            dukshmeFillestare = (bool[])dukshme.Clone();
        }

        public void Dispose()
        {
            if (perfundoi || !aktiv)
                return;
            perfundoi = true;
            if (Kontrollo)
            {
                var gabime = new List<string>();
                for (int i = 0; i < kolonat.Count; i++)
                {
                    int real = lexoIndeksinERuajtur(kolonat[i]);
                    if (real != indeksi[i] || kolonat[i].Visible != dukshme[i])
                        gabime.Add(kolonat[i].Name + ":" + real + "/" + indeksi[i] + "," + kolonat[i].Visible + "/" + dukshme[i]);
                }
                if (gabime.Count > 0)
                    NLog.LogManager.GetCurrentClassLogger().Error("RenditjeKolonash " + grida.ID + ": modeli ndryshon nga DevExpress: " + string.Join(" ", gabime));
                return;
            }
            Shkruaj();
        }
    }
}
