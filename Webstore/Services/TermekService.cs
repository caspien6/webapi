using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data.Models;
using Webstore.OwnExceptions;

namespace Webstore.Services
{
    public class TermekService : ITermekService
    {
        private R0ga3cContext _db;

        public TermekService(R0ga3cContext db)
        {
            _db = db;
        }

        public IEnumerable<Termek> GetAll()
        {
                var query = from t in _db.Termek
                            select t;
                return query.ToList();
        }

        public IEnumerable<Termek> GetById(int id)
        {
            
                var query = from t in _db.Termek
                            where t.Id == id
                            select t;
                if (query.ToArray().Length == 0)
                {
                    throw new EntityNotFoundException(id.ToString());
                }
                return query.ToList();
            
        }

        public IEnumerable<Termek> GetByName(string name)
        {
            
                var query = from t in _db.Termek
                            where t.Nev.Contains( name)
                            select t;
                if (query.ToArray().Length == 0)
                {
                    throw new EntityNotFoundException(name.ToString());
                }
                return query.ToList();
            
        }

        public IEnumerable<Termek> GetByKategoryId(int id)
        { 
        
                var query = from t in _db.Termek
                            where t.KategoriaId == id
                            select t;
                return query.ToList();
            
        }

        public void IncreaseViews(int id)
        {
            var query = from t in _db.Termek
                        where t.Id == id
                        select t;
            foreach (var item in query)
            {
                item.Views += 1;
            }
            _db.SaveChanges();
            
        }
    }
}
