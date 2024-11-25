using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specefication
{
    public class ProductBarams
    {
        public string? sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        private string search
            ;

        public string Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }


        //Paginatio
        private int PagSize = 5;
        public int PageSize
        {
            get { return PageSize; }
            set { PageSize = value > 10 ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;
    }
}
