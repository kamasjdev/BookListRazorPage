using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDBContext _db;

        public UpsertModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book BookUpsert { get; set; }

        public async Task<IActionResult> OnGet(int? id) // id moze byc null jesli zastosuje sie int?
        {
            BookUpsert = new Book();
            if(id==null)
            {
                //create
                return Page();
            }

            //update
            BookUpsert = await _db.Books.FirstOrDefaultAsync(u=>u.Id==id);
            if(BookUpsert == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(BookUpsert.Id == 0)
                {
                    _db.Books.Add(BookUpsert);
                }
                else
                {
                    _db.Books.Update(BookUpsert);
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}