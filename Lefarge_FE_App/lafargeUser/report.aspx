﻿<%@ Page Title="" Language="C#" MasterPageFile="~/report.Master" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="Lefarge_FE_App.admin.report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="well">
        <h3>Report for:</h3>
        <asp:Label runat="server" For="txtEqUn">Unit #</asp:Label>
          <asp:TextBox ID="txtEqUn" runat="server" Enabled="false"></asp:TextBox>
         <asp:Label runat="server" For="txtEqID">Unit #</asp:Label>
        <asp:TextBox ID="txtEqID" runat="server" Enabled="false"></asp:TextBox>
    </div>
    <asp:DropDownList runat="server" ID="ddlDates" AutoPostBack="true"   OnSelectedIndexChanged="ddlDates_SelectedIndexChanged" OnDataBinding="ddlDates_DataBinding"></asp:DropDownList>
    <asp:GridView ID="grdResults" runat="server" CssClass="table table-striped"
        AutoGenerateColumns="False"
        DataKeyNames="ID"  OnRowDataBound="grdResults_RowDataBound" >
        <Columns>        
            <asp:BoundField  DataField="Question_ID" visible="true"  HeaderText="Question" HeaderStyle-Width="300"/>
            <asp:BoundField DataField="Response" HeaderText="Response"/>
            <asp:BoundField DataField="deficiency_defect" HeaderText="deficiency/defect"/>
            <asp:BoundField DataField="Action_plan" HeaderText="Action Plan"/>
             <asp:BoundField DataField="heading_ID" HeaderText="For Heading" />
             <asp:BoundField DataField="Date_Completed" HeaderText="Date Completed"/>
        </Columns>
    </asp:GridView>
</asp:Content>
