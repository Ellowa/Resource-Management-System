using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class UserModel
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public List<RequestModel>? Requests { get; set; }

        public List<ScheduleModel>? Schedules { get; set; }
    }
}
