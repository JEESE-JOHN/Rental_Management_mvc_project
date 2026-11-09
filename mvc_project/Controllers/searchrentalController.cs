using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    public class searchrentalController : Controller
    {
        // GET: searchrental
        mvc_projectEntities dbobj = new mvc_projectEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult search_click(searchpropertycls clsobj)
        {
            string qry = "";
            if (!string.IsNullOrWhiteSpace(clsobj.location))
            {
                qry += " and location like '%" + clsobj.location + "%'";
            }
            if (clsobj.bedrooms != null)
            {
                qry += " and bedrooms = " + clsobj.bedrooms;
            }
            if (clsobj.rent != null)
            {
                qry += " and rent <= " + clsobj.rent;
            }
            var propertylist = getdata(clsobj, qry);

            ViewBag.userdetails = propertylist.selectproperty;
                
            return View("~/Views/login/tenanthome.cshtml", null);
        }
        private searchpropertycls getdata(searchpropertycls clsobj, string qry)
        {
            using (var con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_rentalsearch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry", qry);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var propertylist = new searchpropertycls();
                while (dr.Read())
                {
                    var propertyobj = new searchpropertycls();
                    propertyobj.property_id = Convert.ToInt32(dr["property_id"]);
                    propertyobj.owner_id = Convert.ToInt32(dr["owner_id"]);
                    propertyobj.property_title = dr["property_title"].ToString();
                    propertyobj.description = dr["description"].ToString();
                    propertyobj.location = dr["location"].ToString();
                    propertyobj.rent = Convert.ToDecimal(dr["rent"]);
                    propertyobj.bedrooms = Convert.ToInt32(dr["bedrooms"]);
                    propertyobj.bathrooms = Convert.ToInt32(dr["bathrooms"]);
                    propertyobj.area = Convert.ToInt32(dr["area"]);
                    propertyobj.photo1 = dr["photo1"].ToString();
                    propertyobj.photo2 = dr["photo2"].ToString();
                    propertyobj.available_from = Convert.ToDateTime(dr["available_from"]);
                    propertyobj.lease_duration = dr["lease_duration"].ToString();
                    propertyobj.property_status = dr["property_status"].ToString();
                    propertylist.selectproperty.Add(propertyobj);
                }

                dr.Close();

                return propertylist;
            }

        }
    }
}