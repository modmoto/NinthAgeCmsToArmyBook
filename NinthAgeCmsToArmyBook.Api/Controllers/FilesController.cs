using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NinthAgeCmsToArmyBook.Api.Latex;
using NinthAgeCmsToArmyBook.Api.MongoDb;

namespace NinthAgeCmsToArmyBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly ArmyRepository _armyRepository;
    private readonly TexTransformer _texTransformer;

    public FilesController(ArmyRepository armyRepository, TexTransformer texTransformer)
    {
        _armyRepository = armyRepository;
        _texTransformer = texTransformer;
    }

    [HttpGet("download-pdf")]
    public async Task<ActionResult> DownloadPdf([FromQuery] string armyName, [FromQuery] string version)
    {
        var pdfFile = await _texTransformer.GetPdfFile(armyName, version);
        return File(pdfFile, "application/pdf", $"{armyName} {version}.{"pdf"}");
    }
    
    [HttpPut("build-pdf")]
    public async Task<ActionResult> BuildPdf([FromQuery] string armyId, [FromQuery] string version)
    {
        var army = await _armyRepository.LoadArmy(new ObjectId(armyId));
        await _texTransformer.CreateTexFile(army.ArmyName, army.GetVersion(version));
        await _texTransformer.CreatePdf(army.ArmyName, version);
        return Ok();
    }
}