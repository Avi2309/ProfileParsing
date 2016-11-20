using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProfileParsing.Data.Contracts;
using ProfileParsing.Data.Models;
using ProfileParsing.Domain.Contracts;


namespace ProfileParsing.Domain.Services
{
    public class ProfileQuery: IProfileQuery
    {
        private readonly IProfileRep profileRep;

        public ProfileQuery(IProfileRep i_profileRep)
        {
            profileRep = i_profileRep;
        }
        public async Task<string> profileByName(string i_name) 
        {
            var profilesResult = await profileRep.SearchProfiles(i_name);
            return JsonConvert.SerializeObject(profilesResult);
        }

        public async Task<string> ProfileBySkills(List<string> i_skillsList)
        {
            var memoryCache = MemoryCache.Default;
            var skillsListToGet = new List<string>();
            var profilesRes = new List<Profile>();

            try
            {
                foreach (var skill in i_skillsList)
                {
                    if (!memoryCache.Contains("skill." + skill))
                    {
                        skillsListToGet.Add(skill);
                    }
                    else
                    {
                        profilesRes.AddRange(memoryCache.Get("skill." + skill) as List<Profile>);
                    }
                }

                var profilesRepRes = await profileRep.ProfilesBySkills(skillsListToGet);

                int cacheExpireMin;
                if (!int.TryParse(ConfigurationManager.AppSettings["cacheExpireMin"], out cacheExpireMin))
                    cacheExpireMin = 15;
                var expiration = DateTimeOffset.UtcNow.AddMinutes(cacheExpireMin);

                foreach (var skill in skillsListToGet)
                {
                    var matchProfiles = profilesRepRes.Where(x => x.ListOfSkills.Contains(skill)).ToList();
                    profilesRes.AddRange(matchProfiles);
                    memoryCache.Add("skill." + skill, matchProfiles, expiration);
                }

                List<Profile> profilesResGroupById = profilesRes.GroupBy(x => x.Id)
                    .Select(grp => grp.FirstOrDefault()).ToList();
                return JsonConvert.SerializeObject(profilesResGroupById);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
