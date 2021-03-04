using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileHelper
{
    public class FileHelper
    {
        public static string Add(IFormFile file)
        {
            var destPath = NewPath(file);

            try
            {
                var sourcePath = Path.GetTempFileName();

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(sourcePath, FileMode.Create))
                        file.CopyTo(stream);
                }

                File.Move(sourcePath, destPath);
                Console.WriteLine("File added");
                return destPath;
            }

            catch(Exception exception)
            {
                return exception.Message;
            }
        }

        public static string Update(string sourcePath, IFormFile file)
        {
            var destPath = NewPath(file);

            try
            {
                if (sourcePath.Length > 0)
                {
                    using (var stream = new FileStream(destPath, FileMode.Create))
                        file.CopyTo(stream);
                }
                File.Delete(sourcePath);
            }

            catch(Exception exception)
            {
                return exception.Message;
            }

            return destPath;
        }

        public static IResult Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch(Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        private static string NewPath(IFormFile file)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file.FileName);
            var fileExtension = fileInfo.Extension;

            string path = Environment.CurrentDirectory + @"\wwwroot\Images";
            string destPath = Guid.NewGuid().ToString() + fileExtension;

            var result = $@"{path}\{destPath}"; 
            return result;
        }
    }
}
