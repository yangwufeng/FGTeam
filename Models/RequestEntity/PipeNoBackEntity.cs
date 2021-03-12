using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestEntity
{
 public    class PipeNoBackEntity
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
        /// 单管号
        /// </summary>
        public string singlePipe { get; set; }



        /// <summary>
        /// 管段号
        /// </summary>
        public string pieceNo { get; set; }



        /// <summary>
        /// 物料编码
        /// </summary>
        public string materialCode { get; set; }

    }
}
