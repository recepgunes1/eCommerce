using AutoMapper;
using eCommerce.Entity.ViewModels.Brand;
using eCommerce.Service.Helpers.Images;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;
        private readonly IProductService productService;
        private readonly IImageHelper imageHelper;
        private readonly IMapper mapper;

        public BrandController(IBrandService brandService, IProductService productService, IImageHelper imageHelper, IMapper mapper)
        {
            this.brandService = brandService;
            this.productService = productService;
            this.imageHelper = imageHelper;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetBrands()
        {
            var brands = await brandService.GetAllBrandsNonDeletedAsync();
            return Json(brands.Select(p => new { p.Id, p.Name, p.CreatedDate, p.Image.NameWithPath }));
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await brandService.AddBrandAsync(viewModel);
                return RedirectToAction("Index", "Brand", new { Area = "Admin" });
            }
            return View();
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            var brand = await brandService.GetBrandByGuidAsync(Id);
            var mappedBrand = mapper.Map<UpdateBrandViewModel>(brand);
            return View(mappedBrand);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await brandService.UpdateBrandAsync(viewModel);
            }
            return RedirectToAction("Index", "Brand", new { Area = "Admin" });
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var brand = await brandService.GetBrandByGuidAsync(Id);
            await brandService.DeleteBrandAsync(Id);
            var products = await productService.GetAllProductsWithBrandAndCategoryToBrandNonDeletedAsync(brand);
            foreach (var product in products)
            {
                await productService.DeleteProductAsync(product.Id);
            }
            return RedirectToAction("Index");
        }

    }
}
