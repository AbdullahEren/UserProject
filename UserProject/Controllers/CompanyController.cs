using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos.CompanyDto;
using Entities.Models;
using Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace UserProject.Controllers
{
    [Route("api/companies")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyAsync(id, trackChanges: false);
            return Ok(company);         
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto companyDto)
        {
            await _companyService.CreateCompanyAsync(companyDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyForUpdateDto companyDto)
        {
            await _companyService.UpdateCompanyAsync(id, companyDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return Ok();
        }
    }
}
