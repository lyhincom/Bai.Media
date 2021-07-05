using System;
using System.Linq;
using System.Threading.Tasks;
using Bai.Media.Web.ModelBinders.Base;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bai.Media.Web.ModelBinders
{
    public class ImageBinder : MediaBinder
    {
        public override Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            bindingContext.Result = ModelBindingResult.Success(new Image
            {
                UserId = GetGuid(bindingContext, "UserId"),
                PageId = GetGuid(bindingContext, "PageId"),
                PageType = GetPageType(bindingContext),
                FormImage = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault()
            });

            return Task.CompletedTask;
        }
    }
}
