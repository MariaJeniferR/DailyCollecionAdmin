using DailyCashCollection.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyCashCollecion.Pages
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Properties to bind to the Razor Page
        public IList<Owner> Users { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TodayDemand { get; set; }
        public decimal TodaysCollection { get; set; }
        public decimal YesterdaysCollection { get; set; }
        public decimal AsOnDateDues { get; set; }
        public decimal TotalPendingAmount { get; set; }

        // OnGetAsync method is executed when the page is loaded
        public async Task OnGetAsync()
        {
            Users = await _context.Owner.ToListAsync();
            TotalCustomers = await _context.Owner.CountAsync();
            TodayDemand = 5000;
            TodaysCollection = 4000;
            YesterdaysCollection = 3500;
            AsOnDateDues = 1000;
            TotalPendingAmount = 1500;
        }

        // OnPostDeleteAsync handles the delete action
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _context.Owner.FindAsync(id);
            if (user != null)
            {
                _context.Owner.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(); // Redirect back to the current page after deletion
        }
    }
}




//using DailyCashCollection.Models; // Ensure this is the correct namespace for the Owner model
//using DailyCashCollection.Data;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace DailyCashCollecion.Pages
//{
//    public class UsersModel : PageModel
//    {
//        private readonly ApplicationDbContext _context;

//        public UsersModel(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // Properties to bind to the Razor Page
//        public IList<Owner> Users { get; set; } // List of users to be displayed
//        public int TotalCustomers { get; set; } // Total number of customers
//        public decimal TodayDemand { get; set; } // Total demand for today
//        public decimal TodaysCollection { get; set; } // Today's collection
//        public decimal YesterdaysCollection { get; set; } // Yesterday's collection
//        public decimal AsOnDateDues { get; set; } // Dues as of today
//        public decimal TotalPendingAmount { get; set; } // Total pending amount for users

//        // OnGetAsync method is executed when the page is loaded
//        public async Task OnGetAsync()
//        {
//            // Retrieve users asynchronously from the database
//            Users = await _context.Owner.ToListAsync();

//            // Metrics can be calculated based on your business logic
//            TotalCustomers = await _context.Owner.CountAsync(); // Example: Count of total customers

//            // Replace these values with your actual business logic for the metrics
//            TodayDemand = 5000; // Example: replace with actual demand for today
//            TodaysCollection = 4000; // Example: replace with today's collection
//            YesterdaysCollection = 3500; // Example: replace with yesterday's collection
//            AsOnDateDues = 1000; // Example: replace with dues as of today
//            TotalPendingAmount = 1500; // Example: replace with pending amount
//        }

//    }
//}
