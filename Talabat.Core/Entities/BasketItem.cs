using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
   public class BasketItem//مش هخليه يورث من البيز انتيتي علشان مش عايز احوله لجدول في الداتا بيز وانا بستخدم البيز علشان اتحكم في اللي بيتحول لجدول في الداتا بيز
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
