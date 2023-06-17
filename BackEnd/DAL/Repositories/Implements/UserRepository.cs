using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Rewrite;
using System.Linq.Dynamic.Core;

namespace DAL.Repositories.Implements
{

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly FRMDbContext _dbContext;
        private readonly IRoleRepository _role;
        private readonly IUnitOfWork _unitOfWork;
        public UserRepository(IDbFactory dbFactory, IRoleRepository role, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
            _role = role;
            _unitOfWork = unitOfWork;
        }

        #region Login
        public async Task<User> Login(string email, string password)
        {
            var result = await _dbSet.Where(x => x.Email == email).FirstOrDefaultAsync();

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, result.Password);

            if (isValidPassword)
            {
                return result;
            }

            return null;
        }
        #endregion

        #region FindByEmail
        public async Task<User> FindByEmail(string email)
        {
            var result = await _dbSet.Where(x => x.Email == email).FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region GetAllUser       
        public IQueryable<User> GetAllUser(string? keyword, List<string> sortBy, int PAGE_SIZE, int PAGE_NUMBER)
        {
            sortBy = null;
            IQueryable<User> result = null;
            if (keyword == null && sortBy == null)
            {
                result = _dbSet.Skip(PAGE_SIZE * (PAGE_NUMBER - 1)).Take(PAGE_SIZE);
                return result;
            }
            if (keyword != null && sortBy == null)
            {
                result = _dbSet.Where(x => x.FullName.Contains(keyword)).OrderByDescending(x=>x.DateOfBirth).Skip(PAGE_SIZE * (PAGE_NUMBER - 1)).Take(PAGE_SIZE);

                return result;
            }
            if (keyword == null && sortBy != null)
            {
                result = Sort(sortBy, PAGE_SIZE, PAGE_NUMBER);
                return result;
            }
            if (keyword != null && sortBy != null)
            {
                result = SortSearch(keyword, sortBy, PAGE_SIZE, PAGE_NUMBER);
                return result;
            }
            return _dbSet.Where(x => x.ID == 0);
        }
        #endregion

        #region GetById
        public async Task<User> GetById(long id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
        #endregion

        #region AddUser
        public async Task<User> Add(User user)
        {
            var result = await CheckExist(user.UserName, user.Email);
            if (result == null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }

            return null;


        }
        #endregion

        #region ChangeRole
        public async Task<User> ChangleRole(long id, long IdRole)
        {
            var exist = await _dbSet.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (exist != null && exist.IdRole > 0)
            {
                exist.IdRole = IdRole;
            }
            return exist;
        }
        #endregion

        #region DeActivate       
        public async Task<bool> DeActivate(long id)
        {
            var user = await _dbSet.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (user == null)
                return false;
            if (user.Status == 0)
            {
                user.Status = 1;
            }
            else
                user.Status = 0;
            _dbSet.Update(user);
            _unitOfWork.commitAsync();
            return true;
        }
        #endregion

        #region EditUser
        public async Task<User> Edit(long id, string Fullname, string Email, DateTime Dob, string Address, char Gender, int Status, long IdRole)
        {
            var exist = await _dbSet.Where(x => x.ID == id).FirstOrDefaultAsync();
            var result = await CheckExist(Email);
            if (exist != null && result == null)
            {
                exist.FullName = Fullname;
                exist.Email = Email;
                exist.DateOfBirth = Dob;
                exist.Gender = Gender;
                exist.Address = Address;
                exist.Status = Status;
                exist.IdRole = IdRole;
            }
            return exist;
        }
        #endregion

        #region DeleteUser
        public async Task<bool> Delete(long id)
        {
            var exist = await _dbSet.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (exist != null)
            {
                _dbSet.Remove(exist);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region FindByOtp
        public async Task<User> FindByOtp(string Otp)
        {
            return await _dbSet.Where(x => x.ResetPasswordOtp == Otp).FirstOrDefaultAsync();
        }
        #endregion

        #region Functions
        private async Task<User> CheckExist(string username, string email)
        {
            var result = await _dbSet.Where(x => x.UserName == username || x.Email == email).FirstOrDefaultAsync();
            return result;
        }
        private async Task<User> CheckExist(string email)
        {
            var result = await _dbSet.Where(x => x.Email == email).FirstOrDefaultAsync();
            return result;
        }
        private IQueryable<User> Sort(List<string> sortby, int PAGE_SIZE, int PAGE_NUMBER)
        {           
            String sort = "";
            foreach (var item in sortby)
            {
                sort = sort + ", " + item;
            }
            sort = sort.TrimStart(',');
            sort = sort.TrimStart(' ');
            return _dbContext.Users.AsQueryable().OrderBy(sort).Skip(PAGE_SIZE * (PAGE_NUMBER - 1)).Take(PAGE_SIZE);

        }
        private IQueryable<User> SortSearch(string keyword, List<string>? sortby, int PAGE_SIZE, int PAGE_NUMBER)
        {           
            String sort = "";
            foreach (var item in sortby)
            {
                sort = sort + ", " + item;
            }
            sort = sort.TrimStart(',');
            sort = sort.TrimStart(' ');
            return _dbContext.Users.Where(x => x.FullName.Contains(keyword)).AsQueryable().OrderBy(sort).Skip(PAGE_SIZE * (PAGE_NUMBER - 1)).Take(PAGE_SIZE);
          
        }
        #endregion

        #region InvalidateToken
        public async Task<bool> InvalidateAccessToken(string userName)
        {
            //var result = await _dbSet.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            //if (result != null)
            //{
            //    result.AccessToken = null;
            //    return true;
            //}
            return false;
        }
        #endregion

        #region Other groups
        public User GetUser(long id)
        {
            return _dbSet.FirstOrDefault(u => u.ID == id);
        }

        public User GetUserById(long UserId)
        {
            return _dbSet.Where(u => u.ID == UserId).FirstOrDefault();
        }
        public User Get(long id)
        {
            return _dbSet.FirstOrDefault(x => x.ID == id);
        }

        public async Task<IEnumerable<User>> GetTrainers()
        {
            return await _dbSet.Include(u => u.Role).Where(t => t.Role.Name.Equals("Trainer")).ToListAsync();
        }

        #region Group 5 - Authentication & Authorization
        public User GetUser(string username)
        {
            return this._dbSet.FirstOrDefault(x => x.UserName.Equals(username));
        }
        public List<User> GetUsers(long id)
        {
            return _dbSet.Where(u => u.ID == id).ToList();
        }
        #endregion

        public async Task<User> GetUserAsync(long userId)
        {
            return await _dbSet.Include(x => x.Role).FirstOrDefaultAsync(x => x.ID == userId);
        }

        // Team6
        public User GetUser(string username, string password)
        {
            var result = _dbSet.FirstOrDefault(x => x.Email.Equals(username));

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, result.Password);

            if (isValidPassword)
            {
                return result;
            }
            return null;

        }

        public List<User> GetUsersForImport()
        {
            return this._dbSet.ToList();
        }

        //team4
        public void CreateUserForImport(User user)
        {
            _dbSet.Add(user);
            _dbContext.SaveChanges();
            return;
        }       
        #endregion
    }
}
