using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BAL.Models;

namespace BAL.Validators
{
    public class LessonValidatorForEdit : AbstractValidator<LessonViewModel>
    {

        public LessonValidatorForEdit()
        {
            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("The {PropertyName} of lesson {CollectionIndex} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .Length(2,50).WithMessage("Lesson {PropertyName} must has between {MinLength}..{MaxLength} character");
            //Duration
            RuleFor(s => s.Duration)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("The {PropertyName} of lesson {CollectionIndex} cannot be blank")
                .GreaterThan(0).WithMessage("The {PropertyName} of lesson {CollectionIndex} must larger than 0 (minutes)");
            //IdDeliveryType
            RuleFor(s => s.IdDeliveryType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank");
            //IdFormatType
            RuleFor(s => s.IdFormatType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank");
            //IdOutputStandard
            RuleFor(s => s.IdOutputStandard)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank");
            //Status
            RuleFor(s => s.Status)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Lesson {CollectionIndex}")
                .ExclusiveBetween(0, 3).WithMessage("Lesson {CollectionIndex} {PropertyName} must be between 0..3");
            //Material
            RuleFor(s => s.Materials)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Lesson {CollectionIndex}")
                .NotEmpty().WithMessage("Please add at least 1 {PropertyName} for Lesson {CollectionIndex}");
            RuleForEach(s => s.Materials)
                .SetValidator(new MaterialValidatorForEdit());
        }
    }
    public class LessonValidatorForCreate : AbstractValidator<LessonViewModel>
    {
        public LessonValidatorForCreate()
        {
            //Id
            RuleFor(l => l.Id).Null().WithName("Lesson Id (index {CollectionIndex})");
            //DeliveryType
            RuleFor(l => l.DeliveryType).Null().WithName("Lesson Delivery Type (index {CollectionIndex})");
            //FormatType
            RuleFor(l => l.FormatType).Null().WithName("Lesson Format Type (index {CollectionIndex})");
            //OutputStandard
            RuleFor(l => l.OutputStandard).Null().WithName("Lesson Output Standard (index {CollectionIndex})");
            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .Length(2, 50).WithMessage("Lesson {PropertyName} must has between {MinLength}..{MaxLength} character");
            //Duration
            RuleFor(s => s.Duration)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("The {PropertyName} of lesson {CollectionIndex} cannot be blank")
                .GreaterThan(0).WithMessage("The {PropertyName} of lesson {CollectionIndex} must larger than 0 (minutes)");
            //IdLevel
            RuleFor(s => s.IdDeliveryType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank");
            //IdFormatType
            RuleFor(s => s.IdFormatType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank");
            //IdOutputStandard
            RuleFor(s => s.IdOutputStandard)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank")
                .NotEmpty().WithMessage("Lesson {CollectionIndex} {PropertyName} cannot be blank");
            //Lessons
            RuleForEach(s => s.Materials)
                .SetValidator(new MaterialValidatorForCreate());
        }
    }
}
