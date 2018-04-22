using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Webstore.Controllers;
using Webstore.Data.Models;
using Webstore.Services;
using Xunit;

namespace XUnitTest
{
    public class KategoryControllerTest
    {

        [Theory]
        [InlineData("Ede")]
        [InlineData("")]
        public void Should_NotFound_When_GetNameExceptionThrown(string name)
        {
            var mockDependency = new Mock<IKategoryService>();

            mockDependency.Setup(x =>  x.GetByName(name)).Throws(new Exception("Hiba"));

            
            var controller = new KategoryController(mockDependency.Object);

            ActionResult result = (ActionResult)controller.Get(name);
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(name, ((NotFoundObjectResult)result).Value);
        }

        [Theory]
        [InlineData("Ede")]
        public void Should_ReturnOk_When_GetNameFound(string name)
        {
            var mockDependency = new Mock<IKategoryService>();

            mockDependency.Setup(x => x.GetByName(name)).Returns(new List<Kategoria> {
                new Kategoria { Id = 1, Nev="Valami"}
            });

            // create thing being tested with a mock dependency
            var controller = new KategoryController(mockDependency.Object);

            ActionResult result = (ActionResult)controller.Get(name);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Should_NotFound_When_GetIdExceptionThrown()
        {
            var mockDependency = new Mock<IKategoryService>();

            mockDependency.Setup(x => x.GetById(1)).Throws(new Exception("Hiba"));

            // create thing being tested with a mock dependency
            var controller = new KategoryController(mockDependency.Object);

            ActionResult result = (ActionResult)controller.Get(1);
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(1, ((NotFoundObjectResult) result).Value );
        }

        [Theory]
        [InlineData(1)]
        public void Should_ReturnOk_When_GetIdFound(int id)
        {
            var mockDependency = new Mock<IKategoryService>();

            mockDependency.Setup(x => x.GetById(id)).Returns(new List<Kategoria> {
                new Kategoria { Id = 1, Nev="Valami"}
            });

            // create thing being tested with a mock dependency
            var controller = new KategoryController(mockDependency.Object);

            ActionResult result = (ActionResult)controller.Get(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Should_ReturnOk_When_GetWithoutException()
        {
            var mockDependency = new Mock<IKategoryService>();

            mockDependency.Setup(x => x.GetAll()).Returns(new List<Kategoria> {
                new Kategoria { Id = 1, Nev="Valami"}
            });

            
            var controller = new KategoryController(mockDependency.Object);

            ActionResult result = (ActionResult) controller.Get();
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void Should_InternalServerError_When_GetWithException()
        {
            var mockDependency = new Mock<IKategoryService>();

            mockDependency.Setup(x => x.GetAll()).Throws(new Exception());

            // create thing being tested with a mock dependency
            var controller = new KategoryController(mockDependency.Object);

            ActionResult result = (ActionResult)controller.Get();
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, (int)((StatusCodeResult)result).StatusCode);

        }

    }
}
