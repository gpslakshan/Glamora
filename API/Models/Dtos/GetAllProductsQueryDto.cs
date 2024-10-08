using System.ComponentModel.DataAnnotations;

namespace API.Models.Dtos;

public class GetAllProductsQueryDto
{
    public string? SearchTerm { get; set; }
    public string? Brand { get; set; }
    public string? Type { get; set; }
    public string? Sort { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0.")]
    public int PageNumber { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0.")]
    [PageSizeValidation(ErrorMessage = "Page size must be 5, 10, 15, or 20.")]
    public int PageSize { get; set; }
}

// Custom validation attribute for PageSize
public class PageSizeValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        int[] allowedValues = { 5, 10, 15, 20 };
        if (value is int pageSize && allowedValues.Contains(pageSize))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage ?? "Invalid page size value.");
    }
}
