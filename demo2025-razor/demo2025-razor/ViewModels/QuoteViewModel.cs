namespace demo2025_razor.ViewModels
{
    public class QuoteViewModel
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public CustomerViewModel Customer { get; set; }
        public IQueryable<QuoteProductViewModel> QuoteProducts { get; set; }
        
    }
}
