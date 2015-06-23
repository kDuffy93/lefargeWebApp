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
</asp:Content>
