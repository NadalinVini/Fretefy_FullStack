using Fretefy.Test.Domain.Dto;
using Fretefy.Test.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class RegiaoeController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoeController(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var regioes = _regiaoService.GetAll();
            return Ok(regioes);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var regioes = _regiaoService.GetByName(name);
            return Ok(regioes);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RegiaoDto regiao)
        {
            _regiaoService.Create(regiao);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] RegiaoDto regiao)
        {
            _regiaoService.Update(regiao);
            return Ok();
        }
    }
}
