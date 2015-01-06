using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Data.Managers;
using Sitecore.Globalization;

namespace Sitecore.CacheOptimizer.sitecore.admin.CacheOptimizer
{
    public partial class SublayoutsTunerReferrers : System.Web.UI.Page
    {
        protected struct ItemRef
        {
            public string DatabaseName { get; set; }
            public string ItemID { get; set; }
            public string ItemLanguage { get; set; }
            public string ItemVersion { get; set; }
            public string Path { get; set; }
            public string FieldName { get; set; }
        }


        protected override void OnInit(EventArgs arguments)
        {
            Assert.ArgumentNotNull(arguments, "arguments");
            CheckSecurity(true);
        }

        protected void CheckSecurity(bool isDeveloperAllowed)
        {
            if (Sitecore.Context.User.IsAdministrator || (isDeveloperAllowed && this.IsDeveloper)) return;
            var site = Sitecore.Context.Site;

            if (site != null)
            {
                base.Response.Redirect(string.Format("{0}?returnUrl={1}", site.LoginPage, HttpUtility.UrlEncode(base.Request.Url.PathAndQuery)));
            }
        }

        private bool IsDeveloper
        {
            get
            {
                return User.IsInRole(@"sitecore\developer") || User.IsInRole(@"sitecore\sitecore client developing");
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var masterDb = Sitecore.Data.Database.GetDatabase("master");

                var referrerToItem = masterDb.GetItem(new ID(HttpContext.Current.Request.QueryString["id"]));

                if (referrerToItem != null)
                {
                    lItemName.Text = referrerToItem.Name;
                    lItemId.Text = referrerToItem.ID.ToString();
                    lItemPath.Text = referrerToItem.Paths.FullPath;

                    var linkDatabase = Sitecore.Configuration.Factory.GetLinkDatabase();

                    List<ItemRef> referencesList = new List<ItemRef>();

                    foreach (var lang in LanguageManager.GetLanguages(masterDb))
                    {
                        using (new LanguageSwitcher(lang))
                        {
                            var localReferItem = masterDb.GetItem(referrerToItem.ID);
                            var refItems = from si in linkDatabase.GetReferrers(localReferItem)
                                           select new
                                           {
                                               Item = si.GetSourceItem(),
                                               FieldId = si.SourceFieldID
                                           };

                            referencesList.AddRange(from i in refItems
                                                     select new ItemRef
                                                     {
                                                         DatabaseName = i.Item.Database.Name,
                                                         ItemID = i.Item.ID.ToString(),
                                                         ItemLanguage = i.Item.Language.Name,
                                                         ItemVersion = i.Item.Version.ToString(),
                                                         Path = i.Item.Paths.FullPath,
                                                         FieldName = i.Item.Fields[i.FieldId].Name
                                                     });
                        }
                    }

                    ReferrersListView.DataSource = referencesList;
                    ReferrersListView.DataBind();
                }
            }
        }
    }
}