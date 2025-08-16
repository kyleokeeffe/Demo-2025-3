using demo2025_razor.Interfaces;
using demo2025_razor.Models;
using demo2025_razor.Repositories;
using demo2025_razor.ViewModels;
using demo2025_razor.ViewModels.PageModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
               Products = _repo.Products??Enumerable.Empty<ProductViewModel>().AsQueryable(),
               Customer = _repo.Customers.Where(x=>x.Id==4).FirstOrDefault()

            };
            return View("~/Views/Home/Products/Products.cshtml", viewModel);
        }

        public IActionResult Quotes()
        {
            var customer = new CustomerViewModel { Id = 4, FName = "Kyle", LName = "Smith", FullName = "Kyle Smith", Role = "Guy" };
            
            
            var viewModel = new QuotesPageViewModel
            {
                Quotes = _repo.Quotes ?? Enumerable.Empty<QuoteViewModel>().AsQueryable(),
                Customer = customer,
                QuoteProducts = _repo.QuoteProducts?.Where(x=>x.Customer.Id==customer.Id)??Enumerable.Empty<QuoteProductViewModel>().AsQueryable()?? Enumerable.Empty<QuoteProductViewModel>().AsQueryable()
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
        [HttpGet]
        public IActionResult GetQuoteProductQuoteSelect(int customerId)
        {
            var quoteSelectItems = _repo.Quotes
                .Where(x=>x.Customer.Id==customerId)
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()));

            return PartialView("~/Views/Home/Quotes/_QuoteProductQuoteSelect.cshtml", quoteSelectItems);
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
                var product = _repo.Products?.Where(x => x.Id == id).FirstOrDefault();

                if (product != null)
                {
                    product.IsActive = isActive;

                    // Return success with updated products data
                    //var updatedProducts = _repo.Products.ToList();


                    var newQuoteProduct = new QuoteProductViewModel
                    {
                        Id = _repo.QuoteProducts.Any()? _repo.QuoteProducts.Max(x => x.Id) + 1:1,
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
        public IActionResult AddNewQuote(string name, int customerId)
        {
            try
            {
                //get person 
                //if found person,
                //   set to new status, 
                //   save context 
                _repo.AddNewQuote(new QuoteViewModel { 
                    Id = _repo.Quotes.Any()?_repo.Quotes.Max(q => q.Id) + 1:1, 
                    Name = name,
                Customer = _repo.Customers.Where(x=>x.Id==customerId).FirstOrDefault()});
               
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
