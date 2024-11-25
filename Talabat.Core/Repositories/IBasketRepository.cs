using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories
{
   public interface IBasketRepository
    {
        //فالبتالي هيعملي السيرفر واحدمن عنده Guid علشان هو  Id string هنا بخلي ال 
        public Task<CustomerBasket?>GetBasketAsync(string id);
        public Task<CustomerBasket?>UpdateCustomerAsync(CustomerBasket Basket);
        public Task<bool> DeleteCustomerAsync(string id);
    }
}
