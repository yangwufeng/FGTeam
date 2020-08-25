using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BLLModel
{
    public static class BLLResultFactory
    {
        public static BLLResult Success(Object data, string msg)
        {
            return new BLLResult(true, msg, data);
        }
        public static BLLResult Success(Object data)
        {
            return new BLLResult(true, "", data);
        }

        public static BLLResult Success(string msg)
        {
            return new BLLResult(true, msg, null);
        }
        public static BLLResult Success()
        {
            return new BLLResult(true, "", null);
        }


        public static BLLResult Error(object data, string msg)
        {
            return new BLLResult(false, msg, data);
        }


        public static BLLResult Error(object data)
        {
            return new BLLResult(false, "", data);
        }
        public static BLLResult Error(string msg)
        {
            return new BLLResult(false, msg, null);
        }

        public static BLLResult Error()
        {
            return new BLLResult(false, "", null);
        }


        public static BLLResult Create(BLLResultCode code, Object data, string msg)
        {
            return new BLLResult(code, msg, data);
        }

        #region 泛型
        public static BLLResult<T> Create<T>(BLLResultCode code, T data, string msg)
        {
            return new BLLResult<T>(code, msg, data);
        }

        public static BLLResult<T> Success<T>(T data, String msg)
        {
            return new BLLResult<T>(true, msg, data);
        }

        public static BLLResult<T> Success<T>(String msg)
        {
            return new BLLResult<T>(true, msg, default(T));
        }
        public static BLLResult<T> Success<T>(T data)
        {
            return new BLLResult<T>(true, "", data);
        }

        public static BLLResult<T> Success<T>()
        {
            return new BLLResult<T>(true, "", default(T));
        }

        public static BLLResult<T> Error<T>(T data, string msg)
        {
            return new BLLResult<T>(false, msg, data);
        }
        public static BLLResult<T> Error<T>(T data)
        {
            return new BLLResult<T>(false, "", data);
        }
        public static BLLResult<T> Error<T>(string msg)
        {
            return new BLLResult<T>(false, "", default(T));
        }

        #endregion


    }
    public class BLLResultFactory<T>
    {
        public static BLLResult Create(BLLResultCode code, Object data, string msg)
        {
            return new BLLResult(code, msg, data);
        }
        public static BLLResult<T> Success<T>(T data, String msg)
        {
            return new BLLResult<T>(true, msg, data);
        }
        public static BLLResult<T> Success(T data)
        {
            return new BLLResult<T>(true, "", data);
        }

        public static BLLResult<T> Success(String msg)
        {
            return new BLLResult<T>(true, msg, default(T));
        }

        public static BLLResult<T> Success()
        {
            return new BLLResult<T>(true, "", default(T));
        }
        public static BLLResult<T> Error(T data, String msg)
        {
            return new BLLResult<T>(false, msg, data);
        }

        public static BLLResult<T> Error(T data)
        {
            return new BLLResult<T>(false, "", data);
        }

        public static BLLResult<T> Error(String msg)
        {
            return new BLLResult<T>(false, msg, default(T));
        }

        public static BLLResult<T> Error()
        {
            return new BLLResult<T>(false, "", default(T));
        }
    }
}
