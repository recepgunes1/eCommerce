using AutoMapper;
using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Entity.ViewModels.Product;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IBrandService brandService, ICategoryService categoryService, IProductService productService, IMapper mapper)
        {
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetAllProductsWithBrandAndCategoryNonDeletedAsync();
            return Json(products.Select(p => new { p.Id, p.Name, p.Price, p.Quantity, brand = p.Brand.Name, category = p.Category.Name, p.CreatedDate, image = p.Images.FirstOrDefault()?.NameWithPath }));;

        }

        public async Task<IActionResult> Add()
        {
            var brands = await brandService.GetAllBrandsNonDeletedAsync();
            var categories = await categoryService.GetAllSubCategoriesNonDeletedAsync();
            var viewModel = new AddProductViewModel();
            viewModel.Brands = mapper.Map<IEnumerable<SimpleBrandViewModel>>(brands);
            viewModel.Categories = mapper.Map<IEnumerable<SimpleCategoryViewModel>>(categories);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel viewModel)
        {
            await productService.AddProductAsync(viewModel);
            return RedirectToAction("Index", "Product", new { Area = "Admin" });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await productService.DeleteProductAsync(id);
            return RedirectToAction("Index", "Product", new { Area = "Admin" });
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var product = await productService.GetProductByGuidAsync(id);
            var brands = await brandService.GetAllBrandsNonDeletedAsync();
            var categories = await categoryService.GetAllSubCategoriesNonDeletedAsync();
            var mappedProduct = mapper.Map<UpdateProductViewModel>(product);
            mappedProduct.Brands = mapper.Map<IEnumerable<SimpleBrandViewModel>>(brands);
            mappedProduct.Categories = mapper.Map<IEnumerable<SimpleCategoryViewModel>>(categories);
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel viewModel)
        {
            await productService.UpdateProductAsync(viewModel);
            return RedirectToAction("Index", "Product", new { Area = "Admin" });
        }
    }
}
