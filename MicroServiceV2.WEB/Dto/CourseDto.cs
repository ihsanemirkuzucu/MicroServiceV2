namespace MicroServiceV2.WEB.Dto
{
    public record CourseDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        DateTime Created,
        CategoryDto Category,
        FeatureDto Feature);
}
