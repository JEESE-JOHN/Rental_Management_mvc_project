using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project.Models;


namespace mvc_project.Controllers
{
    public class propertyController : Controller
    {
        mvc_projectEntities dbobj = new mvc_projectEntities();

        // GET: property
        public ActionResult property_pageload()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }

            int oid = Convert.ToInt32(Session["uid"]);
            var data = dbobj.sp_selectpropertybyowner(oid).ToList();
            ViewBag.userdetails = data;
            return View();
        }
        public ActionResult property_click(propertycls clsobj, HttpPostedFileBase file1, HttpPostedFileBase file2, FormCollection form)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }

            clsobj.owner_id = Convert.ToInt32(Session["uid"]);

            if (ModelState.IsValid)
            {
                if (file1 != null && file1.ContentLength > 0)
                {
                    string fname1 = Path.GetFileName(file1.FileName);
                    var s = Server.MapPath("~/phs");
                    string pa1 = Path.Combine(s, fname1);
                    file1.SaveAs(pa1);

                    var fullpath1 = Path.Combine("~\\phs", fname1);
                    clsobj.photo1 = fullpath1;
                }

                if (file2 != null && file2.ContentLength > 0)
                {
                    string fname2 = Path.GetFileName(file2.FileName);
                    var s = Server.MapPath("~/phs");
                    string pa2 = Path.Combine(s, fname2);
                    file2.SaveAs(pa2);

                    var fullpath2 = Path.Combine("~\\phs", fname2);
                    clsobj.photo2 = fullpath2;
                }
                dbobj.sp_propertyinsert(
                    clsobj.owner_id,
                    clsobj.property_title,
                    clsobj.description,
                    clsobj.location,
                    clsobj.rent,
                    clsobj.bedrooms,
                    clsobj.bathrooms,
                    clsobj.area,
                    clsobj.photo1,
                    clsobj.photo2,
                    clsobj.available_from,
                    clsobj.lease_duration
                    );

                TempData["msg"] = "Property added successfully";
                return RedirectToAction("property_pageload");

                // Insert property details into the database here
            }

            return View("property_pageload", clsobj);
        }
    }
}