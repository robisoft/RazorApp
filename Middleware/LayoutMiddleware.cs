using crossPublisher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Runtime;
using System.Threading.Tasks;

namespace RazorApp.Middleware
{
    public class LayoutMiddleware
    {
        private readonly RequestDelegate _next;

        public LayoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }   

        public async Task Invoke(HttpContext context, IOptions<AppSettings> appSettings)
        {
            // ricavo il valore di pages. Per evitare di calcolarlo ad ogni request, potrei definire 'host' come proprietà di appSettings
            // e generarlo una volta sola in program.cs, cosi inietto direttamente pages dove mi pare. In ogni caso:
            var host = $"{context.Request.Scheme}://{context.Request.Host}";
            var data = Utils.GetDataAccess(appSettings.Value, host);
            var repository = new crossRepository(data, 0);
            var pages = repository.GetRepositoryList(0, "WebPage", false, "", "", "", null, "", false);
            
            context.Items["Pages"] = pages;


            var path = context.Request.Path;
            // scelgo il layout della pagina corrente in base al path
            // poi vedi come sono fatti gli oggetti Repository page per mettere in relazione 
            // percorso e layout  come si deve
            var layout = DetermineLayout(path, pages);
            
            // aggiungo 'layout' come info aggiuntiva della richiesta
            context.Items["Layout"] = layout;

            await _next(context);
        }

        private string DetermineLayout(string currentPageName, List<Repository> pages) 
        {
            // implementa la logica per scegliere il layout in base alla pagina (quindi in base al path definito prima!)
            // non so se è nella proprietà URL o da altre parti.  
            var currentPage = pages.FirstOrDefault(p => p.URL == currentPageName);

            if (currentPage == null)
            {
                return "_Layout"; // Layout predefinito se nessuna corrispondenza
            }

            // Restituisci il nome del layout associato alla pagina corrente
            return currentPage.Layout;
        }
    }
}
