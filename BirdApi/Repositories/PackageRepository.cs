using System.Collections;

namespace BirdApi.Models;

public class PackageRepository
{
    private List<PackageData> packages = new List<PackageData>();
    private ulong nextKolliId = 999000000000000000;

    public PackageRepository()
    {
        packages.Add(new PackageData { KolliId = GenerateNewKolliId(), Weight = 10, Height = 10, Length = 10, Width = 10 });
        packages.Add(new PackageData { KolliId = GenerateNewKolliId(), Weight = 10, Height = 65, Length = 10, Width = 10 });
    }

    public IEnumerable<PackageData> GetAllPackages()
    {
        return packages;
    }

    public ulong AddPackage(int weight, double length, double height, double width)
    {
        var kolliId = GenerateNewKolliId();
        packages.Add(new PackageData { KolliId = kolliId, Height = height, Length = length, Weight = weight, Width = width });
        return kolliId;
    }

    private ulong GenerateNewKolliId()
    {
        var kolliId = nextKolliId;
        nextKolliId++;
        return kolliId;
    }
}