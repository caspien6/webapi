using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Webstore.Data.Models;
using Webstore.OwnExceptions;
using Webstore.Services;
using Xunit;

namespace XUnitTest
{
    public class TermekServiceTest
    {
        private TermekService _service;
        private DbContextOptions<R0ga3cContext> options;

        public TermekServiceTest()
        {
            
            options = new DbContextOptionsBuilder<R0ga3cContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using(var context = new R0ga3cContext(options))
            {
                context.Termek.Add(new Termek {Nev = "Apple", Ar=3000,Raktarkeszlet=6, Views=3453356  });
                context.Termek.Add(new Termek {Nev = "Tv", Ar=4212341,Raktarkeszlet=2, Views=3  });
                context.Termek.Add(new Termek {Nev = "Toaster",Ar=200,Raktarkeszlet=1000, Views=356  });
                context.Termek.Add(new Termek {Nev = "Twist", Ar=1 ,Raktarkeszlet=10, Views=234  });
                context.Termek.Add(new Termek {Nev = "Peach", Ar=300,Raktarkeszlet=6, Views=4632  });
                context.SaveChanges();
            }

            _service = new TermekService(new R0ga3cContext(options));
        }

        [Fact]
        public void Should_ReturnAllTermek_When_ThereAreTermekInDb()
        {
            //Setup
            var allTermek = _service.GetAll();

            var count = 0;
            //Assertion
            foreach (var item in allTermek)
            {
                count++;
            }

            Assert.Equal(5, count);
        }

        [Fact]
        public void Should_ReturnTermek_When_GetByName()
        {
            //Setup
            var appleTermek = _service.GetByName("Apple");

            //Assertion
            foreach (var item in appleTermek)
            {
                Assert.Equal("Apple", item.Nev);
            }

            
        }

        [Fact]
        public void Should_ThrowError_When_GetByNameNotFoundEntity()
        {
            //Setup
            Action a = new Action(() =>_service.GetByName("This is not in the db"));
            //Assertion
            Assert.Throws<EntityNotFoundException>(a);
        }
    }
}
