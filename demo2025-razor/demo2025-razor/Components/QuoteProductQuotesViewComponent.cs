using demo2025_razor.Interfaces;
using demo2025_razor.Models;
using demo2025_razor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace demo2025_razor.Components
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
                .Where(q => q.Customer.Id == customerId)// && q.QuoteProducts.Where(x => x.Id == quoteProductId).Any())
                .Select(q => new SelectListItem
                {
                    Value = q.Id.ToString(),
                    Text = q.Name
                }).ToList();

            var viewModel = new QuoteProductQuotesViewModel
            {
                Quotes = quotes,
                OptionId = $"quoteProductQuotesSelect_{quoteProductId}",
                QuoteProductId = quoteProductId,
                CustomerId = customerId
            };

            return await Task.FromResult(View(viewModel));
        }
    }
}
