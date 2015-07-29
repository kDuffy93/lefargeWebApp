using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lefarge_FE_App.Models;
using System.Data;

namespace Lefarge_FE_App.admin
{
    public partial class report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 EquipmentID = Convert.ToInt32(Request.QueryString["selectedEquipment"]);
            if (!IsPostBack)
            {

                txtEqID.Text = EquipmentID.ToString();
                txtEqUn.Text = EquipmentID.ToString();
                convertIDtoValue(EquipmentID);
                selectDate(EquipmentID);
            }


            if (IsPostBack)
            {

                var SelectedIndex = ddlDates.SelectedIndex;
                selectDate(EquipmentID, SelectedIndex);
            }



        }
        protected void selectDate(int EquipmentID)
        {
            using (DefaultConnectionEF conn = new DefaultConnectionEF())
            {
                var possibleDates = (from r in conn.Results
                                     where r.Equipment_ID == EquipmentID
                                     select r.Date_Completed).Distinct().ToList();

                getReport(EquipmentID, possibleDates[0]);


                ddlDates.DataSource = possibleDates;
                ddlDates.DataBind();


            }
        }
        protected void selectDate(int EquipmentID, int index)
        {
            using (DefaultConnectionEF conn = new DefaultConnectionEF())
            {
                var possibleDates = (from r in conn.Results
                                     where r.Equipment_ID == EquipmentID
                                     select r.Date_Completed).Distinct().ToList();

                getReport(EquipmentID, possibleDates[index]);


                ddlDates.SelectedIndex = index;


            }
        }
        protected void convertIDtoValue(int EquipmentID)
        {
            using (DefaultConnectionEF conn = new DefaultConnectionEF())
            {
                var equipName = (from q in conn.Equipments
                                 where q.Unit_Number == EquipmentID
                                 select q).FirstOrDefault();
                txtEqID.Text = equipName.Name;

            }
        }
        protected void getReport(int EquipmentID, DateTime selectedDate)
        {
            using (DefaultConnectionEF conn = new DefaultConnectionEF())
            {
                //use link to query the Departments model
                var result = from r in conn.Results
                             where r.Equipment_ID == EquipmentID & r.Date_Completed == selectedDate
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


                using (DefaultConnectionEF conn = new DefaultConnectionEF())
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

                    var response = dr.Cells[1].Text;

                    if (response == "True")
                    {
                        dr.Cells[1].Text = "yes";

                    }
                    else if (response == "False")
                    {
                        dr.Cells[1].Text = "no";
                    }

                }
            }
        }

        protected void ddlDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            Session["selectedDate"] = Convert.ToDateTime(ddl.SelectedValue);
            Session["selectedDateIndex"] = ddl.SelectedIndex;

        }

        protected void ddlDates_DataBinding(object sender, EventArgs e)
        {
            var b = sender as DropDownList;
            // var firstDate = b.SelectedValue;

            /* foreach (DataRow dr in grdResults.Rows)
              {
                 if(!(dr[5].Text.Contains(firstDate)))
                 {
                     grdResults.Rows.Remove(dr);
                 }
               
              }*/
        }

    }
}




