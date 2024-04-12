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
        private WebAppSession _webApp { get; set; }


        public IndexModel(
            ILogger<IndexModel> logger,
            AppSettings appSettings,
            WebAppSession webAppSession)
        {
            _logger = logger;

            //_httpContext = httpContext;
            _appSettings = appSettings;
            _webApp = webAppSession;
        }

        public void OnGet()
        {
            string url = HttpContext.Request.Host.ToUriComponent();

            DataAccess data = Utils.GetDataAccess(_appSettings, url);
            Console.WriteLine("IndexModel --> webApp.Init");
            
            // da spostare nel Layout: sarebbe bene richiamarla solo 1 volta
            _webApp.Init(data, url, _appSettings);

            // Carico la pagina corrrente
            var currentPagePath = HttpContext.Request.Path;
            _webApp.LoadCurrPage(currentPagePath, "home", "not-found", "reserved");

            ViewData["Layout"] = _webApp.currPage.GetLayout("lay_home");

            ViewData["currPage"] = _webApp.currPage;
            ViewData["currLang"] = _webApp.currLang;
            ViewData["error"] = data.LastError;

            //string host = NavigationManager.BaseUri; // dominio
            // risalgo al dominio con HttpContext, aggiusta la stringa a seconda della necessità
            string host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            //string url = HttpContext.Request.Host.ToUriComponent();

            //DataAccess data = Utils.GetDataAccess(_appSettings, host);

            //Console.WriteLine("_LayoutModel --> OnGet qui non passa");

            _webApp.Init(data, url, _appSettings);


            crossRepository repository = new crossRepository(data, 0);
            var pages = repository.GetRepositoryList(0, "WebPage", false, "", "", "", null, "", false);
            ViewData["Pages"] = pages;  //passo Pages alla view del Layout 


            ViewData["HeaderPartial"] = "_HeaderA";
            ViewData["FooterPartial"] = "_FooterA";

        }
    }
}
