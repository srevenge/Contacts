using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Contacts
{
    public partial class Add : Form
    {
        private string fileName;
        public Main main;
        public Add(Main main)
        {
            InitializeComponent();
            this.fileName = null;
            this.main = main;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image Files|*.jpg;*.png;*.jpeg";
            op.Multiselect = false;
            if(op.ShowDialog() == DialogResult.OK)
                this.fileName = op.FileName;
            if (this.fileName.Trim() == "")
                MessageBox.Show("عکسی را انتخاب نکردید");
            else
            {
                pictureBox1.Image = Image.FromFile(fileName);
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string family = textBox2.Text;
            string phone = textBox3.Text;
            string home = textBox4.Text;
            string email = textBox5.Text;

            Image img = (this.fileName == null)?null:Image.FromFile(fileName);

            if (!name.Trim().Equals("") && !family.Trim().Equals("") && (!phone.Trim().Equals("") || !home.Trim().Equals("")))
            {
                Contact contact = new Contact(name, family, phone, home, email, img);
                if(contact.phone == "-1" && contact.home == "-1")
                {
                    this.reset();
                    MessageBox.Show("لطفا حداقل یک شماره وارد کنید");
                    
                }
                contact.save();
                Main._contacts.Add(contact);
                ListItem listItem = new ListItem();
                listItem.name = contact.name;
                listItem.phone = (contact.phone == "-1" || contact.phone.Trim() == "") ? contact.home : contact.phone;
                listItem.img = contact.img;
                listItem.Click += this.main.btnDel_click;

                if(this.main.flowLayoutPanel1.Controls.Count != 0)
                {
                    Color oldColor = this.main.flowLayoutPanel1.Controls[this.main.flowLayoutPanel1.Controls.Count - 1].BackColor;
                    listItem.BackColor = (oldColor == Color.Bisque) ? Color.DarkSalmon : Color.Bisque;
                }
                else
                    listItem.BackColor = Color.Bisque;
                
                this.main.flowLayoutPanel1.Controls.Add(listItem);

                
                this.Close();

            }
            else
                MessageBox.Show("لطفا موارد را کامل کنید");
        }

        private void reset()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.main.Enabled = true;
        }
    }
}
