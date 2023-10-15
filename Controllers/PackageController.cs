using BirdApi.DTOs;
using BirdApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private readonly ILogger<PackageController> _logger;
    private readonly PackageModel packageModel;

    public PackageController(ILogger<PackageController> logger, PackageModel packageModel)
    {
        _logger = logger;
        this.packageModel = packageModel;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return packageModel.GetAllPackages().Select(package =>
            package.KolliId.ToString());
    }

    [HttpGet("{kolliId}")]
    public ActionResult<PackageDTO> GetPackage(ulong kolliId)
    {
        var strKolliId = kolliId.ToString();

        if (strKolliId.Length != 18)
        {
            return BadRequest("The Kolli Id must be exactly 18 digits");
        }
        else if (!strKolliId.StartsWith("999"))
        {
            return BadRequest("The Kolli Id must start with 999");
        }

        var package = packageModel.GetPackage(kolliId);
        if (package == null)
        {
            return NotFound("No package with that Kolli Id exists in the system");
        }

        return Ok(new PackageDTO
        {
            KolliId = package.KolliId.ToString(),
            Height = package.Height,
            Width = package.Width,
            Length = package.Length,
            IsValid = packageModel.ValidPackage(package.Weight, package.Length, package.Height, package.Width)
        });
    }

    [HttpPost]
    public ActionResult<ulong> PostPackage(PackageDTO package)
    {
        if (!packageModel.ValidPackage(package.Weight, package.Length, package.Height, package.Width))
        {
            return BadRequest("The package does not conform to one of the limits imposed on packages");
        }

        var kolliId = packageModel.AddPackage(package.Weight, package.Length, package.Height, package.Width);

        return CreatedAtAction(nameof(GetPackage), new { kolliId }, kolliId.ToString());
    }
}
