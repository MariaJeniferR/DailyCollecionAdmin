using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DailyCashCollection.Data;

namespace DailyCashCollection.Pages
{
    public class DashModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int NumberOfUsers { get; set; }

        public async Task OnGetAsync()
        {
            NumberOfUsers = await _context.Owner.CountAsync();
        }
    }
}
