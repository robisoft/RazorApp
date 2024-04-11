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
        private readonly ILogger<IndexModel> _logger;


        private AppSettings _appSettings { get; set; }
        private WebAppSession webApp { get; set; }

        private string url;
        //private HttpContext _httpContext;

        public IndexModel(
            ILogger<IndexModel> logger,
            //HttpContext httpContext,
            AppSettings appSettings,
            WebAppSession webAppSession)
        {
            _logger = logger;

            //_httpContext = httpContext;
            _appSettings = appSettings;
            webApp = webAppSession;
        }

        public void OnGet()
        {
            url = HttpContext.Request.Host.ToUriComponent();

            DataAccess data = Utils.GetDataAccess(_appSettings, url);
            // webApp.Init(data, url, appSettings);

            // Carico la pagina corrrente
            var currentPagePath = HttpContext.Request.Path;
            webApp.LoadCurrPage(currentPagePath, "home", "not-found", "reserved");

            //ViewData["Layout"] = webApp.currPage.GetLayout("_HomeLayout");
            ViewData["Layout"] = webApp.currPage.GetLayout("lay_home");

            ViewData["currPage"] = webApp.currPage;
            ViewData["currLang"] = webApp.currLang;
            ViewData["error"] = data.LastError;

        }
    }
}
