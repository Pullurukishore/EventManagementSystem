using EventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;  // For session handling

public class UserController : Controller
{
    private readonly EventManagementContext _db;

    // Constructor injection of the DbContext
    public UserController(EventManagementContext db)
    {
        _db = db;
    }

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(user);
    }
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(string username, string email, string password)
    {
        // Check if user tried logging in with username or email
        var user = _db.Users.FirstOrDefault(u => (u.Email == email || u.UserName == username) && u.Password == password);

        if (user != null)
        {
            // Save user information in session (use SetInt32 for UserId)
            HttpContext.Session.SetInt32("UserId", user.UserId);  // Use SetInt32 for storing integers
            HttpContext.Session.SetString("UserName", user.UserName);

            // Redirect to Event index page after login
            return RedirectToAction("Index", "Event");
        }

        // If login fails, show error message
        ViewBag.Error = "Invalid username/email or password!";
        return View();
    }


    public ActionResult Logout()
    {
        HttpContext.Session.Clear();  // Clear the session
        TempData["LogoutMessage"] = "Logout Successful";  // Set the logout message in TempData
        return RedirectToAction("Index", "Home");  // Redirect to Home page or any desired page
    }




}
