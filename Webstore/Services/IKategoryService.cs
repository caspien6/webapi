using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data.Models;

namespace Webstore.Services
{
    public interface IKategoryService
    {
        IEnumerable<Kategoria> GetAll();
        IEnumerable<Kategoria> GetById(int id);
        IEnumerable<Kategoria> GetByName(string name);
    }
}
