using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUploaderDemo
{
    public class FileDataInfo
    {
        public string fileId { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// 文件的总长度
        /// </summary>
        public Int64 fileSize { get; set; }
        /// <summary>
        /// 文件偏移量开始点
        /// </summary>
        public int fileStart { get; set; }
        /// <summary>
        /// 文件偏移量结束点
        /// </summary>
        public int fileEnd { get; set; }
    }
}