#region Using Statements

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Products;

#endregion

namespace ProductManagement.Web.Pages.Products
{
    public class EditProductModalModel : ProductManagementPageModel
    {
        private readonly IProductAppService _productAppService;

        public EditProductModalModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty] public CreateEditProductViewModel Product { get; set; }

        public SelectListItem[] Categories { get; set; }

        public async Task OnGetAsync()
        {
            var productDto = await _productAppService.GetAsync(Id);
            Product = ObjectMapper.Map<ProductDto, CreateEditProductViewModel>(productDto);

            var categoryLookup = await _productAppService.GetCategoriesAsync();
            Categories = categoryLookup.Items.Select(r => new SelectListItem(r.Name, r.Id.ToString())).ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var createUpdateProductDto = ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product);
            await _productAppService.UpdateAsync(Id, createUpdateProductDto);

            return NoContent();
        }
    }
}