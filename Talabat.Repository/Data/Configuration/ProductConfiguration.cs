using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //هنا علشان اقوله ان فيه علاقه مني تو ميني مع البراند والتيب ودي الطريقة الافضل علشان علشان امثل العلاقة اللي مابين الجداول ودي هتلاقيها في الشرح بتاع الاف كور
            builder.HasOne(P => P.ProductBrand)
                 .WithMany().HasForeignKey(F => F.ProductBrandId);
            //هعملها من ناحية البرودكت او البراند او التيب هي هي مش هتفرق 
            builder.HasOne(P => P.ProductType)
              .WithMany().HasForeignKey(F => F.ProductTypeId);
            //هنا علشان اتخلص من جزءية ال Nullable
            builder.Property(P => P.Name).IsRequired();
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired();
        }
    }
}
