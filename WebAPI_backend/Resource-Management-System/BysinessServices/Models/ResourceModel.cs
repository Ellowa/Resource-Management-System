using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class ResourceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int ResourceTypeId { get; set; }

        public string ResourceTypeName { get; set; }

        public List<ScheduleModel> Schedules { get; set; }

        public List<RequestModel> Requests { get; set; }
    }
}
