using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specefication;

namespace Talabat.Core.Repositories
{
   public interface IGenericRepository<T> where T : BaseEntity
    {
        #region Without Specefication
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        #endregion
        #region With Specefication
        public Task<IReadOnlyList<T>> GetAllAsyncSpecefication(ISpecefication<T> Spec);
        public Task<T> GetByIdAsyncSpecefication(ISpecefication<T> Spec);
        public Task<int> GetCountAsync(ISpecefication<T> Spec);
        #endregion

    }
}
