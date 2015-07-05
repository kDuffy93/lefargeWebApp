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






        }
        protected void Page_Load(object sender, EventArgs e)
        {

            this.EnableViewState = true;


            buildTable();

            fillSelections();

            if (IsPostBack)
            {
                tblSurvey = (Table)Session["surveyTable"];
            }

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
                    c.ID = "heading" + i;
                    var allIDs = (from headings in conn.Headings
                                  where headings.Categories_Under.Contains(SelectedCategory)
                                  select headings.Heading_ID).ToList();
                    var selectedID = allIDs[i];
                    // c.ID = selectedID;
                    r.Controls.Add(c);
                    tblSurvey.Controls.Add(r);
                    getHeadingsQuestions(selectedID);
                    if (i + 1 == neededHeadings.Count)
                    {
                        TableHeaderRow btnRow = new TableHeaderRow();
                        TableHeaderCell btnCell = new TableHeaderCell();
                        Button btnSubmit = new Button();
                        btnSubmit.Click += new EventHandler(btnSubmit_Click);
                        btnSubmit.Text = "submit";
                        btnCell.Controls.Add(btnSubmit);
                        btnRow.Controls.Add(btnCell);
                        tblSurvey.Controls.Add(btnRow);
                    }
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
                    TableCell cellQuestion = new TableCell();
                    cellQuestion.Text = question;
                    cellQuestion.BackColor = Color.Lime;
                    cellQuestion.ForeColor = Color.Navy;
                    var allIDs = (from questions in conn.Questions
                                  where questions.Headings_Under.Contains(selectedID.ToString())
                                  select questions.Question_ID).ToList();
                    cellQuestion.ID = allIDs[i].ToString() + ("_Question_H=") + selectedID;



                    TableCell cellResponse = new TableCell();
                    cellResponse.ID = allIDs[i].ToString() + ("_response_H=") + selectedID;
                    RadioButtonList resp = new RadioButtonList();

                    resp.RepeatDirection = 0;
                    resp.Attributes.Add("ID", allIDs[i].ToString() + "_response_H=" + selectedID + "rbl");
                    for (int w = 0; w < 2; w++)
                    {
                        ListItem answer = new ListItem();
                        if (w == 0)
                        {

                            answer.Text = ("Yes");
                            answer.Value = ("true");

                        }
                        else if (w == 1)
                        {
                            answer.Text = ("No");
                            answer.Value = ("false");

                        }
                        resp.Items.Add(answer);
                    }

                    cellResponse.Controls.Add(resp);

                    TableCell cellDeficency = new TableCell();
                    TextBox txtDeficency = new TextBox();
                    txtDeficency.Width=50px;
                    txtDeficency.ID = allIDs[i] + ("_Deficency_H=") + selectedID;
                    txtDeficency.TextMode = TextBoxMode.MultiLine;
                    cellDeficency.Controls.Add(txtDeficency);

                    TableCell cellAP = new TableCell();
                    TextBox txtAP = new TextBox();
                     txtAP.Width=50px;
                    txtAP.ID = allIDs[i] + ("_ActionPlan_H=") + selectedID;
                    txtAP.TextMode = TextBoxMode.MultiLine;
                    cellAP.Controls.Add(txtAP);

                    TableCell cellDate = new TableCell();


                    TableCell cellDateSubmited = new TableCell();
                    TextBox txtDateSubmited = new TextBox();
                    txtDateSubmited.ID = allIDs[i] + ("_DateSubmited_H=") + selectedID;

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

            Session["sessionTable"] = tblSurvey;
            RadioButtonList button = (RadioButtonList)sender;
            string buttonId = button.Parent.ID;
            TableCell cell = button.Parent as TableCell;
            TableRow row = cell.Parent as TableRow;


            foreach (ListItem item in button.Items)
            {
                if (item.Selected)
                {
                    if (item.Value == "no")
                    {
                        TextBox txtDate = new TextBox();
                        txtDate.Text = DateTime.Now.ToString();
                        txtDate.TextMode = TextBoxMode.Date;
                        txtDate.Attributes.Add("enableViewState", "True");
                        row.Cells[4].Controls.Add(txtDate);
                    }
                    else if (item.Value == "yes")
                    {
                        TextBox txtDate = new TextBox();
                        txtDate.Text = DateTime.Now.ToString();
                        txtDate.TextMode = TextBoxMode.Date;
                        txtDate.Attributes.Add("enableViewState", "True");
                        row.Cells[4].Controls.Add(txtDate);
                    }
                }
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            TableCell cell = button.Parent as TableCell;
            TableRow row = cell.Parent as TableRow;
            Table table = row.Parent as Table;

            foreach (TableRow workingRow in table.Rows)
            {


              
                    if (workingRow.GetType() == typeof(TableHeaderRow))
                    {
                        continue;
                    }
                        
                    else
                    {
                        using (DefaultConnection conn = new DefaultConnection())
                        {
                        Result r = new Result();
                        DateTime dateAndTime = DateTime.Now;
                        // set date completed
                        r.Date_Completed = DateTime.Now;

                        //save white equipment it is
                        r.Equipment_ID = Convert.ToInt32(txtEquipment.Text);

                        foreach (TableCell currentCell in workingRow.Cells)
                        {
                            if (currentCell == workingRow.Cells[0])
                            {
                                TableCell t = (TableCell)currentCell;
                                int indexOfUnderscore;
                                try
                                {
                                    indexOfUnderscore = t.ID.IndexOf("_");
                                    if (indexOfUnderscore == -1)
                                    {
                                        continue;
                                    }
                                }
                                catch (NullReferenceException)
                                {
                                    continue;
                                }


                                int indexOfNumSign = t.ID.IndexOf("=") + 1;
                                // set question ID
                                r.Question_ID = Convert.ToInt32(t.ID.Substring(0, indexOfUnderscore));
                                //set heading id
                                r.heading_ID = Convert.ToInt32(t.ID.Substring(indexOfNumSign));
                            }
                            foreach (Control control in currentCell.Controls)
                            {
                                if (control.GetType() == typeof(RadioButtonList))
                                {

                                    RadioButtonList rbl = (RadioButtonList)control;
                                   
                                        
                                            if (rbl.SelectedValue == "true")
                                            {
                                                r.Response = true;
                                            }
                                       
                                        else if (rbl.SelectedValue == "false")
                                        {
                                            r.Response = false;
                                        }
                                
                                }// check control to see if its a rbl  

                                if (control.GetType() == typeof(TextBox))
                                {
                                    TextBox txt = (TextBox)control;
                                    if (txt.ID == (r.Question_ID + "_Deficency_H=" + r.heading_ID))
                                    {
                                        r.deficiency_defect = txt.Text;
                                    }// checks if txt box is a defect
                                    else if (txt.ID == (r.Question_ID + "_ActionPlan_H=" + r.heading_ID))
                                    {
                                        r.Action_plan = txt.Text;
                                    }//checks if txtbox is action plan
                                }//checks for txtbox  
                            }
                            }//foreach control

                        conn.Results.Add(r);
                        conn.SaveChanges();
                    }// checks to see if workign row is not a header row

                        
                    }//default conn


                } //for each table row
            btnNewSurvey.Visible = true;
            }

        

        protected void btnNewSurvey_Click(object sender, EventArgs e)
        {
            Response.Redirect("startSurvey.aspx");
        }// btn submit click close





        }// partial class close

    }//namespace close

