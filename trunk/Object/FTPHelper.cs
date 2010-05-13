using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace hwj.CommonLibrary.Object
{
    public class FTPHelper
    {
        public static void Upload(string ftpUrl, string user, string password, string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpUrl + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(user, password);

            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;

            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // 指定数据传输类型
            reqFTP.UseBinary = true;

            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = fileInf.Length;

            // 缓冲大小设置为2kb
            int buffLength = 2048;

            byte[] buff = new byte[buffLength];
            int contentLen;

            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();
            try
            {
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();

                // 每次读文件流的2kb
                contentLen = fs.Read(buff, 0, buffLength);

                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream
                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // 关闭两个流
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
