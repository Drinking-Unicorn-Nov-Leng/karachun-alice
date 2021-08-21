using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex.Alice.Sdk.Models;

namespace karachun_alice.Data.Dto
{
    public class AliceResponseDto
    {
        public string Text { get; set; }

        public List<AliceButtonModel> Buttons { get; set; }
    }
}
