namespace MicroServiceV2.WEB.Dto;

public record CreateCourseRequest(string Name,
    string Description,
    decimal Price,
    IFormFile? Picture,
    Guid CategoryId);
