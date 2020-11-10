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
        private ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> OnGet(int? id)
        {
            Book = new Book();

            if (id == null)
            {
                // create
                return Page();
            }

            // update
            Book = await _db.Book.FirstOrDefaultAsync(u=> u.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            return Page();

        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.ID == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
                return RedirectToPage();

        }
    }
}
