<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SublayoutsTunerReferrers.aspx.cs" Inherits="Sitecore.CacheOptimizer.sitecore.admin.CacheOptimizer.SublayoutsTunerReferrers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Referrer list</h2>
		
		Source Item:
		<ul>
			<li>Item Name: <asp:Literal runat="server" ID="lItemName" /></li>
			<li>ID: <asp:Literal runat="server" ID="lItemId" /></li>
			<li>Path: <asp:Literal runat="server" ID="lItemPath" /></li>
		</ul>
		<br/>
		
		<asp:ListView runat="server" ID="ReferrersListView" GroupItemCount="30">
			<LayoutTemplate>	
				<table>
					<asp:PlaceHolder runat="server" id="groupPlaceholder"/>
				</table>
			</LayoutTemplate>
			
			<GroupTemplate>
				<tr></tr>
				<tr  style="background-color: darkgrey">
					<th>DatabaseName</th>					  
					<th>ItemID</th>
					<th>ItemLanguage</th>
					<th>ItemVersion</th>
					<th>Path</th>
					<th>Field</th>
				</tr>
			
				<asp:PlaceHolder runat="server" id="itemPlaceholder"/>
			</GroupTemplate>
			
			<ItemTemplate>
				<tr>
				  <td><%# Eval("DatabaseName") %></td>						  
				  <td><%# Eval("ItemID") %></td>
				  <td><%# Eval("ItemLanguage") %></td>
				  <td><%# Eval("ItemVersion") %></td>
				  <td><%# Eval("Path") %></td>
				  <td><%# Eval("FieldName") %></td>
				</tr>
			</ItemTemplate>
			<AlternatingItemTemplate>
			</AlternatingItemTemplate>
		</asp:ListView>
    </div>
    </form>
</body>
</html>
