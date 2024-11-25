using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    public class BaseSpecefication<T> : ISpecefication<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get  ; set ; }
        //اللي تحت ده علشان امنع التكرار فا هخليه جنب انكلود
        public List<Expression<Func<T, object>>> Incudes { get  ; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get  ; set  ; }
        public Expression<Func<T, object>> OrderByDesc { get  ; set  ; }
        public int Take { get  ; set  ; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get  ; set  ; }

        //Id هروح اعمل كونتسركتور واحد لكل البرودكت وواحد لل بيرج با 
        public BaseSpecefication()
        {
            
           //Incudes = new List<Expression<Func<T, object>>>();
        }
        //GetById ده بتاع 
        //علشان اخدها من برة انا احدد له هتفلتر علي ايه Criteria هبعتله 
        public BaseSpecefication(Expression<Func<T,bool>>CriteriaExpression)
        {
            Criteria = CriteriaExpression;
            // Incudes = List<Expression<Func<T, object>>>();
            //Incudes = new List<Expression<Func<T, object>>>();
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy=OrderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDesc=OrderByDescExpression;
        }

        public void AddPagination(int skip,int take)
        {
            IsPaginationEnabled = true;
            Take = take;
            Skip = skip;
        }

    }
}
