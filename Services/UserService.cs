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
        private readonly IRepositoryInterface<User> _repository;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();
    
        public UserService(IRepositoryInterface<User> repository)
        {
            this._repository= repository;    
            
        }
        public async Task AddUser(UserVM userVM)
        {
            var user = new User()
            {
                Name = userVM.Name,
                Email = userVM.Email,
            };
            user.Password = _hasher.HashPassword(user,userVM.Password);
            await _repository.AddAsync(user);
           await _repository.SaveChangesAsync();
               
        }
        public async Task<User?> Login(UserLoginVM loginVM)
        {
            var user = await _repository.GetUserByEmail(loginVM.Email);
            if (user == null) return null;
            var result =  _hasher.VerifyHashedPassword(user, user.Password, loginVM.Password);
            if (result == PasswordVerificationResult.Failed) return null;
            return user;
        }
        
    }
}
