using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Webstore.Controllers;
using Webstore.Data.Models;
using Webstore.Services;
using Xunit;

namespace XUnitTest
{
    public class CategoryControllerTest
    {
        private Mock<IKategoryService> _service;
        private CategoryController cc;

        public CategoryControllerTest()
        {
            _service = new Mock<IKategoryService>();
            var logger = new Mock<ILogger<CategoryController>>();
            cc = new CategoryController(_service.Object, logger.Object);
        }


        [Fact]
        public void Should_ReturnOk_When_GetWithoutParameters()
        {
            //Mocking
            var ter = new Kategoria
            {
                Nev = "Apple"
            };
            _service.Setup(x => x.GetAll()).Returns(
                new List<Kategoria> {
                    ter
                });

            //Setup
            var actionResult = cc.Get();

            //Assertion
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void Should_ReturnOk_When_GetId()
        {
            //Mocking
            var ter = new Kategoria
            {
                Id = 1,
                Nev = "Apple"
            };
            _service.Setup(x => x.GetById(1)).Returns(
                new List<Kategoria> {
                    ter
                });

            //Setup
            var actionResult = cc.Get();

            //Assertion
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
        }


    }
}
