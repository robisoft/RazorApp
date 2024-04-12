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
           
        }

    }
}
