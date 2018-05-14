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
        private R0ga3cContext _db;

        public KategoryService(R0ga3cContext db)
        {
            _db = db;
        }

        public IEnumerable<Kategoria> GetAll()
        {
            
                var query = from k in _db.Kategoria
                            select k;
                return query.ToList();
            
        }

        public IEnumerable<Kategoria> GetById(int id)
        {
            
                var query = from k in _db.Kategoria
                            where k.Id == id
                            select k;
                if (query.ToArray().Length == 0)
                {
                    throw new EntityNotFoundException(id.ToString());
                }
                return query.ToList();
            
        }

        public IEnumerable<Kategoria> GetByName(string name)
        {
            
                var query = from k in _db.Kategoria
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
