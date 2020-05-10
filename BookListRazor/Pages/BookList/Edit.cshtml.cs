using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDBContext _db;

        public EditModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book BookEdit { get; set; }

        public async Task OnGet(int id)
        {
            BookEdit = await _db.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var BookFromDb = await _db.Books.FindAsync(BookEdit.Id);
                BookFromDb.Name = BookEdit.Name;
                BookFromDb.Author = BookEdit.Author;
                BookFromDb.ISBN = BookEdit.ISBN;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}