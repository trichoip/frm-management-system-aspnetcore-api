using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BAL.Models;

namespace BAL.Validators
{
    public class SessionValidatorForEdit : AbstractValidator<SessionViewModel>
    {
        public SessionValidatorForEdit()
        {
            //Name
            RuleFor(session => session.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Session {CollectionIndex} {PropertyName} must not be blank")
                .Length(2, 500).WithMessage("Session {CollectionIndex} {PropertyName} must be between {MinLength}..{MaxLength} characters");
            //Status
            RuleFor(s => s.Status)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Session {CollectionIndex}")
                .ExclusiveBetween(0, 3).WithMessage("Session {CollectionIndex} {PropertyName} must be between 0..3");
            //Unit
            RuleFor(s => s.Units)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Session {CollectionIndex}")
                .NotEmpty().WithMessage("Please add at least 1 {PropertyName} for Session {CollectionIndex}");
            RuleForEach(s => s.Units).SetValidator(new UnitValidatorForEdit());

        }
    }
    public class SessionValidatorForCreate : AbstractValidator<SessionViewModel>
    {
        public SessionValidatorForCreate()
        {
            //Id
            RuleFor(s => s.Id).Null().WithName("Session {PropertyName} {CollectionIndex} must be null to create");
            //Name
            RuleFor(session => session.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Length(2, 500).WithMessage("Session {CollectionIndex} {PropertyName} must be between {MinLength}..{MaxLength} characters")
                .NotNull().WithMessage("Session {CollectionIndex} {PropertyName} must not be blank");
            //Index
            RuleFor(s => s.Index)
                .GreaterThanOrEqualTo(1).WithMessage("Session {CollectionIndex} {PropertyName} must be greater than or equal to {ComparisonValue}");
            //Units
            RuleForEach(s => s.Units)
                .SetValidator(new UnitValidatorForCreate());
        }
    }
}
