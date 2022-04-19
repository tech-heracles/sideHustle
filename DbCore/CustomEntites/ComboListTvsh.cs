using System;

namespace DbCore
{
    public struct ComboListTvsh
    {
        public int value;
        public string text;
        public decimal norma;
        /// <summary>
        /// caktuar duhet marre vlerat: art, llog, nderm, asnje; ne varesi nese kjo tvsh eshte ajo qe i eshte caktuar artikullit, 
        /// llogarise ndermarrjes apo e pa caktuar si defautl
        /// </summary>
        public string caktuar; 
    }
}