﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.Models
{
    public class JwtPairModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
