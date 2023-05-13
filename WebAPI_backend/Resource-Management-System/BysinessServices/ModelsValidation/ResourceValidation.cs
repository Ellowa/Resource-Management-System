using BysinessServices.Models;
using FluentValidation;

namespace BysinessServices.ModelsValidation
{
    public class ResourceValidation : AbstractValidator<ResourceModel>
    {
        public ResourceValidation() 
        { 
            RuleFor(r => r.Name).Length(4, 50);
            RuleFor(r => r.SerialNumber).Length(3, 15);
            RuleFor(r => r.Id).Null();
            RuleFor(r => r.ResourceTypeName).Null();
        }
    }
}
