using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    public class ownerapplicationController : Controller
    {
        SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString);

        public ActionResult applicationlist_pageload(int id)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("login_pageload", "login");
            }

            int ownerid = Convert.ToInt32(Session["uid"]);

            List<ownerapplicationcls> getlist = new List<ownerapplicationcls>();

            SqlCommand cmd = new SqlCommand("SP_SelectApplicationsByProperty", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@property_id", id);
            cmd.Parameters.AddWithValue("@owner_id", ownerid);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ownerapplicationcls clsobj = new ownerapplicationcls();

                clsobj.app_id = Convert.ToInt32(dr["app_id"]);
                clsobj.tenant_id = Convert.ToInt32(dr["tenant_id"]);
                clsobj.tenant_name = dr["tenant_name"].ToString();
                clsobj.email = dr["email"].ToString();
                clsobj.phone = dr["phone"].ToString();
                clsobj.address = dr["address"].ToString();
                clsobj.occupation = dr["occupation"].ToString();
                clsobj.property_id = Convert.ToInt32(dr["property_id"]);
                clsobj.property_title = dr["property_title"].ToString();
                clsobj.application_date = Convert.ToDateTime(dr["application_date"]);
                clsobj.ap_message = dr["ap_message"].ToString();
                clsobj.document = dr["document"] == DBNull.Value
                    ? null : dr["document"].ToString();
                clsobj.app_status = Convert.ToInt32(dr["app_status"]);

                getlist.Add(clsobj);
            }

            dr.Close();
            con.Close();

            return View(getlist);
        }
    }
}