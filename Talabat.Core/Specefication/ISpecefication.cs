using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    public interface ISpecefication<T> where T :BaseEntity
    {
        //_dbContext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
       
        //هنا انا مش عايز اكتب السطر اللي فوق ده لكل كلاس انا هعمل حاجة مرة واحدة تشتغل مع الكل
  
        
  //الحاجة اللي هفلتر عليها  where(P=>P.Id==id) هنا الجزء ده بيساوي    
        public Expression<Func<T, bool>> Criteria { get; set; }

        // الجزء ده بيساوي اللي تحت بحيث اخليه هو يعملي كده Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
        public List<Expression<Func<T, object>>> Incudes { get; set; }

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        //Skip &&Take 2--
        public int Take { get; set; }
        public int Skip { get; set; }
        // علشان اخلي بتاع الفرونت هو اللي يتحكم في حوار الباجينيشن لوحابب يستخدمه يخليه ترو
        public bool IsPaginationEnabled { get; set; }

       

    }

}
