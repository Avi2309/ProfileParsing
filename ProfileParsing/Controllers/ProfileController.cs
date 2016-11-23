using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json.Converters;
using ProfileParsing.Domain.Contracts;

namespace ProfileParsing.Controllers
{
    [RoutePrefix("api")]
    public class ProfileController : ApiController
    {
        private readonly IProfileParsing _profileParsing;
        private readonly IProfileQuery _profileQuery;

        public ProfileController(IProfileQuery i_profileQuery, IProfileParsing i_profileParsing)
        {
            _profileQuery = i_profileQuery;
            _profileParsing = i_profileParsing;
        }


        [Route("SetProfile")]
        [HttpPost]        
        public async Task<IHttpActionResult> SetProfile(string profileUri)
        {
            try
            {
                await _profileParsing.Parse(profileUri);
                return Ok(new {result = "success"});
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return BadRequest("Error");
            }
        }

        [Route("SearchByName")]
        [HttpPost]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> SearchByName([FromBody]string name)
        {
            try
            {
                return Ok(await  _profileQuery.profileByName(name));
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return BadRequest("Error");
            }
        }

        [Route("SearchBySkills")]
        [HttpPost]
        [ResponseType(typeof(String))]
        public async Task<IHttpActionResult> SearchBySkills([FromBody]List<string> skillsList)
        {
            try
            {
                return Ok(await _profileQuery.ProfileBySkills(skillsList));
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return BadRequest("Error");
            }
        }
    }
}
