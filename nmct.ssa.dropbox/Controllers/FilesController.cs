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
            ViewBag.MijnBestanden = DAFileRegistration.BestandenVanUser(User.Identity.Name);
            ViewBag.ToegangBestanden = DAFileRegistration.BestandenMetToegangVoor(User.Identity.Name);
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
        public FileResult Download(int id)
        {
            FileRegistration reg = DAFileRegistration.GetFileRegistrationById(id);
            string path = Server.MapPath("~/app_data/uploads/");
            path += reg.FileName;
            return File(System.IO.File.ReadAllBytes(path), System.Net.Mime.MediaTypeNames.Application.Octet, reg.FileName);
        }

        [HttpGet]
        public ActionResult ConfirmDelete(int id)
        {
            FileRegistration reg = DAFileRegistration.GetFileRegistrationById(id);
            if (reg == null) // not found
                return RedirectToAction("Index");

            if (reg.UserName != User.Identity.Name) // not his own file, so can't delete it
                return RedirectToAction("Index");

            ViewBag.FileRegistration = reg;
            return View();

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            FileRegistration reg = DAFileRegistration.GetFileRegistrationById(id);
            if (reg == null) // not found
                return RedirectToAction("Index");

            if (reg.UserName != User.Identity.Name) // not his own file, so can't delete it
                return RedirectToAction("Index");

            string path = Server.MapPath("~/app_data/uploads/");
            path += reg.FileName;

            System.IO.File.Delete(path);

            DAFileRegistration.DeleteFile(id);

            return RedirectToAction("Index");
        }
    }
}
