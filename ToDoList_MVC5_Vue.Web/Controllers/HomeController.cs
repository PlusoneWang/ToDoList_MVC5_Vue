﻿namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}