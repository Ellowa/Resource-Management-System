using BysinessServices.Models;
using FluentValidation;

namespace BysinessServices.ModelsValidation
{
    public class ResourceValidator : AbstractValidator<ResourceModel>
    {
        public ResourceValidator() 
        { 
            RuleFor(r => r.Name).Length(4, 50);
            RuleFor(r => r.SerialNumber).Length(3, 15);
            RuleFor(r => r.ResourceTypeName).Null();
        }
    }
}
