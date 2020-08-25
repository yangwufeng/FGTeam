using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BLLModel
{
    public class BLLResult<T>
    {
        public BLLResult() { }

        public BLLResultCode Code { get; set; }
        public T Data { get; set; }
        public string Msg { get; set; }

        #region 判断
        public bool Success
        {
            get { return Code == BLLResultCode.OK ? true : false; }
            set
            {
                if (value)
                {
                    Code = BLLResultCode.OK;
                }
                else
                {
                    Code = BLLResultCode.Fail;
                }
            }
        }
        #endregion

        public BLLResult(bool success, string msg, T data)
        {
            Success = success;
            Msg = msg;
            Data = data;
        }
        public BLLResult(BLLResultCode code, string msg, T data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }

    }

    public class BLLResult
    {
        public BLLResult() { }
        public string Msg { get; set; }
        public object Data { get; set; }
        public BLLResultCode Code { get; set; }

        #region 判断
        public bool Success
        {
            get { return Code == BLLResultCode.OK ? true : false; }
            set
            {
                if (value)
                {
                    Code = BLLResultCode.OK;
                }
                else
                {
                    Code = BLLResultCode.Fail;
                }
            }
        }
        #endregion

        public BLLResult(bool success, string msg, object data)
        {
            Success = success;
            Msg = msg;
            Data = data;
        }
        public BLLResult(BLLResultCode code, string msg, object data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
    }
}

