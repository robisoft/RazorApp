using System.Collections.Generic;
using crossPublisher;

namespace RazorApp.Services
{
    public interface IPageService
    {
        List<Repository> GetPages();
    }
}
