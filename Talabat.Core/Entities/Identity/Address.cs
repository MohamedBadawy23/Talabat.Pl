using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        //Relation 1--1 between Address And AppUser Mnadatory IN Side Of Address So The FK Will Be IN Address=>علشات انت بتاخد بتاع الوبشنال عند المانداتوري وانت عندك الاب يوزر متنداتوري
        public string AppUserId { get; set; }//=> Guid
        public AppUser AppUser { get; set; }
    }
}
