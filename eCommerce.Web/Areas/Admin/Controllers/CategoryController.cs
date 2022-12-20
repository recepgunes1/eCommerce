using AutoMapper;
using eCommerce.Entity.ViewModels.Category;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IProductService productService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesNonDeletedAsync();
            return View(categories);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await categoryService.GetCategoryByGuidAsync(id);
            var products = await productService.GetAllProductsWithBrandAndCategoryToCategoryNonDeletedAsync(category);
            await categoryService.DeleteCategoryAsync(id);
            foreach (var product in products)
            {
                await productService.DeleteProductAsync(product.Id);
            }
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var category = await categoryService.GetCategoryByGuidAsync(id);
            var parentCategories = await categoryService.GetAllParentCategoriesNonDeletedAsync();
            parentCategories = parentCategories.Where(p => p.Id != category.Id);
            var subCategories = await categoryService.GetAllSubCategoriesToParentGuidNonDeletedAsync(id);
            var mappedCategory = mapper.Map<UpdateCategoryViewModel>(category);
            mappedCategory.Categories = mapper.Map<IEnumerable<SimpleCategoryViewModel>>(parentCategories);
            ViewData["HasSubCategory"] = subCategories.Count() == 0;
            return View(mappedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryViewModel viewModel)
        {
            await categoryService.UpdateCategoryAsync(viewModel);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        public async Task<IActionResult> Add()
        {
            var parentCategories = await categoryService.GetAllParentCategoriesNonDeletedAsync();
            var viewModel = new AddCategoryViewModel() { ParentCategories = parentCategories };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel viewModel)
        {
            await categoryService.AddCategoryAsync(viewModel);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

    }
}
