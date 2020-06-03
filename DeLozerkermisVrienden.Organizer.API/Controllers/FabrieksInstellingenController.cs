using DeLozerkermisVrienden.Organizer.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeLozerkermisVrienden.Organizer.API.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api/fabrieksinstellingen")]
    public class FabrieksInstellingenController : ControllerBase
    {
        private readonly IFabrieksInstellingRepository _fabrieksInstellingRepository;

        public FabrieksInstellingenController(IFabrieksInstellingRepository fabrieksInstellingRepository)
        {
            _fabrieksInstellingRepository = fabrieksInstellingRepository ?? throw new ArgumentNullException(nameof(fabrieksInstellingRepository));
        }

        [HttpPost(Name = "FabrieksInstellingenTerugzetten")]
        public IActionResult FabrieksInstellingenTerugzetten()
        {
            _fabrieksInstellingRepository.FabrieksInstellingenTerugzetten();
            return NoContent();
        }
    }
}
