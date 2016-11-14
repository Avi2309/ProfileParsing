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
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//span");
            var profileName = string.Empty;
            foreach (HtmlNode span in collection)
            {
                profileName = span.InnerText;
            }
            return profileName;
        }

        private static string getCurrentTitle(HtmlDocument doc)
        {
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//span");
            var profileName = string.Empty;
            foreach (HtmlNode span in collection)
            {
                profileName = span.InnerText;
            }
            return profileName;

        }

        private static string getCurrentPosition(HtmlDocument doc)
        {
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//span");
            var profileName = string.Empty;
            foreach (HtmlNode span in collection)
            {
                profileName = span.InnerText;
            }
            return profileName;
        }

        private static string getSummary(HtmlDocument doc)
        {
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//span");
            var profileName = string.Empty;
            foreach (HtmlNode span in collection)
            {
                profileName = span.InnerText;
            }
            return profileName;
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
