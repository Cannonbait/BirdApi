using System.Collections;

namespace BirdApi.Models;

public class PackageRepository
{
    private List<PackageData> packages = new List<PackageData>();

    public PackageRepository()
    {
        packages.Add(new PackageData { Id = 999456789012345678, Height = 10, Length = 10, Weight = 10, Width = 10 });
    }

    public IEnumerable<PackageData> GetAllPackages()
    {
        return packages;
    }
}