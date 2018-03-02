using Microsoft.AspNet.Identity;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myapi.Controllers
{
    public class TinyMceController : Controller
    {
        // GET: TinyMce
        [HttpPost]
        [Authorize(Roles = "Editor")]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            var location = SaveFile(file);
            return Json(new { location }, JsonRequestBehavior.AllowGet);
        }


        private string SaveFile(HttpPostedFileBase file)
        {// Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("businesslawyer");
            container.CreateIfNotExists();
            //将容器设置为公共容器
            container.SetPermissions(
                new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            string path = "editor";
            if (User != null)
            {
                path = User.Identity.GetUserId().Substring(0, 18);
            }

            string time = DateTime.Now.ToString("yyMMdd");
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(path + "/" + time + "/" + file.FileName);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = file.InputStream)
            {
                blockBlob.UploadFromStream(fileStream);
            }
            var location = "https://pulang.blob.core.chinacloudapi.cn/businesslawyer/" + path + "/" + time + "/" + file.FileName;
            return location;
        }
    }
}