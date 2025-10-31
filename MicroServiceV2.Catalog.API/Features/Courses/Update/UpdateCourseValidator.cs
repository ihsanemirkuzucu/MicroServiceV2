namespace MicroServiceV2.Catalog.API.Features.Courses.Update
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage(" CourseName is required.")
                .MaximumLength(100).WithMessage("Course Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().NotNull().WithMessage("Course description is required.")
                .MaximumLength(1000).WithMessage("Course Description must not exceed 1000 characters.");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Course price must be greater than zero.");

            RuleFor(x => x.CategoryId).NotEmpty().NotNull().WithMessage("Category ID is required.");
        }
    }
}
