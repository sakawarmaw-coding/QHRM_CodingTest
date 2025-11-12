using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QHRM_CodingTest.Data;
using QHRM_CodingTest.Model;

namespace QHRM_CodingTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _repo;
        public IEnumerable<ProductModel> Products { get; set; } = Enumerable.Empty<ProductModel>();
        public IndexModel(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task OnGetAsync()
        {
            Products = await _repo.GetAllProduct();
        }
    }
}
