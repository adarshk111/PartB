using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glossary.API.DAL.Glossary;
using Glossary.API.Models;
using Glossary.API.Services;

namespace Glossary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlossaryTermsController : ControllerBase
    {
        private readonly IGlossaryTermsService _glossaryTermsService;


        public GlossaryTermsController(IGlossaryTermsService glossaryTermsService)
        {
            _glossaryTermsService = glossaryTermsService;
        }

        // GET: api/GlossaryTerms
        [HttpGet]
        public async Task<IActionResult> GetGlossaryTerms()
        {
            try
            {
                var response = await _glossaryTermsService.GetGlossaryTerms();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest("An unknown error occured");
            }
            
        }

        // PUT: api/GlossaryTerms/5
        [HttpPut]
        public async Task<IActionResult> PutGlossaryTerm(GlossaryTerm glossaryTerm)
        {
            try
            {
                var response = await _glossaryTermsService.UpdateGlossaryTerm(glossaryTerm);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
                
            }
        }

        // POST: api/GlossaryTerms
        [HttpPost]
        public async Task<IActionResult> PostGlossaryTerm(GlossaryTerm glossaryTerm)
        {
            try
            {
                var response = await _glossaryTermsService.CreateGlossaryTerm(glossaryTerm);
                return CreatedAtAction("PostGlossaryTerm", response);
            }
            catch (Exception ex)
            {
                return BadRequest("An unknown error occured");
            }
            
        }

        // DELETE: api/GlossaryTerms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlossaryTerm(long id)
        {
            try
            {
                bool response = await _glossaryTermsService.DeleteGlossaryTerm(id);
                if (response)
                {
                    return Ok(new {glossaryTermId = id});
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
