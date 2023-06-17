using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using BAL.Models;

namespace BAL.Validators
{
  public class ClassValidatorForEdit : AbstractValidator<UpdateClassViewModel>
  {
    public ClassValidatorForEdit()
    {
      //ID
      RuleFor(s => s.Id)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty().WithMessage("Please enter an Id for Class")
          .NotNull().WithMessage("Please enter an Id for Class");

      RuleFor(s => s.Id).GreaterThan(0).WithMessage("The Class id must be larger than 0");
      //Name
      RuleFor(s => s.Name)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty().WithMessage("Class {PropertyName} must not be blank")
              .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");
      //StartTimeLearning
      RuleFor(s => s.StartTimeLearning)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class StartTimeLearning cannot be empty");
      //EndTimeLearing
      RuleFor(s => s.EndTimeLearing)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class StartTimeLearning cannot be empty");
      //ReviewedBy
      RuleFor(s => s.ReviewedBy)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class ReviewedBy cannot be empty");

      RuleFor(s => s.ReviewedBy).GreaterThan(0).WithMessage("The Class id must be larger than 0");
      //ReviewedOn
      RuleFor(s => s.ReviewedOn)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class ReviewedOn cannot be empty");
      //ApprovedBy
      RuleFor(s => s.ApprovedBy)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class ApprovedBy cannot be empty");

      RuleFor(s => s.ApprovedBy).GreaterThan(0).WithMessage("The Class id must be larger than 0");
      //ApprovedOn
      RuleFor(s => s.ApprovedOn)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class ApprovedOn cannot be empty");
      //PlannedAtendee
      RuleFor(s => s.PlannedAtendee)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
         .ExclusiveBetween(0, 100).WithMessage("Class {PropertyName} must be between {From} and {To}");
      //ActualAttendee
      RuleFor(s => s.ActualAttendee)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .ExclusiveBetween(0, 100).WithMessage("Class {PropertyName} must be between {From} and {To}");
      //AcceptedAttendee
      RuleFor(s => s.AcceptedAttendee)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .ExclusiveBetween(0, 100).WithMessage("Class {PropertyName} must be between {From} and {To}");
      //CurrentSession
      RuleFor(s => s.CurrentSession)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
         .ExclusiveBetween(0, 100).WithMessage("Class {PropertyName} must be between {From} and {To}");
      //StartYear
      RuleFor(s => s.StartYear)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class StartYear cannot be empty");
      //EndDate
      RuleFor(s => s.EndDate)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Class EndDate cannot be empty");
      //ClassNumber
      RuleFor(s => s.ClassNumber)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
         .ExclusiveBetween(0, 100).WithMessage("Class {PropertyName} must be between {From} and {To}");
      //IdProgram
      RuleFor(s => s.IdProgram)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Program for this Class");

      RuleFor(s => s.IdProgram).GreaterThan(0).WithMessage("The Program id must be larger than 0");
      //IdTechnicalGroup
      RuleFor(s => s.IdTechnicalGroup)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a TechnicalGroup for this Class");

      RuleFor(s => s.IdTechnicalGroup).GreaterThan(0).WithMessage("The TechnicalGroup id must be larger than 0");
      //IdFSU
      RuleFor(s => s.IdFSU)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a FSU for this Class");

      RuleFor(s => s.IdFSU).GreaterThan(0).WithMessage("The FSU id must be larger than 0");
      //IdSite
      RuleFor(s => s.IdSite)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Site for this Class");

      RuleFor(s => s.IdSite).GreaterThan(0).WithMessage("The Site id must be larger than 0");
      //IdUniversity
      RuleFor(s => s.IdUniversity)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a University for this Class");

      RuleFor(s => s.IdUniversity).GreaterThan(0).WithMessage("The University id must be larger than 0");
      //IdFormatType
      RuleFor(s => s.IdFormatType)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a FormatType for this Class");

      RuleFor(s => s.IdFormatType).GreaterThan(0).WithMessage("The FormatType id must be larger than 0");
      //IdProgramContent
      RuleFor(s => s.IdProgramContent)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a ProgramContent for this Class");

      RuleFor(s => s.IdProgramContent).GreaterThan(0).WithMessage("The ProgramContent id must be larger than 0");
      //IdLocation
      RuleFor(s => s.IdLocation)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Location for this Class");
      //IdAttendeeType
      RuleFor(s => s.IdAttendeeType)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a AttendeeType for this Class");

      RuleFor(s => s.IdAttendeeType).GreaterThan(0).WithMessage("The AttendeeTyp id must be larger than 0");
      //ActiveDate
      RuleFor(s => s.ActiveDate)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a ActiveDate for this Class");
      //IdTrainee
      RuleFor(s => s.IdTrainee)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Trainee for this Class");
      //IdAdmin
      RuleFor(s => s.IdAdmin)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Admin for this Class");
      //IdMentor
      RuleFor(s => s.IdMentor)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Mentor for this Class");
    }

  }
  public class ClassValidatorForSaveAsDraft : AbstractValidator<UpdateClassViewModel>
  {
    public ClassValidatorForSaveAsDraft()
    {
      //ID
      RuleFor(s => s.Id)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty().WithMessage("Please enter an Id for Class")
          .NotNull().WithMessage("Please enter an Id for Class");

      RuleFor(s => s.Id).GreaterThan(0).WithMessage("The Class id must be larger than 0");
      //Name
      RuleFor(s => s.Name)
          .Cascade(CascadeMode.StopOnFirstFailure)
          .NotEmpty().WithMessage("Class {PropertyName} must not be blank")
              .When(s => s.Name.Trim().Equals(""), ApplyConditionTo.CurrentValidator)
          .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .Length(2, 500).WithMessage("{PropertyName} must be between {MinLength}..{MaxLength} characters");
      //ClassNumber
      RuleFor(s => s.ClassNumber)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
         .ExclusiveBetween(0, 100).WithMessage("Class {PropertyName} must be between {From} and {To}");
      //IdProgram
      RuleFor(s => s.IdProgram)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Program for this Class");

      RuleFor(s => s.IdProgram).GreaterThan(0).WithMessage("The Class id must be larger than 0");
      //IdTechnicalGroup
      RuleFor(s => s.IdTechnicalGroup)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a TechnicalGroup for this Class");

      RuleFor(s => s.IdTechnicalGroup).GreaterThan(0).WithMessage("The TechnicalGroup id must be larger than 0");
      //IdFSU
      RuleFor(s => s.IdFSU)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a FSU for this Class");

      RuleFor(s => s.IdFSU).GreaterThan(0).WithMessage("The FSU id must be larger than 0");
      //IdSite
      RuleFor(s => s.IdSite)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a Site for this Class");

      RuleFor(s => s.IdSite).GreaterThan(0).WithMessage("The Site id must be larger than 0");
      //IdUniversity
      RuleFor(s => s.IdUniversity)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a University for this Class");

      RuleFor(s => s.IdUniversity).GreaterThan(0).WithMessage("The University id must be larger than 0");
      //IdFormatType
      RuleFor(s => s.IdFormatType)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a FormatType for this Class");

      RuleFor(s => s.IdFormatType).GreaterThan(0).WithMessage("The FormatType id must be larger than 0");
      //IdProgramContent
      RuleFor(s => s.IdProgramContent)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a ProgramContent for this Class");

      RuleFor(s => s.IdProgramContent).GreaterThan(0).WithMessage("The ProgramContent id must be larger than 0");
      //IdAttendeeType
      RuleFor(s => s.IdAttendeeType)
         .Cascade(CascadeMode.StopOnFirstFailure)
         .NotNull().WithMessage("Please enter a {PropertyName} for Class")
          .NotEmpty().WithMessage("Please choose a AttendeeType for this Class");

      RuleFor(s => s.IdAttendeeType).GreaterThan(0).WithMessage("The AttendeeTyp id must be larger than 0");
    }
  }
}