using BirdApi.Models;

namespace BirdApi.DTOs;

public class PackageDTO
{
    public string KolliId { get; set; }
    public int Weight { get; set; }
    public double Length { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public bool IsValid { get; set; }
}