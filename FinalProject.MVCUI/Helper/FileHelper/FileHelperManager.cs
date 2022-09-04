

namespace FinalProject.MVCUI
{
    public class FileHelperManager : IFileHelper
    {
        public string Add(IFormFile file, string root)
        {   
            if (!Directory.Exists(root))//Dosya dizini kontrol edilir.
                Directory.CreateDirectory(root);//Yoksa oluşturulur.

            string extension = Path.GetExtension(file.FileName); //Dosya uzantısını alıyoruz.
            string imageName = Guid.NewGuid().ToString() + extension; //Guid oluşrutup dosya uzantısı ile beraber veriyoruz.

            CheckFileSize(file);//Dosya boyutunu kontrol ediyoruz.

            CheckFileExtensionExists(extension);//Dosya Uzantısı konrol edilir.

            using (FileStream fileStream = File.Create(root + imageName))//File.Create(root + imageName) argüman olarak dosya yolunu ve adını bildirdik.
            {
                file.CopyTo(fileStream);//Dosyanın yüklenecegi akışı belirledik.
                fileStream.Flush(); //Dosyaya yazdırır.               
            }

            return imageName;
        }

        public void Delete(string filePath)
        {
            if (File.Exists(filePath))//Aynı isimli dosya varmı diye kontrol ediyoruz.
            {
                File.Delete(filePath);//Varsa siliyoruz.
            }
            else
                throw new Exception("Resim Bulunamadı");
        }

        public string Update(IFormFile file, string filePath, string root)
        {
            if (File.Exists(filePath))//Aynı isimli dosya varmı diye kontrol ediyoruz.
                File.Delete(filePath);

            CheckFileSize(file);//Dosya boyutunu kontrol ediyoruz.

          return Add(file, root);
        }

        //Dosya Uzantısı konrol ediyoruz.
        private static void CheckFileExtensionExists(string extension)
        {
            if (extension != ".jpeg" && extension != ".png" && extension != ".jpg")
                throw new Exception("Resim Uzantısı Yanlış");
        }

        private static void CheckFileSize(IFormFile file)
        {
            long size = file.Length;//Dosya boyutunu buluyoruz.
            if (size >= 409600)
                throw new Exception("Dosya boyutu 400kb geçmekte");
        }

    }

}
