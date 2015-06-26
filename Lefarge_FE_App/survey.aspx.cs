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
                    r.ForeColor = Color.Lime;
                    r.BackColor = Color.Navy;
                    var heading = neededHeadings[i];
                   TableHeaderCell c = new TableHeaderCell();
                    c.Text = heading;
                    var allIDs = (from headings in conn.Headings
                                     where headings.Category_ID == SelectedCategory
                                     select headings.Heading_ID).ToList();
                    var selectedID = allIDs[i];
                   // c.ID = selectedID;
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
                    //ADD THE QUESTION
                    TableCell q = new TableCell();
                    q.Text = question;
                    q.BackColor = Color.Lime;
                    q.ForeColor = Color.Navy;
                    var allIDs = (from questions in conn.Questions 
                                 where  questions.Headings_Under.Contains(selectedID.ToString()) 
                select questions.Question_ID).ToList();
                    q.ID = allIDs[i].ToString() + ("_Question");

                    TableCell response = new TableCell();
                    Panel panel = new Panel();
                    RadioButtonList resp = new RadioButtonList();

                    resp.RepeatDirection = 0; resp.AutoPostBack = true; resp.SelectedIndexChanged += new EventHandler(rbl_SelectedIndexChanged);
                ListItem answer = new ListItem();
                    for (int w = 0; w < 2; w++)
                    {
                        if (w == 0)
                        {
                            answer.Text = ("Yes");
                            answer.Value =("_response=YES#_Question_#") + allIDs[i];
                           
                        }
                        else if (w == 1)
                        {
                            answer.Text = ("No");
                            answer.Value = ("_response=NO#_Question_#") + allIDs[i];
                            
                        }
                        resp.Items.Add(answer);
                    }
                    panel.Controls.Add(resp);
                    response.Controls.Add(panel);
                    r.Controls.Add(q);
                    r.Controls.Add(response);
                    tblSurvey.Controls.Add(r);
                  
                    
                }

            }
        }

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
        
    }
}