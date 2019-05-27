﻿using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using NoteApp.BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer
{
	public class AccountService : IAuthService
	{
		private readonly NoteAppDbContext _db;

		public AccountService(NoteAppDbContext context)
		{
			_db = context;
		}

		public async Task<bool> UserCheck(IUserModel model)
		{
			var chek = false;
			var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
			if (user != null)
				chek = true;
			return chek;
		}
		public async Task<User> GetUser(IUserModel model)
		{			
			return await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);		
		}
		public async Task SetUser(IUserModel model)
		{
			var newUser = new User() { Email = model.Email, Password = model.Password };
			_db.Users.Add(newUser);
			await _db.SaveChangesAsync();
		}

	}
}