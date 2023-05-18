using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinthAgeCmsToArmyBook.Api.MongoDb;
using NinthAgeCmsToArmyBook.Shared.Changes;

namespace NinthAgeCmsToArmyBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChangeController : ControllerBase
{
    private readonly ArmyRepository _armyRepository;
    private readonly ChangeManager _changeManager;

    public ChangeController(ArmyRepository armyRepository, ChangeManager changeManager)
    {
        _armyRepository = armyRepository;
        _changeManager = changeManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<UnitChange>>> GetChangesForArmy([FromQuery] string armyName, [FromQuery] string version)
    {
        var army = await _armyRepository.LoadArmiesByName(armyName);
        var currentVersion = army.FirstOrDefault(a => a.BookVersion == version);
        var smallerVersions = army.LastOrDefault(a => string.CompareOrdinal(a.BookVersion, version) < 0);
        var changesToLastVersion = _changeManager.CreateChange(currentVersion, smallerVersions);
        return Ok(changesToLastVersion);
    }
}