using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project.Models;


namespace mvc_project.Controllers
{
    public class loginController : Controller
    {
        mvc_projectEntities dbobj = new mvc_projectEntities();

        public ActionResult login_pageload()
        {
            return View();
        }

        public ActionResult adminhome()
        {
            return View();
        }

        public ActionResult ownerhome()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("login_pageload");
            }

            int oid = Convert.ToInt32(Session["uid"]);

            var data = dbobj.sp_selectpropertybyowner(oid).ToList();
            ViewBag.userdetails = data;

            return View();
        }

        public ActionResult tenanthome()
        {
            var data = dbobj.sp_selectallproperty().ToList();
            ViewBag.userdetails = data;

            return View();
        }

        public ActionResult login_click(logincls objcls)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.sp_logincountid(objcls.uname, objcls.pw).FirstOrDefault();

                if (val == 1)
                {
                    var uid = dbobj.sp_loginid(objcls.uname, objcls.pw).FirstOrDefault();
                    Session["uid"] = uid;

                    var lt = dbobj.sp_logintype(objcls.uname, objcls.pw).FirstOrDefault();

                    if (lt == "owner")
                    {
                        return RedirectToAction("ownerhome");
                    }
                    else if (lt == "tenant")
                    {
                        return RedirectToAction("tenanthome");
                    }
                    else if (lt == "admin")
                    {
                        return RedirectToAction("adminhome");
                    }
                }
                else
                {
                    ModelState.Clear();
                    objcls.msg = "Invalid username and password....";
                    return View("login_pageload", objcls);
                }
            }
            else
            {
                ModelState.Clear();
                objcls.msg = "Invalid login....";
                return View("login_pageload", objcls);
            }

            return View("login_pageload", objcls);
        }
        
    }
}
