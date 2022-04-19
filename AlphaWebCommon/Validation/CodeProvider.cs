using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils.Validation
{
    public static class CodeProvider
    {

        
        /// <summary>
        /// Kontrollon nese teksti mund te sherbeje si emer variabli apo jo
        /// </summary>
        /// <param name="tekst"></param>
        /// <returns></returns>
        public static bool emerIVlefshemVariable(string tekst)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            return provider.IsValidIdentifier(tekst);

        }            
    }
}
