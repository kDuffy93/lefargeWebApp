<%@ Page Title="" Language="C#" MasterPageFile="~/Lefarge.Master" AutoEventWireup="true" CodeBehind="plants.aspx.cs" Inherits="Lefarge_FE_App.listPlants" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Plants</h1>

    <a rel="external" href="Plant.aspx">Add New Plant</a>

    <asp:GridView ID="grdPlants" runat="server" CssClass="table table-striped"
        AutoGenerateColumns="false" OnRowDeleting="grdPlants_RowDeleting"
        DataKeyNames="Plant_ID">
        <Columns>        
            <asp:BoundField DataField="Plant_Name" HeaderText="Plant Name" />
            <asp:BoundField DataField="Address" HeaderText="Address"/>
            <asp:BoundField DataField="Phone_Num" HeaderText="PhoneNum"/>
            <asp:BoundField DataField="Postal_Code" HeaderText="Postal Code"/>
            <asp:BoundField DataField="City" HeaderText="City"/>
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="plant.aspx" 
                 Text="Edit" DataNavigateUrlFields="Plant_ID"
                 DataNavigateUrlFormatString="plant.aspx?Plant_ID={0}" />
            <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
