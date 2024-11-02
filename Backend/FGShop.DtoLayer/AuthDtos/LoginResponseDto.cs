using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.AuthDtos
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public UserLoginDto? User { get; set; }
    }
}
