using System.ComponentModel.DataAnnotations;

namespace MicroServiceV2.Catalog.API.Options
{
    public class MongoOption
    {
        [Required]
        public string DatabaseName { get; set; } = default!;

        [Required]
        public string ConnectionString { get; set; } = default!;
    }
}
