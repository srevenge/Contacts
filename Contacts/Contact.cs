using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Contacts
{
    [Serializable]
    public class Contact 
    {
        private string _name, _family, _phone, _home, _email;
        private Image _img;

        public Contact() { }

        public Contact(string name, string family, string phone, string home, string email, Image img)
        {
            this.name = name;
            this.family = family;
            this.phone = phone;
            this.home = home;
            this.email = email;
            this.img = img;
        }

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = (value.Trim() != "") ? value : "Unknown";
            }
        }
        public string family
        {
            get
            {
                return this._family;
            }
            set
            {
                this._family = (value.Trim() != "") ? value : "Unknown";
            }
        }
        public string phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                double number;
                try
                {
                    number = double.Parse(value);
                }
                catch (Exception e)
                {
                    number = -1;
                }
                this._phone = number.ToString();
            }
        }

        public string home
        {
            get
            {
                return this._home;
            }
            set
            {
                double number;
                try
                {
                    number = double.Parse(value);
                }
                catch (Exception e)
                {
                    number = -1;
                }
                this._home = number.ToString();
            }
        }

        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                if (value.Contains("@") && (value.Contains(".com") || value.Contains(".ir")))
                    this._email = value;
                else
                    this._email = "-1";
            }
        }

        public Image img
        {
            get
            {
                return this._img;
            }
            set
            {
                this._img = (value != null) ? value : Image.FromFile("Images\\Person.png");
            }
        }

        public void save()
        {

            string serializationFile = "cData\\cn.bin";
            if(File.Exists(serializationFile))
            {
                List<Contact> l = new List<Contact>();
                //deserialize
                using (Stream stream = File.Open(serializationFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    if(stream.Length < 4200)
                    {
                        Contact item = (Contact)bformatter.Deserialize(stream);
                        l.Add(item);
                    }
                    else
                    {
                        l = (List<Contact>)bformatter.Deserialize(stream);
                    }

                    
                    stream.Close();
                }
                //serialize
                using (Stream stream = File.Open(serializationFile, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    l.Add(this);
                    bformatter.Serialize(stream, l);
                    stream.Close();
                }
            }
            else
            {
                using (Stream stream = File.Open(serializationFile, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bformatter.Serialize(stream, this);
                    stream.Close();
                }
            }
            
        }

    }
}
