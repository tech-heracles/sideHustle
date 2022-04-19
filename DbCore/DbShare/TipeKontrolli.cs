using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbShare
{
    /// <summary>
    /// Tipet e nje Kontrolli
    /// </summary>
    public enum TipeKontrolli : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = -1,
        /// <summary>
        /// label
        /// </summary>
        ASPxLabel = 0,
        /// <summary>
        /// textBox       
        /// </summary>
        ASPxTextBox = 1,
        /// <summary>
        /// comboBox
        /// </summary>
        ASPxComboBox = 2,
        /// <summary>
        /// dateEdit
        /// </summary>
        ASPxDateEdit = 3,
        /// <summary>
        /// buttonEdit
        /// </summary>
        ASPxButtonEdit = 4,
        /// <summary>
        /// GridView
        /// </summary>
        ASPxGridView = 5,
        /// <summary>
        /// Button
        /// </summary>
        ASPxButton = 6,
        /// <summary>
        /// Memo
        /// </summary>
        ASPxMemo = 7,
        /// <summary>
        /// CheckBox
        /// </summary>
        ASPxCheckBox = 8,
        /// <summary>
        /// HyperLink
        /// </summary>
        ASPxHyperLink = 9,
        /// <summary>
        /// RadioButtonList
        /// </summary>
        ASPxRadioButtonList = 10,
        /// <summary>
        /// SpinEdit
        /// </summary>
        ASPxSpinEdit = 11,
        /// <summary>
        /// CheckBoxList
        /// </summary>
        ASPxCheckBoxList = 12,
        /// <summary>
        /// UploadControl
        /// </summary>
        ASPxUploadControl = 13


    };
}
