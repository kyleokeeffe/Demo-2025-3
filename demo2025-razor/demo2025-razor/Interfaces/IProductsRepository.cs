using demo2025_razor.ViewModels;

namespace demo2025_razor.Interfaces
{
    public interface IProductsRepository
    {
        public IQueryable<ProductViewModel>? Products { get; set; }
    }
}
