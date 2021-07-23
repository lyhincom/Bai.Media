using System;
using System.Linq;
using System.Threading.Tasks;
using Bai.Media.Web.ModelBinders.Base;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Http;
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
                PageId = GetGuid(bindingContext, "PageId"),
                PageType = GetPageType(bindingContext),
                FormImage = GetFile(bindingContext)
            });

            return Task.CompletedTask;
        }

        public IFormFile GetFile(ModelBindingContext bindingContext)
        {
            var file = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault();
            // Note: content-type can be missing if the File is HTTP reposted

            return file;
        }
    }
}
