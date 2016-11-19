using System;
using System.Threading.Tasks;
using System.Web.Http;
using ProfileParsing.Domain.Contracts;

namespace ProfileParsing.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IProfileParsing _profileParsing;
        private readonly IProfileQuery _profileQuery;

        public ProfileController(IProfileParsing i_profileParsing, IProfileQuery i_profileQuery)
        {
            _profileParsing = i_profileParsing;
            _profileQuery = i_profileQuery;
        }

        [Route("api/profile")]
        [HttpPost]
        public async Task<IHttpActionResult> setProfile(string i_profileUrl)
        {
            try
            {
                _profileParsing.Parse(i_profileUrl);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("api/searchPeople")]
        [HttpPost]
        public async Task<IHttpActionResult> searchPeople(string name)
        {
            try
            {
                return _profileQuery.profileByName(name).Result;
            }
            catch (Exception)
            {

            }
        }

        [Route("api/peopleBySkills")]
        [HttpPost]
        public async Task<IHttpActionResult> peopleBySkills()
        {

            try
            {

            }
            catch (Exception)
            {

            }
        }
    }
}
