using nmct.ssa.dropbox.common;
using nmct.ssa.dropbox.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.dropbox.Controllers
{
    public class ExternalUsersController : Controller
    {
        [HttpGet]
        [Authorize(Roles="Administrator")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        public ActionResult List(){
            ViewBag.Models = DAExternalUser.GetExternalUsers();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ExternalUser ext = DAExternalUser.GetById(id);
            return View(ext);
        }

        [HttpPost]
        public ActionResult Edit(ExternalUser ext)
        {
            DAExternalUser.Update(ext);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ExternalUser ext)
        {
            DAExternalUser.New(ext);
            return RedirectToAction("List");
        }
    }
}