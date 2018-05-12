using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data.Models;
using Webstore.OwnExceptions;

namespace Webstore.Services
{
    public class KategoryService : IKategoryService
    {
        public IEnumerable<Kategoria> GetAll()
        {
            using (R0ga3cContext db = new R0ga3cContext(new Microsoft.EntityFrameworkCore.DbContextOptions<R0ga3cContext>()))
            {
                var query = from k in db.Kategoria
                            select k;
                return query.ToList();
            }
        }

        public IEnumerable<Kategoria> GetById(int id)
        {
            using (R0ga3cContext db = new R0ga3cContext(new Microsoft.EntityFrameworkCore.DbContextOptions<R0ga3cContext>()))
            {
                var query = from k in db.Kategoria
                            where k.Id == id
                            select k;
                if (query.ToArray().Length == 0)
                {
                    throw new EntityNotFoundException(id.ToString());
                }
                return query.ToList();
            }
        }

        public IEnumerable<Kategoria> GetByName(string name)
        {
            using (R0ga3cContext db = new R0ga3cContext(new Microsoft.EntityFrameworkCore.DbContextOptions<R0ga3cContext>()))
            {
                var query = from k in db.Kategoria
                            where k.Nev.Contains(name)
                            select k;
                if (query.ToArray().Length == 0)
                {
                    throw new EntityNotFoundException(name.ToString());
                }
                return query.ToList();
            }
        }
    }
}
