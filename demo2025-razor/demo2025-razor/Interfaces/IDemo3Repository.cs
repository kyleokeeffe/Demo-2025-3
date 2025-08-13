using demo2025_razor.ViewModels;

namespace demo2025_razor.Interfaces
{
    public interface IDemo3Repository
    {
        public IQueryable<ProductViewModel>? Products { get; set; }
        public IQueryable<QuoteViewModel>? Quotes { get; set; }
        public IQueryable<QuoteViewModel> AddNewQuote(QuoteViewModel quote);
    }
}
