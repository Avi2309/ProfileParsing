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
            _db = client.GetDatabase("ProfileParsing");
        }
        public async Task<bool> SetNewProfile(Profile i_profile)
        {
            try
            {
                await _db.GetCollection<Profile>("Profiles").InsertOneAsync(i_profile);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Profile>> SearchProfiles(string i_fullName)
        {
            try
            {
                var result = await _db.GetCollection<Profile>("Profiles").FindAsync(x => x.FullName.Contains(i_fullName));
                return result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Profile>> ProfilesBySkills(List<string> i_skillList)
        {
            try
            {
                var result = await _db.GetCollection<Profile>("Profiles")
                            .FindAsync(x => x.ListOfSkills
                                .Intersect(i_skillList).Any());
                return result.ToList();                
            }
            catch(Exception)
            {
                return null;
            }        
        }
    }
}
