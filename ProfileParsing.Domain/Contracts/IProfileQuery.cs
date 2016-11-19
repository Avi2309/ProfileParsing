using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileParsing.Data.Models;

namespace ProfileParsing.Domain.Contracts
{
    public interface IProfileQuery
    {
        List<Profile> ProfileBySkills(List<string> i_skillsList);
        Task<List<Profile>> profileByName(string i_name);
    }
}
