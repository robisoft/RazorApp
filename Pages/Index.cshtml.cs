using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using crossPublisher;
using crossPublisherRazor;
using Microsoft.Extensions.Options;
//using Microsoft.AspNetCore.Http;

namespace RazorApp.Pages
{
    public class IndexModel : PageModel
    {
        static private bool _isWebAppInitialized = false;

        private readonly ILogger<IndexModel> _logger;
        private AppSettings _appSettings { get; set; }
        private WebAppSession _webApp { get; set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            AppSettings appSettings,
            WebAppSession webAppSession)
        {
            _logger = logger;
            _appSettings = appSettings;
            _webApp = webAppSession;
        }

        public void OnGet()
        {
             string url = HttpContext.Request.Host.ToUriComponent();
             DataAccess data = Utils.GetDataAccess(_appSettings, url);

            if (!_isWebAppInitialized)
            {
                Console.WriteLine("IndexModel --> webApp.Init --> cercare di spostare questa init nel _Layout.cshtml, se possibile");

                _webApp.Init(data, url, _appSettings);
                // ho messo WebApp come singleton, così rimane la stessa istanza e la inizializzo una volrta sola.
                _isWebAppInitialized = true;
            }
          

            // Carico la pagina corrrente
            var currentPagePath = HttpContext.Request.Path;
            _webApp.LoadCurrPage(currentPagePath, "home", "not-found", "reserved");

            ViewData["Layout"] = _webApp.currPage.GetLayout("lay_home");

            ViewData["currPage"] = _webApp.currPage;
            ViewData["currLang"] = _webApp.currLang;
            ViewData["error"] = data.LastError;
        }
    }
}
