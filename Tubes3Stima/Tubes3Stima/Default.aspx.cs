using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Tubes3Stima
{
    public partial class _Default : Page
    {
        private List<DataRss> data;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ParsingRss();
            }
        }

        private void ParsingRss()
        {
            List<string> listRss = new List<string>();
            data = new List<DataRss>();

            StreamReader fileRss = new StreamReader(@"D:\ITB Notes\semester 4\Stima\Tubes\3\RssParser\RssProject\Tubes3Stima\Tubes3Stima\\LinkRssAll.txt");
            try
            {
                string linkLine;
                while ((linkLine = fileRss.ReadLine()) != null)
                {
                    listRss.Add(linkLine);
                }

                try
                {
                    XDocument dataTemp = new XDocument();
                    foreach (var linkParser in listRss)
                    {
                        dataTemp = XDocument.Load(linkParser);
                        var item = (from x in dataTemp.Descendants("item")
                                     select new
                                     {
                                         title = x.Element("title").Value,
                                         link = x.Element("link").Value,
                                         pubDate = x.Element("pubDate").Value,
                                         description = x.Element("description").Value
                                     });
                        if (item != null)
                        {
                            foreach (var itemAdd in item)
                            {
                                DataRss tempAdd = new DataRss
                                {
                                    Title = itemAdd.title,
                                    Link = itemAdd.link,
                                    PubDate = itemAdd.pubDate,
                                    Desc = itemAdd.description
                                };
                                data.Add(tempAdd);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ParsingHTML(string link)
        {

        }
    }
}