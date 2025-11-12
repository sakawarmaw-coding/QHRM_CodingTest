using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QHRM_CodingTest.Data;
using QHRM_CodingTest.Model;

namespace QHRM_CodingTest.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository _repo;

        [BindProperty]
        public ProductModel Product { get; set; }

        public DeleteModel(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var p = await _repo.GetProductById(id);
            if (p == null) return RedirectToPage("/Index");
            Product = p;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _repo.DeleteProduct(id);
            return RedirectToPage("/Index");
        }
    }
}
