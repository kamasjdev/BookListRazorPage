using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> HttpGetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDB = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDB == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Books.Remove(bookFromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete succesful" });
        }
    }
}