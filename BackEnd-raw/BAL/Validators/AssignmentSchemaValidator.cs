using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Models;
using FluentValidation;

namespace BAL.Validators
{
    public class AssignmentSchemaValidator : AbstractValidator<AssignmentSchemaViewModel>
    {
        public AssignmentSchemaValidator()
        {
            //PercentQuiz
            RuleFor(s => s.PercentQuiz)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");
            //PercentAssignment
            RuleFor(s => s.PercentAssigment)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");
            //PercentFinal
            RuleFor(s => s.PercentFinal)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");
            //PercentTheory
            RuleFor(s => s.PercentTheory)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");
            //PercentFinalPractice
            RuleFor(s => s.PercentFinalPractice)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");
            //PassingCriteria
            RuleFor(s => s.PassingCriterial)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");
        }
    }
}
