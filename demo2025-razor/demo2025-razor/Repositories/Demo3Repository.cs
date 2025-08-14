using demo2025_razor.Interfaces;
using demo2025_razor.ViewModels;

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
                staticProducts = GetInitialProducts();
            }
            this.Products = staticProducts.AsQueryable();
        }
        private void QuotesInit()
        {
            if (staticQuotes == null)
            {
                staticQuotes = GetInitialQuotes();
            }
            this.Quotes = staticQuotes.AsQueryable();
        }
        private void QuoteProductsInit()
        {
            if (staticQuoteProducts == null)
            {
                staticQuoteProducts = GetInitialQuoteProducts();
            }
            this.QuoteProducts = staticQuoteProducts.AsQueryable();
        }
        private void CustomersInit()
        {
            
            if (staticCustomers == null)
            {
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
           // return staticQuotes.AsQueryable();
           
        }
        public void AddNewQuoteProduct(QuoteProductViewModel quoteProduct)
        {

            staticQuoteProducts.Add(quoteProduct);
            // return staticQuotes.AsQueryable();

        }
    }
}
