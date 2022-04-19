using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public enum SerialeUnike_MetodeTransferimi
    {
        Manuale = 0,
        Automatike = 1,
        Automatike_per_Gjendje_Ditore_Aparate = 2,
        Automatike_per_Gjendje_Mujore_Aparate = 3,
        Manuale_per_Karta_Old = 4
    }
}