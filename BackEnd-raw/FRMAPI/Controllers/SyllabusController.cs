using BAL.Authorization;
using BAL.Models;
using BAL.Services.Interfaces;
using BAL.Validators;
using Castle.Core.Internal;
using DAL.Entities;
using DAL.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IO.Compression;
using static DAL.Entities.Syllabus;

namespace FRMAPI.Controllers
{

    [Route("api/[controller]")]
    public class SyllabusController : Controller
    {
        private readonly ISyllabusService _syllabusService;
        private readonly IUnitOfWork _unitOfWork;
        public SyllabusController(ISyllabusService syllabusService, IUnitOfWork unitOfWork)
        {
            _syllabusService = syllabusService;
            _unitOfWork = unitOfWork;
        }

        #region Deactive Syllabus
        /// <summary>
        /// Deactivate selected syllabus, set syllabus status to inactive
        /// </summary>
        /// <param name="id"> Syllabus ID</param>
        /// <returns>Return result of action and error message (if any) </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Deactivate/{id}
        ///     "id": 1
        ///
        /// </remarks>
        /// <response code="200"> Syllabus deactivated</response>
        /// <response code="404"> Syllabus id not found</response>
        /// <response code="400"> Invalid input</response>
        [HttpPut("[action]/{id}")]
        [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Deactivate([FromRoute] long id)
        {
            var claims = Request.HttpContext.User.Claims.ToList();
            string errorMessage = "";
            bool status = false;

            //Coding session
            SyllabusIdValidator validator = new();
            var validation = validator.Validate(id);
            if (!validation.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                status = false;
                return BadRequest(new
                {
                    status = status,
                    errors = errors
                });
            }
            try
            {

                _syllabusService.DeactivateSyllabus(id, claims);
                _syllabusService.Save();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }


            //End coding session

            return Ok(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
        #endregion

        #region Activate Syllabus
        /// <summary>
        /// Activate selected syllabus, set syllabus status to active
        /// </summary>
        /// <param name="id"> Syllabus ID</param>
        /// <returns>Return result of action and error message (if any) </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Activate/{id}
        ///     "id": 1
        ///
        /// </remarks>
        /// <response code="200"> Syllabus activated</response>
        /// <response code="404"> Syllabus id not found</response>
        /// <response code="400"> Invalid input</response>
        [HttpPut("[action]/{id}")]
        [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Activate([FromRoute] long id)
        {
            var claims = Request.HttpContext.User.Claims.ToList();
            string errorMessage = "";
            bool status = false;

            //Coding session
            SyllabusIdValidator validator = new();
            var validation = validator.Validate(id);
            if (!validation.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                status = false;
                return BadRequest(new
                {
                    status = status,
                    errors = errors
                });
            }
            try
            {

                _syllabusService.ActivateSyllabus(id, claims);
                _syllabusService.Save();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }


            //End coding session

            return Ok(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
        #endregion

        #region Edit/Update Syllabus
        /// <summary>
        /// Edit or delete content of Syllabus. Users edit “Syllabus Name”, “Level”, “Attendee Number”, “Technical Requirement(s)”, “Course Objectives”
        /// </summary>
        /// <param name="syllabusViewModel">Object Type: SyllabusViewModel</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT 
        ///        *Please get a Sylabus from GetById to Edit/Update
        ///
        /// </remarks>
        /// <returns>Return result of action and error message (if any)</returns>
        /// <response code="200">Syllabus Updated</response>
        /// <response code="400">Syllabus wrong input detected</response>
        [HttpPut]
        [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] SyllabusViewModel syllabusViewModel)
        {

            var claims = Request.HttpContext.User.Claims.ToList();

            string errorMessage = "";
            bool status = false;

            SyllabusValidatorForEdit validator = new SyllabusValidatorForEdit();

            try
            {
                if (syllabusViewModel == null) throw new Exception("Api's information has been corrupted, please try again or contact developer for more support");

                var validation = validator.Validate(syllabusViewModel);
                if (!validation.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var error in validation.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                    status = false;
                    return BadRequest(new
                    {
                        status = status,
                        errors = errors
                    });
                }

                _syllabusService.UpdateSyllabus(syllabusViewModel, claims);
                _syllabusService.Save();
                status = true;
            }
            catch (Exception Ex)
            {
                status = false;
                errorMessage = Ex.Message;
            }

            //End coding session
            return Ok(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
        #endregion

        #region Get Syllabus By ID
        /// <summary>
        /// View Syllabus: Get Syllabus by id
        /// </summary>
        /// <param name="id">Syllabus ID</param>
        /// <returns>Return result of action and error message (if any) </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{id}
        ///     "id": 1
        ///
        /// </remarks>
        /// <returns>Return a syllabus with the given ID </returns>
        /// <response code="200"> Return Syllabus with the given ID</response>
        /// <response code="400"> Invalid input</response>
        /// <response code="404"> Syllabus id not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [PermissionAuthorize(new string[] { "View", "Full Access" })]
        public async Task<IActionResult> GetSyllabusById([FromRoute] long id)
        {
            string errorMessage = "";
            bool status = false;

            //Coding session
            SyllabusIdValidator validator = new();
            var validation = validator.Validate(id);
            if (!validation.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                status = false;
                return BadRequest(new
                {
                    status = status,
                    errors = errors
                });
            }

            SyllabusViewModel result = null;
            try
            {

                result = _syllabusService.GetById(id);
                status = true;

            }
            catch (Exception Ex)
            {
                status = false;
                errorMessage = Ex.Message;
            }

            //End coding session

            return Ok(new
            {
                status = status,
                errorMessage = errorMessage,
                result = result
            });
        }
        #endregion

        #region Duplicate Syllabus
        /// <summary>
        /// Duplicate Syllabus with different ID
        /// </summary>
        /// <param name="id">Syllabus ID</param>
        /// <returns>Return result of action and error message (if any)</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Duplicate/{id}
        ///     "id": 1
        ///
        /// </remarks>
        /// <response code="200"> Syllabus Duplicated</response>
        /// <response code="404"> Syllabus id not found</response>
        /// <response code="400"> Invalid input</response>
        [HttpPost("[action]/{id}")]
        [PermissionAuthorize(new string[] { "Create", "Full Access" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Duplicate([FromRoute] long id)
        {
            var claims = Request.HttpContext.User.Claims.ToList();
            string errorMessage = "";
            bool status = false;

            //Coding session
            SyllabusIdValidator validator = new();
            var validation = validator.Validate(id);
            if (!validation.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                status = false;
                return BadRequest(new
                {
                    status = status,
                    errors = errors
                });
            }

            try
            {

                _syllabusService.DuplicateSyllabus(id, claims);
                _syllabusService.Save();
                status = true;
            }
            catch (Exception Ex)
            {
                status = false;
                errorMessage = Ex.Message;
            }
            //End coding session

            return Ok(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
        #endregion

        #region Delete Syllabus
        /// <summary>
        /// Delete selected syllabus
        /// </summary>
        /// <param name="id">Syllabus ID</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /{id}
        ///     "id": 1
        ///
        /// </remarks>
        /// <returns>Return result of action and error message (if any)</returns>
        /// <response code="200"> Syllabus Deleted</response>
        /// <response code="404"> Syllabus id not found</response>
        /// <response code="400"> Invalid input</response>
        [HttpDelete("{id}")]
        [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var claims = Request.HttpContext.User.Claims.ToList();
            string errorMessage = "";
            bool status = false;

            //Coding session
            SyllabusIdValidator validator = new();
            var validation = validator.Validate(id);
            if (!validation.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                status = false;
                return BadRequest(new
                {
                    status = status,
                    errors = errors
                });
            }
            try
            {
                _syllabusService.DeleteSyllabus(id, claims);
                _syllabusService.Save();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }


            //End coding session

            return Ok(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
        #endregion

        #region Create Syllabus
        /// <summary>
        /// Create new Syllabus with inputted information. API will accept list of Sessions with each session contains list of its units, with each unit contains list of its own lessons
        /// </summary>
        /// <param name="syllabusViewModel">Object Type: SyllabusViewModel</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///       "name": "C# Language Program ",
        ///       "code": "NPL",
        ///       "attendeeNumber": 20,
        ///       "technicalRequirement": "\ufeffTrainees\u2019 PCs need to have following software installed and run without any issues: \n\u2022 Microsoft SQL Server 2005 Express (in which the trainees can create and manipulate on their own database \n\u2022 Microsoft Visual Studio 2017 \n\u2022 Microsoft Office 2007 (Visio, Word, PowerPoint)",
        ///       "courseObjectives": "This topic is to introduce about C# programming language knowledge; adapt trainees with skills, lessons and practices which is specifically used in the Fsoft projects.",
        ///       "trainingPrinciple": "Trainee who actively complete online learning according to MOOC links provided - At the end of the day, students complete Daily Quiz for 30 minutes - Trainer/Mentor supports answering questions, guiding exercises 1.5-2.0h/day - Trainer conduct the workshops - Trainees complete Assignments and Labs - Trainees have 1 final test in 4 hours(1 hour theory + 3 hours of practice)",
        ///       "idLevel": 2,
        ///       "assignmentSchema": {
        ///          "percentQuiz": 15,
        ///          "percentAssigment": 15,
        ///          "percentFinal": 70,
        ///          "percentTheory": 40,
        ///           "percentFinalPractice": 60,
        ///           "passingCriterial": 70
        ///         },
        ///       "sessions": [
        ///         {
        ///           "name": "Session 1",
        ///           "index": 1,
        ///           "units": [
        ///             {
        ///               "name": ".NET Introduction",
        ///               "index": 1,
        ///               "lessons": [
        ///                 {
        ///                   "name": ".NET Introduction",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": ".NET Introduction overview.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": ".NET Introduction pattern in lorem.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "What is future.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": ".NET history.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Declaration and Assignment",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Practice Time: Assignment/Mentoring",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             },
        ///             {
        ///               "name": "Operator",
        ///               "index": 2,
        ///               "lessons": [
        ///                 {
        ///                   "name": "Operators",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Comparation",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Logical Operators",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Practice Time: Assignment/Mentoring",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 1,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             }
        ///           ]
        ///         },
        ///         {
        ///           "name": "Session 2",
        ///           "index": 2,
        ///           "units": [
        ///             {
        ///               "name": "Flow control",
        ///               "index": 1,
        ///               "lessons": [
        ///                 {
        ///                   "name": "Lesson 1",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 2",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 3",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             },
        ///             {
        ///               "name": "Basic OOP",
        ///               "index": 2,
        ///               "lessons": [
        ///                 {
        ///                   "name": "Lesson 1",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 2",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 3",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 4",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 1,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             }
        ///           ]
        ///         }
        ///       ]
        ///     } 
        /// 
        /// </remarks>
        /// <returns>Return result of action and error message (if any)</returns>
        /// <response code="201">Syllabus Created</response>
        /// <response code="400"> Invalid input</response>
        /// <response code="500">Syllabus fail to create</response>
        [HttpPost]
        [PermissionAuthorize(new string[] { "Create", "Full Access" })]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SyllabusViewModel syllabusViewModel)
        {
            var claims = Request.HttpContext.User.Claims.ToList();
            string errorMessage = "";
            bool status = false;

            //Coding session

            SyllabusValidatorForCreate createValidator = new SyllabusValidatorForCreate();
            SyllabusValidatorForEdit editValidator = new SyllabusValidatorForEdit();

            try
            {
                if (syllabusViewModel == null) throw new Exception("Api's information has been corrupted, please try again or contact developer for more support");

                // Check if there's already a draft in Database (GET take existed record in Database)
                if (syllabusViewModel.Status == 2)
                {
                    if (syllabusViewModel == null) throw new Exception("Api's information has been corrupted, please try again or contact developer for more support");

                    var editValidation = editValidator.Validate(syllabusViewModel);
                    if (!editValidation.IsValid)
                    {
                        var errors = new List<string>();
                        foreach (var error in editValidation.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                        status = false;
                        return BadRequest(new
                        {
                            status = status,
                            errors = errors
                        });
                    }
                    // Set Flag as True for actions
                    _syllabusService.SetSaveAsDraftToCreateCheck(true);
                    // Update Draft Syllabus from Draft to Active
                    _syllabusService.UpdateSyllabus(syllabusViewModel, claims);
                }
                else
                {
                    // Validate
                    var createValidation = createValidator.Validate(syllabusViewModel);
                    if (!createValidation.IsValid)
                    {
                        var errors = new List<string>();
                        foreach (var error in createValidation.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                        status = false;
                        return BadRequest(new
                        {
                            status = status,
                            errors = errors
                        });
                    }
                    // Create new Syllabus
                    _syllabusService.CreateSyllabus(syllabusViewModel, claims);
                }

                _syllabusService.Save();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }

            //End coding session
            return Created("", new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
        #endregion

        #region Save Syllabus As Draft
        /// <summary>
        /// Create new Syllabus with status "Draft". API will accept list of Sessions with each session contains list of its units, with each unit contains list of its own lessons
        /// </summary>
        /// <param name="syllabusViewModel">Object Type: SyllabusViewModel</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///       "name": "C# Language Program ",
        ///       "code": "NPL",
        ///       "attendeeNumber": 20,
        ///       "technicalRequirement": "\ufeffTrainees\u2019 PCs need to have following software installed and run without any issues: \n\u2022 Microsoft SQL Server 2005 Express (in which the trainees can create and manipulate on their own database \n\u2022 Microsoft Visual Studio 2017 \n\u2022 Microsoft Office 2007 (Visio, Word, PowerPoint)",
        ///       "courseObjectives": "This topic is to introduce about C# programming language knowledge; adapt trainees with skills, lessons and practices which is specifically used in the Fsoft projects.",
        ///       "trainingPrinciple": "Trainee who actively complete online learning according to MOOC links provided - At the end of the day, students complete Daily Quiz for 30 minutes - Trainer/Mentor supports answering questions, guiding exercises 1.5-2.0h/day - Trainer conduct the workshops - Trainees complete Assignments and Labs - Trainees have 1 final test in 4 hours(1 hour theory + 3 hours of practice)",
        ///       "idLevel": 1,
        ///       "assignmentSchema": {
        ///          "percentQuiz": 15,
        ///          "percentAssigment": 15,
        ///          "percentFinal": 70,
        ///          "percentTheory": 40,
        ///          "percentFinalPractice": 60,
        ///          "passingCriterial": 70
        ///         },
        ///       "sessions": [
        ///         {
        ///           "name": "Session 1",
        ///           "index": 1,
        ///           "units": [
        ///             {
        ///               "name": ".NET Introduction",
        ///               "index": 1,
        ///               "lessons": [
        ///                 {
        ///                   "name": ".NET Introduction",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": ".NET Introduction overview.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": ".NET Introduction pattern in lorem.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "What is future.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": ".NET history.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Declaration and Assignment",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Practice Time: Assignment/Mentoring",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             },
        ///             {
        ///               "name": "Operator",
        ///               "index": 2,
        ///               "lessons": [
        ///                 {
        ///                   "name": "Operators",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Comparation",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Logical Operators",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Practice Time: Assignment/Mentoring",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 1,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             }
        ///           ]
        ///         },
        ///         {
        ///           "name": "Session 2",
        ///           "index": 2,
        ///           "units": [
        ///             {
        ///               "name": "Flow control",
        ///               "index": 1,
        ///               "lessons": [
        ///                 {
        ///                   "name": "Lesson 1",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 2",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 3",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             },
        ///             {
        ///               "name": "Basic OOP",
        ///               "index": 2,
        ///               "lessons": [
        ///                 {
        ///                   "name": "Lesson 1",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 2",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 2,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 3",
        ///                   "duration": 30,
        ///                   "idDeliveryType": 3,
        ///                   "idFormatType": 1,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "learning-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-3.youtube",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "learning-material-4.ppt",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 },
        ///                 {
        ///                   "name": "Lesson 4",
        ///                   "duration": 120,
        ///                   "idDeliveryType": 1,
        ///                   "idFormatType": 2,
        ///                   "idOutputStandard": 3,
        ///                   "materials": [
        ///                     {
        ///                       "name": "assignment-material-1.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     },
        ///                     {
        ///                       "name": "assignment-material-2.pdf",
        ///                       "hyperlink": "link-to-file.com"
        ///                     }
        ///                   ]
        ///                 }
        ///               ]
        ///             }
        ///           ]
        ///         }
        ///       ]
        ///     } 
        /// 
        /// </remarks>
        /// <returns>Return result of action and error message (if any)</returns>
        /// <response code="201">Syllabus Created</response>
        /// <response code="400"> Invalid input</response>
        /// <response code="500">Syllabus fail to create</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [PermissionAuthorize(new string[] { "Create", "Full Access" })]
        [HttpPost("[action]")]
        public async Task<IActionResult> SaveAsDraft([FromBody] SyllabusViewModel syllabusViewModel)
        {
            var claims = Request.HttpContext.User.Claims.ToList();
            string errorMessage = "";
            bool status = false;

            //Coding session
            SyllabusValidatorForEdit editValidator = new SyllabusValidatorForEdit();
            SyllabusValidatorForCreate createValidator = new SyllabusValidatorForCreate();
            try
            {
                // Check if there's already a draft in Database (GET take existed record in Database)
                if (syllabusViewModel.Status == 2)
                {
                    var editValidation = editValidator.Validate(syllabusViewModel);
                    if (!editValidation.IsValid)
                    {
                        var errors = new List<string>();
                        foreach (var error in editValidation.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                        status = false;
                        return BadRequest(new
                        {
                            status = status,
                            errors = errors
                        });
                    }
                    // Update Draft if there's an existed draft in Database
                    _syllabusService.UpdateSyllabus(syllabusViewModel, claims);
                }
                else
                {
                    //Validation
                    var createValidation = createValidator.Validate(syllabusViewModel);
                    if (!createValidation.IsValid)
                    {
                        var errors = new List<string>();
                        foreach (var error in createValidation.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                        status = false;
                        return BadRequest(new
                        {
                            status = status,
                            errors = errors
                        });
                    }
                    // Create Draft in Databse when Save new Draft
                    _syllabusService.SaveAsDraft(syllabusViewModel, claims);
                }

                _syllabusService.Save();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }

            //End coding session

            return Created("", new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
        #endregion

        #region API GetSyllabuses
        /// <summary>
        /// đây là API show là list và trong đó có sort, search và search theo created day theo ngày và chọn số lượng phần tử trong một trang
        /// </summary>
        /// 
        /// <param name="PAGE_SIZE">số lượng phần tử có trong 1 trang defaul ban đầu sẽ là 6</param>
        /// <param name="page">la trang dang hien thi vd 1 la trang 1, 2 la trang 2 </param>

        /// <param name="key">có thể có hoặc không tìm kiếm theo từ nào đó</param>
        /// <param name="from">có thể có hoặc không sẽ cho nhập vào ngày bắt đầu từ (yyyy-mm-dd)</param>
        /// <param name="to">có thể có hoặc không sẽ cho nhập đến ngày  mấy  (yyyy-mm-dd)</param>
        /// <param name="sortBy">với duration từ a => z thì nhấn Duration ASC ,với duration từ z => a thì nhấn Duration DESC,với name từ a => z thì nhấn Name ASC, với duration từ z => a thì nhấn Name DESC,...</param>
        /// <returns>hệ thống sẽ trả về một danh sách</returns>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET
        ///     "Search": NET
        ///     "from": 01/10/2021
        ///     "to": 01/11/2021
        ///     {
        ///         "Name":[".NET Basic program"],
        ///         "Code":["NET"],
        ///         "Created on":["07/10/2021"]
        ///         "Created by":["HaNTT2"],
        ///         "Duration":["5 days"],
        ///         "Output standard": ["H4SD"/"K4SD"],
        ///         "Status":["Active"]
        ///     }
        /// </remarks>
        /// <response code="200">trả về list of Syllabus Model</response>
        /// <response code="404">nếu list null</response>
        /// 
        [HttpGet("[action]")]
        [PermissionAuthorize("View", "Full Access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSyllabuses(int PAGE_SIZE, List<string>? key, DateTime? from, DateTime? to, List<string>? sortBy, int page)
        {
            try
            {
                int sophantu = 0;
                int sotrang = 1;
                if (PAGE_SIZE == 0)
                {
                    PAGE_SIZE = 3;
                }
                if(page == 0)
                {
                    page = 1;
                }
                List<SyllabusModel> result = _syllabusService.GetAll(key, PAGE_SIZE, from, to, sortBy, page);
                int a = result.Count();
                #region paginate           
                result = result.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
                #endregion
                if (result.Count != 0)
                {
                    if (a % PAGE_SIZE == 0)
                    {
                        sotrang = a / PAGE_SIZE;
                        sophantu = PAGE_SIZE;
                    }
                    else
                    {
                        sotrang = a / PAGE_SIZE + 1;
                        if (page < sotrang)
                        {
                            sophantu = PAGE_SIZE;
                        }
                        else
                        {
                            sophantu = a % PAGE_SIZE;
                        }
                    }
                }
                return Ok(new
                {

                    PAGE_SIZE = PAGE_SIZE,
                    NumberOfElementInPage = sophantu,
                    PageNumber = page,
                    NumberOfPage = sotrang,
                    Success = true,
                    Data = result
                });
            }
            catch
            {
                return NotFound("We can't get the product");
            }
        }
        #endregion

        #region ImprortSyllabuses
        /// <summary>
        /// đây là API Import Syllabuses
        /// </summary>
        /// <param name="file">đây là file xls/xlxs để import syllabuses</param>
        /// <returns>hệ thống sẽ trả về danh sách </returns>
        /// <remarks>
        /// Sample request
        /// 
        ///      GET
        ///      {
        ///      "File(xlsx)": ["xxx.xlsx"],
        ///      "Import template": download,
        ///      "Scanning": ["Syllabus code"],
        ///      "Duplicate handle": ["Allow"]
        ///      }
        /// </remarks>
        /// <response code="200">trả về list of Syllabus</response>
        /// <response code="400">nếu không import được</response>
        [HttpPost("[action]")]
        [PermissionAuthorize("Create", "Full Access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public async Task<IActionResult> UploadExcelFile([FromForm] UpLoadExcelFileRequest request)
        {
            UpLoadExcelFileResponse response = new UpLoadExcelFileResponse();
            try
            {
                string Path = request.File.FileName;

                try
                {

                    using (FileStream stream = new FileStream(Path, FileMode.CreateNew))
                    {
                        await request.File.CopyToAsync(stream);
                    }

                    response = await _syllabusService.UploadExcelFile(request, Path);
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;

                }
            } catch 
            {
                return BadRequest();
            }
            
            return Ok(response);
        }
        private const string TemplateFilesPath = @"C:Temp\Templates";
        [HttpGet("[action]")]
        [PermissionAuthorize("Create", "Full Access")]
        public async Task<IActionResult> GetTemplateFiles()
        {
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in Directory.GetFiles(TemplateFilesPath))
                    {
                        var fileInArchive = archive.CreateEntry(Path.GetFileName(file), CompressionLevel.Optimal);
                        using (var entryStream = fileInArchive.Open())
                        {
                            using (var fileCompressionStream = new MemoryStream(System.IO.File.ReadAllBytes(file)))
                            {
                                await fileCompressionStream.CopyToAsync(entryStream);
                            }

                        }

                    }
                }
                outStream.Position = 0;
                return File(outStream.ToArray(), "application/zip", "TeamplateFiles.zip");
            }
        }
        #endregion
    }
}
