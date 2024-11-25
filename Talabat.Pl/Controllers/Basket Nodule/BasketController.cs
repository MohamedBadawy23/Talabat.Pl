using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Pl.Errors;
using Talabat.Repository.Repositories;

namespace Talabat.Pl.Controllers.Basket_Nodule
{
    
    public class BasketController :BaseController
    {
        private readonly BasketRepository _basket;

        public BasketController(BasketRepository basket)
        {
            _basket = basket;
        }
        [HttpGet("{id}")]//هنا لازم يكون نفس الاسم اللي عندك في الكونترولر تحت
        public async Task<ActionResult<CustomerBasket>>GetBasket(string id)
        {
            var Basket=await _basket.GetBasketAsync(id);
            // علشان الخطوة دي عملت كونستراكتوري 
            return Basket is null ? new CustomerBasket(id) : Basket;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>UpdateCreateBasket(CustomerBasket basket)
        {
            var CreatedUpdated = await _basket.UpdateCustomerAsync(basket);
            if (CreatedUpdated is null) return BadRequest(new ApiErrorsHandling(400));
            return Ok(CreatedUpdated);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteBasket(string id)
        {
            return await _basket.DeleteCustomerAsync(id);
        }
          
    }
}
