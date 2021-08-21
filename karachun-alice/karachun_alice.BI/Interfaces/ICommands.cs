using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using karachun_alice.Data.Dto;
using Yandex.Alice.Sdk.Models;

namespace karachun_alice.BI.Interfaces
{
    public interface ICommands
    {
        Task<AliceResponseDto> Execute(string command);
    }
}
