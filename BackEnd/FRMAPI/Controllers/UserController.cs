using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Reflection;
using DAL.Repositories.Interfaces;
using BAL.Services.Implements;
using System.Numerics;
using static DAL.Entities.User;
using BAL.Authorization;

namespace FRMAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : Controller
{
    User initUser = new User();
    List<User> detailUser = new List<User>();
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleService _roleService;
    private readonly IPermissionRightService _permissionRightService;

    public UserController(IUserService userService, IUnitOfWork unitOfWork, IRoleService roleService, IPermissionRightService permissionRightService)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _roleService = roleService;
        _permissionRightService = permissionRightService;
    }

    #region GetUser
    /// <summary>
    /// UC10-001
    /// Get all users
    /// </summary>
    /// <param name="keywords">Search by name</param>
    /// <param name="sortBy">ID ASC,ID DESC,FullName ASC,FullName DESC</param> 
    /// <param name="PAGE_SIZE">size of user</param>
    /// <param name="page">page</param>
    /// <returns></returns>
    [HttpGet]
    [PermissionAuthorize("View", "Full Access")]
    [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserAsync([FromQuery] string? keywords, [FromQuery] List<string>? sortBy, int PAGE_SIZE, int page = 1)
    {
        var result = _userService.GetAll(keywords, sortBy, PAGE_SIZE, page);
        return new JsonResult(new
        {
            result
        });
    }
    #endregion

    #region AddUser
    /// <summary>
    /// UC10-002
    /// Add a new user 
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///
    ///         {
    ///           "username": "Hoa",
    ///           "password": "123123",
    ///           "fullname": "Ngoc Hoa",
    ///           "dateOfBirth": "2022-10-31T08:30:36.640Z",
    ///           "gender": "M",
    ///           "phone": "2316843218",
    ///           "email": "hoa@gmail.com",
    ///           "address": "HCM",
    ///           "status": 1,
    ///           "idRole": 2
    ///         }
    ///               
    /// </remarks>
    /// <returns>A new user in the system</returns>
    /// <response code="200">Return user list</response>
    /// <response code="404">If the list is null</response>
    [HttpPost]
    [PermissionAuthorize(new string[] { "Create", "Full Access" })]
    [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddNewUserAsync([FromBody] UserAccountViewModel user)
    {
        string errorMessage = "";
        bool status = true;

        var result = new UserViewModel();
        try
        {
            result = await _userService.Add(user);
            if (result == null)
            {
                errorMessage = "Username or Email are exist";
                status = false;
            }
        }
        catch (Exception ex)
        {

            errorMessage = ex.Message;
        }


        return Ok(new
        {
            status = status,
            result = result,
            ErrorMessage = errorMessage
        });
    }
    #endregion

    #region ChangleUserRole
    /// <summary>
    /// UC10-005
    /// Change the user's role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="IdRole"></param>
    /// 
    /// <remarks>
    ///     Sample request:
    ///
    ///         {
    ///           "userId": 2,
    ///           "roleId": 1
    ///         }
    ///         
    /// </remarks>
    /// <returns>A user whose role has been changed</returns>
    /// <response code="200">Return users </response>
    /// <response code="400">If the list is null</response>
    [HttpPut("{id}")]
    [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeUserRoleAsync(long id, long IdRole)
    {
        string errorMessage = "";
        bool status = false;
        var result = new UserViewModel();
        try
        {
            result = await _userService.ChangleRole(id, IdRole);
            status = true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        return Ok(new
        {
            status = status,
            result = result,
            ErrorMessage = errorMessage
        });
    }
    #endregion

    #region EditUser
    /// <summary>
    /// UC10-006
    /// Edit user's information
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///
    ///         {
    ///           "id": 5
    ///           "fullname": "Trong Tan",
    ///           "dateOfBirth": "2022-10-28",
    ///           "gender": "M",
    ///           "phone": "2316843218",
    ///           "email": "no1@gmail.com",
    ///           "address": "HCM",
    ///           "status": 1,
    ///           "idRole": 4
    ///         }
    ///         
    /// </remarks>
    /// <returns>An edited user in the system</returns>
    /// <response code="200">Return users </response>
    /// <response code="400">If the list is null</response>
    [HttpPut("{id}")]
    [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditUserAsync(long id, string Fullname, DateTime Dob, char Gender, string Email, string Address, int Status, long IdRole)
    {
        string errorMessage = "";
        bool status = true;
        var result = new UserViewModel();
        try
        {
            result = await _userService.Edit(id, Fullname, Dob, Gender, Email, Address, Status, IdRole);
            if (result == null)
            {
                errorMessage = "Email are exist";
                status = false;
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        return Ok(new
        {
            status = status,
            result = result,
            ErrorMessage = errorMessage
        });
    }
    #endregion

    #region ImportUser
    /// <summary>
    /// UC10-007
    /// Import a user from a template file to the list
    /// </summary>
    /// <param name="FileName">this is file xls/xlxs to import user</param>
    /// <remarks>
    ///     Sample request:
    ///
    ///         GET       
    ///         "File(xlsx,xls)": ["xxx.xls"]
    ///         "Import template": download
    ///
    /// </remarks>
    /// <returns>return user mangement</returns>
    /// <response code="200">return User list page</response>
    /// <response code="400">If the list is null</response>
    [HttpPost]
    [PermissionAuthorize(new string[] { "Create", "Full Access" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportUser([FromForm] UpLoadExcelFileRequest request)
    {
        UpLoadExcelFileResponse response = new UpLoadExcelFileResponse();
        string Path = request.File.FileName;
        try
        {

            using (FileStream stream = new FileStream(Path, FileMode.CreateNew))
            {
                await request.File.CopyToAsync(stream);
            }


            response = await _userService.ImportUser(request, Path);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

        }
        return Ok(response);
    }

    #endregion

    #region DeactivateUser
    /// <summary>
    /// UC10-008
    /// Deactivate selected user, set user status to inactive
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    ///     Sample request:
    ///
    ///         {
    ///           "Id" : 1,
    ///           "Status": 0
    ///         }
    ///          
    /// </remarks>
    /// <returns>Return result of action and error message (if any)</returns>
    /// <response code="200">Successful message</response>
    /// <response code="404">User's account is null</response>
    [HttpPut("[action]/{id}")]
    [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
    [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateUserAsync(long id)
    {
        string errorMessage = "";
        bool status = false;

        try
        {
            status = await _userService.DeActivate(id);
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

    #region DeleteUser
    /// <summary>
    /// UC10-009
    /// Delete selected user
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    ///     Sample request:
    ///
    ///         {
    ///           "Id" : 4
    ///         }
    ///          
    /// </remarks>
    /// <returns>Return result of action and error message (if any)</returns>
    /// <response code="200">Successful message</response>
    /// <response code="404">User's account is null</response>
    [HttpDelete("{id}")]
    [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserAsync(long id)
    {
        string errorMessage = "";
        bool status = false;
        try
        {
            status = await _userService.Delete(id);
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        return new JsonResult(new
        {
            status = status
        });
    }
    #endregion 

    #region GetRole
    /// <summary>
    /// UC10-010
    /// Shows list of roles
    /// </summary>
    /// <param name="role"></param>
    /// <remarks>
    /// </remarks>
    /// <returns>A list of roles in the system</returns>
    /// <response code="200">A list of roles in the system</response>
    /// <response code="404">List of roles is null</response>
    [HttpGet]
    [PermissionAuthorize("View", "Full Access")]
    [ProducesResponseType(typeof(List<PermissionViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoleAsync()
    {
        string errorMessage = "";
        bool status = false;
        var result = new List<PermissionViewModel>();

        try
        {
            result = await _permissionRightService.GetAllRole();
            status = true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        return new JsonResult(new
        {
            Result = result,
            status = status,
            errorMessage = errorMessage
        });
    }
    #endregion 

    #region AddRole
    /// <summary>
    /// UC10-011
    /// Add new role
    /// </summary>
    /// <param name="name"></param>
    /// <remarks>
    ///     Sample request:
    ///
    ///         {
    ///           "Name" : "Fresher"
    ///         }
    ///              
    /// </remarks>
    /// <returns>A list of roles</returns>
    /// <response code="200">Return role list</response>
    /// <response code="404">If the list is null</response>
    [HttpPost]
    [PermissionAuthorize(new string[] { "Create", "Full Access" })]
    [ProducesResponseType(typeof(RoleViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddNewRoleAsync(string name)
    {
        string errorMessage = "";
        bool status = false;
        Role result = new Role();
        try
        {
            result = await _roleService.AddNewRoleAsync(name);
            status = true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        return Ok(new
        {
            status = status,
            ErrorMessage = errorMessage,
            result = result
        });
    }
    #endregion

    #region SetRolePermission
    /// <summary>
    /// UC10-012
    /// Set permission to the role
    /// </summary>   
    /// <remarks>
    ///     Sample request:
    ///
    ///         {
    ///           "idRight": 2,
    ///           "idRole": 1,
    ///           "idPermission": 4
    ///         }
    ///         
    /// </remarks>
    /// <returns>A list of roles</returns>
    /// <response code="200">Return role list</response>
    /// <response code="404">If the list is null</response>
    [HttpPut]
    [PermissionAuthorize(new string[] { "Modify", "Full Access" })]
    [ProducesResponseType(typeof(List<PermissionViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetRolePermissionAsync(PermissionViewModel p)
    {
        string errorMessage = "";
        bool status = false;
        var result = new PermissionViewModel();
        try
        {
            result = await _permissionRightService.SetPermission(p.IdRight, p.IdRole, p.IdPermission);
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
            result = result,
            errorMessage = errorMessage
        });
    }
    #endregion
}

