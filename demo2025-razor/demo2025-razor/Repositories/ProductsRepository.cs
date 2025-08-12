using demo2025_razor.ViewModels;

namespace demo2025_razor.Repositories
{
    public class ProductsRepository
    {
        private static List<ProductViewModel>? _staticProducts;
        private IQueryable<ProductViewModel>? _products;
        
        public IQueryable<ProductViewModel>? Products
        { 
            get 
            {
                if (_products == null)
                {
                    ProductsInit();
                }
                return _products;
            }
            set
            {
                _products = value;
            }
        }

        private void ProductsInit()
        {
            if (_staticProducts == null)
            {
                _staticProducts = GetInitialProducts();
            }
            this.Products = _staticProducts.AsQueryable();
        }

        private List<ProductViewModel> GetInitialProducts()
        {
            return new List<ProductViewModel>
            {
                new ProductViewModel{Id= 1,ItemCd="A1", Description="One",IsActive=false},
                new ProductViewModel{ Id = 2, ItemCd = "A2", Description="Two",IsActive=true },
                new ProductViewModel{ Id = 3, ItemCd = "A3", Description="Three",IsActive=false }
            };
        }
    }
}
