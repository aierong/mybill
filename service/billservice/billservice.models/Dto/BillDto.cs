using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public string moneydate
        {
            get; set;
        }


        public string memo
        {
            get; set;
        }
    }
}
