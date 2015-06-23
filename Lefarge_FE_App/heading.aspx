<%@ Page Title="" Language="C#" MasterPageFile="~/Lefarge.Master" AutoEventWireup="true" CodeBehind="heading.aspx.cs" Inherits="Lefarge_FE_App.heading" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Heading</h1>
    
    <h5>Add a heading to a category</h5>
    
    <div class="form-group">
        <label for="txtHeading" class="col-sm-3">Heading:</label>
        <asp:TextBox ID="txtHeading" runat="server" required="true" MaxLength="30" />
    </div>
    
     <div class="form-group">
        
<label for="ddlCategory" class="col-sm-3">Category:</label>
        <asp:DropDownList  runat="server" ID="ddlCategory" DataValueField="Category_ID" DataTextField="Category1"/>
    </div>
    
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" CssClass="btn btn-primary"
             />
    </div>
</asp:Content>
