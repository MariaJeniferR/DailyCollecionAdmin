//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using MySql.Data.MySqlClient;
//using System.Security.Cryptography;
//using System.Text;

//namespace DailyCashCollection.Pages
//{
//    public class LoginModel : PageModel
//    {
//        [BindProperty]
//        public string Username { get; set; }
//        [BindProperty]
//        public string Password { get; set; }

//        private readonly IConfiguration _configuration;

//        public string ErrorMessage { get; set; } // For error message
//        public string SuccessMessage { get; set; } // For success message

//        public LoginModel(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public void OnGet()
//        {
//        }

//        public IActionResult OnPost()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            string connectionString = _configuration.GetConnectionString("DefaultConnection");
//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                connection.Open();

//                // Hash the entered password
//                string hashedPassword = HashPassword(Password);

//                // Check if the username and hashed password match in the database
//                string query = "SELECT COUNT(*) FROM Owner WHERE username = @Username AND password = @Password";
//                using (MySqlCommand command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@Username", Username);
//                    command.Parameters.AddWithValue("@Password", hashedPassword);

//                    int userCount = Convert.ToInt32(command.ExecuteScalar());

//                    if (userCount > 0)
//                    {
//                        // Success, login is valid
//                        SuccessMessage = "Login successful!";
//                        return RedirectToPage("Dashboard"); // Redirect to a Dashboard or Home page
//                    }
//                    else
//                    {
//                        // Failure, invalid username or password
//                        ErrorMessage = "Invalid username or password.";
//                        return Page();
//                    }
//                }
//            }
//        }

//        private string HashPassword(string password)
//        {
//            using (var sha256 = SHA256.Create())
//            {
//                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                StringBuilder builder = new StringBuilder();
//                foreach (var b in bytes)
//                {
//                    builder.Append(b.ToString("x2"));
//                }
//                return builder.ToString();
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using System;

namespace DailyCashCollection.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private readonly IConfiguration _configuration;

        public string ErrorMessage { get; set; } // For error message
        public string SuccessMessage { get; set; } // For success message

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT password FROM Owner WHERE username = @Username";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string storedHashedPassword = result.ToString();

                        if (BCrypt.Net.BCrypt.Verify(Password, storedHashedPassword))
                        {
                            SuccessMessage = "Login successful!";
                            return RedirectToPage("/Account/Dashboard"); // Make sure the path is correct
                        }
                        else
                        {
                            ErrorMessage = "Invalid password.";
                            return Page();
                        }
                    }
                    else
                    {
                        ErrorMessage = "Invalid username.";
                        return Page();
                    }
                    ErrorMessage = "Invalid username or password. Please try again.";
                    return Page();
                }
            }
        }


        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Query to get the hashed password for the given username
        //        string query = "SELECT password FROM Owner WHERE username = @Username";
        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Username", Username);
        //            var result = command.ExecuteScalar();

        //            if (result != null)
        //            {
        //                string storedHashedPassword = result.ToString();

        //                // Verify if the entered password matches the stored hashed password
        //                if (BCrypt.Net.BCrypt.Verify(Password, storedHashedPassword))
        //                {
        //                    // Success, login is valid
        //                    SuccessMessage = "Login successful!";
        //                    return RedirectToPage("Dashboard"); // Redirect to a Dashboard or Home page
        //                }
        //                else
        //                {
        //                    // Failure, invalid password
        //                    ErrorMessage = "Invalid password.";
        //                    return Page();
        //                }
        //            }
        //            else
        //            {
        //                // Failure, invalid username
        //                ErrorMessage = "Invalid username.";
        //                return Page();
        //            }
        //        }
        //    }
        //}
    }
}
