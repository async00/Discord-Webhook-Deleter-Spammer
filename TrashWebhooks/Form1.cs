using LimeLogger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrashWebhooks.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace TrashWebhooks
{
    public partial class Form1 : Form
    {
        internal static string universal_webhook;
        internal static bool acces=false;
        internal static int cooldown = 0;
        internal static bool isattacking = false;
        internal static bool accestoattack = false;
        public Form1()
        {
            InitializeComponent();
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {
            consolelog("sa");

        
            comboBox2.SelectedIndex = 0;
            this.Hide();
            Form2 frm2 = new Form2();
            frm2.Show();
         
            Thread wb = new Thread(waitwebhookdata);
            wb.Start();


        }
        void waitwebhookdata()
        {
           
           while (true)
            {
                if(!acces)
                    this.Hide();
                else
                    this.Show();


                if (!string.IsNullOrWhiteSpace(universal_webhook))
                {
                    try
                    {
                        Form2 frm2 = new Form2();
                        acces = true;
                        frm2.Close();
                        string jsondata = string.Empty;
                        using (WebClient client = new WebClient())
                        {
                            jsondata = client.DownloadString(universal_webhook);

                        }
                        //read json data
                        JSONWebhook webhook = JsonConvert.DeserializeObject<JSONWebhook>(jsondata);


                        listBox1.Items.Add("ID: " + webhook.Id);
                        listBox1.Items.Add("Dynamic Name: " + webhook.Name);
                        listBox1.Items.Add("Webhook Owner: " + webhook.User.Username + $"({webhook.User.Global_name})");
                        listBox1.Items.Add("Webhook Owner ID: " + webhook.User.Id);
                        listBox1.Items.Add("Token: " + webhook.Token);
                        listBox1.Items.Add("Url: " + webhook.Url);


                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                 
                }
                Thread.Sleep(1000);
            }
        }
        class JSONUser
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Global_name { get; set; }
            public string Avatar { get; set; }
        }
        class JSONWebhook
        {
            public string ChannelId { get; set; }
            public string GuildId { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public int Type { get; set; }
            public JSONUser User { get; set; }
            public string Token { get; set; }
            public string Url { get; set; }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string url = universal_webhook;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Succesfuly Deleted");
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Failed status code : "+response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : "+ex.Message);
                    throw;
                }
               
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                textBox1.Text = texts.trashwebhookıshere2000char;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                textBox1.Text = texts.niggernigggernigger2000char;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                textBox1.Text = texts.xxxxxxxxxxxxxxxxxxx2000char;
            }
            if(comboBox2.SelectedIndex == 3)
            {
                textBox1.Text = texts.nomorestealing2000char;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double selectedValue = Convert.ToDouble(comboBox1.SelectedItem)*1000;
            cooldown = Convert.ToInt32(selectedValue);
        }
        
        internal void consolelog(string text2)
        {
           
            listBox2.Items.Add(text2);
            
        }
      
        void attackerthread()
        {

            while (true)
            {
                if(accestoattack)
                {
                    try
                    {
                        byte[] returnedvalue = Discord.SendWebHook(textBox1.Text, textBox2.Text);

                        if(returnedvalue != null)
                        {
                            consolelog($"[+]Succes ! | Total Message count:{Discord.totalsentmessagecount}");
                            listBox2.TopIndex = listBox1.Items.Count - 1;
                        }
                        else
                        {
                            consolelog($"[+]Fail ! : " + Discord.lastexception.Message);
                            Discord.lastexception= null;
                            listBox2.TopIndex = listBox1.Items.Count - 1;
                        }
                           

                    }
                    catch (Exception ex)
                    {

                        attackstart(false);
                        MessageBox.Show(ex.Message);
                    }

                    Thread.Sleep(cooldown);
                }
                else
                {
                   break;
                }
               
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!isattacking)
            {
                attackstart(true);
            }
            else
            {
               
                attackstart(false);
            }
        }
        void attackstart(bool attackorstop)
        {
            Thread attackthread = new Thread(attackerthread);
            if (attackorstop)
            {
                consolelog("STARTED ! ");
                Discord.totalsentmessagecount = 0;
                listBox2.Items.Clear();
                attackbutton.Text = "stop";
                accestoattack = true;
                isattacking = true;
                attackthread.Start();
              
            }
            else if(!attackorstop)
            {
                consolelog("stopped all tasks");
                accestoattack=false;
                isattacking = false;
                attackbutton.Text = "attack";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
