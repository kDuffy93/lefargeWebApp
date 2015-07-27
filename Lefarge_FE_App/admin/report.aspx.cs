﻿using System;
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
           
            Int32 EquipmentID = Convert.ToInt32(Request.QueryString["selectedEquipment"]);
            txtEqID.Text = EquipmentID.ToString();
            txtEqUn.Text = EquipmentID.ToString();
            convertIDtoValue(EquipmentID);
            getReport(EquipmentID);
            selectDate(EquipmentID);
        }
        protected void selectDate(int EquipmentID)
    {
        using (DefaultConnectionEF conn = new DefaultConnectionEF())
                    {
                        var possibleDates = (from r in conn.Results
                                             where r.Equipment_ID == EquipmentID
                                             select r.Date_Completed).Distinct().ToList();


            
            
       ddlDates.DataSource = possibleDates;
                ddlDates.DataBind();
            
      
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
        protected void getReport(int EquipmentID)
        {
            using (DefaultConnectionEF conn = new DefaultConnectionEF())
            {
                //use link to query the Departments model
                var result = from r in conn.Results
                             where r.Equipment_ID == EquipmentID
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

                       if(response == "True")
                       {
                           dr.Cells[1].Text = "yes";

                       }
                       else if(response == "False")
                       {
                           dr.Cells[1].Text = "no";
                       }
                       
                    }
            }
        }

        protected void ddlDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            var a = sender as RadioButtonList;
            
        }

        protected void ddlDates_DataBinding(object sender, EventArgs e)
        {
            var b = sender as RadioButtonList;
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