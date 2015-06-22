<%@ Page Title="" Language="C#" MasterPageFile="~/Lefarge.Master" AutoEventWireup="true" CodeBehind="heading.aspx.cs" Inherits="Lefarge_FE_App.heading" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Heading</h1>
    
    <h5>Add a heading to a category</h5>
    
    <div class="form-group">
        <label for="txtHeading" class="col-sm-3">Heading:</label>
        <asp:TextBox ID="txtHeading" runat="server" required="true" MaxLength="30" />
    </div>
    
     <div class="form-group">
        
<label class="col-sm-3">Category:</label>
        <asp:DropDownList DataValueField="Category_ID" DataTextField="Category1" ID="ddlCategory" runat="server" required="true" />
    </div>
    
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             />
    </div>
</asp:Content>
