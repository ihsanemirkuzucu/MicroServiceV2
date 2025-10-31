namespace MicroServiceV2.WEB.ViewModel
{
    public record CourseViewModel(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        string Created,
        string EducatorFullName,
        string CategoryName,
        int Duration,
        decimal Rating)
    {
        public string TruncatedDescription() =>
            Description.Length > 100 ? Description[..100] + "..." : Description;
    }
}
