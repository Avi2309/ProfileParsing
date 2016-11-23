using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using HtmlAgilityPack;
using Newtonsoft.Json;
using ProfileParsing.Data.Contracts;
using ProfileParsing.Data.Models;
using ProfileParsing.Domain.Contracts;
using RestSharp;

namespace ProfileParsing.Domain.Services
{
    public class ProfileParsing : IProfileParsing
    {
        private IProfileRep profileRep;

        public ProfileParsing(IProfileRep i_profileRep)
        {
            profileRep = i_profileRep;
        }

        public async Task Parse(string i_profileUri)
        {
            var profileUri = new Uri(i_profileUri);
            var client = new RestClient(profileUri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("accept-encoding", "gzip, deflate, sdch, br");
            HtmlDocument doc = new HtmlDocument();

            try
            {
                //client.AddDefaultHeader("accept-encoding", "gzip, deflate, sdch, br");
                var res = await client.ExecuteTaskAsync(request);
                doc.LoadHtml(res.Content);

                Profile newProfile = new Profile();
                newProfile.FullName = getPersonName(doc);
                newProfile.CurrentTitle = getCurrentTitle(doc);
                newProfile.CurrentPosition = getCurrentPosition(doc);
                newProfile.Summary = getSummary(doc);
                newProfile.ListOfSkills = getSkills(doc);
                newProfile.Experience = getExperience(doc);
                newProfile.Education = getEducation(doc);

                await profileRep.SetNewProfile(newProfile);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                throw new Exception();
            }

        }

        private string getPersonName(HtmlDocument doc)
        {
            try
            {
                HtmlNode fullNameNode = doc.DocumentNode.SelectSingleNode("//h1[@id='name']");
                var profileName = fullNameNode.InnerHtml;
                return profileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string getCurrentTitle(HtmlDocument doc)
        {
            try
            {
                HtmlNode fullNameNode = doc.DocumentNode.SelectSingleNode("//p[@class='title']");
                var profileName = fullNameNode.InnerHtml;
                return profileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string getCurrentPosition(HtmlDocument doc)
        {
            try
            {
                HtmlNode currentPositionNode =
                    doc.DocumentNode.SelectSingleNode(
                        "//div[@class='editable-item section-item current-position']//h4//a");
                var currentPosition = currentPositionNode.InnerHtml;
                return currentPosition;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string getSummary(HtmlDocument doc)
        {
            try
            {
                HtmlNode currentPositionNode =
                    doc.DocumentNode.SelectSingleNode("//div[@class='summary']//p[@class='description']");
                var summary = currentPositionNode.InnerHtml;
                return summary;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private List<string> getSkills(HtmlDocument doc)
        {
            try
            {
                HtmlNodeCollection skillNodes =
                    doc.DocumentNode.SelectNodes("//ul[@class='skills-section']//span[@class='endorse-item-name']//a");
                var skillList = new List<string>();
                foreach (HtmlNode skill in skillNodes)
                {
                    skillList.Add(skill.InnerHtml);
                }
                return skillList;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        private string getExperience(HtmlDocument doc)
        {
            try
            {
                List<dynamic> expList = new List<dynamic>();
                HtmlNodeCollection positionTitleNodes =
                    doc.DocumentNode.SelectNodes("//div[@id='background-experience']//div//div//h4");
                HtmlNodeCollection summaryNodes =
                    doc.DocumentNode.SelectNodes(
                        "//div[@id='background-experience']//p[@class='description summary-field-show-more']");

                for (int i = 0; i < positionTitleNodes.Count; i++)
                {
                    expList.Add(
                        new
                        {
                            title = positionTitleNodes[i].InnerHtml,
                            positionSummary = summaryNodes[i].InnerHtml
                        });

                }

                return JsonConvert.SerializeObject(expList);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string getEducation(HtmlDocument doc)
        {
            try
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
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
