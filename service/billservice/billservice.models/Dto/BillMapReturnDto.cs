using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class BillMapReturnDto
    {
        public int billtypeid
        {
            get; set;
        }

        public string typename
        {
            get; set;
        }

        public string avatar
        {
            get; set;
        }

        public decimal moneys
        {
            get; set;
        }

        public decimal ratio
        {
            get; set;
        }
    }
}
