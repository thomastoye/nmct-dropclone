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
            bool isAdmin = User.IsInRole("Administrator");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.MijnBestanden = DAFileRegistration.BestandenVanUser(User.Identity.Name);
            ViewBag.ToegangBestanden = DAFileRegistration.BestandenMetToegangVoor(User.Identity.Name, isAdmin);
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

        [HttpGet]
        public ActionResult Download(int id)
        {
            FileRegistration reg = DAFileRegistration.GetFileRegistrationById(id);

            if (!User.IsInRole("Administrator") && User.Identity.Name != reg.UserName)
                return new HttpUnauthorizedResult();

            string path = Server.MapPath("~/app_data/uploads/");
            path += reg.FileName;
            DAFileLogging.SaveLog(id, User.Identity.Name);
            return File(System.IO.File.ReadAllBytes(path), System.Net.Mime.MediaTypeNames.Application.Octet, reg.FileName);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ConfirmDelete(int id)
        {
            FileRegistration reg = DAFileRegistration.GetFileRegistrationById(id);
            if (reg == null) // not found
                return new HttpNotFoundResult();

            if (reg.UserName != User.Identity.Name) // not his own file, so can't delete it
                return new HttpUnauthorizedResult();

            ViewBag.FileRegistration = reg;
            return View();

        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            FileRegistration reg = DAFileRegistration.GetFileRegistrationById(id);
            if (reg == null) // not found
                return new HttpNotFoundResult();

            if (reg.UserName != User.Identity.Name) // not his own file, so can't delete it
                return new HttpUnauthorizedResult();

            string path = Server.MapPath("~/app_data/uploads/");
            path += reg.FileName;

            System.IO.File.Delete(path);

            DAFileRegistration.DeleteFile(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles="Administrator")]
        public ActionResult Logs()
        {
            ViewBag.Logs = DAFileLogging.GetLogs();
            return View();
        }
    }
}
