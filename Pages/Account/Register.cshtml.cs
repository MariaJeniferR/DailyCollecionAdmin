//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using Org.BouncyCastle.Crypto.Generators;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DailyCashCollection.Pages.Account

//{
//    public class RegisterModel : PageModel
//    {
//        private readonly ApplicationDbContext _context;

//        // Injecting the database context to interact with the database
//        public RegisterModel(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // Binding properties for the form fields
//        [BindProperty]
//        public string Username { get; set; }

//        [BindProperty]
//        public string PhoneNo { get; set; }

//        [BindProperty]
//        public string CompanyName { get; set; }

//        [BindProperty]
//        [Required(ErrorMessage = "Password is required")]
//        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be 6 characters long.")]
//        public string Password { get; set; }

//        [BindProperty]
//        [Required(ErrorMessage = "Confirm Password is required")]
//        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be 6 characters long.")]
//        public string ConfirmPassword { get; set; }

//        [BindProperty]
//        public string ErrorMessage { get; set; }

//        [BindProperty]
//        public string SuccessMessage { get; set; }

//        // OnPost method to handle form submission
//        public async Task<IActionResult> OnPostAsync()
//        {
//            // Check if Password and ConfirmPassword match
//            if (Password != ConfirmPassword)
//            {
//                ErrorMessage = "Password and Confirm Password do not match.";
//                return Page();
//            }

//            // Validate if the phone number already exists in the database
//            var existingUser = _context.Owner.FirstOrDefault(u => u.PhoneNo == PhoneNo);
//            if (existingUser != null)
//            {
//                ErrorMessage = "This phone number is already registered.";
//                return Page();
//            }

//            // Hash the password (for security purposes, you should hash the password before storing it)
//            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

//            // Store the user data in the database
//            var newUser = new Owner
//            {
//                Username = Username,
//                PhoneNo = PhoneNo,
//                CompanyName = CompanyName,
//                Password = hashedPassword
//            };

//            _context.Owner.Add(newUser);
//            await _context.SaveChangesAsync();

//            // Success message after saving the user
//            SuccessMessage = "Registration successful! You can now login.";

//            // Redirect to the login page after successful registration
//            return RedirectToPage("/Account/Login");
//        }
//    }

//    // Owner entity representing the database table
//    public class Owner
//    {
//        public int Id { get; set; }
//        public string Username { get; set; }
//        public string PhoneNo { get; set; }
//        public string CompanyName { get; set; }
//        public string Password { get; set; }
//    }

//    // ApplicationDbContext for interacting with the database
//    public class ApplicationDbContext : DbContext
//    {
//        public DbSet<Owner> Owner { get; set; }

//        // Constructor for passing options to DbContext
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyCashCollection.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNo { get; set; }

        [BindProperty]
        [Required]
        public string CompanyName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Password must be exactly 6 digits.")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if Password and ConfirmPassword match
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Password and Confirm Password do not match.";
                return Page();
            }

            // Validate if the phone number already exists in the database
            var existingUser = _context.Owner.FirstOrDefault(u => u.PhoneNo == PhoneNo);
            if (existingUser != null)
            {
                ErrorMessage = "This phone number is already registered.";
                return Page();
            }

            // Hash the password (for security purposes, you should hash the password before storing it)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            // Store the user data in the database
            var newUser = new Owner
            {
                Username = Username,
                PhoneNo = PhoneNo,
                CompanyName = CompanyName,
                Password = hashedPassword
            };

            _context.Owner.Add(newUser);
            await _context.SaveChangesAsync();

            // Success message after saving the user
            SuccessMessage = "Registration successful! You can now login.";

            // Redirect to the login page after successful registration
            return RedirectToPage("/Account/Login");
        }
    }

    public class Owner
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhoneNo { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Owner> Owner { get; set; } // Example DbSet
    }
}
