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
        public void CreateVevo(string name, string szamlaszam = "empty", string email = "empty")
        {
            using (var db = new R0ga3cContext(new Microsoft.EntityFrameworkCore.DbContextOptions<R0ga3cContext>()))
            {
                
                var queryVevo = from v in db.Vevo
                                where v.Nev == name && v.Szamlaszam == szamlaszam
                                select v;
                var queryStatus = from s in db.Statusz
                                  where s.Id == 1
                                  select s;

                bool vevoExists = queryVevo.ToArray().Length != 0;
                var feldolgozasStatusz = queryStatus.FirstOrDefault();
                if (!vevoExists)
                {
                    var vevo = new Vevo { Nev = name, Szamlaszam = szamlaszam, Email = email };
                    var kosara = new Kosar { Datum = DateTime.Now, StatuszId = 1, Statusz = feldolgozasStatusz, TelephelyId = 1, Vevo = vevo, VevoId = vevo.Id };
                    db.Add(vevo);
                    db.Add(kosara);
                    db.SaveChanges();
                }
                else
                {
                    throw new EntityAlreadyExistsException(name);
                }
            }
        }

        public Vevo FindVevo(string name)
        {
            using (var db = new R0ga3cContext(new Microsoft.EntityFrameworkCore.DbContextOptions<R0ga3cContext>()))
            {
                var queryVevo = from v in db.Vevo
                                where v.Nev.Contains(name)
                                select v;
                var vevo = queryVevo.FirstOrDefault();
                if (vevo == null)
                {
                    throw new EntityNotFoundException(name);
                }
                return vevo;

            }
        }
    }
}
