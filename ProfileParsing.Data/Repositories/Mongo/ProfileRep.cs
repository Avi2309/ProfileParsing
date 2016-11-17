using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GeoJsonObjectModel;
using ProfileParsing.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ProfileParsing.Data.Repositories
{
    public class ProfileRep
    {
        private readonly IMongoDatabase _db;
        public ProfileRep(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase("Test");
        }
        public async Task<bool> SetNewProfile(Profile profile)
        {
            try
            {
                await _db.GetCollection<Profile>("Profiles").InsertOneAsync(profile);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Profile>> SearchProfiles(string fullName)
        {
            try
            {
                var result = await _db.GetCollection<Profile>("Profiles").FindAsync(x => x.FullName == fullName);
                return result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task ProfilesBySkills(List<string> skillList)
        {
            JsonConvert.SerializeObject(skillList);
            _db.GetCollection<Profile>("Profiles").FindAsync(x => (JsonConvert.SerializeObject(x.ListOfSkills)));
        }
    }
}
