using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrashWebhooks
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Form1.universal_webhook = textBox1.Text;
                Form2 frm2= new Form2();
                this.Hide();
                Form1.acces = true;
                Form1 frm1= new Form1();
              //  frm1.Show();
            }
            else
            {
                MessageBox.Show("Textbox is null or empty");
            }
        }

      
    }
}
