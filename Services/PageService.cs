using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using crossPublisher;

namespace RazorApp.Services
{
    public class PageService : IPageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;

        public PageService(IHttpContextAccessor httpContextAccessor, AppSettings appSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        public List<Repository> GetPages()
        {
            var host = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            DataAccess data = Utils.GetDataAccess(_appSettings, host);
            crossRepository repository = new crossRepository(data, 0);
            var pages = repository.GetRepositoryList(0, "WebPage", false, "", "", "", null, "", false);

            Console.WriteLine("prova pagine " + pages);

            return pages;
        }
    }
}
