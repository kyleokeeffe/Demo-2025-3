namespace demo2025_razor.ViewModels.PageModels
{
    public class ProductsPageViewModel
    {

        public IQueryable<ProductViewModel> Products { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
