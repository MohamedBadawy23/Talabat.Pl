namespace Talabat.Pl.DTO
{
    public class ProductDTO
    {
        //builder.Services.AutoMapper(); بعد ما تخلص ده روح قوله كده في البروجرام 
        //builder.Services.AddAutoMapper(typeof(ProductProfile)); وكده برده
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int ProductBrandId { get; set; }
        //انا مش عايز كل البيانات اللي موجودة في ال Prodcut Brand
        //  string انا عايز بس ال
        //غلشان الفرونت مش عايز يشوف غير داتا معينة فا انا الجزء ده مسىول عن كده
        public string ProductBrand { get; set; }

        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
    }
}
