using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string JwtRefreshToken { get; set; }

        public int RoleId { get; set; }

        public AdditionalRole Role { get; set; }

        public ICollection<Request> Requests { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
