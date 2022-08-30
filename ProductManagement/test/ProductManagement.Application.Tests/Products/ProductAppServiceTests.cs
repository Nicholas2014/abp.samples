using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace ProductManagement.Products
{
    public class ProductAppServiceTests:ProductManagementApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        public ProductAppServiceTests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
        }

        [Fact]
        public async Task Should_Get_ProductList()
        {
            // Arrange, Act
            var list = await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Assert
            list.TotalCount.ShouldBe(3);
            list.Items.ShouldContain(x=>x.Name.Contains("Acme Monochrome Laser Printer"));
        }
    }
}
