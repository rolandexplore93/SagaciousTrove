using System;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using Utility;

namespace Data.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Password { get; set; }

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;

            UserName = _configuration["Admin:UserName"];
            Email = _configuration["Admin:Email"];
            Name = _configuration["Admin:Name"];
            PhoneNumber = _configuration["Admin:PhoneNumber"];
            StreetAddress = _configuration["Admin:StreetAddress"];
            City = _configuration["Admin:City"];
            State = _configuration["Admin:State"];
            PostalCode = _configuration["Admin:PostalCode"];
            Password = _configuration["Admin:Password"];
        }

        public void Initialize()
        {
            // apply migrations if they are not yet applied
            if (_applicationDbContext.Database.GetPendingMigrations().Count() > 0)
            {
                _applicationDbContext.Database.Migrate();
            }
            // create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi)).GetAwaiter().GetResult();

                // if roles are not created, also create an admin user
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = UserName,
                    Email = Email,
                    Name = Name,
                    PhoneNumber = PhoneNumber,
                    StreetAddress = StreetAddress,
                    City = City,
                    State = State,
                    PostalCode = PostalCode,
                }, Password).GetAwaiter().GetResult();

                ApplicationUser user = _applicationDbContext.ApplicationUsers.FirstOrDefault(u => u.Email == Email);
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                user.EmailConfirmed = true;
                _applicationDbContext.SaveChanges();
            }
            return;
        }
    }
}

