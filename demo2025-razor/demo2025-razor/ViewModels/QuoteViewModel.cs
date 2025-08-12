namespace demo2025_razor.ViewModels
{
    public class QuoteViewModel
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public List<QuoteProductViewModel> QuoteProducts { get; set; }
        
    }
}
