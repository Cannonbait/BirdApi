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
    public IEnumerable<PackageDTO> Get()
    {
        return packageModel.GetAllPackages();
    }
}
