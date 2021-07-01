using System;
using System.Linq;
using System.Threading.Tasks;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bai.Media.Web.ModelBinders
{
    public class AvatarBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var userIdString = bindingContext.ValueProvider.GetValue("UserId").FirstOrDefault();
            Guid.TryParse(userIdString, out var userId);
            if (userId == default)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            bindingContext.Result = ModelBindingResult.Success(new Avatar
            {
                UserId = userId,
                FormImage = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault()
            });

            return Task.CompletedTask;
        }
    }
}
