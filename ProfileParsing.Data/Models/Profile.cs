using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileParsing.Data.Models
{
    public class Profile
    {
        public string Name { get; set; }
        public string CurrentTitle { get; set; }
        public string CurrentPosition { get; set; }
        public string Summary { get; set; }
        public string ListOfSkills { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
    }
}
