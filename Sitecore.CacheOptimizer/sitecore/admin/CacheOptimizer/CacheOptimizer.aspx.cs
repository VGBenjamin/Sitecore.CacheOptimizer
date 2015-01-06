using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Diagnostics;

namespace Sitecore.CacheOptimizer.sitecore.admin.CacheOptimizer
{
    public partial class CacheOptimizer : System.Web.UI.Page
    {
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

        }
    }
}