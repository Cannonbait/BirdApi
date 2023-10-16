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
        var kolliIdValidity = packageModel.ValidateKolliId(kolliId);

        if (kolliIdValidity == KolliIdValidity.IncorrectLength)
        {
            return BadRequest("The Kolli Id must be exactly 18 digits");
        }
        else if (kolliIdValidity == KolliIdValidity.IncorrectStart)
        {
            return BadRequest("The Kolli Id must start with 999");
        }
        else if (kolliIdValidity == KolliIdValidity.NotFound)
        {
            return NotFound("No package with that Kolli Id exists in the system");
        }

        var package = packageModel.GetPackage(kolliId);

        return Ok(new PackageDTO
        {
            KolliId = package.KolliId.ToString(),
            Height = package.Height,
            Width = package.Width,
            Length = package.Length,
            IsValid = packageModel.ValidatePackageMeasurements(package.Weight, package.Length, package.Height, package.Width)
        });
    }

    [HttpPost]
    public ActionResult<ulong> PostPackage(PackageDTO package)
    {
        if (!packageModel.ValidatePackageMeasurements(package.Weight, package.Length, package.Height, package.Width))
        {
            return BadRequest("The package does not conform to at least one of the limits imposed on packages (weight: 20 kg, width/height/length: 60 cm)");
        }

        var kolliId = packageModel.AddPackage(package.Weight, package.Length, package.Height, package.Width);

        return CreatedAtAction(nameof(GetPackage), new { kolliId }, kolliId.ToString());
    }
}
