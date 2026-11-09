using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project.Models;
namespace mvc_project.Controllers
{
    public class ownerregController : Controller
    {
        // GET: ownerreg
        mvc_projectEntities dbobj = new mvc_projectEntities();

        public ActionResult ownerreg_pageload()
        {
            return View();
        }
        public ActionResult ownerreg_click(ownerregcls clsobj)
        {
            if (ModelState.IsValid)
            {
                var getmaxid = dbobj.sp_maxidlogin().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;

                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }

                dbobj.sp_ownerreg(regid, clsobj.own_name, clsobj.own_email, clsobj.own_address, clsobj.own_phone);
                dbobj.sp_login(regid, clsobj.username, clsobj.pass, "owner");

                clsobj.ownermsg = "Successfully registered";

                return View("ownerreg_pageload", clsobj);
            }

            return View("ownerreg_pageload", clsobj);
        }
    }
}