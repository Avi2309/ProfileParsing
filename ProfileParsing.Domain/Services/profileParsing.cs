using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;

namespace ProfileParsing.Domain.Services
{
    public static class ProfileParsing
    {
        public static bool Parse(string i_profileUri)
        {
            var profileUri = new Uri(i_profileUri);
            var client = new RestClient(profileUri);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);
            var profilePageRes = response.Content;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(profilePageRes);
            getPersonName(doc);
        }

        private static string getPersonName(HtmlDocument doc)
        {
            HtmlNode fullNameNode = doc.DocumentNode.SelectSingleNode("//span[@class='full-name']");
            var profileName = fullNameNode.InnerHtml;
            return profileName;
        }

        private static string getCurrentTitle(HtmlDocument doc)
        {
            HtmlNode fullNameNode = doc.DocumentNode.SelectSingleNode("//p[@class='title']");
            var profileName = fullNameNode.InnerHtml;
            return profileName;
        }

        private static string getCurrentPosition(HtmlDocument doc)
        {
            HtmlNode currentPositionNode = doc.DocumentNode.SelectSingleNode("//div[@class='editable-item section-item current-position']//h4//a");
            var currentPosition = currentPositionNode.InnerHtml;
            return currentPosition;
        }

        private static string getSummary(HtmlDocument doc)
        {
            HtmlNode currentPositionNode = doc.DocumentNode.SelectSingleNode("//div[@class='summary']//p[@class='description']");
            var summary = currentPositionNode.InnerHtml;            
            return summary;
        }

        private static string getSkills(HtmlDocument doc)
        {
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//span");
            var profileName = string.Empty;
            foreach (HtmlNode span in collection)
            {
                profileName = span.InnerText;
            }
            return profileName;
        }

        private static string getExperience(HtmlDocument doc)
        {
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//span");
            var profileName = string.Empty;
            foreach (HtmlNode span in collection)
            {
                profileName = span.InnerText;
            }
            return profileName;
        }

        private static string getEducation(HtmlDocument doc)
        {
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//span");
            var profileName = string.Empty;
            foreach (HtmlNode span in collection)
            {
                profileName = span.InnerText;
            }
            return profileName;

        }
    }
}
