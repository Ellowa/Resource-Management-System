using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class RequestModel
    {
        public int? Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string? Purpose { get; set; }

        public int ResourceId { get; set; }

        public int UserId { get; set; }

        public string? ResourceName { get; set; }
    }
}
