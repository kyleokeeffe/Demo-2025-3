using demo2025_razor.Interfaces;
using demo2025_razor.Models;
using demo2025_razor.Repositories;
using demo2025_razor.ViewModels;
using demo2025_razor.ViewModels.PageModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo2025_razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDemo3Repository _repo;

        public HomeController(ILogger<HomeController> logger, IDemo3Repository repo)
        {
            _logger = logger;
            _repo = repo;
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
               Products = _repo.Products,
               Customer = _repo.Customers.Where(x=>x.Id==4).FirstOrDefault()

            };
            return View("~/Views/Home/Products/Products.cshtml", viewModel);
        }

        public IActionResult Quotes()
        {
            var customer = new CustomerViewModel { Id = 4, FName = "Kyle", LName = "Smith", FullName = "Kyle Smith", Role = "Guy" };
            var viewModel = new QuotesPageViewModel
            {
                Quotes = _repo.Quotes,
                Customer = customer,
                QuoteProducts = _repo.QuoteProducts.Where(x=>x.Quote.Customer.Id==customer.Id)
            };
            return View("~/Views/Home/Quotes/Quotes.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult GetProductsTableData()
        {
            var products = _repo.Products;
            return PartialView("~/Views/Home/Products/_ProductsTable.cshtml", products);
        }

        [HttpGet]
        public IActionResult GetQuotesTableData()
        {
            var quotes = _repo.Quotes;
            return PartialView("~/Views/Home/Quotes/_QuotesTable.cshtml", quotes);
        }
        [HttpGet]
        public IActionResult GetQuoteProductsTableData()
        {
            var quoteProducts = _repo.QuoteProducts;
            return PartialView("~/Views/Home/Quotes/_QuoteProductsTable.cshtml", quoteProducts);
        }


        [HttpPost]
        public IActionResult UpdateActiveStatus(int id, bool isActive, int customerId)
        {
            try
            {
                //get person 
                //if found person,
                //   set to new status, 
                //   save context
                var product = _repo.Products.Where(x => x.Id == id).FirstOrDefault();

                if (product != null)
                {
                    product.IsActive = isActive;

                    // Return success with updated products data
                    //var updatedProducts = _repo.Products.ToList();
                    try
                    {

                    var Id = _repo.QuoteProducts.Max(x => x.Id) + 1;//TODO: kyle, need to initialize quoteProducts repo prior to quotes page load
                        var customer = _repo.Customers.Where(x => x.Id == customerId).FirstOrDefault();
                    }catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                    var newQuoteProduct = new QuoteProductViewModel
                    {
                        Id = _repo.QuoteProducts==null?1:_repo.QuoteProducts.Max(x => x.Id) + 1,
                        Customer = _repo.Customers.Where(x => x.Id == customerId).FirstOrDefault()
                    };
                    _repo.AddNewQuoteProduct(newQuoteProduct);

                    return Json(new { 
                        success = true, 
                        message = "Product has been updated."
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


        [HttpPost]
        public IActionResult AddNewQuote(string name)
        {
            try
            {
                //get person 
                //if found person,
                //   set to new status, 
                //   save context 
                _repo.AddNewQuote(new QuoteViewModel { 
                    Id = _repo.Quotes.Max(q => q.Id) + 1, 
                    Name = name });
               
                    // Return success

                    var response =  Json(new
                    {
                        success = true,
                        message = "Quote has been updated."
                        
                    });
                return response;
               // }
               // else
               //     return Json(new { success = false, message = "Product not found." });
                //context.savechanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error adding quote." });
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
