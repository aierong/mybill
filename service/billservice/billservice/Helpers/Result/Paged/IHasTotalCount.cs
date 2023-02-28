namespace billservice.Helpers.Result.Paged
{
    public interface IHasTotalCount
    {
        /// <summary>
        /// 总数
        /// </summary>
        int Total
        {
            get; set;
        }
    }
}
