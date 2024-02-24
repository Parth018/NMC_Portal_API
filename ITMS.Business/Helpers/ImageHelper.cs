using ITMS.Business.Extensions;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ITMS.Business.Helpers
{
    public class ImageHelper
    {
        public static IHtmlString Base64ImageFor<TModel>(HtmlHelper<TModel> html, byte[] imageBytes)
        {
            var base64ImageContent = Convert.ToBase64String(imageBytes ?? new byte[] { });
            return new ServiceStack.MiniProfiler.HtmlString(string.Format("data:image/jpeg;base64,{0}", base64ImageContent));
        }

        public Guid SaveImage(IFormFile inputStream, string folder, Guid? identifier = null)
        {
            return SaveImage(inputStream, folder, 800, 600, identifier);
        }

        public Guid SaveImage(IFormFile inputStream, string folder, int maxWidth, int maxHeight, Guid? identifier = null)
        {
            using (var input = Image.FromStream(inputStream.OpenReadStream()))
            {
                using (var image = input.Resize(maxWidth, maxHeight))
                {
                    if (identifier == null || identifier == Guid.Empty)
                    {
                        identifier = Guid.NewGuid();
                    }
                    else
                    {
                        DeleteImage(folder, identifier ?? new Guid());
                    }
                    CheckAndCreateDirectory($"{FilePath(folder)}");
                    SaveFile(image.ConvertToJpeg(75), $"{FilePath(folder)}{identifier}.full.jpg");
                    SaveFile(image.ConvertToThumbnail(), $"{FilePath(folder)}{identifier}.thumbnail.jpg");
                    return identifier.Value;
                }
            }
        }

        private bool CheckAndCreateDirectory(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool SaveFile(Stream fileStream, string pathAndFilename, string contentType = null)
        {
            try
            {
                Image img = Image.FromStream(fileStream);
                img.Save(pathAndFilename);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DeleteImage(string folder, Guid identifier)
        {
            // Delete full version
            DeleteFile(folder, identifier.ToString(), "full.jpg");

            // Delete thumbnail
            DeleteFile(folder, identifier.ToString(), "thumbnail.jpg");
        }

        private void DeleteFile(string folder, string fileName, string fileExtension)
        {
            DeleteFile($"{FilePath(folder)}{fileName}.{fileExtension}");
        }

        public bool DeleteFile(string pathAndFilename)
        {
            try
            {
                if (File.Exists(pathAndFilename))
                {
                    File.Delete(pathAndFilename);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string FilePath(string folder)
        {
            if (!folder.StartsWith("\\"))
            {
                folder = "\\" + folder;
            }

            if (!folder.EndsWith("\\"))
            {
                folder += "\\";
            }

            //return HttpContext.Current.Server.MapPath("~/Upload/" + folder + "/");
            return string.Empty;
        }

        public string GetFullPath(string folder, Guid? identifier)
        {
            return $"{ConfigurationManager.AppSettings["ImageUrl"] + "/"}{"Upload/"}{(folder) + "/"}{identifier}.full.jpg" + "?" + new Random().Next(99999);
        }

        public string GetThumbnailPath(string folder, Guid? identifier)
        {
            return $"{ConfigurationManager.AppSettings["ImageUrl"] + "/"}{"Upload/"}{(folder) + "/"}{identifier}.thumbnail.jpg" + "?" + new Random().Next(99999);
        }

        public string GetDefaultThumbnailPath()
        {
            return $"{ConfigurationManager.AppSettings["ImageUrl"] + "/"}{"Upload/"}Default.thumbnail.jpg" + "?" + new Random().Next(99999);
        }

        public string GetDefaultFullPath()
        {
            return $"{ConfigurationManager.AppSettings["ImageUrl"] + "/"}{"Upload/"}Default.full.jpg" + "?" + new Random().Next(99999);
        }
    }
}
