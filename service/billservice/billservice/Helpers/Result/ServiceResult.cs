using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace billservice.Helpers.Result
{

    /// <summary>
    /// 
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceResult ()
        {
            // 给几个默认值

            //Code = ServiceResultCode.Succeed;

            Message = string.Empty;

            Success = true;
        }



        ///// <summary>
        ///// 响应码
        ///// </summary>
        //public ServiceResultCode Code
        //{
        //    get; set;
        //}



        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message
        {
            get; set;
        }



        /// <summary>
        /// 成功
        /// </summary>
        public bool Success
        {
            //get
            //{
            //    return Code == ServiceResultCode.Succeed;
            //}

            get; set;
        }




        //public long Timestamp { get; } = ( DateTime.Now.ToUniversalTime().Ticks - 621355968000000000 ) / 10000;



        /// <summary>
        /// 时间戳(毫秒)
        /// </summary>
        public long TimestampUtc
        {
            get
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , 0 );

                return Convert.ToInt64( ts.TotalMilliseconds );
            }
        }



        /// <summary>
        /// 时间戳(毫秒)
        /// </summary>
        public long Timestamp
        {
            get
            {
                TimeSpan ts = DateTime.Now - new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , 0 );

                return Convert.ToInt64( ts.TotalMilliseconds );
            }
        }



        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="message"></param>
        public void IsSuccess ( string message = "" )
        {
            Message = message;

            //Code = ServiceResultCode.Succeed;

            Success = true;
        }



        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message"></param>
        public void IsFailed ( string message = "" )
        {
            Message = message;

            //Code = ServiceResultCode.Failed;
            Success = false;
        }



        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="exception"></param>
        public void IsFailed ( Exception exception )
        {
            Message = exception.InnerException?.StackTrace;

            //Code = ServiceResultCode.Failed;
            Success = false;
        }
    }






}
