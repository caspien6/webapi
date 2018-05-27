using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data.Models;
using Webstore.OwnExceptions;

namespace Webstore.Services
{
    public class VevoService : IVevoService
    {
        private R0ga3cContext _db;

        public VevoService(R0ga3cContext db)
        {
            _db = db;
        }

        public async void CreateVevoAsync(string identityId, string name, string password, string loginEmail, string szamlaszam = "empty")
        {
                var queryVevo = from v in _db.Vevo
                                where v.Nev == name && v.Szamlaszam == szamlaszam
                                select v;
                var queryStatus = from s in _db.Statusz
                                  where s.Id == 1
                                  select s;

                bool vevoExists = queryVevo.ToArray().Length != 0;
                var feldolgozasStatusz = queryStatus.FirstOrDefault();
            
                if (!vevoExists)
                {
                    var vevo = new Vevo { IdentityId = identityId, Nev = name, Jelszo = password, Login = loginEmail, Szamlaszam = szamlaszam, Email = loginEmail };
                    var kosara = new Kosar { Datum = DateTime.Now, StatuszId = feldolgozasStatusz.Id, VevoId = vevo.Id, Vevo = vevo };
                    await _db.AddAsync(vevo);
                    await _db.AddAsync(kosara);
                    await _db.SaveChangesAsync();
                }
                else
                {

                    throw new EntityAlreadyExistsException(name);
                }
            
            
                
            
        }

        public Vevo FindVevo(string username)
        {
            
                var queryVevo = from v in _db.Vevo.Include(vev => vev.Kosar)
                                where v.Email.Contains(username)
                                select v;
                var vevo = queryVevo.FirstOrDefault();
                if (vevo == null)
                {
                    throw new EntityNotFoundException(username);
                }
                return vevo;

            
        }
    }
}
