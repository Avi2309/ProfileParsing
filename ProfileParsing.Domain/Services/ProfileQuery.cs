using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ProfileParsing.Data.Contracts;
using ProfileParsing.Data.Models;


namespace ProfileParsing.Domain.Services
{
    public class ProfileQuery
    {
        private readonly IProfileRep profileRep;

        public ProfileQuery(IProfileRep i_profileRep)
        {
            profileRep = i_profileRep;
        }
        public Task<List<Profile>> profileByName(string i_name) 
        {
            var profilesResult = profileRep.SearchProfiles(i_name);
            return profilesResult;
        }

        public List<Profile> ProfileBySkills(List<string> i_skillsList)
        {
            var memoryCache = MemoryCache.Default;
            var skillsListToGet = new List<string>();
            List<Profile> profilesRes = new List<Profile>();

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

                var profilesRepRes = profileRep.ProfilesBySkills(skillsListToGet);

                int cacheExpireMin;
                if (!int.TryParse(ConfigurationManager.AppSettings["cacheExpireMin"], out cacheExpireMin))
                    cacheExpireMin = 15;
                var expiration = DateTimeOffset.UtcNow.AddMinutes(cacheExpireMin);

                foreach (var skill in skillsListToGet)
                {
                    var matchProfiles = profilesRepRes.Result.Where(x => x.ListOfSkills.Contains(skill)).ToList();
                    profilesRes.AddRange(matchProfiles);
                    memoryCache.Add("skill." + skill, matchProfiles, expiration);
                }

                List<Profile> profilesResGroupById = profilesRes.GroupBy(x => x.Id)
                    .Select(grp => grp.FirstOrDefault()).ToList();
                return profilesResGroupById;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
