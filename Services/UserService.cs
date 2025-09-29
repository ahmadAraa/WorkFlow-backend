using Data;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   
    public class UserService
    {
    private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();
    
        public UserService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;    
            
        }
        public void AddUser(UserVM userVM)
        {
            var user = new User()
            {
                Name = userVM.Name,
                Email = userVM.Email,
            };
            user.Password = _hasher.HashPassword(user,userVM.Password);
            _dbContext.users.Add(user);
            _dbContext.SaveChanges();
               
        }
        public User? Login(UserLoginVM loginVM)
        {
            var user = _dbContext.users.FirstOrDefault(u=>u.Email == loginVM.Email);
            if (user == null) return null;
            var result = _hasher.VerifyHashedPassword(user, user.Password, loginVM.Password);
            if (result == PasswordVerificationResult.Failed) return null;
            return user;
        }
        
    }
}
