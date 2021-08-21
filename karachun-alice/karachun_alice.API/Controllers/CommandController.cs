using AutoMapper;
using AutoMapper.Collection;
using AutoMapper.EntityFrameworkCore;
using karachun_alice.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yandex.Alice.Sdk.Models;
using karachun_alice.BI.Interfaces;

namespace karachun_alice.API.Controllers
{
    [ApiController]
    [Route("alice/[Controller]")]
    public class CommandController : BaseController
    {
        private readonly ILogger<CommandController> _logger;
        private readonly IMapper _mapper;
        private readonly ICommands _commands;

        public CommandController(ILogger<CommandController> logger, IMapper mapper, ICommands commands)
        {
            _logger = logger;
            _mapper = mapper;
            _commands = commands;
        }

        [HttpPost]
        public async Task<IActionResult> PostCommand([FromBody] AliceRequest model) =>
             Ok(await _commands.Execute(model.Session.New ? "старт" : model.Request.Command));
    }
}
