using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        //هي هنا مش هعمل انجيكشن زي الداتا بيز العاديه
        private readonly IDatabase database;
        public BasketRepository(IConnectionMultiplexer multiplexer)
        {
            database = multiplexer.GetDatabase();
        }
        public async Task<bool> DeleteCustomerAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var Result=await database.StringGetAsync(id);

            //هنا بتحولي من يوزر فاليو لكستومر فاليو
            //Deserialize =>Convert From Json To Redies 
            return Result.IsNull?null:JsonSerializer.Deserialize<CustomerBasket>(Result);
        }

        public async Task<CustomerBasket?> UpdateCustomerAsync(CustomerBasket Basket)
        {
            // الفنكشن دي بتعمل الاتنينCreate Update
            //هحول من نوع كستومر للحاجة اللي عايز اشتغل بيها
            //Deserialize =>Convert From Redies To Json Value
            var JsonSerialize =JsonSerializer.Serialize(Basket);                             //هنا معناه اني هفضل محتفظ بيها لمدة يوم واحد بس
            var CreatedOrUpdated = await database.StringSetAsync(Basket.Id, JsonSerialize, TimeSpan.FromDays(1));
            if (!CreatedOrUpdated) return null;
            return await GetBasketAsync(Basket.Id);



        }
    }
}
