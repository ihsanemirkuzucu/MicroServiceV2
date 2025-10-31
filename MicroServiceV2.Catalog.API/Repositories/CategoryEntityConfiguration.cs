﻿namespace MicroServiceV2.Catalog.API.Repositories
{
    public class CategoryEntityConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           builder.ToCollection("categories");
           builder.HasKey(x => x.Id);
           builder.Property(x => x.Id).ValueGeneratedNever();
           builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
           builder.Ignore(x => x.Courses);
        }
    }
}
