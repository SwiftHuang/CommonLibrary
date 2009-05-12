using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace hwj.CommonLibrary
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
    }
}
