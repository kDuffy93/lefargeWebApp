using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lefarge_FE_App.Models;

namespace Lefarge_FE_App
{

    public partial class survey1 : System.Web.UI.Page
    {
        public int selectedCategory { get; set; }

        public int selectedPlant { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

          
            fillSelections();
            buildTable();
        }

        
        private void fillSelections()
        {
            var plant = Session["selectedPlant"];
            var category = Session["selectedCategory"];
            var equipment = Request.QueryString["selectedEquipment"].ToString();
            // Write the modified stock picks list back to session state.
            txtCategory.Text = category.ToString();
            txtPlant.Text = plant.ToString();
            txtEquipment.Text = equipment;
        }
        protected void buildTable()
        {
            using (DefaultConnection conn = new DefaultConnection())
            {
                var SelectedCategory = Convert.ToInt32(Session["selectedCategory"]);
                var neededHeadings = (from headings in conn.Headings
                                      where headings.Category_ID == SelectedCategory
                
                         select headings.Heading1).ToList();

             

                for (int i = 0; i < neededHeadings.Count; i++)
                {
                    TableHeaderRow r = new TableHeaderRow();
                    r.BackColor = Color.DarkBlue;
                    var heading = neededHeadings[i];
                   TableHeaderCell c = new TableHeaderCell();
                    c.Text = heading;
                    var allIDs = (from headings in conn.Headings
                                     where headings.Category_ID == SelectedCategory
                                     select headings.Heading_ID).ToList();
                    var selectedID = allIDs[i];
                    r.Controls.Add(c);
                    tblSurvey.Controls.Add(r);
                    getHeadingsQuestions(selectedID);
                }

              


            }
        }

        public void getHeadingsQuestions(int selectedID)
        {
            using (DefaultConnection conn = new DefaultConnection())
            {
                var qList = (from questions in conn.Questions
                    where questions.Headings_Under.Contains(selectedID.ToString()) 
                select questions.Question1).ToList();
               
                for (int i = 0; i < qList.Count; i++)
                {
                    TableRow r = new TableRow();
                    var question = qList[i];
                    TableHeaderCell q = new TableHeaderCell();
                    q.Text = question;
                    q.BackColor = Color.Chartreuse;
                    var allIDs = (from questions in conn.Questions 
                                 where  questions.Headings_Under.Contains(selectedID.ToString()) 
                select questions.Question_ID).ToList();
                    q.ID = allIDs[i].ToString();

                    
                    
                    r.Controls.Add(q);
                    tblSurvey.Controls.Add(r);
                    
                }

            }
        }
        
    }
}