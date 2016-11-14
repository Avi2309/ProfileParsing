using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace ProfileParsing.Controllers
{
    public class ProfileController : ApiController
    {
        [Route("api/profile")]
        [HttpPost]
        public async Task<IHttpActionResult> setProfile(string profileUrl)
        {

            try
            {
                

            }
            catch (Exception)
            {

            }
        }

        [Route("api/searchPeople")]
        [HttpPost]
        public async Task<IHttpActionResult> searchPeople()
        {

            try
            {

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
