using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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

    [HttpGet("pdf")]
    public async Task<HttpResponseMessage> DownloadPdf([FromQuery] string armyId, [FromQuery] string version)
    {
        var army = await _armyRepository.LoadArmy(new ObjectId(armyId));
        await _texTransformer.CreateTexFile(army.ArmyName, army.GetVersion(version));
        await _texTransformer.CreatePdf(army.ArmyName, version);
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StreamContent(_texTransformer.GetPdfFile(army.ArmyName, version));
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        response.Content.Headers.ContentDisposition.FileName = GetFileName(army.ArmyName, version, "pdf");
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        return response;
    }

    private string GetFileName(string armyName, string version, string suffix)
    {
        return $"{armyName.Replace(" ", "_")}{version}.{suffix}";
    }
}