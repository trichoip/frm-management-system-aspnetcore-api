using AutoMapper;
using BAL.Models;
using DAL.Entities;
using FRMAPI.Helpers;
using BAL.Authorization;
using DAL.Infrastructure;
using System.Security.Claims;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FRMAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TrainingProgramController : ControllerBase
{
    List<Syllabus> syllabuses = new List<Syllabus>();
    List<Session> sessions = new List<Session>();
    List<Lesson> lessons = new List<Lesson>();
    List<Material> materials = new List<Material>();
    TrainingProgram initTrainingProgram = new TrainingProgram();
    List<TrainingProgram> detailTrainingPrograms = new List<TrainingProgram>();
    HistoryMaterial historyMaterial = new HistoryMaterial();

    // Team6
    private readonly IMaterialService _materialService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUnitService _unitService;
    private readonly ISessionService _sessionService;
    private readonly ISyllabusService _syllabusService;
    private readonly ILessonService _lessonService;
    private readonly IMapper _mapper;
    private readonly ITrainingProgramService _trainingProgramService;
    public TrainingProgramController(IMaterialService materialService,
        IUnitOfWork unitOfWork,
        IUnitService unitService,
        ISessionService sessionService,
        ISyllabusService syllabusService,
        IMapper mapper,
        ITrainingProgramService trainingProgramService,
        ILessonService lessonService)

    {
        _materialService = materialService;
        _unitOfWork = unitOfWork;
        _unitService = unitService;
        _sessionService = sessionService;
        _syllabusService = syllabusService;
        _lessonService = lessonService;
        _mapper = mapper;
        _trainingProgramService = trainingProgramService;

    }
    // Team6


    /// <summary>
    /// UC4-001
    /// View training program list
    /// </summary>
    /// <param name="sortBy">sort</param>
    /// <param name="pagesize">sort</param>
    /// <returns>Return list of training program</returns>
    /// <response code="400">If the list is null</response>
    /// <response code="200">Get training program list</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET   
    ///     {
    ///     
    ///     "keyWord": "string",
    ///     "locations": [
    ///     "string"
    ///     ],
    ///     "startTime": "string",
    ///     "endTime": "string",
    ///     "timeClasses": [
    ///     "string"
    ///     ],
    ///     "statuses": [
    ///     "string"
    ///     ],
    ///     "attendees": [
    ///     "string"
    ///     ],
    ///     "idFSU": 0,
    ///     "idTrainer": 0
    ///     }
    ///              
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(TrainingCalendarViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [PermissionAuthorize(new string[] { "View " })]



    public async Task<IActionResult> ViewTrainingProgram([FromQuery] string? sortBy,int pagesize)
    {
        string errorMessage = "";
        bool status = false;
        List<TrainingProgram> trainingprogram = new List<TrainingProgram>();
        var result = new List<TrainingProgramViewModel>();
        try
        {
            result = await _trainingProgramService.GetAll(sortBy, pagesize);
            status = true;
        }
        catch (Exception ex)
        {
            status = false;
            errorMessage = ex.Message;
        }

        //End coding session

        return new JsonResult(new
        {
            trainingprogramList = result,
            status = status,
            errorMessage = errorMessage
        });
    }
    /// <summary>
    /// UC4-003
    /// Edit training program
    /// </summary>
    /// <param name="Id">Id of the training program </param>
    /// <param name="name">Name</param>
    /// <param name="program_status">program_status</param>
    ///  <remarks>
    /// Sample request:
    ///
    ///     PUT 
    ///     "Id": 0
    ///     "syllabuses": [
    ///     {
    ///     "assignmentSchema": null,
    ///     "name": MANH,
    ///     "code": C#,
    ///     "version": 6.0.0,
    ///     "technicalrequirement": NOTHING,
    ///     "courseObjectives": Intermediate C# knowledge,
    ///     "status": 0,
    ///     "trainingPrinciple": Hoc toi chet,
    ///     "description": chet cx phai hoc,
    ///     "hyperLink": none,
    ///     "idLevel": 0,
    ///     "level": 1,
    ///     "syllabusTrainers": VU VAN MANH,
    ///     "historySyllabi": none,
    ///     "sessions": none,
    ///     "curricula": none
    ///     }
    /// </remarks>

    /// <response code="400">If the list is null</response>
    /// <response code="200">Get the Syllabus required for edit</response>
    /// <returns>Search for ID of the program and proceed to make changes to the program</returns>
    [HttpPut("{Id}")]

    [ProducesResponseType(typeof(TrainingProgramViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [PermissionAuthorize(new string[] { "Modify " })]

    public async Task<IActionResult> Edit(long Id, string name, int program_status)
    {
        string errorMessage = "";
        bool status = false;
        List<TrainingProgramViewModel> syllabuses = new List<TrainingProgramViewModel>();
        // syllabuses.Add(model);

        status = true;
        //End coding session

        var result = new List<TrainingProgramViewModel>();
        try
        {
            status = await _trainingProgramService.Edit(Id, name, program_status);
        }
        catch (Exception ex)
        {
            status = false;
            errorMessage = ex.Message;
        }

        //End coding session

        return new JsonResult(new
        {
            syllabuses = syllabuses,
            status = status,
            errorMessage = status ? "Success" : "Something went wrong!"
        });
    }
    ///// <summary>
    ///// UC4-002
    ///// Filter training program
    ///// </summary>
    ///// <param name="name">Create By of the training class program </param>
    ///// <param name="createBy">Name of the training class program </param>
    ///// <returns>Only take the one that matched the required information</returns>
    //[HttpGet("{createBy}&{name}")]
    ///// <response code="400">If the list is null</response>
    ///// <response code="200">Filter training program</response>
    ///// ///  <remarks>
    ///// Sample request:
    /////
    /////     PUT 
    /////     "CreateBy": VU VAN MANH
    /////     "NAME"    : NONE
    /////
    ///// </remarks>
    //public async Task<IActionResult> Filter(List<HistoryTrainingProgram> historyTrainingPrograms, string name)
    //{
    //    string errorMessage = "";
    //    bool status = false;
    //    List<TrainingProgram> trainingprogram = new List<TrainingProgram>();

    //    status = true;
    //    //End coding session

    //    return new JsonResult(new
    //    {
    //        trainingprogramList = trainingprogram,
    //        status = status,
    //        errorMessage = errorMessage
    //    });
    //}

    /// <summary>
    /// Filter training program
    /// </summary>
    /// <param name="programNames"></param>
    /// <returns></returns>
    [HttpPut]
    [PermissionAuthorize(new string[] { "View " })]

    public async Task<IActionResult> GetByFilter(List<string> programNames)
    {
        string errorMessage = "";
        bool status = false;

        var filteredList = await _trainingProgramService.GetByFilter(programNames);
        status = true;
        //End coding session

        return new JsonResult(new
        {
            trainingprogramList = filteredList,
            status = status,
            errorMessage = errorMessage
        });
    }

    /// <summary>
    /// UC4-004
    /// Delete training program
    /// </summary>
    /// <param name="Id">Id of the training class program </param>
    /// <returns>Search for matched id program and delete</returns>
    /// <response code="400">If the list is null</response>
    /// <response code="200">Delete training program</response>
    /// /// ///  <remarks>
    /// Sample request:
    ///
    ///     DELETE 
    ///     "Id": 1
    ///
    /// </remarks>

    [HttpDelete("{Id}")]
    [PermissionAuthorize(new string[] { "Delete while viewing " })]

    public async Task<IActionResult> Delete(long Id)
    {
        string errorMessage = "";
        bool status = false;
        string mess;

        status = true;
        //End coding session


        try
        {
            status = await _trainingProgramService.Delete(Id);
            mess = "Delete Complete";
        }
        catch (Exception ex)
        {
            status = false;
            mess = "Training Program is Active";
        }

        //End coding session

        return new JsonResult(new
        {
            status = status,
            mess
        });
    }

    /// <summary>
    /// UC4-005
    /// Duplicate training program
    /// Role: Super Admin
    /// </summary>
    /// <param name="Id">Id of the training class program </param>
    /// <returns>Duplicate the program with full detail</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST 
    ///     "Id": 1
    ///     "Name": Manh
    ///     "Status": Active
    ///     "Class": {}
    ///     "HistoryTrainingProgram": {}
    ///     "Curriculum": {}
    /// </remarks>

    /// <response code="400">If the list is null</response>
    /// <response code="200">Get the Program required for duplicate</response>
    [HttpPost("{Id}")]
    [ProducesResponseType(typeof(TrainingProgramViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [PermissionAuthorize(new string[] { "Full access " })]

    public async Task<IActionResult> Duplicate(long Id)
    {
        string errorMessage = "";
        bool status = false;


        status = true;
        //End coding session

        long result = 0;
        try
        {
            result = await _trainingProgramService.Duplicate(Id);
            if (result > 0)
            {
                status = true;
            }
            else
            {
                status = false;
                errorMessage = "Some thing went wrong!";
            }
        }
        catch (Exception ex)
        {
            status = false;
            errorMessage = ex.Message;
        }

        //End coding session

        return new JsonResult(new
        {
            newProgramId = result,
            status = status,
            errorMessage = errorMessage
        });
    }
    /// <summary>
    /// UC4-006
    /// Deactivate training program
    /// </summary>
    /// <param name="id">Id of the training class program </param>
    /// <returns>De-Activate or Activate the program</returns>
    /// /// <response code="400">If the list is null</response>
    /// <response code="200">Deactivate training program</response>
    /// /// ///  <remarks>
    /// Sample request:
    ///
    ///     PUT 
    ///     "Id": 1
    ///     "Status": 0
    ///
    /// </remarks>
    [HttpPut("{Id}")]
    [PermissionAuthorize(new string[] { "Modify " })]

    public async Task<IActionResult> DeActivate(long Id)
    {
        string errorMessage = "";
        bool status = false;
        try
        {
            status = await _trainingProgramService.DeActivate(Id);
        }
        catch (Exception ex)
        {
            status = false;
            errorMessage = ex.Message;
        }

        //End coding session

        return new JsonResult(new
        {
            status = status
        });
    }




    //------------------------------------------FIGMA 5-------------------------------------------------------

    // Team6

    /// <summary>
    /// Get detail of a Training program by id Training Program
    /// </summary>
    /// <param name="programId">This is a training program id</param>
    /// <returns>Detail of Training program</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     1
    ///
    /// </remarks>
    /// <response code="200">Return detail of training program</response>
    /// <response code="404">If training program is null</response>
    [HttpGet("{programId}")]
    [ProducesResponseType(typeof(TrainingProgramDetailViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [PermissionAuthorize("View", "Modify", "Full access")]
    public async Task<IActionResult> GetDetailTrainingProgram(long programId)
    {
        string errorMessage = "";
        bool status = false;

        TrainingProgramDetailViewModel result = new TrainingProgramDetailViewModel();
        try
        {

            result = _syllabusService.GetDetailTrainingProgram(programId);

            if (result == null)
            {
                return NotFound();
            }

            status = true;

        }
        catch (Exception Ex)
        {
            status = false;
            errorMessage = Ex.Message;
        }

        return Ok(new
        {
            Success = true,
            Data = result,
        });

        /* return Ok(new
         {
             //syllabus = syllabus,
             status = status,
             errorMessage = errorMessage,
             result = result
         }); */
    }




  

    // Team6

    /// <summary>
    /// Update material by id 
    /// </summary>
    /// <returns>Returns new lesson material</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     PUT 
    ///     "meterialId": 1
    ///     {
    ///         "name": C# tutorial.pdf
    ///         "HyperLink": https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/
    ///     }
    ///     
    ///
    /// </remarks>
    /// <response code="200">Returns history of new material</response>
    /// <response code="400">Update fail</response>
    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [PermissionAuthorize("Modify", "Full access")]
    public async Task<IActionResult> UpdateMaterial(MaterialViewModel materialViewModel)
    {
        string errorMessage = "";
        bool status = false;

        try
        {
            _materialService.UpdateMaterial(materialViewModel);
            _materialService.Save();
            status = true;
        }
        catch (Exception Ex)
        {
            status = false;
            errorMessage = Ex.Message;
        }

        return Ok(new
        {
            status = status,
            errorMessage = errorMessage
        });
    }


    /// <summary>
    /// Delete material by id
    /// </summary>
    /// <param name="meterialId">This is a id of lesson material</param>
    /// <returns>Success or fail base on status code</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     
    ///     1
    ///
    /// </remarks>
    /// <response code="200">Return success message</response>
    /// <response code="400">Return fail message</response>
    [HttpDelete("{meterialId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [PermissionAuthorize("Delete while viewing", "Full access")]
    public async Task<IActionResult> DeleteLessonMaterial(long meterialId)
    {
        string errorMessage = "";
        bool status = false;

        try
        {
            _materialService.DeleteMaterial(meterialId);
            _materialService.Save();
            status = true;
        }
        catch (Exception ex)
        {
            status = false;
            errorMessage = ex.Message;
        }

        return Ok(new
        {
            status = status,
            errorMessage = errorMessage

        });
    }


    //------------------------------------------FIGMA 6-------------------------------------------------------

    /// <summary>
    /// Search list of syllabus 
    /// </summary>
    /// <param name="syllabusName">This is a name of syllabus</param>
    /// <returns>List of syllabus</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     GET 
    ///     "syllabusName": Linux 
    ///
    /// </remarks>
    /// <response code="200">Returns a list of syllabus</response>
    /// <response code="404">If the list is null</response>
    [HttpGet()]
    [ProducesResponseType(typeof(List<SearchSyllabusViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [PermissionAuthorize("View", "Full access")]
    public IActionResult SearchSyllabuses(string syllabusName)
    {
        string mess;
        var syllabuses = _syllabusService.SearchSyllabusByName(syllabusName);
        if (syllabuses.Count == 0)
        {
            return new JsonResult(new
            {
                mess = "Syllabus not found!"
            });
        }
        return Ok(syllabuses);
    }


    /// <summary>
    /// Create a new training program 
    /// </summary>
    /// <param name="newProgram">This is a object view model of training program</param>
    /// <returns>Success or fail base on status code</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST 
    ///     
    ///         {
    ///             "name":"Foundation C#",
    ///            "syllabi": [
    ///                {   "idSyllabus":1,  "numberOrder":1 },
    ///                {   "idSyllabus":2,  "numberOrder":2 }
    ///             ]
    ///          }
    ///     
    ///
    /// </remarks>
    /// <response code="200">Return success message</response>
    /// <response code="400">Return fail message</response>
    [HttpPost()]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [PermissionAuthorize("Create", "Full access")]
    public IActionResult CreateTrainingProgram([FromBody] ProgramViewModel newProgram)
    {

        string errorMessage = "";
        bool status = false;

        //Coding session

        try
        {

            var usernameClaim = TokenHelpers.ReadToken(HttpContext).Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            string username = usernameClaim.Value;

            newProgram.createdBy = username;

            List<TrainingProgram> trainingPrograms = new List<TrainingProgram>();
            var result = _trainingProgramService.CreateTrainingProgram(newProgram);
            _trainingProgramService.Save();
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


}