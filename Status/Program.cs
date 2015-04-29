using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.XPath;

namespace Status
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new Form2());
            Thread myThread = new Thread(lastToVk);
            myThread.Start(); 
        }

        static void lastToVk()
        {
            string buf = " ";
            string str = " ";
            while (!Settings1.Default.auth)
            {
                Thread.Sleep(500);
            }
            while (true)
            {
                string lastFm = GET("http://ws.audioscrobbler.com/2.0/?method=user.getrecenttracks&user=DonetSSS&api_key=72eea7cc279bbb9e1ffb4515acfd052b");
                XPathDocument xml = new XPathDocument(new System.IO.StringReader(lastFm));
                XPathNavigator nav = xml.CreateNavigator();
                foreach (XPathNavigator n in nav.Select("/lfm/recenttracks/track"))
                {
                    if (n.GetAttribute("nowplaying", "") == "true")
                    {
                        if (buf != n.SelectSingleNode("artist").Value + "%20-%20" + n.SelectSingleNode("name").Value)
                        {
                            buf = n.SelectSingleNode("artist").Value + "%20-%20" + n.SelectSingleNode("name").Value;
                            Console.Write(buf + " ");
                            string vkR =  GET("https://api.vk.com/method/audio.search.xml?q=" + buf + "&count=1&access_token=" + Settings1.Default.token);
                            XmlDocument vkXml = new XmlDocument();
                            vkXml.Load(new System.IO.StringReader(vkR));
                            foreach (XmlNode m in vkXml.SelectNodes("/response/audio"))
                            {
                                str = m.SelectSingleNode("owner_id").InnerText + "_" + m.SelectSingleNode("aid").InnerText;
                                Console.WriteLine(str);
                                string svkR = GET("https://api.vk.com/method/audio.setBroadcast.xml?audio=" + str + "&access_token=" + Settings1.Default.token);
                              
                            }
                        }
                    }
                }
                lastFm = "";
                Thread.Sleep(5000);
            }
        }

        private static string GET(string Url)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }
    }
}
