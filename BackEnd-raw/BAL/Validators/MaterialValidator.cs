using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BAL.Models;

namespace BAL.Validators
{
    public class MaterialValidatorForEdit : AbstractValidator<MaterialViewModel>
    {
        public MaterialValidatorForEdit()
        {
            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson material {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson material {CollectionIndex} {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .Length(2,50).WithMessage("Lesson material {CollectionIndex} {PropertyName} must has {MinLength}..{MaxLength} characters");
            //HyperLink
            RuleFor(s => s.HyperLink)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson material {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson material {CollectionIndex} {PropertyName} cannot be blank")
                    .When(s => s.HyperLink.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .Length(10,500).WithMessage("Lesson material {CollectionIndex} {PropertyName} must have {MinLength}..{MaxLength} characters");
            //Status
            RuleFor(s => s.Status)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Lesson material {CollectionIndex}")
                .ExclusiveBetween(0, 3).WithMessage("Lesson material {CollectionIndex} {PropertyName} must be between 0..3");
        }
    }
    public class MaterialValidatorForCreate : AbstractValidator<MaterialViewModel>
    {
        public MaterialValidatorForCreate()
        {
            //Id
            RuleFor(m => m.Id).Null().WithName("Material {PropertyName} must be null to create");
            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson material {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson material {CollectionIndex} {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .Length(2, 50).WithMessage("Lesson material {CollectionIndex} {PropertyName} must has {MinLength}..{MaxLength} characters");
            //Hyperlink
            RuleFor(s => s.HyperLink)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson material {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson material {CollectionIndex} {PropertyName} cannot be blank")
                    .When(s => s.HyperLink.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .Length(10, 500).WithMessage("Lesson material {CollectionIndex} {PropertyName} must have {MinLength}..{MaxLength} characters");
        }
    }
}
