using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using crossPublisher;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Security.Policy;
using crossPublisherRazor;

// ?? passo 1 inject AppSettings appSettings

namespace RazorApp.Pages
{
    public class _LayoutModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        private readonly AppSettings _appSettings;
        private WebAppSession _webApp { get; set; }

        public List<Repository> Pages { get; set; } = new List<Repository>();


        public _LayoutModel(ILogger<IndexModel> logger,
                            AppSettings appSettings,
                            WebAppSession webApp)  
        {
            _logger = logger;
            
            _appSettings = appSettings;
            _webApp = webApp;
            //_httpContext = httpContext;
        }

        public string HeaderPartial { get; private set; }
        public string FooterPartial { get; private set; }

        public void OnGet()
        {
            //string host = NavigationManager.BaseUri; // dominio
            // risalgo al dominio con HttpContext, aggiusta la stringa a seconda della necessità
            string host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            
            string url = HttpContext.Request.Host.ToUriComponent();

            DataAccess data = Utils.GetDataAccess(_appSettings, host);

            Console.WriteLine("_LayoutModel --> OnGet qui non passa");
            _webApp.Init(data, url, _appSettings);


            crossRepository repository = new crossRepository(data, 0);
            Pages = repository.GetRepositoryList(0, "WebPage", false, "", "", "", null, "", false);
            ViewData["Pages"] = Pages;  //passo Pages alla view del Layout 


            ViewData["HeaderPartial"] = "_HeaderA";
            ViewData["FooterPartial"] = "_FooterA";
        }

    }
}
