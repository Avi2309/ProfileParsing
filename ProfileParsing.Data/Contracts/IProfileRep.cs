using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ProfileParsing.Data.Models;

namespace ProfileParsing.Data.Contracts
{
    public interface IProfileRep
    {
        Task<bool> SetNewProfile(Profile profile);
        Task<List<Profile>> SearchProfiles(string i_fullName);
        Task<List<Profile>> ProfilesBySkills(List<string> skillList);

    }
}
