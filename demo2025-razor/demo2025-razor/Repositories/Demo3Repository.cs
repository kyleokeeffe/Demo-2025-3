using demo2025_razor.Interfaces;
using demo2025_razor.ViewModels;
using System.Text.Json;

namespace demo2025_razor.Repositories
{
    public class Demo3Repository : IDemo3Repository
    {
        private static List<ProductViewModel>? staticProducts;
        private static List<QuoteViewModel>? staticQuotes;
        private static List<QuoteProductViewModel>? staticQuoteProducts;
        private static List<CustomerViewModel>? staticCustomers;
        private IQueryable<ProductViewModel>? products;
        private IQueryable<QuoteViewModel>? quotes;
        private IQueryable<QuoteProductViewModel>? quoteProducts;
        private IQueryable<CustomerViewModel>? customers;
        public IQueryable<ProductViewModel>? Products
        {
            get
            {
                if (products == null)
                {
                    ProductsInit();
                }
                return products;
            }
            set
            {
                products = value;
            }
        }
        public IQueryable<QuoteViewModel>? Quotes
        {
            get
            {
                if (quotes == null)
                {
                    QuotesInit();
                }
                return quotes;
            }
            set
            {
                quotes = value;
            }
        }
        public IQueryable<QuoteProductViewModel>? QuoteProducts
        {
            get
            {
                if (quoteProducts == null)
                {
                    QuoteProductsInit();
                }
                return quoteProducts;
            }
            set
            {
                quoteProducts = value;
            }
        }
        public IQueryable<CustomerViewModel>? Customers
        {
            get
            {
                if (customers == null)
                {
                    CustomersInit();
                }
                return customers;
            }
            set
            {
                customers = value;
            }
        }
        private void ProductsInit()
        {
            if (staticProducts == null)
            {
                if (File.Exists("products.json"))
                {
                    try
                    {
                        staticProducts = GetProductsLocal();
                    }
                    catch (Exception ex)
                    {
                        staticProducts = GetInitialProducts();
                    }
                }
                else
                    staticProducts = GetInitialProducts();
            }
            this.Products = staticProducts.AsQueryable();
        }
        private void QuotesInit()
        {
            if (staticQuotes == null)
            {
                if (File.Exists("quotes.json"))
                {
                    try
                    {
                        staticQuotes = GetQuotesLocal();
                    }
                    catch (Exception ex)
                    {
                        staticQuotes = GetInitialQuotes();
                    }
                }
                else
                    staticQuotes = GetInitialQuotes();
            }
            this.Quotes = staticQuotes.AsQueryable();
        }
        private void QuoteProductsInit()
        {
            if (staticQuoteProducts == null)
            {
                if (File.Exists("quoteProducts.json"))
                {
                    try
                    {
                        staticQuoteProducts = GetQuoteProductsLocal();
                    }
                    catch (Exception ex)
                    {
                        staticQuoteProducts = GetInitialQuoteProducts();
                    }
                }
                else
                    staticQuoteProducts = GetInitialQuoteProducts();
            }
            this.QuoteProducts = staticQuoteProducts.AsQueryable();
        }
        private void CustomersInit()
        {
            if (staticCustomers == null)
            {
                if (File.Exists("customers.json"))
                {
                    try
                    {
                        staticCustomers = GetCustomersLocal();
                    }
                    catch (Exception ex)
                    {
                        staticCustomers = GetInitialCustomers();
                    }
                }
                else
                    staticCustomers = GetInitialCustomers();
            }
            this.Customers = staticCustomers.AsQueryable();
        }
        private List<ProductViewModel> GetInitialProducts()
        {
            return new List<ProductViewModel>
            {
                new ProductViewModel{Id= 1,ItemCd="A1", Description="One",IsActive=false},
                new ProductViewModel{ Id = 2, ItemCd = "A2", Description="Two",IsActive=false },
                new ProductViewModel{ Id = 3, ItemCd = "A3", Description="Three",IsActive=false }
            };
        }
        private List<QuoteViewModel> GetInitialQuotes()
        {
            return new List<QuoteViewModel>();
            //{
            //    new QuoteViewModel{Id = 1 ,ItemCd="A1", Description="One",IsActive=false},
            //    new QuoteViewModel{ Id = 2, ItemCd = "A2", Description="Two",IsActive=true },
            //    new QuoteViewModel{ Id = 3, ItemCd = "A3", Description="Three",IsActive=false }
            //};
        }
        private List<QuoteProductViewModel> GetInitialQuoteProducts()
        {
            return new List<QuoteProductViewModel>();
            //{
            //    new QuoteViewModel{Id = 1 ,ItemCd="A1", Description="One",IsActive=false},
            //    new QuoteViewModel{ Id = 2, ItemCd = "A2", Description="Two",IsActive=true },
            //    new QuoteViewModel{ Id = 3, ItemCd = "A3", Description="Three",IsActive=false }
            //};
        }
        private List<CustomerViewModel>? GetInitialCustomers()
        {
            return new List<CustomerViewModel>
            {
               new CustomerViewModel { Id = 4, FName = "Kyle", LName = "Smith", FullName = "Kyle Smith", Role = "Guy" }

            };
        }
        public void AddNewQuote(QuoteViewModel quote)
        {

            staticQuotes.Add(quote);
            UpdateQuotesLocal();
            // return staticQuotes.AsQueryable();

        }
        public void AddNewQuoteProduct(QuoteProductViewModel quoteProduct)
        {

            staticQuoteProducts.Add(quoteProduct);
            UpdateQuoteProductsLocal();
            // return staticQuotes.AsQueryable();

        }

