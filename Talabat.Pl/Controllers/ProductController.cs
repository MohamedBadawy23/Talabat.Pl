using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specefication;
using Talabat.Pl.DTO;
using Talabat.Pl.Errors;
using Talabat.Pl.Helper;

namespace Talabat.Pl.Controllers
{
    //بعد ما تخلص روح ضيف سيرفيس في البروجرام
    
    public class ProductController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _brands;
        private readonly IGenericRepository<ProductType> _types;

        //لو عايز حاجة من نوع االبراندز او التيبس انت هتبعته لانترفيس الجينيريك 
        public ProductController(IGenericRepository<Product> ProductRepo,IMapper mapper,IGenericRepository<ProductBrand>Brands,IGenericRepository<ProductType>Types)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _brands = Brands;
            _types = Types;
        }

        //حاليا انا هستخدم لود القديم وهيكون في الجينيريك
      
        #region GetAll With Spec
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetAllProducts([FromQuery]ProductBarams barams)
        {
            var Spec = new ProductWitheBrandsAndTypsSpecefication(barams);
            var Result = await _productRepo.GetAllAsyncSpecefication(Spec);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(Result);
            var Counted=new ProductWithBrandsSpeceficationsToGetCount(barams);
            var Count =await _productRepo.GetCountAsync(Counted);
          
            return Ok(new Pagination<ProductDTO>(barams.PageSize, barams.PageIndex,Count, Data));
        }
        #endregion
        #region GetById With Spec
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var Spec = new ProductWitheBrandsAndTypsSpecefication(id);
            var Result = await _productRepo.GetByIdAsyncSpecefication(Spec);
            if (Result is null) return NotFound(new ApiErrorsHandling(404));
            var MapperProduct=_mapper.Map<Product, ProductDTO>(Result);
            return Ok(MapperProduct);
        }
        #endregion

        #region GetAll Without Spec
        //[HttpGet]
        //public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts()
        //{
        //    var Products = await _productRepo.GetAllAsync();
        //    return Ok(Products);
        //}
        #endregion
        #region GetById Without Spec
        //[HttpGet("{id}")]
        //public async Task<ActionResult>GetProductById(int id)
        //{
        //    var Product = await _productRepo.GetByIdAsync(id);
        //    return Ok(Product);
        //}
        #endregion
        #region Get All Brands&&Types
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _brands.GetAllAsync();
            return Ok(Brands);

            //هنا انت كده كده شغال علي نوع كلاس واحد فا مش محتاج تحمل الداتا سبيسفيكاشن
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Brands = await _types.GetAllAsync();
            return Ok(Brands);

            //هنا انت كده كده شغال علي نوع كلاس واحد فا مش محتاج تحمل الداتا سبيسفيكاشن
        }
        #endregion





    }
}
