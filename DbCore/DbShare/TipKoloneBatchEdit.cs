using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbShare
{
    public enum TipKoloneBatchEdit
    {

        /// <summary>
        /// eshte default 
        /// </summary>
        TextEdit = 0,
        /// <summary>
        /// perp kolonat spin
        /// </summary>
        SpinEdit = 1,
        /// <summary>
        /// per kombot
        /// </summary>
        ComboBox = 2,
        /// <summary>
        /// per kolonat date
        /// </summary>
        DateEdit = 3,
        /// <summary>
        /// per kolonat ore
        /// </summary>
        TimeEdit = 4,
        /// <summary>
        /// per kolonat spin qe pranojne vetem vlera te plota
        /// </summary>
        SpinEditInteger = 5,
        /// <summary>
        /// per kolonat combo qe do te hapin nje lupe
        /// </summary>
        ComboBoxbuttonEdit = 6
    }
}
