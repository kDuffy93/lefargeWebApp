using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lefarge_FE_App.Models;
using System.Data;



namespace Lefarge_FE_App
{
    public partial class report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getReport();
            Int32 EquipmentID = Convert.ToInt32(Request.QueryString["selectedEquipment"]);
            txtEqID.Text = EquipmentID.ToString();
            convertIDtoValue(EquipmentID);
        }

        protected void convertIDtoValue(int EquipmentID)
        {
             using (DefaultConnection conn = new DefaultConnection())
                    {
 
                        var equipName = (from q in conn.Equipments
                                            where q.Unit_Number == EquipmentID
                                            select q).FirstOrDefault();

                        txtEqID.Text = equipName.Name;
        }
        }
        protected void getReport()
        {
            using (DefaultConnection conn = new DefaultConnection())
            {
                //use link to query the Departments model
                var result = from r in conn.Results
                            select r;
                //bind the query result to the gridview
                grdResults.DataSource = result.ToList();
                grdResults.DataBind();
            }
        }

       
        protected void grdResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow dr = e.Row as GridViewRow;
                
                
                    using (DefaultConnection conn = new DefaultConnection())
                    {
                        Int32 qID = Convert.ToInt32(dr.Cells[0].Text);

                        var tempQuestion = (from q in conn.Questions
                                            where q.Question_ID == qID
                                            select q).FirstOrDefault();

                        dr.Cells[0].Text = tempQuestion.Question1;

                        var hID = Convert.ToInt32(dr.Cells[4].Text);

                        var tempHeading = (from h in conn.Headings
                                           where h.Heading_ID == hID
                                           select h).FirstOrDefault();

                        dr.Cells[4].Text = tempHeading.Heading1;
                    }
            }
        }

    }
}