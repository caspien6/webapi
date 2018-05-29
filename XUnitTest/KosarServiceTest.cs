using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webstore.Data.Models;
using Webstore.OwnExceptions;
using Webstore.Services;
using Xunit;

namespace XUnitTest
{
    public class KosarServiceTest
    {
        private KosarService _service;
        private DbContextOptions<R0ga3cContext> options;
        

        public KosarServiceTest()
        {

            options = new DbContextOptionsBuilder<R0ga3cContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new R0ga3cContext(options))
            {
                var vevo = new Vevo { Id = 1, Nev = "Ede" };
                var kosar = new Kosar { Id = 1, Vevo = vevo };
                var status = new Statusz { Id = 1, Nev = "Dolgozunk" };
                kosar.Statusz = status;
                vevo.Kosar = new List<Kosar> { kosar };

                context.Kosar.Add(kosar);
                context.Vevo.Add(vevo);
                context.Statusz.Add(status);
                
                context.SaveChanges();
            }

            _service = new KosarService(new R0ga3cContext(options));
        }

        [Fact]
        public void Should_ReturnAllKosar_When_GetVevoKosars()
        {
            //Setup
            var allKosar = _service.FindKosars(1);

            var count = 0;
            //Assertion
            foreach (var item in allKosar)
            {
                count++;
                Assert.Equal(1, item.Statusz.Id);
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void Should_MakeNewKosarTetel_When_AddKosarTetel()
        {

            //Setup
            var appleTermek = new Termek { Id = 1, Nev = "Apple" };
            using (var context = new R0ga3cContext(options))
            {
                context.Termek.Add(appleTermek);
                context.SaveChanges();
            }

            _service.AddKosarTetel(1, appleTermek, 3);
            //Assertion
            using(var context = new R0ga3cContext(options))
            {
                var queryTetel = from kt in context.KosarTetel.Include(z => z.Kosar)
                                 where kt.Kosar.Id == 1
                                 select kt;
                foreach (var item in queryTetel)
                {
                    Assert.Equal(1, item.TermekId);
                    Assert.Equal(3, item.Mennyiseg);
                    Assert.Equal(1, item.KosarId);
                }
            }

        }

        [Fact]
        public void Should_ThrowException_When_MoreQuantityThenStock()
        {

            //Setup
            var appleTermek = new Termek { Id = 1, Nev = "Apple" , Raktarkeszlet = 2};
            using (var context = new R0ga3cContext(options))
            {
                context.Termek.Add(appleTermek);
                context.SaveChanges();
            }

            Action a = new Action(() => _service.AddKosarTetel(1, appleTermek, 4));
            Assert.Throws<ProductQuantityException>(a);

        }

        [Fact]
        public void Should_ThrowException_When_TermekNotExists()
        {

            //Setup
            var appleTermek = new Termek { Id = 1, Nev = "Harmat", Raktarkeszlet = 2 };

            Action a = new Action(() => _service.AddKosarTetel(1, appleTermek, 4));
            Assert.Throws<EntityNotFoundException>(a);

        }

    }
}
