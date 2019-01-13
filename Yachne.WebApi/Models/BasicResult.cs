using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Yachne.Core.YachneEnums;

namespace Yachne.WebApi.Models
{
    /// <summary>
    /// 基本返回类型
    /// </summary>
    public class BasicResult
    {
        public BasicResult()
        {
            StatusCode = (int)WebApiStatusCode.Success;
            Success = true;
            Msg = "";
        }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Msg { get; set; }

        public void SetErrorResult(int statusCode, string msg)
        {
            SetResult(statusCode, false, msg);
        }

        public void SetResult(int statusCode, bool isSuccess, string msg)
        {
            StatusCode = statusCode;
            Success = isSuccess;
            Msg = msg;
        }
    }

    /// <summary>
    /// 基本返回类型<泛型>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasicResult<T> : BasicResult
    {
        public BasicResult(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}