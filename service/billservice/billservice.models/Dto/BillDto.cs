namespace billservice.models.Dto
{
    public class BillDto
    {
        public int billtypeid
        {
            get; set;
        }

        public bool isout
        {
            get; set;
        }

        public decimal moneys
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string moneydate
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string memo
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool isadd
        {
            get; set;
        }

        /// <summary>
        /// 记录id (修改模式才传递值)
        /// </summary>
        public int ids
        {
            get; set;
        }


    }
}
