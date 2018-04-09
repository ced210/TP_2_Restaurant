using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Utilities
{
    /// <summary>
    /// Author: Nicolas Chourot (all rights reserved)
    /// Give access of CRUD request to image file and automatic naming with a Global Unique Identifier
    /// </summary>
    public class ImageGUIDReference
    {
        /// <summary>
        /// Name with file extension (prefered .png) of the default image
        /// </summary>        
        public String DefaultImage { get; set; }
        /// <summary>
        /// Path of images folder
        /// </summary>        
        public String BasePath { get; set; }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="basePath">Folder path of all image files</param>
        /// <param name="defaultImage">Default image</param>
        public ImageGUIDReference(String basePath, String defaultImage)
        {
            this.BasePath = basePath;
            this.DefaultImage = defaultImage;
        }
        /// <summary>
        /// provides the URL of the image file
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        public String GetURL(String GUID)
        {
            String url;
            if (String.IsNullOrEmpty(GUID))
                url = "~" + BasePath + DefaultImage;
            else
                url = "~" + BasePath + GUID + ".png";
            return url;
        }
        /// <summary>
        /// Upload the image data from the HttpRequest to an image file
        /// </summary>
        /// <param name="Request">The HttpRequest that hold the image BLOB</param>
        /// <param name="PreviousGUID">Previous Global Unique Id</param>
        /// <returns>Global Unique Id</returns>
        public String UpLoadImage(HttpRequestBase Request, String PreviousGUID)
        {
            String GUID = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    if (!String.IsNullOrEmpty(PreviousGUID))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(GetURL(PreviousGUID)));
                    }
                    GUID = Guid.NewGuid().ToString();
                    file.SaveAs(HttpContext.Current.Server.MapPath(GetURL(GUID)));
                    return GUID;
                }
            }
            return PreviousGUID;
        }
        /// <summary>
        /// Remove the image file named GUID.png
        /// </summary>
        /// <param name="GUID"></param>
        public void Remove(String GUID)
        {
            if (!String.IsNullOrEmpty(GUID))
            {
                try
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(GetURL(GUID)));
                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
            }
        }
    }
}
