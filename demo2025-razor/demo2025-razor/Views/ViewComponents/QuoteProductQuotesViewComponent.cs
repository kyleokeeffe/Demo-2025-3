using demo2025_razor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace demo2025_razor.Views.ViewComponents
{
    public class QuoteProductQuotesViewComponent: ViewComponent
    {
        private IDemo3Repository _demo3Repository;
        public QuoteProductQuotesViewComponent(IDemo3Repository demo3Repository)
        {
            _demo3Repository = demo3Repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int quoteProductId, int customerId)
        {
            var quotes = _demo3Repository.Quotes
                .Where(q => q.Customer.Id == customerId && q.QuoteProducts.Where(x => x.Id == quoteProductId).Any())
                .Select(q => new
                {
                    q.Id,
                    q.Name
                })
                .ToList();
            return await Task.FromResult(View(quotes));
        }
    }
}
