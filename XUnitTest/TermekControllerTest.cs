using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Webstore.Controllers;
using Webstore.Data.Models;
using Webstore.OwnExceptions;
using Webstore.Services;
using Xunit;

namespace XUnitTest
{
    public class TermekControllerTest
    {
        private Mock<ITermekService> _service;
        private TermekController tc;

        public TermekControllerTest()
        {
            _service = new Mock<ITermekService>();
            var logger = new Mock<ILogger<TermekController>>();
            tc = new TermekController(_service.Object, logger.Object);
        }

        [Fact]
        public void Should_ReturnOk_When_GetByNameMethodFoundTermek()
        {
            //Mocking
            var ter = new Termek
            {
                Nev = "Apple",
                Ar = 2000
            };
            _service.Setup(x => x.GetByName(It.IsAny<string>())).Returns(
                new List<Termek> {
                    ter
                });

            //Setup
            var actionResult = tc.GetTermekByName("Apple");

            //Assertion
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void Should_ReturnNotFound_When_GetByNameMethodNotFoundTermek()
        {
            //Mocking
            _service.Setup(x => x.GetByName(It.IsAny<string>())).Throws(new EntityNotFoundException("2"));

            //Setup
            var actionResult = tc.GetTermekByName("Apple");

            //Assertion
            var viewResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("Entity not found: 2", viewResult.Value);
        }

        [Fact]
        public void Should_ReturnInternalServerError_When_GetByNameUnexpectedExveption()
        {
            //Mocking
            _service.Setup(x => x.GetByName(It.IsAny<string>())).Throws(new Exception());

            //Setup
            var actionResult = tc.GetTermekByName("Apple");

            //Assertion
            var viewResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, viewResult.StatusCode);
        }
    }
}
