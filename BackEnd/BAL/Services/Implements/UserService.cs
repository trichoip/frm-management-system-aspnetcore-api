using AutoMapper;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using static DAL.Entities.User;
using System.Data;
using ExcelDataReader;
using DAL;
using Microsoft.AspNetCore.Rewrite;
using MimeKit;
using System.Drawing.Printing;

namespace BAL.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly FRMDbContext _context;
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, FRMDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        #region GetAllUser
        public List<UserViewModel> GetAll(string? keywords, List<string>? sortBy, int PAGE_SIZE, int page = 1)
        {
            var newList = new List<UserViewModel>();
            var list = _userRepository.GetAllUser(keywords, sortBy, PAGE_SIZE, page);
            foreach (var user in list)
            {
                UserViewModel viewModel = new UserViewModel();
                viewModel.ID = user.ID;
                viewModel.Fullname = user.FullName;
                viewModel.DateOfBirth = user.DateOfBirth;
                viewModel.Gender = user.Gender;
                viewModel.Phone = user.Phone;
                viewModel.Email = user.Email;
                viewModel.Address = user.Address;
                viewModel.IdRole = user.IdRole;
                viewModel.Status = user.Status;
                newList.Add(viewModel);
            }
            return newList.ToList();
        }
        #endregion

        #region Deactivate
        public async Task<bool> DeActivate(long id)
        {
            return await _userRepository.DeActivate(id);
        }
        #endregion

        #region Edit
        public async Task<UserViewModel> Edit(long id, string Fullname, DateTime Dob, char Gender, string Email, string Address, int Status, long IdRole)
        {
            UserViewModel result = _mapper.Map<UserViewModel>(await _userRepository.Edit(id, Fullname, Email, Dob, Address, Gender, Status, IdRole));
            SaveAsync();
            return result;
        }
        #endregion

        #region ChangeRole
        public async Task<UserViewModel> ChangleRole(long id, long IdRole)
        {
            UserViewModel result = _mapper.Map<UserViewModel>(await _userRepository.ChangleRole(id, IdRole));
            SaveAsync();
            return result;
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(long id)
        {
            return await _userRepository.Delete(id);
        }
        #endregion

        #region Login
        public async Task<AccountViewModel> Login(string email, string password)
        {
            var result = _mapper.Map<AccountViewModel>(await _userRepository.Login(email, password));
            return result;
        }
        #endregion

        #region FindByEmail
        public async Task<User> FindByEmail(string email)
        {
            var result = await _userRepository.FindByEmail(email);
            return result;
        }
        #endregion

        #region FindByOtp
        public async Task<User> FindByOtp(string Otp)
        {
            return await _userRepository.FindByOtp(Otp);
        }
        #endregion

        #region InvalidateToken
        public async Task<bool> InvalidateAccessToken(string userName)
        {
            if (await _userRepository.InvalidateAccessToken(userName))
            {
                SaveAsync();
                return true;
            }
            return false;
        }
        #endregion

        #region AddUser
        public async Task<UserViewModel> Add(UserAccountViewModel user)
        {
            var userAccount = _mapper.Map<User>(user);
            var result = _mapper.Map<UserViewModel>(await _userRepository.Add(userAccount));
            return result;
        }
        #endregion

        #region ImportUser
        public async Task<User.UpLoadExcelFileResponse> ImportUser(User.UpLoadExcelFileRequest request, string path)

        {
            UpLoadExcelFileResponse response = new UpLoadExcelFileResponse();
            var user1 = new List<User>();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (request.File.FileName.ToLower().Contains(".xlsx"))
                {
                    FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                    DataSet dataset = reader.AsDataSet(
                        configuration: new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                    for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                    {
                        User user = new User();

                        user.UserName = dataset.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[0]).ToString() : "-1";
                        user.Password = dataset.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[1]).ToString() : "-1";
                        user.FullName = dataset.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[2]).ToString() : "-1";
                        user.DateOfBirth = dataset.Tables[0].Rows[i].ItemArray[3] != null ? DateTime.Parse(dataset.Tables[0].Rows[i].ItemArray[3].ToString()) : DateTime.Parse("0000-00-00");
                        user.Gender = dataset.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToChar(dataset.Tables[0].Rows[i].ItemArray[4]) : 'N';
                        user.Phone = dataset.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[5]).ToString() : "-1";
                        user.Email = dataset.Tables[0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[6]).ToString() : "-1";
                        user.Address = dataset.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataset.Tables[0].Rows[i].ItemArray[7]).ToString() : "-1";
                        user.Status = dataset.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[8]) : -1;
                        user.IdRole = dataset.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[9]) : -1;


                        //user.ID = dataset.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToInt32(dataset.Tables[0].Rows[i].ItemArray[10]) : -1;
                        user1.Add(user);

                    }
                    stream.Close();
                    if (user1.Count > 0)
                    {
                        foreach (var user in user1)
                        {
                            await _userRepository.Add(user);
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Incorrect File";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _context.DisposeAsync();
            }
            return response;
        }
        #endregion

        #region Functions
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }
        #endregion

        #region Other groups
        public User GetByID(long id)
        {
            return _userRepository.Get(id);
        }

        public async Task<User> GetById(long id)
        {
            return await _userRepository.GetById(id);
        }

        #region Group 5 - Authentication & Authorization

        public User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        public User GetUser(string username, string password)
        {
            return _userRepository.GetUser(username, password);

        }

        public async Task<IEnumerable<TrainerViewModel>> GetTrainers()
        {
            return _mapper.Map<IEnumerable<TrainerViewModel>>(await _userRepository.GetTrainers());
        }

        public UserViewModel GetUserViewModelById(long userId)
        {
            return _mapper.Map<UserViewModel>(_userRepository.GetUserById(userId));
        }
        #endregion

        #endregion
    }
}
