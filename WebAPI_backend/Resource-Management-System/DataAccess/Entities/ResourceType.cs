using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class ResourceType : BaseEntity
    {
        public string TypeName { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
