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


    public PackageData? GetPackage(ulong kolliId)
    {
        return packageRepository.GetAllPackages().Where(package => package.KolliId == kolliId).FirstOrDefault();
    }

    public ulong AddPackage(int weight, double length, double height, double width)
    {
        return packageRepository.AddPackage(weight, length, height, width);
    }

    public bool ValidatePackageMeasurements(int weight, double length, double height, double width)
    {
        return 0 < weight && weight <= 20000 &&
        0 < length && length <= 60 &&
        0 < height && height <= 60 &&
        0 < width && width <= 60;
    }

    public KolliIdValidity ValidateKolliId(ulong kolliId)
    {
        var strKolliId = kolliId.ToString();

        if (strKolliId.Length != 18)
        {
            return KolliIdValidity.IncorrectLength;
        }
        else if (!strKolliId.StartsWith("999"))
        {
            return KolliIdValidity.IncorrectStart;
        }
        else if (GetPackage(kolliId) == null)
        {
            return KolliIdValidity.NotFound;
        }
        return KolliIdValidity.Ok;
    }


}