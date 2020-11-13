using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace COMMON.Tools
{
    public static class ImageUploader
    {
        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {
            if (file != null) // eğer dosya varsa
            {
                Guid uniqueName = Guid.NewGuid(); // uniq bir isim oluştur
                serverPath = serverPath.Replace("~", string.Empty); // gelen dosya yolundan ~ işaretini ayıkla
                string[] fileArray = file.FileName.Split('.'); // dosya isminden . yı ayıkla
                string extension = fileArray[fileArray.Length - 1].ToLower(); // dosya uzantısını bul
                string fileName = $"{uniqueName}.{extension}"; // yeni dosya ismini ve uzantısını ver
                
                if (extension == "jpeg" || extension == "gif" || extension == "png" || extension == "jpg") // eğer yerni dosya uzantısı jpeg,gif,png,jpg ise
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath+fileName))) // ve aynı dosyadan varmı yokmu yol doğrulaması yap 
                    {
                        return "1";//Dosya Zaten Var
                    }
                    else // eğer yoksa 
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName); // kaydedilecek doysa yolu ve dosya ismini al
                        file.SaveAs(filePath); // ilgili dizine kaydet
                        return serverPath + fileName; 
                    }
                }
                else
                {
                    return "2";//seçilen resim değil
                }
            }
            else
            {
                return "3";//Dosya boş
            }
        }
    }
}
