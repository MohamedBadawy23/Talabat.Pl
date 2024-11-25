using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    //علشان احمل الداتا في الكلاس ده لازم علشان اقوله انك تحمل بتوع البراند والتيب
    public class ProductWitheBrandsAndTypsSpecefication:BaseSpecefication<Product>
    {
        public ProductWitheBrandsAndTypsSpecefication(ProductBarams barams) : base(P =>
        (string.IsNullOrEmpty(barams.Search) ||P.Name .Contains(barams.Search))&&
        (!barams.BrandId.HasValue || barams.BrandId == P.ProductBrandId) &&
        (!barams.TypeId.HasValue || barams.TypeId == P.ProductTypeId))
        {
            // GetAll ده يعتبر يساوي  Prameterless Constructor 
            Incudes.Add(P => P.ProductBrand);
            Incudes.Add(P => P.ProductType);
            switch (barams.sort)
            {
                case "PriceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "PriceDesc":
                    AddOrderByDesc(P => P.Price);
                    break;
                    default:
                    AddOrderBy(P => P.Name);
                    break;

            }
            //Product=100 
            //Size=10
            //Index=5
            //skip=> 40  10*4
            //Take 10 start from 40
            //10              4                                         10
            AddPagination(barams.PageSize * (barams.PageIndex - 1), barams.PageSize);
        }
        public ProductWitheBrandsAndTypsSpecefication(int id) : base(P=>P.Id==id)//==اللي باعتها فيه Contructor اللي انا باعتهاله كنت في   Criteria  دي تساوي ال 
        {
            // GetAll ده يعتبر يساوي    
            Incudes.Add(P => P.ProductBrand);
            Incudes.Add(P => P.ProductType);


        }
       
    }
}
