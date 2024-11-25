using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    public class ProductWithBrandsSpeceficationsToGetCount:BaseSpecefication<Product>
    {
        public ProductWithBrandsSpeceficationsToGetCount(ProductBarams barams) : base(P =>
         (string.IsNullOrEmpty(barams.Search) || P.Name .Contains(barams.Search)) &&
        (!barams.BrandId.HasValue || barams.BrandId == P.ProductBrandId) &&
        (!barams.TypeId.HasValue || barams.TypeId == P.ProductTypeId))
        {
                
        }
    }
}
