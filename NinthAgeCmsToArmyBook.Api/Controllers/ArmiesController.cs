using System.Collections.Generic;
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
    public async Task<ActionResult<IEnumerable<ArmyBook>>> Get()
    {
        var armyVersionsList = await _armyRepository.LoadArmies();
        return Ok(armyVersionsList);
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
        var army = await _armyRepository.LoadArmy(new ObjectId(id));
        army.ReplaceVersion(armyBook);
        var worked = await _armyRepository.Update(army);
        return worked ? Ok(army) : Conflict();
    }
}