<%@ Page Title="" Language="C#" MasterPageFile="~/Lefarge.Master" AutoEventWireup="true" CodeBehind="questionList.aspx.cs" Inherits="Lefarge_FE_App.questionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1>Questions</h1>

    <a rel="external"  href="Question.aspx">Add a New Question</a>
    <asp:GridView ID="grdQuestions" runat="server" CssClass="table table-striped"
        AutoGenerateColumns="false" OnRowDeleting="grdQuestions_RowDeleting" DataKeyNames="Question_ID">

         <Columns>  
              <asp:BoundField DataField="Question1" HeaderText="Heading" />
              <asp:BoundField DataField="Headings_Under" HeaderText="Categories Under"/>
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="question.aspx" 
                 Text="Edit" DataNavigateUrlFields="Question_ID"
                 DataNavigateUrlFormatString="question.aspx?Question_ID={0}" />

            <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderText="Delete" />

             </Columns>
    </asp:GridView>
</asp:Content>
