using demo2025_razor.Models;
using demo2025_razor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo2025_razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Products()
        {
            var viewModel = new ProductsPageViewModel
            {
                Products = new List<ProductViewModel>
                {

                new ProductViewModel{Id= 1,ItemCd="A1", Description="One",IsActive=false},
                new ProductViewModel{ Id = 2, ItemCd = "A2", Description="Two",IsActive=true },
                new ProductViewModel{ Id = 3, ItemCd = "A3", Description="Three",IsActive=false }
                },
                Customer = new CustomerViewModel { Id = 4, FName = "Kyle", LName="Smith", FullName="Kyle Smith", Role = "Guy" }

            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult UpdateActiveStatus(int id, bool isActive)
        {
            try
            {
                //get person 
                //if found person,
                //   set to new status, 
                //   save context
                return Json(new { success = true, message = "Product has been updated." });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Product not found." });
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
