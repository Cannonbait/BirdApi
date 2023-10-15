using BirdApi.DTOs;

namespace BirdApi.Models;

public class PackageModel
{
    private readonly PackageRepository packageRepository;

    public PackageModel(PackageRepository packageRepository)
    {
        this.packageRepository = packageRepository;
    }

    public IEnumerable<PackageDTO> GetAllPackages()
    {
        return packageRepository.GetAllPackages().Select(package =>
            new PackageDTO
            {
                Id = package.Id,
                Height = package.Height,
                Width = package.Width,
                Length = package.Length,
                IsValid = true
            });
    }
}