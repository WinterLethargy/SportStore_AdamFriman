using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;
namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository _repository;
        public NavigationMenuViewComponent(IStoreRepository storeRepository)
        {
            _repository = storeRepository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
