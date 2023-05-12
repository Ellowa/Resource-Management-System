using BysinessServices.Models;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BysinessServices.ModelsValidation
{
    public class RequestValidator : AbstractValidator<RequestModel> 
    {
        public RequestValidator() 
        { 
            RuleFor(r => r.Start).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(r => r.End).GreaterThan(r => r.Start);
            RuleFor(r => r.Purpose).MinimumLength(5).When(r => !r.Purpose.IsNullOrEmpty());
            RuleFor(r => r.Id).Null();
            RuleFor(r => r.ResourceName).Null();
        }
    }
}
