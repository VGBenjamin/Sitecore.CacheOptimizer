<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubLayoutCache.ascx.cs" Inherits="Sitecore.CacheOptimizer.sitecore.admin.CacheOptimizer.Controls.SubLayoutCache" %>

<div id="subLayoutReferersDialog" style="display: none">
    <iframe id="subLayoutRefererIframe" src="" style="width: 100%; height: 100%"></iframe>
</div>

<asp:ListView runat="server" ID="SublayoutsListView" OnItemDataBound="SublayoutsListView_ItemDataBound" GroupItemCount="30">
	<LayoutTemplate>	
		<table>
			<asp:PlaceHolder runat="server" id="groupPlaceholder"/>
		</table>
	</LayoutTemplate>
			
	<GroupTemplate>
		<tr></tr>
		<tr  style="background-color: darkgrey">
			<th>Sublayout path</th>
			<th>Enable Datasource Query</th>
			<th>Cacheable</th>
			<th>Clear on Index Update</th>
			<th>VaryByData</th>
			<th>VaryByRenderingParam</th>
			<th>VaryByQueryString</th>
			<th>VaryByDevice</th> 
			<th>VaryByLogin</th>
			<th>VaryByUser</th>
			<th>References</th>
		</tr>
			
		<asp:PlaceHolder runat="server" id="itemPlaceholder"/>
	</GroupTemplate>
			
	<ItemTemplate>
		<tr>
			<td><asp:Literal runat="server" ID="ItemPath"/><asp:HiddenField runat="server" ID="ItemId"/></td>
			<td><asp:CheckBox runat="server" ID="cbDatasourceQuery"/></td>
			<td><asp:CheckBox runat="server" ID="cbCacheable"/></td>
			<td><asp:CheckBox runat="server" ID="cbClearOnIndex"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByData"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByRP"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByQS"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByDevice"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByLogin"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByUser"/></td>
			<td><asp:HyperLink runat="server" ID="hReferers" CssClass="referersDetails" NavigateUrl="..\#" /></td>
		</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
		<tr style="background-color: lightgrey">
			<td><asp:Literal runat="server" ID="ItemPath"/><asp:HiddenField runat="server" ID="ItemId"/></td>
			<td><asp:CheckBox runat="server" ID="cbDatasourceQuery"/></td>
			<td><asp:CheckBox runat="server" ID="cbCacheable"/></td>
			<td><asp:CheckBox runat="server" ID="cbClearOnIndex"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByData"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByRP"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByQS"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByDevice"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByLogin"/></td>
			<td><asp:CheckBox runat="server" ID="cbVaryByUser"/></td>
			<td><asp:HyperLink runat="server" ID="hReferers" CssClass="referersDetails" NavigateUrl="..\#" /></td>
		</tr>
	</AlternatingItemTemplate>
</asp:ListView>

<asp:Button runat="server" Text="Save" OnClick="btnSave_OnClick"/>