using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbShare
{
    public class clsMenuItem
    {
        #region Attribute

        private int idMenuItem;
        private string name;
        private string text;
        private string imageUrl;
        private int idPrindi;
        private bool enabled;
        private string prind;
        private string urlHelp;
        private int idgjuha;
        private bool position;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Get Set: id e menuitem
        /// </summary>
        public int IdMenuItem { get { return idMenuItem; } set { idMenuItem = value; } }

        /// <summary>
        /// Get Set: name e menuitem
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Get Set: textin e menuitem
        /// </summary>
        public string Text { get { return text; } set { text = value; } }

        /// <summary>
        /// Get Set:  image url e menuitem
        /// </summary>
        public string ImageUrl { get { return imageUrl; } set { imageUrl = value; } }

        /// <summary>
        /// Get Set: idprindi
        /// </summary>
        public int IdPrindi { get { return idPrindi; } set { idPrindi = value; } }
        /// <summary>
        /// get set emrin e prinidt
        /// </summary>
        public string Prind
        {
            get
            {
                return prind;
            }
            set
            {
                prind = value;
            }
        }
        /// <summary>
        /// get set enabled sipas te drejtave
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }
        /// <summary>
        /// url e butonin te helpit
        /// </summary>
        public string UrlHelp
        {
            get
            {
                return urlHelp;
            }
            set
            {
                urlHelp = value;
            }
        }
        /// <summary>
        /// Get/Set position. False is left, True is Right
        /// </summary>
        public bool Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        /// <param name="idgjuha"></param>
        public clsMenuItem(int idgjuha)
        {
        }

        /// <summary>
        /// konstruktori me id
        /// </summary>
        /// <param name="idgjuha"></param>
        /// <param name="idMenuItem">id-ja e dhene per te lexuar te dhenat nga db-ja</param>
        public clsMenuItem(int idgjuha, int idMenuItem)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            if (!this.mbushMenuItem(idgjuha, data.merrMenuItemSipasId(idMenuItem)))
                return; //roli me id idroli nuk ekziston
            //throw new Exception("ERROR: Gabim gjate leximit te rolit " + idRoli + "nga databaza");
            data.Dispose();
        }

        /// <summary>
        /// konstruktori i plote
        /// </summary>
        /// <param name="idgjuha"></param>
        /// <param name="idMenuItem"> id ritese e menu item</param>
        /// <param name="name">emri</param>
        /// <param name="text">texti</param>
        /// <param name="imageUrl">url e imazhit</param>
        /// <param name="idPrindi">id e prindit</param>
        public clsMenuItem(int idgjuha, int idMenuItem, string name, string text, string imageUrl, int idPrindi,bool enabled, string prind, string urlhelp)
        {
            this.idMenuItem = idMenuItem;
            this.name = name;
            this.text = text;
            this.imageUrl = imageUrl;
            this.idPrindi = idPrindi;
            this.enabled = enabled;
            this.prind = prind;
            this.urlHelp = urlhelp;
        
            this.idgjuha = idgjuha;

        }

        public clsMenuItem(int idgjuha, DataRow rreshti)
        {
            
            mbushMenuItem(idgjuha, rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush objektin nga nje datarow i marr nga db-ja
        /// </summary>
        /// <param name="idgjuha"></param>
        /// <param name="rreshti">datarow me te dhenat e te drejtes</param>
        /// <returns>True nese te dhenat merren me sukses, False perndryshe</returns>
        internal bool mbushMenuItem(int idgjuha, DataRow rreshti)
        {
            string kodGjuhe = MessagesResource.KtheKodGjuhe(idgjuha);
            try
            {
                this.text = Convert.ToString(rreshti["TEXTI_" + kodGjuhe]);
                this.idMenuItem = Convert.ToInt32(rreshti["IDMENUITEM"]);
                this.name = Convert.ToString(rreshti["NAME"]);
                this.imageUrl = Convert.ToString(rreshti["IMAGEURL"]);
                this.urlHelp = Convert.ToString(rreshti["URLHELP"]);
                bool.TryParse(Convert.ToBoolean(rreshti["ENABLED"]).ToString(), out enabled);
                this.prind = Convert.ToString(rreshti["PRIND"]);
                int.TryParse(rreshti["IDPRIND"].ToString(), out idPrindi);
                bool.TryParse(Convert.ToBoolean(rreshti["POSITION"]).ToString(), out position);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        #endregion
    }
}