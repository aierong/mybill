using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class BillSumDayReturnDto
    {
        public int moneyday
        {
            get; set;
        }

        public int moneymonth
        {
            get; set;
        }

        public int moneyyear
        {
            get; set;
        }

        public decimal moneys
        {
            get; set;
        }
    }
}
