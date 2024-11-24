using Microsoft.AspNetCore.Mvc;

namespace FML.Familiares.API.Controllers
{
    [Route("api/familia")]

    public class FamiliaController : MainController
    {
        private readonly IFamilyService _familiaService;

        public FamiliaController(IFamilyService familiaService)
        {
            _familiaService = familiaService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AdicionarFamilia(Family family)
        {

            await _familiaService.AddFamily(family);

            return CustomResponse(family);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarFamilia(Guid id)
        {
            return CustomResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletarFamilia(Guid id)
        {
            return CustomResponse();
        }

        [HttpGet("familiares/{id:guid}")]
        public async Task<IActionResult> GetFamilyById(Guid id)
        {
            return CustomResponse();
        }

    }
}
