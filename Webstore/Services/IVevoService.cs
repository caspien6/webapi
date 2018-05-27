using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data.Models;

namespace Webstore.Services
{
    public interface IVevoService
    {
        Vevo FindVevo(string name);

        void CreateVevoAsync(string identityId, string name, string password, string loginEmail, string szamlaszam = "empty");
    }
}
