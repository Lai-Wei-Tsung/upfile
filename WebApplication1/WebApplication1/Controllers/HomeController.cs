using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebApplication1.Models;
using System.Data;
using System.Net;
using System.Data.Entity;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        test1Entities db = new test1Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowList()
        {
            List<t1> data = db.t1.ToList();

            return View(data);
        }
        [HttpPost]
        public ActionResult ShowList(string search)
        {
            var data = from m in db.t1 select m;
            if (!String.IsNullOrEmpty(search))
            {
                data = data.Where(s => s.Name.Contains(search));
            }
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(t1 PostBack, HttpPostedFileBase Imag)
        {
            string FileName = Path.GetFileName(Imag.FileName);
            string FilePath = Path.Combine(Server.MapPath("~/Images/"), FileName);
            string FileRealPath = Path.Combine("/Images/", FileName);
            Imag.SaveAs(FilePath);
            try{
                PostBack.Imag = FileRealPath;
                PostBack.LImag = FilePath;
                db.t1.Add(PostBack);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ;
            }
            
         return RedirectToAction("ShowList");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t1 data = db.t1.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(t1 PostBack, HttpPostedFileBase Imag)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(id).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("ShowList");
            //}
            if (ModelState.IsValid)
            {
                string FileName = Path.GetFileName(Imag.FileName);
                string FilePath = Path.Combine(Server.MapPath("~/Images/"), FileName);
                string FileRealPath = Path.Combine("/Images/", FileName);
                var edits = db.t1.Where(x => x.Id == PostBack.Id).FirstOrDefault();
                edits.Name = PostBack.Name;
                edits.Imag = FileRealPath;
                edits.LImag = FilePath;
                //db.Entry(id).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowList");
            }
            return View(PostBack);
        }

        public ActionResult Delete(int? id)
        {
            var data = db.t1.Find(id);
            db.t1.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ShowList");
        }

        public ActionResult Tiny()
        {
            ViewBag.Message = "TinyMCE";

            return View();
        }

        [HttpPost]
        public ActionResult Tiny(string val)
        {
            string word = val;
            
            return View();
        }
        /*
        要求的 URL: /UploadFile/Uploaded
        */
        public ActionResult Upload_Multiple()
        {
            ViewBag.Et = "新增";
            return View();
        }
        public ActionResult Uploaded(HttpPostedFileBase[] files)
        {
            if (files.Count() > 0)
            {
                foreach (var uploadFile in files)
                {
                    if (uploadFile.ContentLength > 0)
                    {
                        string savePath = Server.MapPath("~/Images/");
                        uploadFile.SaveAs(savePath + uploadFile.FileName);
                    }
                }
            }
            return RedirectToAction("Upload_Multiple");
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase File)
        {
            if (File != null && File.ContentLength > 0)
            {
                //存到資料夾
                var FileName = Path.GetFileName(File.FileName);
                var FilePath = Path.Combine(Server.MapPath("~/Files/"), FileName);
                File.SaveAs(FilePath);
            }
            return View();
        }
    }
}