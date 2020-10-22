using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glossary.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Glossary.API.DAL.Glossary
{
    public class GlossaryContext : DbContext
    {
        public DbSet<GlossaryTerm> GlossaryTerms { get; set; }

        public GlossaryContext(DbContextOptions<GlossaryContext> options)
            : base(options)
        {

        }
    }
}
