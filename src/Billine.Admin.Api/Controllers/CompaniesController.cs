using AutoMapper;
using Billine.Admin.Contracts.Companies;
using Billine.Admin.Domain.Companies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Billine.Admin.Api.Controllers
{
    [Route("api/v1/companies")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<CompanyResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            List<Company> response = await _companyService.GetAll();

            return Ok(_mapper.Map<List<CompanyResponse>>(response));
        }

        [HttpGet, Route("CNPJ")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCNPJ([FromQuery] string cnpj)
        {
            Company response = await _companyService.GetByCNPJ(cnpj);

            return Ok(_mapper.Map<CompanyResponse>(response));
        }

        [HttpGet, Route("name")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<CompanyResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            List<Company> response = await _companyService.GetByName(name.ToUpper());

            return Ok(_mapper.Map<List<CompanyResponse>>(response));
        }
    }
}
