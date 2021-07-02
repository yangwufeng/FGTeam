using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BLLModel
{
    public static class BllResultFactory
    {
        public static BllResult Success(Object data, string msg)
        {
            return new BllResult(true, msg, data);
        }
        public static BllResult Success(Object data)
        {
            return new BllResult(true, "", data);
        }

        public static BllResult Success(string msg)
        {
            return new BllResult(true, msg, null);
        }
        public static BllResult Success()
        {
            return new BllResult(true, "", null);
        }


        public static BllResult Error(object data, string msg)
        {
            return new BllResult(false, msg, data);
        }


        public static BllResult Error(object data)
        {
            return new BllResult(false, "", data);
        }
        public static BllResult Error(string msg)
        {
            return new BllResult(false, msg, null);
        }

        public static BllResult Error()
        {
            return new BllResult(false, "", null);
        }


        public static BllResult Create(BllResultCode code, Object data, string msg)
        {
            return new BllResult(code, msg, data);
        }

        #region 泛型
        public static BllResult<T> Create<T>(BllResultCode code, T data, string msg)
        {
            return new BllResult<T>(code, msg, data);
        }

        public static BllResult<T> Success<T>(T data, String msg)
        {
            return new BllResult<T>(true, msg, data);
        }

        public static BllResult<T> Success<T>(String msg)
        {
            return new BllResult<T>(true, msg, default(T));
        }
        public static BllResult<T> Success<T>(T data)
        {
            return new BllResult<T>(true, "", data);
        }

        public static BllResult<T> Success<T>()
        {
            return new BllResult<T>(true, "", default(T));
        }

        public static BllResult<T> Error<T>(T data, string msg)
        {
            return new BllResult<T>(false, msg, data);
        }
        public static BllResult<T> Error<T>(T data)
        {
            return new BllResult<T>(false, "", data);
        }
        public static BllResult<T> Error<T>(string msg)
        {
            return new BllResult<T>(false, "", default(T));
        }

        #endregion


    }
    public class BllResultFactory<T>
    {
        public static BllResult Create(BllResultCode code, Object data, string msg)
        {
            return new BllResult(code, msg, data);
        }
        public static BllResult<T> Success<T>(T data, String msg)
        {
            return new BllResult<T>(true, msg, data);
        }
        public static BllResult<T> Success(T data)
        {
            return new BllResult<T>(true, "", data);
        }

        public static BllResult<T> Success(String msg)
        {
            return new BllResult<T>(true, msg, default(T));
        }

        public static BllResult<T> Success()
        {
            return new BllResult<T>(true, "", default(T));
        }
        public static BllResult<T> Error(T data, String msg)
        {
            return new BllResult<T>(false, msg, data);
        }

        public static BllResult<T> Error(T data)
        {
            return new BllResult<T>(false, "", data);
        }

        public static BllResult<T> Error(String msg)
        {
            return new BllResult<T>(false, msg, default(T));
        }

        public static BllResult<T> Error()
        {
            return new BllResult<T>(false, "", default(T));
        }
    }
}
