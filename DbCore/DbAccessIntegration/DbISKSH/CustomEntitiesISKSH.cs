using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAccessIntegration.DbISKSH
{
    /// <summary>
    /// Enumeracion qe permban llojet e objekteve qe mund te krijohen per ISKSH
    /// </summary>
    public enum EnumStructTypeISKSH
    {
        FatureShitjejeISKSH,
        FatureShitjejeDetajeISKSH,
        FleteHyrjeISKSH,
        FleteHyrjeDetajeISKSH,
        DepoFarmaceutikeISKSH,
        FarmaciISKSH,
        FirmeFarmaceutikeISKSH,
        MedikamentISKSH
    }

    /// <summary>
    /// Enumeracion qe permban llojet e veprimeve per ISKSH
    /// </summary>
    public enum EnumLlojVeprimiISKSH
    {
        Shitje,
        Blerje
    }

    /// <summary>
    /// Klase statike qe permban per cdo objekt qe krijohet per ISKSH, tabelen perkatese ne DB e Access
    /// </summary>
    public static class TablesISKSH
    {
        public static String FatureShitjejeISKSH { get { return "FaturaShitjeje"; } }
        public static String FatureShitjejeDetajeISKSH { get { return "FaturaShitjejeDetaje"; } }
        public static String FleteHyrjeISKSH { get { return "FleteHyrjet"; } }
        public static String FleteHyrjeDetajeISKSH { get { return "FleteHyrjeDetaje"; } }
        public static String DepoFarmaceutikeISKSH { get { return "DepotFarmaceutike"; } }
        public static String FarmaciISKSH { get { return "Farmacite"; } }
        public static String FirmeFarmaceutikeISKSH { get { return "FirmatFarmaceutike"; } }
        public static String MedikamentISKSH { get { return "Medikamentet"; } }
        public static String GjendjeISKSH { get { return "Gjendja"; } }
    }
}
