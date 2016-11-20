using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ProfileParsing.Domain.Contracts;

namespace ProfileParsing.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IProfileParsing _profileParsing;
        private readonly IProfileQuery _profileQuery;


        [Route("api/profile")]
        [HttpPost]
        public async Task<IHttpActionResult> setProfile(string i_profileUrl)
        {
            try
            {
                await _profileParsing.Parse(i_profileUrl);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("api/searchByName")]
        [HttpPost]
        public async Task<string> SearchPeople(string name)
        {
            try
            {
                return await  _profileQuery.profileByName(name);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [Route("api/searchBySkills")]
        [HttpPost]
        public async Task<string> peopleBySkills(List<string> skillsList)
        {

            try
            {
                return await _profileQuery.ProfileBySkills(skillsList);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
