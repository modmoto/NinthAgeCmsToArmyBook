using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinthAgeCmsToArmyBook.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ArmiesController : ControllerBase
{
    private readonly ArmyRepository _armyRepository;

    public ArmiesController(ArmyRepository armyRepository)
    {
        _armyRepository = armyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArmyBook>>> Get()
    {
        var armyVersionsList = await _armyRepository.LoadArmies();
        return Ok(armyVersionsList);
    }
}