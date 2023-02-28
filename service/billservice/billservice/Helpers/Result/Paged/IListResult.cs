using System.Collections.Generic;

namespace billservice.Helpers.Result.Paged
{
    public interface IListResult<T>
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        IReadOnlyList<T> Item
        {
            get; set;
        }
    }
}
