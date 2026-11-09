using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    public class rentalapplicationController : Controller
    {
        // GET: rentalapplication
        mvc_projectEntities dbobj = new mvc_projectEntities();

        public ActionResult application_pageload(int id)
        {
            Session["property_id"] = id;

            int tenantid = Convert.ToInt32(Session["uid"]);
            int propertyid = Convert.ToInt32(Session["property_id"]);

            ObjectParameter op = new ObjectParameter("status", typeof(int));

            dbobj.SP_CountTenantAppliedPropertyWithID(
                tenantid, propertyid, op);

            int countapply = Convert.ToInt32(op.Value);

            if (countapply == 1)
            {
                ViewBag.msg = "You have already applied for this property.";
            }

            ViewBag.isapplied = countapply;

            return View(getdata());
        }
        public ActionResult application_click(
            HttpPostedFileBase file,
            rentalapplicationcls clsobj)
        {
            if (ModelState.IsValid)
            {
                string docpath = null;

                if (file != null && file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);

                    string folder = Server.MapPath(
                        "~/ApplicationDocuments/");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string uniqueName = Guid.NewGuid().ToString()
                        + Path.GetExtension(fname);

                    string path = Path.Combine(
                        folder, uniqueName);

                    file.SaveAs(path);

                    docpath = "~/ApplicationDocuments/" + uniqueName;
                }

                clsobj.application_date = DateTime.Today;

                clsobj.tenant_id = Convert.ToInt32(
                    Session["uid"]);

                clsobj.property_id = Convert.ToInt32(
                    Session["property_id"]);

                ObjectParameter op = new ObjectParameter(
                    "status", typeof(int));

                dbobj.SP_CountTenantAppliedPropertyWithID(
                    clsobj.tenant_id,
                    clsobj.property_id,
                    op);

                int countapply = Convert.ToInt32(op.Value);

                if (countapply == 1)
                {
                    ViewBag.isapplied = 1;
                    ViewBag.msg = "You have already applied for this property.";

                    return View("application_pageload", getdata());
                }

                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[
                        "importdataconnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        "sp_rentalapplication", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@tid", clsobj.tenant_id);

                    cmd.Parameters.AddWithValue(
                        "@pid", clsobj.property_id);

                    cmd.Parameters.AddWithValue(
                        "@msg",
                        (object)clsobj.ap_message ?? DBNull.Value);

                    cmd.Parameters.AddWithValue(
                        "@doc",
                        (object)docpath ?? DBNull.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("tenanthome", "login");
            }

            return View("application_pageload", getdata());
        }

        private rentalapplicationcls getdata()
        {
            rentalapplicationcls clsobj = new rentalapplicationcls();
            clsobj.propertydetails = new propertycls();

            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings[
                    "importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SP_GetPropertyDataWithID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@property_id",
                    Convert.ToInt32(Session["property_id"]));

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    clsobj.propertydetails.property_id =
                        Convert.ToInt32(dr["property_id"]);

                    clsobj.propertydetails.property_title =
                        dr["property_title"].ToString();

                    clsobj.propertydetails.description =
                        dr["description"].ToString();

                    clsobj.propertydetails.location =
                        dr["location"].ToString();

                    clsobj.propertydetails.rent =
                        Convert.ToDecimal(dr["rent"]);

                    clsobj.propertydetails.bedrooms =
                        Convert.ToInt32(dr["bedrooms"]);

                    clsobj.propertydetails.bathrooms =
                        Convert.ToInt32(dr["bathrooms"]);

                    clsobj.propertydetails.area =
                        Convert.ToInt32(dr["area"]);

                    clsobj.propertydetails.photo1 =
                        dr["photo1"].ToString();

                    clsobj.propertydetails.photo2 =
                        dr["photo2"].ToString();

                    clsobj.propertydetails.available_from =
                        Convert.ToDateTime(dr["available_from"]);

                    clsobj.propertydetails.lease_duration =
                        dr["lease_duration"].ToString();

                    clsobj.propertydetails.property_status =
                        dr["property_status"].ToString();

                    clsobj.property_id =
                        clsobj.propertydetails.property_id;
                }

                dr.Close();
            }

            return clsobj;
        }
    }
}