using BAL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.Entities.User;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> FindByOtp(string Otp);
        Task<User> FindByEmail(string email);
        Task<bool> InvalidateAccessToken(string userName);
        Task<AccountViewModel> Login(string email, string password);
        Task<UserViewModel> Add(UserAccountViewModel user);
        Task<UserViewModel> Edit(long id, string Fullname, DateTime Dob, char Gender, string Email,string Address, int Status, long IdRole);
        Task<UpLoadExcelFileResponse> ImportUser(UpLoadExcelFileRequest request, string path);
        Task<bool> DeActivate(long id);
        Task<UserViewModel> ChangleRole(long id, long IdRole);
        Task<bool> Delete(long id);
        List<UserViewModel> GetAll(string? keyword, List<string>? sortby, int PAGE_SIZE, int PAGE_NUMBER);
        Task<User> GetById(long id);
        #region Other groups
        User GetByID(long id);
        Task<IEnumerable<TrainerViewModel>> GetTrainers();
        #region Group 5 - Authentication & Authorization
        public User GetUser(string username);
        public User GetUser(string username, string password);
        #endregion
        UserViewModel GetUserViewModelById(long userId);
        #endregion
        #region Functions
        void Save();
        void SaveAsync();
        #endregion
    }
}
