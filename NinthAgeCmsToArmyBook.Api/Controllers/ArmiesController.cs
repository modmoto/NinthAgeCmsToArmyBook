using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NinthAgeCmsToArmyBook.Api.MongoDb;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArmiesController : ControllerBase
{
    private readonly ArmyRepository _armyRepository;

    public ArmiesController(ArmyRepository armyRepository)
    {
        _armyRepository = armyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArmyBook>>> Get([FromQuery] string nameFilter)
    {
        return nameFilter == default 
            ? Ok(await LoadArmyVersions())
            : Ok(await _armyRepository.LoadArmiesByName(nameFilter));
    }

    private async Task<List<ArmyVersions>> LoadArmyVersions()
    {
        var loadArmiesByName = await _armyRepository.LoadArmies();
        if (!loadArmiesByName.Any())
        {
            await _armyRepository.InitArmies();
            loadArmiesByName = await _armyRepository.LoadArmies();
        }
        var latestVersion = loadArmiesByName.GroupBy(a => a.ArmyName).Select(g => g.Last());
        return latestVersion.Select(a => new ArmyVersions(a.ArmyName, a.BookVersion)).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArmyBook>> GetById([FromRoute] string id)
    {
        var army = await _armyRepository.LoadArmy(new ObjectId(id));
        return Ok(army);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ArmyBook>> Update([FromRoute] string id, [FromBody] ArmyBook armyBook)
    {
        armyBook.Id = new ObjectId(id);
        var worked = await _armyRepository.Update(armyBook);
        return worked ? Ok(armyBook) : Conflict();
    }
    
    [HttpPost("{id}")]
    public async Task<ActionResult<ArmyBook>> NewVersion([FromRoute] string id)
    {
        var armyBook = await _armyRepository.LoadArmy(new ObjectId(id));
        var clone = armyBook.Clone();
        await _armyRepository.Create(clone);
        return Ok(clone);
    }
}