﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class RoleModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<UserProtectedModel>? Users { get; set; }
    }
}
