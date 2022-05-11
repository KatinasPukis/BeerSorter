using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Foundation.Core.Extentions
{
    public static class TemplateItemExtention
    {
        public static bool IsDerived(this TemplateItem template, ID templateId)
        {
            return template.ID == templateId || template.BaseTemplates.Any(baseTemplate => IsDerived(baseTemplate, templateId));
        }
        public static bool IsItemDerived(this Item item, ID templateId)
        {
            return item.TemplateID == templateId || item.Template.BaseTemplates.Any(baseTemplate => IsDerived(baseTemplate, templateId));
        }

    }
}