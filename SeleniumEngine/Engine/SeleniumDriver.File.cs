using CoreLibrary.Configuration;
using CoreLibrary.Controls;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : IFile
    {
        /// <summary>
        /// Gets the first file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public FileInfo GetFirstFile(string path)
        {

            var directory = new DirectoryInfo(path);
            var myFile = directory.GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .First();
            return myFile;

        }


        /// <summary>
        /// Gets the name of recently downloaded file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string GetNameOfRecentlyDownloadedFile(string path)
        {
            return GetFirstFile(path).Name;
        }


        public string GetFileExtension(string fileName)
        {
            try
            {

                string[] extension = fileName.Split('.');
                return extension[1];

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }



        public void WaitForFileToDownload(string fileCount)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (string.Equals(NumberOfFilesInDownloadFolder(),
                        fileCount))
                    {
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Helper, WaitForFileToDownload , Not able to wait for file to download " + e.Message + e.StackTrace);

            }
        }

        /// <summary>
        /// Numbers of files in download folder.
        /// </summary>
        /// <returns>string - number of files</returns>
        public string NumberOfFilesInDownloadFolder()
        {
            try
            {
                return Directory.GetFiles(new AppConfigReader().GetDownloadFilePath(), "*", SearchOption.TopDirectoryOnly).Length.ToString();

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Exception in Method : NumberOfFilesInDownloadFolder in GenericHelper Class";
            }
        }

      
    }
}
