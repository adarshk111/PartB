using System;
using System.Collections.Generic;
using System.Text;
using Glossary.API.DAL.Glossary;
using Microsoft.EntityFrameworkCore.Internal;

namespace Glossary.API.Tests
{
    static class TestDataSeeder
    {
        public static void PopulateTestData(GlossaryContext glossaryContext)
        {
            if (glossaryContext.GlossaryTerms.Any())
            {
                return;
            }

            glossaryContext.GlossaryTerms.Add(new DAL.Models.GlossaryTerm()
            {
                Term = "automagically",
                Definition = "in a way that seems magical, especially by computer"
            });
            glossaryContext.GlossaryTerms.Add(new DAL.Models.GlossaryTerm()
            {
                Term = "bitcoin",
                Definition = "an online payment system that does not require an intermediary"
            });
            glossaryContext.SaveChanges();
        }
    }
}
