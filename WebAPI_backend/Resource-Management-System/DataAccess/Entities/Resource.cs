using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Resource : BaseEntity
    {
        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int ResourceTypeId { get; set; }

        public ResourceType ResourceType { get; set; }

        public ICollection<Schedule> Schedules { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
