using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BAL.Models;

namespace BAL.Validators
{
    public class UnitValidatorForEdit : AbstractValidator<UnitViewModel>
    {
        public UnitValidatorForEdit()
        {
            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Unit {CollectionIndex} {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Unit {CollectionIndex} {PropertyName} must not be blank")
                .Length(2, 50).WithMessage("Unit {PropertyName} must be between {MinLength}..{MaxLength} characters");
            //Status
            RuleFor(s => s.Status)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Unit {CollectionIndex}")
                .ExclusiveBetween(0, 3).WithMessage("Unit {CollectionIndex} {PropertyName} must be between 0..3");
            //Lesson
            RuleFor(s => s.Lessons)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Unit {CollectionIndex}");
            RuleForEach(s => s.Lessons).SetValidator(new LessonValidatorForEdit());
        }
    }
    public class UnitValidatorForCreate : AbstractValidator<UnitViewModel>
    {
        public UnitValidatorForCreate()
        {
            //Id
            RuleFor(u => u.Id).Null().WithName("Unit {CollectionIndex} {PropertyName} must be null to create");
            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Unit {CollectionIndex} {PropertyName} must not be blank")
                .NotEmpty().WithMessage("Unit {CollectionIndex} {PropertyName} must not be blank")
                    .When(s=>s.Name.Trim().Equals(""),ApplyConditionTo.CurrentValidator)
                .Length(2,50).WithMessage("Unit {PropertyName} must has {MinLength}..{MaxLength} characters");
            //Index
            RuleFor(u => u.Index)
                .GreaterThanOrEqualTo(1).WithMessage("Unit {CollectionIndex} {PropertyName} must be greater than or equal to {ComparisonValue}");
            //Lessons
            RuleForEach(s => s.Lessons)
                .SetValidator(new LessonValidatorForCreate());
        }
    }
}
