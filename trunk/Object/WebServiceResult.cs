using System;
using System.Collections.Generic;
using System.Text;

namespace hwj.CommonLibrary.Object
{
    public class WebServiceResult
    {
        /// <summary>
        /// 扩展参数1
        /// </summary>
        public string Ext1 { get; set; }
        /// <summary>
        /// 扩展参数2
        /// </summary>
        public string Ext2 { get; set; }
        /// <summary>
        /// 扩展参数3
        /// </summary>
        public string Ext3 { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 是否错误
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// 错误编号
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        public WebServiceResult()
        {

            Ext1 = string.Empty;
            Ext2 = string.Empty;
            Ext3 = string.Empty;
            Version = string.Empty;
            IsError = false;
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
        }
        public WebServiceResult FromXml(string xml)
        {
            return SerializationHelper.FromXml<WebServiceResult>(xml);
        }
        public string ToXml()
        {
            return SerializationHelper.SerializeToXml(this);
        }

    }
}
