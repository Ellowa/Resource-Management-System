using BysinessServices.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.ModelsValidation
{
    public class ResourceTypeValidator : AbstractValidator<ResourceTypeModel>
    {
        public ResourceTypeValidator()
        {
            RuleFor(r => r.TypeName).Length(4, 50);
        }
    }
}
