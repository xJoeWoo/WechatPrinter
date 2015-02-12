﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WechatPrinter.Support
{
    public class FileUtils
    {

        private static string RootPath = AppDomain.CurrentDomain.BaseDirectory;
        private static string ResPath = RootPath + "res\\";
        public enum ResPathsEnum { PrintImg, AdImg, AdVid };
        public static string[] ResPaths = { ResPath + "print\\", ResPath + "ad\\img\\", ResPath + "ad\\vid\\" };
        public const int FOLDER_SIZE_LIMIT = 300;

        public static BitmapImage LoadImage(string filepath, int decodeWidth)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bm = new Bitmap(filepath);
            bm.Save(ms, ImageFormat.Png);
            bm.Dispose();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.DecodePixelWidth = decodeWidth;
            bi.EndInit();
            bi.Freeze();
            return bi;
        }

        public static bool CheckFile(ResPathsEnum path, string filename)
        {
            if (!Directory.Exists(ResPaths[(int)path]))
            {
                Directory.CreateDirectory(ResPaths[(int)path]);
                //Console.WriteLine("File folder doesn't exists, created: " + ResPaths[(int)path]);
                return false;
            }
            else if (!File.Exists(ResPaths[(int)path] + filename))
            {
                Console.WriteLine("File doesn't exists: " + filename);
                return false;
            }
            else
            {
                //Console.WriteLine("File exists: " + filename);
                return true;
            }
        }

        public static string SaveFile(Stream stream, ResPathsEnum path, string filename)
        {
            DeleteOldFiles(path);
            string filepath = ResPaths[(int)path];
            try
            {
                if (!CheckFile(path, filename))
                {
                    using (FileStream fs = File.Create(filepath + filename))
                    {
                        stream.CopyTo(fs);
                    }
                }
                return filepath + filename;
            }
            catch (Exception)
            {
                Console.WriteLine("[FileUtils: SaveFile Error]");
                throw;
            }

        }

        public static void DeleteOldFiles(ResPathsEnum path)
        {
            string filepath = ResPaths[(int)path];
            if (Directory.Exists(filepath))
            {
                long size = 0;
                FileInfo[] fis = new DirectoryInfo(filepath).GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }
                //Console.WriteLine(filepath + " size: {0:#0.00}MB", size / 1024d / 1024d);
                if (size > FOLDER_SIZE_LIMIT * 1024 * 1024)
                {
                    Array.Sort(fis, new FileCreateTimeComparer());
                    foreach (FileInfo fi in fis)
                    {
                        Console.WriteLine(filepath + " delete: " + fi.FullName + "\t" + fi.CreationTime);
                        size -= fi.Length;
                        fi.Delete();
                        if (size <= FOLDER_SIZE_LIMIT)
                            break;
                    }
                }
            }
        }

        class FileCreateTimeComparer : IComparer
        {
            int IComparer.Compare(Object o1, Object o2)
            {
                FileInfo fi1 = o1 as FileInfo;
                FileInfo fi2 = o2 as FileInfo;
                return fi1.CreationTime.CompareTo(fi2.CreationTime);
            }
        }

    }
}