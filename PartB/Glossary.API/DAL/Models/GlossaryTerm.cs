using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glossary.API.DAL.Models
{
    public class GlossaryTerm 
    {
        public long Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
