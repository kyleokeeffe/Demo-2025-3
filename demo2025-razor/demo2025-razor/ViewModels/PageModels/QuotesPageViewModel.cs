namespace demo2025_razor.ViewModels.PageModels
{
    public class QuotesPageViewModel
    {
        public IQueryable<QuoteProductViewModel> QuoteProducts { get; set; }
        public IQueryable<QuoteViewModel> Quotes{ get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
