using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lefarge_FE_App.Models;

namespace Lefarge_FE_App
{
    public partial class heading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page isn't posted back, check the url for an id to see know add or edit
            if (!IsPostBack)
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    //we have a url parameter if the count is > 0 so populate the form
                    GetHeading();
                    
                }
                
            }
            fillDropDown();
        }

        protected void GetHeading()
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //get id from url parameter and store in a variable
                Int32 HeadingID = Convert.ToInt32(Request.QueryString["Heading_ID"]);

                var c = (from head in conn.Headings
                         where head.Heading_ID == HeadingID
                         select head).FirstOrDefault();

                //populate the form from our department object
                txtHeading.Text = c.Heading1;
                ddlCategory.SelectedIndex = c.Category_ID;

            }
        }

        protected void fillDropDown()
        {
            using (DefaultConnection conn = new DefaultConnection())
            {
                var c = (from cat in conn.Categories
                   
                select cat).ToList();
                ddlCategory.DataSource = c;
                ddlCategory.DataBind();



            }
        }


       

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection conn = new DefaultConnection())
            {
                //instantiate a new deparment object in memory
                Heading c = new Heading();

                //decide if updating or adding, then save
                if (Request.QueryString.Count > 0)
                {
                    Int32 HeadingID = Convert.ToInt32(Request.QueryString["Heading_ID"]);

                    c = (from head in conn.Headings
                         where head.Heading_ID == HeadingID
                         select head).FirstOrDefault();

                }

                //fill the properties of our object from the form inputs


                c.Heading1 = txtHeading.Text;
                c.Category_ID = Convert.ToInt32(ddlCategory.SelectedValue);

                if (Request.QueryString.Count == 0)
                {
                    conn.Headings.Add(c);
                }

                conn.SaveChanges();

                //redirect to updated departments page
                Response.Redirect("Headings.aspx");
            }

        }
    }
}
