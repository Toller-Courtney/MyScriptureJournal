using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Models.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Models.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string scriptureBooks { get; set; }
     
        public string BookSort { get; set; }
        public string DateSort { get; set; }

        public string CurrentSort { get; set; }
        public string SortOrder { get; set; }

     
        public async Task OnGetAsync(string SearchString, string SortOrder)
        {
            //filtering scriptures by book name and note keywords
            IQueryable<string> bookQuery = from sc in _context.Scripture
                                           orderby sc.Book
                                           select sc.Book;

            var scriptures = from sc in _context.Scripture
                             select sc;
            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(y => y.Notes.Contains(SearchString));
            }
            if(!string.IsNullOrEmpty(scriptureBooks))
            {
                scriptures = scriptures.Where(x => x.Book == scriptureBooks);
            }
            //Sorting my scriptures by date and book
            BookSort = String.IsNullOrEmpty(SortOrder) ? "book_desc" : "";
            DateSort = SortOrder == "Date" ? "date_desc" : "Date";
            switch (SortOrder)
                {
                    case "book_desc":
                        scriptures = scriptures.OrderByDescending(sc =>sc.Book);
                        break;
                    case "Date":
                        scriptures = scriptures.OrderBy(sc => sc.DateAdded);
                        break;
                    case "date_desc":
                        scriptures = scriptures.OrderByDescending(sc => sc.DateAdded);
                        break;
                    default:
                        scriptures = scriptures.OrderBy(sc=> sc.Book);
                        break;
                }
            
            

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
           
            Scripture = await scriptures.AsNoTracking().ToListAsync();
          
        }
    }
}
