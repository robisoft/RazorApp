using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RazorApp.Services;
using crossPublisher;

namespace RazorApp.ViewComponents
{
    public class HeaderNavigationViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;
        public HeaderNavigationViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Pages = _pageService.GetPages();

            return View(Pages);
        }
    }
}
