using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace Contacts
{
    
    public partial class Main : Form
    {
        public static Contact contact;
        public static List<Contact> _contacts;
        private ArrayList listItems;
        private bool flag;
        public Main()
        {
            InitializeComponent();
            contact = new Contact();
            this.flag = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Add add = new Add(this);
            add.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            initContacts();
            this.setContacts();
        }

        public static void initContacts()
        {
            _contacts = new List<Contact>();
            string serializationFile = "cData\\cn.bin";
            if (File.Exists(serializationFile))
            {
                //deserialize
                using (Stream stream = File.Open(serializationFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    if (stream.Length < 4200)
                    {
                        Contact item = (Contact)bformatter.Deserialize(stream);
                        _contacts.Add(item);
                    }
                    else
                    {
                        _contacts = (List<Contact>)bformatter.Deserialize(stream);
                    }
                    stream.Close();
                }

            }
        }

        public void setContacts()
        {
            Color[] colors = new Color[]{ Color.Bisque, Color.DarkSalmon};
            listItems = new ArrayList();
            int j = 0;
            foreach(Contact c in _contacts)
            {
                ListItem listItem = new ListItem();
                listItem.name = c.name + "   " + c.family;
                listItem.phone = (c.phone == "-1" || c.phone.Trim() == "") ? c.home : c.phone;
                listItem.img = c.img;
                listItem.Click += btnDel_click;
                if (j == 2)
                    j = 0;
                listItem.BackColor = colors[j++];
                listItems.Add(listItem);
                flowLayoutPanel1.Controls.Add(listItem);
            }

        }

        public void btnDel_click(object sender, EventArgs e)
        {
            ListItem l = (ListItem) sender;
            Boolean flag = false;
            foreach (Contact c in _contacts)
                if ((l.phone.Equals(c.phone) || l.phone.Equals(c.home)) && (l.name.Contains(c.name)))
                {
                    contact = c;
                    flag = true;
                    break;
                }
            if(flag)
            {
                this.Enabled = false;
                
                new Update(this).Show();
            }else
            {
                MessageBox.Show("همچین موردی یافت نشد");
            }
            

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            if (txt.Trim() == "" && flag == false)
            {
                flowLayoutPanel1.Controls.Clear();
                this.setContacts();
                flag = true;

            }
            else
            {
                flag = false;
                foreach (ListItem c in flowLayoutPanel1.Controls)
                {
                    if (!c.name.Contains(txt) && !c.phone.Contains(txt))
                    {
                        flowLayoutPanel1.Controls.Remove(c);
                    }

                }
            }
        }
        
    }
}
