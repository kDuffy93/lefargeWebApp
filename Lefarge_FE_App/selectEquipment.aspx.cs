using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lefarge_FE_App.Models;

namespace Lefarge_FE_App
{
    public partial class survey : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Keys.Count > 0)
                {
                    //we have a url parameter if the count is > 0 so populate the form


                
                }
               
                fillSelections();
                fillBtnPnl();
            }

        }

        private void fillSelections()
        {
            var plant = Session["selectedPlant"];
            var category = Session["selectedCategory"];
// Write the modified stock picks list back to session state.
            txtCategory.Text = category.ToString();
            txtPlant.Text = plant.ToString();
        }

        private void fillBtnPnl()
        {
            using (DefaultConnection conn = new DefaultConnection())
            {
                var SelectedPlant = Convert.ToInt32(Session["selectedPlant"]);
                var SelectedCategory = Convert.ToInt32(Session["selectedCategory"]);
                //create a list the holds the plants table
                var b = (from equip in conn.Equipments
                    where equip.Category_ID == SelectedCategory & equip.Plant_ID == SelectedPlant
                    select equip).ToList();

               
                    for (int i = 0; i < b.Count; i++)
                    {
                       
                        HyperLink equipmentButton = new HyperLink();
                        equipmentButton.Text = b[i].Name;
                        equipmentButton.CssClass = "btn btn-primary";
                        equipmentButton.NavigateUrl = "survey.aspx?selectedEquipment=" + b[i].Unit_Number;
                        pnlButtons.Controls.Add(equipmentButton);

                    }
                

                //set the datasource to the created list and bind it to the dropdown


            }
        }

        protected void equipmentButton_Click(object sender, EventArgs e)
        {
           
            Button button = (Button)sender;
            string buttonId = button.ID;
            Session["selectedEquipment"] = buttonId.ToString();
            Response.Redirect("survey.aspx");
        }
    }
}