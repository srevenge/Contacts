using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Contacts
{
    public partial class Update : Form
    {
        private string fileName;
        private Main main;
        public Update(Main main)
        {
            InitializeComponent();
            this.Text = Main.contact.name + " " + Main.contact.family;
            fileName = null;
            this.main = main;
        }

        private void Update_Load(object sender, EventArgs e)
        {
            this.setValues();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string family = textBox2.Text;
            string phone = textBox3.Text;
            string home = textBox4.Text;
            string email = textBox5.Text;

            Image img = (this.fileName == null) ? null : Image.FromFile(fileName);

            if (!name.Trim().Equals("") && !family.Trim().Equals("") && (!phone.Trim().Equals("") || !home.Trim().Equals("")))
            {
                
                if (phone == "-1" && home == "-1")
                {
                    this.reset();
                    MessageBox.Show("لطفا حداقل یک شماره وارد کنید");

                }
                else
                {
                    Contact contact = new Contact(name, family, phone, home, email, img);

                    int index = Main._contacts.IndexOf(Main.contact);
                    Main._contacts[index] = contact;
                    this.write();

                    this.main.flowLayoutPanel1.Controls.Clear();

                    this.main.setContacts();

                    this.Close();
                }

                


            }
            else
                MessageBox.Show("لطفا موارد را کامل کنید");
    } 

        private void setValues()
        {
            this.pictureBox2.Image = Main.contact.img;
            this.textBox1.Text = Main.contact.name;
            this.textBox2.Text = Main.contact.family;
            this.textBox3.Text = (Main.contact.phone == "-1") ? "" : Main.contact.phone;
            this.textBox4.Text = (Main.contact.home == "-1") ? "" : Main.contact.phone;
            this.textBox5.Text = (Main.contact.email == "-1") ? "" : Main.contact.email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serializationFile = "C:\\Users\\Admin\\Desktop\\cn.bin";
            Main._contacts.Remove(Main.contact);

            if (Main._contacts.Count == 0)
                File.Delete(serializationFile);
            else
                this.write();

            Console.WriteLine(Main._contacts.Count);
            
            this.main.flowLayoutPanel1.Controls.Clear();
                
            this.main.setContacts();

            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image Files|*.jpg;*.png;*.jpeg";
            op.Multiselect = false;
            if (op.ShowDialog() == DialogResult.OK)
                this.fileName = op.FileName;
            if (this.fileName.Trim() == "")
                MessageBox.Show("عکسی را انتخاب نکردید");
            else
            {
                pictureBox2.Image = Image.FromFile(fileName);
            }
        }

        private void reset()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void write()
        {
            string serializationFile = "C:\\Users\\Admin\\Desktop\\cn.bin";
            //serialize
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, Main._contacts);
                stream.Close();
            }
        }

        private void Update_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.main.Enabled = true;
        }
    }
}