        public List<ProductViewModel>? GetProductsLocal()
        {
            var productList = File.ReadAllText("products.json");
            return JsonSerializer.Deserialize<List<ProductViewModel>>(productList);
        }
        public List<QuoteViewModel> GetQuotesLocal()
        {
            try
            {

                var quoteList = File.ReadAllText("quotes.json");
                return JsonSerializer.Deserialize<List<QuoteViewModel>>(quoteList);
            }
            catch (Exception ex)
            {
                return new List<QuoteViewModel>();
                throw new Exception("Error reading quotes from local file.", ex);

            }
        }
        public List<QuoteProductViewModel> GetQuoteProductsLocal()
        {
            try
            {
                var quoteProductList = File.ReadAllText("quoteProducts.json");
                return JsonSerializer.Deserialize<List<QuoteProductViewModel>>(quoteProductList);
            }
            catch (Exception ex)
            {
                return new List<QuoteProductViewModel>();
                throw new Exception("Error reading quote products from local file.", ex);
            }
        }
        public List<CustomerViewModel>? GetCustomersLocal()
        {
            try
            {
                var customerList = File.ReadAllText("customers.json");
                return JsonSerializer.Deserialize<List<CustomerViewModel>>(customerList);
            }
            catch (Exception ex)
            {
                return new List<CustomerViewModel>();
                throw new Exception("Error reading customers from local file.", ex);
            }
        }
        public void UpdateProductsLocal()
        {
            var productLIst = JsonSerializer.Serialize(staticProducts);

            if (File.Exists("products.json"))
                File.Delete("products.json");

            File.WriteAllText(productLIst, "products.json");
        }
        public void UpdateQuotesLocal()
        {
            var quoteList = JsonSerializer.Serialize(staticQuotes);
            if (File.Exists("quotes.json"))
                File.Delete("quotes.json");
            File.WriteAllText(quoteList, "quotes.json");
        }
        public void UpdateQuoteProductsLocal()
        {
            var quoteProductList = JsonSerializer.Serialize(staticQuoteProducts);
            if (File.Exists("quoteProducts.json"))
                File.Delete("quoteProducts.json");
            File.WriteAllText(quoteProductList, "quoteProducts.json");
        }
        public void UpdateCustomersLocal()
        {
            var customerList = JsonSerializer.Serialize(staticCustomers);
            if (File.Exists("customers.json"))
                File.Delete("customers.json");
            File.WriteAllText(customerList, "customers.json");
        }
    }
}
