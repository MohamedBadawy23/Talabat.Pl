using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Pl.DTO;

namespace Talabat.Pl.Helper
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            else
                return string.Empty;
        }
    }
}
