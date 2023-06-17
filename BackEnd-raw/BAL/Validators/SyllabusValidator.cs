using BAL.Models;
using FluentValidation;

namespace BAL.Validators
{
    public class SyllabusValidatorForEdit : AbstractValidator<SyllabusViewModel>
    {
        public SyllabusValidatorForEdit()
        {

            //ID
            RuleFor(s => s.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please enter an Id for Syllabus")
                .NotNull().WithMessage("Please enter an Id for Syllabus");

            RuleFor(s => s.Id).GreaterThan(0).WithMessage("The syllabus id must be larger than 0");

            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");

            //Code
            RuleFor(s => s.Code)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .NotEmpty().WithMessage("Syllabus code cannot be empty");

            //Attendee Number
            RuleFor(s => s.AttendeeNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");

            //Version
            RuleFor(s => s.Version)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The syllabus version is currently empty")
                .Must(x => x >= 1).WithMessage("Syllabus version must larger than 1");

            //Technical Requirement
            RuleFor(s => s.Technicalrequirement)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");

            //CourseObjectives
            RuleFor(s => s.CourseObjectives)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");

            //Status
            RuleFor(s => s.Status)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(0, 3).WithMessage("{PropertyName} must be between 0..3");

            //TrainingPrinciple
            RuleFor(s => s.TrainingPrinciple)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");

            //IdLevel
            RuleFor(s => s.IdLevel)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .NotEmpty().WithMessage("Please choose a level for this syllabus");

            //LevelName
            RuleFor(s => s.LevelName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .NotEmpty().WithMessage("Please choose a level for this syllabus");

            //AssignmentSchema
            RuleFor(s => s.AssignmentSchema)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please set up an {PropertyName} for this syllabus")
                .NotEmpty().WithMessage("Please set up an {PropertyName} for this syllabus");
            RuleFor(s=>s.AssignmentSchema).SetValidator(new AssignmentSchemaValidator());

            //Sessions
            RuleFor(s => s.Sessions)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .NotEmpty().WithMessage("Please add at least 1 session for this syllabus");
            RuleForEach(syllabus => syllabus.Sessions)
                .SetValidator(new SessionValidatorForEdit());

        }
    }

    public class SyllabusIdValidator : AbstractValidator<long>
    {
        public SyllabusIdValidator()
        {
            RuleFor(i => i)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Null Id input!")
                .GreaterThan(0).WithMessage("Id is an integer greater than 0!");
        }
    }
    public class SyllabusValidatorForCreate : AbstractValidator<SyllabusViewModel>
    {
        public SyllabusValidatorForCreate()
        {
            //ID
            RuleFor(s => s.Id).Null().WithName("Syllabus {PropertyName} must be null to create");
            //Version
            RuleFor(s => s.Version).Null().WithName("Syllabus {PropertyName} must be null to create");
            //Status
            RuleFor(s => s.Status).Null().WithName("Syllabus {PropertyName} must be null to create");
            //LevelName
            RuleFor(s => s.LevelName).Null().WithName("Syllabus {PropertyName} must be null to create");
            //HistorySyllabus
            RuleFor(s => s.HistorySyllabus).Null().WithName("Syllabus {PropertyName} must be null to create");
            //Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");
            //Code
            RuleFor(s => s.Code)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .NotEmpty().WithMessage("Syllabus code cannot be empty");
            //AttendeeNumber
            RuleFor(s => s.AttendeeNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .ExclusiveBetween(1, 100).WithMessage("Syllabus {PropertyName} must be between {From} and {To}");
            RuleFor(s => s.AssignmentSchema).SetValidator(new AssignmentSchemaValidator());
            //Technical Requirement
            RuleFor(s => s.Technicalrequirement)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");

            //CourseObjectives
            RuleFor(s => s.CourseObjectives)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");
            //TrainingPrinciple
            RuleFor(s => s.Technicalrequirement)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Syllabus {PropertyName} must not be blank")
                    .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");

            //IdLevel
            RuleFor(s => s.IdLevel)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .NotEmpty().WithMessage("Please choose a level for this syllabus");
            //AssignmentSchema
            RuleFor(s => s.AssignmentSchema)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please set up an {PropertyName} for this syllabus")
                .NotEmpty().WithMessage("Please set up an {PropertyName} for this syllabus");

            //Sessions
            RuleFor(s => s.Sessions)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter a {PropertyName} for Syllabus")
                .NotEmpty().WithMessage("Please add at least 1 session for this syllabus");
            RuleForEach(s => s.Sessions)
                .SetValidator(new SessionValidatorForCreate());
        }
    }
}
