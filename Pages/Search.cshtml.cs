using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movie_Explorer.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public SearchModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}