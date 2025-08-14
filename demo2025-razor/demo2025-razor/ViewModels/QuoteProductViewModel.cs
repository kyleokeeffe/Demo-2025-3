namespace demo2025_razor.ViewModels
{
    public class QuoteProductViewModel
    {
        public int Id { get; set; }
        public CustomerViewModel Customer { get; set; }
        public QuoteViewModel Quote { get; set; }
        public ProductViewModel Product { get; set; }
        public double CustomPrice { get; set; }
    }
}
