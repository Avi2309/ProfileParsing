using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileParsing.Domain.Contracts
{
    public interface IProfileParsing
    {
        Task Parse(string i_profileUri);
    }
}
