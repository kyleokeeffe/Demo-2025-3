using demo2025_razor.Models;
using demo2025_razor.Repositories;
using demo2025_razor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo2025_razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductsRepository productsRepository = new ProductsRepository();
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
               Products = productsRepository.Products.ToList(),
               Customer = new CustomerViewModel { Id = 4, FName = "Kyle", LName="Smith", FullName="Kyle Smith", Role = "Guy" }

            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetProductsTableData()
        {
            var products = productsRepository.Products.ToList();
            return PartialView("_ProductsTable", products);
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
                var product = productsRepository.Products.Where(x => x.Id == id).FirstOrDefault();

                if (product != null)
                {
                    product.IsActive = isActive;
                    
                    // Return success with updated products data
                    var updatedProducts = productsRepository.Products.ToList();
                    return Json(new { 
                        success = true, 
                        message = "Product has been updated.",
                        products = updatedProducts
                    });
                }
                else
                    return Json(new { success = false, message = "Product not found." });
                //context.savechanges();
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
