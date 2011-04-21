using System;
using System.Collections.Generic;
using System.Text;

namespace hwj.CommonLibrary.Object
{
    public class ErrorMsg
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

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
        /// 是否错误
        /// </summary>
        public bool IsError { get; set; }
        Error _FirstError = null;
        /// <summary>
        /// 获取首个Error对象
        /// </summary>
        public Error FirstError
        {
            get
            {
                if (ErrorList != null && ErrorList.Count > 0)
                {
                    return ErrorList[0];
                }
                else
                {
                    return null;
                }
            }
            set { _FirstError = value; }
        }
        List<Error> _ErrorList = new List<Error>();
        /// <summary>
        /// 错误列表
        /// </summary>
        public List<Error> ErrorList
        {
            get { return _ErrorList; }
            set
            {
                _ErrorList = value;
                if (_ErrorList != null && _ErrorList.Count > 0)
                {
                    IsError = true;
                }
                else
                {
                    IsError = false;
                }
            }
        }

        public ErrorMsg()
        {
            IsError = false;
            Ext1 = string.Empty;
            Ext2 = string.Empty;
            Ext3 = string.Empty;
            Version = string.Empty;
            ErrorList = new List<Error>();
        }

        #region Public Function
        public void Add(string message)
        {
            Add(string.Empty, message);
        }
        public void Add(string code, string message)
        {
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(message))
                return;

            if (ErrorList == null)
                ErrorList = new List<Error>();
            ErrorList.Add(new Error(code, message));

            IsError = ErrorList.Count > 0;
        }

        public ErrorMsg FromXml(string xml)
        {
            return SerializationHelper.FromXml<ErrorMsg>(xml);
        }
        public string ToXml()
        {
            return SerializationHelper.SerializeToXml(this);
        }
        #endregion

        #region Error Class
        public class Error
        {
            public string Code { get; set; }
            public string Message { get; set; }

            public Error()
            {
                Code = string.Empty;
                Message = string.Empty;
            }

            public Error(string code, string message)
            {
                Code = code;
                Message = message;
            }

            public string ToString()
            {
                return string.Format("{0}-{1}", Code, Message);
            }
        }
        #endregion
    }

}
