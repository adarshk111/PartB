using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Glossary.API.Models;

namespace Glossary.API
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<DAL.Models.GlossaryTerm, GlossaryTerm>().ReverseMap();
        }
    }
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }
    }
}
