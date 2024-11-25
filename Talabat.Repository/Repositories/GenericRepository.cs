using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specefication;
using Talabat.Repository.Data.Context;
using Talabat.Repository.Specefication;

namespace Talabat.Repository.Repositories
{
    //علشان اقوله انه مش هشتغل غير علي اللي بيورث من البيس انتيتي BaseEntity بقوله 
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalabatDbContext _dbContext;

        public GenericRepository(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Without Specefication

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IReadOnlyList<T>)await _dbContext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
             
        }

       

        public async Task<T> GetByIdAsync(int id)
        {
            //return await _dbContext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).FirstOrDefaultAsync(P => P.Id == id);



            return await _dbContext.Set<T>().FindAsync(id);
        }
        #endregion

        #region With Specefication

       
        public async Task<T> GetByIdAsyncSpecefication(ISpecefication<T> Spec)
        {
            return await ApplySpecefication(Spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsyncSpecefication(ISpecefication<T> Spec)
        {
          return await ApplySpecefication(Spec).ToListAsync();

        }
        public async Task<int> GetCountAsync(ISpecefication<T> Spec)
        {
            return await ApplySpecefication(Spec).CountAsync();
        }
        //دي علشان اقلل الكود بس
        private IQueryable<T>ApplySpecefication(ISpecefication<T> Spec)
        {
            return SpeceficationEvaluator<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }

        
        #endregion
    }
}
