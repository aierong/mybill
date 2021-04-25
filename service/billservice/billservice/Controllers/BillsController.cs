using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using billservice.interfaces;
using billservice.models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace billservice.Controllers
{
    [Authorize]
    [Route( "api/bills" )]
    [ApiController]
    public class BillsController : Base.BaseController
    {

        private readonly IMapper mapper;
        readonly IBill Ibill;

        public BillsController ( IBill Ibill , IMapper mapper )
        {
            this.Ibill = Ibill;
            this.mapper = mapper;
        }



        // POST api/<BillController>
        [HttpPost]
        public void add ( [FromBody] BillDto billTypeDto )
        {


        }




    }
}
