using BGL.Test.Portal.Models;
using System.Web.Mvc;
using BGL.Test.Service.Interfaces;

namespace BGL.Test.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitService service;

        public HomeController(IGitService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(SearchModel model)
        {
            var search = model;
            var userDetails = service.GetUserDetails(model.SearchViewModel.Name);
            var user = new UserViewModel
            {
                UserName = userDetails.UserName ?? string.Empty,
                Avatar = userDetails.AvatarUrl ?? string.Empty,
                Location = userDetails.Location ?? string.Empty,
                Repository = userDetails.RepoList
            };
            search.UserViewModel = user;
            return View("Index", search);
        }
    }
}