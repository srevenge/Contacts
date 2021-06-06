using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        #region Properties
        private string _name;
        private string _phone;
        private Image _prof;

        [Category("Custom prop")]
        public string name
        {
            set
            {
                this._name = value;
                this.lblName.Text = value;
            }
            get
            {
                return this._name;
            }
        }

        [Category("Custom prop")]
        public string phone
        {
            set
            {
                this._phone = value;
                this.lblPhone.Text = value;
            }
            get
            {
                return this._phone;
            }
        }

        [Category("Custom prop")]
        public Image img
        {
            set
            {
                this._prof = value;
                this.PicProf.Image = value;
            }
            get
            {
                return this._prof;
            }
        }

        #endregion

        private void ListItem_Load(object sender, EventArgs e)
        {

        }

        private void ListItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
