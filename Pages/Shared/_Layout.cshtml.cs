using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using crossPublisher;
using Microsoft.AspNetCore.Components;
// ?? passo 1 inject AppSettings appSettings

namespace RazorApp.Pages
{
    public class _LayoutModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Repository> pages = new List<Repository>();
        private string lang = "it";


        public _LayoutModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            string host = NavigationManager.BaseUri; // dominio
            DataAccess data = Utils.GetDataAccess(appSettings, host);
            crossRepository repository = new crossRepository(data, 0);
            pages = repository.GetRepositoryList(0, "WebPage", false, "", "", "", null, "", false);

        }

        public void OnGet()
        {

        }
    }
}
