using FreeSql.DataAnnotations;



namespace billservice.models.spiapi.EMAIL.models.db
{
    /// <summary>
    /// EMAIL_SETTING模型
    /// </summary>
    public class EMAIL_SETTING
    {
        [Column(IsPrimary = true)]
        public int ID
        {
            get; set;
        }


        public string CATE
        {
            get; set;
        }


        public string ADDRESS_HOST
        {
            get; set;
        }


        public string ADDRESS_FROM
        {
            get; set;
        }


        public string ADDRESS_TO
        {
            get; set;
        }


        public string ADDRESS_CC
        {
            get; set;
        }


        public string SUBJECT
        {
            get; set;
        }


        public string SQL_CODE
        {
            get; set;
        }


        public int IS_ENABLE
        {
            get; set;
        }


        public string BODY_BEGIN
        {
            get; set;
        }


        public string BODY_END
        {
            get; set;
        }


        public string REPEAT_CATE
        {
            get; set;
        }


        public string CYCLE_TYPE
        {
            get; set;
        }

        public int CYCLE_NUMBER
        {
            get; set;
        }

        public string CYCLE_TIME
        {
            get; set;
        }



        /// <summary>
        /// 分钟(interval模式)
        /// </summary>
        [Column(IsIgnore = true)]
        public int CYCLE_NUMBER_MI_Interval
        {
            get; set;
        }


        /// <summary>
        /// cron表达式
        /// </summary>
        [Column(IsIgnore = true)]
        public string cron
        {
            get; set;
        }



        /// <summary>
        /// 描述
        /// </summary>
        [Column(IsIgnore = true)]
        public string remarks
        {
            get; set;
        }


        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(IsIgnore = true)]
        public bool IsValid
        {
            get; set;
        }
    }
}
