using System;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Exceptions.Base;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Services.ValidationServices
{
    public class FormFileValidationService : IFormFileValidationService
    {
        private const long ImageMinimumBytesSize = 512;
        private const long ImageMaximumBytesSize = 800000; // 0.8MB

        public void ValidateFormFile(IFormFile formImage)
        {
            ValidateFormFileFileExtension(formImage);
            ValidateFormFileByteSize(formImage);
        }

        private void ValidateFormFileFileExtension(IFormFile formImage)
        {
            if (formImage.ContentType == "image/png" ||
                formImage.ContentType == "image/jpg" ||
                formImage.ContentType == "image/jpeg" ||
                formImage.ContentType == "image/gif")
            {
                throw new MediaValidationException("Allowed image ContentTypes: png, jpg, jpeg, gif.");
            }
        }

        private void ValidateFormFileByteSize(IFormFile formImage)
        {
            if (formImage.Length < ImageMinimumBytesSize)
            {
                throw new MediaValidationException($"Image byte size is too small to upload. Current byte size is {formImage.Length} is less than accepted minimum {ImageMinimumBytesSize}.");
            }

            if (formImage.Length > ImageMaximumBytesSize)
            {
                throw new MediaValidationException($"Image byte size is too large to upload. Current byte size is {formImage.Length} is greater than accepted maximum {ImageMaximumBytesSize}.");
            }
        }
    }
}
