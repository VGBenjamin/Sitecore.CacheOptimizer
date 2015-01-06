using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

using System.Diagnostics;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.UI.XamlSharp.Xaml;

namespace Sitecore.CacheOptimizer.sitecore.admin.CacheOptimizer.Controls
{
    public partial class SubLayoutCache : System.Web.UI.UserControl
    {
        private bool IsDeveloper
        {
            get
            {
                return Page.User.IsInRole(@"sitecore\developer") || Page.User.IsInRole(@"sitecore\sitecore client developing");
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sublayoutsItem = Sitecore.Data.Database.GetDatabase("master").GetItem("/sitecore/layout/sublayouts");
                var sublayoutsCollection = new List<Item>();

                ProcessSublayoutsCollection(sublayoutsItem, sublayoutsCollection);

                SublayoutsListView.DataSource = sublayoutsCollection;
                SublayoutsListView.DataBind();
            }
        }

        private void ProcessSublayoutsCollection(Item rootItem, List<Item> collection)
        {
            foreach (Item child in rootItem.Children)
            {
                if (child.TemplateID == new ID("{0A98E368-CDB9-4E1E-927C-8E0C24A003FB}"))
                {
                    collection.Add(child);
                }

                if (child.HasChildren)
                    ProcessSublayoutsCollection(child, collection);
            }
        }

        protected void SublayoutsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var linkDatabase = Sitecore.Configuration.Factory.GetLinkDatabase();

                var item = e.Item.DataItem as Item;

                var hiddenId = e.Item.FindControl("ItemId") as HiddenField;
                var litPath = e.Item.FindControl("ItemPath") as Literal;
                var cbDatasourceQuery = e.Item.FindControl("cbDatasourceQuery") as CheckBox;
                var cbCacheable = e.Item.FindControl("cbCacheable") as CheckBox;
                var cbClearOnIndex = e.Item.FindControl("cbClearOnIndex") as CheckBox;
                var cbVaryByData = e.Item.FindControl("cbVaryByData") as CheckBox;
                var cbVaryByRp = e.Item.FindControl("cbVaryByRP") as CheckBox;
                var cbVaryByQs = e.Item.FindControl("cbVaryByQS") as CheckBox;
                var cbVaryByDevice = e.Item.FindControl("cbVaryByDevice") as CheckBox;
                var cbVaryByLogin = e.Item.FindControl("cbVaryByLogin") as CheckBox;
                var cbVaryByUser = e.Item.FindControl("cbVaryByUser") as CheckBox;
                var hReferers = e.Item.FindControl("hReferers") as HyperLink;

                hiddenId.Value = item.ID.ToString();
                litPath.Text = item.Paths.FullPath.Replace("/sitecore/layout/Sublayouts/", "");
                cbDatasourceQuery.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["Enable Datasource Query"]).Checked;
                cbCacheable.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["Cacheable"]).Checked;
                cbClearOnIndex.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["ClearOnIndexUpdate"]).Checked;
                cbVaryByData.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["VaryByData"]).Checked;
                cbVaryByRp.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["VaryByParm"]).Checked;
                cbVaryByQs.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["VaryByQueryString"]).Checked;
                cbVaryByDevice.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["VaryByDevice"]).Checked;
                cbVaryByLogin.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["VaryByLogin"]).Checked;
                cbVaryByUser.Checked = ((Sitecore.Data.Fields.CheckboxField)item.Fields["VaryByUser"]).Checked;

                if (linkDatabase != null)
                {
                    hReferers.Text = linkDatabase.GetReferrerCount(item).ToString();
                    hReferers.Attributes["data-refer-id"] = item.ID.ToString();
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var db = Sitecore.Data.Database.GetDatabase("master");

            foreach (ListViewDataItem row in SublayoutsListView.Items)
            {
                if (row.ItemType == ListViewItemType.DataItem)
                {
                    var hiddenId = row.FindControl("ItemId") as HiddenField;
                    var cbDatasourceQuery = row.FindControl("cbDatasourceQuery") as CheckBox;
                    var cbCacheable = row.FindControl("cbCacheable") as CheckBox;
                    var cbClearOnIndex = row.FindControl("cbClearOnIndex") as CheckBox;
                    var cbVaryByData = row.FindControl("cbVaryByData") as CheckBox;
                    var cbVaryByRp = row.FindControl("cbVaryByRP") as CheckBox;
                    var cbVaryByQs = row.FindControl("cbVaryByQS") as CheckBox;
                    var cbVaryByDevice = row.FindControl("cbVaryByDevice") as CheckBox;
                    var cbVaryByLogin = row.FindControl("cbVaryByLogin") as CheckBox;
                    var cbVaryByUser = row.FindControl("cbVaryByUser") as CheckBox;

                    var item = db.GetItem(new ID(hiddenId.Value));

                    item.Editing.BeginEdit();
                    item["Enable Datasource Query"] = cbDatasourceQuery.Checked ? "1" : "0";
                    item["Cacheable"] = cbCacheable.Checked ? "1" : "0";
                    item["ClearOnIndexUpdate"] = cbClearOnIndex.Checked ? "1" : "0";
                    item["VaryByData"] = cbVaryByData.Checked ? "1" : "0";
                    item["VaryByParm"] = cbVaryByRp.Checked ? "1" : "0";
                    item["VaryByQueryString"] = cbVaryByQs.Checked ? "1" : "0";
                    item["VaryByDevice"] = cbVaryByDevice.Checked ? "1" : "0";
                    item["VaryByLogin"] = cbVaryByLogin.Checked ? "1" : "0";
                    item["VaryByUser"] = cbVaryByUser.Checked ? "1" : "0";
                    item.Editing.EndEdit();
                }
            }
        }
    }
}