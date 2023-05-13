using BysinessServices.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.ModelsValidation
{
    internal class RoleValidation : AbstractValidator<RoleModel>
    {
        public RoleValidation()
        {
            RuleFor(r => r.Name).Length(4, 50);
            RuleFor(r => r.Id).Null();
        }
    }
}
