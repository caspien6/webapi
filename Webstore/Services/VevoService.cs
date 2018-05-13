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

                bool vevoExists = queryVevo.ToArray().Length != 0;
                if (!vevoExists)
                {
                    var vevo = new Vevo { Nev = name, Szamlaszam = szamlaszam, Email = email };
                    db.Add(vevo);
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
