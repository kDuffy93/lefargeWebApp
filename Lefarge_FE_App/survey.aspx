<%@ Page Title="" Language="C#" MasterPageFile="~/Lefarge.Master" AutoEventWireup="true" CodeBehind="survey.aspx.cs" Inherits="Lefarge_FE_App.survey1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="well">
        <div>
        <asp:Label runat="server">Selected Plant:</asp:Label>
        <asp:TextBox ID="txtPlant" runat="server" Enabled="false" Text="0"></asp:TextBox>
            </div>
        <div>
        <asp:Label runat="server">Selected Category</asp:Label>
        <asp:TextBox ID="txtCategory" runat="server" Enabled="false" Text="0"></asp:TextBox>
            </div>
         <div>
        <asp:Label runat="server">selected piece of equipment:</asp:Label>
        <asp:TextBox ID="txtEquipment" runat="server" Enabled="false" Text="0"></asp:TextBox>
            </div> 
        <div><asp:TextBox ID="txtTimeout" runat="server" enables="false"></asp:TextBox></div>  
        </div> 
     <asp:Button runat="server" Visible="false" text="Start a new survey" ID="btnNewSurvey" OnClick="btnNewSurvey_Click" CssClass="btn btn-primary"/>
    <div id="srvMain">
        <asp:Table id="tblSurvey"  CssClass="table table-striped" CellPadding="5" CellSpacing="5"
        Gridlines="both" runat="server" border-right-width="1" EnableViewState="true">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell width="175">Question</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="75">Yes/No</asp:TableHeaderCell>
                <asp:TableHeaderCell width="200">Describe Defeciency/defect</asp:TableHeaderCell>
                <asp:TableHeaderCell width="200">Corrective Action Plan</asp:TableHeaderCell>
            </asp:TableHeaderRow>
         </asp:Table>
    </div>
   
</asp:Content>
