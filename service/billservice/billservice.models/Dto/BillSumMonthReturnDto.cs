using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class BillSumMonthReturnDto
    {
        public decimal moneys
        {
            get; set;
        }

        public int moneyyear
        {
            get; set;
        }


        public int moneymonth
        {
            get; set;
        }

    }
}
