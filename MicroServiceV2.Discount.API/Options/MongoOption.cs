using System.ComponentModel.DataAnnotations;

namespace MicroServiceV2.Discount.API.Options
{
    public class MongoOption
    {
        [Required]
        public string DatabaseName { get; set; } = default!;

        [Required]
        public string ConnectionString { get; set; } = default!;
    }
}
