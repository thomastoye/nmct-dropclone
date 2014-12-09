using nmct.ssa.dropbox.DataAccess;
using nmct.ssa.dropbox.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.dropbox.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        // GET: Files
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            var context = new ApplicationDbContext();
            ViewBag.Users = context.Users;
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string description, string[] wie)
        {
            if (file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/app_data/uploads"), filename);
                file.SaveAs(path);
                int fileId = DAFileRegistration.SaveFileRegistration(filename, description, User.Identity.Name);

                foreach(string user in wie)
                    DAFileRegistration.SaveDownloaders(user, fileId);
            }
            return RedirectToAction("Index");

        }
    }
}
