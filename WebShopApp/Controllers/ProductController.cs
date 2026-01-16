using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebShopApp.Infrastructure.Contracts;
using WebShopApp.Infrastructure_.Data.Entities;
using WebShopApp.Models.Brand;
using WebShopApp.Models.Category;
using WebShopApp.Models.Product;

namespace WebShopApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        // GET: Product/Index
        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            var products = _productService
                .GetProducts(searchStringCategoryName, searchStringBrandName)
                .Select(product => new ProductIndexVM
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.CategoryName,
                    Picture = product.Picture,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Discount = product.Discount
                })
                .ToList();

            return View(products);
        }


        // GET: Product/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            var model = new ProductCreateVM
            {
                Brands = _brandService.GetBrands()
                    .Select(b => new BrandPairVM
                    {
                        Id = b.Id,
                        Name = b.BrandName
                    })
                    .ToList(),

                Categories = _categoryService.GetCategories()
                    .Select(c => new CategoryPairVM
                    {
                        Id = c.Id,
                        Name = c.CategoryName
                    })
                    .ToList()
            };

            return View(model);
        }


        // POST: Product/Create
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateVM product)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns
                product.Brands = _brandService.GetBrands()
                    .Select(b => new BrandPairVM
                    {
                        Id = b.Id,
                        Name = b.BrandName
                    })
                    .ToList();

                product.Categories = _categoryService.GetCategories()
                    .Select(c => new CategoryPairVM
                    {
                        Id = c.Id,
                        Name = c.CategoryName
                    })
                    .ToList();

                return View(product);
            }

            var created = _productService.Create(
                product.ProductName,
                product.BrandId,
                product.CategoryId,
                product.Picture,
                product.Quantity,
                product.Price,
                product.Discount
            );

            if (!created)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductEditVM
            {
                Id = product.Id,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount,

                Brands = _brandService.GetBrands()
                    .Select(b => new BrandPairVM
                    {
                        Id = b.Id,
                        Name = b.BrandName
                    })
                    .ToList(),

                Categories = _categoryService.GetCategories()
                    .Select(c => new CategoryPairVM
                    {
                        Id = c.Id,
                        Name = c.CategoryName
                    })
                    .ToList()
            };

            return View(model);
        }


        // POST: Product/Edit/5
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns
                product.Brands = _brandService.GetBrands()
                    .Select(b => new BrandPairVM
                    {
                        Id = b.Id,
                        Name = b.BrandName
                    })
                    .ToList();

                product.Categories = _categoryService.GetCategories()
                    .Select(c => new CategoryPairVM
                    {
                        Id = c.Id,
                        Name = c.CategoryName
                    })
                    .ToList();

                return View(product);
            }

            var updated = _productService.Update(
                id,
                product.ProductName,
                product.BrandId,
                product.CategoryId,
                product.Picture,
                product.Quantity,
                product.Price,
                product.Discount
            );

            if (!updated)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }

        // GET: ProductController/Delete/5
        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }
        // POST: ProductController/Delete/5
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}