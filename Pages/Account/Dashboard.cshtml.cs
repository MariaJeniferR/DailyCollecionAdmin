using DailyCashCollection.Data;  // Make sure this is correct
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DailyCashCollecion.Pages.Account
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
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
