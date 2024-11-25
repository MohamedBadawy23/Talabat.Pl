using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
       
        //مش هعمل ميجريشن علطول علشان دي تعتبر داتا بيز وهميه
        //Radies =>هي الفاليو بتاعتي public List<BasketItem> Items وال  Key هو ال   Id عبارة عن  ديكتشنري اللي هو كي وفاليو ال 

        public string Id { get; set; }//=>GUID علشان لازم يكون مميز علشان اعرف اوصله وسط الحاجات الكتير اللي هتكون هناك


        public List<BasketItem> Items { get; set; }
        //هم داتا بيز وهمية وهتتخزن في ان ميموري اللي هي زاكرة جهازك الداخلبة
        public CustomerBasket(string id)
        {
            Id= id;
        }
    }
}
