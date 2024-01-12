using System.Collections.Generic;
using billservice.Helpers.Result.Paged;



namespace billservice.Helpers.Result
{
    public class PagedList<T> : ListResult<T>, IPagedList<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total
        {
            get; set;
        }

        public PagedList ()
        {
        }

        public PagedList ( int total , IReadOnlyList<T> result ) : base( result )
        {
            Total = total;
        }
    }
}
