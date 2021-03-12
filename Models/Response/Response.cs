using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class Response : IResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public bool Status { get; set; }

        public dynamic Result { get; set; }

        /// <summary>
        /// 登陆标识
        /// </summary>
        public string Token { get; set; }


        /// <summary>
        /// 默认返回操作成功
        /// </summary>
        public Response()
        {
            Code = (int)RetCode.SUCCESS;
            Message = "操作成功";
            Status = true;
        }

        /// <summary>
        /// 统一返回前端失败
        /// </summary>
        /// <param name="msg">失败消息</param>
        public void ResponseErr(string msg)
        {
            Code = (int)RetCode.FAIL;
            Message = msg;
            Status = false;
        }
    }

    public class Response<T> : Response
    {
        /// <summary>
        /// 回传的结果
        /// </summary>
        public T Result { get; set; }
    }

    public enum RetCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 200,

        /// <summary>
        /// 失败
        /// </summary>
        FAIL = 400,

        /// <summary>
        /// 未认证（签名错误）
        /// </summary>
        UNAUTHORIZED = 401,

        /// <summary>
        /// 找不到请求文件
        /// </summary>
        NoHandlerFoundException = 404,

        /// <summary>
        /// 未登录 
        /// </summary>
        UNAUTHEN = 401,

        /// <summary>
        /// 未授权，拒绝访问
        /// </summary>
        UNAUTHZ = 403,

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        INTERNAL_SERVER_ERROR = 500,

        /// <summary>
        /// 业务处理错误
        /// </summary>
        Business_ERROR = 600,

    }
}
