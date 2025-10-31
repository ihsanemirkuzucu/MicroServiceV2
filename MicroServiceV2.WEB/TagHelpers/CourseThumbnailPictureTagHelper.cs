using System.Threading.Tasks;
using MicroServiceV2.WEB.Options;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MicroServiceV2.WEB.TagHelpers
{
    public class CourseThumbnailPictureTagHelper(MicroServiceOption microServiceOption):TagHelper
    {
        public string? Src { get; set; }
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            var blankCourseThumbnailImagePath = "/images/course.jpeg";

            if(string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankCourseThumbnailImagePath);
            }
            else
            {
                var courseThumbnailImagePath = $"{microServiceOption.File.BaseAddress}/{Src}";
                output.Attributes.SetAttribute("src", courseThumbnailImagePath);
            }

            return base.ProcessAsync(context, output);
        }
    }
}
