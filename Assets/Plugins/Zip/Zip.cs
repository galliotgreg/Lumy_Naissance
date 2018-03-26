using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using Ionic.Zip;
using System.Text;
using System.IO;

namespace Zip_Tool
{
    public class ZipUtil
    {
        public static void Unzip(string zipFilePath, string location)
        {
            Directory.CreateDirectory(location);
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                zip.ExtractAll(location, ExtractExistingFileAction.OverwriteSilently);
            }
        }

        public static void Zip(string folder_name, string destination_path, params string[] files)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (string file in files)
                {
                    zip.AddFile(file, "");
                }
                zip.Save(destination_path + "\\" + folder_name + ".zip");


            }
        }
    }
}

