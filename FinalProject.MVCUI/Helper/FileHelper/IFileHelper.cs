﻿
namespace FinalProject.MVCUI
{

    public interface IFileHelper
    {
        string Add(IFormFile file, string root);

        void Delete(string filePath);

        string Update(IFormFile file, string filePath, string root);
    }

}
