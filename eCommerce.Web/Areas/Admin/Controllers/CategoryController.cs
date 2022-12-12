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
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesNonDeletedAsync();
            return View(categories);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var category = await categoryService.GetCategoryByGuidAsync(id);
            var parentCategories = await categoryService.GetParentCategoriesNonDeletedAsync();
            var mappedCategory = mapper.Map<UpdateCategoryViewModel>(category);
            mappedCategory.Categories = mapper.Map<IEnumerable<SimpleCategoryViewModel>>(parentCategories);
            return View(mappedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryViewModel viewModel)
        {
            var categories = await categoryService.GetAllCategoriesNonDeletedAsync();
            return View();
        }

        public async Task<IActionResult> Add()
        {
            var parentCategories = await categoryService.GetParentCategoriesNonDeletedAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel viewModel)
        {
            await categoryService.AddCategoryAsync(viewModel);
            return RedirectToAction("Index", "Category", new {Area = "Admin"});
        }

    }
}
