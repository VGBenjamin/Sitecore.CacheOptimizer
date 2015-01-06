using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web;

namespace Sitecore.CacheOptimizer.sitecore.admin.CacheOptimizer.SPEAK.Controllers
{
    public class CacheOptimizerController : System.Web.Mvc.Controller
    {
        public JsonResult GetSublayoutsList()
        {
            var filter = WebUtil.GetFormValue("input");

            var sublayoutsItem = Sitecore.Data.Database.GetDatabase("master")
                .GetItem("/sitecore/layout/sublayouts");

            var sublayoutsCollection = new List<Item>();

            ProcessSublayoutsCollection(sublayoutsItem, sublayoutsCollection);
            var linkDatabase = Sitecore.Configuration.Factory.GetLinkDatabase();

            var ret = from s in sublayoutsCollection
                select new
                {
                    ID = s.ID,
                    Name = s.Name,
                    DisplayName = s.DisplayName,
                    Path = s.Paths.FullPath.Replace("/sitecore/layout/Sublayouts/", ""),
                    DatasourceQuery = ((Sitecore.Data.Fields.CheckboxField) s.Fields["Enable Datasource Query"]).Checked,
                    Cacheable = ((Sitecore.Data.Fields.CheckboxField) s.Fields["Cacheable"]).Checked,
                    ClearOnIndex = ((Sitecore.Data.Fields.CheckboxField) s.Fields["ClearOnIndexUpdate"]).Checked,
                    VaryByData = ((Sitecore.Data.Fields.CheckboxField) s.Fields["VaryByData"]).Checked,
                    VaryByRP = ((Sitecore.Data.Fields.CheckboxField) s.Fields["VaryByParm"]).Checked,
                    VaryByQS = ((Sitecore.Data.Fields.CheckboxField) s.Fields["VaryByQueryString"]).Checked,
                    VaryByDevice = ((Sitecore.Data.Fields.CheckboxField) s.Fields["VaryByDevice"]).Checked,
                    VaryByLogin = ((Sitecore.Data.Fields.CheckboxField) s.Fields["VaryByLogin"]).Checked,
                    VaryByUser = ((Sitecore.Data.Fields.CheckboxField) s.Fields["VaryByUser"]).Checked,
                    Referers = linkDatabase != null ? linkDatabase.GetReferrerCount(s) : 0
                };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                ret = ret.Where(s => s.Name.Contains(filter) || s.DisplayName.Contains(filter));
            }

            return Json(ret);
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
    }
}