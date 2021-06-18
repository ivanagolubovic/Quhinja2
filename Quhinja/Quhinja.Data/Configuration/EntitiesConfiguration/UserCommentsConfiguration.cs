using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quhinja.Data.Entiities.Quhinja.Data.Entiities;

namespace Quhinja.Data.Configuration.EntitiesConfiguration
{
    public class UserCommentsConfiguration : IEntityTypeConfiguration<UserCommentsForDish>
    {
        public void Configure(EntityTypeBuilder<UserCommentsForDish> builder)
        {

            builder.Property(ing => ing.com)
                  .IsRequired(true);

        }
    }
}
