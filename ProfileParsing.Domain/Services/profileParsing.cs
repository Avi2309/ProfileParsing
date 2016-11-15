using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            HtmlNodeCollection skillNodes = doc.DocumentNode.SelectNodes("//ul[@class='skills-section']//span[@class='endorse-item-name']//a");
            var skillList = new List<string>();
            foreach (HtmlNode skill in skillNodes)
            {
                skillList.Add(skill.InnerHtml);
            }
            return JsonConvert.SerializeObject(skillList);
        }

        private static string getExperience(HtmlDocument doc)
        {
            List<dynamic> expList = new List<dynamic>();
            HtmlNodeCollection positionTitleNodes = doc.DocumentNode.SelectNodes("//div[@id='background-experience']//div//div//h4");
            HtmlNodeCollection summaryNodes = doc.DocumentNode.SelectNodes("//div[@id='background-experience']//p[@class='description summary-field-show-more']");

            for (int i = 0; i < positionTitleNodes.Count; i++)
            {
                expList.Add(
                    new { title = positionTitleNodes[i].InnerHtml,
                          positionSummary = summaryNodes[i].InnerHtml
                        });

            }

            return JsonConvert.SerializeObject(expList);
        }

        private static string getEducation(HtmlDocument doc)
        {
            List<dynamic> educationList = new List<dynamic>();
            HtmlNodeCollection schoolTitleNodes = doc.DocumentNode.SelectNodes("//div[@id=background-education]//h4[@class='summary fn org']//a");
            HtmlNodeCollection facultyNodes = doc.DocumentNode.SelectNodes("//div[@id=background-education]//h5[@class='major']//a");

            for (int i = 0; i < schoolTitleNodes.Count; i++)
            {
                educationList.Add(
                    new
                    {
                        school = schoolTitleNodes[i].InnerHtml,
                        faculty = facultyNodes[i].InnerHtml
                    });

            }

            return JsonConvert.SerializeObject(educationList);
        }
    }
}
