﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lefarge_FE_App.Models;

namespace Lefarge_FE_App
{
    public partial class headings : System.Web.UI.Page
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                GetHeading();
            }
        }
        protected void GetHeading()
        {

            //connect using our connection string from web.config and EF context class
            using (DefaultConnection conn = new DefaultConnection())
            {
                //use link to query the Departments model
                var  head = from c in conn.Headings
                            select c;

                //bind the query result to the gridview
                grdHeadings.DataSource = head.ToList();
                grdHeadings.DataBind();
            }
        }
        protected void grdHeadings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //get the selected DepartmentID
                Int32 HeadingID = Convert.ToInt32(grdHeadings.DataKeys[e.RowIndex].Values["Heading_ID"]);

                var c = (from head in conn.Categories
                         where head.Category_ID == HeadingID
                         select head).FirstOrDefault();

                //process the delete
                conn.Categories.Remove(c);
                conn.SaveChanges();

                //update the grid
                GetHeading();
            }
        }

    }
}
