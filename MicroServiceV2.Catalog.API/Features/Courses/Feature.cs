namespace MicroServiceV2.Catalog.API.Features.Courses
{
    public class Feature
    {
        public int Duration { get; set; }
        public decimal Rating { get; set; }
        public string EducatorFullName { get; set; } = default!;
    }
}
