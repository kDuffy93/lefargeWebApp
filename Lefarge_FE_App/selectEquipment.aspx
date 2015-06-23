<%@ Page Title="" Language="C#" MasterPageFile="~/Lefarge.Master" AutoEventWireup="true" CodeBehind="selectEquipment.aspx.cs" Inherits="Lefarge_FE_App.survey" %>

    <asp:Content ID="cntMain" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="well">
        <div>
        <asp:Label runat="server">Selected Plant:</asp:Label>
        <asp:TextBox ID="txtPlant" runat="server" Enabled="false" Text="0"></asp:TextBox>
            </div>
        <div>
        <asp:Label runat="server">Selected Category</asp:Label>
        <asp:TextBox ID="txtCategory" runat="server" Enabled="false" Text="0"></asp:TextBox>
            </div>
        </div>
        
       
            <div>
              <asp:Panel ID="pnlButtons" runat="server" on></asp:Panel>
               
            </div>
        
     
</asp:Content>
