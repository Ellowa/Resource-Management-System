using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Schedule : BaseEntity
    {
        public DateTime Start {  get; set; }

        public DateTime End { get; set; }

        public string Purpose { get; set; }

        public int ResourceId { get; set; }

        public Resource Resource { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
