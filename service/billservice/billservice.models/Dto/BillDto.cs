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



        public bool isadd
        {
            get;set;
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
