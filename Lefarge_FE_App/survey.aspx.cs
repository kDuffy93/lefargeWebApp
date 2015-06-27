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

        protected void Page_Init(object sender, EventArgs e)
        {
         

                buildTable();
                
         


        }
        protected void Page_Load(object sender, EventArgs e)
        {

          
            fillSelections();
           
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
                var SelectedCategory = Session["selectedCategory"].ToString();
                var neededHeadings = (from headings in conn.Headings
                                      where headings.Categories_Under.Contains(SelectedCategory)
                
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
                                  where headings.Categories_Under.Contains(SelectedCategory)
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
                    where questions.Headings_Under.Contains(selectedID.ToString() )
                select questions.Question1).ToList();
               
                for (int i = 0; i < qList.Count; i++)
                {
                    TableRow r = new TableRow();
                    var question = qList[i];
                    //ADD THE QUESTION
                    TableCell cellQuestion = new TableCell();
                    cellQuestion.Text = question;
                    cellQuestion.BackColor = Color.Lime;
                    cellQuestion.ForeColor = Color.Navy;
                    var allIDs = (from questions in conn.Questions 
                                 where  questions.Headings_Under.Contains(selectedID.ToString()) 
                select questions.Question_ID).ToList();
                    cellQuestion.ID = allIDs[i].ToString() + ("_Question");

                    TableCell cellResponse = new TableCell();
                    Panel panel = new Panel();
                    RadioButtonList resp = new RadioButtonList();

                    resp.RepeatDirection = 0; resp.AutoPostBack = true; resp.SelectedIndexChanged += new EventHandler(rbl_SelectedIndexChanged);
                
                    for (int w = 0; w < 2; w++)
                    {
                        ListItem answer = new ListItem();
                        if (w == 0)
                        {
                            answer.Text = ("Yes");
                            answer.Value =("yes") + allIDs[i];
                           
                        }
                        else if (w == 1)
                        {
                            answer.Text = ("No");
                            answer.Value = ("no") + allIDs[i];
                            
                        }
                        resp.Items.Add(answer);
                    }
                    panel.Controls.Add(resp);
                    cellResponse.Controls.Add(panel);

                    TableCell cellDeficency = new TableCell();
                    TextBox txtDeficency = new TextBox();
                    txtDeficency.ID = allIDs[i] + ("_Question_Deficency");
                    txtDeficency.TextMode = TextBoxMode.MultiLine;
                    cellDeficency.Controls.Add(txtDeficency);
                  
                    TableCell cellAP = new TableCell();
                    TextBox txtAP = new TextBox();
                    txtAP.ID = allIDs[i] + ("_Question_ActionPlan");
                    txtAP.TextMode = TextBoxMode.MultiLine;
                    cellAP.Controls.Add(txtAP);
                  
                    TableCell cellDate = new TableCell();
                    TextBox txtDate = new TextBox();
                    txtDate.ID = allIDs[i] + ("_Question_DateCompleted");
                    txtDate.TextMode = TextBoxMode.SingleLine;
                    txtDate.Enabled = false;
                    cellDate.Controls.Add(txtDate);

                    TableCell cellDateSubmited = new TableCell();
                    TextBox txtDateSubmited = new TextBox();
                    txtDateSubmited.ID = allIDs[i] + ("_Question_DateSubmited");

                    cellDateSubmited.Controls.Add(txtDateSubmited);
                    cellDateSubmited.Visible = false;

                    r.Controls.Add(cellQuestion);
                    r.Controls.Add(cellResponse);
                    r.Controls.Add(cellDeficency);
                    r.Controls.Add(cellAP);
                    r.Controls.Add(cellDate);
                    r.Controls.Add(cellDateSubmited);
                    
                    tblSurvey.Controls.Add(r);
                  
                    
                }

            }
        }

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList button = (RadioButtonList)sender;
            string buttonId = button.ID;

            foreach (ListItem item in button.Items)
            {
                if (item.Selected)
                    item.Selected = true;
            }
            Response.Redirect("default.aspx");
        }
        
    }
}