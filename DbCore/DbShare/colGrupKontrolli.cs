using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbShare
{
    public class colGrupKontrolli:List<clsGrupKontrolli>
    {
        /// <summary>
        /// tregon nese ekziston grupi me idgrupi ne koleksion apo jo.
        /// </summary>
        /// <param name="idGrupi">id-ja e grupit</param>
        /// <returns>True nese gjendet, False perndryshe</returns>
        public bool ekziston(int idGrupi)
        {
            foreach (clsGrupKontrolli grupKontrolli in this)
            { 
                if (grupKontrolli.IdGrupi == idGrupi)
                    return true;
            }
            return false;
        }
        public clsGrupKontrolli ktheGrup(int idGrupi)
        {
            foreach (clsGrupKontrolli grupKontrolli in this)
            {
                if (grupKontrolli.IdGrupi == idGrupi)
                    return grupKontrolli;
            }
            return null;
        }
    }
}
