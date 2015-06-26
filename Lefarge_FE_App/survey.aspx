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
        <asp:Label runat="server">Selected Equipment ID</asp:Label>
        <asp:TextBox ID="txtEquipment" runat="server" Enabled="false" Text="0"></asp:TextBox>
            </div>
        </div>
    <div id="srvMain" class="well">
        <asp:Table id="tblSurvey"  CssClass="table table-striped" CellPadding="5" CellSpacing="5"
        Gridlines="both" runat="server">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell>Question</asp:TableHeaderCell>
                <asp:TableHeaderCell>Yes/No</asp:TableHeaderCell>
                <asp:TableHeaderCell>Describe Defeciency/defect</asp:TableHeaderCell>
                <asp:TableHeaderCell>Corrective Action Plan</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date Completed</asp:TableHeaderCell></asp:TableHeaderRow>
            
            <asp:TableHeaderRow runat="server">
                 <asp:TableHeaderCell>Runways</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            
            <asp:TableRow runat="server">
                 <asp:TableCell>test Row (THIS IS NOT GENERATEd FROM A DATABASE ITS HARD CODED</asp:TableCell>
                <asp:TableCell runat="server">
               <asp:RadioButtonList ID="rbl1" RepeatDirection="Horizontal" AutoPostBack="True" runat="server" OnSelectedIndexChanged="rbl_SelectedIndexChanged">
                    <asp:ListItem Value="yes" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                   </asp:RadioButtonList>
                    </asp:TableCell>
                <asp:tableCell><asp:textBox TextMode="multiline" ID="tb1" runat="server"></asp:textBox></asp:tableCell>
                <asp:tableCell><asp:textBox TextMode="multiline" ID="tb2" runat="server"></asp:textBox></asp:tableCell>
                <asp:tableCell><asp:textBox Enabled="False" ID="tb3" runat="server"></asp:textBox></asp:tableCell>
                
               
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
