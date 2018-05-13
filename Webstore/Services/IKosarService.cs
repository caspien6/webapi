using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data.Models;

namespace Webstore.Services
{
    public interface IKosarService
    {
        IEnumerable<Kosar> FindKosars(int vevoId);
        void AddKosarTetel(int kosarId, Termek termek, int mennyiseg);
    }
}
