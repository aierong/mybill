using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class BillUpdateDto
    {
        public int ids
        {
            get; set;
        }



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
