using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByOtp(string Otp);
        Task<User> FindByEmail(string email);
        Task<bool> InvalidateAccessToken(string userName);
        Task<User> Login(string email, string password);
        IQueryable<User> GetAllUser(string? keyword, List<string>? sortby, int PAGE_SIZE, int PAGE_NUMBER);
        Task<bool> Delete(long id);
        Task<User> GetById(long id);
        Task<User> Edit(long id, string Fullname, string Email, DateTime Dob, string Address, char Gender, int Status, long IdRole);
        Task<User> Add(User user);
        Task<bool> DeActivate(long id);
        Task<User> ChangleRole(long id, long IdRole);
        User GetUserById(long UserId);
        #region Other groups
        User Get(long id);
        Task<IEnumerable<User>> GetTrainers();
        #region Group 5 - Authentication & Authorization
        public User GetUser(string username);
        public User GetUser(string username, string password);
        Task<User> GetUserAsync(long userId);
        #endregion
        // Team6
        User GetUser(long id);
        List<User> GetUsers(long id);
        // Team6
        //team4
        List<User> GetUsersForImport();
        void CreateUserForImport(User user);
        #endregion        
    }
}
