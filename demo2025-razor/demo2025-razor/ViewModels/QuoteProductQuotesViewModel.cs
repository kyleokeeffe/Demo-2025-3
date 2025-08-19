using Microsoft.AspNetCore.Mvc.Rendering;

namespace demo2025_razor.ViewModels
{
    public class QuoteProductQuotesViewModel
    {
        public List<SelectListItem> Quotes { get; set; } = new List<SelectListItem>();
        public string OptionId { get; set; } = string.Empty;
        public int QuoteProductId { get; set; }
        public int CustomerId { get; set; }
    }
}