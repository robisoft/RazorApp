using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using crossPublisher;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Newtonsoft.Json;

// ?? passo 1 inject AppSettings appSettings

namespace RazorApp.Pages
{
    public class _LayoutModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppSettings _appSettings;
        private readonly HttpContext _httpContext;
        public List<Repository> Pages { get; set; } = new List<Repository>();
        private readonly string lang = "it";


        public _LayoutModel(ILogger<IndexModel> logger, 
                            IOptions<AppSettings> appSettings,
                            HttpContext httpContext)  
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContext = httpContext;
        }

        public string HeaderPartial { get; private set; }
        public string FooterPartial { get; private set; }

        public void OnGet()
        {
            //string host = NavigationManager.BaseUri; // dominio
            // risalgo al dominio con HttpContext, aggiusta la stringa a seconda della necessità
            string host = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}"; 

            DataAccess data = Utils.GetDataAccess(_appSettings, host);
            crossRepository repository = new crossRepository(data, 0);
            Pages = repository.GetRepositoryList(0, "WebPage", false, "", "", "", null, "", false);
            ViewData["Pages"] = Pages;  //passo Pages alla view del Layout 

            var currentPagePath = _httpContext.Request.Path;
            var layout = DetermineLayout(currentPagePath, Pages);

            HeaderPartial = GetHeaderPartial(layout);
            FooterPartial = GetFooterPartial(layout);

            ViewData["HeaderPartial"] = HeaderPartial;
            ViewData["FooterPartial"] = FooterPartial;
        }

        private string DetermineLayout(string currentPageName, List<Repository> pages)
        {
            // implementa la logica per scegliere il layout in base alla pagina (quindi in base al path definito prima!)
            // non so se è nella proprietà URL o da altre parti.  
            var currentPage = pages.FirstOrDefault(p => p.URL == currentPageName);

            if (currentPage == null)
            {
                return "_Layout"; // Layout predefinito se nessuna corrispondenza, 
            }

            // Restituisci il nome del layout associato alla pagina corrente
            return currentPage.GetLayout("_Layout");
        }
        private string GetHeaderPartial(string layout) {
            //caricati header appropriato a seconda del layout

            if (layout == "LayoutA")
            {
                return "_HeaderA";
            }
            else if (layout == "LayoutB")
            {
                return "_HeaderB";
            }
            else
            {
                return "_HeaderDefault"; 
            }
        }

        private string GetFooterPartial(string layout) {
            if (layout == "LayoutA")
            {
                return "_FooterA";
            }
            else if (layout == "LayoutB")
            {
                return "_FooterB";
            }
            else
            {
                return "_FooterDefault"; 
            }
       
        }
    }
}
