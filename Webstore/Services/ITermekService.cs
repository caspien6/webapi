using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data.Models;

namespace Webstore.Services
{
    public interface ITermekService
    {
        IEnumerable<Termek> GetAll();
        IEnumerable<Termek> GetById(int id);
        IEnumerable<Termek> GetByKategoryId(int id);
        IEnumerable<Termek> GetByName(string name);
        void IncreaseViews(int id);
    }
}
