using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class UserUnsafeModel :UserProtectedModel
    {
        public List<RequestModel>? Requests { get; set; }

        public List<ScheduleModel>? Schedules { get; set; }

        public string? Password { get; set;}
    }
}
