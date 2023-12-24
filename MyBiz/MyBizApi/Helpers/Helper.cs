namespace MyBizApi.Helpers
{
    public class Helper
    {
        public async static Task<string> UploadFile(IFormFile imgFile)
        {
            string fileName = "";

            if (imgFile != null)
            {
                FileInfo fileInfo = new FileInfo(imgFile.FileName);

                fileName = imgFile.FileName + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\employeesImages");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
;
                var exactPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\employeesImages", fileName);
                using (FileStream fileStream = new FileStream(exactPath, FileMode.Create))
                {
                    await imgFile.CopyToAsync(fileStream);
                }
            }

            return fileName;
        }

        public async static Task<string> GetFileName(string rootPath, string folderName, IFormFile imageFile)
        {
            string fileName = imageFile.FileName.Length > 64 ? imageFile.FileName.Substring(imageFile.FileName.Length - 64, 64) : imageFile.FileName;
            fileName = Guid.NewGuid().ToString() + imageFile.FileName;
            string path = Path.Combine(rootPath, folderName, fileName);

            using (FileStream Stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(Stream);
            }

            return fileName;
        }
    }
}
