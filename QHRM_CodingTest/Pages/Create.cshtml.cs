using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QHRM_CodingTest.Data;
using QHRM_CodingTest.Model;

namespace QHRM_CodingTest.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _repo;

        [BindProperty]
        public ProductModel Product { get; set; }

        public CreateModel(IProductRepository repo)
        {
            _repo = repo;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var id = await _repo.CreateProduct(Product);
            return RedirectToPage("/Index");
        }
    }
}
