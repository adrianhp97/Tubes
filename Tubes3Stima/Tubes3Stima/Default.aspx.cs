using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Net;
using HtmlAgilityPack;

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
            string tempText = "";
            bool found = false;

            StreamReader fileRss = new StreamReader(@"C:\Users\MASTER\Desktop\MatKul SMT 4\Stima\Tubes 3\Tubes\Tubes3Stima\Tubes3Stima\\LinkRssAll.txt");
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
                                found = false;
                                tempText = itemAdd.title + itemAdd.description;
                                if (Searching.KmpMatch(itemAdd.link.ToLower(), "detik.com".ToLower()))
                                {
                                    WebClient webClient = new WebClient();
                                    var page = webClient.DownloadString(itemAdd.link.ToLower());
                                    HtmlDocument doc = new HtmlDocument();
                                    doc.LoadHtml(page);

                                    try
                                    {
                                        foreach (var td in doc.DocumentNode.SelectNodes("//div[@class='detail_text'][@id='detikdetailtext']"))
                                        {
                                            tempText = tempText + td.InnerText;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }

                                    if (Searching.KmpMatch(tempText.ToLower(), "vivada".ToLower()))
                                        found = true;
                                    else
                                        found = false;
                                }
                                else if (Searching.KmpMatch(itemAdd.link.ToLower(), "tempo.co".ToLower()))
                                {
                                    WebClient webClient = new WebClient();
                                    var page = webClient.DownloadString(itemAdd.link.ToLower());
                                    HtmlDocument doc = new HtmlDocument();
                                    doc.LoadHtml(page);

                                    try
                                    {
                                        foreach (var td in doc.DocumentNode.SelectNodes("//div[@class='artikel']/p"))
                                        {
                                            tempText = tempText + td.InnerText;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }

                                    if (Searching.KmpMatch(tempText.ToLower(), "vivada".ToLower()))
                                        found = true;
                                    else
                                        found = false;
                                }
                                else if (Searching.KmpMatch(itemAdd.link.ToLower(), "viva.co.id".ToLower()))
                                {
                                    WebClient webClient = new WebClient();
                                    var page = webClient.DownloadString(itemAdd.link.ToLower());
                                    HtmlDocument doc = new HtmlDocument();
                                    doc.LoadHtml(page);

                                    try
                                    {
                                        foreach (var td in doc.DocumentNode.SelectNodes("//p"))
                                        {
                                            tempText = tempText + td.InnerText;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }

                                    if (Searching.KmpMatch(tempText.ToLower(), "vivada".ToLower()))
                                        found = true;
                                    else
                                        found = false;
                                }
                                else if (Searching.KmpMatch(itemAdd.link.ToLower(), "antaranews.com".ToLower()))
                                {
                                    WebClient webClient = new WebClient();
                                    var page = webClient.DownloadString(itemAdd.link.ToLower());
                                    HtmlDocument doc = new HtmlDocument();
                                    doc.LoadHtml(page);

                                    try
                                    {
                                        foreach (var td in doc.DocumentNode.SelectNodes("//div[@id='content_news'][@itemprop='articleBody']"))
                                        {
                                            tempText = tempText + td.InnerText;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }

                                    if (Searching.KmpMatch(tempText.ToLower(), "vivada".ToLower()))
                                        found = true;
                                    else
                                        found = false;
                                }
                                if (found)
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
                    gvRss.DataSource = data;
                    gvRss.DataBind();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
               
            }
        }
    }
}