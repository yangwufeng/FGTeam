using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.APIModel
{
    public class credentials
    {

        /// <summary>
        /// IDF图片编码
        /// </summary>
        public string idfCode { get; set; }

        /// <summary>
        /// 管线号/图纸号
        /// </summary>
        public string lineNo { get; set; }

        /// <summary>
        /// 证书名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 证书号
        /// </summary>
        public string certificateNo { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public string isNG { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark { get; set; }


        public int flag { get; set; }

    }
}
