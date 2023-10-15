using BirdApi.DTOs;

namespace BirdApi.Models;

public class PackageModel
{
    private readonly PackageRepository packageRepository;

    public PackageModel(PackageRepository packageRepository)
    {
        this.packageRepository = packageRepository;
    }

    public IEnumerable<PackageData> GetAllPackages()
    {
        return packageRepository.GetAllPackages();
    }

    public bool ValidPackage(int weight, double length, double height, double width)
    {
        return weight <= 20000 && length <= 600 && height <= 600 && width <= 600;
    }

    public PackageData? GetPackage(ulong kolliId)
    {
        return packageRepository.GetAllPackages().Where(package => package.KolliId == kolliId).FirstOrDefault();
    }

    public ulong AddPackage(int weight, double length, double height, double width)
    {
        return packageRepository.AddPackage(weight, length, height, width);
    }


}