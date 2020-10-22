using AutoMapper;
using Glossary.API.DAL.Glossary;
using Glossary.API.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Glossary.API.Models;

namespace Glossary.API.Tests.Services
{
    [TestClass]
    public class GlossaryTermsServiceTests
    {
        private GlossaryContext subGlossaryContext;
        private IMapper subMapper;
        private ServiceCollection _serviceCollection;

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<GlossaryContext>(c => c.UseInMemoryDatabase("GlossaryDb"));
            using (subGlossaryContext = _serviceCollection.BuildServiceProvider().GetService<GlossaryContext>())
            {
                TestDataSeeder.PopulateTestData(subGlossaryContext);
            }
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapper()));
            subMapper = config.CreateMapper();
        }
      

        [TestMethod]
        public async Task GetGlossaryTerms_Should_Return_AllValues()
        {
            using (var glossaryContext = _serviceCollection.BuildServiceProvider().GetService<GlossaryContext>())
            {
                TestDataSeeder.PopulateTestData(glossaryContext);
                var service = this.CreateService(glossaryContext);

                // Act
                var result = await service.GetGlossaryTerms();

                // Assert
                Assert.AreEqual(2, result.Count);
            }
            
        }

        [TestMethod]
        public async Task CreateGlossaryTerm_ValidInputs_Should_Create_NewRecord()
        {
            using (var subGlossaryContext = _serviceCollection.BuildServiceProvider().GetService<GlossaryContext>())
            {
                TestDataSeeder.PopulateTestData(subGlossaryContext);
                var service = this.CreateService(subGlossaryContext);
                GlossaryTerm glossaryTerm = new GlossaryTerm()
                {
                    Term = "chillax",
                    Definition = "calm down and relax"
                };

                // Act
                var result = await service.CreateGlossaryTerm(
                    glossaryTerm);

                // Assert
                Assert.AreEqual(glossaryTerm.Definition, result.Definition);
                Assert.AreEqual(glossaryTerm.Term, result.Term);
                Assert.AreEqual(3, subGlossaryContext.GlossaryTerms.Count());
            }
        }

        [TestMethod]
        public async Task UpdateGlossaryTerm_ValidInputs_Should_UpdateRecord()
        {
            // Arrange
            using (var subGlossaryContext = _serviceCollection.BuildServiceProvider().GetService<GlossaryContext>())
            {
                TestDataSeeder.PopulateTestData(subGlossaryContext);
                var service = this.CreateService(subGlossaryContext);
                GlossaryTerm glossaryTerm = new GlossaryTerm()
                {
                    Id = 2,
                    Definition = "Changed Meaning"
                };

                // Act
                var result = await service.UpdateGlossaryTerm(
                    glossaryTerm);
                
                // Assert
                Assert.AreEqual(glossaryTerm.Definition, subGlossaryContext.GlossaryTerms.Where(x => x.Id == 2).FirstOrDefault().Definition);
            }
        }
        [TestMethod]
        public async Task UpdateGlossaryTerm_InvalidInputs_Should_Return_Null()
        {
            // Arrange
            using (var subGlossaryContext = _serviceCollection.BuildServiceProvider().GetService<GlossaryContext>())
            {
                TestDataSeeder.PopulateTestData(subGlossaryContext);
                var service = this.CreateService(subGlossaryContext);
                GlossaryTerm glossaryTerm = new GlossaryTerm()
                {
                    Id = 10,
                    Definition = "Something invalid"
                };

                // Act
                var result = await service.UpdateGlossaryTerm(
                    glossaryTerm);

                // Assert
                Assert.AreEqual(null, result);
            }
        }


        [TestMethod]
        public async Task DeleteGlossaryTerm_ValidId_ShouldDeleteRecord()
        {
            using (var subGlossaryContext = _serviceCollection.BuildServiceProvider().GetService<GlossaryContext>())
            {
                TestDataSeeder.PopulateTestData(subGlossaryContext);
                var service = this.CreateService(subGlossaryContext);
                long glossaryId = 1;

                // Act
                var result = await service.DeleteGlossaryTerm(
                    glossaryId);

                // Assert
                Assert.IsTrue(!subGlossaryContext.GlossaryTerms.Any(x => x.Id == 1));
            }
        }
        [TestMethod]
        public async Task DeleteGlossaryTerm_InvalidId_Should_Return_False()
        {
            using (var subGlossaryContext = _serviceCollection.BuildServiceProvider().GetService<GlossaryContext>())
            {
                TestDataSeeder.PopulateTestData(subGlossaryContext);
                var service = this.CreateService(subGlossaryContext);
                long glossaryId = 10;

                // Act
                var result = await service.DeleteGlossaryTerm(
                    glossaryId);

                // Assert
                Assert.IsFalse(result);
            }
        }
        private GlossaryTermsService CreateService(GlossaryContext context)
        {
            return new GlossaryTermsService(context,
                this.subMapper);
        }
    }
}
