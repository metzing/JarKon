using System;
using System.IO;

namespace JarKon.Core
{
    class FileManager
    {
        public static string LoadText(string fileName)
        {
            try
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, fileName);
                return System.IO.File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                App.Current.MainPage.DisplayAlert("Error reading file " + fileName, e.Message, "OK");
                return "";
            }
        }

        public static void SaveText(string fileName, string text)
        {
            try
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, fileName);
                System.IO.File.WriteAllText(filePath, text);
            }
            catch (Exception e)
            {
                App.Current.MainPage.DisplayAlert("Error saving file " + fileName, e.Message, "OK");
            }
        }
    }
}
