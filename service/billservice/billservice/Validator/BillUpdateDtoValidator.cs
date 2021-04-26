using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models.Dto;
using FluentValidation;

namespace billservice.Validator
{
    public class BillUpdateDtoValidator : AbstractValidator<BillUpdateDto>
    {
        readonly IBillType billtype;


        public BillUpdateDtoValidator ( IBillType billtype )
        {
            this.billtype = billtype;

            CascadeMode = CascadeMode.Stop;







        }


    }
}
