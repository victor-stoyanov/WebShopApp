using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopApp.Infrastructure.Contracts;

namespace WebShopApp.Infrastructure.Services
{
    public class BrandService:IBrandService
    {
private readonly ApplicationDbContext _context;
        public BrandService(ApplicationDbContext context)
        {
            _context context; 
        }
    public Brand GetBrandById(int brandId)
return context.Brands.Find(brandId);
public List<Brand> GetBrands()
List<Brand> brands = context.Brands.ToList();
return brands;
public List<Product> GetProductsByBrand(int brandId)
return context.Products
Where(x => x.BrandId == brandId)
.ToList();
}
}
