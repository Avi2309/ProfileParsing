using System;
using System.Collections.Generic;
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


        [Route("SetProfile")]
        [HttpPost]        
        public async Task<IHttpActionResult> SetProfile([FromBody]string profileUri)
        {
            try
            {
                await _profileParsing.Parse(profileUri);
                return Ok(new {result = "success"});
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
                return BadRequest("Error");
            }
        }
    }
}
