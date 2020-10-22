using Glossary.API.Controllers;
using Glossary.API.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glossary.API.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;

namespace Glossary.API.Tests.Controllers
{
    [TestClass]
    public class GlossaryTermsControllerTests
    {
        private IGlossaryTermsService subGlossaryTermsService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.subGlossaryTermsService = Substitute.For<IGlossaryTermsService>();
        }

        private GlossaryTermsController CreateGlossaryTermsController()
        {
            return new GlossaryTermsController(
                this.subGlossaryTermsService);
        }

        [TestMethod]
        public async Task GetGlossaryTerms_ShouldReturn_ValidResponse()
        {
            // Arrange
            var glossaryTermsController = this.CreateGlossaryTermsController();
            List<GlossaryTerm> glossaryTerms = new List<GlossaryTerm>()
            {
                new GlossaryTerm()
                {
                    Id = 1,
                    Definition = "New Value",
                    Term = "New"
                },
                new GlossaryTerm()
                {
                    Id = 2,
                    Definition = "online content to attract more visitors to a particular website",
                    Term = "clickbait"
                }
            };
            subGlossaryTermsService.GetGlossaryTerms().Returns(glossaryTerms);
            // Act
            var result = await glossaryTermsController.GetGlossaryTerms();

            // Assert
            Assert.IsNotNull(result);
           Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            await subGlossaryTermsService.Received(1).GetGlossaryTerms();
        }

        [TestMethod]
        public async Task PutGlossaryTerm_Should_Return_ValidResponse()
        {
            // Arrange
            var glossaryTermsController = this.CreateGlossaryTermsController();
            GlossaryTerm glossaryTerm = new GlossaryTerm()
            {
                Id = 1,
                Term = "New",
                Definition = "Something New"
            };
            subGlossaryTermsService.UpdateGlossaryTerm(glossaryTerm).Returns(glossaryTerm);
            // Act
            var result = await glossaryTermsController.PutGlossaryTerm(
                glossaryTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            await subGlossaryTermsService.Received(1).UpdateGlossaryTerm(glossaryTerm);
        }
        [TestMethod]
        public async Task PutGlossaryTerm_Should_InvalidServiceResult_Return_NotFoundResponse()
        {
            // Arrange
            var glossaryTermsController = this.CreateGlossaryTermsController();
            GlossaryTerm glossaryTerm = null;
            subGlossaryTermsService.UpdateGlossaryTerm(glossaryTerm).Returns(glossaryTerm);
            // Act
            var result = await glossaryTermsController.PutGlossaryTerm(
                glossaryTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            await subGlossaryTermsService.Received(1).UpdateGlossaryTerm(glossaryTerm);
        }

        [TestMethod]
        public async Task PostGlossaryTerm_ValidInput_ValidResponse()
        {
            // Arrange
            var glossaryTermsController = this.CreateGlossaryTermsController();
            GlossaryTerm glossaryTerm = new GlossaryTerm()
            {
                Id = 1,
                Term = "New",
                Definition = "Something New"
            };
            subGlossaryTermsService.CreateGlossaryTerm(glossaryTerm).Returns(glossaryTerm);

            // Act
            var result = await glossaryTermsController.PostGlossaryTerm(
                glossaryTerm);

            // Assert
            Assert.IsNotNull(result);
            await subGlossaryTermsService.Received(1).CreateGlossaryTerm(glossaryTerm);
        }
        [TestMethod]
        public async Task PostGlossaryTerm_InvalidInputs_BadRequestResponse()
        {
            // Arrange
            var glossaryTermsController = this.CreateGlossaryTermsController();
            GlossaryTerm glossaryTerm = new GlossaryTerm()
            {
                Id = 1,
                Term = "New",
                Definition = "Something New"
            };
            subGlossaryTermsService.CreateGlossaryTerm(glossaryTerm).Throws(new Exception());

            // Act
            var result = await glossaryTermsController.PostGlossaryTerm(
                glossaryTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            await subGlossaryTermsService.Received(1).CreateGlossaryTerm(glossaryTerm);
        }

        [TestMethod]
        public async Task DeleteGlossaryTerm_ValidInputs_ValidResponse()
        {
            // Arrange
            var glossaryTermsController = this.CreateGlossaryTermsController();
            subGlossaryTermsService.DeleteGlossaryTerm(2).Returns(true);
            

            // Act
            var result = await glossaryTermsController.DeleteGlossaryTerm(
                2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            await subGlossaryTermsService.Received(1).DeleteGlossaryTerm(2);
        }
        [TestMethod]
        public async Task DeleteGlossaryTerm_ServiceError_NotFoundResponse()
        {
            // Arrange
            var glossaryTermsController = this.CreateGlossaryTermsController();
            subGlossaryTermsService.DeleteGlossaryTerm(2).Returns(false);


            // Act
            var result = await glossaryTermsController.DeleteGlossaryTerm(
                2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            await subGlossaryTermsService.Received(1).DeleteGlossaryTerm(2);
        }
    }
}
