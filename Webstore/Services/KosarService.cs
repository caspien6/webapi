using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Webstore.Data.Models;
using Webstore.OwnExceptions;

namespace Webstore.Services
{
    public class KosarService : IKosarService
    {

        private R0ga3cContext _db;

        public KosarService(R0ga3cContext db)
        {
            _db = db;
        }

        public IEnumerable<Kosar> FindKosars(int vevoId)
        {               
                var queryKosar = from k in _db.Kosar.Include(nameof(KosarTetel)).Include(nameof(Statusz))
                                where k.VevoId == vevoId
                                select k;
                var vevo = queryKosar.ToArray();
                if (vevo.Length == 0)
                {
                    throw new EntityNotFoundException(vevoId.ToString());
                }
            return vevo;
        }

        public void AddKosarTetel(int kosarId, Termek termek, int mennyiseg)
        {
            
                var queryKosar = from k in _db.Kosar
                                where k.Id == kosarId
                                select k;

                var queryTermek = from t in _db.Termek
                                  where t.Nev.Equals(termek.Nev)
                                  select t;

                var queryStatus = from s in _db.Statusz
                                  where s.Id == 1
                                  select s;

                var kosar = queryKosar.FirstOrDefault();
                var feldolgozasStatusz = queryStatus.FirstOrDefault();
                termek = queryTermek.FirstOrDefault();

                if (kosar == null || termek == null)
                {
                    throw new EntityNotFoundException("");
                }
                if (termek.Raktarkeszlet < mennyiseg)
                {
                    throw new ProductQuantityException(termek.Nev);
                }

                var kosartetel = new KosarTetel {
                    Ar = termek.Ar * mennyiseg,
                    Mennyiseg = mennyiseg,
                    Kosar = kosar,
                    KosarId = kosar.Id,
                    Statusz = feldolgozasStatusz,
                    StatuszId = feldolgozasStatusz.Id,
                    Termek = termek,
                    TermekId = termek.Id
                };

                _db.Add(kosartetel);
                _db.SaveChanges();
            
        }
    }
}
