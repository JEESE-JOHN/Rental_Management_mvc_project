using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project.Models;


namespace mvc_project.Controllers
{
    public class tenantregController : Controller
    {
        // GET: tenantreg
        mvc_projectEntities dbobj = new mvc_projectEntities();

        public ActionResult tenantreg_pageload()
        {
            return View();
        }
        public ActionResult tenantreg_click(tenantregcls clsobj)
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

                dbobj.sp_tenantreg(regid, clsobj.name, clsobj.age, clsobj.email, clsobj.phone, clsobj.address, clsobj.occupation);
                dbobj.sp_login(regid, clsobj.username, clsobj.pass, "tenant");

                clsobj.tenantmsg = "Successfully registered";

                return View("tenantreg_pageload", clsobj);
            }

            return View("tenantreg_pageload", clsobj);
        }
    }
}