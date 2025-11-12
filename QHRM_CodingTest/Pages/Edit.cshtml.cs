using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QHRM_CodingTest.Data;
using QHRM_CodingTest.Model;

namespace QHRM_CodingTest.Pages
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository _repo;

        [BindProperty]
        public ProductModel Product { get; set; }

        public EditModel(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var p = await _repo.GetProductById(id);
            if (p is null) return RedirectToPage("/Index");
            Product = p;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _repo.UpdateProduct(Product);
            return RedirectToPage("/Index");
        }
    }
}
