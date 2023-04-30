﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class ResourceTypeModel
    {
        public int? Id { get; set; }

        public string TypeName { get; set; }

        public List<ResourceModel>? Resources { get; set; }
    }
}
