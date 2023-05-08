using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class UserWithAuthInfoModel : UserProtectedModel
    {
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? JwtRefreshToken { get; set; }
    }
}
