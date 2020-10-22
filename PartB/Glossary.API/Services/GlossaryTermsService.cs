using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Glossary.API.DAL.Glossary;
using Glossary.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Glossary.API.Services
{
    public interface IGlossaryTermsService
    {
        Task<List<GlossaryTerm>> GetGlossaryTerms();
        Task<GlossaryTerm> CreateGlossaryTerm(GlossaryTerm glossaryTerm);
        Task<GlossaryTerm> UpdateGlossaryTerm(GlossaryTerm glossaryTerm);

        Task<bool> DeleteGlossaryTerm(long glossaryId);

    }
    public class GlossaryTermsService : IGlossaryTermsService
    {
        private readonly IMapper _autoMapper;
        private readonly GlossaryContext _glossaryContext;

        public GlossaryTermsService(GlossaryContext glossaryContext, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _glossaryContext = glossaryContext;
        }
        public async Task<List<GlossaryTerm>> GetGlossaryTerms()
        {
            var response = _glossaryContext.GlossaryTerms.ToList().OrderBy(x => x.Term);
            return _autoMapper.Map<List<GlossaryTerm>>(response);
        }

        public async Task<GlossaryTerm> CreateGlossaryTerm(GlossaryTerm glossaryTerm)
        {

            var response = _glossaryContext.GlossaryTerms.Add(_autoMapper.Map<DAL.Models.GlossaryTerm>(glossaryTerm));
            await _glossaryContext.SaveChangesAsync();
            return _autoMapper.Map<GlossaryTerm>(response.Entity);
        }

        public async Task<GlossaryTerm> UpdateGlossaryTerm(GlossaryTerm glossaryTerm)
        {
            var dalGlossaryTerm = await GetGlossaryTermById(glossaryTerm.Id);
            if (dalGlossaryTerm != null)
            {
                dalGlossaryTerm.Term = glossaryTerm.Term;
                dalGlossaryTerm.Definition = glossaryTerm.Definition;
                var response = _glossaryContext.GlossaryTerms.Update(dalGlossaryTerm);
                await _glossaryContext.SaveChangesAsync();
                return _autoMapper.Map<GlossaryTerm>(response.Entity);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteGlossaryTerm(long glossaryId)
        {
            var dalGlossaryTerm = await GetGlossaryTermById(glossaryId);
            if (dalGlossaryTerm != null)
            {
                _glossaryContext.GlossaryTerms.Remove(dalGlossaryTerm);
                await _glossaryContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<DAL.Models.GlossaryTerm> GetGlossaryTermById(long glossaryId)
        {
            return await _glossaryContext.GlossaryTerms.Where(x => x.Id == glossaryId).FirstOrDefaultAsync();
        }
    }

   
}
