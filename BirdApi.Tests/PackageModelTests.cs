using NUnit.Framework;
using BirdApi.Models;

namespace BirdApi.Tests;

public class PackageTests
{
    [Test]
    public void ValidatePackageMeasurements()
    {
        var packageModel = new PackageModel(null);

        // Maximum weight is 20000
        Assert.That(packageModel.ValidatePackageMeasurements(25000, 10, 10, 10) == false);

        // Maximum length is 60
        Assert.That(packageModel.ValidatePackageMeasurements(20000, 61, 10, 10) == false);
        // Maximum height is 60
        Assert.That(packageModel.ValidatePackageMeasurements(20000, 10, 61, 10) == false);
        // Maximum width is 60
        Assert.That(packageModel.ValidatePackageMeasurements(20000, 10, 10, 61) == false);

        // Maximum size & weight of package that should pass
        Assert.That(packageModel.ValidatePackageMeasurements(20000, 60, 60, 60));

        // 0 weight/size packages are not acceptable
        Assert.That(packageModel.ValidatePackageMeasurements(0, 60, 60, 60) == false);
        Assert.That(packageModel.ValidatePackageMeasurements(20000, 0, 60, 60) == false);
        Assert.That(packageModel.ValidatePackageMeasurements(20000, 60, 0, 60) == false);
        Assert.That(packageModel.ValidatePackageMeasurements(20000, 60, 60, 0) == false);

        // Smallest allowed package
        Assert.That(packageModel.ValidatePackageMeasurements(1, 1, 1, 1));
    }

    [Test]
    public void ValidateKolliId()
    {
        //The PackageRepository should be handled with mocking, but trying to keep scope limited
        var packageModel = new PackageModel(new PackageRepository()); 

        // Incorrect length of Kolli Id
        Assert.That(packageModel.ValidateKolliId(9998887776665554443) == KolliIdValidity.IncorrectLength);
        Assert.That(packageModel.ValidateKolliId(99988877766655544) == KolliIdValidity.IncorrectLength);

        // Kolli Id not starting with 999
        Assert.That(packageModel.ValidateKolliId(899888777666555444) == KolliIdValidity.IncorrectStart);
        Assert.That(packageModel.ValidateKolliId(989888777666555444) == KolliIdValidity.IncorrectStart);
        Assert.That(packageModel.ValidateKolliId(998888777666555444) == KolliIdValidity.IncorrectStart);

        // Kolli Id that does not exist
        Assert.That(packageModel.ValidateKolliId(999888777666555444) == KolliIdValidity.NotFound);

        // Kolli Id that exists
        Assert.That(packageModel.ValidateKolliId(999000000000000000) == KolliIdValidity.Ok);
    }
}