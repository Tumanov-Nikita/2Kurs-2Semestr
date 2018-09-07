﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCustomers.aspx.cs" Inherits="FabricWebView.FormCustomers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="Добавить" />
        <asp:Button ID="ButtonChange" runat="server" OnClick="ButtonChange_Click" Text="Изменить" />
        <asp:Button ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" Text="Удалить" />
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить" OnClick="ButtonUpd_Click" />
         <asp:GridView ID="dataGridView" runat="server"  ShowHeaderWhenEmpty="True" BackColor="White" BContractColor="#8B0000" BContractWidth="3px" CellPadding="4" GridLines="Horizontal" BContractStyle="Double">
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#8B0000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#8B0000" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFA07A" ForeColor="White" Font-Bold="True" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
    
        <br />
        <br />
        <asp:Button ID="ButtonBack" runat="server" Text="Вернуться" OnClick="ButtonBack_Click" />
    
    </div>
    </form>
</body>
</html>
