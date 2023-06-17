using BAL.Models;
using DAL.Entities;
using FRMAPI.Helpers;
using BAL.Authorization;
using DAL.Infrastructure;
using DAL.Infrastructure;
using System.Security.Claims;
using BAL.Services.Interfaces;
using BAL.Services.Interfaces;
using BAL.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using static DAL.Entities.Class;
using DAL.Repositories.Interfaces;
using BAL.Validators;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FRMAPI.Controllers;
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[ApiController]
public class ClassController : ControllerBase
{
  public readonly IClassService _class;
  List<Class> classDetails = new List<Class>();
  List<TrainingProgram> trainingProgram = new List<TrainingProgram>();
  List<ClassTrainee> classTrainee = new List<ClassTrainee>();
  private readonly IClassService _service;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserService _userService;
  private IClassRepository _classRepository;
  public ClassController(IClassService service, IUnitOfWork unitOfWork, IClassService classes, IUserService userService, IClassRepository classRepository)
  {
    _class = classes;
    _service = service;
    _unitOfWork = unitOfWork;
    _userService = userService;
    _classRepository = classRepository;
  }





  //------------------------------------------FIGMA 7-------------------------------------------------------//

  #region GetClasses
  /// <summary>
  /// UC7-001
  /// Get Class Detail by ID
  /// </summary>
  /// <param name="Id">Id of the training class program </param>
  /// <returns>Get detail of the class program and display on screen</returns>
  /// <response code="400">If the list is null</response>
  /// <response code="200">View Class Detail by ID</response>
  ///  <remarks>
  /// Sample request:
  ///
  ///     GET 
  ///     "Id": 1
  ///
  /// </remarks>
  [HttpGet("{Id}")]
  [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
  [PermissionAuthorize(new string[] { "View" })]
  public async Task<IActionResult> GetClass([FromRoute] long Id)
  {
    string errorMessage = "";
    bool status = false;

    //Coding session

    ClassDetailViewModel result = null;
    try
    {

      result = await _service.GetDetail(Id);
      if (result == new ClassDetailViewModel())
      {
        status = false;
      }
      else
      {
        status = true;
      }
    }
    catch (Exception Ex)
    {
      status = false;
      errorMessage = Ex.Message;
    }

    //End coding session

    return Ok(new
    {
      class_details = status ? result : null,
      status = status,
      errorMessage = errorMessage,
    });

  }
  #endregion


    #region GetAttendee
    /// <summary>
    /// UC7-003
    /// Get training class’s attendee list
    /// </summary>
    /// <param name="IdClass">Id of the training class program </param>
    /// <param name="PageSize">Page Size </param>
    /// <returns>Get attendee list of the class program and display on screen</returns>
    /// <response code="400">If the list is null</response>
    /// <response code="200">Get training class’s attendee list</response>
    ///  <remarks>
    /// Sample request:
    ///
    ///     GET 
    ///     "Id": 1
    ///
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(List<AttendeeInClassViewModels>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [PermissionAuthorize(new string[] { "View" })]

    public async Task<IActionResult> GetClassAttendee([FromQuery] long IdClass, int PageSize)
    {
        bool status = false;
        int PageNumber = 1;
        //Coding session
        var result = await _service.GetClassAttendee(IdClass, PageNumber, PageSize);
        status = result.Count > 0;
        //End coding session

    return new JsonResult(new
    {
      attendeeList = status ? result : null,
      status = status,
      errorMessage = status ? "" : "No more attendee yet!"
    });
  }
  #endregion

  #region GetClassTraining
  /// <summary>
  /// UC7-002
  /// Get class’s training program
  /// </summary>
  /// <returns>Full information display on the page</returns>
  ///<response code="400">If the list is null</response>
  /// <response code="200">Get class’s training program</response>
  ///  <remarks>
  /// <remarks>
  /// Sample request:
  ///
  ///     GET   
  ///     {
  ///     "id": 0,
  ///     "trainingProgramName": "string",
  ///     "duration": "string",
  ///     "modifiedBy": "string",
  ///     "modifiedOn": "2022-10-18T11:35:24.721Z",
  ///     "sysllabus": 
  ///     
  ///     "id": 0,
  ///     "name": "string",
  ///     "version": 0,
  ///     "status": 0,
  ///     "modifiedOn": "2022-10-18T11:35:24.721Z",
  ///     "classSession": 
  ///        
  ///     "id": 0,
  ///     "name": "string",
  ///     "unit": 
  ///            
  ///     "id": 0,
  ///     "name": "string",
  ///     "classLeson": 
  ///                
  ///     "id": 0,
  ///     "name": "string",
  ///     "duration": 0,
  ///     "outputStandard": "string",
  ///     "deliveryType": "string",
  ///     "formatType": "string"
  ///                
  ///              
  ///     "classAdmiin": 
  ///                
  ///     "id": 0,
  ///     "name": "string",
  ///     "location": "string",
  ///     "image": "string"
  ///              }
  /// </remarks>
  [HttpGet("{Id}")]

    [ProducesResponseType(typeof(ClassTrainingProgamViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [PermissionAuthorize(new string[] { "View" })]

    public async Task<IActionResult> GetClassTraining(long Id)
    {
        bool status = true;
        //Coding session


        var result = await _service.GetTrainingProgram(Id);
        status = result != new ClassDetailTrainingViewModel();
        //End coding session

    return new JsonResult(new
    {
      classList = status ? result : null,
      status = status,
      errorMessage = status ? "" : "No training program in this class"
    });
  }
  #endregion

    #region Delete
    /// <summary>
    /// UC7-005
    /// Delete training class
    /// </summary>
    /// <param name="id">Id of the training class program </param>
    /// <returns>Delete training program</returns>
    /// ///<response code="400">If the list is null</response>
    /// <response code="200">Delete training class</response>
    /// ///  <remarks>
    /// Sample request:
    ///
    ///     DELETE 
    ///     "Id": 1
    ///
    /// </remarks>
    [HttpDelete("{Id}")]
    [PermissionAuthorize(new string[] { "Delete while viewing" })]

    public async Task<IActionResult> Delete(long Id)
    {
        string errorMessage = "";
        bool status = false;
        string mess;


    status = true;
    //End coding session

    var result = new List<ClassViewModel>();
    try
    {
      status = await _service.Delete(Id);
      mess = "Delete Complete";
    }
    catch (Exception ex)
    {
      status = false;
      mess = "Class is Active";
    }

    //End coding session

    return new JsonResult(new
    {
      status = status,
      mess
    });
  }
  #endregion

  #region DuplicateClass
  /// <summary>
  /// UC7-006
  /// Duplicate training class
  /// </summary>
  /// <param name="id">Id of the training class program </param>
  /// <returns>Duplicate the program with full detail</returns>
  /// <response code="400">If the list is null</response>
  /// <response code="200">Duplicate training class</response>
  /// <remarks>
  /// Sample request:
  ///
  ///     POST 
  ///     "classes": [
  ///     {
  ///     "id": 0,
  ///     "classCode": null,
  ///     "status": 0,
  ///     "startTimeLearning": null,
  ///     "endTimeLearing": null,
  ///     "reviewedBy": null,
  ///     "reviewedUser": null,
  ///     "reviewedOn": null,
  ///     "createdBy": 0,
  ///     "createdUser": null,
  ///     "createdOn": "0001-01-01T00:00:00",
  ///     "approvedBy": null,
  ///     "approvedUser": null,
  ///     "approvedOn": null,
  ///     "plannedAtendee": null,
  ///     "actualAttendee": null,
  ///     "acceptedAttendee": null,
  ///     "currentSession": null,
  ///     "currentUnit": null,
  ///     "startYear": null,
  ///     "startDate": null,
  ///     "endDate": null,
  ///     "classNumber": 0,
  ///     "idProgram": null,
  ///     "trainingProgram": null,
  ///     "idTechnicalGroup": null,
  ///     "classTechnicalGroup": null,
  ///     "idFSU": null,
  ///     "fsoftUnit": null,
  ///     "idFSUContact": null,
  ///     "fsuContactPoint": null,
  ///     "idStatus": 0,
  ///     "classStatus": null,
  ///     "idSite": null,
  ///     "classSite": null,
  ///     "idUniversity": null,
  ///     "classUniversityCode": null,
  ///     "idFormatType": null,
  ///     "classFormatType": null,
  ///     "idProgramContent": null,
  ///     "classProgramCode": null,
  ///     "idAttendeeType": null,
  ///     "attendeeType": null,
  ///     "locations": null,
  ///     "classSelectedDates": null,
  ///     "classUpdateHistories": null,
  ///     "classTrainees": null,
  ///     "classAdmins": null,s
  ///      "classMentors": null
  ///     }
  ///     ]
  /// </remarks>

    /// <response code="400">If the list is null</response>
    /// <response code="200">Get the Program required for duplicate</response>
    [HttpPost("{Id}")]
    [ProducesResponseType(typeof(Class), StatusCodes.Status200OK)]
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
      result = await _service.Duplicate(Id);
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
      newClassId = result,
      status = status,
      errorMessage = errorMessage
    });
  }
  #endregion

    #region De-ActivateClasses
    /// <summary>
    /// UC7-007
    /// Deactivate training class details
    /// </summary>
    /// <param name="id">Id of the training class program </param>
    /// <returns>De-Activate or Activate the program</returns>
    ///  <response code="400">If the list is null</response>
    /// <response code="200">Deactivate training class details</response>
    ///   <remarks>
    /// Sample request:
    ///
    ///     PUT 
    ///     "Id": 1
    ///     "Status" : 0
    /// </remarks>
    [HttpPut("{Id}")]
    [PermissionAuthorize(new string[] { "Modify " })]

    public async Task<IActionResult> De_Activate(int Id)
    {
        string errorMessage = "";
        bool status = false;

    try
    {
      status = await _service.DeActivate(Id);
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
  #endregion
  // /// <summary>
  // /// UC7-004
  // /// Edit training class
  // /// </summary>
  // /// <param name="id">Id of the training class program </param>
  // /// <param name="classCode">classCode of the training class program </param> 
  // /// <returns>Edit the program detail</returns>
  // /// <response code="400">If the list is null</response>
  // /// <response code="200">Edit training class</response>
  // ///  <remarks>
  // ///Sample request:
  // ///
  // ///     PUT
  // ///     "classes": [
  // ///     {
  // ///     "id": 0,
  // ///     "classCode": null,
  // ///     "status": 0,
  // ///     "startTimeLearning": null,
  // ///     "endTimeLearing": null,
  // ///     "reviewedBy": null,
  // ///     "reviewedUser": null,
  // ///     "reviewedOn": null,
  // ///     "createdBy": 0,
  // ///     "createdUser": null,
  // ///     "createdOn": "0001-01-01T00:00:00",
  // ///     "approvedBy": null,
  // ///     "approvedUser": null,
  // ///     "approvedOn": null,
  // ///     "plannedAtendee": null,
  // ///     "actualAttendee": null,
  // ///     "acceptedAttendee": null,
  // ///     "currentSession": null,
  // ///     "currentUnit": null,
  // ///     "startYear": null,
  // ///     "startDate": null,
  // ///     "endDate": null,
  // ///     "classNumber": 0,
  // ///     "idProgram": null,
  // ///     "trainingProgram": null,
  // ///     "idTechnicalGroup": null,
  // ///     "classTechnicalGroup": null,
  // ///     "idFSU": null,
  // ///     "fsoftUnit": null,
  // ///     "idFSUContact": null,
  // ///     "fsuContactPoint": null,
  // ///     "idStatus": 0,
  // ///     "classStatus": null,
  // ///     "idSite": null,
  // ///     "classSite": null,
  // ///     "idUniversity": null,
  // ///     "classUniversityCode": null,
  // ///     "idFormatType": null,
  // ///     "classFormatType": null,
  // ///     "idProgramContent": null,
  // ///     "classProgramCode": null,
  // ///     "idAttendeeType": null,
  // ///     "attendeeType": null,
  // ///     "locations": null,
  // ///     "classSelectedDates": null,
  // ///     "classUpdateHistories": null,
  // ///     "classTrainees": null,
  // ///     "classAdmins": null,s
  // ///      "classMentors": null
  // ///     }
  // ///     ]
  // /// </remarks>
  // /// <response code="400">If the list is null</response>
  // /// <response code="200">Get the Class required for edit</response>
  // /// <returns>Search for ID of the program and proceed to make changes to the program</returns>
  // [HttpPut("{Id}")]

  //   [ProducesResponseType(typeof(Class), StatusCodes.Status200OK)]
  //   [ProducesResponseType(StatusCodes.Status400BadRequest)]
  //   public async Task<IActionResult> EditClass(long Id)
  //   {
  //       string errorMessage = "";
  //       bool status = false;
  //       List<Class> classes = new List<Class>();
  //       classes.Add(new Class());
  //       status = true;
  //       //End coding session

  //       return new JsonResult(new
  //       {
  //           classes = classes,
  //           status = status,
  //           errorMessage = errorMessage
  //       });
  //   }




    //------------------------------------------FIGAMA 7.1----------------------------------------------------//
    #region API Get Classes
    /// <summary>
    /// đây là API Get Classes trong đó có search by keyword, filter và sort phần tử ở trong trang
    /// </summary>
    ///
    /// <param name="key">tìm kiếm theo từ khóa nhập vào từ bàn phím nếu trùng sẽ ra kết quả</param>
    /// <param name="sortBy">với duration từ a => z thì nhấn Duration ASC ,với duration từ z => a thì nhấn Duration DESC,với name từ a => z thì nhấn Name ASC, với duration từ z => a thì nhấn Name DESC,...</param>
    /// <param name="pageSize">đây là số phần tử có trong 1 trang ở đây tụi mình defaul là 3 phần tử trong 1 trang</param>
    /// <param name="classTimeFrom">có hay không cũng được nó sẽ tìm từ ngày bắt đầu (yyyy-mm-dd)</param>
    /// <param name="classTimeTo">có hay không cũng được nó sẽ tìm trước ngày kết thúc (yyyy-mm-dd)</param>
    /// <param name="pageNumber">có hay không cũng được nó sẽ tìm trước ngày kết thúc (yyyy-mm-dd)</param>
    /// <param name="status">có đang hoạt động hay không (-1 là tất cả, 0 là không hoạt động, 1 là hoạt động)</param>
    /// <returns>trả về một danh sách các phần tử trong list of class</returns>
    /// <remarks>
    /// 
    /// Sample request
    /// 
    ///     GET
    ///     "Key":"HCM22_FR_DevOps_01"
    ///     {
    ///         "Class Code":["HCM22_FR_DevOps_01","HCM22_FR_DevOps_01"]
    ///         "Traning Program":["C# basic program","DevOps Foundation_1"]
    ///         "Duration":["36 days","12 days"]
    ///         "Attendee":["Fresher","Fresher"]
    ///         "Current Module":["Design Figma","Design SRS"]
    ///         "Status":["Active","Active"]
    ///         "Location":["Ho Chi Minh","Ho Chi Minh"]
    ///         "FSU":["FHM","FHM"]
    ///     }
    /// </remarks>
    /// <response code="200">trả về list of Classes</response>
    /// <response code="404">nếu list null</response>
    [HttpGet]
    [PermissionAuthorize("View", "Full Access")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetClasses([FromQuery] List<string> key, [FromQuery] List<string> sortBy, [FromQuery] List<long> location, [FromQuery] DateTime? classTimeFrom, [FromQuery] DateTime? classTimeTo, [FromQuery] List<long> classTime, [FromQuery] List<long> status, [FromQuery] List<long> attendee, [FromQuery] int FSU, [FromQuery] int trainer, int pageNumber, int pageSize)
    {
        int sophantu = 0;
        int sotrang = 1;
        if (pageSize == 0)
        {
            pageSize = 3;
        }
        if (pageNumber == 0)
        {
            pageNumber = 1;
        }


    bool statusResult = false;
    string erroMessage = "";
    List<Class> classes = new List<Class>();
    List<ClassModel> result = new List<ClassModel>();
    try
    {
      classes = _service.GetClassess(key, sortBy, location, classTimeFrom, classTimeTo, classTime, status, attendee, FSU, trainer, pageNumber, pageSize);
      result = _service.ShowClasses(classes);
      int a = _service.CountClass(key, location, classTimeFrom, classTimeTo, classTime, status, attendee, FSU, trainer);
      if (classes.Count != 0)
      {
        if (a % pageSize == 0)
        {
          sotrang = a / pageSize;
          sophantu = pageSize;
        }
        else
        {
          sotrang = a / pageSize + 1;
          if (pageNumber < sotrang)
          {
            sophantu = pageSize;
          }
          else
          {
            sophantu = a % pageSize;
          }
        }
      }

      statusResult = true;
    }
    catch (Exception ex)
    {
      statusResult = false;
      erroMessage = ex.Message;

    }

        return new JsonResult(new
        {
            PageSize = pageSize,
            NumberOfPage = sotrang,
            NumberOfElementInPage = sophantu,
            PageNumber = pageNumber,
            statusResult = statusResult,
            erroMessage = erroMessage,
            data = result
        });
    }
    #endregion
    #region API Import Classes
    /// <summary>
    /// đây là API tạo mới 1 classes
    /// </summary>
    /// <param name="file">đây là file xls/xlxs để import</param>
    /// <returns>hệ thống sẽ trả về danh sách </returns>
    /// <remarks>
    /// Sample request
    /// 
    ///      GET
    ///      {
    ///      "File(xlsx,xls)": ["xxx.xls"],
    ///      "Import template": download,
    ///      "Scanning": ["Class code"],
    ///      "Duplicate handle": ["Allow"]
    ///      }
    /// </remarks>
    /// <response code="200">trả về list of Classes</response>
    /// <response code="400">nếu không import được</response>
    [HttpPost]
    [PermissionAuthorize("Create", "Full Access")]
    [RequestFormLimits (MultipartBodyLengthLimit = 52428800)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportCLasses([FromForm] ImportClassRequest request)
    {
        ImportClassResponse response = new ImportClassResponse();
        try
        {
            string path = request.File.FileName;
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.CreateNew))
                {
                    await request.File.CopyToAsync(stream);
                }

                response = await _service.ImportCLasses(request, path);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
        }catch
        {
            return BadRequest();
        }
        return Ok(response);
    }
    #endregion


  //------------------------------------------FIGMA 8-------------------------------------------------------//


  #region GetClassByClassCode
  /// <summary>
  /// Get Information of Class
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     GET
  ///      {
  ///       "classCode": "HCM22_FR_DevOps_01",
  ///     }
  /// </remarks>
  /// <returns>
  /// List information of class
  /// </returns>
  /// 
  /// <response code="200">Show message successfully and return View Class page</response>
  /// <response code="404">Show message error</response>
  [ProducesResponseType(typeof(ClassSearchViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
  [PermissionAuthorize(new string[] { "Modify" })]
  [HttpGet]
  public async Task<JsonResult> GetClassByClassCode([FromQuery] string? classCode)
  {

    string errorMessage = "";
    bool status = false;

    //Coding session

    List<ClassSearchViewModel> result = new List<ClassSearchViewModel>();
    try
    {
      result = await _service?.GetClassByCodeService(classCode);
      return new JsonResult(new
      {
        status = true,
        data = result
      });
    }
    catch (Exception ex)
    {
      //status = false;
      //erroMessage = ex.Message;
      throw;
    }

  }



  #endregion

  #region GetTrainingProgramByName

  /// <summary>
  /// Get Class Training Program
  /// </summary>
  /// <returns>Full information display on the page</returns>
  ///<response code="400">If the list is null</response>
  /// <response code="200">Get class’s training program</response>
  ///  <remarks>
  /// Sample request:
  ///
  ///     GET   
  ///       {
  ///           "trainingProgramName": "DevOps Foudation",
  ///       }
  /// </remarks>
  /// <returns>
  /// List information of training program
  /// </returns>
  /// 
  /// <response code="200">Show message successfully and return View Class page</response>
  /// <response code="404">Show message error</response>
  [ProducesResponseType(typeof(ClassTrainingProgamViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
  [PermissionAuthorize(new string[] { "Modify" })]
  [HttpGet]
  public async Task<JsonResult> SearchTraningProgramByName(string? name)
  {
    bool status = false;
    string erroMessage = "";
    List<ClassTrainingProgamViewModel> data = new List<ClassTrainingProgamViewModel>();
    try
    {
      data = await _service.GetClassTrainingProgams(name);
      return new JsonResult(new
      {
        status = true,
        data = data
      });
    }
    catch (Exception ex)
    {
      //status = false;
      //erroMessage = ex.Message;
      throw;
    }

  }

  #endregion


  #region SaveAsDraft
  /// <summary>
  /// Create Class and save with status "Draft" 
  /// </summary>
  /// <remarks>
  /// Sample request:
  /// 
  ///      POST
  ///        {
  ///           "id": 1,
  ///           "name": "Fresher Develop Operation",
  ///           "status": 0,
  ///           "startTimeLearning": "09:00:00",
  ///           "endTimeLearing":"11:00:00",
  ///           "reviewedBy": 2,
  ///           "reviewedOn": "2022-11-05",
  ///           "approvedBy": 2,
  ///           "approvedOn": "2022-11-05",
  ///           "plannedAtendee": 1,
  ///           "actualAttendee": 1,
  ///           "acceptedAttendee": 1,
  ///           "currentSession": 1,
  ///           "currentUnit": 1,
  ///           "startYear": 1,
  ///           "startDate": "2022-10-28",
  ///           "endDate": "2022-10-28",
  ///           "classNumber": 1,
  ///           "idProgram": 1,
  ///           "idTechnicalGroup": 1,
  ///           "idFSU": 1,
  ///           "idFSUContact": 1,
  ///           "idSite": 1,
  ///           "idUniversity": 1,
  ///           "idFormatType": 1,
  ///           "idProgramContent": 1,
  ///           "idAttendeeType": 1,
  ///           "activeDate": [
  ///             "2022-10-01",
  ///             "2022-10-02",
  ///             "2022-10-03"
  ///             ], 
  ///           "idLocation": [
  ///              1,2
  ///              ],
  ///           "idTrainee": [
  ///             3,2
  ///              ],
  ///           "idAdmin": [
  ///             4
  ///              ],
  ///           "idMentor": [
  ///             3
  ///              ],
  ///            "syllabi": [
  ///             {
  ///               "idSyllabus": 1,
  ///               "numberOrder": 1
  ///             },
  ///             {
  ///               "idSyllabus": 2,
  ///               "numberOrder": 2
  ///             }
  ///             ]
  ///        }
  /// </remarks>
  /// <returns>
  /// Create a new class and save to the database with status “draft”
  /// </returns>
  /// 
  /// <response code="200">Show message successfully and return View Class page</response>
  /// <response code="404">Show message error</response>
  [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
  [PermissionAuthorize(new string[] { "Modify" })]
  [HttpPost]
  public async Task<IActionResult> SaveAsDraft([FromBody] UpdateClassViewModel SaveAsDraftClass)
  {
    ClassValidatorForSaveAsDraft validator = new ClassValidatorForSaveAsDraft();
    try
    {
      var usernameClaim = TokenHelpers.ReadToken(HttpContext).Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
      string username = usernameClaim.Value;

      SaveAsDraftClass.CreatedBy = _userService.GetUser(username).ID;

      var validation = validator.Validate(SaveAsDraftClass);
      if (!validation.IsValid)
      {
        var errors = new List<string>();
        foreach (var error in validation.Errors)
        {
          errors.Add(error.ErrorMessage);
        }
        return BadRequest(new
        {
          status = false,
          Message = errors
        });
      }

      await _service.SaveAsDraft(SaveAsDraftClass);
      _service.Save();
      return new JsonResult(new
      {
        Sucess = true,
        Message = "Save as draft Success"

      });
    }
    catch (Exception ex)
    {
      return BadRequest(new
      {
        Sucess = false,
        Message = ex.Message
      });
    }
  }
  #endregion

  #region Save
  /// <summary>
  /// Create Class and save with status "Reviewing"
  /// </summary>
  /// <remarks>
  /// Sample request:
  /// 
  ///      POST
  ///        {
  ///           "id": 1,
  ///           "name": "Fresher Develop Operation",
  ///           "status": 0,
  ///           "startTimeLearning": "09:00:00",
  ///           "endTimeLearing":"11:00:00",
  ///           "reviewedBy": 2,
  ///           "reviewedOn": "2022-11-05",
  ///           "approvedBy": 2,
  ///           "approvedOn": "2022-11-05",
  ///           "plannedAtendee": 1,
  ///           "actualAttendee": 1,
  ///           "acceptedAttendee": 1,
  ///           "currentSession": 1,
  ///           "currentUnit": 1,
  ///           "startYear": 1,
  ///           "startDate": "2022-10-28",
  ///           "endDate": "2022-10-28",
  ///           "classNumber": 1,
  ///           "idProgram": 1,
  ///           "idTechnicalGroup": 1,
  ///           "idFSU": 1,
  ///           "idFSUContact": 1,
  ///           "idSite": 1,
  ///           "idUniversity": 1,
  ///           "idFormatType": 1,
  ///           "idProgramContent": 1,
  ///           "idAttendeeType": 1,
  ///           "activeDate": [
  ///             "2022-10-01",
  ///             "2022-10-02",
  ///             "2022-10-03"
  ///             ],
  ///           "idLocation": [
  ///              1,2
  ///              ],
  ///           "idTrainee": [
  ///             3,2
  ///              ],
  ///           "idAdmin": [
  ///             4
  ///              ],
  ///           "idMentor": [
  ///             3
  ///              ],
  ///            "syllabi": [
  ///             {
  ///               "idSyllabus": 1,
  ///               "numberOrder": 1
  ///             },
  ///             {
  ///               "idSyllabus": 2,
  ///               "numberOrder": 2
  ///             }
  ///             ]
  ///        }
  /// </remarks>
  /// <returns>
  /// Create a new class and save to the database with status “Reviewing”
  /// </returns>
  /// 
  /// <response code="200">Show message successfully and return View Class page</response>
  /// <response code="404">Show message error</response>
  [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
  [PermissionAuthorize(new string[] { "Modify" })]
  [HttpPost]
  public async Task<IActionResult> Edit([FromBody] UpdateClassViewModel classViewModel)
  {
    ClassValidatorForEdit validator = new ClassValidatorForEdit();
    try
    {
      var usernameClaim = TokenHelpers.ReadToken(HttpContext).Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
      string username = usernameClaim.Value;

      classViewModel.CreatedBy = _userService.GetUser(username).ID;

      if (classViewModel == null)
        throw new Exception("Api's information has been corrupted, please try again or contact developer for more support");

      var validation = validator.Validate(classViewModel);
      if (!validation.IsValid)
      {
        var errors = new List<string>();
        foreach (var error in validation.Errors)
        {
          errors.Add(error.ErrorMessage);
        }
        return BadRequest(new
        {
          status = false,
          Message = errors
        });
      }

      await _service.UpdateClass(classViewModel);
      _service.Save();
      return new JsonResult(new
      {
        Sucess = true,
        Messasge = "Edit Success"
      });
    }
    catch (Exception ex)
    {
      return BadRequest(new
      {
        Sucess = false,
        Message = ex.Message
      });
    }

  }

  #endregion

}