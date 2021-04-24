using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models.Dto;
using FluentValidation;

namespace billservice.Validator
{
    public class BillDtoValidator : AbstractValidator<BillDto>
    {
        public BillDtoValidator ()
        {

        }

    }
}
