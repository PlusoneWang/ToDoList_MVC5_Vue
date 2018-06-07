﻿namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Po.Result;

    using ToDoList_MVC5_Vue.Library.ViewModels.ToDoLists;
    using ToDoList_MVC5_Vue.Service;

    public class ToDoListController : BaseController
    {
        private readonly ToDoListService toDoListService;

        public ToDoListController()
        {
            this.toDoListService = new ToDoListService();
        }

        /// <summary>
        /// ToDoList主頁面
        /// </summary>
        /// <returns>主頁面</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 取得使用者待辦清單
        /// </summary>
        /// <returns>使用者待辦清單</returns>
        [HttpGet]
        public ActionResult GetListAndFolders()
        {
            var userListsResult = this.toDoListService.GetUserToDoLists(this.CurrentUser.Id);
            if (userListsResult.Success)
            {
                var sidebarLists = new List<SidebarListsVm>();
                userListsResult.Data.ForEach(o =>
                    {
                        sidebarLists.Add(new SidebarListsVm
                        {
                            Id = o.Id,
                            Folder = o.Folder == null ? null : new SidebarListsVm.FolderVm { Id = o.Folder.Id, Name = o.Folder.Name },
                            Name = o.Name,
                            Sort = o.Sort,
                            TaskCount = o.ToDoTasks.Count,
                        });
                    });

                return this.Json(
                    new PoResult<List<SidebarListsVm>>
                    {
                        Success = true,
                        Message = userListsResult.Message,
                        Data = sidebarLists
                    },
                    JsonRequestBehavior.AllowGet);
            }

            return this.Json(userListsResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 建立清單
        /// </summary>
        /// <param name="listName">清單名稱</param>
        /// <returns>建立結果</returns>
        [HttpPost]
        public JsonResult CreateList(string listName)
        {
            var createResult = this.toDoListService.CreateList(listName, this.CurrentUser.Id);
            return this.Json(createResult);
        }
    }
}