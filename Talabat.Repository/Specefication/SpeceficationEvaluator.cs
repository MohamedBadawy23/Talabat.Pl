using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specefication;

namespace Talabat.Repository.Specefication
{
    public static class SpeceficationEvaluator<T> where T:BaseEntity
    {
        public static IQueryable<T>GetQuery(IQueryable<T> inputQuery,ISpecefication<T> Spec)
        {
            //_dbContext.Products الجزء ده اللي انا هبعته له اللي هو مثلا هيكون كده
            var Query = inputQuery;
            if(Spec.Criteria is not null)
            {
                // where(P=>P.Id==id) هنا انا هروح ابعتله اللي هيفلتر بناء عليه زي كده مثلا 
                Query = Query.Where(Spec.Criteria);
            }
            //قبل ما هرجع له الداتا هعملها سورتنج
            if(Spec.OrderBy is not null)
            {
                Query=Query.OrderBy(Spec.OrderBy);
            }
            if(Spec.OrderByDesc is not null)
            {
                Query=Query.OrderByDescending(Spec.OrderByDesc);
            }
            if (Spec.IsPaginationEnabled)
            {
                Query=Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            //يضيفهم معاي وبرده هيجمع لي كل ده مع بعضه Include  علشان لو فيه اي  Aggrigate بعدها هستخدم ال 
            //  _dbContext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync(); الكلام اللي تحت ده كله يساوي
            Query = Spec.Incudes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }

    }
}
