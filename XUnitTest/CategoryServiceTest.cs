using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webstore.Data.Models;
using Webstore.Services;
using Xunit;

namespace XUnitTest
{
    public class CategoryServiceTest
    {
        private KategoryService _service;
        private DbContextOptions<R0ga3cContext> options;

        public CategoryServiceTest()
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

                var termek = new Termek { Nev = "Autó", Id = 1 };
                context.Termek.Add(termek);
                context.Kategoria.Add(new Kategoria { Id = 1, Nev = "Jármű", Termek = new List<Termek> { termek } } );

                context.Kosar.Add(kosar);
                context.Vevo.Add(vevo);
                context.Statusz.Add(status);

                context.SaveChanges();
            }

            _service = new KategoryService(new R0ga3cContext(options));
        }

        [Fact]
        public void Should_ReturnAllCategory_When_GetAll()
        {
            //Setup
            var allKategory = _service.GetAll();

            var count = 0;
            //Assertion
            foreach (var item in allKategory)
            {
                count++;
                Assert.Equal(1, item.Id);
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void Should_ReturnCategoryById_When_GetById()
        {
            //Setup
            var oneKategory = _service.GetById(1);

            var count = 0;
            //Assertion
            foreach (var item in oneKategory)
            {
                count++;
                Assert.Equal("Jármű", item.Nev);
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void Should_ReturnCategoryByName_When_GetByName()
        {
            //Setup
            var oneKategory = _service.GetByName("Jármű");

            var count = 0;
            //Assertion
            foreach (var item in oneKategory)
            {
                count++;
                Assert.Equal("Jármű", item.Nev);
                Assert.Equal(1, item.Id);
            }

            Assert.Equal(1, count);
        }

    }
}
