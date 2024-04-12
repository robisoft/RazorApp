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
    public class lay_homeModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppSettings _appSettings;
        private readonly HttpContext _httpContext;
        public List<Repository> Pages { get; set; } = new List<Repository>();
        private readonly string lang = "it";


        public lay_homeModel(ILogger<IndexModel> logger, 
                            IOptions<AppSettings> appSettings,
                            HttpContext httpContext)  
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContext = httpContext;
        }

        public void OnGet()
        {
            Console.WriteLine("lay_homeModel --> onGet");
        }

    }
}
