namespace demo2025_razor.ViewModels.PageModels
{
    public class QuotesPageViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<QuoteViewModel> Quotes{ get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
