using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.IO.Compression;

namespace hwj.CommonLibrary.Object
{
    public class FileHelper
    {
        public static bool RegisterFile(string fileName, bool displayMessage)
        {
            try
            {
                if (displayMessage)
                    System.Diagnostics.Process.Start("regsvr32.exe", fileName);
                else
                    System.Diagnostics.Process.Start("regsvr32.exe", "/s " + fileName);
                return true;
            }
            catch { return false; }
        }
        public static bool UnRegisterFile(string fileName, bool displayMessage)
        {
            try
            {
                if (displayMessage)
                    System.Diagnostics.Process.Start("regsvr32.exe", "/u " + fileName);
                else
                    System.Diagnostics.Process.Start("regsvr32.exe", "/s /u " + fileName);
                return true;
            }
            catch { return false; }
        }

        public static DataSet TxtToDataSet(string fileName, bool changeRegistry)
        {
            try
            {
                if (changeRegistry)
                {
                    RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE", true).OpenSubKey("Microsoft", true);
                    RegistryKey jet = rk.OpenSubKey("Jet", true).OpenSubKey("4.0", true);
                    RegistryKey Engines = jet.OpenSubKey("Engines", true);
                    RegistryKey Text = Engines.OpenSubKey("Text", true);
                    if (Text.GetValue("Format").ToString() != "TabDelimited")
                        Text.SetValue("Format", "TabDelimited");
                }
                FileInfo fi = new FileInfo(fileName);
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + fi.DirectoryName + "\";Extended Properties='text;HDR=NO;FMT=TabDelimited';";
                OleDbConnection conn = new OleDbConnection(strConn);
                OleDbDataAdapter oada = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", fi.Name), conn);
                DataSet ds = new DataSet();
                oada.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet ExcelToDataSet(string path, string selectCommand, bool hasHeader)
        {
            try
            {
                string strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX=1'",
                            path,
                            hasHeader ? "YES" : "NO");
                string select = string.IsNullOrEmpty(selectCommand) ? "select * from [Sheet1$]" : selectCommand;
                OleDbConnection conn = new OleDbConnection(strConn);
                OleDbDataAdapter oada = new OleDbDataAdapter(select, strConn);
                DataSet ds = new DataSet();
                oada.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<string> ReadFileList(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                List<string> lines = new List<string>();
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                return lines;
            }
        }
        public static string ReadFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }


        /* - - - - - - - - - - - - - - - - - - - - - - - - 
         * Stream 和 文件之间的转换
         * - - - - - - - - - - - - - - - - - - - - - - - */
        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static void CreateFile(string fileName)
        {
            CreateFile(fileName, null);
        }
        public static void CreateFile(string fileName, string text)
        {
            if (!File.Exists(fileName))
            {
                string directory = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                else
                    using (File.Create(fileName)) { }
            }
            if (!string.IsNullOrEmpty(text))
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(text);
                }
            }
        }

        public static MemoryStream StreamToMemoryStream(Stream stream)
        {
            Byte[] buffer = hwj.CommonLibrary.Object.FileHelper.StreamToBytes(stream);
            return BytesToMemoryStream(buffer);
        }
        public static MemoryStream StringToMemoryStream(string data)
        {
            Byte[] buffer = Encoding.UTF8.GetBytes(data);
            return BytesToMemoryStream(buffer);
        }
        public static MemoryStream BytesToMemoryStream(Byte[] buffer)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream zipStream = new GZipStream(ms, CompressionMode.Compress, true);
            zipStream.Write(buffer, 0, buffer.Length);
            zipStream.Close();
            ms.Position = 0;

            return ms;
        }
    }
}
