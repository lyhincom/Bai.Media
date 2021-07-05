using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bai.Media.Web.ModelBinders.Base
{
    public abstract class MediaBinder : IModelBinder
    {
        public abstract Task BindModelAsync(ModelBindingContext bindingContext);

        protected Guid GetGuid(ModelBindingContext bindingContext, string propertyName)
        {
            var guidIdString = bindingContext.ValueProvider.GetValue(propertyName).FirstOrDefault();
            Guid.TryParse(guidIdString, out var guidId);
            if (guidId == default)
            {
                throw new ArgumentNullException(propertyName);
            }

            return guidId;
        }

        protected string GetPageType(ModelBindingContext bindingContext)
        {
            var pageType = bindingContext.ValueProvider.GetValue("PageType").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(pageType))
            {
                throw new ArgumentNullException("PageType");
            }

            return pageType;
        }
    }
}
