namespace BirdApi.Models;


/**
    Weight measured in grams, dimensions measured in cm.
**/
public class PackageData
{
    public ulong KolliId { get; set; }
    public int Weight { get; set; }
    public double Length { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
}