using CourseWork.Helper;
using LaptopWebSite.Core;
using LaptopWebSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace LaptopWebSite.Controllers
{
    public class TestCropperController : Controller
    {
        // GET: TestCropper
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult UploadBase64(string base64image)
        {
            string filename = Guid.NewGuid().ToString() + ".jpg";
            string imageBig = Server.MapPath(Constants.ProductImagePath) + filename;
            string json = null;
            try
            {
                // The Complete method commits the transaction. If an exception has been thrown,
                // Complete is not  called and the transaction is rolled back.
                Bitmap imgCropped = base64image.FromBase64StringToBitmap();
                var saveImage = ImageWorker.CreateImage(imgCropped, 300, 300);
                if (saveImage == null)
                    throw new Exception("Error save image");

                saveImage.Save(imageBig, ImageFormat.Jpeg);

                //var saveImageIcon = ImageWorker.CreateImage(imgCropped, 32, 32);
                //if (saveImageIcon == null)
                //    throw new Exception("Error save image");
                //saveImageIcon.Save(imageSmall, ImageFormat.Jpeg);

                //var productImage = new ProductImage { FileName = filename };
                //_context.ProductImages.Add(productImage);
                //_context.SaveChanges();

                json = JsonConvert.SerializeObject(new
                {
                    imagePath = Url.Content(Constants.ProductImagePath) + filename,
                    id = 0//productImage.Id
                });

            }
            catch (Exception)
            {
                json = JsonConvert.SerializeObject(new
                {
                    imagePath = ""
                });
                //if (System.IO.File.Exists(imageSmall))
                //{
                //    System.IO.File.Delete(imageSmall);
                //}
                if (System.IO.File.Exists(imageBig))
                {
                    System.IO.File.Delete(imageBig);
                }
            }
            return Content(json, "application/json");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult DeleteImageAjax(int id)
        {
            string json = JsonConvert.SerializeObject(new
            {
                success = false
            });
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //ProductImage productImage = _context.ProductImages.Find(id);
                    //if (productImage != null)
                    //{
                    //    string filename = productImage.FileName;
                    //    _context.ProductImages.Remove(productImage);
                    //    _context.SaveChanges();
                    //    string imageBig = Server.MapPath(Constants.ProductImagePath) + filename;
                    //    string imageSmall = Server.MapPath(Constants.ProductThumbnailPath) + filename;
                    //    if (System.IO.File.Exists(imageSmall))
                    //    {
                    //        System.IO.File.Delete(imageSmall);
                    //    }
                    //    if (System.IO.File.Exists(imageBig))
                    //    {
                    //        System.IO.File.Delete(imageBig);
                    //    }
                    //    json = JsonConvert.SerializeObject(new
                    //    {
                    //        success = true
                    //    });
                    //}
                    //// The Complete method commits the transaction. If an exception has been thrown,
                    //// Complete is not  called and the transaction is rolled back.
                    //scope.Complete();
                }
            }
            catch
            {
            }
            return Content(json, "application/json");
        }
    }
}